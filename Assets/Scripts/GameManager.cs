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
    [SerializeField] public List<string> possibleCards;
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

        initialCards.Add(cardDatabase.GetNewCard("inkslice"));
        initialCards.Add(cardDatabase.GetNewCard("inkslice"));
        initialCards.Add(cardDatabase.GetNewCard("inkslice"));
        initialCards.Add(cardDatabase.GetNewCard("inkslice"));
        initialCards.Add(cardDatabase.GetNewCard("inkslice"));

        return initialCards;
    }

    public void OnPlay()
    {
        // give 8 cards to player

        // generate 3 enemies


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
        float timer = 0;
        float delayPerEnemy = 0.5f;

        List<Enemy> enemies = GetEnemies();
        int enemyLooped = 0;

        while(enemyLooped != 3)
        {
            while(timer < 1)
            {
                timer += Time.deltaTime / delayPerEnemy;

                enemies[enemyLooped].OnAttack();

                yield return new WaitForEndOfFrame();
            }

            timer = 0;
            enemyLooped++;
        }

        playerManager.OnStartTurn();
    }

    public void OnClearFloor()
    {
        currentFloor++;

        // play mountain animation for 3 secs

        // generate new enemies
    }

    public void GenerateThreeEnemies()
    {

    }

    public void GenerateEnemy()
    {

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
