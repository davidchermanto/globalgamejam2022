using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Inspire : Card
{
    public override void Setup()
    {
        cardName = "Inspiration";
        cardLabel = "inspire";
        text = "Gives a random card in deck <damage> bonus damage. Gains <orbValue> <orbColor> Orb.";

        damage = Random.Range(2, 3);
        orbValue = 1;

        singleTarget = false;
        passive = false;

        isLight = true;
    }

    public override void OnUse()
    {
        playerManager.AddOrb(orbValue, isLight);

        List<Card> cards = playerManager.GetUnusedCards();

        int tries = 0;

        if(cards.Count > 1)
        {
            bool valid = false;
            while (!valid)
            {
                Card randomCard = cards[Random.Range(0, cards.Count - 1)];

                if (!randomCard.cardLabel.Equals(cardLabel) && randomCard.GetDamage() != 0)
                {
                    randomCard.AddDamage(damage);
                    playerManager.UpdateCard(randomCard);

                    valid = true;
                }

                tries++;

                if (tries > 20)
                {
                    break;
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
