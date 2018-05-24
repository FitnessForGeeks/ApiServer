using System;

namespace FitnessForGeeksWebApi.Database
{
    public class Account : IDatabaseObject
    {
        public Account(int? id, string username, string password, DateTime? birthdate, double? weight, double? height, bool? isVerified, string authKey, string firstName, string lastName, string email, string description)
        {
            Id = id;
            Username = username;
            Password = password;
            Birthdate = birthdate;
            Weight = weight;
            Height = height;
            IsVerified = isVerified;
            AuthKey = authKey;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Description = description;
        }

        public int? Id { get; }
        public string Username { get; }
        public string Password { get; }
        public DateTime? Birthdate { get; }
        public double? Weight { get; }
        public double? Height { get; }
        public bool? IsVerified { get; }
        public string AuthKey { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public string Description { get; }

        
    }
}