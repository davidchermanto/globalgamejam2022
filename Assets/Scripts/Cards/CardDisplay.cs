using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class CardDisplay : MonoBehaviour
{
    public int id;

    [Header("Displays")]
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI textBox;

    [SerializeField] private Image cardColor;

    [SerializeField] private TextMeshProUGUI orbValue;

    public void UpdateValues(Sprite icon, string text, int orbValue, Sprite card, int id)
    {
        this.icon.sprite = icon;
        this.textBox.SetText(text);
        this.orbValue.SetText(orbValue.ToString());
        this.cardColor.sprite = card;
        this.id = id;
    }
}
