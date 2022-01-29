using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SavedCard
{
    public string label;
    public int id;
    public int damage;
}

public class Card
{
    [Header("Dependency")]
    [SerializeField] protected PlayerManager playerManager;

    public int displayId;

    [Header("Display")]
    public string cardLabel;
    public string cardName;

    [SerializeField] protected Sprite icon;

    [SerializeField] protected string text;

    [Header("Combat")]
    [SerializeField] protected int damage;
    [SerializeField] protected bool singleTarget;
    [SerializeField] protected bool isLight;
    [SerializeField] protected int orbValue;
    [SerializeField] protected bool passive;

    public virtual void Setup(Sprite icon)
    {
        this.icon = icon;
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

    public virtual void OnEndTurn()
    {

    }

    public virtual string GetText()
    {
        string orbColor;

        if (isLight)
        {
            orbColor = "Light";
        }
        else
        {
            orbColor = "Dark";
        }

        return text.Replace("<damage>", damage.ToString())
            .Replace("<orbValue>", orbValue.ToString())
            .Replace("<orbColor>", orbColor);
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

    public virtual Sprite GetIcon()
    {
        return icon;
    }
}
