using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EParking.Models
{
    public class KampKucicaMjesto : Mjesto
    {
        public static double cijena;

        public KampKucicaMjesto(int sprat, int red, int kolona, Boolean zauzeto, int cijena) 
        {
            Sprat = sprat;
            Red = red;
            Kolona = kolona;
            Zauzeto = zauzeto;
            ID = generisiID();
        }
        public KampKucicaMjesto(){}

        public override double dajCijena() 
        {
            return cijena*(cijena+3)/2;
        }
        public override void postaviCijena(double cijenaParkinga)
        {
            cijena = cijenaParkinga;
        }
    }
}
