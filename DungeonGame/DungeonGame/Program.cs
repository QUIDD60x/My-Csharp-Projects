using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Threading;
using Microsoft.VisualBasic;

namespace ConsoleApp
{

    /*--- GAME FUNCTIONS ---*/
    public class Rooms // This generates the randomness for each floor. Will randomize encounters such as loot, enemies, what level enemies, etc.
    {
        public string RoomType { get; set; }
        public int Difficulty { get; set; }
        public bool HasEnemy { get; set; }
    }

    public class Enemy // Sets up the basic enemy structure.
    {
        public string Name { get; set; }
        public int Strength { get; set; } // This is not the damage, but instead their "power level", higher levels tend to spawn less often
        public int Health { get; set; }
        public string Art { get; set; }
        public List<EnemyMoves> Moves { get; set; }

        public Enemy(string name, int strength, int health, string art, List<EnemyMoves> moves)
        {
            Name = name;
            Strength = strength;
            Health = health;
            Art = art;
            Moves = moves;
        }
    }

    public class EnemyMoves // I don't think you included this in your example, but I think I implemented it correctly.
    {
        public string MoveName { get; set; }
        public int Damage { get; set; }
        public int Accuracy { get; set; }
        public string Effect { get; set; } // Special effects for possible poison, burn, etc.

        public EnemyMoves(string name, int damage, int accuracy, string effect = "")
        {
            MoveName = name;
            Damage = damage;
            Accuracy = accuracy;
            Effect = effect;
        }
        
    }

    class Game
    {
        public List<Enemy> enemies { get; set; }
        public Game()
        {
            enemies = new List<Enemy>(); // I copied the layout you gave me, while also adding the enemy moves I think.
            {
                enemies.Add(new Enemy("Slime", 1, 20, "Art here", new List<EnemyMoves>
                {
                    new EnemyMoves("Slime Punch", 5, 100),
                    new EnemyMoves("Slime Spit", 2, 25),
                    new EnemyMoves("Envelope", 85, 15, "You are now enveloped in slime!")
                }));
                enemies.Add(new Enemy("Dog", 2, 30, "Art here", new List<EnemyMoves>
                {
                    new EnemyMoves("Bite", 10, 75),
                    new EnemyMoves("Growl", 0, 50, "Lowered your attack!")
                }));
                enemies.Add(new Enemy("Bat", 3, 15, "Art here", new List<EnemyMoves>
                {
                    new EnemyMoves("Bite", 5, 75),
                    new EnemyMoves("Screech", 0, 50, "Lowered your defense!"),
                    new EnemyMoves("Screech", 5, 75, "Your turn has been skipped!")
                }));
            }
        }
        public static void PrintCentered(string text)
        {
            int screenWidth = Console.WindowWidth;
            int textWidth = text.Length;
            int leftPadding = (screenWidth - textWidth) / 2;
            Console.WriteLine(new string(' ', leftPadding) + text);
        }

        public static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine(@"
 _____           _       _       _    ____   _____        
|  _  |         (_)     | |     | |  / ___| |  _  |       
| | | |  _   _   _    __| |   __| | / /___  | |/' | __  __
| | | | | | | | | |  / _` |  / _` | | ___ \ |  /| | \ \/ /
\ \/' / | |_| | | | | (_| | | (_| | | \_/ | \ |_/ /  >  < 
 \_/\_\  \__,_| |_|  \__,_|  \__,_| \_____/  \___/  /_/\_\");
            Console.WriteLine("Quidd's game test");
            Console.WriteLine("Type 1 to continue to the game, 2 to view credits/changelog, and 3 to exit.");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int userInput))
            {
                switch (userInput)
                {
                    case 1:
                        PrintCentered(new string('-', 60));
                        Console.Beep(100, 800);
                        Thread.Sleep(200);
                        PrintCentered(new string('#', 60));
                        Console.Beep(200, 800);
                        Thread.Sleep(200);  
                        PrintCentered(new string('#', 60));
                        Console.Beep(300, 800);
                        Thread.Sleep(200);
                        PrintCentered(new string('#', 60));
                        Console.Beep(400, 800);
                        Thread.Sleep(200);
                        PrintCentered(new string('#', 60));
                        Console.Beep(480, 800);
                        Thread.Sleep(200);
                        PrintCentered(new string('-', 60));
                        Console.Beep(410, 800);
                        PrintCentered("You awake in a cold and dusty dungeon. All you know is you must keep fighting.");
                        Console.Beep(450, 800);
                        Console.ReadKey();
                        Game game = new Game();
                        Floor1(game);
                        break;
                    case 2:
                        Credits();
                        break;
                    case 3:
                        Console.WriteLine("Thank you for playing!");
                        Thread.Sleep(150);
                        Environment.Exit(0);
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

        /*--- Floors ---*/
        public static void Floor1(Game game)
        {
            Console.Clear();
            PrintCentered("FLOOR ONE");
            foreach (var enemy in game.enemies) // It simply skips this entire part and asks to continue.
            {
                PrintCentered($"Enemy: {enemy.Name}, Strength: {enemy.Strength}, Art: {enemy.Art}");
            }

            PrintCentered("Press any key to continue...");
            Console.ReadKey();
        }

        public static void Credits()
        {
            PrintCentered("Credits:");
            Console.WriteLine("Quidd - Programmer");
            Console.WriteLine("Changelog:");
            Console.WriteLine("0.0.1 - Initial release");
            Console.WriteLine("Please press any button to return to the main menu.");
            Console.ReadKey();
            Main(null);
        }
    }
}