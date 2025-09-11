using PlanMyMeal.Domain.Models;

namespace PlanMyMeal.Api.Interface
{
    public interface IUserService
    {
        public User GetUserByEmail(string email);
        public User GetUserById(string id);
        public void PostUser(string pseudo, string email, string hashedPassword);
        public void PutImage(string userId, string imageUrl);
        public Task<string> PostImageToBlob(string userId, IFormFile imageUrl);
    }
}
