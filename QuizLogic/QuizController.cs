﻿using Microsoft.EntityFrameworkCore;
using QuizLogic.Context;
using QuizLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static QuizLogic.Models.Question;

namespace QuizLogic
{
    public static class QuizController
    {
        /// <summary>
        /// Przechowuje poziom trudności rozgrywanego quizu
        /// </summary>
        public static QuestionLevelEnum Level;

        /// <summary>
        /// Pobiera pytania z danej kategorii losuje i zwraca 7
        /// </summary>
        /// <param name="categoryId">Id kategorii</param>
        /// <returns>Lista pomieszanych pytań</returns>
        public static async Task<List<Question>> GetQuestions(int categoryId)
        {
            using (var context = new QuizEntityContext())
            {
                return await context.Questions.Where(q => q.CategoryId == categoryId && q.QuestionLevel == Level).OrderBy(q => new Guid()).Take(7).ToListAsync();
            }
        }
        
        /// <summary>
        /// Udzielanie odpowiedzi, sprawdza czy podana wartość jest prawidłowa
        /// </summary>
        /// <param name="question"></param>
        /// <param name="answer"></param>
        /// <returns>bool</returns>
        public static bool GetAnswer(Question question, int answer)
        {
            bool isCorrect = answer > 0 && answer < 5;
            if (isCorrect)
            {
                question.MarkedAnswer = answer;
                return true;
            }

            else return false;
        }

        /// <summary>
        /// Wynik quizu
        /// </summary>
        /// <param name="questions">List<Question>z udzielonymi odpowiedziami</param>
        /// <returns>INT - liczba punktów za quiz</returns>
        public static int GetResult(List<Question> questions)
        {
            int points = 1;
            if (Level == QuestionLevelEnum.Medium) points = 2;
            if (Level == QuestionLevelEnum.Hard) points = 3;

            int result = 0;
            questions.ForEach(element => {
                if(element.CorrectAnswer == element.MarkedAnswer)
                {
                    result += points;
                }
            });
            ResultController.CollectedPoints = result;
            return result;
        }
        

    }
}
