using System;
using FitnessForGeeksWebApi.Database;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessForGeeksWebApi.Database.EatenRecipeDB
{
    public class EatenRecipe : IDatabaseObject
    {
        public int? Id { get; }
        public int? AccountId { get; }
        public int? RecipeId { get; }
        public DateTime? Date { get; }

        public EatenRecipe(int? id, int? accountId, int? recipeId, DateTime? date)
        {
            Id = id;
            AccountId = accountId;
            RecipeId = recipeId;
            Date = date;
        }

    }
}
