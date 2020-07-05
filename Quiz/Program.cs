using QuizLogic;
using System;
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

            //var x = await CategoryController.GetCategories();

            //Console.WriteLine(x.First().Name);
        }
    }
}
