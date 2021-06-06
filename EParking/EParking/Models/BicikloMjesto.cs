using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EParking.Models
{
    public class BicikloMjesto : Mjesto
    {
        
       
        public BicikloMjesto() { }

        public override double dajCijena(double cijena)
        {
            return cijena;
        }
        
    }
}
