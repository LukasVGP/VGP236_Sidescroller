using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Image healthBar;
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private TextMeshProUGUI pointsText;

    [Header("Health Bar Visuals")]
    [SerializeField] private Sprite healthBarFull;
    [SerializeField] private Sprite healthBarEmpty;

    public void UpdateHealthBar(float fillAmount)
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = fillAmount;
        }
    }

    public void UpdateAmmoText(int currentAmmo)
    {
        if (ammoText != null)
        {
            ammoText.text = "Ammo: " + currentAmmo.ToString();
        }
    }

    public void UpdatePointsText(int currentPoints)
    {
        if (pointsText != null)
        {
            pointsText.text = "Points: " + currentPoints.ToString();
        }
    }
}