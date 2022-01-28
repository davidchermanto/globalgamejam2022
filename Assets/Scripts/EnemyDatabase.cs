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

    public AudioClip attackingSfx;
    public AudioClip attackedSfx;
    public AudioClip deathSfx;
}

public class EnemyDatabase : MonoBehaviour
{
    [SerializeField] public List<EnemyStruct> enemies;

    public void Setup()
    {

    }

    public EnemyStruct GetEnemy(int floor, string label)
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

        enemyStruct.health = Mathf.FloorToInt((enemyStruct.health + Mathf.FloorToInt(enemyStruct.healthPerFloor * floor)) * Mathf.Pow(difficultyMultiplier, floor));

        return enemyStruct;
    }
}
