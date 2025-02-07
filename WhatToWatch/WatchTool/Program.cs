using System;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace WatchTool // One of my more favorite creations actually, it saves the movies you'd like to watch! Very fun, very cool.
{

    public class Movie // A lot of the code for the file saving and creation is basically identical to the PCApp. Like before, this is initializing the json file format, but with a bool instead of a password.
    {
        public string Name { get; set;}
        public bool IsWatched { get; set;}
    }

    public class Functions
    {
        private string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MovieList"); // I decided printing to the documents folder would be a bit better (although my windows detects a security risk when debugging lol).

        public List<Movie> LoadMovies()
        {
            if (File.Exists(path))
            {
                string jsonString = File.ReadAllText(path);
                return JsonSerializer.Deserialize<List<Movie>>(jsonString) ?? new List<Movie>();
            }
            return new List<Movie>();
        }

        public void ListMovies()
        {
            List<Movie> movies = LoadMovies();

            if (movies.Count == 0)
            {
                Console.WriteLine("No movies found.");
                return;
            }

            Console.WriteLine("Your saved movies:");
            Console.WriteLine("+---------------------------+-----------+");
            Console.WriteLine("| Movie Name                | Watched?  |");
            Console.WriteLine("+---------------------------+-----------+");

            foreach (var movie in movies)
            {
                Console.WriteLine($"| {movie.Name,-25} | {(movie.IsWatched ? "✔ Yes" : "❌ No"),-9}");
            }
            Console.WriteLine("+---------------------------+-----------+");

        }

        public void AddMovie(string movie)
        {
            List<Movie> movies = LoadMovies();

            if (movies.Exists(u => u.Name == movie))
            {
                Console.WriteLine("This movie already exists!");
                return;
            }

            movies.Add(new Movie {Name = movie, IsWatched = false});
            SaveMovies(movies);
            Console.WriteLine("Movie added successfully.");
        }

        public void RemoveMovie(string movieName)
        {
            List<Movie> movies = LoadMovies();

            Movie movieToRemove = movies.Find(u => u.Name == movieName);
            if (movieToRemove != null)
            {
                movies.Remove(movieToRemove);
                SaveMovies(movies);
                Console.WriteLine($"Movie '{movieToRemove.Name}' removed successfully.");
            }
            else
            {
                Console.WriteLine("Movie not found.");
            }
        }

        public void MarkMovieAsWatched(string movieName)
        {
            List<Movie> movies = LoadMovies();
            Movie watchedMovie = movies.Find(u => u.Name == movieName);

            if (watchedMovie != null)
            {
                watchedMovie.IsWatched = true;
                SaveMovies(movies);
                Console.WriteLine($"Movie '{watchedMovie.Name}' marked as watched.");
            }
            else
            {
                Console.WriteLine("Movie not found.");
            }
        }

        public void SaveMovies(List<Movie> movies)
        {
            string jsonString = JsonSerializer.Serialize(movies, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, jsonString);
        }
    }

    class Entry // I typically like to add the functions first (that's why the stuff above are all the functions in a function class, duh). Here's the actual UI and inputs.
    {
        static void Main(string[] args)
        {
            Functions movieFunctions = new Functions();
            bool keepRunning = true;
            int loop = 0;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(@"
 _____           _       _       _    ____   _____        
|  _  |         (_)     | |     | |  / ___| |  _  |       
| | | |  _   _   _    __| |   __| | / /___  | |/' | __  __
| | | | | | | | | |  / _` |  / _` | | ___ \ |  /| | \ \/ /
\ \/' / | |_| | | | | (_| | | (_| | | \_/ | \ |_/ /  >  < 
 \_/\_\  \__,_| |_|  \__,_|  \__,_| \_____/  \___/  /_/\_\");
            Console.WriteLine(new string('-', 60));
            Console.WriteLine(" Quidds' Movie Planner ");
            Console.WriteLine(new string('-', 60));
            Console.WriteLine("Welcome! This movie/show planner will allow you to easily list, add/remove, and mark movies as read, all from your CLI!\n- To get started, type (1) to list all movies, (2) to add/remove a movie, and (3) to mark a movie as completed! You can type (4) to quit the program safely.");
            while (keepRunning)
            {
                if( loop >= 1)
                {
                    Console.WriteLine("Type (1) to view all listed movies, (2) to add a movie, (3) to delete a movie, (4) to mark a movie as completed, or (5) to exit.");
                }
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        Console.Write("Loading list"); // Instantly beaming the list was kinda jarring so I added a small wait loop (manufactured lag is CRAZY).
                        for (int i = 0; i < 3; i++)
                        {
                            Console.Write(".");
                            Thread.Sleep(700);
                        }
                        movieFunctions.ListMovies();
                        break;
                    case "2":
                        Console.WriteLine("Please enter the movie you'd like to add:");
                        string add = Console.ReadLine();
                            movieFunctions.AddMovie(add);
                        break;
                    case "3":
                        Console.WriteLine("Please enter the movie you'd like to remove:");
                        string remove = Console.ReadLine();
                        movieFunctions.RemoveMovie(remove);
                        break;
                    case "4":
                        Console.WriteLine("Please enter the movie you've watched: ");
                        string watchedMovie = Console.ReadLine();
                        movieFunctions.MarkMovieAsWatched(watchedMovie);
                        break;
                    case "5":
                        Console.WriteLine("Thank you for using my MovieCheck!");
                        Console.ReadKey();
                        keepRunning = false;
                        break;
                    default:
                        Console.WriteLine("Invalid input, please try again.");
                    break;
                }
                loop++; // Thought it'd be a bit nicer to give a different prompt after using it for the first time.
            }

        }
    }
}