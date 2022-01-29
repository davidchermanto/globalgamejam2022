using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class Enemy : MonoBehaviour
{
    [Header("Visual")]
    [SerializeField] protected EnemyDisplayer enemyDisplayer;

    [SerializeField] protected Sprite sprite;

    [Header("Combat")]
    [SerializeField] protected PlayerManager playerManager;

    [SerializeField] protected int health;
    [SerializeField] protected int attack;
    [SerializeField] protected bool isLight;

    public virtual void Setup(EnemyStruct enemyStruct, EnemyDisplayer enemyDisplayer, PlayerManager playerManager)
    {
        health = enemyStruct.health;
        attack = enemyStruct.attack;

        this.enemyDisplayer = enemyDisplayer;
        this.playerManager = playerManager;
    }

    public virtual void OnSpawn()
    {

    }

    public virtual void OnAttack()
    {

    }

    public virtual void OnTakeDamage(int damage, bool isLight)
    {
        health -= damage;

        enemyDisplayer.UpdateHealth(health);

        if(health >= 0)
        {
            enemyDisplayer.OnAttacked();
        }
        else
        {
            enemyDisplayer.OnDie();
        }

    }

    public virtual void OnDie()
    {

    }

    public EnemyDisplayer GetEnemyDisplayer()
    {
        return enemyDisplayer;
    }
}
