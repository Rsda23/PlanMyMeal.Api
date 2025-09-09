using Microsoft.AspNetCore.Mvc;
using PlanMyMeal.Api.Interface;
using PlanMyMeal.Domain.Models;

namespace PlanMyMeal.Api.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _service;

        public UsersController(IUserService user)
        {
            _service = user;
        }

        [HttpGet("GetUserByEmail")]
        public User GetUserByEmail(string email)
        {
            return _service.GetUserByEmail(email);
        }
    }
}
