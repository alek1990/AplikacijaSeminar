using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Aplikacija.Models
{
    public class Predbiljezba
    {
        [Key]
        public int IdPredbiljezba { get; set; }
        
        [Display(Name ="Datum i vrijeme prijave")]
        [Required]
        public DateTime Datum { get; set; }

        [Required(ErrorMessage = "Ime je obavezno!")]
        [StringLength(50)]
        public string Ime { get; set; }

        [Required(ErrorMessage = "Prezime je obavezno!")]
        [StringLength(50)]
        public string Prezime { get; set; }

        [Required(ErrorMessage ="Adresa je obavezna!")]
        [StringLength(50)]
        public string Adresa { get; set; }

        [Required(ErrorMessage ="Email adresa je obavezna!")]
        [EmailAddress(ErrorMessage ="Email adresa je neispravna!")]
        [StringLength(50)]
        public string Email { get; set; }

        [Required(ErrorMessage ="Kontakt je obavezan!")]
        [StringLength(50)]
        public string Kontakt { get; set; }

        [StringLength(200)]
        public string Napomena { get; set; }

        [Display(Name ="Status")]
        [DataType("TemplPredbiljezenDisplay")] //Template za display
        [UIHint("TemplPredbiljezen")]  //Template za editor
        public bool? Predbiljezen { get; set; }

        public virtual int SeminarId { get; set; }

        [ForeignKey("SeminarId")]
        public virtual Seminar Seminar { get; set; }
    }
}