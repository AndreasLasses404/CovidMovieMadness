using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CovidMovieMadness.ViewModels;
using CovidMovieMadness.DAL;
using CovidMovieMadness.Models;

namespace CovidMovieMadness.Controllers
{
    public class MoviesController : Controller
    {
        private PostContext db = new PostContext();

        // GET: Movies
        public ActionResult Index()
        {
            var viewModel = new MovieIndexData();
            viewModel.Movies = db.Movies;
            viewModel.Posts = db.Posts;
            viewModel.Comments = db.Comments;

            return View(viewModel.Movies.ToList());
        }

        // GET: Movies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            Post post = db.Posts.Where(p => p.Movie.Id == movie.Id).FirstOrDefault();
            List<Comment> commentsList = new List<Comment>();

            if(post != null)
            {
                foreach (var c in post.Comments)
                {
                    commentsList.Add(c);
                }
                MovieDetailsViewModel movieDetails = new MovieDetailsViewModel()
                {
                    MovieId = movie.Id,
                    Name = movie.Name,
                    Genre = movie.Genre,
                    Year = movie.Year,

                    PostId = post.Id,
                    Rating = post.Rating,
                    PostText = post.PostText,

                    Comments = commentsList
                    

                };
                return View(movieDetails);

            }
            if (post == null)
            {
                MovieDetailsViewModel movieDetails = new MovieDetailsViewModel()
                {
                    MovieId = movie.Id,
                    Name = movie.Name,
                    Genre = movie.Genre,
                    Year = movie.Year,
                };
                return View(movieDetails);

            }
            if (movie == null)
            {
                return HttpNotFound();
            }
            return RedirectToAction("Index");

        }
        public ActionResult DeletePost(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return RedirectToAction("Delete", "Posts", new { id = id });
        }

        // GET: Movies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,AvgRating,Genre,Name,Year")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Movies.Add(movie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(movie);
        }

        // GET: Movies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        public ActionResult CreatePost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Movie tempMovie = db.Movies.Find(id);
            TempData["movie"] = tempMovie;
            return RedirectToAction("Create", "Posts");
        }

        public ActionResult EditComment(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return RedirectToAction("Edit", "Comments", new { id = id });
        }

        public ActionResult CreateComment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Post tempPost = db.Posts.Find(id);
            TempData["post"] = tempPost;

            return RedirectToAction("Create", "Comments");
        }

        public ActionResult EditPost(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return RedirectToAction("Edit", "Posts", new { id = id });
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AvgRating,Genre,Name,Year")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        public ActionResult DeleteComment(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return RedirectToAction("Delete", "Comments", new { id = id });
        }

        // GET: Movies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movie movie = db.Movies.Find(id);
            db.Movies.Remove(movie);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
