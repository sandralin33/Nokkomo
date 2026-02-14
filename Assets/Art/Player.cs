using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // player
    [SerializeField] float forwardsSpeed = 10f;
    private Rigidbody2D rb;

    // score
    [SerializeField] public int score = 0; // actual score
    float scoreAcc = 0f; // temp counter that keeps track of how much distance the player has moved
    public TextMeshProUGUI scoreText;
    [SerializeField] public int health = 1;

    // bullet
    [SerializeField] GameObject mintPrefab;
    [SerializeField] Transform openMouth;
    [SerializeField] float fireRate = 0.25f;
    float nextFireTime = 0f;
    [SerializeField] Animator animator;

    // camera
    [SerializeField] Camera cam;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        // move player forward
        transform.Translate(forwardsSpeed * Time.deltaTime, 0, 0);

        if(Input.GetKey(KeyCode.Space) && Time.time >= nextFireTime) // Spacebar
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
            Debug.Log("space key was pressed");
        }
        
        // move camera
        cam.transform.Translate(forwardsSpeed * Time.deltaTime, 0, 0);


        // score increases with distance
        scoreAcc += forwardsSpeed * Time.deltaTime;
        if(scoreAcc >= 1f)
        {
            scoreAcc = 0f;
            score += 1;
            scoreText.text = "Score: " + score;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if(collision.compareTag == "Coin")
        // {
        //     Debug.Log("Collected");
        //     score += 1000;
        //     Destroy(collision.gameObject);
        // }

        if (collision.tag == "Enemy")
        {
            Debug.Log("Dry Mouth");
            health -= 1;
            if(health <= 0)
            {
                Debug.Log("Died"); //TODO: call end of game
            }
        }
    }

    // double check this code
    void Shoot()
    {
        // spawn bullet
        Instantiate(mintPrefab, openMouth.position, Quaternion.identity);

        // shoot animation
        animator.SetTrigger("Shoot");
    }
}
