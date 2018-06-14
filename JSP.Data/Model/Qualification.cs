using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace JSP.Data.Model
{
    public class Qualification
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime UpdatedOn { get; set; } = DateTime.Now;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}
