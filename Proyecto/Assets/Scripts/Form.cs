using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Form : MonoBehaviour
{
    public TMP_InputField playerNameInput;
    public Button submitButton;

    void Start()
    {
        // Desactivar el bot�n al inicio
        submitButton.interactable = false;

        // Asignar la funci�n de validaci�n al cambio en el input
        playerNameInput.onValueChanged.AddListener(delegate { ValidateInput(); });

        // Asignar la funci�n de redirecci�n al clic en el bot�n
        submitButton.onClick.AddListener(SubmitName);
    }

    void ValidateInput()
    {
        // Validar si la longitud del nombre es mayor o igual a 3
        if (playerNameInput.text.Length >= 3)
        {
            // Habilitar el bot�n si el nombre es v�lido
            submitButton.interactable = true;
        }
        else
        {

            // Desactivar el bot�n si el nombre no es v�lido
            submitButton.interactable = false;
        }
    }

    void SubmitName()
    {
        // Guardar el nombre en PlayerPrefs para recuperarlo despu�s
        PlayerPrefs.SetString("PlayerName", playerNameInput.text);
        PlayerPrefs.Save();

        // Redireccionar a la escena del juego
        SceneManager.LoadScene("Test");
        Time.timeScale = 1f;
    }
}
