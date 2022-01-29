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

    [Header("Combat")]
    public bool isPlayerTurn;
    [SerializeField] private int maxHealth;
    [SerializeField] private int health;

    // Why not only 1 orb, with dark being 0 to -5?
    // Thats confusing
    [SerializeField] private int lightOrb;
    [SerializeField] private int darkOrb;

    [SerializeField] private List<Card> ownedCards;
    [SerializeField] private List<Card> activeCards;

    [Header("Effects")]
    [SerializeField] private List<GameObject> specialEffects;

    public void Setup(List<Card> initialCards)
    {
        ownedCards = initialCards;

        // todo
        activeCards = initialCards;

        cardDisplayer.AddCards(ownedCards);

        maxHealth = baseHealth;
        health = baseHealth;

        UpdateHealthBar();
    }
    // Darras
    public void DrawCards()
    {
        
    }

    public void UseCard(int cardIndex)
    {
        activeCards[cardIndex].OnUse();
    }

    public void UseCardTargetted(int cardIndex, Enemy enemy)
    {
        activeCards[cardIndex].OnUseTargetted(enemy);
    }

    public void OnAttack()
    {
        foreach (Card card in activeCards)
        {
            card.OnPlayerAttack();
        }
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

    }

    public void OnEndTurn()
    {
        foreach (Card card in activeCards)
        {
            card.OnEndTurn();
        }

        gameManager.OnEnemyTurn();
    }

    public void AddOrb(int count, bool isLight)
    {
        if (isLight)
        {
            if(darkOrb > 0)
            {
                darkOrb -= count;

                if(darkOrb < 0)
                {
                    lightOrb = -darkOrb;
                    darkOrb = 0;

                    TestOrbOverload();
                }
            }
            else
            {
                lightOrb += count;

                TestOrbOverload();
            }
        }
        else
        {
            if (lightOrb > 0)
            {
                lightOrb -= count;

                if (lightOrb < 0)
                {
                    darkOrb = -lightOrb;
                    lightOrb = 0;

                    TestOrbOverload();
                }
            }
            else
            {
                darkOrb += count;

                TestOrbOverload();
            }
        }

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
        if(lightOrb > maxOrbs)
        {
            OnOrbOverload(true);
        }

        if(darkOrb > maxOrbs)
        {
            OnOrbOverload(true);
        }
    }

    public void OnOrbOverload(bool isLight)
    {
        // lose half of current HP
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

        }
        else
        {
            // What
        }
    }

    public GameObject GetSpecialEffect(string effectType)
    {
        // TODO

        return null;
    }

    private void UpdateOrbDisplay()
    {

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

    public int GetHealth()
    {
        return health;
    }

    public void SetHealth(int health)
    {
        this.health = health;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public void SetMaxHealth(int maxHealth)
    {
        this.maxHealth = maxHealth;
    }

    public int GetLightOrb()
    {
        return lightOrb;
    }

    public int GetDarkOrb()
    {
        return darkOrb;
    }
}
