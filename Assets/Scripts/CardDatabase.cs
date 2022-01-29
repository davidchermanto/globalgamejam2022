using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : MonoBehaviour
{
    [SerializeField] private List<Sprite> cardSprites;

    public Card GetNewCard(string label)
    {
        Card card = new Card();

        switch (label)
        {
            case "inkslice":
                card = new Card_InkSlice();
                break;
        }

        card.Setup(GetSprite(label));

        return card;
    }

    public Sprite GetSprite(string label)
    {
        switch (label)
        {
            case "inkslice":
                return cardSprites[0];
            default:
                return cardSprites[0];
        }
    }
}
