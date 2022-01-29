using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct EnemyStruct
{
    public string label;

    public Sprite display;
    public int health;
    public int attack;
    public bool isLight;

    public float healthPerFloor;
    public float attackPerFloor;
}

public class EnemyDatabase : MonoBehaviour
{
    [SerializeField] public List<EnemyStruct> enemies;
    [SerializeField] public List<Sprite> enemySprites;

    public void Setup()
    {

    }

    public Enemy GetEnemy(string label)
    {
        Enemy enemy = null;

        switch (label)
        {
            case "biter_light":
                enemy = new Enemy_Biter_Light();
                break;
            case "biter_dark":
                enemy = new Enemy_Biter_Dark();
                break;
            default:
                Debug.LogError("Not found label: " + label);
                break;
        }

        return enemy;
    }

    public EnemyStruct GetEnemyStruct(int floor, string label)
    {
        float difficultyMultiplier = 1.1f;

        EnemyStruct enemyStruct = new EnemyStruct();

        foreach(EnemyStruct enemy in enemies)
        {
            if (enemy.label.Equals(label))
            {
                enemyStruct = enemy;
                break;
            }
        }

        /*if(floor == 0)
        {
            floor = 1;
        }*/

        enemyStruct.health = Mathf.FloorToInt((enemyStruct.health + Mathf.FloorToInt(enemyStruct.healthPerFloor * floor)) * (Mathf.Pow(difficultyMultiplier, floor)));
        enemyStruct.attack = Mathf.FloorToInt((enemyStruct.attack + Mathf.FloorToInt(enemyStruct.attackPerFloor * floor)) * (Mathf.Pow(difficultyMultiplier, floor)));

        return enemyStruct;
    }

    public Sprite GetEnemySprite(string label)
    {
        Sprite sprite = null;

        switch (label)
        {
            case "biter_light":
                sprite = enemySprites[0];
                break;
            case "biter_dark":
                sprite = enemySprites[1];
                break;
            default:
                Debug.LogError("Label not valid: " + label);
                break;
        }

        return sprite;
    }
}
