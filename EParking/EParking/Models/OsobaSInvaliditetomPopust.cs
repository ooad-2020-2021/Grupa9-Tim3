using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EParking.Models
{
    public class OsobaSInvaliditetomPopust : IPopust
    {
        [Key]
        [Required]
        public int ID { get; set; }

        private static OsobaSInvaliditetomPopust instanca = new OsobaSInvaliditetomPopust();
        [Required]
        [RegularExpression(@"[0-9|,]*", ErrorMessage = "Morate unijeti broj")]

        public static double iznos { get; set; }

        private OsobaSInvaliditetomPopust()
        {
            iznos = 0.1;
        }
        public static OsobaSInvaliditetomPopust getInstance()
        {
            return instanca;
        }
        public double dajIznos()
        {
            return iznos;
        }
        public int dajKriterij()
        {
            return 0;
        }
        public void postaviIznos(double noviIznos)
        {
            iznos = noviIznos;
        }
        
        public string PrikaziGresku()
        {
            return "Korisnik nema pravo na popust!";
        }

    }
}
