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
        public User GetUserByEmail([FromQuery] string email)
        {
            return _service.GetUserByEmail(email);
        }

        [HttpGet("GetUserById")]
        public User GetUserById([FromQuery]string id)
        {
            return _service.GetUserById(id);
        }

        [HttpPost("PostUser")]
        public void PostUser([FromBody] User user)
        {
            _service.PostUser(user.Pseudo, user.Email, user.HashedPassword);
        }

        [HttpPost("PostImageToBlob")]
        public Task<string> PostImageToBlob([FromQuery] string userId, IFormFile imageUrl)
        {
            return _service.PostImageToBlob(userId, imageUrl);
        }

        [HttpPut("PutImage")]
        public void PutImage([FromQuery] string userId, [FromQuery] string imageUrl)
        {
            _service.PutImage(userId, imageUrl);
        }
    }
}
