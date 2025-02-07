using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Formats.Tar;
using System.Net.Sockets;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using System.Text;

namespace DateCalculator
{
    class DateCalc // I actually forgot why I made this, but to the github it goes!
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
            Console.WriteLine("--------------------\nHow many days until? Quidds' Calendar day calculator\n--------------------");
            DateTime today = DateTime.Now;       
            Console.WriteLine($"Todays date is {today.ToShortDateString()}.");  
            string todayDate = $"{today.ToShortDateString()}";
            Console.WriteLine("Please input the date you'd like to calculate from, in MM/DD/YYYY format.");
            string input = Console.ReadLine();

            Console.WriteLine($"You input {input}.");

            if (DateTime.TryParse(input, out DateTime userDate))
            {
                TimeSpan difference = userDate.Subtract(today);
                Console.WriteLine($"The difference between {DateTime.Now} and {userDate.Date} is {difference.Days} days.");
            }
            else
            {
                Console.WriteLine("Invalid format. Please make sure your date was inputted in the format 'DD/MM/YYYY'");
            }
        }
    }
}