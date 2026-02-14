using UnityEngine;

public class Mint : MonoBehaviour
{
    [SerializeField] float speed = 20f;
    private Rigidbody2D rb;

    [SerializeField] float life = 3f; // destroys after 3 seconds

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.right * speed; // always moves right

        Destroy(gameObject, life);
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject); // destroys enemy
            Destroy(gameObject); // destroys bullet
        }
    }
}
