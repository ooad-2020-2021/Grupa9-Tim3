using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EParking.Models
{
    public class AutobusMjesto : Mjesto
    {
        
        public AutobusMjesto () {}
        #region Metode
        public override double dajCijena(double cijena)
        {
            return cijena/(2+cijena);
        }
       
        #endregion
    }
}
