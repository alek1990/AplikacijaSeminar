using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Aplikacija.Models.Validacije;

namespace Aplikacija.Models
{
    
    [NeManjiOdBrojaPolaznika(ErrorMessage ="Maskimalan broj polaznika ne može biti manji od prihvaćenog broja predbilježbi.")]
    [NeManjeOdJedan(ErrorMessage ="Maksimalan broj polaznika mora biti veći od 0!")]
    [NeManjeOdDanas(ErrorMessage ="Datum početka seminara ne smije biti stariji od današnjeg datuma!")]
    public class Seminar
    {   [Key]
        public int IdSeminar { get; set; }

        [Required(ErrorMessage ="Naziv seminara je obavezan!")]
        [StringLength(50)]
        public string Naziv { get; set; }

        [Required(ErrorMessage ="Opis seminara je obavezan!")]
        [StringLength(200)]
        public string Opis { get; set; }
        
        [Display(Name ="Datum i vrijeme dodavanja")]
        [Required]
        public DateTime DatumDodavanja { get; set; }

        [Display(Name ="Datum početka seminara")]
        [DisplayFormat(DataFormatString ="{0:d}", ApplyFormatInEditMode =true)]
        [Required(ErrorMessage ="Datum početka seminara je obavezan!")]
        public DateTime DatumPocetkaSeminara { get; set; }

        [Display(Name = "Maksimalan broj polaznika")]
        [Required(ErrorMessage ="Maksimalan broj polaznika je obavezan:")]
        public int MaxBrojPolaznika { get; set; }

        [Display(Name = "Broj polaznika")]
        public int BrojPolaznika { get; set; }

        [Display(Name = "Popunjen (automatski)")]
        public bool Popunjen { get; set; }

        [Display(Name = "Zatvaranje (ručno)")]
        public bool Zatvaranje { get; set; }



    }
}