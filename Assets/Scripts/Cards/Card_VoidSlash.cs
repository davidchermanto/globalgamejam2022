using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_VoidSlash : Card
{
    public override void Setup()
    {
        cardName = "Void Slash";
        cardLabel = "voidslash";
        text = "Deals <damage> Damage to 1 target, with an additional 50% for every <orbColor> Orb. Gains <orbValue> <orbColor> Orbs.";

        damage = Random.Range(8, 10);
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
        if (isLight)
        {
            enemy.OnTakeDamage(Mathf.RoundToInt(damage * (1 + playerManager.GetLightOrb() * 0.5f)), isLight);
        }
        else
        {
            enemy.OnTakeDamage(Mathf.RoundToInt(damage * (1 + playerManager.GetDarkOrb() * 0.5f)), isLight);
        }


        playerManager.AddOrb(orbValue, isLight);

        // do visual effects
        playerManager.DisplayAttackEffect(enemy.GetEnemyDisplayer(), "slash");
        AudioManager.Instance.PlayOneShot("hitlight");

        CameraEffects.Instance.Shake();
    }
}
