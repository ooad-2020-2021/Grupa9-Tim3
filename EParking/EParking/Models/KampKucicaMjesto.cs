using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EParking.Models
{
    public class KampKucicaMjesto : Mjesto
    {
        
        public KampKucicaMjesto(){}

        public override double dajCijena(double cijena) 
        {
            return cijena*(cijena+3)/2;
        }
       
    }
}
