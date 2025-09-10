using PlanMyMeal.Domain.Models;

namespace PlanMyMeal.Api.Interface
{
    public interface IUserService
    {
        public User GetUserByEmail(string email);
        public User GetUserById(string id);
    }
}
