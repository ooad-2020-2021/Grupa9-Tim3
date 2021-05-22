using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EParking.Models
{
    public class StalniGostMjesecnoPopust : IPopust
    {
        [Key]
        [Required]
        public int ID { get; set; }

        private static StalniGostMjesecnoPopust instanca = new StalniGostMjesecnoPopust();
        
        double IPopust.Iznos { get => ((IPopust)instanca).Iznos; set => ((IPopust)instanca).Iznos = value; }

        private StalniGostMjesecnoPopust()
        {
        }
        public StalniGostMjesecnoPopust getInstance()
        {
            return instanca;
        }
        public string PrikaziGresku()
        {
            return "Nemate pravo na taj popust!";
        }
        

    }
}
