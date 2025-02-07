using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Formats.Tar;
using System.Net.Sockets;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using System.Text;

namespace Calendar
{
    class CalendarApp // Very basic calendar app I made just to keep up on my C#, was mainly a test to make sure I knew how to divide a string based on a given character.
    {
        static void Main(string[] args)
        {
            bool secret1 = false;
            bool secret2 = false;
            bool secret3 = false;
            string dayAbbreviation = "";
            string displayMonth = "";

            Console.WriteLine(@"
 _____           _       _       _    ____   _____        
|  _  |         (_)     | |     | |  / ___| |  _  |       
| | | |  _   _   _    __| |   __| | / /___  | |/' | __  __
| | | | | | | | | |  / _` |  / _` | | ___ \ |  /| | \ \/ /
\ \/' / | |_| | | | | (_| | | (_| | | \_/ | \ |_/ /  >  < 
 \_/\_\  \__,_| |_|  \__,_|  \__,_| \_____/  \___/  /_/\_\");

            bool keepRunning = true;
            while(keepRunning)
            {
                Console.WriteLine("----------------------------------------\nQuidd's Online Calendar\nPlease start by entering todays date, in the format DD/MM/YYYY.");
                string[] input = Console.ReadLine().Split('/');

                if (input.Length != 3)
                {
                    Console.WriteLine("You did not input enough dates, or did an incorrect format.");
                    break;
                }

                string day = input[0];
                string month = input[1];
                string year = input[2];

                if (int.TryParse(day, out int dayInt))
                {
                    if (dayInt > 31 || dayInt < 1)
                    {
                        Console.WriteLine("Your day is invalid, please try again.");
                        break;
                    }

                    switch(dayInt)
                    {
                        case 1:
                            dayAbbreviation = "st";
                            break;
                        case 2:
                            dayAbbreviation = "nd";
                            break;
                        case 3:
                            dayAbbreviation = "rd";
                            break;
                        default:
                            dayAbbreviation = "th";
                            break;
                    }

                    if (dayInt == 25)
                    {
                        secret1 = true;
                    }
                }

                if (int.TryParse(month, out int monthInt))
                {
                    if (monthInt > 12 || monthInt < 1)
                    {
                        Console.WriteLine("Your month is invalid, please try again.");
                        break;
                    }

                    switch(monthInt)
                    {
                        case 01:
                            displayMonth = "January";
                            break;
                        case 02:
                            displayMonth = "Febuary";
                            break;
                        case 03:
                            displayMonth = "March";
                            break;
                        case 04:
                            displayMonth = "April";
                            break;
                        case 05:
                            displayMonth = "May";
                            break;
                        case 06:
                            displayMonth = "June";
                            break;
                        case 07:
                            displayMonth = "July";
                            break;
                        case 08:
                            displayMonth = "August";
                            break;
                        case 09:
                            displayMonth = "September";
                            break;
                        case 10:
                            displayMonth = "October";
                            break;
                        case 11:
                            displayMonth = "November";
                            break;
                        case 1203:
                            displayMonth = "December";
                            break;
                    }

                    if (monthInt == 05)
                    {
                        secret2 = true;
                    }
                }

                if (int.TryParse(year, out int yearInt))
                {
                    if (yearInt > 2025 || yearInt < 1000)
                    {
                        Console.WriteLine("Your year is invalid, please try again.");
                        break;
                    }

                    if (yearInt == 2006)
                    {
                        secret3 = true;
                    }
                }

                Console.WriteLine(@" ________________________________________");
                Console.WriteLine(@"|                                        |"); 
                Console.WriteLine(@"|                                        |");               
                Console.WriteLine($"|\t\t{day}/{month}/{year}\t\t |");                    
                Console.WriteLine(@"|                                        |");                   
                Console.WriteLine(@"|________________________________________|");   
                Console.WriteLine($"Would you look at that! it's the {day}{dayAbbreviation} of {displayMonth}.");
                
                if (secret1 && secret2 && secret3)
                {
                    Console.WriteLine("HAPPY BIRTHDAY QUIDD!");
                }

                keepRunning = false;               

            }
            
        }
    }
}