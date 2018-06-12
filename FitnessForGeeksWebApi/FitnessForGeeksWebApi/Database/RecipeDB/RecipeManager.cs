using System;
using System.Collections.Generic;
using FitnessForGeeksWebApi.Database;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace FitnessForGeeksWebApi.RecipeDB
{
    public class RecipeManager : IDatabaseManager<Recipe>
    {
        public List<Recipe> GetAll()
        {
            var recipes = new List<Recipe>();

            MySqlDatabase.ExecuteReader("select * from recipes", reader =>
            {
                recipes.Add(NewRecipeFromReader(reader));
            });

            return recipes;
        }

        private Recipe NewRecipeFromReader(MySqlDataReader reader)
        {
            return new Recipe(
                MySqlDatabase.GetValueOrNull<int>(reader, "id"),
                MySqlDatabase.GetValue<String>(reader, "title"),
                MySqlDatabase.GetValue<String>(reader, "image"),
                MySqlDatabase.GetValueOrNull<int>(reader, "calories")
            );
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
            MySqlDatabase.ExecuteReader($"select * from recipes where id = {id}", reader =>
            {
                recipe = NewRecipeFromReader(reader);
            });
            return recipe;
        }
    }
}
