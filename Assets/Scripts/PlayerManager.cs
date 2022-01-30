using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class PlayerManager : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private CardDisplayer cardDisplayer;

    [Header("Constants")]
    [SerializeField] private int baseHealth;
    [SerializeField] private int maxOrbs;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private List<SpriteRenderer> lightOrbs;
    [SerializeField] private List<SpriteRenderer> darkOrbs;

    [Header("Combat")]
    public bool isPlayerTurn;
    [SerializeField] private int health;

    // Why not only 1 orb, with dark being 0 to -5?
    // Thats confusing
    [SerializeField] private int lightOrb;
    [SerializeField] private int darkOrb;

    [SerializeField] private List<Card> ownedCards;
    [SerializeField] private List<Card> activeCards;

    [Header("Effects")]
    [SerializeField] private List<GameObject> specialEffects;
    [SerializeField] private CanvasGroup whiteScreen;

    public void Setup(List<Card> initialCards)
    {
        ownedCards = initialCards;
        health = baseHealth;

        UpdateHealthBar();
    }

    // Picks cards
    public void DrawCards()
    {
        int maxSize = ownedCards.Count;
        List<int> intList = new List<int>();

        for(int i = 0; i < maxSize; i++)
        {
            intList.Add(i);
        }

        while(intList.Count > 5)
        {
            intList.RemoveAt(Random.Range(0, intList.Count - 1));
        }

        // Shuffle
        for(int i = 0; i < 20; i++)
        {
            int indexOne = Random.Range(0, intList.Count - 1);
            int indexTwo = Random.Range(0, intList.Count - 1);

            int number = intList[indexOne];
            intList[indexOne] = intList[indexTwo];
            intList[indexTwo] = number;
        }

        activeCards = new List<Card>();

        foreach (int i in intList)
        {
            activeCards.Add(ownedCards[i]);
            ownedCards[i].used = false;
        }

        cardDisplayer.AddCards(activeCards);
    }

    public void DeleteAllCards()
    {
        cardDisplayer.DestroyAllCards();
        activeCards = new List<Card>();
    }

    public void UseCard(int cardIndex)
    {
        GetActiveCardByIndex(cardIndex).OnUse();
        GetActiveCardByIndex(cardIndex).used = true;

        OnCardUseAfter();
    }

    public void UseCardTargetted(int cardIndex, Enemy enemy)
    {
        GetActiveCardByIndex(cardIndex).OnUseTargetted(enemy);
        GetActiveCardByIndex(cardIndex).used = true;

        OnCardUseAfter();
    }

    private void OnCardUseAfter()
    {
        //UpdateCards();
        gameManager.EvaluateFloor();

        if (GetUnusedCards().Count == 0 && !gameManager.isEnemyTurn)
        {
            OnEndTurn();
        }
    }

    public void OnAttack()
    {
        foreach (Card card in activeCards)
        {
            card.OnPlayerAttack();
        }
    }

    public void OnHeal(int amount)
    {
        health += amount;
        UpdateHealthBar();
    }

    public void TakeDamage(int damage, bool isLight)
    {
        foreach(Card card in activeCards)
        {
            card.OnPlayerTakeDamage(damage, isLight);
        }

        health -= damage;
        UpdateHealthBar();

        if (health <= 0)
        {
            OnDefeat();
        }
    }

    public void OnDefeat()
    {
        gameManager.OnGameOver();
    }

    public void OnDefeatEnemy()
    {
        foreach(Card card in activeCards)
        {
            card.OnDefeatEnemy();
        }
    }

    public void OnStartTurn()
    {
        DrawCards();
        OnHeal(1);
    }

    public void OnEndTurn()
    {
        if (!gameManager.isEnemyTurn)
        {
            foreach (Card card in activeCards)
            {
                card.OnEndTurn();
            }

            cardDisplayer.DestroyAllCards();

            gameManager.OnEnemyTurn();
            AudioManager.Instance.PlayOneShot("turnend");
        }
    }

    public void AddOrb(int count, bool isLight)
    {
        int absoluteValue = lightOrb - darkOrb;

        if (isLight)
        {
            absoluteValue += count;
        }
        else
        {
            absoluteValue -= count;
        }

        if(absoluteValue < 0)
        {
            darkOrb = Mathf.Abs(absoluteValue);
            lightOrb = 0;
        }
        else
        {
            lightOrb = absoluteValue;
            darkOrb = 0;
        }

        TestOrbOverload();
        UpdateOrbDisplay();
    }

    public void NeutralizeOrb()
    {
        lightOrb = 0;
        darkOrb = 0;

        UpdateOrbDisplay();
    }

    private void TestOrbOverload()
    {
        if(lightOrb >= maxOrbs)
        {
            OnOrbOverload(true);
        }

        if(darkOrb >= maxOrbs)
        {
            OnOrbOverload(true);
        }
    }

    public void OnOrbOverload(bool isLight)
    {
        TakeDamage(Mathf.FloorToInt(health / 2), isLight);

        darkOrb = 0;
        lightOrb = 0;

        WhiteScreen();
        UpdateOrbDisplay();
    }

    private void WhiteScreen()
    {
        whiteScreen.alpha = 1;
        StartCoroutine(FadeWhiteScreen());
    }

    private IEnumerator FadeWhiteScreen()
    {
        float timer = 0;
        float fadeTime = 1;

        yield return new WaitForSeconds(0.6f);

        while(timer < 1)
        {
            timer += Time.deltaTime / fadeTime;

            whiteScreen.alpha = Mathf.Lerp(1, 0, timer);

            yield return new WaitForEndOfFrame();
        }
    }

    public void UpdateCard(Card card)
    {
        cardDisplayer.UpdateCard(card);
    }

    public void DisplayAttackEffectAll(string effectType)
    {
        List<EnemyDisplayer> enemies = gameManager.GetEnemiesDisplayer();

        foreach(EnemyDisplayer enemy in enemies)
        {
            DisplayAttackEffect(enemy, effectType);
        }
    }

    public void DisplayAttackEffect(EnemyDisplayer enemy, string effectType)
    {
        if(enemy != null)
        {
            GameObject effect = null;

            switch (effectType)
            {
                case "slash":
                    effect = specialEffects[0];
                    break;
                case "cross":
                    effect = specialEffects[1];
                    break;
                default:
                    break;
            }

            if(effect != null)
            {
                GameObject newEffect = Instantiate(effect);
                newEffect.transform.position = enemy.transform.position;
            }
        }
        else
        {
            // What
        }
    }

    public Card GetActiveCardByIndex(int index)
    {
        foreach(Card card in activeCards)
        {
            if(card.displayId == index)
            {
                return card;
            }
        }

        return null;
    }

    public GameObject GetSpecialEffect(string effectType)
    {
        // TODO

        return null;
    }

    private void UpdateOrbDisplay()
    {
        if(lightOrb > 0)
        {
            for (int i = 0; i < darkOrbs.Count; i++)
            {
                darkOrbs[i].gameObject.SetActive(false);
            }

            for (int i = 0; i < lightOrbs.Count; i++)
            {
                if (i < lightOrb)
                {
                    lightOrbs[i].gameObject.SetActive(true);
                }
                else
                {
                    lightOrbs[i].gameObject.SetActive(false);
                }
            }
        }
        else
        {
            for (int i = 0; i < lightOrbs.Count; i++)
            {
                lightOrbs[i].gameObject.SetActive(false);
            }

            for (int i = 0; i < darkOrbs.Count; i++)
            {
                if(i < darkOrb)
                {
                    darkOrbs[i].gameObject.SetActive(true);
                }
                else
                {
                    darkOrbs[i].gameObject.SetActive(false);
                }

            }
        }
    }

    private void UpdateHealthBar()
    {
        healthText.SetText(health.ToString());
    }

    public List<Card> GetOwnedCards()
    {
        return ownedCards;
    }

    public void AddOwnedCard(Card card)
    {
        ownedCards.Add(card);
    }

    public List<Card> GetActiveCards()
    {
        return activeCards;
    }

    public List<Card> GetUnusedCards()
    {
        List<Card> cards = new List<Card>();

        foreach(Card card in activeCards)
        {
            if (!card.used)
            {
                cards.Add(card);
            }
        }

        return cards;
    }

    public int GetHealth()
    {
        return health;
    }

    public void SetHealth(int health)
    {
        this.health = health;
    }

    public int GetLightOrb()
    {
        return lightOrb;
    }

    public int GetDarkOrb()
    {
        return darkOrb;
    }

    public string GetCardToString()
    {
        string newString = "";

        foreach(Card card in ownedCards)
        {
            newString += card.cardName + ": " + card.GetText() + "\n";
        }

        return newString;
    }
}
