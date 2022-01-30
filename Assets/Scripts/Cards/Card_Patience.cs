using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Patience : Card
{
    public override void Setup()
    {
        cardName = "Patience";
        cardLabel = "patience";
        text = "Deals <damage> Damage to 1 target, then increases this Card's damage by 1. Gains <orbValue> <orbColor> Orbs.";

        damage = Random.Range(7, 10);
        orbValue = 2;

        singleTarget = true;
        passive = false;

        if (Random.Range(0, 2) == 0)
        {
            isLight = true;
        }
        else
        {
            isLight = false;
        }
    }

    public override void OnUseTargetted(Enemy enemy)
    {
        // deal damage
        enemy.OnTakeDamage(damage, isLight);
        damage += 3;

        playerManager.AddOrb(orbValue, isLight);

        // do visual effects
        playerManager.DisplayAttackEffect(enemy.GetEnemyDisplayer(), "cross");

        AudioManager.Instance.PlayOneShot("hitheavy");
        CameraEffects.Instance.Shake();
    }
}
