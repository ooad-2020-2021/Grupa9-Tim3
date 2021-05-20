using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EParking.Models
{
    public class Korisnik : AspNetUser
    {

        #region Properties
        public Boolean Invaliditet { get; set; }
        #endregion


        #region Konstruktor

        public Korisnik()
        {
        }

        #endregion

        
    }
}
