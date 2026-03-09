using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; 

public class Player : MonoBehaviour
{    
    [SerializeField] private TextMeshProUGUI controlsText; // controls UI
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private bool isGrounded;
    
    public int score = 0; // score counter
    [SerializeField] private TextMeshProUGUI scoreText; // UI text
    private float scoreAcc = 0f;

    [SerializeField] private GameObject mintPrefab; // bullet
    [SerializeField] private Transform openMouth; // spawn point
    [SerializeField] private float fireRate = 1f; // delay between shots
    private float nextFireTime = 0f;
    private Animator animator;

    private Rigidbody2D rb;
    private bool isDead = false;
    [SerializeField] private TextMeshProUGUI gameOverText; // game over UI
    [SerializeField] private TextMeshProUGUI restartText; // restart UI

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        controlsText.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(false);
        restartText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!isDead) {
            // press F to shoot
            if (Input.GetKey(KeyCode.F) && Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + fireRate; // fire rate cooldown
            }

            // press space to jump
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isDead)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                isGrounded = false;
            }

            // score increases over time
            scoreAcc += Time.deltaTime * 5f;
            while (scoreAcc >= 1f)
            {
                scoreAcc -= 1f;
                score++;
                scoreText.text = "Score: " + score;
            }
        }

        // press R to restart
        if (isDead && Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void Shoot()
    {
        // shoot mint
        Instantiate(mintPrefab, openMouth.position, Quaternion.identity);
        animator.SetTrigger("Shoot");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // check if player hits enemy
        if (collision.gameObject.CompareTag("Enemy")) {
            Die();
        }

        // check if player touches ground
        if (collision.gameObject.CompareTag("Ground")) {
            isGrounded = true;
        }
    }

    void Die()
    {
        isDead = true;
        Time.timeScale = 0f;
        controlsText.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(true);
        restartText.gameObject.SetActive(true);
        Debug.Log("PLAYER DIED");
    }
}