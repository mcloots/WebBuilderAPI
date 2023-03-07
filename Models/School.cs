using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace WebBuilder.Models;

public class School
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string name { get; set; } = null!;
    public string tel { get; set; } = null!;
    public string type { get; set; } = null!; //Katholieke school, School, Basisschool
    public string street { get; set; } = null!;
    public string streetNumber { get; set; } = null!;
    public string postalCode { get; set; } = null!;
    public string place { get; set; } = null!; //City - Municipality
    

    //[BsonElement("items")]
    //[JsonPropertyName("items")]
    //public List<string> movieIds { get; set; } = null!;

}