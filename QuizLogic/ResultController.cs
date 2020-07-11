using QuizLogic.Context;
using QuizLogic.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QuizLogic
{
    public static class ResultController
    {

        public static int CollectedPoints = 0;

        public async static Task SaveQuizResult()
        {
            Result result = new Result()
            {
                CategoryId = CategoryController.ChoosedCategory.Id,
                Points = CollectedPoints,
                UserName = UserController.Name,
                CreatedOn = DateTime.Now
            };

            using(var context = new QuizEntityContext())
            {
                context.Results.Add(result);
                await context.SaveChangesAsync();
            }

        }
    }
}
