using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Beast_Dark : Enemy
{
    public override void SetColor()
    {
        isLight = false;
    }

    public override void OnAttack()
    {
        if (isAlive())
        {
            playerManager.TakeDamage(attack, isLight);
            enemyDisplayer.OnAttack();
            enemyDisplayer.SpawnIndicator(attack, true);

            attack += 2;
            enemyDisplayer.UpdateAttack(attack);

            AudioManager.Instance.PlayOneShot("hitlight");
        }
    }
}
