using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities;
// Abstract class for every class that inherits from this to have unique ID and a date when the object was created
public abstract class Document : IDocument
{
    //returns new ObjectId for every new object 
    //reference https://www.mongodb.com/docs/manual/reference/method/ObjectId/ 
    public ObjectId Id { get; set; }

    public DateTime CreatedAt => Id.CreationTime;
}

public interface IDocument
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    ObjectId Id { get; set; }

    DateTime CreatedAt { get; }
}