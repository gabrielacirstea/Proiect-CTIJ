using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelComplete : MonoBehaviour
{
    public Button nextLevelButton;
    public Button mainMenuButton;
    public TextMeshProUGUI completionText;

    private void Start()
    {
        if (nextLevelButton != null)
            nextLevelButton.onClick.AddListener(LoadNextLevel);
        
        if (mainMenuButton != null)
            mainMenuButton.onClick.AddListener(GoToMainMenu);

        // Play level complete sound for all levels
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayLevelCompleteSound();

        // Check if this is Level3 (build index 3)
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex == 3) // Level3 is at build index 3
        {
            HandleLevel3Completion();
        }
    }

    private void HandleLevel3Completion()
    {
        // Play level complete sound
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayLevelCompleteSound();
        
        // Hide the "Next Level" button since there's no Level4
        if (nextLevelButton != null)
            nextLevelButton.gameObject.SetActive(false);

        // Update the completion text to show game completion message
        if (completionText != null)
        {
            completionText.text = "Congrats you finished the game you are a pro!";
        }
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
