using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelComplete : MonoBehaviour
{
    public Button nextLevelButton;
    public Button mainMenuButton;

    private void Start()
    {
        if (nextLevelButton != null)
            nextLevelButton.onClick.AddListener(LoadNextLevel);
        
        if (mainMenuButton != null)
            mainMenuButton.onClick.AddListener(GoToMainMenu);
    }

    public void LoadNextLevel()
    {
        Time.timeScale = 1f; // Resume time
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        
        // Check if next level exists
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("No more levels! Going to main menu.");
            GoToMainMenu();
        }
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f; // Resume time
        SceneManager.LoadScene("MainMenu");
    }
}
