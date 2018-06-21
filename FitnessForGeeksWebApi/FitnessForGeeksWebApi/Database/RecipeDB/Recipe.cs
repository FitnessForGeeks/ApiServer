using FitnessForGeeksWebApi.Database.ReviewDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessForGeeksWebApi.RecipeDB
{
    public class Recipe
    {
        private readonly ReviewManager reviewManager = new ReviewManager();

        public Recipe(int? id, int reviewCount, double avgRating, int? accountId, string title, string description, int? calories, string owner, List<string> directions, List<string> ingredients, DateTime? createdAt)
        {
            Id = id;
            Title = title;
            Calories = calories;
            Directions = directions;
            AccountId = accountId;
            Owner = owner;
            CreatedAt = createdAt;
            Description = description;
            Ingredients = ingredients;
            ReviewCount = reviewCount;
            AvgRating = avgRating;
        }

        public int? Id { get; }
        public int? AccountId { get; }
        public string Title { get; }
        public string Image {
            get
            {
                return "http://localhost:5000/api/static/recipes/" + Title;
            }
        }
        public string Owner { get; }
        public DateTime? CreatedAt { get; }
        public string Description { get; }
        public int? Calories { get; }
        public double AvgRating { get; }
        public int ReviewCount { get; }
        public List<string> Directions { get; }
        public List<string> Ingredients { get; }
    }
}
