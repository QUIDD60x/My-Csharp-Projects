using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Threading;
using System.Linq;

#pragma warning disable CA1416 // Validate platform compatibility

namespace ConsoleApp
{

    /*--- GAME FUNCTIONS ---*/
    public class Rooms // This generates the randomness for each floor. Will randomize encounters such as loot, enemies, what level enemies, etc.
    {
        public string RoomType { get; set; }
        public int Difficulty { get; set; }
        public bool HasEnemy { get; set; }

        public Rooms(string roomType, int difficulty, bool hasEnemy)
        {
            RoomType = roomType;
            Difficulty = difficulty;
            HasEnemy = hasEnemy;
        }
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

    public class Player // This is the player class, it will have stats, inventory, etc.
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int Health { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Gold { get; set; }

        public Player(string name, int level, int health, int attack, int defense, int gold)
        {
            Name = name;
            Level = level;
            Health = health;
            Attack = attack;
            Defense = defense;
            Gold = gold;
        }
    }

    public class Inventory
    {
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public int ItemQuantity { get; set; }

        public Inventory(string itemName, string itemDescription, int itemQuantity)
        {
            ItemName = itemName;
            ItemDescription = itemDescription;
            ItemQuantity = itemQuantity;
        }
    }

    class Game
    {
        Random rand = new Random();
        public List<Enemy> enemies { get; set; }
        public List<Rooms> rooms { get; set; }
        public List<Player> players { get; set; }
        public Game()
        {
            players = new List<Player>();
            {
                players.Add(new Player("", 1, 50, 10, 0, 0));
            }
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
            rooms = new List<Rooms>();
            {
                rooms.Add(new Rooms("Empty", 0, false));
                rooms.Add(new Rooms("Loot", 1, rand.Next(0, 4) == 1));
                rooms.Add(new Rooms("Trap", 2, rand.Next(0, 6) == 1));
                rooms.Add(new Rooms("Enemy", 3, true));
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
                        Thread.Sleep(200);
                        PrintCentered(new string('#', 60));
                        Thread.Sleep(200);  
                        PrintCentered(new string('@', 60));
                        Thread.Sleep(200);
                        PrintCentered(new string('$', 60));
                        Thread.Sleep(200);
                        PrintCentered(new string('%', 60));
                        Thread.Sleep(200);
                        PrintCentered(new string('-', 60));
                        PrintCentered("You awake in a cold and dusty dungeon. All you know is you must keep fighting.");
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
            Random rand = new Random();
            PrintCentered("Please enter your name:");
            game.players[0].Name = Console.ReadLine();
            if(string.IsNullOrEmpty(game.players[0].Name))
            {
                game.players[0].Name = "Stupid idiot dummy";
            }
            PrintCentered($"Welcome, {game.players[0].Name}.");
            Console.ReadKey();
            Console.Clear();
            PrintCentered("FLOOR ONE");
            Rooms selectedRoom = game.rooms[rand.Next(game.rooms.Count)];
            foreach (var enemy in game.enemies)
            {
                PrintCentered($"Enemy: {enemy.Name}\tStrength: {enemy.Strength}\tMoves: {enemy.Moves.First().MoveName}");
            }
            PrintCentered("Press any key to continue...");
            Console.ReadKey();
            Console.ForegroundColor = ConsoleColor.Green;
            PrintCentered(new string('-', 60));
            Thread.Sleep(250);
            PrintCentered(new string('@', 60));
            Thread.Sleep(250);
            PrintCentered(new string('@', 60));
            Thread.Sleep(250);
            PrintCentered("ENCOUNTER ONE...");
            Thread.Sleep(250);
            PrintCentered(new string('@', 60));
            Thread.Sleep(250);
            PrintCentered(new string('@', 60));
            Thread.Sleep(250);
            PrintCentered(new string('-', 60));
            Thread.Sleep(250);
            Console.ReadKey();
            if(selectedRoom.HasEnemy)
            {
                PrintCentered($"You entered a {selectedRoom.RoomType} room, and an enemy has appeared!");
                Console.ReadKey();
            }
            else
            {
                PrintCentered($"You entered a {selectedRoom.RoomType} room! You're safe, for now...");
                Console.ReadKey();
            }
        }

        public static void Credits()
        {
            PrintCentered("Credits:");
            Console.WriteLine("Quidd - Programmer");
            PrintCentered("Changelog:");
            Console.WriteLine("0.2.0 - added a player class, inventory class, and a basic floor system (03/01/2025)");
            Console.WriteLine("0.1.0 - Initial release (02/26/2025)");
            Console.WriteLine("Please press any button to return to the main menu.");
            Console.ReadKey();
            Main(null);
        }
    }
}