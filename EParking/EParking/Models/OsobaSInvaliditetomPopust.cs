using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EParking.Models
{
    public class OsobaSInvaliditetomPopust : IPopust
    {
        private static OsobaSInvaliditetomPopust instanca = new OsobaSInvaliditetomPopust();
        double IPopust.Iznos { get => ((IPopust)instanca).Iznos; set => ((IPopust)instanca).Iznos = value; }

        private OsobaSInvaliditetomPopust()
        {
        }
        public OsobaSInvaliditetomPopust getInstance()
        {
            return instanca;
        }
        public string PrikaziGresku()
        {
            return "Nemate pravo na taj popust!";
        }
        
    }
}
