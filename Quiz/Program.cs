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

            if (UserController.Name == string.Empty || UserController.Name == null) UserController.Name = "Bezimienny";

            Console.Clear();

            await _getTheMenuOption();

    
        }

        /// <summary>
        /// Metoda pobiera i wyświetla statystyki
        /// </summary>
        /// <returns>void</returns>
        private static async Task _showStats()
        {
            Console.Clear();
            Console.WriteLine("Pobieram, proszę czekać");
            var results = await ResultController.GetResultsAsync();
            Console.Clear();
            Console.WriteLine("Indeks Gracz Kategoria Punkty Data");
            results.Each((e, i) =>
            {
                Console.WriteLine($"{(i + 1)}. {e.UserName} {e.Category.Name} {e.Points} {e.CreatedOn.ToString("dd'/'MM'/'yyyy")}");
            });

            Console.WriteLine("0. Wróć do menu, inny klawisz zakończy działanie programu");
            Console.Write("Wybieram: ");
            int.TryParse(Console.ReadLine(), out int choose);
            if (choose == 0)
            {
                Console.Clear();
                await _getTheMenuOption();
            }
            else Environment.Exit(0);
        }

       /// <summary>
       /// Wyświetla menu
       /// </summary>
       /// <returns>void</returns>
        private async static Task _getTheMenuOption()
        {
            Console.Clear();
            bool isCorrect = false;
            int choose = 0;
            while (!isCorrect)
            {
                Console.WriteLine($"Witaj {UserController.Name}! \n Co chcesz zrobić: ");
                Console.WriteLine($" 1. Zagraj! \n 2. Zobacz ranking \n 3. Zakończ program");
                Console.Write("Wybierz: ");
                isCorrect = int.TryParse(Console.ReadLine(), out choose);
                if (isCorrect) isCorrect = choose == 1 || choose == 2 || choose == 3 ? true : false;
                if (!isCorrect) Console.Clear();
            }

            switch (choose)
            {
                case 1: await _startQuiz(); break;
                case 2: await _showStats(); break;
                case 3: Environment.Exit(0); break;
                default: await _getTheMenuOption(); break;
            }
     
        }

        /// <summary>
        /// Rozpoczęcie quizu
        /// </summary>
        /// <returns>void</returns>
        private static async Task _startQuiz()
        {
            Console.Clear();
            Console.WriteLine("Pobieram, proszę czekać");
            List<Category> categories = await CategoryController.GetCategories();
            Console.Clear();

            bool isCategoryNumberCorrect = false;
            while(!isCategoryNumberCorrect)
            {
                categories.Each((element, index) => Console.WriteLine($"{ index + 1 }. {element.Name}"));
                Console.Write("Wybieram kategorię: ");
                isCategoryNumberCorrect = int.TryParse(Console.ReadLine(), out int choose);
                if (isCategoryNumberCorrect && (choose == 1 || choose == 2)) CategoryController.ChoosedCategory = categories[choose - 1];
                else isCategoryNumberCorrect = false;
                Console.Clear();
            }

           
           
            bool isCorrect = false;
            while (!isCorrect) {
                Console.WriteLine("Wybierz poziom trudności: ");
                Console.WriteLine("1. Łatwy");
                Console.WriteLine("2. Średni");
                Console.WriteLine("3. Trudny");
                Console.Write("Wybieram poziom: ");
                isCorrect = int.TryParse(Console.ReadLine(), out int hardLevel);
                if ((hardLevel < 1 || hardLevel > 3) && isCorrect)
                {
                    isCorrect = false;
                    Console.Clear();
                }
                else QuizController.Level = (QuestionLevelEnum)hardLevel - 1;
            }


            await _quiz();

        }

        /// <summary>
        /// Przeprowadzenie właściwej gry
        /// </summary>
        /// <returns>void</returns>
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
            await ResultController.SaveQuizResult();
            Console.WriteLine("Twój wynik został zapisany.");
            Console.WriteLine("0. Wróć do menu, inny klawisz zakończy działanie programu");
            Console.Write("Wybieram: ");
            int.TryParse(Console.ReadLine(), out int choose);
            if(choose == 0)
            {
                Console.Clear();
                await _getTheMenuOption();
            }
            else Environment.Exit(0);

        }
    }
}
