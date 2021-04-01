using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Aplikacija.Models.Validacije
{
    //Validacija - Ključ za regisraciju zaposlenika
    public class KljucAttribute:ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is RegisterViewModel)
            {
                RegisterViewModel model = (RegisterViewModel)value;
                if (model.Kljuc != "Zaposlenik")
                {
                    return false;
                }

            }
            return true;
        }
    }
}
