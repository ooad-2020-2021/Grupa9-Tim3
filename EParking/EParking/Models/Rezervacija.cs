using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EParking.Models
{
    public class DatumIzBuducnosti : RangeAttribute
    {
        public DatumIzBuducnosti() : base(typeof(DateTime), DateTime.Now.ToString(), DateTime.MaxValue.ToString())
        {
        }
    }
    public class Rezervacija
    {
        #region Properties
        [Key]
        [Required]
        public int ID { get; set; }
        [Required]
        [ForeignKey("Korisnik")]
        public string KorisnikID { get; set; }   
        [Required]
        [ForeignKey("Mjesto")]
       
        public int MjestoID { get; set; }
        [Required]
        [DatumIzBuducnosti(ErrorMessage = "Datum mora biti u budućnosti!")]
        public DateTime VrijemePocetka { get; set; }

        [Required]
        [DatumIzBuducnosti(ErrorMessage = "Datum mora biti u budućnosti!")]
        public DateTime VrijemeIsteka { get; set; }

        [Required]
        public double Cijena { get; set; }
        #endregion
        #region Konstruktor
        
        public Rezervacija()
        {

        }
        public Korisnik AzurirajKorisnika(Korisnik k)
        {
            k.ProvedenoVrijeme  = VrijemeIsteka.TimeOfDay.Hours - VrijemePocetka.TimeOfDay.Hours;
            k.UzastopnoVrijeme = VrijemeIsteka.TimeOfDay.Hours - VrijemePocetka.TimeOfDay.Hours;
            return k;
        }

        #endregion
        

    }
}
