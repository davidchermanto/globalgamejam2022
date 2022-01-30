using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Enemy
{
    [Header("Visual")]
    [SerializeField] protected GameManager gameManager;
    [SerializeField] protected EnemyDisplayer enemyDisplayer;

    [SerializeField] protected Sprite sprite;

    [Header("Combat")]
    [SerializeField] protected PlayerManager playerManager;

    [SerializeField] protected int health;
    [SerializeField] protected int attack;
    [SerializeField] protected bool isLight;

    public virtual void Setup(EnemyStruct enemyStruct, EnemyDisplayer enemyDisplayer, PlayerManager playerManager, Sprite sprite)
    {
        health = enemyStruct.health;
        attack = enemyStruct.attack;

        this.enemyDisplayer = enemyDisplayer;
        this.playerManager = playerManager;

        enemyDisplayer.Setup(sprite, health, attack, isLight, this);
        enemyDisplayer.OnSpawn();
    }

    public virtual void SetColor()
    {

    }

    public virtual void OnSpawn()
    {
        enemyDisplayer.OnSpawn();
    }

    public virtual void OnAttack()
    {
        if (isAlive())
        {
            playerManager.TakeDamage(attack, isLight);
            enemyDisplayer.OnAttack();
            enemyDisplayer.SpawnIndicator(attack, true);

            attack++;
            enemyDisplayer.UpdateAttack(attack);

            AudioManager.Instance.PlayOneShot("hitverylight");
        }
    }

    public virtual void OnTakeDamage(int damage, bool isLight)
    {
        health -= damage;

        enemyDisplayer.UpdateHealth(health);

        if(health > 0)
        {
            enemyDisplayer.OnAttacked();
        }
        else
        {
            enemyDisplayer.OnDie();
        }

        enemyDisplayer.SpawnIndicator(damage, false);
        enemyDisplayer.OnAttacked();
    }

    public virtual void OnDie()
    {
        enemyDisplayer.OnDie();
    }

    public EnemyDisplayer GetEnemyDisplayer()
    {
        return enemyDisplayer;
    }

    public bool isAlive()
    {
        return health > 0;
    }
}
