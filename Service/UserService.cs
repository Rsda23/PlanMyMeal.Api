using MongoDB.Driver;
using PlanMyMeal.Api.Entities;
using PlanMyMeal.Api.Interface;
using PlanMyMeal.Api.MongoHelpers;
using PlanMyMeal.Domain.Models;

namespace PlanMyMeal.Api.Service
{
    public class UserService : IUserService
    {
        private readonly IMongoDatabase _database;

        public UserService(MongoHelper mongoHelpers) 
        { 
            _database = mongoHelpers.GetDatabase();
        }

        public User GetUserByEmail(string email)
        {
            var collection = _database.GetCollection<UserEntity>("users");

            var builder = MongoHelper.BuildFindByChampRequest<UserEntity>("Email", email);
            UserEntity user = collection.Find(builder).FirstOrDefault();

            return user.MapToDomain();
        }

    }
}
