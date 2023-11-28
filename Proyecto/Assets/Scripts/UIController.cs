using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    public Slider sliderHealth;
    public TextMeshProUGUI textHealth;
    public Slider sliderHealthEnemy;
    public TextMeshProUGUI textHealthenemy;
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;
    private float tiempoRestante = 60.0f; // 300 segundos = 5 minutos

    public GameObject pauseScreen;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        // Desactivar interacción táctil en el inicio del juego
        sliderHealth.interactable = false;
        //sliderHealthEnemy.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if (tiempoRestante > 0)
        //{
        //    tiempoRestante -= Time.deltaTime;

        //    // Convertir tiempoRestante a minutos y segundos
        //    int minutos = Mathf.FloorToInt(tiempoRestante / 60);
        //    int segundos = Mathf.FloorToInt(tiempoRestante % 60);

        //    // Actualizar el texto del cronómetro
        //    timeText.text = minutos.ToString("00") + ":" + segundos.ToString("00");
        //}
        //else
        //{
        //    timeText.text = "00:00";
        //    SceneManager.LoadScene("Victory");
        //}

        if (tiempoRestante > 0)
        {
            tiempoRestante -= Time.deltaTime;

            // Verificar si el tiempoRestante es menor o igual a cero
            if (tiempoRestante <= 0)
            {
                tiempoRestante = 0; // Establecer el tiempoRestante a cero
                timeText.text = "00:00";

                // Llamar a la función EndGame solo al finalizar el tiempo
                GameManager.instance.EndGame();
            }
            else
            {
                // Convertir tiempoRestante a minutos y segundos
                int minutos = Mathf.FloorToInt(tiempoRestante / 60);
                int segundos = Mathf.FloorToInt(tiempoRestante % 60);

                // Actualizar el texto del cronómetro
                timeText.text = minutos.ToString("00") + ":" + segundos.ToString("00");
            }
        }
    }
}
