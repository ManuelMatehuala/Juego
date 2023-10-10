using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] animalPrefabs;
    private float spawnRangeX = 20;
    private float spawnPosZ = 20;
    private float startDelay = 2;
    private float spawnInterval = 1.5f;
    //public int animalIndex
    // Start is called before the first frame update
    void Start() {
        InvokeRepeating("SpawnRandomAnimal", startDelay, spawnInterval);
    }
    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.S)) {
        //    //Randomly generate animal index and spawn position
        //    SpawnRandomAnimal()
        //}   
    }
    void SpawnRandomAnimal() {
        int animalIndex = Random.Range(0, animalPrefabs.Length);
        Vector3 spawnPost = new Vector3(Random.Range(-spawnRangeX, spawnRangeX),0, spawnPosZ);
        Instantiate(animalPrefabs[animalIndex], spawnPost,
            animalPrefabs[animalIndex].transform.rotation);
    }
}
