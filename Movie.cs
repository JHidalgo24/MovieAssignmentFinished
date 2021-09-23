using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using CsvHelper;
using System.Linq;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

namespace MovieAssignment
{
    public class Movie
    {

        [Name("movieId")] public int Id { get; set; }
        [Name("title")] public string title { get; set; }
        [Name("genres")] public string genres { get; set; }


        public override string ToString()
        {
            return $"ID:{Id} Title:{title} Genre:{genres}";
        }
    }
}