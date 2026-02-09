using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] float forwardsSpeed = 30f;
    private Rigidbody2D rb;

    [SerializeField] public int score = 0;
    [SerializeField] public int health = 1;

    //camera
    [SerializeField] Camera cam;

    //score
    float scoreAcc = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // move player forward
        transform.Translate(forwardsSpeed * Time.deltaTime, 0, 0);

        if(Input.GetKey(KeyCode.Space)) // Spacebar
        {
            // use mint
           
        }
        
        // move camera
        cam.transform.Translate(forwardsSpeed * Time.deltaTime, 0, 0);


        // score increases with distance
        scoreAcc += forwardsSpeed * Time.deltaTime;
        if(scoreAcc >= 1f)
        {
            scoreAcc = 0f;
            score += 1;
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
}
