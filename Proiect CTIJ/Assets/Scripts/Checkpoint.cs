using UnityEngine;
using UnityEngine.SceneManagement; // Needed to know which level we are in

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                // 1. Tell player to set respawn point (your old code)
                player.SetCheckpoint(transform.position);
                Debug.Log("✓ Checkpoint: Player respawn position saved at " + transform.position);
                
                // 2. NEW: Save the current Level Index to the disk
                int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
                PlayerPrefs.SetInt("SavedLevel", currentSceneIndex);
                PlayerPrefs.Save();
                Debug.Log("✓ Checkpoint: Level index saved: " + currentSceneIndex);
                
                // 3. Save coin progress at checkpoint
                if (CoinProgress.Instance != null)
                {
                    CoinProgress.Instance.SaveCheckpoint();
                    Debug.Log("✓ Checkpoint: Coin progress saved");
                }
                else
                {
                    Debug.LogWarning("⚠ Checkpoint: CoinProgress.Instance is NULL!");
                }

                // 4. Refill lives
                player.RefillLives();
                Debug.Log("✓ Checkpoint: Lives refilled");
                
                Debug.Log("Game Saved at Level Index: " + currentSceneIndex);

                // Visual feedback
                GetComponent<SpriteRenderer>().color = Color.green;
                GetComponent<Collider2D>().enabled = false; // Disable so we don't save twice
            }
            else
            {
                Debug.LogWarning("⚠ Checkpoint: Player has no PlayerController component!");
            }
        }
    }
}