using UnityEngine;
using System.Collections;
public class EnemigoDisparaController : MonoBehaviour
{
    public float movementSpeed = 10f;
    public float movementRange = -3f;
    public float fireRate = 200f;
    public GameObject bulletPrefab;


    private void Start()
    {
        StartCoroutine(SpawnProyectiles());
    }

    private void Update()
    {
        // Mover de un lado a otro
        float horizontalMovement = Mathf.PingPong(Time.time * movementSpeed, movementRange)-12;
        transform.position = new Vector3(transform.position.x, transform.position.y, horizontalMovement);

        
    }

    IEnumerator SpawnProyectiles()
    {
        for (int i = 0; i < 100; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(1);
        }
    }
}
