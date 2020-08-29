using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.ConsoleApp
{
    public abstract class Menu : IMenu
    {       
        public int CloseOption { get; set; }
        public string[] MenuOptions { get; set; }

        public void ConsoleLoop()
        {
            bool running = true;
            while (running)
            {
                ShowOptions(MenuOptions);
                var option = ChooseOption(CloseOption);
                ProcessOption(option);
                if (option != CloseOption)
                {
                    Console.WriteLine("Press any key to continue");
                    Console.ReadLine();
                }
                else
                {
                    running = false;
                }
            }
        }

        public void ShowOptions(string[] menuOptions)
        {
            Console.Clear();
            for (int i = 0; i < menuOptions.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {menuOptions[i]}");
            }
        }

        public int ChooseOption(int options)
        {
            int option;
            Console.WriteLine("\nSelect option by typing number:");
            while (!int.TryParse(Console.ReadLine(), out option) || option < 1 || option > options)
            {
                Console.WriteLine("You must type a number from the ones listed above");
            }
            return option;
        }

        public string GetUserInput(string command)
        {
            Console.WriteLine(command);
            return Console.ReadLine();
        }

        public void ShowItems<T>(List<T> items)
        {
            foreach (var item in items)
            {
                Console.WriteLine($"{item}\n");
            }
        }

        public abstract void ProcessOption(int option);
    }
}
