using System;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;


namespace NumberRoulette
{
    class NumberRoulette // Basic roulette game with Lists! 
    {
        static void Main(string[] args)
        {
            Random random = new Random(); // Start with declaring the random method, which I'll use for generating random numbers (duh)
            List<int> numbers = new List<int>(); // Initilizing our list of numbers

            Console.WriteLine("Generating numbers..."); // For debugging
            bool keepRunning = true; // Making a loop so it's easier to test
            while (keepRunning)
            {
                for (int i = 0; i < 10; i++) // Allows for the selection of numbers from 1 through 10
                {
                    numbers.Add(random.Next(0,10)); // Selects one random number from 1 through 10 
                    // Console.WriteLine("Generated number is " + string.Join(", ", numbers));  FOR DEBUGGING so you can see the generated number (cheater!)
                    Console.WriteLine("Numbers generated!"); // Debugging
                    Console.WriteLine("----- Quidd's list roulette ------\nPick a number between 1 and 10.");
                    string pickedNumber = Console.ReadLine();
                    if (int.TryParse(pickedNumber, out int playerChoice)) // I don't like keeping numerical values as a string, so I parse it into an int, then convert it to another variable.
                    {
                        if (numbers.Contains(playerChoice)) // If the players choice of number is one of the randomly generated they lose.
                        {
                            Console.Beep();
                            Console.WriteLine("You lose! I'm injecting a virus into your system now."); // No actual virus duh
                        }
                        else if (playerChoice <= 10 && playerChoice >= 1) // checks to see if their choice is equal to or less than 10 & equal to or more than 1, if it passes the first check then boom they've won!
                        {
                            Console.WriteLine("You win! Try again.");
                        }
                        if (playerChoice >= 11 || playerChoice <= 0) // Makes sure a player isn't just selecting a number out of the applicable range.
                        {
                            Console.WriteLine("Nice try, you lose.");
                        }
                    }
                    numbers.Clear(); // Clears the currently listed numbers (if removed it'll add more and more, more challenge?)
                }
            }   
        }    
    }
}