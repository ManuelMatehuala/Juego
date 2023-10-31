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

        // Gira hacia la direcci�n del jugador
        transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
    }
    //private void LateUpdate()
    //{
    //    // Gira hacia la direcci�n del jugador
    //    transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
    //}
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.collider.tag != "Player")
        {
            //Debug.Log("Choco con otro");
            // Si colisiona con un objeto que no es el jugador, cambia la direcci�n de manera aleatoria
            direccion = Random.insideUnitSphere;
            direccion.y = 0;
            transform.Translate(direccion.normalized * velocidad * Time.deltaTime);
            transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
        }
        else
        {
            //Aqui se restaria vida
            Debug.Log("Choco con el jugador");
        }
    }
}
