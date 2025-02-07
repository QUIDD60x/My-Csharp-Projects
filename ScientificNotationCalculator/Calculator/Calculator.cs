using System;
using System.Collections;
using System.Collections.Generic;
using System.Formats.Tar;
using System.Net.Sockets;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using System.Text;

namespace ScientificNotationCalculator
{
    class Calculator // Made this originally hoping for extra credit in a math class (turns out my professor doesn't allow extra credit).
    {
        static void Main(string[] args)
        {
            Console.WriteLine(@"
 _____           _       _       _    ____   _____        
|  _  |         (_)     | |     | |  / ___| |  _  |       
| | | |  _   _   _    __| |   __| | / /___  | |/' | __  __
| | | | | | | | | |  / _` |  / _` | | ___ \ |  /| | \ \/ /
\ \/' / | |_| | | | | (_| | | (_| | | \_/ | \ |_/ /  >  < 
 \_/\_\  \__,_| |_|  \__,_|  \__,_| \_____/  \___/  /_/\_\");
            Console.WriteLine("---------- Scientific Notation Converter ----------");
            bool keepRunning = true;
            while(keepRunning)
            {
                Console.WriteLine("Please input your whole number!");
                string userInput = Console.ReadLine();
                if (double.TryParse(userInput, out double userNumber))
                {
                    string scientificNotation = Convert2SN(userNumber);
                    Console.WriteLine($"Scientific Notation: {scientificNotation}");
                }
                else 
                {
                    Console.WriteLine("Invalid input, please input a valid number (0-999999999, no decimals).");
                }
                if (userInput.ToLower() == "exit")
                {
                    keepRunning = false;
                }
            }
        }

        static string Convert2SN(double userNumber)
        {
            if (userNumber == 0)
                return "0 * 10^0"; // There's no point in doing an operation for this

            int exponent = (int)Math.Floor(Math.Log10(Math.Abs(userNumber))); // This defines the exponent, basically divides the number you provided by 10 until it's a single real number, then sets the amount of times it could be divided as the exponent variable.

            double coefficient = userNumber / Math.Pow(10, exponent); // This gives the single number (i.e 6.4) to use, it's basically solving the problem. As you can see it divides your inputted number with 10 to the power of your exponent.

            return $"{coefficient:F5} * 10^{exponent}";
        }
    }
}