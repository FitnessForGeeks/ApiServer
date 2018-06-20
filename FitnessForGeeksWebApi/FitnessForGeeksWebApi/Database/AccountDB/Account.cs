using FitnessForGeeksWebApi.Database.EatenRecipeDB;
using FitnessForGeeksWebApi.RecipeDB;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace FitnessForGeeksWebApi.Database.AccountDB
{
    public class Account : IDatabaseObject
    {
        private readonly EatenRecipeManager eatenRecipeManager = new EatenRecipeManager();
        private readonly RecipeManager recipeManager = new RecipeManager();
		private List<EatenRecipe> eatenRecipes = new List<EatenRecipe>();

        public Account(int? id, string username, string password, DateTime? birthdate, double? weight, int? height, bool? isVerified, string authKey, string firstName, string lastName, string email, string description, bool? isMale)
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
            IsMale = isMale;
        }

        public int? Id { get; }
        public string Username { get; }
        [JsonIgnore]
        public string Password { get; }
        public DateTime? Birthdate { get; }
        public double? Weight { get; }
        public int? Height { get; }
        public bool? IsVerified { get; }
        public string AuthKey { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public string Description { get; }
        public bool? IsMale { get; }
        public string ProfilePicture
        {
            get
            {
                return $"http://localhost:5000/api/static/{Username}/profilePicture";
            }
        }
        /// <summary>
        /// Returns all recipes that were eaten today
        /// </summary>
        [JsonIgnore]
        public List<EatenRecipe> RecipesEatenToday
        {
            get {
                return eatenRecipes = eatenRecipeManager.GetCurrentByAccountId(Id.Value);
            }
        }

		[JsonIgnore]
		public List<Recipe> MyRecipes
		{
			get
			{
				return recipeManager.GetAllByAccountId(Id.Value);
			}
		}

        public int? Age
        {
            get
            {
                if(Birthdate.HasValue)
                    return (int) (DateTime.Now - Birthdate.Value).TotalDays / 365;
                return null;
            }
        }

        public int? TDEE
        {
            get
            {
                if(Height.HasValue && Weight.HasValue && IsMale.HasValue)
                {
                    return (int) (Height.Value * 6.25 + Weight.Value * 9.99 - Age * 4.92 + (IsMale.Value ? 5 : -161));
                }
                return null;
            }
        }

        public int? RemainingCalories
        {
            get
            {
				var eatenRecipes = RecipesEatenToday;
                var calories = TDEE;
                if(calories.HasValue)
                {
                    if(eatenRecipes.Count != 0)
                    {
                        foreach(var recipe in eatenRecipes)
                        {
                            calories -= recipe.Calories.Value;
                        }
                    }
                    return (int) calories;
                }
                return null;
            }
        }

        
    }
}