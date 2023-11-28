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
        // Desactivar el botón al inicio
        submitButton.interactable = false;

        // Asignar la función de validación al cambio en el input
        playerNameInput.onValueChanged.AddListener(delegate { ValidateInput(); });

        // Asignar la función de redirección al clic en el botón
        submitButton.onClick.AddListener(SubmitName);
    }

    void ValidateInput()
    {
        // Validar si la longitud del nombre es mayor o igual a 3
        if (playerNameInput.text.Length >= 3)
        {
            // Habilitar el botón si el nombre es válido
            submitButton.interactable = true;
        }
        else
        {

            // Desactivar el botón si el nombre no es válido
            submitButton.interactable = false;
        }
    }

    void SubmitName()
    {
        // Guardar el nombre en PlayerPrefs para recuperarlo después
        PlayerPrefs.SetString("PlayerName", playerNameInput.text);
        PlayerPrefs.Save();

        // Redireccionar a la escena del juego
        SceneManager.LoadScene("Test");
        Time.timeScale = 1f;
    }
}
