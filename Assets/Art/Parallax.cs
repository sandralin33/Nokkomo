using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] float scrollSpeed = 5f; // same as enemy speed
    private float length;
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        // Move background left
        transform.Translate(Vector2.left * scrollSpeed * Time.deltaTime);

        // Loop background
        if (transform.position.x <= startPos.x - length)
        {
            transform.position = new Vector3(transform.position.x + length * 2f, transform.position.y, transform.position.z);
        }
    }
}