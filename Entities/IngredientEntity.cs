using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using PlanMyMeal.Api.Interface.Map;
using PlanMyMeal.Domain.Models;

namespace PlanMyMeal.Api.Entities
{
    public class IngredientEntity : IMapToDomain<Ingredient>
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string IngredientId { get; set; }

        [BsonElement]
        public string RecipeId { get; set; }

        [BsonElement]
        public string UserId { get; set; }

        [BsonElement]
        public string Title { get; set; }

        public IngredientEntity()
        {

        }

        public IngredientEntity(string recipeId, string userId, string title)
        {
            RecipeId = recipeId;
            UserId = userId;
            Title = title;
        }

        public Ingredient MapToDomain()
        {
            Ingredient result = new Ingredient
            {
                IngredientId = IngredientId,
                RecipeId = RecipeId,
                UserId = UserId,
                Title = Title
            };

            return result;
        }

    }
}
