using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Windmill : Card
{
    public override void Setup()
    {
        cardName = "Windmill";
        cardLabel = "winmill";
        text = "Deals <damage> Damage to all targets twice. Gains <orbValue> <orbColor> Orb.";

        damage = Random.Range(4, 5);
        orbValue = 2;

        singleTarget = false;
        passive = false;

        isLight = false;
    }

    public override void OnUse()
    {
        playerManager.AddOrb(orbValue, isLight);

        List<Enemy> enemies = gameManager.GetEnemies();

        foreach (Enemy enemy in enemies)
        {
            if (enemy.isAlive())
            {
                enemy.OnTakeDamage(damage, isLight);
                enemy.OnTakeDamage(damage, isLight);

                playerManager.DisplayAttackEffect(enemy.GetEnemyDisplayer(), "slash");
                playerManager.DisplayAttackEffect(enemy.GetEnemyDisplayer(), "slash");
                CameraEffects.Instance.Shake();
            }
        }

        AudioManager.Instance.PlayOneShot("hitheavy");
    }
}
