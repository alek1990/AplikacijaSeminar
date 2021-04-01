using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Aplikacija.Models;

namespace Aplikacija.Controllers
{
    [Authorize]
    public class SeminariController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // Lista seminara za uređivanje
        public ActionResult Index(string pretraga)
        {
 
 
            var seminari = db.Seminari.ToList();

            if (!String.IsNullOrEmpty(pretraga))
            {
                seminari = seminari.Where(s => s.Naziv.ToLower().Contains(pretraga.ToLower())).ToList();
            }

            return View(seminari);
        }

        //Metoda za popunjavanje seminara
        //public void PopuniSeminar(int? id)
        //{
        //    if (id != null)
        //    {
        //    int brojPolaznika = db.Seminari.Find(id).BrojPolaznika;
        //    int maxBroj= db.Seminari.Find(id).MaxBrojPolaznika;

        //    if (brojPolaznika <= maxBroj)
        //    {
        //        db.Seminari.Find(id).Popunjen = false;
        //    }

        //    else
        //    {
        //        db.Seminari.Find(id).Popunjen = true;
        //        db.Seminari.Find(id).Zatvaranje = true;
        //    }

        //    db.SaveChanges();
        //    }


        //}

        //Entity Framework CRUD metode za seminar

        // GET: Seminari/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seminar seminar = db.Seminari.Find(id);
            if (seminar == null)
            {
                return HttpNotFound();
            }
            return View(seminar);
        }

        // GET: Seminari/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Seminari/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdSeminar,Naziv,Opis,DatumDodavanja,DatumPocetkaSeminara,MaxBrojPolaznika,BrojPolaznika,Popunjen,Zatvaranje")] Seminar seminar)
        {
            if (ModelState.IsValid)
            {
                seminar.DatumDodavanja = DateTime.Now;
                db.Seminari.Add(seminar);
                db.SaveChanges();
                TempData["Promjene"] = $"Seminar \"{seminar.Naziv}\" je kreiran!";
                return RedirectToAction("Index");
            }

            return View(seminar);
        }

        // GET: Seminari/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seminar seminar = db.Seminari.Find(id);
            if (seminar == null)
            {
                return HttpNotFound();
            }
            return View(seminar);
        }

        // POST: Seminari/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdSeminar,Naziv,Opis,DatumDodavanja,DatumPocetkaSeminara,BrojPolaznika,MaxBrojPolaznika,Popunjen,Zatvaranje")] Seminar seminar )
        {
        

            if (ModelState.IsValid)
            {
                string poruka = "";
                if (seminar.MaxBrojPolaznika>seminar.BrojPolaznika)
                {
                    seminar.Popunjen = false;
                }

                else
                {
                    seminar.Popunjen = true;
                    poruka = $" Seminar \"{seminar.Naziv}\" je popunjen!";
                }
                
                db.Entry(seminar).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Promjene"] = "Promjene su spremljene!"+poruka;
                return RedirectToAction("Index");
            }
            return View(seminar);
        }

        // GET: Seminari/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seminar seminar = db.Seminari.Find(id);
            if (seminar == null)
            {
                return HttpNotFound();
            }
            return View(seminar);
        }

        // POST: Seminari/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Seminar seminar = db.Seminari.Find(id);
            db.Seminari.Remove(seminar);
            db.SaveChanges();
            TempData["Promjene"] = $"Seminar \"{seminar.Naziv}\" je izbrisan!";
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
