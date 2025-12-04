using UnityEngine;

public class SpikeSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject spikePrefab;
    public GameObject checkpointPrefab; // NEW: Slot for the checkpoint

    [Header("Level Settings")]
    public float levelLength = 50f;
    public float startX = 5f;

    [Header("Difficulty / Spacing")]
    public float minGap = 2.0f; 
    public float maxGap = 5.0f; 

    [Header("Vertical Positions")]
    public float groundY = 0.788f; 
    public float ceilingY = 4.21f; 

    void Start()
    {
        GenerateLevel();
        SpawnCheckpoints(); // NEW: Call the function to spawn checkpoints
    }

    void GenerateLevel()
    {
        float currentX = startX;

        while (currentX < levelLength)
        {
            float gap = Random.Range(minGap, maxGap);
            currentX += gap;

            if (currentX >= levelLength) break;

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
    }

    // --- NEW FUNCTION TO PLACE CHECKPOINTS ---
    void SpawnCheckpoints()
    {
        if (checkpointPrefab == null) return;

        // Calculate positions
        float firstThird = levelLength / 3f;
        float secondThird = (levelLength / 3f) * 2;

        // Spawn Checkpoint 1
        Instantiate(checkpointPrefab, new Vector3(firstThird, groundY, 0), Quaternion.identity);

        // Spawn Checkpoint 2
        Instantiate(checkpointPrefab, new Vector3(secondThird, groundY, 0), Quaternion.identity);
    }
}