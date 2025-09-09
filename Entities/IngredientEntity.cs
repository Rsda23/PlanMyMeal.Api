using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PlanMyMeal.Api.Entities
{
    public class IngredientEntity
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
    }
}
