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

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayLevelCompleteSound();

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex == 3)
        {
            HandleLevel3Completion();
        }
    }

    private void HandleLevel3Completion()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayLevelCompleteSound();
        
        if (nextLevelButton != null)
            nextLevelButton.gameObject.SetActive(false);

        if (completionText != null)
        {
            completionText.text = "Congrats you finished the game you are a pro!";
        }
    }

    public void LoadNextLevel()
    {
        Time.timeScale = 1f;
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        
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
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
