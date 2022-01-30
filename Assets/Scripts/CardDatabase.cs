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
            case "manifest":
                card = new Card_Manifest();
                break;
            case "voidslash":
                card = new Card_VoidSlash();
                break;
            case "thunder":
                card = new Card_Thunder();
                break;
            case "windmill":
                card = new Card_Windmill();
                break;
            case "patience":
                card = new Card_Patience();
                break;
            case "supply":
                card = new Card_Supply();
                break;
            case "purify":
                card = new Card_Purify();
                break;
            default:
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
            case "voidslash":
                return cardSprites[4];
            case "thunder":
                return cardSprites[5];
            case "windmill":
                return cardSprites[6];
            case "patience":
                return cardSprites[7];
            case "supply":
                return cardSprites[8];
            case "purify":
                return cardSprites[9];
            default:
                return cardSprites[0];
        }
    }
}
