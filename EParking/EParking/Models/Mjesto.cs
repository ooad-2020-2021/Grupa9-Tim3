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
        [RegularExpression(@"[0-9]*", ErrorMessage="Morate unijeti cijeli broj")]
        public int Sprat { get; set; }
        [Required]
        [RegularExpression(@"[0-9]*", ErrorMessage = "Morate unijeti cijeli broj")]
        public int Red { get; set; }
        [Required]
        [RegularExpression(@"[0-9]*", ErrorMessage = "Morate unijeti cijeli broj")]
        public int Kolona { get; set; }
        [Required]
        public Boolean Zauzeto { get; set; }
        
        [Required]
        public string Discriminator { get; set; }
        [Required]
        public int ParkingId { get; set; }
        #endregion

        
        #region Metode


        abstract public double dajCijena(double cijena);

        
        

        #endregion
    }
}
