using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public float decreaseRate = 5f; // Health lost per second

    private Image healthBarFill;

    void Start()
    {
        currentHealth = maxHealth;

        // Create a Canvas
        GameObject canvasObj = new GameObject("HealthCanvas");
        canvasObj.layer = LayerMask.NameToLayer("UI");
        Canvas canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasObj.AddComponent<CanvasScaler>();
        canvasObj.AddComponent<GraphicRaycaster>();

        // Create a background panel
        GameObject bgObj = new GameObject("HealthBarBackground");
        bgObj.transform.SetParent(canvasObj.transform, false);
        Image bgImage = bgObj.AddComponent<Image>();
        bgImage.color = new Color(0, 0, 0, 0.6f);

        RectTransform bgRect = bgObj.GetComponent<RectTransform>();
        bgRect.anchorMin = new Vector2(1, 0); // bottom right corner
        bgRect.anchorMax = new Vector2(1, 0);
        bgRect.pivot = new Vector2(1, 0);
        bgRect.anchoredPosition = new Vector2(-20, 20); // padding from bottom and right
        bgRect.sizeDelta = new Vector2(400, 50);

        // Create the red health bar
        GameObject barObj = new GameObject("HealthBarFill");
        barObj.transform.SetParent(bgObj.transform, false);
        healthBarFill = barObj.AddComponent<Image>();
        healthBarFill.color = Color.red;

        RectTransform barRect = barObj.GetComponent<RectTransform>();
        barRect.anchorMin = new Vector2(0, 0);
        barRect.anchorMax = new Vector2(1, 1);
        barRect.pivot = new Vector2(0, 0.5f);
        barRect.offsetMin = new Vector2(0, 0);
        barRect.offsetMax = new Vector2(0, 0);
    }

    void Update()
    {
        // Decrease health over time
        currentHealth -= decreaseRate * Time.deltaTime;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Update bar width (by scaling)
        float fillAmount = currentHealth / maxHealth;
        healthBarFill.rectTransform.localScale = new Vector3(fillAmount, 1f, 1f);
    }
}
