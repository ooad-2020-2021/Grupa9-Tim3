﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EParking.Models
{
    public class IzabranaKategorijaVozila
    {
        [Key]
        public int ID { get; set; }
        public string Vozilo { get; set; }
        public IzabranaKategorijaVozila()
        {

        }
    }
}
