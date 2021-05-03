using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EParking.Models
{
    public class Rezervacija
    {
        public static int zadnjiID = 0;

        #region Properties
        [Key]
        [Required]
        public int ID { get; set; }

        [Required]
        public Korisnik Korisnik { get; set; }   
        [Required]
        public Mjesto Mjesto { get; set; }
        [Required]
        public DateTime VrijemeIsteka { get; set; }
        #endregion
        
        
        #region Konstruktor
        public Rezervacija (Korisnik korisnik,Mjesto mjesto, DateTime vrijemeIsteka)
        {

            ID = generisiID();
            Korisnik = korisnik;
            Mjesto = mjesto;
            VrijemeIsteka = vrijemeIsteka;
        }
        /*public Rezervacija(Korisnik korisnik, Mjesto mjesto, DateTime vrijemeIsteka)
        {
            ID = generisiID();
            Korisnik = korisnik;
            Mjesto = mjesto;
            VrijemeIsteka = vrijemeIsteka;
        }*/

        public Rezervacija()
        {

        }

        #endregion
        
        
        #region Metode

        public int generisiID()
        {
            return zadnjiID++;
        }

        #endregion

    }
}
