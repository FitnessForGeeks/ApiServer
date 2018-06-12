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

            MySqlDatabase.ExecuteReader($"select * from eatenRecipes where accountId = {accountId}", reader => {
                eatenRecipes.Add(new EatenRecipe(
                    MySqlDatabase.GetValueOrNull<int>(reader, "id"),
                    MySqlDatabase.GetValueOrNull<int>(reader, "accountId"),
                    MySqlDatabase.GetValueOrNull<int>(reader, "recipeId"),
                    MySqlDatabase.GetValueOrNull<DateTime>(reader, "date")
                ));
            });

            return eatenRecipes;
        }

        /// <summary>
        /// Returns all recipes that were eaten by an account today 
        /// </summary>
        /// <param name="accountId">The id of the account</param>
        /// <returns></returns>
        public List<EatenRecipe> GetCurrentByAccountId(int accountId)
        {
            var eatenRecipes = new List<EatenRecipe>();
            Debug.WriteLine(DateTime.Now);

            MySqlDatabase.ExecuteReader($"select * from eatenRecipes where accountId = {accountId} and date = str_to_date({DateTime.Now.ToString("dd/mm/yyyy")}, 'dd/mm/yyyy')", reader => {
                eatenRecipes.Add(new EatenRecipe(
                    MySqlDatabase.GetValueOrNull<int>(reader, "id"),
                    MySqlDatabase.GetValueOrNull<int>(reader, "accountId"),
                    MySqlDatabase.GetValueOrNull<int>(reader, "recipeId"),
                    MySqlDatabase.GetValueOrNull<DateTime>(reader, "date")
                ));
            });

            return eatenRecipes;
        }
    }
}
