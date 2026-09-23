using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RealEstateMediaPlatform.API.Collections;


namespace RealEstateMediaPlatform.API.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;
        private readonly string UserEventLogCollectionName;
        private readonly string ListingCaseEventLogCollectionName;



        public MongoDbContext(IOptions<MongoDbSettings> mongodbSettings)
        {
            var client = new MongoClient(mongodbSettings.Value.ConnectionString);
            _database = client.GetDatabase(mongodbSettings.Value.DatabaseName);
            UserEventLogCollectionName = mongodbSettings.Value.UserEventLogCollectionName;
            ListingCaseEventLogCollectionName = mongodbSettings.Value.ListingCaseEventLogCollectionName;

            UserEventLogs = _database.GetCollection<RealEstateEventLog>(UserEventLogCollectionName);
            ListingCaseEventLogs = _database.GetCollection<RealEstateEventLog>(ListingCaseEventLogCollectionName);
           
        }

        public virtual IMongoCollection<RealEstateEventLog> UserEventLogs { get; set; }
        public virtual IMongoCollection<RealEstateEventLog> ListingCaseEventLogs {  get; set; }
    }
}

