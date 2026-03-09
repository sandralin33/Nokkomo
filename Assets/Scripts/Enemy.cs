using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jitterAmount = 0.1f; // how strong the jitter is
    [SerializeField] float jitterSpeed = 20f;   // how fast it jitters

    void Update()
    {
        // move left
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);

        // jitter up and down
        float jitter = Mathf.Sin(Time.time * jitterSpeed) * jitterAmount;
        transform.Translate(Vector2.up * jitter * Time.deltaTime);

        // destroy if off-screen
        if (transform.position.x < -20f)
        {
            Destroy(gameObject);
        }
    }
}