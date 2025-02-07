using System;

namespace Hasher
{
    class Program // Okay so this was supposed to be a hashing algorithim but I didn't know what a hashing algorithim actually is LMAOOOO I didn't know they're always supposed to return a set value
    {             // Anyways this is basically trash, not my proudest project (not even really finished either).
        static string username = "";
        static string storedPassHash = "";
        static Random rand = new Random();
        static int hashRand = rand.Next(1, 10000);
        static bool keepRunning = true;
        static void Main(string[] args)
        {
            Console.WriteLine(@" 
 _____           _       _       _    ____   _____        
|  _  |         (_)     | |     | |  / ___| |  _  |       
| | | |  _   _   _    __| |   __| | / /___  | |/' | __  __
| | | | | | | | | |  / _` |  / _` | | ___ \ |  /| | \ \/ /
\ \/' / | |_| | | | | (_| | | (_| | | \_/ | \ |_/ /  >  < 
 \_/\_\  \__,_| |_|  \__,_|  \__,_| \_____/  \___/  /_/\_\");
            Console.WriteLine("--- Quidd's Custom Hasher ---\n----------------------------------------");

            while (keepRunning)
            {
                Console.WriteLine("Welcome to my Hashing Program!\nIf this is your first time logging in, please create an account (by typing create).\nIf you've already created an account, please log in (by typing login).");
                string input = Console.ReadLine();
                if (input.ToLower() == "create")
                {
                    CreateAccount();
                }
                else if (input.ToLower() == "login" && storedPassHash != "")
                {
                    LogIn();
                }
                else
                {
                    Console.WriteLine("Invalid input. Please try again.\n----------------------------------------");
                }
            }
        }

        static void CreateAccount()
        {
            Console.WriteLine("--- Create Account ---\nPlease enter a username:");
            username = Console.ReadLine();
            Console.WriteLine("Now please enter a password:");
            string password = Console.ReadLine();
            storedPassHash = HashPass(password);
            password = "";
            Console.WriteLine($"Thank you, {username}! Your account has been created.");
        }

        static void LogIn()
        {
            Console.WriteLine("--- Log In ---\nPlease enter your username:");
            string input = Console.ReadLine();
            if (input == username)
            {
                Console.WriteLine("Please enter your password:");
                string password = Console.ReadLine();
                if (HashPass(password) == storedPassHash)
                {
                    Console.WriteLine("Welcome back, " + username + "!");
                    keepRunning = false;
                }
                else
                {
                    Console.WriteLine("Incorrect password. Please try again.");
                }
            }
            else
            {
                Console.WriteLine("Username not found. Please try again.");
            }
        }

        static string HashPass(string password)
        { // Not a secure hashing algorithm, but it's just for fun
            return hashRand + $"{hashRand}~`!@#$%^&*()_+" + (2^2 + 3^3^3 + 4^4^4^4 + 5^5^5^5^5) + password + $"{password}ASDMKL{hashRand}32@r#@R32MKR32L3R{hashRand}2LKNR32{password}43T43MK{hashRand}L4334TLK34$#{hashRand}t#t$mkl#{hashRand}4M4TM3KL{hashRand}msdklali{hashRand}AFM$3";
        }

    }
}