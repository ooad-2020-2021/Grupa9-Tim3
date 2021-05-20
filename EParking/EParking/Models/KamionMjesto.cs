using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EParking.Models
{
    public class KamionMjesto : Mjesto
    {
        public static double cijena;

        public KamionMjesto(int sprat, int red, int kolona, Boolean zauzeto, int cijena)
        {
            Sprat = sprat;
            Red = red;
            Kolona = kolona;
            Zauzeto = zauzeto;
            ID = generisiID();
        }
        public KamionMjesto() { }

        public override double dajCijena()
        {
            return cijena*cijena;
        }
        public override void postaviCijena(double cijenaParkinga)
        {
            cijena = cijenaParkinga;
        }
    }
}
