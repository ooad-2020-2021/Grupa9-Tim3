using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EParking.Models
{
    public class RegistrovaniKorisnik : Korisnik
    {
        #region Properties
        public int ProvedenoVrijeme { get; set; }
        public int UzastopnoVrijeme { get; set; }
        public Popust PopustKorisnika { get; set; }
        #endregion

        #region Konstruktor
        public RegistrovaniKorisnik (string imeIPrezime, string email, string password,
            int uzastopnoVrijeme, int provedenoVrijeme) : base(imeIPrezime,email,password)
        {
            UzastopnoVrijeme = uzastopnoVrijeme;
            ProvedenoVrijeme = provedenoVrijeme;
        }

        public RegistrovaniKorisnik (string imeIPrezime, string email, string password) : base (imeIPrezime, email, password) { }

        #endregion
    }
}
