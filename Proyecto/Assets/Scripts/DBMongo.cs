using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using System.Text.Json;
using Palmmedia.ReportGenerator.Core.Common;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using System.Text;
public class DBMongo : MonoBehaviour
{
    public static DBMongo instance;
    string apiUrl = "https://api-bytebattle-mongo.onrender.com";
    public Dictionary<string, object> scores;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public async System.Threading.Tasks.Task GetScores()
    {
        // Realiza la llamada a la API y obtén la respuesta
        string apiUrl = "https://api-bytebattle-mongo.onrender.com";
        List<int> levels = new List<int>();
        levels.Add(1);
        levels.Add(2);
        string cadena = "";
        foreach (int level in levels)
        {
            if (level == 2) {
                cadena += "\n";
            }
            cadena += $"Nivel: {level}\n";
            string apiResponse = await GetTopScores(level, apiUrl);
            var bsonArray = BsonSerializer.Deserialize<BsonArray>(apiResponse);
            if (level == 1)
            {
                foreach (BsonDocument bsonDocument in bsonArray)
                {
                    // Acceder a los elementos dentro de cada BsonDocument
                    BsonElement levelElement = bsonDocument.GetElement("Level1");
                    BsonElement nameElement = bsonDocument.GetElement("Name");

                    // Obtener los valores
                    int nivel = levelElement.Value.AsInt32;
                    string name = nameElement.Value.AsString;

                    // Imprimir los valores
                    Debug.Log($"Name: {name}, Level1: {nivel}");
                    cadena += $"{name}: {nivel} puntos\n";
                }
            }
            else if (level == 2)
            {
                foreach (BsonDocument bsonDocument in bsonArray)
                {
                    // Acceder a los elementos dentro de cada BsonDocument
                    BsonElement levelElement = bsonDocument.GetElement("Level2");
                    BsonElement nameElement = bsonDocument.GetElement("Name");

                    // Obtener los valores
                    int nivel = levelElement.Value.AsInt32;
                    string name = nameElement.Value.AsString;

                    // Imprimir los valores
                    Debug.Log($"Name: {name}, Level2: {nivel}");

                    cadena += $"{name}: {nivel} puntos\n";
                }
            }
        }
        Debug.Log(cadena);
        TopPlayersMenu.instance.ProcesarScores(cadena);
    }

    public static async Task<string> GetTopScores(int level, string apiUrl)
    {
        string url = $"{apiUrl}/top_scores/{level}";

        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new HttpRequestException($"Error al llamar a la API. Código de estado: {response.StatusCode}");
            }
        }

    }
    public void SaveScore(string name, int score, int level)
    {
        _ = SaveScoreRoutineAsync(name, score, level);
    }

    async Task<string> SaveScoreRoutineAsync(string name, int score, int level)
    {
        string url = $"{apiUrl}/save_score";

        //Score data = new Score { name = name, score = score, level = level };
        BsonDocument doc = new BsonDocument
        {
            { "name", name },
            { "score", score },
            { "level", level }
        };
        // Convertir el BsonDocument a una cadena JSON
        string json = doc.ToJson();
        HttpContent content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

        // Crear un cliente HTTP
        using (HttpClient client = new HttpClient())
        {
            // Enviar la solicitud POST
            HttpResponseMessage response = await client.PostAsync(url, content);

            // Verificar el estado de la respuesta
            if (response.IsSuccessStatusCode)
            {
                // Leer y devolver el contenido de la respuesta
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                // Manejar errores
                throw new HttpRequestException($"Error al llamar a la API. Código de estado: {response.StatusCode}");
            }
        }
    }

}

public class Score
{
    public string name;
    public int score;
    public int level;
}
public class ScoreLevel2
{
    public int Level2;
    public string Name;
}
public class ScoreLevel1
{
    public int Level1;
    public string Name;
}
public class MyClass
{
    public int Level2 { get; set; }
    public string Name { get; set; }
}