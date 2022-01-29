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
            case "inksplash":
                card = new Card_InkSplash();
                break;
            case "inspire":
                card = new Card_Inspire();
                break;
        }

        return card;
    }

    public Sprite GetSprite(string label)
    {
        switch (label)
        {
            case "inkslice":
                return cardSprites[0];
            case "inksplash":
                return cardSprites[1];
            case "inspire":
                return cardSprites[2];
            case "manifest":
                return cardSprites[3];
            default:
                return cardSprites[0];
        }
    }
}
