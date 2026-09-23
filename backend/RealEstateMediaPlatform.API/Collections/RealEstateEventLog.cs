using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace RealEstateMediaPlatform.API.Collections
{
    public class RealEstateEventLog
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string ?OperatorUserId { get; set; }
        public string? ResourceCaseId { get; set; }
        public string ?EventType { get; set; }
        [Required]
        public string EventMessage { get; set; }  
        public bool IsSuccess { get; set; }

        public DateTime EventTime = DateTime.Now;
        
    }
}

