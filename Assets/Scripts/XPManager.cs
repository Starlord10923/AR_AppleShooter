using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPManager : MonoBehaviour
{
    public Slider xpBar;
    public Image gunIcon;
    public Sprite[] gunSprites;

    private int currentXP = 0;
    private int currentGunIndex = 0;
    private int[] xpThresholds;
    private bool xpEnabled = true;

    public void SetXPThresholds(int[] thresholds)
    {
        xpThresholds = thresholds;
        currentXP = 0;
        xpBar.value = 0f;
        xpEnabled = true;
        UpdateGunIcon();
    }

    public void DisableXP()
    {
        xpEnabled = false;
        xpBar.value = 1f;
        gunIcon.sprite = gunSprites[2];
    }

    public void AddXP(int amount)
    {
        if (!xpEnabled || currentGunIndex >= xpThresholds.Length) return;

        currentXP += amount;
        xpBar.value = (float)currentXP / xpThresholds[currentGunIndex];

        if (currentXP >= xpThresholds[currentGunIndex])
        {
            currentGunIndex++;
            currentXP = 0;
            GameManager.Instance.SwitchGun(currentGunIndex);
            UpdateGunIcon();

            if (currentGunIndex >= xpThresholds.Length)
            {
                xpBar.value = 1f;
                xpEnabled = false;
            }
        }
    }

    void UpdateGunIcon()
    {
        gunIcon.sprite = gunSprites[currentGunIndex];
    }
}
