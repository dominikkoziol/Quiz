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
    public static class QuizController
    {
        

        /// <summary>
        /// Pobiera pytania z danej kategorii losuje i zwraca 7
        /// </summary>
        /// <param name="categoryId">Id kategorii</param>
        /// <returns>Lista pomieszanych pytań</returns>
        public static async Task<List<Question>> GetQuestions(Guid categoryId)
        {
            using (var context = new QuizEntityContext())
            {
                return await context.Questions.Where(q => q.CategoryId == categoryId).OrderBy(q => new Guid()).Take(7).ToListAsync();
            }
        }


    }
}
