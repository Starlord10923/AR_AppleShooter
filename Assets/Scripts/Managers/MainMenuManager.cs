using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public List<Button> levelButtons;
    private void Start()
    {
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);
        for (int i = 0; i < levelButtons.Count; i++)
        {
            // levelButtons[i].interactable = i + 1 <= levelReached;
            levelButtons[i].interactable = true;
        }
    }

    public void LoadLevel(int level)
    {
        LevelManager.SelectedLevel = level;
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }
}
