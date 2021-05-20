using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EParking.Models
{
    interface IPopust
    {
        double Iznos { get; set; }
        
        public string PrikaziGresku();
    }
}
