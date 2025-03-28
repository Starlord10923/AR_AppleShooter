using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform character;
    public XPManager xpManager;

    public List<GameObject> guns;
    public List<GameObject> applePrefabs;

    public static GameManager Instance;
    private Gun activeGun;

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
                PositionApples(character, 2.5f);
                break;
            case 2:
                PositionApples(character, 4f);
                break;
            case 3:
                xpManager.DisableXP();
                PositionApples(character, 6f);
                break;
        }
    }

    void PositionApples(Transform character, float distance)
    {
        for (int i = 0; i < 3; i++)
        {
            Vector3 pos = character.position + Quaternion.Euler(0, i * 120f, 0) * (character.forward * distance);
            Instantiate(applePrefabs[Random.Range(0, applePrefabs.Count)], pos, Quaternion.identity);
        }
    }

    public void SwitchGun(int index)
    {
        for (int i = 0; i < guns.Count; i++)
        {
            guns[i].SetActive(i == index);
            activeGun = guns[i].GetComponent<Gun>();
        }
    }
    public void ToggleGun()
    {
        int unlockedCount = xpManager.GetMaxUnlockedGunIndex() + 1;

        if (unlockedCount <= 1) return;

        int currentGunIndex = guns.FindIndex(g => g.activeSelf);
        int nextGunIndex = (currentGunIndex + 1) % unlockedCount;

        SwitchGun(nextGunIndex);
    }

    public void ShootGun()
    {
        activeGun.Shoot();        
    }
}
