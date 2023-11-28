using UnityEngine;

public class MoveFoEnemy3 : MonoBehaviour
{
    // Velocidad de movimiento de la esfera
    public float velocidad = 5f;

    void Update()
    {

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {

            PlayerHealthController.instance.DamagePlayer(20);
            Destroy(gameObject);
        }
    }


}
