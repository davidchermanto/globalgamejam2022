using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Constants")]
    [SerializeField] private int baseHealth;

    [Header("Combat")]
    [SerializeField] private int health;

    [SerializeField] private int lightOrb;
    [SerializeField] private int darkOrb;

    [SerializeField] private List<Card> cards;

    public void Setup()
    {
        health = baseHealth;
    }

    public void UseCard(int cardIndex)
    {
        cards[cardIndex].OnUse();
    }

    public void UseCardTargetted(int cardIndex, Enemy enemy)
    {
        cards[cardIndex].OnUseTargetted(enemy);
    }

    public void OnAttack()
    {
        foreach (Card card in cards)
        {
            card.OnPlayerAttack();
        }
    }

    public void TakeDamage(int damage, bool isLight)
    {
        foreach(Card card in cards)
        {
            card.OnPlayerTakeDamage(damage, isLight);
        }

        health -= damage;

        if(health <= 0)
        {
            OnDefeat();
        }
    }

    public void OnDefeat()
    {

    }

    public void OnDefeatEnemy()
    {
        foreach(Card card in cards)
        {
            card.OnDefeatEnemy();
        }
    }

    public void OnFinishBattle()
    {
        foreach(Card card in cards)
        {
            card.OnFinishBattle();
        }
    }
}
