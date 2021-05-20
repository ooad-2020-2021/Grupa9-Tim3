using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EParking.Models
{
    public enum Popust
    {
        StalniGostUzastopno = 10,
        StalniGostMjesecno = 15,
        OsobaSInvaliditetom = 40,
        PopustIgrica = 15
    }

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
