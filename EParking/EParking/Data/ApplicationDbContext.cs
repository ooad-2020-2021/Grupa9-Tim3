using EParking.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EParking.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
          
        }
        public DbSet<EParking.Models.Korisnik> Korisnik { get; set; }
        public DbSet<EParking.Models.Mjesto> Mjesto { get; set; }
        public DbSet<EParking.Models.RegistrovaniKorisnik> RegistrovaniKorisnik { get; set; }
        public DbSet<EParking.Models.Rezervacija> Rezervacija { get; set; }
        public DbSet<EParking.Models.AutobusMjesto> AutobusMjesto { get; set; }
        public DbSet<EParking.Models.AutomobilMjesto> AutomobilMjesto { get; set; }
        public DbSet<EParking.Models.BicikloMjesto> BicikloMjesto { get; set; }
        public DbSet<EParking.Models.KamionMjesto> KamionMjesto { get; set; }
        public DbSet<EParking.Models.KampKucicaMjesto> KampKucicaMjesto { get; set; }
        public DbSet<EParking.Models.MotociklMjesto> MotociklMjesto { get; set; }
        public DbSet<EParking.Models.StalniGostUzastopnoPopust> StalniGostUzastopnoPopust { get; set; }
        public DbSet<EParking.Models.StalniGostMjesecnoPopust> StalniGostMjesecnoPopust { get; set; }
        public DbSet<EParking.Models.OsobaSInvaliditetomPopust> OsobaSInvaliditetomPopust { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var entity in builder.Model.GetEntityTypes())
            {
                if (entity.BaseType == null)
                {
                    entity.SetTableName(entity.DisplayName());
                }
            }
            /*
            builder.Entity<Korisnik>().ToTable("Korisnik");
            builder.Entity<Mjesto>().ToTable("Mjesto");
            builder.Entity<RegistrovaniKorisnik>().ToTable("RegistrovaniKorisnik");
            builder.Entity<Rezervacija>().ToTable("Rezervacija");
            builder.Entity<AutobusMjesto>().ToTable("AutobusMjesto");
            builder.Entity<AutomobilMjesto>().ToTable("AutomobilMjesto");
            builder.Entity<BicikloMjesto>().ToTable("BicikloMjesto");
            builder.Entity<KamionMjesto>().ToTable("KamionMjesto");
            builder.Entity<KampKucicaMjesto>().ToTable("KampKucicaMjesto");
            builder.Entity<MotociklMjesto>().ToTable("MotociklMjesto");
            builder.Entity<StalniGostUzastopnoPopust>().ToTable("StalniGostUzastopnoPopust");
            builder.Entity<StalniGostMjesecnoPopust>().ToTable("StalniGostMjesecnoPopust");
            builder.Entity<OsobaSInvaliditetomPopust>().ToTable("OsobaSInvaliditetomPopust");
            */
            base.OnModelCreating(builder);

        }
    }
}
