using UnityEngine;
using UnityEngine.UI;

public class DamageFlash : MonoBehaviour
{
    [SerializeField] private HealthHandler playerHealth;
    [SerializeField] private Image flashImage;
    [SerializeField] private float maxAlpha = 0.45f;
    [SerializeField] private float fadeSpeed = 2f;
    [SerializeField] private float ignoreFirstSeconds = 1f;

    private float lastHealth;
    private float alpha;
    private bool initialised;

    private void Update()
    {
        if (playerHealth == null || flashImage == null) return;

        float current = playerHealth.GetHealth();
        if (!initialised)
        {
            lastHealth = current;
            initialised = true;
        }

        bool settled = Time.timeSinceLevelLoad > ignoreFirstSeconds;
        if (settled && current < lastHealth)
        {
            alpha = maxAlpha;
        }

        lastHealth = current;

        alpha = Mathf.MoveTowards(alpha, 0f, fadeSpeed * Time.unscaledDeltaTime);
        Color c = flashImage.color;

        c.a = alpha;
        flashImage.color = c;
    }
}
