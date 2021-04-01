using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Aplikacija.Models.Validacije
{      
    //Validacija- broj polaznika mora biti veći od nule
    public class NeManjeOdJedanAttribute:ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if(value is Seminar)
            {
                Seminar mr = (Seminar)value;
                if (mr.MaxBrojPolaznika<=0)
                {
                    return false;
                }
            }        
            return true;
        }
    }
}