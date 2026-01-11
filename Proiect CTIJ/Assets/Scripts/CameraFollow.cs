using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    
    [Range(1, 10)]
    public float smoothFactor = 5f;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, -10f) + offset;

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothFactor * Time.deltaTime);

        transform.position = smoothedPosition;
    }
}