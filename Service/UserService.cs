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

        public User GetUserById(string id)
        {
            var collection = _database.GetCollection<UserEntity>("users");

            var filter = MongoHelper.BuildFindByIdRequest<UserEntity>(id);
            UserEntity user = collection.Find(filter).FirstOrDefault();

            return user.MapToDomain();
        }

        public void PostUser(string pseudo, string email, string hashedPassword)
        {
            UserEntity user = new UserEntity(pseudo, email, hashedPassword);
            if (_database == null)
            {
                throw new InvalidOperationException("Error collection MongoDB");
            }

            var collection = _database.GetCollection<UserEntity>("users");
            collection.InsertOne(user);
        }

        public Task PutImage(string userId, string imageUrl)
        {
            var collection = _database.GetCollection<UserEntity>("users");

            var filter = MongoHelper.BuildFindByIdRequest<UserEntity>(userId);

            var update = Builders<UserEntity>.Update.Combine(
                Builders<UserEntity>.Update.Set(f => f.ImageUrl, imageUrl));

            collection.UpdateOne(filter, update);

            return Task.CompletedTask;
        }
    }
}
