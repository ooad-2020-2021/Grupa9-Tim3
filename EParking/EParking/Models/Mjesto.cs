using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EParking.Models
{

    public abstract class Mjesto : IMjesto
    {
        public static int zadnjiID = 0;

        #region Properties
        [Key]
        [Required]
        public int ID { get; set; }
        [Required]
        public int Sprat { get; set; }
        [Required]
        public int Red { get; set; }
        [Required]
        public int Kolona { get; set; }
        [Required]
        public Boolean Zauzeto { get; set; }
        
        #endregion
        

        #region Metode
        public int generisiID()
        {
            return zadnjiID++;
        }

        virtual public double dajCijena()
        {
            throw new NotImplementedException();
        }
        virtual public void postaviCijena(double cijena)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
