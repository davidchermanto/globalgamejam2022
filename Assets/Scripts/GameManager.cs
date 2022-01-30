using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private EnemyDatabase enemyDatabase;
    [SerializeField] private CardDatabase cardDatabase;

    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private CardDisplayer cardDisplayer;

    [Header("Enemy Displays")]
    [SerializeField] private EnemyDisplayer enemyLeftDisplayer;
    [SerializeField] private EnemyDisplayer enemyMidDisplayer;
    [SerializeField] private EnemyDisplayer enemyRightDisplayer;

    [SerializeField] private Enemy enemyLeft;
    [SerializeField] private Enemy enemyMid;
    [SerializeField] private Enemy enemyRight;

    [Header("Card Chooser")]
    [SerializeField] private Card leftCard;
    [SerializeField] private Card rightCard;

    [Header("Other")]
    public bool isEnemyTurn;

    [SerializeField] public List<string> possibleCards;
    [SerializeField] public List<string> possibleEnemies;

    [SerializeField] private TextMeshProUGUI wave;
    [SerializeField] private int currentFloor;

    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private TextMeshProUGUI waveGameOver;

    [SerializeField] private GameObject bag;
    [SerializeField] private TextMeshProUGUI bagText;

    void Start()
    {
        cardDisplayer.Setup();
        enemyDatabase.Setup();

        playerManager.Setup(GenerateInitialCards());
        wave.SetText("WAVE - " + currentFloor.ToString());

        OnPlay();
    }

    private List<Card> GenerateInitialCards()
    {
        List<Card> initialCards = new List<Card>();
        
        string label = "inkslice";

        Card card = cardDatabase.GetNewCard(label);
        card.Setup();
        card.SetupDependencies(cardDatabase.GetSprite(label), playerManager, this);

        initialCards.Add(card);
        
        card = cardDatabase.GetNewCard(label);
        card.Setup();
        card.SetupDependencies(cardDatabase.GetSprite(label), playerManager, this);

        initialCards.Add(card);

        card = cardDatabase.GetNewCard(label);
        card.Setup();
        card.SetupDependencies(cardDatabase.GetSprite(label), playerManager, this);

        initialCards.Add(card);

        label = "inksplash";

        card = cardDatabase.GetNewCard(label);
        card.Setup();
        card.SetupDependencies(cardDatabase.GetSprite(label), playerManager, this);

        initialCards.Add(card);

        label = "inspire";

        card = cardDatabase.GetNewCard(label);
        card.Setup();
        card.SetupDependencies(cardDatabase.GetSprite(label), playerManager, this);

        initialCards.Add(card);

        label = "manifest";

        card = cardDatabase.GetNewCard(label);
        card.Setup();
        card.SetupDependencies(cardDatabase.GetSprite(label), playerManager, this);

        initialCards.Add(card);

        return initialCards;
    }

    public void OnPlay()
    {
        // draw player cards?
        playerManager.DrawCards();

        // generate 3 enemies
        GenerateThreeEnemies();

        // start player turn
        OnPlayerTurn();
    }

    public void OnPlayerTurn()
    {
        playerManager.isPlayerTurn = true;
    }

    public void OnEnemyTurn()
    {
        StartCoroutine(EnemyTurnCoroutine());
    }

    public void OnGameOver()
    {
        gameOverCanvas.SetActive(true);

        enemyLeftDisplayer.gameObject.SetActive(false);
        enemyMidDisplayer.gameObject.SetActive(false);
        enemyRightDisplayer.gameObject.SetActive(false);

        waveGameOver.SetText("WAVE - " + currentFloor.ToString());
    }

    public void OnOpenBag()
    {
        bag.SetActive(true);
        bagText.SetText(playerManager.GetCardToString());
    }

    public void OnCloseBag()
    {
        bag.SetActive(false);
    }

    private IEnumerator EnemyTurnCoroutine()
    {
        isEnemyTurn = true;

        float delayPerEnemy = 0.75f;

        List<Enemy> enemies = GetLivingEnemies();
        int enemyLooped = 0;

        yield return new WaitForSeconds(delayPerEnemy * 3);

        while (enemyLooped != enemies.Count)
        {
            enemies[enemyLooped].OnAttack();

            yield return new WaitForSeconds(delayPerEnemy);

            enemyLooped++;
        }

        isEnemyTurn = false;
        playerManager.OnStartTurn();
    }

    public void EvaluateFloor()
    {
        if(GetLivingEnemies().Count == 0)
        {
            isEnemyTurn = true;
            OnClearFloor();
        }
    }

    public void OnClearFloor()
    {
        wave.SetText("WAVE - " + currentFloor.ToString());
        playerManager.DeleteAllCards();

        if (Random.Range(0, 2) == 0)
        {
            AudioManager.Instance.PlayOneShot("battle1");
        }
        else
        {
            AudioManager.Instance.PlayOneShot("battle2");
        }

        StartCoroutine(ClearAnimation());
    }

    private IEnumerator ClearAnimation()
    {
        currentFloor++;

        // offer player a new card
        yield return new WaitForSeconds(1);

        isEnemyTurn = false;

        OfferCards();
    }

    public void OfferCards()
    {
        string label = possibleCards[Random.Range(0, possibleCards.Count)];

        leftCard = cardDatabase.GetNewCard(label);
        leftCard.Setup();
        leftCard.SetupDependencies(cardDatabase.GetSprite(label), playerManager, this);

        label = possibleCards[Random.Range(0, possibleCards.Count)];

        rightCard = cardDatabase.GetNewCard(label);
        rightCard.Setup();
        rightCard.SetupDependencies(cardDatabase.GetSprite(label), playerManager, this);

        cardDisplayer.OnChooseCard(leftCard, rightCard);
    }

    public void ChooseLeftCard()
    {
        AudioManager.Instance.PlayOneShot("turnend");

        playerManager.AddOwnedCard(leftCard);

        cardDisplayer.CloseChooser();

        OnPlay();
    }

    public void ChooseRightCard()
    {
        AudioManager.Instance.PlayOneShot("turnend");

        playerManager.AddOwnedCard(rightCard);

        cardDisplayer.CloseChooser();

        OnPlay();
    }

    public void GenerateThreeEnemies()
    {
        enemyLeft = GenerateEnemy("left");
        enemyMid = GenerateEnemy("mid");
        enemyRight = GenerateEnemy("right");
    }

    public Enemy GenerateEnemy(string direction)
    {
        string label = possibleEnemies[Random.Range(0, Mathf.Min(currentFloor + 2, 10))];

        Enemy enemy = enemyDatabase.GetEnemy(label);
        EnemyStruct enemyStruct = enemyDatabase.GetEnemyStruct(currentFloor, label);

        enemy.SetColor();

        switch (direction)
        {
            case "left":
                enemy.Setup(enemyStruct, enemyLeftDisplayer, playerManager, enemyStruct.display);
                break;
            case "mid":
                enemy.Setup(enemyStruct, enemyMidDisplayer, playerManager, enemyStruct.display);
                break;
            case "right":
                enemy.Setup(enemyStruct, enemyRightDisplayer, playerManager, enemyStruct.display);
                break;
        }

        return enemy;
    }

    public List<Enemy> GetEnemies()
    {
        List<Enemy> list = new List<Enemy>()
        {
            enemyLeft,
            enemyMid,
            enemyRight
        };

        return list;
    }

    public List<Enemy> GetLivingEnemies()
    {
        List<Enemy> list = new List<Enemy>();

        if (enemyLeft.isAlive())
        {
            list.Add(enemyLeft);
        }

        if (enemyMid.isAlive())
        {
            list.Add(enemyMid);
        }

        if (enemyRight.isAlive())
        {
            list.Add(enemyRight);
        }

        return list;
    }

    public List<EnemyDisplayer> GetEnemiesDisplayer()
    {
        List<EnemyDisplayer> list = new List<EnemyDisplayer>()
        {
            enemyLeftDisplayer,
            enemyMidDisplayer,
            enemyRightDisplayer
        };

        return list;
    }

    public int GetFloor()
    {
        return currentFloor;
    }
}
