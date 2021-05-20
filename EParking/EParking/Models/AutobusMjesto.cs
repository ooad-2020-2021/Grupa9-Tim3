using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EParking.Models
{
    public class AutobusMjesto : Mjesto
    {
        public static double cijena;

        public AutobusMjesto(int sprat, int red, int kolona, Boolean zauzeto, int cijena)
        {
            Sprat = sprat;
            Red = red;
            Kolona = kolona;
            Zauzeto = zauzeto;
            ID = generisiID();
        }
        public AutobusMjesto() { }

        public override double dajCijena()
        {
            return cijena/(2+cijena);
        }
        public override void postaviCijena(double cijenaParkinga)
        {
            cijena = cijenaParkinga;
        }
    }
}
