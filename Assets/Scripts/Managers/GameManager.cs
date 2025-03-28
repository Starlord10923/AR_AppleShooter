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
    public bool isGameOver = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        isGameOver = false;
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

    void PositionApples(Transform character, float minDistance = 2f, float maxDistance = 3f)
    {
        for (int i = 0; i < 3; i++)
        {
            float angle = Random.Range(0f, 360f);
            float distance = Random.Range(minDistance, maxDistance);

            Vector3 offset = Quaternion.Euler(0, angle, 0) * (Vector3.forward * distance);
            Vector3 pos = character.position + offset;

            // Keep apples at character's Y level
            pos.y = character.position.y;

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
        if (isGameOver) return;
        activeGun.Shoot();
    }

    public void GameOver()
    {
        isGameOver = true;
    }
}
