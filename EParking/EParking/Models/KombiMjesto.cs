using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EParking.Models
{
    public class KombiMjesto : Mjesto
    {
        
        public KombiMjesto() { }

        public override double dajCijena(double cijena)
        {
            return cijena;
        }
       
    }
}
