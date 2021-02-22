using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CovidMovieMadness.Models;


namespace CovidMovieMadness.ViewModels
{
    public class MovieDetailsViewModel
    {
        public int MovieId { get; set; }
        public double AvgRating { get; set; }  //Movie
        public string Genre { get; set; }  //Movie
        public string Name { get; set; }  //Movie
        public int Year { get; set; }  //Movie

        public int PostId { get; set; }
        public int Rating { get; set; }  //Post
        public string PostText { get; set; }  //Post
        public string Message { get; set; }

        public List<Comment> Comments = new List<Comment>();
    }
}