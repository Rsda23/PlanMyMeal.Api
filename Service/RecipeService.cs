using MongoDB.Driver;
using PlanMyMeal.Api.Entities;
using PlanMyMeal.Api.Interface;
using PlanMyMeal.Api.MongoHelpers;
using PlanMyMeal.Domain.Models;

namespace PlanMyMeal.Api.Service
{
    public class RecipeService : IRecipeService
    {
        private readonly IMongoDatabase _database;

        public RecipeService(IMongoDatabase database)
        {
            _database = database;
        }

        public Recipe GetRecipeById(string recipeId)
        {
            var collection = _database.GetCollection<RecipeEntity>("recipes");

            var filter = MongoHelper.BuildFindByIdRequest<RecipeEntity>(recipeId);
            RecipeEntity recipe = collection.Find(filter).FirstOrDefault();

            return recipe.MapToDomain();
        }

        public List<Recipe> GetAllRecipes()
        {
            var collection = _database.GetCollection<RecipeEntity>("recipes");
            List<RecipeEntity> recipes = collection.Find(Builders<RecipeEntity>.Filter.Empty).ToList();

            return MapToList(recipes);
        }

        public void PostRecipe(Recipe recipe)
        {
            if (_database == null)
            {
                throw new InvalidOperationException("Erreur avec la database");
            }

            var collection = _database.GetCollection<RecipeEntity>("recipes");

            RecipeEntity newRecipe = new RecipeEntity(recipe.Title, recipe.Difficulty, recipe.Type);

            collection.InsertOne(newRecipe);
        }

        public void PostFullRecipe(Recipe recipe)
        {
            if (_database == null)
            {
                throw new InvalidOperationException("Erreur avec la database");
            }

            var collection = _database.GetCollection<RecipeEntity>("recipes");

            List<IngredientEntity> ingredients = new List<IngredientEntity>();

            foreach (var item in recipe.Ingredients)
            {
                var t = new IngredientEntity(item.RecipeId, item.UserId, item.Title);

                ingredients.Add(t);
            }

            RecipeEntity newRecipe = new RecipeEntity(recipe.UserId, recipe.Title, recipe.ImageUrl, recipe.Difficulty, recipe.TotalTimeMinutes, recipe.Type, recipe.Calories, recipe.Diets, recipe.Tags, ingredients);

            collection.InsertOne(newRecipe);
        }

        public List<Recipe> MapToList(List<RecipeEntity> data)
        {
            var result = new List<Recipe>();
            foreach (var item in data)
            {
                result.Add(item.MapToDomain());
            }
            return result;
        }
    }
}
