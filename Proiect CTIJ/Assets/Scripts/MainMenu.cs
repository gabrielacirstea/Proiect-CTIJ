using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Needed for Button interaction

public class MainMenu : MonoBehaviour
{
    [Header("UI References")]
    public GameObject optionsPanel;
    public Button resumeButton;

    void Start()
    {
        // Check if we have a saved game. If not, make the Resume button non-clickable.
        if (!PlayerPrefs.HasKey("SavedLevel"))
        {
            resumeButton.interactable = false; // Greys out the button
        }
    }

    public void NewGame()
    {
        // Optional: Delete old save if starting new
        // PlayerPrefs.DeleteKey("SavedLevel"); 
        
        // Load the first level (Index 1 in Build Settings)
        SceneManager.LoadScene(1);
    }

    public void ResumeGame()
    {
        // Get the saved level index. Default to 1 if something goes wrong.
        int levelToLoad = PlayerPrefs.GetInt("SavedLevel", 1);
        SceneManager.LoadScene(levelToLoad);
    }

    public void OpenOptions()
    {
        optionsPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}