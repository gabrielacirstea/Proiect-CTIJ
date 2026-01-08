using UnityEngine;
using UnityEngine.UI;

public class CoinProgress : MonoBehaviour
{
    public static CoinProgress Instance { get; private set; }

    [Header("UI")]
    public Slider progressBar;
    public GameObject levelCompletePanel;

    [Header("Progress Settings")]
    public int targetCoins = 10;

    private int coinsCollected = 0;
    private int checkpointCoins = 0;
    private bool levelCompleted = false;

    private void Awake()
    {
        // Allow one instance per scene (no persistence across scenes)
        Instance = this;
    }

    private void Start()
    {
        if (progressBar != null)
        {
            progressBar.minValue = 0;
            progressBar.maxValue = targetCoins;
            progressBar.value = 0;
        }
        
        if (levelCompletePanel != null)
            levelCompletePanel.SetActive(false);
    }

    public void AddCoin(int amount)
    {
        if (levelCompleted)
            return;
            
        coinsCollected += amount;

        if (progressBar != null)
            progressBar.value = coinsCollected;
            
        // Check if level is completed
        if (coinsCollected >= targetCoins)
        {
            CompleteLevel();
        }
    }
    
    private void CompleteLevel()
    {
        levelCompleted = true;
        Time.timeScale = 0f; // Pause the game
        
        if (levelCompletePanel != null)
            levelCompletePanel.SetActive(true);
    }

    public void ResetProgress()
    {
        coinsCollected = checkpointCoins;
        if (progressBar != null)
            progressBar.value = checkpointCoins;
    }

    public void SaveCheckpoint()
    {
        checkpointCoins = coinsCollected;
    }
}
