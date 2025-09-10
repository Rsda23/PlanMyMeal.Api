using MongoDB.Bson;
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

        public static FilterDefinition<T> BuildFindByChampRequest<T>(string champ, string value)
        {
            var builder = Builders<T>.Filter;
            var filter = builder.Eq(champ, value);
            return filter;
        }

        public static FilterDefinition<T> BuildFindByIdRequest<T>(string id)
        {
            var builder = Builders<T>.Filter;
            ObjectId objectId = ObjectId.Parse(id);
            var filter = builder.Eq("_id", objectId);
            return filter;
        }
    }
}
