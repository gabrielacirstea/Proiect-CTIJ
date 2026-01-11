using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 8f; 
    public int maxJumps = 2; 
    
    [Header("Difficulty Progression")]
    public float acceleration = 0.5f;
    public float maxSpeed = 12f;
    private float startingSpeed;

    [Header("Controls")]
    public bool allowManualGravityFlip = true;

    [Header("Lives")]
    public int maxLives = 3;
    [SerializeField] private int currentLives;
    [Tooltip("Optional: assign 3 UI Images (dots) to visualize lives.")]
    public Image[] lifeDots;

    private int jumpCount;   
    private Rigidbody2D rb;
    private bool isGrounded;
    private Vector3 respawnPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        respawnPosition = transform.position;
        jumpCount = 0;
        
        startingSpeed = moveSpeed;

        currentLives = maxLives;
        UpdateLivesUI();
    }

    void Update()
    {
        if (moveSpeed < maxSpeed)
        {
            moveSpeed += acceleration * Time.deltaTime;
        }

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

        if (allowManualGravityFlip && Input.GetKeyDown(KeyCode.LeftShift) && isGrounded)
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
        
        moveSpeed = startingSpeed;

        if (rb.gravityScale < 0)
        {
            rb.gravityScale *= -1;
            Vector3 newScale = transform.localScale;
            newScale.y *= -1;
            transform.localScale = newScale;
        }

        moveSpeed = startingSpeed;

        if (CoinProgress.Instance != null)
        {
            CoinProgress.Instance.ResetProgress();
        }
    }

    public void TakeHit(bool bypassLives = false)
    {
        if (bypassLives)
        {
            Respawn();
            UpdateLivesUI();
            return;
        }

        currentLives = Mathf.Max(0, currentLives - 1);
        UpdateLivesUI();

        if (currentLives <= 0)
        {
            currentLives = maxLives;
            Respawn();
            UpdateLivesUI();
        }
    }

    public void RefillLives()
    {
        currentLives = maxLives;
        UpdateLivesUI();
    }

    public void ResetJumps()
    {
        jumpCount = 0;
    }

    private void UpdateLivesUI()
    {
        if (lifeDots == null) return;
        for (int i = 0; i < lifeDots.Length; i++)
        {
            if (lifeDots[i] != null)
                lifeDots[i].enabled = i < currentLives;
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