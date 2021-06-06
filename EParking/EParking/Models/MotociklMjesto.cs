using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EParking.Models
{
    public class MotociklMjesto : Mjesto
    {

       
        public MotociklMjesto() { }

        public override double dajCijena(double cijena)
        {
            return cijena/5;
        }
        
    }
}
