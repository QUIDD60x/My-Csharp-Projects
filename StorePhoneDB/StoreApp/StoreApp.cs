// UNFINISHED : Ran into a bit of a roadblock on the last features, no idea how to edit the information or select value info.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

namespace StorePhoneDB
{
    class StoreApp
    {
        static Dictionary<long, string> phonebook = new Dictionary<long, string>();

        static string userName = "Steven V";
        static string password = "B599reG2w"; // Decided against adding any protection because already demonstrated I can do it before (and lazy).
        static void Main(string[] args)
        {
            phonebook.Add(5099641253, "Steven V");
            phonebook.Add(9107890695, "Rumaisa K");
            phonebook.Add(5094453830, "Adam L");
            phonebook.Add(2121121111, "Bobby B");

            Console.WriteLine("-------------------- TrueSec Number Catalog --------------------");
            Console.WriteLine(@"
████████╗██████╗ ██╗   ██╗███████╗███████╗███████╗ ██████╗
╚══██╔══╝██╔══██╗██║   ██║██╔════╝██╔════╝██╔════╝██╔════╝
   ██║   ██████╔╝██║   ██║█████╗  ███████╗█████╗  ██║     
   ██║   ██╔══██╗██║   ██║██╔══╝  ╚════██║██╔══╝  ██║     
   ██║   ██║  ██║╚██████╔╝███████╗███████║███████╗╚██████╗
   ╚═╝   ╚═╝  ╚═╝ ╚═════╝ ╚══════╝╚══════╝╚══════╝ ╚═════╝");
            Console.WriteLine("----------------------------------------------------------------");

            bool keepRunning = true;
            while (keepRunning)
            {
                Console.WriteLine("Welcome to the TrueSec phone number catalog!\nPlease type 1 to view all saved numbers.\nType 2 to search for an employee.\nType 3 to add a name and number to the database (login required).\nType 4 to exit the program.");
                string choice = Console.ReadLine();
                if (int.TryParse(choice, out int userChoice))
                {
                    switch (userChoice)
                    {
                        case 1:
                            Console.WriteLine("You chose to view all saved numbers.");
                            ViewNumbers();
                            break;    
                        case 2:
                            Console.WriteLine("You chose to search for an employee.\nPlease enter their name or number.");
                            NameSearch();
                            break;
                        case 3:
                            Console.WriteLine("You chose to add a number. Please log in.");
                            Login();
                            break;
                        case 4:
                            Console.WriteLine("Quitting program now!");
                            keepRunning = false;
                            break;
                        default:
                            Console.WriteLine("Please select a provided option.\n----------------------------------------");
                            break;
                    }            
                }
            }

            static void ViewNumbers()
            {
                Console.WriteLine("\n--- All Employee Numbers ---");
                if (phonebook.Count == 0)
                {
                    Console.WriteLine("No currently registered employees.");
                }
                foreach (KeyValuePair<long, string> number in phonebook) // Could also do var entry in phonebook
                {
                    Console.WriteLine($" | Number (" + number.Key + ") belongs to " + number.Value + " | ");
                    Console.WriteLine("----------------------------------------");
                }
                
            }

            static void NameSearch()
            {
                if (phonebook.Count == 0)
                {
                    Console.WriteLine("No entries are currently available.");
                    return;
                }

                Console.WriteLine("\n--- Name search ---\nPlease enter the phone number/name of the employee you want to find.\tFormat: phone number with no spaces, full first name with abbreviated last.");
                string userChoice = Console.ReadLine();
                bool found = false;
                if (long.TryParse(userChoice, out long phoneNumber))
                {
                    foreach (var entry in phonebook)
                    {
                        if (entry.Key == phoneNumber)
                        {
                            Console.WriteLine($"Phone number {phoneNumber} is associated with {phoneNumber}."); // TODO: Figure out how to do this part idk
                            found = true;
                        }
                    }
                }
                else
                {
                    foreach (var entry in phonebook)
                    {
                        if (entry.Value == userChoice)
                        {
                            Console.WriteLine($"{userChoice}'s number is {entry.Key}.\n----------------------------------------");
                            found = true;
                        }
                    }
                }

                if (!found)
                {
                    Console.WriteLine("Number was not found in our system. Please try again.\n----------------------------------------");
                }                
            }

            static void Login()
            {
                Console.WriteLine("Please enter your account name.");
                string inputUserName = Console.ReadLine();
                if (inputUserName == userName)
                {
                    Console.WriteLine($"Welcome, {userName}! Please provide your password.");
                    string inputPassword = Console.ReadLine();
                    if (inputPassword == password)
                    {
                        Console.WriteLine("Logged in successfully!");
                        AddEmployees();
                    }
                    else
                    {
                        Console.WriteLine($"The password you have entered ({inputPassword}) is incorrect.");
                    }
                }
            }

            static void AddEmployees()
            {
                Console.WriteLine("----------------------------------------\nWould you like to add, remove, or edit an employee?");
                string choice = Console.ReadLine().ToLower();
                switch (choice)
                {
                    case "add":
                        Console.WriteLine("Please provide the employee name.");
                        string empName = Console.ReadLine();
                        Console.WriteLine($"You have entered: {empName}. Now please provide their phone number.");
                        string empNumber = Console.ReadLine();
                        if (long.TryParse(empNumber, out long phoneNumber))
                        {
                            phonebook.Add(phoneNumber, empName);
                        }
                        else
                        {
                            Console.WriteLine("You did not give an appropriate number.");
                        }
                    break;

                    case "remove":
                        Console.WriteLine("Please give the employee number you'd like to remove.");
                        string employeeNumber = Console.ReadLine();
                        if (long.TryParse(employeeNumber, out long EmployeeNumberSearch))
                        { 
                            if (phonebook.ContainsKey(EmployeeNumberSearch))
                            {
                                Console.WriteLine($"You provided the number {EmployeeNumberSearch}, associated with the name");
                            }
                        }
                    break;

                    case "edit":
                    break;
                }
            }

        }
    }
}