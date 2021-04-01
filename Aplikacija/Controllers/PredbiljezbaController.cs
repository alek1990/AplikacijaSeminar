using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Aplikacija.Models;

namespace Aplikacija.Controllers
{
    [AllowAnonymous]
    public class PredbiljezbaController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
       
        // Lista seminara za predbilježbu korisnika
        public ActionResult Index(string pretraga)
        {
            var seminari = _db.Seminari.Where(s => s.Popunjen == false && s.Zatvaranje==false).OrderBy(s=>s.DatumPocetkaSeminara).ToList();

            if (!String.IsNullOrEmpty(pretraga))
            {
                seminari = seminari.Where(s => s.Naziv.ToLower().Contains(pretraga.ToLower())).ToList();
            }

            return View(seminari);
        }

        //Predbilježba korisnika na seminar
        [HttpGet]
        public ActionResult PredbiljeziSe(int id)
        {
            var seminar = _db.Seminari.Where(s => s.IdSeminar == id).FirstOrDefault();
            ViewBag.Naziv = seminar.Naziv;

            return View(new Predbiljezba());
        }

        [HttpPost]
        public ActionResult PredbiljeziSe(int id, Predbiljezba predbiljezba)
        {
            
            if (ModelState.IsValid)
            {
                 predbiljezba.Datum = DateTime.Now;
                 predbiljezba.SeminarId = id;
                _db.Predbiljezbe.Add(predbiljezba);
                _db.SaveChanges();
                TempData["Predbiljezba"] = $"Uspješno ste se predbilježili za seminar \"{_db.Predbiljezbe.Where(p=>p.SeminarId==id).Select (p=>p.Seminar.Naziv).FirstOrDefault() }\"!"; 

                return RedirectToAction("Index" );
            }


                return View(predbiljezba);
            
        }
    }
}