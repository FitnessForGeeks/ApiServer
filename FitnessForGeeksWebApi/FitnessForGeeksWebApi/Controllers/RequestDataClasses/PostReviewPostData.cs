using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessForGeeksWebApi.Controllers.RequestDataClasses
{
    public class PostReviewPostData
    {
		public int AccountId { get; set; }
		public int RecipeId { get; set; }
		public double Rating { get; set; }
		public string Text { get; set; }
	}
}
