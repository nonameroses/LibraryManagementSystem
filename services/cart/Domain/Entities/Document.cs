using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;


namespace Domain.Entities;
// Abstract class for every class that inherits from this to have unique ID and a date when the object was created
public abstract class Document : IDocument
{
    //returns new ObjectId for every new object 
    //reference https://www.mongodb.com/docs/manual/reference/method/ObjectId/ 

    public string Id { get; set; }

    public DateTime CreatedAt => DateTime.UtcNow;
}

public interface IDocument
{   
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [JsonIgnore]
    string Id { get; set; }

    DateTime CreatedAt { get; }
}
