using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using MongoDB.Bson;
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
        private readonly IConfiguration _configuration;

        public UserService(MongoHelper mongoHelpers, IConfiguration configuration) 
        { 
            _database = mongoHelpers.GetDatabase();
            _configuration = configuration;
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

        public async Task<string> PostImageToBlob(string userId, IFormFile image)
        {
            if (image == null || image.Length == 0)
            {
                throw new ArgumentException("Aucune image reçue");
            }

            var connectionString = _configuration["AzureBlob:ConnectionString"];
            var containerName = _configuration["AzureBlob:Containers:Users"];

            var blobServiceClient = new BlobServiceClient(connectionString);
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);

            var user = GetUserById(userId);
            if (user != null && !string.IsNullOrEmpty(user.ImageUrl) && !user.ImageUrl.Contains("default"))
            {
                var oldFileName = Path.GetFileName(new Uri(user.ImageUrl).LocalPath);
                var oldBlobClient = containerClient.GetBlobClient(oldFileName);

                await oldBlobClient.DeleteIfExistsAsync();
            }

            //Eviter un conflit du aux noms
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            var blobClient = containerClient.GetBlobClient(fileName);

            var blobHttpHeaders = new BlobHttpHeaders
            {
                ContentType = image.ContentType
            };

            await blobClient.UploadAsync(image.OpenReadStream(), new BlobUploadOptions
            {
                HttpHeaders = blobHttpHeaders
            });

            var imageUrl = blobClient.Uri.ToString();

            await PutImage(userId, imageUrl);

            return blobClient.Uri.ToString();
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
