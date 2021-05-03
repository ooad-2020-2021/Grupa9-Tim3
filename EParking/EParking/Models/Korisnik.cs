using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EParking.Models
{
    public class Korisnik
    {   
        public static int zadnjiID=0;

        #region Properties
        [Key]
        [Required]
        public int ID { get; set; }

        [Required]
        public string ImeIPrezime { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        #endregion

        #region Konstruktor

        public Korisnik (string imeIPrezime, string email, string password)
        {
            ID = generisiID();
            ImeIPrezime = imeIPrezime;
            Email = email;
            Password = password;
        }

        private int generisiID()
        {
           
            return zadnjiID++;
         
        }

        #endregion

        
    }
}
