namespace RealEstateMediaPlatform.API.Data
{
    public class MongoDbSettings
    {
        public string DatabaseName {  get; set; }
        public string ConnectionString { get; set; }
        public string UserEventLogCollectionName {  get; set; }
        public string ListingCaseEventLogCollectionName { get; set; }
    }
}
