using UnityEngine;

public class ControladorEnemigos : MonoBehaviour
{
    public float velocidadMinima = 2.0f;
    public float velocidadMaxima = 5.0f;

    private float velocidad;
    private Vector3 direccion;

    private void Start()
    {
        velocidad = Random.Range(velocidadMinima, velocidadMaxima); // Velocidad aleatoria
        direccion = Vector3.forward; // Direcci�n aleatoria
        direccion.y = 0; // Aseg�rate de que los enemigos no se muevan verticalmente
    }

    private void Update()
    {
        // Mueve el enemigo hacia la direcci�n aleatoria
        transform.Translate(direccion.normalized * velocidad * Time.deltaTime);

        // Busca el objeto del jugador por etiqueta
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        // Verifica si se encontr� el objeto del jugador antes de intentar acceder a su transform
        if (player != null)
        {
            // Gira hacia la direcci�n del jugador
            transform.LookAt(player.transform);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag != "Player")
        {
            // Si colisiona con un objeto que no es el jugador, cambia la direcci�n de manera aleatoria
            direccion = Random.insideUnitSphere;
            direccion.y = 0;
            transform.Translate(direccion.normalized * velocidad * Time.deltaTime);

            // Busca el objeto del jugador por etiqueta
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            // Verifica si se encontr� el objeto del jugador antes de intentar acceder a su transform
            if (player != null)
            {
                // Gira hacia la direcci�n del jugador
                transform.LookAt(player.transform);
            }
        }
        else
        {
            // Aqu� se restar�a vida
            Debug.Log("Choco con el jugador");
            PlayerHealthController.instance.DamagePlayer(5);
        }
    }
}

