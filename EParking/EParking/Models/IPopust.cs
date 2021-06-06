using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EParking.Models
{
    interface IPopust
    {
        public double dajIznos();
        public int dajKriterij();
        public string PrikaziGresku();

    }
}
