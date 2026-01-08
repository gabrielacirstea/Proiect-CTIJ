using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;
    public float despawnDistance = -15f; // When to destroy the obstacle

    private void Update()
    {
        // Move left (towards negative X)
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        // Destroy if out of screen
        if (transform.position.x < despawnDistance)
        {
            Destroy(gameObject);
        }
    }

}
