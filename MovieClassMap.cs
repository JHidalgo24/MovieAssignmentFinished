using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using System.Linq;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using NLog;

namespace MovieListAssignment
{
    public class MovieClassMap : ClassMap<Movie>
    {
        public MovieClassMap()
        {
        Map(m => m.Id).Name("movieId");
        Map(m=>m.title).Name("title");
        Map(m=>m.genres).Name("genres");
        }
    }
}