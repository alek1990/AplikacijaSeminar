using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Aplikacija.Models;


namespace Aplikacija.Controllers
{
    [Authorize]
    public class PredbiljezbeController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        
        // Lista predbilježbi - svi
        public ActionResult Index(string pretraga)
        {


            var predbiljezbe = _db.Predbiljezbe.ToList();

            if (!String.IsNullOrEmpty(pretraga))
            {
                predbiljezbe = predbiljezbe.Where(
                    p => p.Ime.ToLower().Contains(pretraga.ToLower()) 
                || p.Prezime.ToLower().Contains(pretraga.ToLower())
                || p.Seminar.Naziv.ToLower().Contains(pretraga.ToLower())
                ).ToList();
            }


            return View(predbiljezbe);
        }

        // Lista predbilježbi - odobreni
        public ActionResult GetOdobreni(string pretraga)
        {
            var odobreni = _db.Predbiljezbe.Where(p => p.Predbiljezen==true).ToList();
            if (!String.IsNullOrEmpty(pretraga))
            {
                odobreni = odobreni.Where(
                    p => p.Ime.ToLower().Contains(pretraga.ToLower())
                || p.Prezime.ToLower().Contains(pretraga.ToLower())
                || p.Seminar.Naziv.ToLower().Contains(pretraga.ToLower())
                ).ToList();
            }
            return View("Index",odobreni);
        }

        // Lista predbilježbi - odbijeni
        public ActionResult GetOdbijeni(string pretraga)
        {
            var odbijeni = _db.Predbiljezbe.Where(p => p.Predbiljezen == false).ToList();
            if (!String.IsNullOrEmpty(pretraga))
            {
                odbijeni = odbijeni.Where(
                    p => p.Ime.ToLower().Contains(pretraga.ToLower())
                || p.Prezime.ToLower().Contains(pretraga.ToLower())
                || p.Seminar.Naziv.ToLower().Contains(pretraga.ToLower())
                ).ToList();
            }
            return View("Index",odbijeni);
        }

        // Lista predbilježbi - neobrađeni
        public ActionResult GetNeobradjeni(string pretraga)
        {
            var neobradjeni = _db.Predbiljezbe.Where(p => p.Predbiljezen == null).ToList();
            if (!String.IsNullOrEmpty(pretraga))
            {
                neobradjeni = neobradjeni.Where(
                    p => p.Ime.ToLower().Contains(pretraga.ToLower())
                || p.Prezime.ToLower().Contains(pretraga.ToLower())
                || p.Seminar.Naziv.ToLower().Contains(pretraga.ToLower())
                ).ToList();
            }
            return View("Index",neobradjeni);
        }

        //Odabir predbilježbe za editiranje i potvrdu

        public ActionResult OdaberiPredbiljezbu(int id)
        {

            Predbiljezba predbiljezba = _db.Predbiljezbe.Find(id);


            return View(predbiljezba);
        }

        [HttpPost]
        public ActionResult OdaberiPredbiljezbu(int id, int idS, Predbiljezba predbiljezba)
        {
            Seminar seminar = _db.Seminari.Find(idS);
            ViewBag.Seminar = seminar.Naziv;

            bool? primljenOld = _db.Predbiljezbe.Where(p => p.IdPredbiljezba==id).Select(p=>p.Predbiljezen).FirstOrDefault();
            bool? primljen = predbiljezba.Predbiljezen;
            int brojPolaznika = seminar.BrojPolaznika;
            int maxBroj = seminar.MaxBrojPolaznika;
            bool popunjen = seminar.Popunjen;



            if (ModelState.IsValid)
            {
                //Logika za automatsko računanje broja prihvaćenih predbilježbi
                int broj = 0;

                if (primljenOld == true)
                {
                    if (primljen == true)
                    {
                        broj = 0;
                    }

                    else
                    {
                        broj = -1;
                    }

                }

                else
                {
                    if (primljen == true)
                    {
                        broj = 1;
                    }

                    else
                    {
                        broj = 0;
                    }

                }

                //Automatsko popunjavanje seminara
                string poruka = "";
                bool flag = false;

                //if uvjet - zatvara seminar (Popunjen= true)
                if ((brojPolaznika == (maxBroj - 1) && (primljen == true) && (primljenOld==false || primljenOld==null) )) //ovdje sam morao dodati primljenOld==null
                {
                     flag = true;
                     popunjen = true;
                     poruka = $" Seminar \"{seminar.Naziv}\" je popunjen!";
                }

                // if uvjet - ponovno otvara seminar (Popunjen=false)
                if ((brojPolaznika == maxBroj && (primljen == false || primljen == null) && primljenOld == true))
                {
                    popunjen = false;
                }

                //If uvjet - seminar je već popunjen!
                if (popunjen == true && primljen == true && primljenOld == false && flag==false)
                {
                    TempData["Message"] = "Seminar je već popunjen! Za prihvaćanje novih predbilježbi povećajte maksimalan broj polaznika.";
                    return View(predbiljezba);
                }

                //Spremanje u bazu
                else
                {
                    _db.Seminari.Find(idS).Popunjen = popunjen;
                    _db.Seminari.Find(idS).BrojPolaznika += broj;

                    _db.Entry(predbiljezba).State = EntityState.Modified;
                    _db.SaveChanges();

                    TempData["Message"] = "Promjene su spremljene!" + poruka;
                    return RedirectToAction("Index");
                }


            }
            return View(predbiljezba);

        }
    }
}