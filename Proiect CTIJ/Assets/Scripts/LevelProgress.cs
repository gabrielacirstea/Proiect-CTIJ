using UnityEngine;
using UnityEngine.UI; // Required for UI elements

public class LevelProgress : MonoBehaviour
{
    [Header("References")]
    public Transform player;       // The player object to track
    public Transform endPoint;     // An empty object placed at the end of the level
    public Slider progressSlider;  // The UI Slider we just made

    private float startX;
    private float totalDistance;

    void Start()
    {
        // 1. Record where the player starts
        startX = player.position.x;

        // 2. Calculate the total length of the level
        // We use the absolute distance to handle cases where endPoint might be negative
        totalDistance = Vector3.Distance(player.position, endPoint.position);
    }

    void Update()
    {
        // 3. Calculate current distance traveled
        float currentDistance = player.position.x - startX;

        // 4. Get a percentage (0 to 1) by dividing current by total
        float progress = currentDistance / totalDistance;

        // 5. Update the slider value
        progressSlider.value = progress;
    }
}