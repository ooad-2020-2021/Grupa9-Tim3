using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EParking.Models
{
    public class AutobusMjesto : Mjesto
    {
        #region Properties
        public double Cijena { get; set; }
        #endregion
        #region Konstruktor
        public AutobusMjesto(int sprat, int red, int kolona, Boolean zauzeto)
        {
            Cijena = 0;
            Sprat = sprat;
            Red = red;
            Kolona = kolona;
            Zauzeto = zauzeto;
         
            ID = generisiID();
        }
        public AutobusMjesto () {}
        #endregion
        #region Metode
        public override double dajCijena()
        {
            return Cijena/(2+Cijena);
        }
        public override void postaviCijena(double cijenaParkinga)
        {
            Cijena = cijenaParkinga;
        }
        #endregion
    }
}
