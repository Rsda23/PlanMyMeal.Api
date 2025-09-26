using MongoDB.Driver;
using PlanMyMeal.Api.Interface;

namespace PlanMyMeal.Api.Service
{
    public class RecipeService : IRecipeService
    {
        private readonly IMongoDatabase _database;

        public RecipeService(IMongoDatabase database)
        {
            _database = database;
        }
    }
}
