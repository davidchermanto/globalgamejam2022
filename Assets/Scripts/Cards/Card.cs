using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SavedCard
{
    public string label;
    public int id;
    public int damage;
}

[System.Serializable]
public class Card
{
    [Header("Dependency")]
    [SerializeField] protected GameManager gameManager;
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

    public bool used;

    public virtual void Setup()
    {
        
    }

    public virtual void SetupDependencies(Sprite icon, PlayerManager playerManager, GameManager gameManager)
    {
        this.playerManager = playerManager;
        this.gameManager = gameManager;
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

    public virtual void AddDamage(int damage)
    {
        this.damage += damage;
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

    public virtual string GetTitle()
    {
        return cardName;
    }

    public virtual bool GetIsSingleTarget()
    {
        return singleTarget;
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
