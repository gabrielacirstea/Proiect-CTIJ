using UnityEngine;

public class SpikeSpawner : MonoBehaviour
{
    [Header("Settings")]
    public GameObject spikePrefab;
    public float levelLength = 50f;
    public float startX = 5f;

    [Header("Difficulty / Spacing")]
    // The smallest gap between spikes (Must be big enough for player to land)
    public float minGap = 2.0f; 
    // The largest gap (Keep it reasonable so the level isn't empty)
    public float maxGap = 5.0f; 

    [Header("Vertical Positions")]
    public float groundY = 0.788f; // Your specific floor value
    public float ceilingY = 4.21f; // Your specific ceiling value

    void Start()
    {
        GeneratePlayableLevel();
    }

    void GeneratePlayableLevel()
    {
        // Start generating at the safe start position
        float currentX = startX;

        // Keep adding spikes until we reach the end of the level
        while (currentX < levelLength)
        {
            // 1. Calculate the Gap
            // We add a random distance to our current position to find the NEXT spike spot.
            // This guarantees spikes never overlap and always have space between them.
            float gap = Random.Range(minGap, maxGap);
            currentX += gap;

            // If we went past the end of the level, stop spawning
            if (currentX >= levelLength) break;

            // 2. Decide: Floor or Ceiling?
            // 50/50 chance
            bool isCeiling = Random.value > 0.5f;

            // 3. Spawn the Spike
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
            rotation = Quaternion.Euler(0, 0, 180); // Point down
        }
        else
        {
            position = new Vector3(xPos, groundY, 0);
            rotation = Quaternion.identity; // Point up
        }

        Instantiate(spikePrefab, position, rotation);
    }
}