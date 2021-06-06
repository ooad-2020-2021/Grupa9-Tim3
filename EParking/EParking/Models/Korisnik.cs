using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EParking.Models
{

    public class Korisnik : IdentityUser
    {
        #region Properties
        public Boolean Invaliditet { get; set; }

        public int ProvedenoVrijeme { get; set; }
        public int UzastopnoVrijeme { get; set; }

        
        #endregion
    }
}
