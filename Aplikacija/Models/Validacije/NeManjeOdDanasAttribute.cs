using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Aplikacija.Models.Validacije
{
    //Validacija - Datum ne smije biti stariji od danas
    public class NeManjeOdDanasAttribute:ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is Seminar)
            {
                Seminar seminar = (Seminar)value;
                if (seminar.DatumPocetkaSeminara<DateTime.Now)
                {
                    return false;
                }

            }
            return true;
        }
    }
}