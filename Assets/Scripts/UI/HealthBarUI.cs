using UnityEngine;
using UnityEngine.UI;
using Platformer.Mechanics;

public class HealthBarUI : MonoBehaviour
{
    public Health health;
    public Image fillImage;

    void Update()
    {
        if (health != null)
            fillImage.fillAmount = health.HealthPercent;
    }
}
