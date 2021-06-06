using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EParking.Models
{
    public class StalniGostMjesecnoPopust : IPopust
    {
        [Key]
        [Required]
        public int ID { get; set; }

        private static StalniGostMjesecnoPopust instanca = new StalniGostMjesecnoPopust();

        [Required]
        [RegularExpression(@"[0-9|,]*", ErrorMessage = "Morate unijeti broj")]
        public static double iznos { get; set; }

        [Required]
        [RegularExpression(@"[0-9]*", ErrorMessage = "Morate unijeti cijeli broj")]

        public static int kriterij { get; set; }

        private StalniGostMjesecnoPopust()
        {
            iznos = 0.1;
            kriterij = 10;
        }
        public static StalniGostMjesecnoPopust getInstance()
        {
            return instanca;
        }
        public double dajIznos()
        {
            return iznos;
        }
        public int dajKriterij()
        {
            return kriterij;
        }
        public void postaviIznos(double noviIznos)
        {
            iznos = noviIznos;
        }
        public void postaviKriterij(double noviKriterij)
        {
            iznos = noviKriterij;
        }
        public string PrikaziGresku()
        {
            return "Korisnik nema pravo na popust!";
        }


    }
}
