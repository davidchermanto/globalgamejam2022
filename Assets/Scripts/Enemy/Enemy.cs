using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Sprite display;
    [SerializeField] private TextMeshPro healthDisplay;
    [SerializeField] private TextMeshPro attackDisplay;

    [SerializeField] private int health;
    [SerializeField] private int attack;
    [SerializeField] private bool isLight;

    public virtual void Setup(EnemyStruct enemyStruct)
    {

    }

    public virtual void OnSpawn()
    {

    }

    public virtual void OnAttack()
    {

    }

    public virtual void OnTakeDamage()
    {

    }

    public virtual void OnDie()
    {

    }
}
