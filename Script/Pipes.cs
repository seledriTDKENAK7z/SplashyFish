using UnityEngine;

public class Pipes : MonoBehaviour
{
    public float speed = 3f;
    private float leftEdge;
    public Transform topPipe;
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
