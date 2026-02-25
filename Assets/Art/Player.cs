using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float jumpForce = 12f; // jump strength
    private Rigidbody2D rb;
    private bool isGrounded = false;

    [Header("Score")]
    public int score = 0; // score counter
    [SerializeField] private TextMeshProUGUI scoreText; // UI text
    private float scoreAcc = 0f;

    [Header("Shooting")]
    [SerializeField] private GameObject mintPrefab; // projectile prefab
    [SerializeField] private Transform openMouth; // spawn point
    [SerializeField] private float fireRate = 0.25f; // delay between shots
    private float nextFireTime = 0f;
    [SerializeField] private Animator animator; // for shooting animation

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI gameOverText; // game over UI

    private bool isDead = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (animator == null)
            animator = GetComponent<Animator>();

        // hide game over text at start
        if (gameOverText != null)
            gameOverText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isDead) return;

        // jump input
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            Jump();

        // shoot input
        if (Input.GetKey(KeyCode.F) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }

        // score
        scoreAcc += Time.deltaTime * 5f;
        while (scoreAcc >= 1f)
        {
            scoreAcc -= 1f;
            score++;
        }

        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }

    void Jump()
    {
        isGrounded = false;
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f); // reset Y velocity
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    void Shoot()
    {
        // spawn projectile
        if (mintPrefab != null && openMouth != null)
            Instantiate(mintPrefab, openMouth.position, Quaternion.identity);

        // play shooting animation
        if (animator != null)
            animator.SetTrigger("Shoot");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // check if player lands on ground
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;

        // check if player hits enemy
        if (collision.gameObject.CompareTag("Enemy"))
            Die();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // player leaves ground
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = false;
    }

    void Die()
    {
        isDead = true;
        rb.linearVelocity = Vector2.zero;

        // show game over text
        if (gameOverText != null)
            gameOverText.gameObject.SetActive(true);

        Debug.Log("PLAYER DIED");
    }
}