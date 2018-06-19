using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessForGeeksWebApi.Database.EatenRecipeDB
{
    public class EatenRecipeManager : IDatabaseManager<EatenRecipe>
    {
        public List<EatenRecipe> GetByAccountId(int accountId)
        {
            var eatenRecipes = new List<EatenRecipe>();

            MySqlDatabase.ExecuteReader(
				$"select eatenRecipes.*, recipes.calories, recipes.title, recipes.image from eatenRecipes " +
				$"join recipes on recipes.id = eatenRecipes.recipeId " +
				$"where eatenRecipes.accountId = {accountId}",
				reader => {
					eatenRecipes.Add(NewEatenRecipeFromReader(reader));
				}
			);

            return eatenRecipes;
        }

		private EatenRecipe NewEatenRecipeFromReader(MySqlDataReader reader)
		{
			return new EatenRecipe(
				MySqlDatabase.GetValueOrNull<int>(reader, "id"),
				MySqlDatabase.GetValueOrNull<int>(reader, "accountId"),
				MySqlDatabase.GetValueOrNull<int>(reader, "recipeId"),
				MySqlDatabase.GetValueOrNull<int>(reader, "calories"),
				MySqlDatabase.GetValue<string>(reader, "title"),
				MySqlDatabase.GetValue<string>(reader, "image"),
				MySqlDatabase.GetValueOrNull<DateTime>(reader, "date")
			);
		}
        /// <summary>
        /// Returns all recipes that were eaten by an account today 
        /// </summary>
        /// <param name="accountId">The id of the account</param>
        /// <returns></returns>
        public List<EatenRecipe> GetCurrentByAccountId(int accountId)
        {
            var eatenRecipes = new List<EatenRecipe>();

            MySqlDatabase.ExecuteReader($"select eatenRecipes.*, recipes.calories, recipes.title, recipes.image from eatenRecipes " +
				$"join recipes on recipes.id = eatenRecipes.recipeId " +
				$"where eatenRecipes.accountId = {accountId} and eatenRecipes.date = current_date()",
				reader => {
					eatenRecipes.Add(NewEatenRecipeFromReader(reader));
				}
			);

            return eatenRecipes;
        }

		public void Create(int accountId, int recipeId)
		{
			MySqlDatabase.ExecuteNoneQuery($"insert into eatenRecipes(accountId, recipeId, date) values({accountId}, {recipeId}, current_date())");
		}
	}
}
