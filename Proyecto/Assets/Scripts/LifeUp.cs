using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LifeUp : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealthController.instance.DamagePlayer(-10);
            Destroy(gameObject);
        }
    }
}

