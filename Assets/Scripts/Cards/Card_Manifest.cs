using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Manifest : Card
{
    public override void Setup()
    {
        cardName = "Manifestation";
        cardLabel = "manifest";
        text = "Instantly gains <damage> health and an additional 50% every <orbColor> Orb. Gains <orbValue> <orbColor> Orbs.";

        damage = Random.Range(6, 8);
        orbValue = 2;

        singleTarget = false;
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

    public override void OnUse()
    {
        playerManager.OnHeal(Mathf.RoundToInt(damage * (1 + playerManager.GetLightOrb() * 0.5f)));

        playerManager.AddOrb(orbValue, isLight);

        AudioManager.Instance.PlayOneShot("wave");
    }
}
