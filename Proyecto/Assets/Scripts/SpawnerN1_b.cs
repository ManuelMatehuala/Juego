using System.Collections;
using UnityEngine;

public class SpawnerN1_b : MonoBehaviour
{
    public GameObject enemyPrefab; // El GameObject del enemigo que has creado
    public Transform spawnPoint; // El punto de generación
    private int cantidadDePrefabs = 20;

    void Start()
    {
        StartCoroutine(SpawnEnemiesWithDelay());
    }

    IEnumerator SpawnEnemiesWithDelay()
    {
        for (int i = 0; i < cantidadDePrefabs; i++)
        {
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            yield return new WaitForSeconds(10);
        }
    }

    public int getPrefabs(int cantidadDePrefabs)
    {
        return cantidadDePrefabs;
    }
}
