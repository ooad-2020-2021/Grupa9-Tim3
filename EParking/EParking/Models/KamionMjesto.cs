using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EParking.Models
{
    public class KamionMjesto : Mjesto
    {
        
        public KamionMjesto() { }

        public override double dajCijena(double cijena)
        {
            return cijena*cijena;
        }
        
    }
}
