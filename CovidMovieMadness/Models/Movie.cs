using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CovidMovieMadness.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public double AvgRating { get; set; }
        public string Genre { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
    }
}