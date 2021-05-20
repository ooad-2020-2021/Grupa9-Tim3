using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EParking.Models
{
    public class MotociklMjesto : Mjesto
    {
        public static double cijena;

        public MotociklMjesto(int sprat, int red, int kolona, Boolean zauzeto, int cijena)
        {
            Sprat = sprat;
            Red = red;
            Kolona = kolona;
            Zauzeto = zauzeto;
            ID = generisiID();
        }
        public MotociklMjesto() { }

        public override double dajCijena()
        {
            return cijena/5;
        }
        public override void postaviCijena(double cijenaParkinga)
        {
            cijena = cijenaParkinga;
        }
    }
}
