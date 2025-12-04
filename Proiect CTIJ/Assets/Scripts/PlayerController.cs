using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 8f; 
    public int maxJumps = 2; 
    
    [Header("Difficulty Progression")] // --- NEW SECTION ---
    public float acceleration = 0.5f; // How much speed to add per second
    public float maxSpeed = 12f;      // The speed limit (so it doesn't get impossible)
    private float startingSpeed;      // Remembers your original speed

    // Internal variables
    private int jumpCount;   
    private Rigidbody2D rb;
    private bool isGrounded;
    private Vector3 respawnPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        respawnPosition = transform.position;
        jumpCount = 0;
        
        // --- NEW: Remember the speed we started with ---
        startingSpeed = moveSpeed;
    }

    void Update()
    {
        // --- NEW: Increase speed over time ---
        // We use Time.deltaTime so the increase is smooth (per second), not per frame
        if (moveSpeed < maxSpeed)
        {
            moveSpeed += acceleration * Time.deltaTime;
        }

        // 1. HORIZONTAL MOVEMENT
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // 2. JUMP
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (jumpCount < maxJumps)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
                float direction = Mathf.Sign(rb.gravityScale); 
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce * direction);
                jumpCount++;
            }
        }

        // 3. GRAVITY FLIP
        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded)
        {
            rb.gravityScale *= -1;
            Vector3 newScale = transform.localScale;
            newScale.y *= -1;
            transform.localScale = newScale;
            jumpCount = 0;
        }
    }

    public void SetCheckpoint(Vector3 newPosition)
    {
        respawnPosition = newPosition;
    }

    public void Respawn()
    {
        transform.position = respawnPosition;
        rb.linearVelocity = Vector2.zero;
        
        // --- NEW: Reset speed to normal when dying ---
        moveSpeed = startingSpeed;

        if (rb.gravityScale < 0)
        {
            rb.gravityScale *= -1;
            Vector3 newScale = transform.localScale;
            newScale.y *= -1;
            transform.localScale = newScale;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) isGrounded = false;
    }
}