using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Powerup : MonoBehaviour
{
    public int powerupValue = 1;
    public TextMeshProUGUI scoreText;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.AddScore(powerupValue);
            Destroy(gameObject);
        }
    }
}

