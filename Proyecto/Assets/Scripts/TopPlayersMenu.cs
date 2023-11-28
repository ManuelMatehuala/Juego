using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TopPlayersMenu : MonoBehaviour
{
    public TextMeshProUGUI topPlayersText;

    // Start is called before the first frame update
    void Start()
    {
        DisplayTopPlayers(1); // Mostrar los mejores jugadores para el nivel 1
        DisplayTopPlayers(2); // Mostrar los mejores jugadores para el nivel 2
    }

    void DisplayTopPlayers(int level)
    {
        // Obtener los mejores jugadores para el nivel dado
        List<PlayerData> topPlayers = DBMongo.instance.GetTopPlayers(level);

        // Construir el mensaje
        string message = $"Nivel {level}:\n";
        foreach (var player in topPlayers)
        {
            int levelScore = 0;

            // Evaluar el nivel para determinar qué campo utilizar
            if (level == 1)
            {
                levelScore = player.Level1;
            }
            else if (level == 2)
            {
                levelScore = player.Level2;
            }

            // Mostrar el mensaje
            message += $"{player.Name}: {levelScore} puntos\n";
        }

        // Mostrar el mensaje
        if (topPlayersText != null)
        {
            topPlayersText.text += message + "\n";
        }
        else
        {
            Debug.LogError("No se ha asignado el TextMeshProUGUI para mostrar los mejores jugadores en el Inspector.");
        }
    }
}
