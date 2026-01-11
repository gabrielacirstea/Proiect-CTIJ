using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                player.SetCheckpoint(transform.position);
                Debug.Log("✓ Checkpoint: Player respawn position saved at " + transform.position);
                
                int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
                PlayerPrefs.SetInt("SavedLevel", currentSceneIndex);
                PlayerPrefs.Save();
                Debug.Log("✓ Checkpoint: Level index saved: " + currentSceneIndex);
                
                if (CoinProgress.Instance != null)
                {
                    CoinProgress.Instance.SaveCheckpoint();
                    Debug.Log("✓ Checkpoint: Coin progress saved");
                }
                else
                {
                    Debug.LogWarning("⚠ Checkpoint: CoinProgress.Instance is NULL!");
                }

                player.RefillLives();
                Debug.Log("✓ Checkpoint: Lives refilled");
                
                Debug.Log("Game Saved at Level Index: " + currentSceneIndex);

                GetComponent<SpriteRenderer>().color = Color.green;
                GetComponent<Collider2D>().enabled = false;
            }
            else
            {
                Debug.LogWarning("⚠ Checkpoint: Player has no PlayerController component!");
            }
        }
    }
}