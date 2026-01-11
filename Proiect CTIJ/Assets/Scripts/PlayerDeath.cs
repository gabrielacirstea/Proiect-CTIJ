using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Spike"))
        {
            var player = GetComponent<PlayerController>();
            if (player != null)
            {
                if (AudioManager.Instance != null)
                    AudioManager.Instance.PlayDamageSound();
                
                player.TakeHit(bypassLives: true);
                Debug.Log("✓ Spike hit! Instant reset to checkpoint.");
            }
            else
            {
                Debug.LogWarning("⚠ Player has no PlayerController component!");
            }
        }
    }
}
