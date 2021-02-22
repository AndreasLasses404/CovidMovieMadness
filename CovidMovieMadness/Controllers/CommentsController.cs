using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CovidMovieMadness.DAL;
using CovidMovieMadness.Models;

namespace CovidMovieMadness.Controllers
{
    public class CommentsController : Controller
    {
        private PostContext db = new PostContext();

        // GET: Comments
        public ActionResult Index()
        {
            return View(db.Comments.ToList());
        }

        // GET: Comments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // GET: Comments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CommentText,UserRating,UserName")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                Post tempPost = TempData["post"] as Post;
                Post post = db.Posts.Include("Movie").Where(p => p.Id == tempPost.Id).FirstOrDefault();
                post.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Details", "Movies", new { id = post.Movie.Id });
            }

            return View(comment);
        }

        // GET: Comments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CommentText,UserRating,UserName")] Comment comment)
        {
            if (ModelState.IsValid)
            {

                db.Entry(comment).State = EntityState.Modified;
                Post post = new Post();
                db.SaveChanges();
                foreach (var c in db.Posts)
                {
                    foreach (var co in c.Comments)
                    {
                        if (co.Id == comment.Id)
                        {
                            post = c;
                        }
                    }
                }
                Post tempPost = db.Posts.Include("Movie").Where(p => p.Id == post.Id).FirstOrDefault();
                return RedirectToAction("Details", "Movies", new { id = tempPost.Movie.Id });
            }
            return View(comment);
        }

        // GET: Comments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = db.Comments.Find(id);
            Post post = new Post();
            foreach(var c in db.Posts)
            {
                foreach(var co in c.Comments)
                {
                    if(co.Id == comment.Id)
                    {
                        post = c;
                    }
                }
            }
            db.Comments.Remove(comment);
            db.SaveChanges();
            Post tempPost = db.Posts.Include("Movie").Where(p => p.Id == post.Id).FirstOrDefault();
            return RedirectToAction("Details", "Movies", new { id = tempPost.Movie.Id });
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
