using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryScreen : MonoBehaviour
{

    public float timeBetweenShowing = 1f;
    public GameObject textBox, returnButton, levelButton, againButton;
    public TextMeshProUGUI victoryMessage;

    //public Image blackScreen;
    public float blackScreenFade = 2f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("ShowObjectsCo");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        ShowVictoryMessage();
    }

    // Update is called once per frame
    void Update()
    {
        //blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 0f, blackScreenFade * Time.deltaTime));
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Level1()
    {
        SceneManager.LoadScene("Test");
    }
    public void Level2()
    {
        // Incrementar el nivel
        int nextLevel = GameManager.instance.GetCurrentLevel() + 1;
        GameManager.instance.SetCurrentLevel(nextLevel);

        // Cargar la escena correspondiente
        SceneManager.LoadScene("Level" + nextLevel);
    }

    public IEnumerator ShowObjectsCo()
    {
        yield return new WaitForSeconds(timeBetweenShowing);
        textBox.SetActive(true);
        yield return new WaitForSeconds(timeBetweenShowing);
        returnButton.SetActive(true);
        yield return new WaitForSeconds(timeBetweenShowing);
        levelButton.SetActive(true);
    }

    void ShowVictoryMessage()
    {
        // Obtener el nombre y puntaje del jugador desde PlayerPrefs y GameManager
        string playerName = PlayerPrefs.GetString("PlayerName", "");
        int playerScore = GameManager.instance.GetScore();

        // Construir el mensaje
        string message = $"{playerName} has obtenido {playerScore} puntos";

        // Asignar el mensaje al TextMeshProUGUI
        if (victoryMessage != null)
        {
            victoryMessage.text = message;
        }
        else
        {
            Debug.LogError("No se ha asignado el TextMeshProUGUI para el mensaje de victoria en el Inspector.");
        }
    }

}
