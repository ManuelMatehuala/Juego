using UnityEngine;

public class MoveForward : MonoBehaviour
{
    // Velocidad de movimiento de la esfera
    public float velocidad = 5f;

    // Tiempo de vida de la esfera en segundos
    public float tiempoDeVida = 30f;


    void Update()
    {
        // Mover la esfera hacia adelante en el eje Z
        transform.Translate(Vector3.forward * velocidad * Time.deltaTime);

        //// Reducir el tiempo de vida
        //tiempoDeVida -= Time.deltaTime;

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {

            PlayerHealthController.instance.DamagePlayer(10);
        }
    }
}
