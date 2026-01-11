using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Prefab")]
    public GameObject obstaclePrefab;

    [Header("Spawn Area (Y only)")]
    public float minSpawnY = -3f; // fallback if bounds not set
    public float maxSpawnY = 3f;  // fallback if bounds not set
    public Transform bottomBound; // optional: set to lower black bar transform
    public Transform topBound;    // optional: set to upper black bar transform

    [Header("Spawn Timing")]
    public float spawnInterval = 1.2f; // seconds between spawns

    [Header("Spawn Position (X)")]
    public bool useSpawnerX = true; // if true, spawn at this GameObject's X
    public float fixedSpawnX = 15f; // else, use this X value
    
    private float spawnTimer = 0f;

    private void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            SpawnObstacle();
            spawnTimer = 0f;
        }
    }

    private void SpawnObstacle()
    {
        if (obstaclePrefab == null)
        {
            Debug.LogWarning("Obstacle prefab not assigned!");
            return;
        }

        float minY = minSpawnY;
        float maxY = maxSpawnY;

        // If bounds are provided, use their Y positions (with small padding)
        if (bottomBound != null && topBound != null)
        {
            minY = Mathf.Min(bottomBound.position.y, topBound.position.y) + 0.2f;
            maxY = Mathf.Max(bottomBound.position.y, topBound.position.y) - 0.2f;
        }

        float spawnY = Random.Range(minY, maxY);
        float spawnX = useSpawnerX ? transform.position.x : fixedSpawnX;
        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0f);

        Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        float minY = minSpawnY;
        float maxY = maxSpawnY;
        if (bottomBound != null && topBound != null)
        {
            minY = Mathf.Min(bottomBound.position.y, topBound.position.y) + 0.2f;
            maxY = Mathf.Max(bottomBound.position.y, topBound.position.y) - 0.2f;
        }

        float x = useSpawnerX ? transform.position.x : fixedSpawnX;
        Vector3 a = new Vector3(x, minY, 0f);
        Vector3 b = new Vector3(x, maxY, 0f);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(a, b);
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(new Vector3(x, (minY + maxY) * 0.5f, 0f), 0.1f);
    }
#endif
}
