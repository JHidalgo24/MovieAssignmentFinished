using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using CsvHelper.TypeConversion;
using System.Linq;
using NLog;
using System.Diagnostics;
using NLog.Config;

namespace MovieListAssignment
{
    class Program
    {
        public static string movieFile = "C:\\Users\\justi\\Desktop\\MovieAssignment\\movies.csv";
        public static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public static void Main(string[] args)
        {

            int number = 1;
            int lastId = 0;

            do
            {
                PrintMenu();
                number = ValueGetter();
                switch (number)
                {
                    case 1:
                        var temp = ReturnFilmList();
                        lastId = temp[temp.Count - 1].Id;
                        AddMovie(lastId);
                        break;
                    case 2:
                        logger.Info("Option 2 chosen");
                        System.Console.WriteLine($"There are {ReturnFilmList().Count} movies in file what range do you wish to see?");
                        Console.WriteLine($"What is the first number from 1 - {ReturnFilmList().Count}");
                        int firstNumber = ValueGetter();
                        Console.WriteLine($"What is the second number from {firstNumber} - {ReturnFilmList().Count}");
                        int secondNumber = ValueGetter();
                        while (secondNumber < firstNumber)
                        {
                            System.Console.WriteLine("Second value can't be smaller!");
                            secondNumber = ValueGetter();
                        }
                        logger.Debug($"First Number: {firstNumber} Second Number: {secondNumber} out of {ReturnFilmList().Count} films");
                        ListFilms(firstNumber, secondNumber);
                        break;
                    case 3:
                        logger.Info("Option 3 chosen");
                        Console.WriteLine("What movie do you want to look up?(Press Enter for all)");
                        string filmPicked = Console.ReadLine();
                        logger.Info($"User searched for: {filmPicked}");
                        SearchMovie(filmPicked);
                        break;
                    case 4:
                        logger.Trace("Application ended");
                        Console.WriteLine("Goodbye!");
                        break;
                    default:
                        logger.Trace($"User tried to input {number}");
                        Console.WriteLine("That is not an option sorry!");
                        break;
                }
            } while (number != 4);
        }

        public static void PrintMenu() => Console.WriteLine("What do you want to do?\n1.)Add movie\n2.)List Movies\n3.)Search Movie\n4.)Exit)");

        public static void ListFilms(int firstNum, int secondNum)
        {
            List<Movie> temp = ReturnFilmList();

            for (int i = firstNum - 1; i <= secondNum - 1; i++)
            {
                Console.WriteLine(temp[i]);
            }
        }

        public static void AddMovie(int id)
        {
            try
            {
                string titlePicked = "", genreString = "";
                List<string> genresPicked = new List<string>();
                int genresTotal;
                Console.WriteLine("What is the title of the film");
                titlePicked = Console.ReadLine();
                while (DuplicateChecker(titlePicked))
                {
                    Console.WriteLine("Sorry the film is already in the list enter a new one");
                    titlePicked = Console.ReadLine();
                }
                System.Console.WriteLine("What year was the movie made?");
                string year = "(" + Console.ReadLine() + ")";
                titlePicked = titlePicked + " " + year;
                Console.WriteLine("How many genres do you want to add?");
                genresTotal = ValueGetter();
                for (int i = 0; i < genresTotal; i++)
                {
                    Console.WriteLine($"What is the {i + 1} genre?");
                    genresPicked.Add(Console.ReadLine());
                }
                genreString = string.Join("|", genresPicked);

                var records = new List<Movie> { new Movie { Id = id + 1, title = titlePicked, genres = genreString }, };
                var config = new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = false, };
                using (var stream = File.Open(movieFile,
                    FileMode.Append))
                using (var writer = new StreamWriter(stream))
                using (var csv = new CsvWriter(writer, config))
                {
                    csv.WriteRecords(records);
                }
            }
            catch (Exception e)
            {
                logger.Debug($"{e.Message} error happened");
                Console.WriteLine("Unable to write to file!");
            }
        }

        public static void SearchMovie(string filmPicked)
        {
            try
            {
                List<Movie> temp = ReturnFilmList();
                foreach (Movie movies in temp)
                {
                    String currentMovie = movies.title.ToLower();
                    if (currentMovie.Contains(filmPicked.ToLower()))
                    {
                        Console.WriteLine(movies);
                    }
                }
            }
            catch (Exception e)
            {
                logger.Debug($"{e.Message} error happened");
                Console.WriteLine("Unable to search film");
            }
        }

        public static bool DuplicateChecker(string filmPicked)
        {
            bool contained = false;
            try{
            List<Movie> temp = ReturnFilmList();
            foreach (Movie movies in temp)
            {
                String currentMovie = movies.title.ToLower();
                if (currentMovie.Contains(filmPicked.ToLower()))
                {
                    contained = true;
                }
            }
            }
            catch(Exception e){
                System.Console.WriteLine("Unable to return duplicate");
                logger.Debug(e.Message);
            }
            return contained;
        }

        public static List<Movie> ReturnFilmList()
        {
            List<Movie> movies;
            try
            {
                using (var streamReader =
                    new StreamReader(movieFile))
                using (var csv = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<Movie>().ToList();
                    movies = records;
                }
            }
            catch (Exception e)
            {
                logger.Debug($"{e.Message} error happened");
                Console.WriteLine("Could not read file sorry!");
                throw;
            }

            return movies;
        }

        public static int ValueGetter()
        {
            string option = Console.ReadLine();
            int number;
            bool success = Int32.TryParse(option, out number);

            while (!success)
            {
                Console.WriteLine("That isn't a number sorry!");
                option = Console.ReadLine();
                success = Int32.TryParse(option, out number);
            }

            return number;
        }
    }
}