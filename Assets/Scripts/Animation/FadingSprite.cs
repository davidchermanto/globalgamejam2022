using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingSprite : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    [SerializeField] private float holdDuration;
    [SerializeField] private float fadeDuration;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));

        StartCoroutine(Fade());
    }

    private IEnumerator Fade()
    {
        yield return new WaitForSeconds(holdDuration);

        float timer = 0;

        Color initialColor = spriteRenderer.color;
        Color targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, 0);

        while (timer < 1)
        {
            timer += Time.deltaTime / fadeDuration;

            spriteRenderer.color = Color.Lerp(initialColor, targetColor, timer);

            yield return new WaitForEndOfFrame();
        }

        Destroy(gameObject);
    }
}
