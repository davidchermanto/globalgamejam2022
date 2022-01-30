using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Purify : Card
{
    public override void Setup()
    {
        cardName = "Purify";
        cardLabel = "purify";
        text = "Neutralizes all orbs.";

        damage = 0;
        orbValue = 0;

        singleTarget = false;
        passive = false;

        isLight = true;
    }

    public override void OnUse()
    {
        playerManager.NeutralizeOrb();
        AudioManager.Instance.PlayOneShot("wave");
    }
}
