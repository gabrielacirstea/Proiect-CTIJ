using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public Transform ground;
    public Transform ceiling;
    public GameObject coinPrefab;

    [Header("Spawn Settings")]
    public float spawnAheadDistance = 20f;
    public float spawnInterval = 1f;
    public float margin = 0.5f;   // distanță față de bare
    [Range(0f, 1f)]
    public float spawnChance = 0.6f;

    private float nextSpawnX;

    private void Start()
    {
        if (player == null || ground == null || ceiling == null)
        {
            Debug.LogError("CoinSpawner: Missing references!");
            enabled = false;
            return;
        }

        nextSpawnX = player.position.x + 5f;
        InvokeRepeating(nameof(TrySpawn), 0.2f, spawnInterval);
    }

    private void TrySpawn()
    {
        float targetX = player.position.x + spawnAheadDistance;

        while (nextSpawnX < targetX)
        {
            if (Random.value <= spawnChance)
            {
                float minY = ground.position.y + margin;
                float maxY = ceiling.position.y - margin;

                float y = Random.Range(minY, maxY);
                Vector3 pos = new Vector3(nextSpawnX, y, 0f);

                Instantiate(coinPrefab, pos, Quaternion.identity);
            }

            nextSpawnX += Random.Range(2.5f, 5.5f);
        }
    }
}
