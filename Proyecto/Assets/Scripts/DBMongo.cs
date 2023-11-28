using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Bson;
using MongoDB.Driver;

public class DBMongo : MonoBehaviour
{
    public static DBMongo instance;

    private MongoClient client;
    private IMongoDatabase database;
    private IMongoCollection<PlayerData> collection;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        client = new MongoClient("mongodb+srv://unity:unity@cluster0.kczqgdb.mongodb.net/?retryWrites=true&w=majority");
        database = client.GetDatabase("unity");
        collection = database.GetCollection<PlayerData>("player");
    }

    public List<PlayerData> GetTopPlayers(int level)
    {
        var filter = Builders<PlayerData>.Filter.Empty;
        var sort = Builders<PlayerData>.Sort.Descending($"Level{level}");

        var topPlayers = collection.Find(filter).Sort(sort).Limit(3).ToList();

        return topPlayers;
    }

    public void SaveScore(string playerName, int score, int level)
    {
        var filter = Builders<PlayerData>.Filter.Eq("Name", playerName);

        // Crear el documento de actualización con la nueva información del nivel
        var update = Builders<PlayerData>.Update
            .Set($"Level{level}", score)
            .SetOnInsert("Name", playerName); // Asegura que se inserte el nombre si no existe

        // Intentar actualizar el documento existente, y si no existe, insertar uno nuevo
        var result = collection.UpdateOne(filter, update, new UpdateOptions { IsUpsert = true });

        if (result.IsAcknowledged)
        {
            Debug.Log($"Nivel {level} actualizado o nuevo registro creado con éxito.");
        }
        else
        {
            Debug.LogError($"Error al actualizar el nivel {level}.");
        }
    }
}