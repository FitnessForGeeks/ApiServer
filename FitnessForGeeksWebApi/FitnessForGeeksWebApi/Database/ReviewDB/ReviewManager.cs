using FitnessForGeeksWebApi.Controllers.RequestDataClasses;
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

		public List<Review> GetAllByRecipeId(int id, int offset, int length)
		{
			var reviews = new List<Review>();

            MySqlDatabase.ExecuteReader($"select reviews.*, accounts.username from reviews join accounts on accounts.id = reviews.accountId where recipeId = {id} limit {offset}, {length}", reader =>
            {
                reviews.Add(NewReviewFromReader(reader));
            });

            return reviews;
		}

		public object GetAllByRecipeId(int id, int offset, int length, string sortText, bool isAscending)
		{
			var reviews = new List<Review>();
			var sortType = isAscending ? "ASC" : "DESC";
			var columnName = "createdAt";

			if (sortText.ToUpper() == "RATING")
				columnName = "rating";

            MySqlDatabase.ExecuteReader($"select reviews.*, accounts.username from reviews join accounts on accounts.id = reviews.accountId " +
				$"where recipeId = {id} order by {columnName} {sortType} limit {offset}, {length}", reader =>
            {
                reviews.Add(NewReviewFromReader(reader));
            });

            return reviews;
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

		public int? Create(PostReviewPostData data)
		{
			int? id = null;
			MySqlDatabase.ExecuteReader($"select insertReview({data.AccountId},{data.RecipeId},'{data.Text}',{data.Rating}) id;", reader =>
			{
				if (reader.HasRows)
					id = reader.GetInt32("id");
			});
			return id;
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
