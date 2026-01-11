using UnityEngine;

public class Hazard : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (CompareTag("Spike"))
                return;

            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                if (AudioManager.Instance != null)
                    AudioManager.Instance.PlayDamageSound();
                
                player.TakeHit(bypassLives: false);
            }
        }
    }
}