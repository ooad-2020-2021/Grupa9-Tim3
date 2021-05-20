using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EParking.Models
{
    public class StalniGostUzastopnoPopust : IPopust
    {
        private static StalniGostUzastopnoPopust instanca = new StalniGostUzastopnoPopust();
        double IPopust.Iznos { get => ((IPopust)instanca).Iznos; set => ((IPopust)instanca).Iznos = value; }

        private StalniGostUzastopnoPopust()
        {
        }
        public StalniGostUzastopnoPopust getInstance ()
        {
            return instanca;
        }
        public string PrikaziGresku()
        {
            return "Nemate pravo na taj popust!"; ;
        }

    }
}
