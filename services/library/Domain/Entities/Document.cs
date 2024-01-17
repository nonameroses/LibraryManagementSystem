using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities;
//returns new ObjectId for every new object 
//reference https://www.mongodb.com/docs/manual/reference/method/ObjectId/ 
// Abstract class for every class that inherits from this to have unique ID and a date when the object was created
public abstract class Document : IDocument
{
    public string Id { get; set; }
    public DateTime CreatedAt => DateTime.UtcNow;
}

public interface IDocument
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    string Id { get; set; }
    DateTime CreatedAt { get; }
}