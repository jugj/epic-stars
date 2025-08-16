using UnityEngine;

public class FallingBlocks1 : MonoBehaviour
{
    public float fadeDelay = 1f;
    public float fadeDuration = 2f;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private BoxCollider2D col;
    private bool hasCollided = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();

        if (rb != null)
            rb.isKinematic = true; // Start als statisch
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (hasCollided) return;

        hasCollided = true;

        if (rb != null)
            rb.isKinematic = false; // Physik aktivieren
            col.isTrigger = true;

        // Starte Fading
        Invoke(nameof(StartFade), fadeDelay);
        }
        
    }

    void StartFade()
    {
        StartCoroutine(FadeAndDestroy());
    }

    private System.Collections.IEnumerator FadeAndDestroy()
    {
        
        float elapsed = 0f;
        Color originalColor = sr.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            sr.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        Destroy(gameObject);
    }
}
