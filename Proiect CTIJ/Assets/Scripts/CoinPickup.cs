using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CoinProgress.Instance.AddCoin(1);
            Destroy(gameObject);
        }
    }
}
