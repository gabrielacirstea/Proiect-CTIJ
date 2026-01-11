using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Play coin collection sound
            if (AudioManager.Instance != null)
                AudioManager.Instance.PlayCoinSound();
            
            CoinProgress.Instance.AddCoin(1);
            Destroy(gameObject);
        }
    }
}
