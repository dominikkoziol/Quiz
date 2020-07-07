using Quiz.Extensions;
using QuizLogic;
using QuizLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz
{
    class Program
    {
        /// <summary>
        /// Główna metoda programu, wywołuję w niej podmetodę ExecuteProgram() ponieważ main nie może być async
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args) => ExecuteProgram().GetAwaiter().GetResult();
        
        public async static Task ExecuteProgram()
        {
            Console.Write($"Witaj, w aplikacji Quiz! Podaj swój pseudonim: ");
            User.Name = Console.ReadLine();

            Console.Clear();

            Console.WriteLine($"Witaj {User.Name}! \n Co chcesz zrobić: ");
            Console.WriteLine($" 1. Zagraj! \n 2. Zobacz ranking");

            int.TryParse(Console.ReadLine(), out int choose);

            switch(choose)
            {
                case 1: await _startQuiz(); break;
            }
        }

        private static async Task _startQuiz()
        {
            Console.Clear();

            List<Category> categories = await CategoryController.GetCategories();

            categories.Each((element, index) => Console.WriteLine($"{ index + 1 }. {element.Name}"));

            Console.Write("Wybieram kategorię: ");
            int.TryParse(Console.ReadLine(), out int choose);

            CategoryController.ChoosedCategory = categories[choose - 1];

            await _quiz();

        }


        private static async Task _quiz()
        {
            Console.Clear();

            Console.WriteLine($"Wybrałeś: {CategoryController.ChoosedCategory.Name}");

            List<Question> questions = await QuizController.GetQuestions(CategoryController.ChoosedCategory.Id);
            questions.Each((element, index) => {
                Console.WriteLine($"{index + 1}. {element.QuestionContent}");
                Console.WriteLine($" 1. {element.AnswerA}");
                Console.WriteLine($" 2. {element.AnswerB}");
                Console.WriteLine($" 3. {element.AnswerC}");
                Console.WriteLine($" 4. {element.AnswerD}");
            });
        }
    }
}
