using UnityEngine;

public class Seaweeds : MonoBehaviour
{
    public float speed = 3f;
    private float leftEdge;
    public Transform topSeaweed;
    private void Start()
    {
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 3f;
    }

    private void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;

        if(transform.position.x < leftEdge)
        {
            Destroy(gameObject);
        }
    }
}
