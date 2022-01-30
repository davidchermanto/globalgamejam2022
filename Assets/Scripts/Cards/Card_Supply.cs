using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Supply : Card
{
    public override void Setup()
    {
        cardName = "Hefty Supply";
        cardLabel = "supply";
        text = "Gives all cards in deck <damage> bonus damage. Gains <orbValue> <orbColor> Orb.";

        damage = 1;
        orbValue = 3;

        singleTarget = false;
        passive = false;

        isLight = true;
    }

    public override void OnUse()
    {
        playerManager.AddOrb(orbValue, isLight);

        List<Card> cards = playerManager.GetUnusedCards();

        if (cards.Count > 1)
        {
            foreach(Card card in cards)
            {
                if(card.GetDamage() != 0)
                {
                    card.AddDamage(damage);
                    playerManager.UpdateCard(card);
                }
            }
            AudioManager.Instance.PlayOneShot("wave");
        }
        else
        {
            // Do nothing
        }
    }
}
