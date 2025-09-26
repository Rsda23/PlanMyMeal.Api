using Microsoft.AspNetCore.Mvc;
using PlanMyMeal.Api.Interface;
using PlanMyMeal.Domain.Models;

namespace PlanMyMeal.Api.Controllers
{
    public class RecipesController : Controller
    {
        private readonly IRecipeService _recipeService;

        public RecipesController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        [HttpGet("GetRecipeById")]
        public Recipe GetRecipeById([FromQuery]string recipeId)
        {
            return _recipeService.GetRecipeById(recipeId);
        }

        [HttpGet("GetAllRecipes")]
        public List<Recipe> GetAllRecipes()
        {
            return _recipeService.GetAllRecipes();
        }

        [HttpGet("PostRecipe")]
        public void PostRecipe(Recipe recipe)
        {
            _recipeService.PostRecipe(recipe);
        }

        [HttpPost("PostFullRecipe")]
        public void PostFullRecipe(Recipe recipe)
        {
            _recipeService.PostFullRecipe(recipe);
        }
    }
}
