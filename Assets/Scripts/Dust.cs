using UnityEngine;

public class Dust : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2.5f; // slower

    void Update()
    {
        // move left slowly
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);

        // destroy if off-screen
        if (transform.position.x < -20f)
        {
            Destroy(gameObject);
        }
    }
}