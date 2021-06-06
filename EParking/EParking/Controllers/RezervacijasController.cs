using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EParking.Data;
using EParking.Models;
using System.Net.Http;
using System.Net;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace EParking.Controllers
{
    

    public class RezervacijasController : Controller
    {
       
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Korisnik> _userManager;
        
        public RezervacijasController(ApplicationDbContext context, UserManager<Korisnik> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        

        [HttpGet]
        public async Task<IActionResult> Rezervisanje(int id, string kategorija)
        {
            IEnumerable<Mjesto> mjesta = (IEnumerable<Mjesto>)await _context.Mjesto.Where(mjesto => mjesto.ParkingId.Equals(id) && mjesto.Discriminator.Equals(kategorija + "Mjesto")).ToListAsync();
            ViewBag.mjesta = mjesta;
            return View();

        }

        [HttpPost]

        public async Task<IActionResult> Rezervisanje(int id, string kategorija, [Bind("ID,KorisnikID,MjestoID,VrijemePocetka,VrijemeIsteka")] Rezervacija rezervacija)
        {
            IEnumerable<Mjesto> mjesta = (IEnumerable<Mjesto>)await _context.Mjesto.Where(mjesto => mjesto.ParkingId.Equals(id) && mjesto.Discriminator.Equals(kategorija + "Mjesto")).ToListAsync();
            ViewBag.mjesta = mjesta;
            if (ModelState.IsValid)
            {
                OsobaSInvaliditetomPopust osobaSInvaliditetomPopust = OsobaSInvaliditetomPopust.getInstance();
                StalniGostMjesecnoPopust stalniGostMjesecnoPopust= StalniGostMjesecnoPopust.getInstance();
                StalniGostUzastopnoPopust stalniGostUzastopnoPopust = StalniGostUzastopnoPopust.getInstance();

                var user = await _userManager.GetUserAsync(User);
                var trenutniKorisnik = await _context.Users.FirstOrDefaultAsync(usercic => usercic.UserName.Equals(user.UserName));

                Mjesto mjesto = await _context.Mjesto.FindAsync(rezervacija.MjestoID);
                mjesto.Zauzeto = true;
                

                Parking parking = await _context.Parking.FindAsync(mjesto.ParkingId);
                rezervacija.Cijena = mjesto.dajCijena(parking.Cijena);
                if (stalniGostMjesecnoPopust.dajKriterij() < trenutniKorisnik.ProvedenoVrijeme)
                {
                    rezervacija.Cijena = rezervacija.Cijena - stalniGostMjesecnoPopust.dajIznos()* rezervacija.Cijena;
                }
                if (stalniGostUzastopnoPopust.dajKriterij() < trenutniKorisnik.UzastopnoVrijeme)
                {
                    rezervacija.Cijena = rezervacija.Cijena - stalniGostUzastopnoPopust.dajIznos()* rezervacija.Cijena;
                }
                if (trenutniKorisnik.Invaliditet)
                {
                    rezervacija.Cijena = rezervacija.Cijena - osobaSInvaliditetomPopust.dajIznos()* rezervacija.Cijena;
                }
                
                if (parking.PocetakJeftinogVremena != new TimeSpan(0,0,0) && parking.KrajJeftinogVremena != new TimeSpan(0, 0, 0))
                {
                    if (rezervacija.VrijemePocetka.TimeOfDay > parking.PocetakJeftinogVremena && rezervacija.VrijemeIsteka.TimeOfDay < parking.KrajJeftinogVremena)
                    {
                        rezervacija.Cijena = rezervacija.Cijena - 0.1 * rezervacija.Cijena;
                    }
                }
                rezervacija.KorisnikID = trenutniKorisnik.Id;
                List<Rezervacija> rezervacije = await _context.Rezervacija.ToListAsync();
                if (rezervacije.Count > 0)
                {
                    rezervacija.ID = rezervacije[rezervacije.Count - 1].ID + 1;
                }
                _context.Add(rezervacija);
                await _context.SaveChangesAsync();
                _context.Update(mjesto);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Pocetna");
                
                
            }
            return View(rezervacija);

        }

        public async Task<IActionResult> KategorijaVozila()
        {
            IzabranaKategorijaVozila vozilica = await _context.IzabranaKategorijaVozila.FindAsync(1);
            return View(vozilica);
        }
        [HttpPost]
        
        public async Task<IActionResult> KategorijaVozila([Bind("ID,Vozilo")] IzabranaKategorijaVozila odabranoVozilo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(odabranoVozilo);
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IzabranaKategorijaVozilaExists(odabranoVozilo.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                    
                }
                
                return RedirectToAction("OdabirParkinga",new { id = odabranoVozilo.ID});
            }
            return View(odabranoVozilo);
        }
        public async Task<IActionResult> OdabirParkinga(int id)
        {
            IzabranaKategorijaVozila odabranovoziloKlase = await _context.IzabranaKategorijaVozila.FindAsync(id);
            List<Parking> parkinzi = await _context.Parking.ToListAsync();
            List<Parking> odabraniParkinzi = new List<Parking>();
            List<Mjesto> svaMjesta = await _context.Mjesto.Where(mjesto => mjesto.Discriminator.Equals(odabranovoziloKlase.Vozilo + "Mjesto") && mjesto.Zauzeto==false).ToListAsync();
            
            foreach(Parking parking in parkinzi)
            {
               if(svaMjesta.Find(mjesto => mjesto.ParkingId.Equals(parking.ID))!=null)
               {
                    odabraniParkinzi.Add(parking);
               }
            }
            string poruka = "";
            if (odabraniParkinzi.Count == 0)
            {
                poruka = "Nema pakringa sa slobodnim mjestom za vašu vrstu vozila";
            }
            ViewBag.poruka = poruka;
            ViewBag.kategorija = odabranovoziloKlase.Vozilo;
            return View(odabraniParkinzi);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rezervacija = await _context.Rezervacija
                .FirstOrDefaultAsync(m => m.ID == id);
            if (rezervacija == null)
            {
                return NotFound();
            }

            return View(rezervacija);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]


        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rezervacija = await _context.Rezervacija.FindAsync(id);
            var mjesto = await _context.Mjesto.FindAsync(rezervacija.MjestoID);
            mjesto.Zauzeto = false;
            _context.Update(mjesto);
            _context.Rezervacija.Remove(rezervacija);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index","Pocetna");
        }


        private bool IzabranaKategorijaVozilaExists(int id)
        {
            return _context.IzabranaKategorijaVozila.Any(e => e.ID == id);
        }
    }
}
