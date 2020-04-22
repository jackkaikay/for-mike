using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplicationMovies.Models;

namespace WebApplicationMovies.Controllers
{
    public class FilmsController : Controller
    {
        private DBContext db = new DBContext();

        public ActionResult Search(string term)
        {
            var films = from f in db.Films
                        select new
                        {
                            id = f.FilmID,
                            label = f.FilmTitle
                        };

            films = films.Where(f => f.label.Contains(term));

            return Json(films, JsonRequestBehavior.AllowGet);

        }


        // GET: Films
        public ActionResult Index(string sortOrder, string searchString)
        {
            

                ViewBag.TitleSortParm =
                    String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";

                var films = from f in db.Films
                            select f;



                if (!String.IsNullOrEmpty(searchString))
                {
                    films = films.Where(f => f.FilmTitle.Contains(searchString));
                }



                switch (sortOrder)
                {
                    case "title_desc":

                        films = films.OrderByDescending(f => f.FilmTitle);
                        break;

                    default:
                        films = films.OrderBy(f => f.FilmTitle);
                        break;
                }




                return View(films.ToList());
        }



            // GET: Films/Details/5
            public ActionResult Details(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Film film = db.Films.Find(id);
                if (film == null)
                {
                    return HttpNotFound();
                }
                return View(film);
            }

        




            // GET: Films/Create
            public ActionResult Create()
            {
                return View();
            }

            // POST: Films/Create
            // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
            // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FilmID,FilmTitle,FilmGenre," +
            "FilmDesc,FilmReleaseDate,FilmImage")] Film film, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    if (upload.ContentType == "image/jpeg" ||
                        upload.ContentType == "image/jpg"||
                        upload.ContentType == "image/gif" ||
                        upload.ContentType == "image/png" )
                    {
                        string path = Path.Combine(Server.MapPath("~/Content/Images"),
                                      Path.GetFileName(upload.FileName));
                                       
                        upload.SaveAs(path);

                        film.FilmImage = "~/Content/Images/" +
                            Path.GetFileName(upload.FileName);
                    }
                    else
                    {
                        ViewBag.Message = "Not valid image format";
                    }
                }
                db.Films.Add(film);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(film);
        }

        // GET: Films/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Film film = db.Films.Find(id);
            if (film == null)
            {
                return HttpNotFound();
            }
            return View(film);
        }

        // POST: Films/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FilmID,FilmTitle,FilmGenre," +
            "FilmDesc,FilmReleaseDate,FilmImage")] Film film, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    if (upload.ContentType == "image/jpeg" ||
                        upload.ContentType == "image/jpg" ||
                        upload.ContentType == "image/gif" ||
                        upload.ContentType == "image/png")
                    {
                        string path = Path.Combine(Server.MapPath("~/Content/Images"),
                                      Path.GetFileName(upload.FileName));

                        upload.SaveAs(path);

                        film.FilmImage = "~/Content/Images/" +
                            Path.GetFileName(upload.FileName);
                    }
                    else
                    {
                        ViewBag.Message = "Not valid image format";
                    }
                }
                db.Entry(film).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(film);
        }



        // GET: Films/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Film film = db.Films.Find(id);
            if (film == null)
            {
                return HttpNotFound();
            }
            return View(film);
        }

        // POST: Films/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Film film = db.Films.Find(id);
            db.Films.Remove(film);
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
