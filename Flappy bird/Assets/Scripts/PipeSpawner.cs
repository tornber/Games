using UnityEngine;

public class PipeSpawner : MonoBehaviour
{

    [SerializeField] private float maxTime = 1.5f;
    [SerializeField] private float heightRange = 0.4f;
    [SerializeField] private GameObject Pipe;

    private float timer;

    void Start()
    {
        SpawnPipe();
    }

    void Update()
    {
        if (timer > maxTime)
        {
            timer = 0;
            SpawnPipe();
            return;
        }
        timer += Time.deltaTime;
    }
    
    private void SpawnPipe()
    {
        Vector3 spawnPosition = transform.position + new Vector3(0, Random.Range(-heightRange, heightRange));
        GameObject pipe = Instantiate(Pipe, spawnPosition, Quaternion.identity);
        Destroy(pipe, 5f);
    }
}
