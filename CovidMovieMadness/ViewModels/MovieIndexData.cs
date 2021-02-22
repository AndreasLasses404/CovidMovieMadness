using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CovidMovieMadness.Models;

namespace CovidMovieMadness.ViewModels
{
    public class MovieIndexData
    {
        public IEnumerable<Movie> Movies { get; set; }
        public IEnumerable<Post> Posts { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
    }
}