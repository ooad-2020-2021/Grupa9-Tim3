using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EParking.Models
{

    public class RegistrovaniKorisnik : Korisnik
    {
        #region Properties
        public int ProvedenoVrijeme { get; set; }
        public int UzastopnoVrijeme { get; set; }
        
        #endregion

        #region Konstruktor
       
        public RegistrovaniKorisnik (){ }

        #endregion
    }
}
