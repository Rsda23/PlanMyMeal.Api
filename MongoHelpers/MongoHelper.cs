using MongoDB.Driver;

namespace PlanMyMeal.Api.MongoHelpers
{
    public class MongoHelper
    {
        private readonly IConfiguration _configuration;
        private readonly IMongoDatabase _database;
        public MongoHelper(IConfiguration configuration)
        {
            _configuration = configuration;

            var connectionString = _configuration["PlanMyMealDatabase:ConnectionString"];
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new Exception("Error lors de la connection à MongoDB");
            }
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase("PlanMyMeal");
        }
        public IMongoDatabase GetDatabase()
        {
            return _database;
        }
    }
}
