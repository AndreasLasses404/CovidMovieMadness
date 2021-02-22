using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CovidMovieMadness.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string CommentText { get; set; }
        public int UserRating { get; set; }
        public string UserName { get; set; }
    }
}