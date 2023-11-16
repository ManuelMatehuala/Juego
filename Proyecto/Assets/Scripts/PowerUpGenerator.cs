using UnityEngine;

public class PowerupGenerator : MonoBehaviour
{
    public GameObject powerupPrefab;
    public float spawnInterval = 10f;

    void Start()
    {
        InvokeRepeating("SpawnPowerup", 0f, spawnInterval);
    }

    void SpawnPowerup()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-50.0f,-20.0f), Random.Range(-2.0f,-3.0f), Random.Range(-8.0f,-14.0f));
        Instantiate(powerupPrefab, spawnPosition, Quaternion.identity);
    }
}

