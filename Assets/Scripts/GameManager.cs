using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [Header("Other")]
    public bool isEnemyTurn;

    [SerializeField] public List<string> possibleCards;
    [SerializeField] public List<string> possibleEnemies;

    [SerializeField] private int currentFloor;

    void Start()
    {
        cardDisplayer.Setup();
        enemyDatabase.Setup();

        playerManager.Setup(GenerateInitialCards());

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

        card = cardDatabase.GetNewCard(label);
        card.Setup();
        card.SetupDependencies(cardDatabase.GetSprite(label), playerManager, this);

        label = "inspire";

        card = cardDatabase.GetNewCard(label);
        card.Setup();
        card.SetupDependencies(cardDatabase.GetSprite(label), playerManager, this);

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

    private IEnumerator EnemyTurnCoroutine()
    {
        isEnemyTurn = true;

        float delayPerEnemy = 0.75f;

        List<Enemy> enemies = GetLivingEnemies();
        int enemyLooped = 0;

        yield return new WaitForSeconds(delayPerEnemy);

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
        playerManager.DeleteAllCards();
        StartCoroutine(ClearAnimation());
    }

    private IEnumerator ClearAnimation()
    {
        currentFloor++;

        // offer player a new card?

        // play mountain animation for 3 secs
        yield return new WaitForSeconds(3);

        // generate new enemies
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
        string label = possibleEnemies[Random.Range(0, 2 /*Mathf.Min(currentFloor + 2, 10)*/)];

        Enemy enemy = enemyDatabase.GetEnemy(label);
        EnemyStruct enemyStruct = enemyDatabase.GetEnemyStruct(currentFloor, label);
        Sprite enemySprite = enemyDatabase.GetEnemySprite(label);

        enemy.SetColor();

        switch (direction)
        {
            case "left":
                enemy.Setup(enemyStruct, enemyLeftDisplayer, playerManager, enemySprite);
                break;
            case "mid":
                enemy.Setup(enemyStruct, enemyMidDisplayer, playerManager, enemySprite);
                break;
            case "right":
                enemy.Setup(enemyStruct, enemyRightDisplayer, playerManager, enemySprite);
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
