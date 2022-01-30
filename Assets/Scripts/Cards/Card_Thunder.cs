using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Thunder : Card
{
    public override void Setup()
    {
        cardName = "Thunder";
        cardLabel = "thunder";
        text = "Deals <damage> Damage to 1 target twice. Gains <orbValue> <orbColor> Orbs.";

        damage = Random.Range(6, 8);
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
        enemy.OnTakeDamage(damage, isLight);
        enemy.OnTakeDamage(damage, isLight);

        playerManager.AddOrb(orbValue, isLight);

        // do visual effects
        playerManager.DisplayAttackEffect(enemy.GetEnemyDisplayer(), "slash");
        AudioManager.Instance.PlayOneShot("hitheavy");

        CameraEffects.Instance.Shake();
    }
}
