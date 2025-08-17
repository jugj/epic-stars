using UnityEngine;

public class FallingBlocks1 : MonoBehaviour
{
    public GameObject[] bridgeParts;
    public float fadeDelay = 1f;
    public float fadeDuration = 2f;
    public float fallDelay = 0.5f;

    private bool hasCollided = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (hasCollided) return;

            hasCollided = true;

            StartCoroutine(DestroyBridgeParts());
        }
    }

    private System.Collections.IEnumerator DestroyBridgeParts()
    {
        for (int i = 0; i < bridgeParts.Length - 1; i++)
        {
            if (bridgeParts[i] != null)
            {
                StartCoroutine(DestroyPart(bridgeParts[i]));
                yield return new WaitForSeconds(fadeDelay);
            }
        }

        DestroyLastPart();
    }

    private System.Collections.IEnumerator DestroyPart(GameObject part)
    {
        Rigidbody2D rb = part.GetComponent<Rigidbody2D>();
        SpriteRenderer sr = part.GetComponent<SpriteRenderer>();
        BoxCollider2D col = part.GetComponent<BoxCollider2D>();

        if (rb != null)
            rb.isKinematic = false;
        if (col != null)
            col.isTrigger = true;

        float elapsed = 0f;
        Color originalColor = sr.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            sr.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        Destroy(part);
    }

    void DestroyLastPart()
    {
        if (bridgeParts.Length > 0 && bridgeParts[bridgeParts.Length - 1] != null)
        {
            GameObject lastPart = bridgeParts[bridgeParts.Length - 1];
            Rigidbody2D rb = lastPart.GetComponent<Rigidbody2D>();
            SpriteRenderer sr = lastPart.GetComponent<SpriteRenderer>();
            BoxCollider2D col = lastPart.GetComponent<BoxCollider2D>();

            if (rb != null)
                rb.isKinematic = false;
            if (col != null)
                col.isTrigger = true;

            StartCoroutine(DestroyPart(lastPart));
        }
    }
}
