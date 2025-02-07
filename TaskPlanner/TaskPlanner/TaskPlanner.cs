using System;
using System.Diagnostics;
using System.IO;
using System.Linq.Expressions;

namespace TaskPlanner
{
    class TaskPlanner // Lowkey I don't remember creating this, I was probably REALLY tired lol.
    {
        static string folderName = "TaskTracker"; // This entire section is just setting up the storage file for the tasks. This might not even work, I don't recognize the path method.
        static string fileName = "tasks.txt";
        static string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, folderName);
        static string filePath = Path.Combine(folderPath, fileName);

        static void Main(string[] args)
        {
            Console.WriteLine(@"
 _____           _       _       _    ____   _____        
|  _  |         (_)     | |     | |  / ___| |  _  |       
| | | |  _   _   _    __| |   __| | / /___  | |/' | __  __
| | | | | | | | | |  / _` |  / _` | | ___ \ |  /| | \ \/ /
\ \/' / | |_| | | | | (_| | | (_| | | \_/ | \ |_/ /  >  < 
 \_/\_\  \__,_| |_|  \__,_|  \__,_| \_____/  \___/  /_/\_\");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("--- Quidds' Task Tracker ---");

            EnsureLogExists();      

            bool keepRunning = true;
            while(keepRunning)
            {
                Console.WriteLine("1. Add a task");
                Console.WriteLine("2. List all tasks");
                Console.WriteLine("3. Exit");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch(choice)
                {
                    case "1":
                        AddTask();
                        break;
                    case "2":
                        ListTasks();
                        break;
                    case "3":
                        keepRunning = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void EnsureLogExists() // Went a bit extra on the logging I see
        {
            if (!File.Exists(folderPath))
            {
                File.WriteAllText(folderPath, @"
 $$$$$$\                        $$\             $$\             $$\        $$$$$$\                                                  
$$  __$$\                       \__|            $$ |            $$ |      $$  __$$\                                                 
$$ /  $$ |      $$\   $$\       $$\        $$$$$$$ |       $$$$$$$ |      $$ /  \__|       $$$$$$\         $$$$$$\         $$$$$$\  
$$ |  $$ |      $$ |  $$ |      $$ |      $$  __$$ |      $$  __$$ |      $$ |            $$  __$$\       $$  __$$\       $$  __$$\ 
$$ |  $$ |      $$ |  $$ |      $$ |      $$ /  $$ |      $$ /  $$ |      $$ |            $$ /  $$ |      $$ |  \__|      $$ /  $$ |
$$ $$\$$ |      $$ |  $$ |      $$ |      $$ |  $$ |      $$ |  $$ |      $$ |  $$\       $$ |  $$ |      $$ |            $$ |  $$ |
\$$$$$$ /       \$$$$$$  |      $$ |      \$$$$$$$ |      \$$$$$$$ |      \$$$$$$  |      \$$$$$$  |      $$ |            $$$$$$$  |
 \___$$$\        \______/       \__|       \_______|       \_______|       \______/        \______/       \__|            $$  ____/ 
     \___|                                                                                                                $$ |      
                                                                                                                          $$ |      
                                                                                                                          \__|
------------------------------------------------------------------------------------------------------------------------------------");
                File.AppendAllText(folderPath, $"\nQuidds' Task Tracker: Task log\nUse this file to keep track of your tasks.\nLog created on {DateTime.Now}\n\n");
            }
        }

        static void AddTask()
        {
            Console.Clear();
            Start:
            Console.Write("Enter task name: ");
            string taskName = Console.ReadLine();
            Console.Write("Enter task description: ");
            string taskDescription = Console.ReadLine();
            Console.Write("Enter task due date: ");
            string taskDueDate = Console.ReadLine();
            Console.WriteLine("Are you sure all details are correct? (Y/N)");
            string confirm = Console.ReadLine();
            if (confirm.ToLower() == "n")
            {
                goto Start;
            }
            else if (confirm.ToLower() != "y")
            {
                Console.WriteLine("Invalid choice. Please try again.");
                goto Start;
            }

            string task = $"{taskName},{taskDescription},{taskDueDate}";
            File.AppendAllText(folderPath, task + Environment.NewLine);
            Console.WriteLine("Task added successfully!");
        }

        static void ListTasks()
        {
            Console.Clear();
            if (!File.Exists(folderPath))
            {
                Console.WriteLine("No tasks found.");
                return;
            }
            string[] tasks = File.ReadAllLines(folderPath);
            bool foundTasks = false;

            for (int i = 16; i < tasks.Length; i ++)
            {
                if (!string.IsNullOrWhiteSpace(tasks[i]))
                {
                    string task = tasks[i];
                    string[] taskDetails = task.Split(',');
                    Console.WriteLine($"Task Name: {taskDetails[0]}");
                    Console.WriteLine($"Task Description: {taskDetails[1]}");
                    Console.WriteLine($"Task Due Date: {taskDetails[2]}");
                    Console.WriteLine("----------------------------------------");
                    foundTasks = true;
                }
            }

            if (!foundTasks)
            {
                Console.WriteLine("No tasks found.");
            }
        }
    }
}