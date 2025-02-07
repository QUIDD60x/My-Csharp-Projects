using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace PCApp // One of the programs I'm more fond of. I never ended up adding a hashing system to secure the passwords, but it's supposed to be a file writing test (I didn't know how to create and write to a file).
{

    class User // Sets up a user class for the .JSON user file
    {
        public string Username { get; set;}
        public string Password { get; set;}
    }

    class PCApp
    {
        static bool keepRunning = true;
        static bool loggedIn = false;
        static string userName = "";
        static string userPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"PCApp\users.json"); // Initializes a path to the .json file, it's dynamic (will always generate in Appdata.local).

        static void WriteToFile()
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PCApp");
            Directory.CreateDirectory(path);
            Console.WriteLine("Please enter the file name:");
            string fileName = Console.ReadLine();
            path += @"\" + fileName; // Appends the filename to the path (so you'd get `appdata/local/PCApp/filename.txt`).
            
            if(File.Exists(path))
            {
                Console.WriteLine("File already exists. Would you like to overwrite? (Y/N)");
                string userInput = Console.ReadLine().ToLower();
                switch(userInput)
                {
                    case "y":
                    Console.WriteLine("Please enter the text you want to write to the file:");
                    string text = Console.ReadLine();
                    File.WriteAllText(path, text);
                    Console.WriteLine($"File overwritten successfully at " + path);
                    break;
                    case "n":
                        Console.WriteLine("Aborting...");
                    break;
                    default:
                        Console.WriteLine("Invalid Input.");
                    break;
                }
            }
            else
            {
                Console.WriteLine("Please enter the text you want to write to the file:");
                string text = Console.ReadLine();
                if(text.Contains(@"\"))
                {
                    Console.WriteLine(@"File name cannot include '\', or special characters.");
                }
                else
                {
                    File.WriteAllText(path, text);
                    Console.WriteLine($"File written successfully at " + path);
                } 
            }
        }

        static void ReadFile()
        {
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"PCApp"); // I might be stupid
            Console.WriteLine("Please enter the file path:");
            string fileName = Console.ReadLine();
            filePath += @"\" + fileName;
            if (File.Exists(filePath))
            {
                string text = File.ReadAllText(filePath);
                Console.WriteLine("File contents:");
                Console.WriteLine(text);
            }
            else
            {
                Console.WriteLine("File not found.");
            }
        }

        static List<User> LoadUsers(string filePath) // This reads the .json user file and discerns what is what.
        {
            if (File.Exists(filePath))
            {
                string jsonString = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<List<User>>(jsonString) ?? new List<User>();
            }
            return new List<User>();
        }

        static bool UserExists(string Username, string Password, string userPath) // Self explanatory
        {
            List<User> users = LoadUsers(userPath);
            return users.Exists(u => u.Username == Username && u.Password == Password);
        }

        static void RemoveUser(string username, string filePath) // Self explanatory
        {
            List<User> users = LoadUsers(filePath);

            User userToRemove = users.Find(u => u.Username == username);
            if (userToRemove != null)
            {
                users.Remove(userToRemove);
                SaveUsers(users, filePath);
                Console.WriteLine($"User '{username}' removed successfully.");
            }
            else
            {
                Console.WriteLine("User not found.");
            }
        }

        static void ListUsers(string filePath) // Also self explanatory, I truly hope.
        {
            List<User> users = LoadUsers(filePath);

            if (users.Count == 0)
            {
                Console.WriteLine("No users found.");
                return;
            }

            Console.WriteLine("Registered Users:");
            foreach (var user in users)
            {
                Console.WriteLine("- " + user.Username);
            }
        }



        static void ManageUsers()
        {
            Console.WriteLine("--- Manage Users ---\nPlease select an option:\n(1) Add user\t(2) Remove user\t(3) View users\t(4) Exit back to main screen.");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int userInput))
            {
                switch (userInput)
                {
                    case 1:
                        Console.WriteLine("Please provide the new users' username:");
                        string username = Console.ReadLine();
                        Console.WriteLine("Now please enter the users' password:");
                        string password = Console.ReadLine();
                        Console.WriteLine("Entering user into the database...");
                        if(username == "" || password == "")
                        {
                            Console.WriteLine("Username or password is empty, please try again.");
                        }
                        else
                        {
                            AddUser(username, password, userPath);
                        }              
                        break;
                    case 2:
                        Console.WriteLine("Please provide the user you'd like to remove:");
                        username = Console.ReadLine();
                        Console.WriteLine("Now please enter their password:");   
                        password = Console.ReadLine();
                        if (UserExists(username, password, userPath))
                        {
                            Console.WriteLine("Initalizing removal...");
                            RemoveUser(username, userPath);
                        }
                        else
                        {
                            Console.WriteLine("User was not found in our system.");
                        }
                        break;
                    case 3:
                        ListUsers(userPath);
                        break;
                    case 4:
                        Console.WriteLine("Exiting to main menu.");
                        break;
                    default:
                        Console.WriteLine("Invalid input.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }
        }

        static void AddUser(string username, string password, string userPath)
        {
            List<User> users = LoadUsers(userPath);

            if (users.Exists(u => u.Username == username))
            {
                Console.WriteLine("Username already exists.");
                return;
            }

            users.Add(new User { Username = username, Password = password });
            SaveUsers(users, userPath);
            Console.WriteLine("User added successfully.");
        }

        static void SaveUsers(List<User> users, string filePath)
        {
            string jsonString = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, jsonString);
        }

        static bool AuthenticateUser(string username, string password, string filePath)
        {
            List<User> users = LoadUsers(filePath);
            return users.Exists(u => u.Username == username && u.Password == password);
        }



        static void Main(string[] args)
        {
            Console.WriteLine(@"
  _______   _____    _    _   ______    _____   ______    _____ 
 |__   __| |  __ \  | |  | | |  ____|  / ____| |  ____|  / ____|
    | |    | |__) | | |  | | | |__    | (___   | |__    | |     
    | |    |  _  /  | |  | | |  __|    \___ \  |  __|   | |
    | |    | | \ \  | |__| | | |____   ____) | | |____  | |____ 
    |_|    |_|  \_\  \____/  |______| |_____/  |______|  \_____|");

            Console.WriteLine("-----------------------------------------------------------------\n--- TrueSec PC ---");
            LogIn();
        }

        static void LogIn()
        {
            while (keepRunning)
            {
                Console.WriteLine("Please type 'login' to continue, or 'exit' to exit the program.");
                string input = Console.ReadLine().ToLower();
                switch (input)
                {
                    case "login":
                        Console.WriteLine("Please enter your username first:");
                        userName = Console.ReadLine();
                        Console.WriteLine("Enter password: ");
                        string password = Console.ReadLine();

                        if(AuthenticateUser(userName, password, userPath))
                        {
                            Console.WriteLine("Login successful!");
                            keepRunning = false;
                            loggedIn = true;
                            LoggedIn();
                        }
                        else
                        {
                            Console.WriteLine("Invalid Credentials.");
                        }
                    break;

                    case "exit":
                        Console.WriteLine("Thank you for using TrueSec!");
                        keepRunning = false;
                    break;
                }
            }
        }

        static void LoggedIn()
        {
            while(loggedIn)
            {
                Console.WriteLine("----------------------------------------");
                Console.WriteLine("Welcome to TrueSec! Please select an option:\n(1) Write to a file\t(2) Read a file\t\t(3) View and manage users\t(4) Sign out.");
                string input = Console.ReadLine();
                if (int.TryParse(input, out int userInput))
                {
                    switch (userInput)
                    {
                        case 1:
                            WriteToFile();
                            break;
                        case 2:
                            ReadFile();
                            break;
                        case 3:
                            if(userName != "Admin")
                            {
                                Console.WriteLine("You are not authorized to manage users. Please sign in using an Administrator account.");
                            }
                            else
                            {
                                ManageUsers();
                            }
                            break;
                        case 4:
                            Console.WriteLine("Returning to main menu...");
                            keepRunning = true;
                            LogIn();
                            loggedIn = false;
                            break;
                        default:
                            Console.WriteLine("Invalid input.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input.");
                }
            }
            
        }

    }
}