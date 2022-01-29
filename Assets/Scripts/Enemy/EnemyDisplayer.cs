using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class EnemyDisplayer : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private Enemy enemy;

    [Header("Display")]
    [SerializeField] private GameObject mainObject;
    [SerializeField] private GameObject particles;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private TextMeshPro healthDisplay;
    [SerializeField] private TextMeshPro attackDisplay;

    [SerializeField] private Sprite displaySprite;

    [Header("Prefabs")]
    [SerializeField] private GameObject lightParticle;
    [SerializeField] private GameObject darkParticle;

    public void Setup(Sprite sprite, int health, int attack, bool isLight, Enemy enemy)
    {
        SetSprite(sprite);
        UpdateHealth(health);
        UpdateAttack(attack);
        SetParticles(isLight);

        this.enemy = enemy;
    }

    public void SetSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }

    public void SetParticles(bool isLight)
    {
        if(particles != null)
        {
            Destroy(particles);
        }

        if (isLight)
        {
            particles = Instantiate(lightParticle);
        }
        else
        {
            particles = Instantiate(darkParticle);
        }

        particles.transform.SetParent(mainObject.transform);
        particles.transform.localPosition = new Vector3(0, 0, 0);
    }

    public void UpdateHealth(int health)
    {
        healthDisplay.SetText(health.ToString());
    }

    public void UpdateAttack(int attack)
    {
        attackDisplay.SetText(attack.ToString());
    }

    public void Idle()
    {

    }

    public void OnAttack()
    {

    }

    public void OnAttacked()
    {

    }

    public void OnDie()
    {

    }

    public Enemy GetEnemy()
    {
        return enemy;
    }
}
