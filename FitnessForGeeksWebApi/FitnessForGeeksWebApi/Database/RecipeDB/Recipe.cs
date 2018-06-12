using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessForGeeksWebApi.RecipeDB
{
    public class Recipe
    {
        public Recipe(int? id, string title, string image, int? calories)
        {
            Id = id;
            Title = title;
            Image = image;
            Calories = calories;
        }

        public int? Id { get; }
        public string Title { get; }
        public string Image { get; }
        public int? Calories { get; }
    }
}
