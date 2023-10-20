using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerBig : MonoBehaviour
{
    public GameObject[] enemySmallPrefabs;
    private float spawnRangeXmax = -40;
    private float spawnRangeXmin = -10;
    private float spawnPosZmax = -12;
    private float spawnPosZmin = -8;
    private float startDelay = 3;
    private float spawnInterval = 1.5f;
    //public int animalIndex
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnRandomEnemySmall", startDelay, spawnInterval);
    }
    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.S)) {
        //    //Randomly generate animal index and spawn position
        //    SpawnRandomAnimal()
        //}   
    }
    void SpawnRandomEnemySmall()
    {
        int enemyIndex = Random.Range(0, enemySmallPrefabs.Length);
        Vector3 spawnPost = new Vector3(Random.Range(spawnRangeXmin, spawnRangeXmax), Random.Range(3,4), Random.Range(spawnPosZmin, spawnPosZmax));
        Instantiate(enemySmallPrefabs[enemyIndex], spawnPost,
            enemySmallPrefabs[enemyIndex].transform.rotation);
    }
}
