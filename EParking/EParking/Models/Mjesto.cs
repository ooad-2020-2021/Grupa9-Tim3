using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EParking.Models
{
    public enum KategorijaVozila
    {
        Automobil,
        Motocikl,
        Biciklo,
        Kamion,
        KampKucica,
        Kombi
    }

    public class Mjesto
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
        [Required]
        public KategorijaVozila Kategorija { get; set; }

        #endregion

        #region Konstruktor
        public Mjesto (int sprat, int red, int kolona, Boolean zauzeto, KategorijaVozila kategorija)
        {
            ID = generisiID();
            Sprat = sprat;
            Red = red;
            Kolona = kolona;
            Zauzeto = zauzeto;
            Kategorija = kategorija;
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
