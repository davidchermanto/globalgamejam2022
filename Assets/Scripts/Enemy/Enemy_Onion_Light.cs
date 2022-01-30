using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Onion_Light : Enemy
{
    public override void SetColor()
    {
        isLight = true;
    }

    public override void OnTakeDamage(int damage, bool isLight)
    {
        health -= damage;
        health += 2;

        enemyDisplayer.UpdateHealth(health);

        if (health > 0)
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

}
