using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_InkSlice : Card
{
    public override void Setup(Sprite icon)
    {
        this.icon = icon;

        cardName = "Ink Slice";
        cardLabel = "inkslice";
        text = "Deals <damage> Damage to 1 target.";

        damage = Random.Range(4, 6);
        orbValue = 1;

        singleTarget = true;
        passive = false;
        
        if(Random.Range(0, 2) == 0)
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

        playerManager.AddOrb(orbValue, isLight);

        // do visual effects
        playerManager.DisplayAttackEffect(enemy.GetEnemyDisplayer(), "slash");
    }
}
