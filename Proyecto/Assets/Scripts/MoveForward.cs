using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MoveForward : MonoBehaviour
{
    public float speed = 5.0f;
    private Transform player; // Referencia al transform del jugador

    private void Start()
    {
        // Encuentra el jugador (puedes hacerlo de diferentes maneras, por ejemplo, por etiqueta o nombre del objeto)
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void LateUpdate()
    {
        if (player != null)
        {
            // Mira hacia la posición del jugador
            transform.LookAt(player);

            // Mueve el objeto hacia adelante
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }
    //void LateUpdate()
    //{
    //    transform.position = target.position;
    //    transform.rotation = target.rotation;

    //}
}
