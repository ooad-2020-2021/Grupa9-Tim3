using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public List<Popust> PopustKorisnika { get; set; }
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

        #region Metode
        public Boolean addPopust (string popust)
        {
            if (popust.Equals("StalniGostMjesecno"))
            {
                if (ProvedenoVrijeme > 100)
                {
                    PopustKorisnika.Add(Popust.StalniGostMjesecno);
                    return true;
                }
                               
            }
            if (popust.Equals("StalniGostUzastopno"))
            {
                if (UzastopnoVrijeme > 30) { 
                    PopustKorisnika.Add(Popust.StalniGostUzastopno);
                    return true;
                }

            }
            if (popust.Equals("OsobaSInvaliditetom"))
            {
                PopustKorisnika.Add(Popust.OsobaSInvaliditetom);
                return true;
            }

            return false;
        }


        #endregion
    }
}
