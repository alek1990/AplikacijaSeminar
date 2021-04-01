using Aplikacija.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Aplikacija.Models.Validacije
{
    //Validacija- maksimalan broj polaznika ne smije biti manji od prihvaćenog broja polaznika
    public class NeManjiOdBrojaPolaznikaAttribute:ValidationAttribute
    {      
        public override bool IsValid(object value)
        {
            if (value is Seminar)
            {
                Seminar mr = (Seminar)value;
                if (mr.MaxBrojPolaznika < mr.BrojPolaznika)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
