using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIControllerLevel2 : MonoBehaviour
{
    public static UIControllerLevel2 instance;
    public Slider sliderHealth;
    public TextMeshProUGUI textHealth;
    public Slider sliderHealthEnemy;
    public TextMeshProUGUI textHealthenemy;
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI scoreText;
    public GameObject pauseScreen;
    public TextMeshProUGUI EscudoText;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        // Desactivar interacci�n t�ctil en el inicio del juego
        sliderHealth.interactable = false;
        //sliderHealthEnemy.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}


