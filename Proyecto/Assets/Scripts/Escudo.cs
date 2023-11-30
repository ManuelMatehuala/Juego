using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Escudo : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealthController.instance.rutinaEscudo();
            Destroy(gameObject);
        }
    }
}


