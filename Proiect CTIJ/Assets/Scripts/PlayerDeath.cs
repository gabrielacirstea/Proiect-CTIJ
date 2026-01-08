using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Spike"))
        {
            if (CoinProgress.Instance != null)
            {
                CoinProgress.Instance.ResetProgress();
            }
        }
    }
}
