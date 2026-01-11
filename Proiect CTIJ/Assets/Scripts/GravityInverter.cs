using UnityEngine;

public class GravityInverter : MonoBehaviour
{
    [Header("Auto Invert Settings")]
    public float invertInterval = 5f;
    public bool autoInvert = true;

    private float invertTimer = 0f;
    private PlayerController playerController;
    private Rigidbody2D rb;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
        if (playerController == null || rb == null)
        {
            Debug.LogWarning("GravityInverter: Missing PlayerController or Rigidbody2D on Player!");
        }
    }

    private void Update()
    {
        if (!autoInvert || playerController == null || rb == null)
            return;

        invertTimer += Time.deltaTime;

        if (invertTimer >= invertInterval)
        {
            InvertGravity();
            invertTimer = 0f;
        }
    }

    public void InvertGravity()
    {
        if (rb == null || playerController == null) return;

        rb.gravityScale *= -1;

        Vector3 newScale = transform.localScale;
        newScale.y *= -1;
        transform.localScale = newScale;

        playerController.ResetJumps();

        Debug.Log("✓ Gravity inverted! Timer reset.");
    }
}
