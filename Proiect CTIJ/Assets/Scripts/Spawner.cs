using UnityEngine;

public class SpikeSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject spikePrefab;
    public GameObject checkpointPrefab;

    [Header("Level Settings")]
    public float levelLength = 50f;
    public float startX = 5f;

    [Header("Difficulty / Spacing")]
    public float minGap = 2.0f;
    public float maxGap = 5.0f;

    [Header("Vertical Positions")]
    public float groundY = 0.788f;
    public float ceilingY = 4.21f;

    [Header("Checkpoint Settings")]
    public int spikesPerCheckpoint = 10;

    private int spikeCount = 0;

    void Start()
    {
        GenerateLevel();
    }

    void GenerateLevel()
    {
        float currentX = startX;

        while (currentX < levelLength)
        {
            float gap = Random.Range(minGap, maxGap);
            currentX += gap;

            if (currentX >= levelLength)
                break;

            bool isCeiling = Random.value > 0.5f;
            SpawnSpike(currentX, isCeiling);
        }
    }

    void SpawnSpike(float xPos, bool onCeiling)
    {
        Vector3 position;
        Quaternion rotation;

        if (onCeiling)
        {
            position = new Vector3(xPos, ceilingY, 0);
            rotation = Quaternion.Euler(0, 0, 180);
        }
        else
        {
            position = new Vector3(xPos, groundY, 0);
            rotation = Quaternion.identity;
        }

        Instantiate(spikePrefab, position, rotation);

        spikeCount++;

        if (checkpointPrefab != null && spikeCount % spikesPerCheckpoint == 0)
        {
            Vector3 checkpointPos = new Vector3(
                xPos + 2f,
                groundY,
                0
            );

            Instantiate(checkpointPrefab, checkpointPos, Quaternion.identity);
        }
    }
}
