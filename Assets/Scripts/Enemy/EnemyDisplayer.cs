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
    [SerializeField] private SpriteRenderer shadow;
    [SerializeField] private TextMeshPro healthDisplay;
    [SerializeField] private TextMeshPro attackDisplay;

    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Prefabs")]
    [SerializeField] private GameObject lightParticle;
    [SerializeField] private GameObject darkParticle;

    public void Setup(Sprite sprite, int health, int attack, bool isLight, Enemy enemy)
    {
        spriteRenderer.color = new Color(1, 1, 1, 1);
        shadow.color = new Color(0, 0, 0, 0.588f);

        boxCollider.enabled = true;

        SetSprite(sprite);
        UpdateHealth(health);
        UpdateAttack(attack);
        SetParticles(isLight);

        this.enemy = enemy;

        Idle();
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
        particles.transform.localScale = new Vector3(1, 1, 1);
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
        StartCoroutine(IdleFloating());
    }

    private IEnumerator IdleFloating()
    {
        float timer = 0;
        bool up = true;

        Vector3 initialPos = new Vector3(0, 0, 0);
        Vector3 upPos = new Vector3(0, 0.7f, 0);

        spriteRenderer.transform.localPosition = initialPos;

        while (gameObject.activeInHierarchy)
        {
            float upDuration = Random.Range(2f, 4f);

            while(timer < 1)
            {
                timer += Time.deltaTime / upDuration;

                if (up)
                {
                    spriteRenderer.transform.localPosition = Vector3.Lerp(initialPos, upPos, Mathf.SmoothStep(0, 1, timer));
                }
                else
                {
                    spriteRenderer.transform.localPosition = Vector3.Lerp(upPos, initialPos, Mathf.SmoothStep(0, 1, timer));
                }

                yield return new WaitForEndOfFrame();
            }

            timer = 0;

            if (up)
            {
                up = false;
            }
            else
            {
                up = true;
            }
        }
    }

    public void OnSpawn()
    {
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        float timer = 0;
        float timeToFadeIn = 0.4f;

        Color targetColor = spriteRenderer.color;
        Color initialColor = new Color(1, 1, 1, 0);

        while (timer < 1)
        {
            timer += Time.deltaTime / timeToFadeIn;

            spriteRenderer.color = Color.Lerp(initialColor, targetColor, timer);

            yield return new WaitForEndOfFrame();
        }
    }

    public void OnAttack()
    {
        StartCoroutine(AttackCoroutine());
    }

    private IEnumerator AttackCoroutine()
    {
        float timer = 0;
        float jumpDuration = 0.3f;

        Vector3 currentPos = transform.localPosition;
        Vector3 initialPos = new Vector3(0, 0, 0);
        Vector3 jumpPos = new Vector3(0, 2f, 0);

        CameraEffects.Instance.Shake();

        while (timer < 1)
        {
            timer += Time.deltaTime / jumpDuration;

            spriteRenderer.transform.localPosition = Vector3.Lerp(jumpPos, initialPos, timer);

            yield return new WaitForEndOfFrame();
        }
    }

    public void OnAttacked()
    {
        // display damage indicator
    }

    public void OnDie()
    {
        boxCollider.enabled = false;

        healthDisplay.SetText("-");
        attackDisplay.SetText("-");

        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        float timer = 0;
        float timeToBlack = 0.4f;
        float holdInBlack = 0.2f;
        float fadeOut = 0.4f;

        Color currentColor = spriteRenderer.color;
        Color targetColor = new Color(0, 0, 0);

        while (timer < 1)
        {
            timer += Time.deltaTime / timeToBlack;

            spriteRenderer.color = Color.Lerp(currentColor, targetColor, timer);

            yield return new WaitForEndOfFrame();
        }

        timer = 0;
        yield return new WaitForSeconds(holdInBlack);

        Color shadowColor = shadow.color;
        currentColor = spriteRenderer.color;
        targetColor = new Color(0, 0, 0, 0);

        while (timer < 1)
        {
            timer += Time.deltaTime / fadeOut;

            spriteRenderer.color = Color.Lerp(currentColor, targetColor, timer);
            shadow.color = Color.Lerp(shadowColor, targetColor, timer);

            yield return new WaitForEndOfFrame();
        }

        Destroy(particles);
    }

    public Enemy GetEnemy()
    {
        return enemy;
    }
}
