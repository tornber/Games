using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float maxTime = 1.5f;
    [SerializeField] private float heightRange = 0;

    [SerializeField] private GameObject sprite;
    
    private float timer;

    private void SpawnObject()
    {
        Vector3 spawnPos = transform.position + new Vector3(0,heightRange > 0 ? Random.Range(-heightRange,+heightRange) : 0);
        spawnPos.z = 0; 
        GameObject spawnedObj = Instantiate(sprite, spawnPos, Quaternion.identity);
    }

    private void Start()
    {
        SpawnObject();
    }

    private void Update()
    {
        if (timer > maxTime)    
        {
            SpawnObject();
            timer = 0;
        }
        timer += Time.deltaTime;
    }
}
