// HASHING SYSTEM IMPLEMENTED WITH CHATGPT (I did not know where to start with that), everything else is designed by me (Quidd/Quidd60x).
using System;
using System.Security.Cryptography;
using System.Text;


namespace LoginApp
{
    class LoginApp
    {
        static void Main(string[] args)
        {
            string userName = ""; // Initializes the username and password.
            string storedPasswordHash = "";
            int loginAttempts = 3;
            Console.WriteLine("TrueSec login page");
            bool keepRunning = true;
            while (keepRunning)
            {
                /*------------------------ PASSWORD HASHING ------------------------*/
                static string HashPassword(string password) // Starts with hashing the password (so it's safely stored when looping to log in?).
                {
                    using (SHA256 sha256 = SHA256.Create())
                    {
                        byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                        return Convert.ToBase64String(hashBytes);
                    }
                }

                static bool VerifyPassword(string loginDeets, string storedPasswordHash) // This hashes the password the user is using to log in, then compares them (in an equal manner so hackers can't use small time differences to compare what's right and what's wrong?) This is the stuff chatgpt made so I don't fully understand it.
                {
                    string inputHash = HashPassword(loginDeets);
                    return CryptographicOperations.FixedTimeEquals(Encoding.UTF8.GetBytes(inputHash), Encoding.UTF8.GetBytes(storedPasswordHash));
                }

                /*------------------------ ENTRY PAGE ------------------------*/
                Console.WriteLine("Welcome to TrueSec! Type 'create' to create an account, or type 'login' to log in.");
                string choice = Console.ReadLine();
                if (choice == "login" || choice == "Login")
                {
                    if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(storedPasswordHash)) // Makes sure you have an account first.
                    {
                        Console.WriteLine("No account has been made yet. Please create an account first.");
                        continue;
                    }

                    /*------------------------ LOGIN ------------------------*/
                    Console.WriteLine("You've chosen to log in!\nPlease insert your username and password, seperated by a comma.");
                    string[] loginDeets = Console.ReadLine().Split(','); // Splits the login username and password into two seperate parts (shown below as inputUsername and inputPassword), less work than doing a username login and then a check password login (also makes more sense).

                    if (loginDeets.Length != 2) // to make sure you have both a username and password entered.
                    {
                        Console.WriteLine("Invalid input. Please provide your username and password.");
                        continue;
                    }
                    
                    string inputUsername = loginDeets[0]; // the beforementioned two parts of the login.
                    string inputPassword = loginDeets[1];

                    if (inputUsername == userName && VerifyPassword(inputPassword, storedPasswordHash)) // If the details match, you're in!
                    {
                        Console.WriteLine($"Login successful! Welcome to TrueSec, {userName}.");
                        Console.WriteLine("");
                        keepRunning = false;
                        
                       Console.WriteLine(@" _______                          _____               ");
                       Console.WriteLine(@"|__   __|                        / ____|              ");
                       Console.WriteLine(@"   | |     _ __   _   _    ___  | (___     ___    ___ ");  
                       Console.WriteLine(@"   | |    | '__| | | | |  / _ \  \___ \   / _ \  / __|");            
                       Console.WriteLine(@"   | |    | |    | |_| | |  __/  ____) | |  __/ | (__ "); 
                       Console.WriteLine(@"   |_|    |_|     \__,_|  \___| |_____/   \___|  \___|");
                            
                    }
                    else
                    {
                        loginAttempts --; // Pretty self explanatory, if you get it wrong you lose an attempt and are told off.
                        Console.WriteLine($"Your username/password is incorrect. You have {loginAttempts} more attempts to log in.");
                        if (loginAttempts < 1)
                        {
                            Console.WriteLine("Too many failed attempts, try again later.");
                            keepRunning = false;
                        }
                    }
                }

                /*------------------------ CREATING ACCOUNT ------------------------*/
                if (choice == "create" || choice == "Create") // Pretty basic as far as account creation goes, I only bother hashing the password because this isn't anything serious.
                {
                    Console.WriteLine("You've chosen to create an account! Please enter a username first."); 
                    userName = Console.ReadLine();
                    Console.WriteLine($"You've chosen {userName}. Now please type a password.");
                    string password = Console.ReadLine();
                    storedPasswordHash = HashPassword(password); // This takes the password and immediately hashes it (I think), so it's never plaintext.
                    password = "null";
                    Console.WriteLine($"Your account has been created! Please log in to continue.");
                }
            }
        }    
    }
}