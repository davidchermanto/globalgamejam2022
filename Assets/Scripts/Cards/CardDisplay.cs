using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class CardDisplay : MonoBehaviour
{
    public int id;

    [Header("Displays")]
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI textBox;

    [SerializeField] private Image cardColor;

    [SerializeField] private TextMeshProUGUI orbValue;

    public void UpdateValues(Sprite icon, string text, int orbValue, Sprite card, int id, bool isLight, string title)
    {
        this.icon.sprite = icon;
        this.title.SetText(title);
        this.textBox.SetText(text);
        this.orbValue.SetText(orbValue.ToString());
        this.cardColor.sprite = card;
        this.id = id;

        if (isLight)
        {
            textBox.color = new Color(1, 1, 1);
            this.orbValue.color = new Color(0, 0, 0);
            this.title.color =  new Color(1, 1, 1);
        }
        else
        {
            textBox.color = new Color(0, 0, 0);
            this.orbValue.color = new Color(1, 1, 1);
            this.title.color = new Color(0, 0, 0);
        }
    }
}
