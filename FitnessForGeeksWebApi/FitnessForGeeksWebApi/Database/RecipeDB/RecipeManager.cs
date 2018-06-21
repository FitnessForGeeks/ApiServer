using System;
using System.Collections.Generic;
using FitnessForGeeksWebApi.Database;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using FitnessForGeeksWebApi.Database.ReviewDB;

namespace FitnessForGeeksWebApi.RecipeDB
{
    public class RecipeManager : IDatabaseManager<Recipe>
    {

        public List<Recipe> GetAll()
        {
            var recipes = new List<Recipe>();

            MySqlDatabase.ExecuteReader("select recipes.*, accounts.username from recipes join accounts on accounts.id = recipes.accountId", reader =>
            {
                recipes.Add(NewRecipeFromReader(reader));
            });

            return recipes;
        }

        private Recipe NewRecipeFromReader(MySqlDataReader reader)
        {
            var directionsAsString = MySqlDatabase.GetValue<string>(reader, "directions");
            var ingredientsAsString = MySqlDatabase.GetValue<string>(reader, "ingredients");
            var description = MySqlDatabase.GetValue<String>(reader, "description");
            var id = MySqlDatabase.GetValueOrNull<int>(reader, "id");
            return new Recipe(
                id,
                MySqlDatabase.GetValueOrNull<int>(reader, "reviewCount").Value,
                MySqlDatabase.GetValueOrNull<double>(reader, "avgRating").Value,
                MySqlDatabase.GetValueOrNull<int>(reader, "accountId"),
                MySqlDatabase.GetValue<String>(reader, "title"),
                description ?? "No description",
                MySqlDatabase.GetValueOrNull<int>(reader, "calories"),
                MySqlDatabase.GetValue<String>(reader, "username"),
                directionsAsString == String.Empty? null : directionsAsString?.Split(";").ToList(),
                ingredientsAsString == String.Empty? null : ingredientsAsString?.Split(";").ToList(),
                MySqlDatabase.GetValueOrNull<DateTime>(reader, "createdAt")
            );
        }

        public List<Recipe> GetAll(int accountId, int pageNumber, bool isAscending, string query, string sortText)
		{
			var sortType = isAscending ? "ASC" : "DESC";
			var length = 13;
			var offset = length * (pageNumber - 1);
			var sortColumn = "avgRating";
			var recipes = new List<Recipe>();

			if (sortText.ToUpper() == "TITLE")
				sortColumn = "title";


			MySqlDatabase.ExecuteReader(
                $"select recipes.*, accounts.username from recipes join accounts on accounts.id = recipes.accountId where accountId = {accountId}" +
				(query == ""? " " : $" title like '%{query}%' ") + 
                $"order by {sortColumn} {sortType} limit {offset}, {length}", reader =>
			{
				recipes.Add(NewRecipeFromReader(reader));
			});

			return recipes;
		}

        public void Create(Recipe recipe)
        {
            var parsedDirections = String.Join(";", recipe.Directions);
            var parsedIngredients = String.Join(";", recipe.Ingredients);
            var query = $"insert into recipes(title, accountId, createdAt, description, image, calories, directions, ingredients) " +
                $"values('{recipe.Title}', {recipe.AccountId}, current_timestamp(), '{recipe.Description}', '{recipe.Title}', {recipe.Calories}, '{parsedDirections}', '{parsedIngredients}')";

            MySqlDatabase.ExecuteNoneQuery(query);
        }

        public List<Recipe> GetAll(int pageNumber, bool isAscending, string query, string sortText)
		{
			var sortType = isAscending ? "ASC" : "DESC";
			var length = 13;
			var offset = length * (pageNumber - 1);
			var sortColumn = "avgRating";
			var recipes = new List<Recipe>();

			if (sortText.ToUpper() == "TITLE")
				sortColumn = "title";

            var sql = $"select recipes.*, accounts.username from recipes join accounts on accounts.id = recipes.accountId where public = false" +
                (query == "" ? " " : $" title like '%{query}%' ") +
                $"order by {sortColumn} {sortType} limit {offset}, {length}";

			MySqlDatabase.ExecuteReader(sql, reader =>
			{
				recipes.Add(NewRecipeFromReader(reader));
			});

			return recipes;
		}

		public List<Recipe> GetByQuery(string query)
        {
            if (query == null)
                return GetAll();
            return GetAll().Where(recipe => recipe.Title.Contains(query)).ToList();
        }

        public Recipe GetRecipeById(int id)
        {
            Recipe recipe = null;
            MySqlDatabase.ExecuteReader($"select recipes.* accounts.username from recipes join accounts on accounts.id = recipes.accountId where id = {id}", reader =>
            {
                recipe = NewRecipeFromReader(reader);
            });
            return recipe;
        }

		public List<Recipe> GetAllByAccountId(int accountId)
		{
            var recipes = new List<Recipe>();

            MySqlDatabase.ExecuteReader($"select recipes.*, accounts.username from recipes join accounts on accounts.id = recipes.accountId where accounts.id = {accountId}", reader =>
            {
                recipes.Add(NewRecipeFromReader(reader));
            });

            return recipes;
		}
	}
}
