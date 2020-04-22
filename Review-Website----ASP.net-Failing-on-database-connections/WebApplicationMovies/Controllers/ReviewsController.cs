using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplicationMovies.Models;
using WebApplicationMovies.Models.ViewModels;

namespace WebApplicationMovies.Controllers
{
    public class ReviewsController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Reviews
        public ActionResult Index()
        {
            List<FilmReviewViewModel> FilmReviewList =
                new List<FilmReviewViewModel>();

            List<Review> Reviews;

            Reviews = db.Reviews.ToList();

            foreach (Review r in Reviews)

            {
                Film film = db.Films.Where(x => x.FilmID == r.FilmID).Single();

                FilmReviewViewModel toAdd = new FilmReviewViewModel();

                toAdd.Review = r;
                toAdd.Film = film;

                FilmReviewList.Add(toAdd);
            }





            return View(FilmReviewList);
        }

        // GET: Reviews/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            Film film = db.Films.Where(x => x.FilmID == review.FilmID).Single();

            FilmReviewViewModel FilmReview = new FilmReviewViewModel();
            FilmReview.Review = review;
            FilmReview.Film = film;


            return View(FilmReview);
        }

        // GET: Reviews/Create
        public ActionResult Create(int id = 0)
        {
            if (id == 0)
            {
                return RedirectToAction("Index");
            }
            Film film = db.Films.Where(x => x.FilmID == id).Single();

            ViewBag.FilmID = id;
            ViewBag.FilmTitle = film.FilmTitle;

            return View();
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReviewID,FilmID,ReviewUname,ReviewContent,ReviewRating")] Review review)
        {
            if (ModelState.IsValid)
            {
                db.Reviews.Add(review);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(review);
        }

        // GET: Reviews/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReviewID,FilmID,ReviewUname,ReviewContent,ReviewRating")] Review review)
        {
            if (ModelState.IsValid)
            {
                db.Entry(review).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(review);
        }

        // GET: Reviews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Review review = db.Reviews.Find(id);
            db.Reviews.Remove(review);
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
