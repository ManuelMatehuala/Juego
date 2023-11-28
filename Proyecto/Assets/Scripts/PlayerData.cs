using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

public class PlayerData
{
    [BsonId]
    public ObjectId Id { get; set; }

    [BsonElement("Name")]
    public string Name { get; set; }

    [BsonElement("Level1")]
    public int Level1 { get; set; }

    [BsonElement("Level2")]
    public int Level2 { get; set; }
}
