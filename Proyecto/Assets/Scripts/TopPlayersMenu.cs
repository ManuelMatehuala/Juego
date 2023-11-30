using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TopPlayersMenu : MonoBehaviour
{
    public static TopPlayersMenu instance;
    public TextMeshProUGUI topPlayersText;
    private string message;
    private int level;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        DisplayTopPlayers();
    }

    public void ProcesarScores(string cadena)
    {
        // Mostrar el mensaje
        if (topPlayersText != null)
        {
            topPlayersText.text =cadena;
        }
        else
        {
            Debug.LogError("No se ha asignado el TextMeshProUGUI para mostrar los mejores jugadores en el Inspector.");
        }
    }

    void DisplayTopPlayers()
    {
        // Obtener los mejores jugadores para el nivel dado
        if (DBMongo.instance != null)
        {
            System.Threading.Tasks.Task task = DBMongo.instance.GetScores();
        }
        else {
            Debug.Log("DBMongo es nulo");
        }


    }
}
