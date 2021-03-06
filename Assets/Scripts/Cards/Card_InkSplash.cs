using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_InkSplash : Card
{
    public override void Setup()
    {
        cardName = "Ink Splash";
        cardLabel = "inksplash";
        text = "Deals <damage> Damage to all targets. Gains <orbValue> <orbColor> Orb.";

        damage = Random.Range(3, 4);
        orbValue = 1;

        singleTarget = false;
        passive = false;

        isLight = false;
    }

    public override void OnUse()
    {
        playerManager.AddOrb(orbValue, isLight);

        List<Enemy> enemies = gameManager.GetEnemies();

        foreach(Enemy enemy in enemies)
        {
            if (enemy.isAlive())
            {
                enemy.OnTakeDamage(damage, isLight);
                playerManager.DisplayAttackEffect(enemy.GetEnemyDisplayer(), "cross");
                CameraEffects.Instance.Shake();
            }
        }

        AudioManager.Instance.PlayOneShot("hitlight");
    }
}
