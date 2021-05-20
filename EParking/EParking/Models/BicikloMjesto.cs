using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EParking.Models
{
    public class BicikloMjesto : Mjesto
    {
        public static double cijena;

        public BicikloMjesto(int sprat, int red, int kolona, Boolean zauzeto, int cijena)
        {
            Sprat = sprat;
            Red = red;
            Kolona = kolona;
            Zauzeto = zauzeto;
            ID = generisiID();
        }
        public BicikloMjesto() { }

        public override double dajCijena()
        {
            return cijena;
        }
        public override void postaviCijena(double cijenaParkinga)
        {
            cijena = cijenaParkinga;
        }
    }
}
