using UnityEngine;
using UnityEngine.SceneManagement;

public class LifeUpGenerator : MonoBehaviour
{
    public GameObject powerupPrefab;
    public float spawnInterval = 40f;

    void Start()
    {
        InvokeRepeating("SpawnPowerup", 0f, spawnInterval);
    }

    void SpawnPowerup()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName == "Test")
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-50.0f, -20.0f), Random.Range(-2.0f, -3.0f), Random.Range(-8.0f, -14.0f));
            Instantiate(powerupPrefab, spawnPosition, Quaternion.identity);
        }
        else if (currentSceneName == "Level2")
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-50.0f, -20.0f), Random.Range(2.0f, 4.0f), Random.Range(-8.0f, -14.0f));
            Instantiate(powerupPrefab, spawnPosition, Quaternion.identity);
        }
    }
}

