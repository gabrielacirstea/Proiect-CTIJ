using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Needed for Button interaction

public class MainMenu : MonoBehaviour
{
    [Header("UI References")]
    public GameObject resumeButton;
    public GameObject settingsButton;

    void Start()
    {
        // Hide Resume and Settings buttons
        if (resumeButton != null)
            resumeButton.SetActive(false);
        if (settingsButton != null)
            settingsButton.SetActive(false);
    }

    public void NewGame()
    {
        // Optional: Delete old save if starting new
        // PlayerPrefs.DeleteKey("SavedLevel"); 
        
        // Load the first level (Index 1 in Build Settings)
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}