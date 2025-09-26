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
        public string UserId { get; set; } = string.Empty;

        [BsonElement]
        public string Title { get; set; }

        [BsonElement]
        public string ImageUrl { get; set; } = string.Empty;

        [BsonElement]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [BsonElement]
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;

        [BsonElement]
        public string Difficulty { get; set; }

        [BsonElement]
        public bool IsFavorite { get; set; }

        [BsonElement]
        public int TotalTimeMinutes { get; set; }

        [BsonElement]
        public string Type { get; set; }

        [BsonElement]
        public int Calories { get; set; }

        [BsonElement]
        public List<string> Diets { get; set; } = new();

        [BsonElement]
        public List<string> Tags { get; set; } = new();

        [BsonElement]
        public List<IngredientEntity> Ingredients { get; set; } = new List<IngredientEntity>();


        public RecipeEntity()
        {

        }

        public RecipeEntity(string title, string difficulty, string type)
        {
            Title = title;
            Difficulty = difficulty;
            Type = type;
        }

        public Recipe MapToDomain()
        {
            Recipe result = new Recipe
            {
                RecipeId = RecipeId,
                UserId = UserId,
                Title = Title,
                ImageUrl = ImageUrl,
                CreatedAt = CreatedAt,
                UpdateAt = UpdateAt,
                Difficulty = Difficulty,
                IsFavorite = IsFavorite,
                TotalTimeMinutes = TotalTimeMinutes,
                Type = Type,
                Calories = Calories,
                Diets = Diets,
                Tags = Tags,
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
