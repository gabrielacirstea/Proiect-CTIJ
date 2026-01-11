using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Prefab")]
    public GameObject obstaclePrefab;

    [Header("Spawn Area (Y only)")]
    public float minSpawnY = -3f;
    public float maxSpawnY = 3f;
    public Transform bottomBound;
    public Transform topBound;

    [Header("Spawn Timing")]
    public float spawnInterval = 1.2f;

    [Header("Spawn Position (X)")]
    public bool useSpawnerX = true;
    public float fixedSpawnX = 15f;
    
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
