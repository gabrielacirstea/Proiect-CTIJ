using UnityEngine;

public class Hazard : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Ignore spikes here; spikes are handled by PlayerDeath for instant reset
            if (CompareTag("Spike"))
                return;

            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                // Obstacles (red rectangles) consume a life; on zero, respawn & reset coins.
                player.TakeHit(bypassLives: false);
            }
        }
    }
}