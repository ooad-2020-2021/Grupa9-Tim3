using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EParking.Models
{
    public class Parking
    {
        [Key]
        [Required]
        public int ID { get; set; }
        [Required]
        [RegularExpression(@"[a-z|A-Z| ]*")]
        public string Naziv { get; set; }


        public TimeSpan PocetakRadnogVremena { get; set; }
        public TimeSpan KrajRadnogVremena { get; set; }
        public TimeSpan PocetakJeftinogVremena { get; set; }
        public TimeSpan KrajJeftinogVremena { get; set; }
        [Required]
        public string Sifra { get; set; }

        [Required]
        public Boolean OdobrenSGMjesecno { get; set; }
        [Required]
        public Boolean OdobrenSGUzastopno { get; set; }
        [Required]
        public Boolean OdobrenOSInvaliditetom { get; set; }
        [Required]
        [RegularExpression(@"-?\d+(?:\.\d+)?", ErrorMessage = "Morate unijeti broj")]
        public double Cijena { get; set; }


        public Parking() { }



    }
}
