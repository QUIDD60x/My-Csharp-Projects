using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;


namespace ListSelector
{
    class ListSelector
    {
        static void Main(string[] args) // VERY early C# program, using it to figure out how to store numbers.
        {   
            List<int> numbers = [];
            bool keepRunning = true;
            while (keepRunning)
            {
                Console.WriteLine("Number Saving Test\nType 1 to create a new number, type 2 to display saved numbers, and type 3 to quit.");
                string choice = Console.ReadLine();
                if (int.TryParse(choice, out int choiceInt))
                if (choiceInt > 3 || choiceInt < 1)
                {
                    Console.WriteLine("You did not give a valid choice.");
                    keepRunning = false;
                    continue;
                }
                switch (choiceInt)
                {
                    case 1:
                        Console.WriteLine("You chose to save a number!");
                        Console.WriteLine("--------------------\nPlease choose a number to save:");
                        string savedNum = Console.ReadLine();
                        if (int.TryParse(savedNum, out int intSavedNum))
                        {
                            numbers.Add(intSavedNum);
                            Console.WriteLine($"Number {intSavedNum} has been saved! Current list is: {string.Join(", ", numbers)}");
                        }
                        else
                        {
                            Console.WriteLine("Your number was invalid, or you inputted a letter.");
                        }
                        continue;
                    case 2:
                        if (numbers.Count > 0)
                        {
                            Console.WriteLine("You chose to display saved numbers.");
                            foreach (int number in numbers)
                            {
                                Console.WriteLine($"--------------------\nYour saved numbers are:\n{string.Join(", ", numbers)}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("You have no saved numbers.");
                        }
                        continue;
                    case 3:
                        Console.WriteLine("Quitting program.");
                        keepRunning = false;
                        continue;                           
                }
            }
        }
    }
}