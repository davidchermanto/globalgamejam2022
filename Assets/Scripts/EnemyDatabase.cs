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
            case "shieldy_light":
                enemy = new Enemy_Shieldy_Light();
                break;
            case "shieldy_dark":
                enemy = new Enemy_Shieldy_Dark();
                break;
            case "vampire_light":
                enemy = new Enemy_Vampire_Light();
                break;
            case "vampire_dark":
                enemy = new Enemy_Vampire_Dark();
                break;
            case "onion_dark":
                enemy = new Enemy_Onion_Dark();
                break;
            case "onion_light":
                enemy = new Enemy_Onion_Light();
                break;
            case "beast_light":
                enemy = new Enemy_Beast_Light();
                break;
            case "beast_dark":
                enemy = new Enemy_Beast_Dark();
                break;
            default:
                Debug.LogError("Not found label: " + label);
                break;
        }

        return enemy;
    }

    public EnemyStruct GetEnemyStruct(int floor, string label)
    {
        float difficultyMultiplier = 1.04f;

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
}
