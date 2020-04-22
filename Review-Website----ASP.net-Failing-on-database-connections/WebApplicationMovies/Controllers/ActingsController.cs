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
    public class ActingsController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Actings
        public ActionResult Index()

        {

            List<ActingListViewModel> ActingList =
                new List<ActingListViewModel>();

            List<Acting> actingCredits;

            actingCredits = db.Actings.ToList();

            foreach (Acting a in actingCredits)
            {

                Film film = db.Films.Where(x => x.FilmID == a.FilmId).Single();

                
                Entry actor = db.Entries.Where(x => x.EntryID == a.PersonId).Single();


                ActingListViewModel toAdd = new ActingListViewModel();


                toAdd.ActingCredit = a;
                toAdd.Film = film;
                toAdd.Actor = actor;



                ActingList.Add(toAdd);
            }
            return View(ActingList);


        }



        // GET: Actings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Acting acting = db.Actings.Find(id);
            if (acting == null)
            {
                return HttpNotFound();
            }
            return View(acting);
        }

        // GET: Actings/Create
        public ActionResult Create(int FilmId = 0, int EntryId = 0)
        {
            var filmQuery = from m in db.Films
                orderby m.FilmTitle 
                select m;

            if (FilmId == 0)
                ViewBag.FilmId = new SelectList(filmQuery, "FilmID",
                                                "FilmTitle", null);
            else
                ViewBag.FilmId = new SelectList(filmQuery, "FilmID",
                                                "FilmTitle", FilmId);



            var personsQuery = from p in db.Entries
                               orderby p.EntrySname
                               select new
                               {
                                   Name = p.EntryFname + " " + p.EntrySname,
                                   p.EntryID
                               };

                                if (EntryId == 0) 
                                
                                ViewBag.EntryId = new SelectList(personsQuery, "EntryId",
                                                "Name", null);

                                else 
                                
                                    ViewBag.EntryId = new SelectList(personsQuery, "EntryId",
                                                "Name", EntryId);

            return View();
        }
        














        // POST: Actings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ActingId,PersonId,FilmId")] Acting acting)
        {
            if (ModelState.IsValid)
            {
                db.Actings.Add(acting);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(acting);
        }

        // GET: Actings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Acting acting = db.Actings.Find(id);
            if (acting == null)
            {
                return HttpNotFound();
            }
            return View(acting);
        }

        // POST: Actings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ActingId,PersonId,FilmId")] Acting acting)
        {
            if (ModelState.IsValid)
            {
                db.Entry(acting).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(acting);
        }

        // GET: Actings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Acting acting = db.Actings.Find(id);
            if (acting == null)
            {
                return HttpNotFound();
            }
            return View(acting);
        }

        // POST: Actings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Acting acting = db.Actings.Find(id);
            db.Actings.Remove(acting);
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
