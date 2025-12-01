using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;       // The player we want to follow
    public Vector3 offset;         // Fine-tuning the position (optional)
    
    [Range(1, 10)]
    public float smoothFactor = 5f; // How "snappy" or "lazy" the camera is

    // We use LateUpdate because we want the camera to move AFTER
    // the player has finished its physics movement for the frame.
    void LateUpdate()
    {
        if (target == null) return; // Stop if player is destroyed

        // 1. Determine where the camera SHOULD be
        // We keep the Camera's original Z (-10) so it doesn't clip into the world
        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, -10f) + offset;

        // 2. Smoothly move from current position to target position
        // "Lerp" stands for Linear Interpolation (moving gradually between points)
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothFactor * Time.deltaTime);

        // 3. Apply the position
        transform.position = smoothedPosition;
    }
}