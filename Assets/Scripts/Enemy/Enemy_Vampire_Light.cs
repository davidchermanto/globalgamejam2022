using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Vampire_Light : Enemy
{
    public override void SetColor()
    {
        isLight = true;
    }

    public override void OnAttack()
    {
        if (isAlive())
        {
            playerManager.TakeDamage(attack, isLight);
            enemyDisplayer.OnAttack();
            enemyDisplayer.SpawnIndicator(attack, true);

            attack++;
            health += Mathf.Min(attack / 4, 2);
            enemyDisplayer.UpdateAttack(attack);
            enemyDisplayer.UpdateHealth(health);

            AudioManager.Instance.PlayOneShot("hitlight");
        }
    }
}
