using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 8f; 
    public int maxJumps = 2; 
    private int jumpCount;   
    private Rigidbody2D rb;
    private bool isGrounded;
    private Vector3 respawnPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        respawnPosition = transform.position;
        jumpCount = 0;
    }

    void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

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