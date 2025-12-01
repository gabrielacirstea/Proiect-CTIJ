using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Update the player's respawn position
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                player.SetCheckpoint(transform.position);
                GetComponent<SpriteRenderer>().color = Color.green; // Visual feedback
            }
        }
    }
}