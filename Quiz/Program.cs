using Quiz.Extensions;
using QuizLogic;
using QuizLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static QuizLogic.Models.Question;

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
            UserController.Name = Console.ReadLine();

            Console.Clear();

            int choose = _getTheMenuOption();

            switch (choose)
            {
                case 1: await _startQuiz(); break;
                default: _getTheMenuOption(); break;
            }
        }

        private static int _getTheMenuOption()
        {
            bool isCorrect = false;
            int choose = 0;
            while (!isCorrect)
            {
                Console.WriteLine($"Witaj {UserController.Name}! \n Co chcesz zrobić: ");
                Console.WriteLine($" 1. Zagraj! \n 2. Zobacz ranking");

                isCorrect = int.TryParse(Console.ReadLine(), out choose);
                if (isCorrect) isCorrect = choose == 1 || choose == 2 ? true : false;
                if (!isCorrect) Console.Clear();
            }

            return choose;
        }

        private static async Task _startQuiz()
        {
            Console.Clear();

            List<Category> categories = await CategoryController.GetCategories();

            categories.Each((element, index) => Console.WriteLine($"{ index + 1 }. {element.Name}"));

            Console.Write("Wybieram kategorię: ");
            int.TryParse(Console.ReadLine(), out int choose);

            CategoryController.ChoosedCategory = categories[choose - 1];
            Console.Clear();
            Console.WriteLine("Wybierz poziom trudności: ");
            Console.WriteLine("1. Łatwy");
            Console.WriteLine("2. Średni");
            Console.WriteLine("3. Trudny");

            int.TryParse(Console.ReadLine(), out int hardLevel);
            QuizController.Level = (QuestionLevelEnum)hardLevel -1;


            await _quiz();

        }


        private static async Task _quiz()
        {
            Console.Clear();

            Console.WriteLine($"Wybrałeś: {CategoryController.ChoosedCategory.Name}");

            List<Question> questions = await QuizController.GetQuestions(CategoryController.ChoosedCategory.Id);
            questions.Each((element, index) =>
            {
                Console.WriteLine($"{index + 1}. {element.QuestionContent}");
                Console.WriteLine($" 1. {element.AnswerA}");
                Console.WriteLine($" 2. {element.AnswerB}");
                Console.WriteLine($" 3. {element.AnswerC}");
                Console.WriteLine($" 4. {element.AnswerD}");

                int answer = 0;
                while(!QuizController.GetAnswer(element, answer))
                {
                    Console.Write("Podaj odpowiedź: ");
                    int.TryParse(Console.ReadLine(), out answer);
                }
                Console.Clear();

            });


       
            int result = QuizController.GetResult(questions);
            
            Console.WriteLine($"Zdobyłeś: {result} na {((int)QuizController.Level + 1) * questions.Count()} punktów, gratulacje!");
            Console.WriteLine();
        }
    }
}
