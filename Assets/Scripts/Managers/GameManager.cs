

using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform character;
    public XPManager xpManager;

    public List<GameObject> guns;

    public static GameManager Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        int level = LevelManager.SelectedLevel;
        SetupLevel(level);
    }

    void SetupLevel(int level)
    {
        switch (level)
        {
            case 1:
                SwitchGun(0);
                xpManager.SetXPThresholds(new int[] { 3, 5 });
                PositionApples(character, 2.5f);
                break;
            case 2:
                SwitchGun(0);
                xpManager.SetXPThresholds(new int[] { 4 });
                PositionApples(character, 4f);
                break;
            case 3:
                SwitchGun(0);
                xpManager.DisableXP();
                PositionApples(character, 6f);
                break;
        }
    }

    void PositionApples(Transform character, float distance)
    {
        // You can implement logic to move character or apples to desired AR distance
    }

    public void SwitchGun(int index)
    {
        for (int i = 0; i < guns.Count; i++)
        {
            guns[i].SetActive(i == index);
        }
    }
}
