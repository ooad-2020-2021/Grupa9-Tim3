using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EParking.Models
{
    public class AutomobilMjesto : Mjesto
    {
        
        public AutomobilMjesto() { }

        public override  double dajCijena(double cijena)
        {
            return cijena;
        }
        public double DajCijena(double cijena)
        {
            return cijena;
        }
    }
}
