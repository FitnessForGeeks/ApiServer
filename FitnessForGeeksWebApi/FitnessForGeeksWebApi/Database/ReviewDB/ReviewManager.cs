using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessForGeeksWebApi.Database.ReviewDB
{
    public class ReviewManager : IDatabaseManager<Review>
    {
        public int? GetAmountOfReviewsByRecipeId(object id)
        {
            int amount = 0;
            MySqlDatabase.ExecuteReader($"select count(*) amount from reviews where recipeId = {id}", reader =>
            {
                amount = (int)MySqlDatabase.GetValueOrNull<long>(reader, "amount").Value;
            });
            return amount;
        }

        private Review NewReviewFromReader(MySqlDataReader reader)
        {
            return new Review(
               MySqlDatabase.GetValueOrNull<int>(reader, "id").Value,
               MySqlDatabase.GetValueOrNull<int>(reader, "accountId").Value,
               MySqlDatabase.GetValueOrNull<int>(reader, "recipeId").Value,
               MySqlDatabase.GetValue<string>(reader, "text"),
               MySqlDatabase.GetValueOrNull<double>(reader, "rating").Value,
               MySqlDatabase.GetValue<string>(reader, "username"),
               MySqlDatabase.GetValueOrNull<DateTime>(reader, "createdAt").Value
            );
        }

        public List<Review> GetAllByRecipeId(int id)
        {
            var reviews = new List<Review>();

            MySqlDatabase.ExecuteReader($"select reviews.*, accounts.username from reviews join accounts on accounts.id = reviews.accountId where recipeId = {id}", reader =>
            {
                reviews.Add(NewReviewFromReader(reader));
            });

            return reviews;
        }
    }
}
