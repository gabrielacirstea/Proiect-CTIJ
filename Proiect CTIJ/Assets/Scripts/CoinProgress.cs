using UnityEngine;
using UnityEngine.UI;

public class CoinProgress : MonoBehaviour
{
    public static CoinProgress Instance { get; private set; }

    [Header("UI")]
    public Slider progressBar;

    [Header("Progress Settings")]
    public int targetCoins = 50;

    private int coinsCollected = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
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
    }

    public void AddCoin(int amount)
    {
        coinsCollected += amount;

        if (progressBar != null)
            progressBar.value = coinsCollected;
    }

    public void ResetProgress()
{
    coinsCollected = 0;
    if (progressBar != null)
        progressBar.value = 0;
}

}
