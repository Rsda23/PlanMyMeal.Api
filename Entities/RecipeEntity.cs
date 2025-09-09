using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using PlanMyMeal.Api.Interface.Map;
using PlanMyMeal.Domain.Models;

namespace PlanMyMeal.Api.Entities
{
    public class RecipeEntity : IMapToDomain<Recipe>
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string RecipeId { get; set; }

        [BsonElement]
        public string UserId { get; set; }

        [BsonElement]
        public string Title { get; set; }

        [BsonElement]
        public List<IngredientEntity> Ingredients { get; set; } = new List<IngredientEntity>();

        public RecipeEntity()
        {

        }

        public RecipeEntity(string userId, string title, List<IngredientEntity> ingredients)
        {
            UserId = userId;
            Title = title;
            Ingredients = ingredients;
        }

        public Recipe MapToDomain()
        {
            Recipe result = new Recipe
            {
                UserId = UserId,
                Title = Title,
                Ingredients = MapIngredients(),
            };

            return result;
        }

        private List<Ingredient> MapIngredients()
        {
            List<Ingredient> result = new List<Ingredient>();
            if (Ingredients != null) 
            { 
                foreach (IngredientEntity ingredient in Ingredients)
                {
                    result.Add(new Ingredient(ingredient.RecipeId, ingredient.UserId, ingredient.Title));
                }
            }

            return result;
        }
    }
}
