using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Transform character;
    public XPManager xpManager;
    public List<GameObject> guns;
    public GameObject humanPrefab;

    public GameObject gameOverPanel;
    public GameObject gameWinPanel;
    public static GameManager Instance;
    public TextMeshProUGUI applesText;

    private Gun activeGun;
    public bool isGameOver = false;
    private int totalApples = 0;

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
        gameOverPanel.SetActive(false);
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
    void PositionApples(Transform character, float minDistance = 4f, float maxDistance = 6f)
    {
        totalApples = 3 + LevelManager.SelectedLevel * 2;
        Transform playerTransform = Camera.main.transform;

        // Use a forward-facing arc (e.g., 60° to 120° in front of player)
        float arcStart = -60f;
        float arcEnd = 60f;

        for (int i = 0; i < totalApples; i++)
        {
            float angleInArc = Mathf.Lerp(arcStart, arcEnd, i / (float)(totalApples - 1));
            angleInArc += Random.Range(-5f, 5f); // add spread variance

            // Generate position in world space relative to player's forward
            Quaternion rotation = Quaternion.Euler(0, angleInArc, 0);
            Vector3 direction = rotation * playerTransform.forward;

            float distance = Random.Range(minDistance, maxDistance);
            Vector3 pos = playerTransform.position + direction.normalized * distance;
            pos.y = character.position.y + 0.15f;

            GameObject target = Instantiate(humanPrefab, pos, Quaternion.identity);

            // Make the target face the character
            Vector3 toCharacter = character.position - target.transform.position;
            toCharacter.y = 0f;
            if (toCharacter != Vector3.zero)
                target.transform.rotation = Quaternion.LookRotation(toCharacter);
        }

        applesText.text = $"Apples : {totalApples}";
    }

    public void SwitchGun(int index)
    {
        for (int i = 0; i < guns.Count; i++)
        {
            guns[i].SetActive(i == index);
        }
        activeGun = guns[index].GetComponent<Gun>();
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

    public void AppleDestroyed()
    {
        Debug.Log("Apple Destroyed!");
        xpManager.AddXP(30);
        totalApples--;
        applesText.text = $"Apples : {totalApples}";

        if (totalApples <= 0 && !isGameOver)
        {
            GameWin();
        }
    }

    public void GameWin()
    {
        isGameOver = true;
        gameWinPanel.SetActive(true);
        gameWinPanel.transform.GetChild(2).GetComponentInChildren<TextMeshProUGUI>().text = LevelManager.SelectedLevel >= 3 ? "Retry" : "Next";
        Debug.Log("Game Over — All apples shot!");
    }

    public void GameOver()
    {
        isGameOver = true;
        gameWinPanel.SetActive(true);
        Debug.Log("Game Lost — Shot a human!");
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void NextLevel()
    {
        LevelManager.SelectedLevel += 1;
        SceneManager.LoadScene(1);
    }

    public void RetryLevel()
    {
        SceneManager.LoadScene(1);
    }
}
