using Microsoft.AspNetCore.Mvc;
using PlanMyMeal.Api.Interface;

namespace PlanMyMeal.Api.Controllers
{
    public class RecipesController : Controller
    {
        private readonly IRecipeService _recipeService;

        public RecipesController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }
    }
}
