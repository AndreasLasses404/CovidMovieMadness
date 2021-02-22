using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using CovidMovieMadness.Models;
using System.Data.Entity.ModelConfiguration;

namespace CovidMovieMadness.DAL
{
    public class PostContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

    }
}