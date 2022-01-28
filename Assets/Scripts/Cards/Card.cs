using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [Header("Display")]
    [SerializeField] private Sprite icon;

    [SerializeField] private string text;

    [Header("Combat")]
    [SerializeField] private int damage;
    [SerializeField] private bool singleTarget;
    [SerializeField] private bool isLight;
    [SerializeField] private int orbValue;

    [SerializeField] private bool passive;

    public virtual void Setup()
    {

    }

    public virtual void OnUse()
    {

    }

    public virtual void OnUseTargetted(Enemy enemy)
    {

    }

    public virtual void OnPlayerAttack()
    {

    }

    public virtual void OnPlayerTakeDamage(int damage, bool isLight)
    {

    }

    public virtual void OnDefeatEnemy()
    {

    }

    public virtual void OnFinishBattle()
    {

    }

    public virtual string GetText()
    {
        return text.Replace("<damage>", damage.ToString());
    }

    public virtual int GetDamage()
    {
        return damage;
    }

    public virtual bool GetIsLight()
    {
        return isLight;
    }

    public virtual bool GetIsPassive()
    {
        return passive;
    }

    public virtual int GetOrbValue()
    {
        return orbValue;
    }
}
