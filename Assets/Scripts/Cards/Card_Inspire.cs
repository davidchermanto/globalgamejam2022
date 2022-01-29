using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Inspire : Card
{
    public override void Setup()
    {
        cardName = "Inspiration";
        cardLabel = "inspire";
        text = "Gives a random card in deck <damage> bonus damage.";

        damage = Random.Range(2, 3);
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
        playerManager.AddOrb(orbValue, isLight);

        List<Card> cards = playerManager.GetUnusedCards();

        if(cards.Count > 1)
        {
            bool valid = false;
            while (!valid)
            {
                Card randomCard = cards[Random.Range(0, cards.Count - 1)];

                if (!randomCard.cardLabel.Equals(cardLabel))
                {
                    randomCard.AddDamage(damage);
                    playerManager.UpdateCard(randomCard);

                    valid = true;
                }
            }
        }
        else
        {
            // Do nothing
        }

    }
}
