using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class DamageIndicator : MonoBehaviour
{
    public void SetDamage(int damage, bool player)
    {
        GetComponent<TextMeshPro>().SetText("-" + damage.ToString());

        if (player)
        {
            // Change color to player color
            GetComponent<TextMeshPro>().color = new Color(0.15f, 0.15f, 0.18f);
        }
        else
        {
            GetComponent<TextMeshPro>().color = new Color(0.64f, 0.25f, 0.28f);
        }

        transform.position += new Vector3(Random.Range(-1f, 1f), 1.5f, 0);

        StartCoroutine(DamageIndicatorAnimation());
    }

    private IEnumerator DamageIndicatorAnimation()
    {
        //Debug.Log("Successfully activated damage indicator");
        float moveUpTime = 0.2f;
        float moveSpeed = 2f;
        float waitTime = 0.3f;
        float disappearTime = 0.1f;
        float shrinkSpeed = 6f;

        while (moveUpTime > 0f)
        {
            transform.position += new Vector3(0, moveSpeed) * Time.deltaTime;

            yield return new WaitForFixedUpdate();
            moveUpTime -= Time.deltaTime;
        }

        yield return new WaitForSeconds(waitTime);

        while (disappearTime > 0f)
        {
            transform.localScale -= Vector3.one * shrinkSpeed * Time.deltaTime;
            transform.position -= new Vector3(0, moveSpeed * shrinkSpeed * 0.64f) * Time.deltaTime;

            yield return new WaitForFixedUpdate();
            disappearTime -= Time.deltaTime;
        }

        Destroy(gameObject);
    }
}
