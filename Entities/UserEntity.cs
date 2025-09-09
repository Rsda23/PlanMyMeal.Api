using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using PlanMyMeal.Api.Interface.Map;
using PlanMyMeal.Domain.Models;

namespace PlanMyMeal.Api.Entities
{
    public class UserEntity : IMapToDomain<User>
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }

        [BsonElement("role")]
        public string Role { get; set; } = "member";

        [BsonElement("imageUrl")]
        public string ImageUrl { get; set; } = string.Empty;

        [BsonElement("pseudo")]
        public string Pseudo { get; set; }
        [BsonElement("email")]
        public string Email { get; set; }
        [BsonElement("hashedPassword")]
        public string HashedPassword { get; set; }
        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; }
        [BsonElement("lastLoginAt")]
        public DateTime LastLoginAt { get; set; }

        public UserEntity() 
        { 

        }

        public UserEntity(string role, string imageUrl, string pseudo, string email, string hashedPassword, DateTime createdAt, DateTime lastLoginAt)
        {
            Role = role;
            ImageUrl = imageUrl;
            Pseudo = pseudo;
            Email = email;
            HashedPassword = hashedPassword;
            CreatedAt = createdAt;
            LastLoginAt = lastLoginAt;
        }

        public User MapToDomain()
        {
            User result = new User
            {
                UserId = UserId,
                Role = Role,
                ImageUrl = ImageUrl,
                Pseudo = Pseudo,
                Email = Email,
                HashedPassword = HashedPassword,
                CreatedAt = CreatedAt,
                LastLoginAt = LastLoginAt
            };

            return result;
        }
    }
}
