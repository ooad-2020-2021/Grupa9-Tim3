using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EParking.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
          
        }
        public DbSet<EParking.Models.Korisnik> Korisnik { get; set; }
        public DbSet<EParking.Models.Mjesto> Mjesto { get; set; }
        
        public DbSet<EParking.Models.RegistrovaniKorisnik> RegistrovaniKorisniks { get; set; }
        public DbSet<EParking.Models.Rezervacija> Rezervacija { get; set; }
    }
}
