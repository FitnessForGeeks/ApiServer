using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessForGeeksWebApi.Database.ReviewDB
{
    public class Review : IDatabaseObject
    {
        public Review(int id, int accountid, int recipeId, string text, double rating, string username, DateTime createdAt)
        {
            Id = id;
            Accountid = accountid;
            RecipeId = recipeId;
            Text = text;
            Rating = rating;
            CreatedAt = createdAt;
            Username = username;
        }

        public int Id { get; }
        public int Accountid { get; }
        public int RecipeId { get; }
        public string Text { get; }
        public double Rating { get; }
        public string Username { get; }
        public string ProfilePicture { get
            {
                return $"http://localhost:5000/api/static/{Username}/profilePicture";
            }
        }
        public DateTime CreatedAt { get; }
    }
}
