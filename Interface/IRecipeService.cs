using PlanMyMeal.Domain.Models;

namespace PlanMyMeal.Api.Interface
{
    public interface IRecipeService
    {
        public Recipe GetRecipeById(string recipeId);
    }
}
