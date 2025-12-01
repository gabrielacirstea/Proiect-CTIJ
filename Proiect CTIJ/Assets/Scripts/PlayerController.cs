using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f; // Optional if you want jumping + gravity flip

    [Header("Components")]
    public Rigidbody2D rb;
    private bool isGrounded;
    private bool isGravityFlipped = false;

    void Start()
    {
        // Automatically find the Rigidbody on this object
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 1. Horizontal Movement
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // 2. NORMAL JUMP (Press W or Up Arrow) - If you want a standard jump
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && isGrounded)
        {
            // If gravity is normal (1), jump up. If flipped (-1), jump down.
            float direction = rb.gravityScale > 0 ? 1 : -1; 
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce * direction);
        }

        // 3. GRAVITY FLIP (Press Space)
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            FlipGravity();
        }
    }

    void FlipGravity()
    {
        // Invert the gravity scale (1 becomes -1, -1 becomes 1)
        rb.gravityScale *= -1;

        // Rotate the player sprite vertically so feet point to the new "down"
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * -1, transform.localScale.z);
        
        // Toggle our flag
        isGravityFlipped = !isGravityFlipped;
    }

    // Check if we are touching the ground
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}