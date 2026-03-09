using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;

    void Update()
    {
        // move left
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);

        // destroy if off-screen
        if(transform.position.x < -20f) // adjust to camera view
        {
            Destroy(gameObject);
        }
    }
}