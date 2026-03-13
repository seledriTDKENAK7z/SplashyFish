using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject easyPipe;
    public GameObject mediumPipe;
    public GameObject hardPipe;
    public GameObject insanePipe;

    public GameManager gameManager;

    public float spawnRate = 2f;
    public float minHeight = -1f;
    public float maxHeight = 1f;

    private void OnEnable()
    {
        InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(Spawn));
    }

    private void Spawn()
    {
        GameObject pipeToSpawn;

        if (gameManager.score >= 12)
        {
            pipeToSpawn = insanePipe;
        }
        else if (gameManager.score >= 8)
        {
            pipeToSpawn = hardPipe;
        }
        else if (gameManager.score >= 3)
        {
            pipeToSpawn = mediumPipe;
        }
        else
        {
            pipeToSpawn = easyPipe;
        }

        GameObject pipes = Instantiate(pipeToSpawn, transform.position, Quaternion.identity);
        pipes.transform.position += Vector3.up * Random.Range(minHeight, maxHeight);
    }
}