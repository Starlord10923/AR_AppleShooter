using UnityEngine;
using UnityEngine.UI;

public class XPManager : MonoBehaviour
{
    public Slider xpBar;
    public Image gunIcon;
    public Sprite[] gunSprites;

    private int currentXP = 0;
    private int currentGunIndex = 0;
    private int[] xpThresholds = new int[] { 100, 150 };
    private bool xpEnabled = true;

    private const string XPKey = "CurrentXP";
    private const string GunIndexKey = "CurrentGunIndex";
    private const string MaxGunKey = "MaxUnlockedGunIndex";

    private void Start()
    {
        currentGunIndex = PlayerPrefs.GetInt(GunIndexKey, 0);
        currentXP = PlayerPrefs.GetInt(XPKey, 0);
        xpEnabled = true;

        if (currentGunIndex >= xpThresholds.Length)
        {
            xpEnabled = false;
            xpBar.value = 1f;
        }
        else
        {
            xpBar.value = (float)currentXP / xpThresholds[currentGunIndex];
        }

        UpdateGunIcon();
        GameManager.Instance.SwitchGun(currentGunIndex);
    }

    public void DisableXP()
    {
        xpEnabled = false;
        xpBar.value = 1f;
        PlayerPrefs.SetInt(MaxGunKey, gunSprites.Length - 1);
        gunIcon.sprite = gunSprites[gunSprites.Length - 1];
    }

    public void AddXP(int amount)
    {
        if (!xpEnabled || currentGunIndex >= xpThresholds.Length)
            return;

        currentXP += amount;
        xpBar.value = (float)currentXP / xpThresholds[currentGunIndex];

        PlayerPrefs.SetInt(XPKey, currentXP);

        if (currentXP >= xpThresholds[currentGunIndex])
        {
            currentGunIndex++;
            currentXP = 0;

            PlayerPrefs.SetInt(XPKey, currentXP);
            PlayerPrefs.SetInt(GunIndexKey, currentGunIndex);

            int maxUnlocked = Mathf.Max(PlayerPrefs.GetInt(MaxGunKey, 0), currentGunIndex);
            PlayerPrefs.SetInt(MaxGunKey, maxUnlocked);

            GameManager.Instance.SwitchGun(currentGunIndex);
            UpdateGunIcon();

            if (currentGunIndex >= xpThresholds.Length)
            {
                xpBar.value = 1f;
                xpEnabled = false;
            }
        }
    }

    public int GetMaxUnlockedGunIndex()
    {
        return PlayerPrefs.GetInt(MaxGunKey, 0);
    }

    void UpdateGunIcon()
    {
        gunIcon.sprite = gunSprites[Mathf.Clamp(currentGunIndex, 0, gunSprites.Length - 1)];
    }
}
