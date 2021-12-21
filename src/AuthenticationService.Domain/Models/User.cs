using MongoDB.Bson;
using System;

namespace AuthenticationService.Domain.Models
{
    public sealed class User
    {

        public User()
        {
            Id = ObjectId.GenerateNewId();
            UpdatedAt = DateTime.UtcNow;
        }

        public ObjectId Id { get; set; }
        public string Username { get; private set; }
        public string HashedPassword { get; private set; }
        public bool Enabled { get; private set; }
        public DateTime UpdatedAt { get; set; }

        public void SetUserName(string username) => Username = username;

        public void SetHashedPassword(string hashedPassword) => HashedPassword = hashedPassword;

        public void EnabledUser() => Enabled = true;

        public void DisableUser() => Enabled = false;
    }
}
