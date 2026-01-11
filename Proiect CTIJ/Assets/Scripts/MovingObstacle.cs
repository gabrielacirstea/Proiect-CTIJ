using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;
    public float despawnDistance = -15f;

    private void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        if (transform.position.x < despawnDistance)
        {
            Destroy(gameObject);
        }
    }

}
