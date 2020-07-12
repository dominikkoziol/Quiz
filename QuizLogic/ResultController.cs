using Microsoft.EntityFrameworkCore;
using QuizLogic.Context;
using QuizLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizLogic
{
    public static class ResultController
    {

        public static int CollectedPoints = 0;

        /// <summary>
        /// Zapisuje wynik quizu
        /// </summary>
        /// <returns>void</returns>
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

        /// <summary>
        /// Pobieranie wyniki rozgrywek
        /// </summary>
        /// <returns>List<Result></returns>
        public async static Task<List<Result>> GetResultsAsync()
        {
            using (var context = new QuizEntityContext())
            {
                return await context.Results.AsNoTracking().Include(q => q.Category).OrderByDescending(q => q.CreatedOn).ToListAsync();
            }

        }
    }
}
