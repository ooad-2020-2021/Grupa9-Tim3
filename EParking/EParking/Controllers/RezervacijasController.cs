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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

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
        [Authorize(Roles="Administrator,RegistrovaniKorisnik")]
        public async Task<IActionResult> Rezervisanje(int id, string kategorija, int mjestoId)
        {
            IEnumerable<Rezervacija> rezervacije = (IEnumerable<Rezervacija>)await _context.Rezervacija.Where(rezervacija => rezervacija.MjestoID.Equals(mjestoId)).ToListAsync();
            Mjesto mjesto = await _context.Mjesto.FindAsync(mjestoId);
            Parking parking = await _context.Parking.FindAsync(mjesto.ParkingId);
            ViewBag.pocetakRadnogVremena = parking.PocetakRadnogVremena.ToString();
            ViewBag.krajRadnogVremena = parking.KrajRadnogVremena.ToString();
            ViewBag.pocetakJeftinogVremena = parking.PocetakJeftinogVremena.ToString();
            ViewBag.krajJeftinogVremena = parking.KrajJeftinogVremena.ToString();
            ViewBag.rezervacije = rezervacije;
            ViewBag.poruka = "";
            return View();

        }

        [HttpPost]
        [Authorize(Roles = "Administrator,RegistrovaniKorisnik")]
        public async Task<IActionResult> Rezervisanje(int id, string kategorija, int mjestoId, [Bind("ID,KorisnikID,MjestoID,VrijemePocetka,VrijemeIsteka")] Rezervacija rezervacija, IFormCollection form)
        {
            List<Rezervacija> sveRezervacije = await _context.Rezervacija.ToListAsync();
            List<Rezervacija> rezervacije = await _context.Rezervacija.Where(rezervacija => rezervacija.MjestoID.Equals(mjestoId)).ToListAsync();
            ViewBag.rezervacije = rezervacije;
            Mjesto mjesto = await _context.Mjesto.FindAsync(mjestoId);
            Parking parking = await _context.Parking.FindAsync(mjesto.ParkingId);
            ViewBag.pocetakRadnogVremena = parking.PocetakRadnogVremena.ToString();
            ViewBag.krajRadnogVremena = parking.KrajRadnogVremena.ToString();
            ViewBag.pocetakJeftinogVremena = parking.PocetakJeftinogVremena.ToString();
            ViewBag.krajJeftinogVremena = parking.KrajJeftinogVremena.ToString();
            ViewBag.poruka = "";
            bool? zaposlenikRezervacija = null;
            if (!form["zaposlenikRezervacija"].ToString().Equals("")) {
                zaposlenikRezervacija = Convert.ToBoolean(form["zaposlenikRezervacija"].ToString().Split(',')[0]);
            }
            if (ModelState.IsValid)
            {
                if (rezervacija.VrijemeIsteka < rezervacija.VrijemePocetka)
                {
                    ViewBag.poruka = "Vrijeme početka mora biti prije vremena isteka";
                    return View(rezervacija);
                }

                
                foreach (Rezervacija item in rezervacije)
                {
                    if (!(rezervacija.VrijemePocetka > item.VrijemeIsteka || rezervacija.VrijemeIsteka < item.VrijemePocetka))
                    {
                        ViewBag.poruka = "Taj termin je već zauzet";
                        return View(rezervacija);
                    }
                 }
                
                mjesto.Zauzeto = true;
                rezervacija.Cijena = mjesto.dajCijena(parking.Cijena);

                if (sveRezervacije.Count > 0)
                {
                    rezervacija.ID = sveRezervacije[sveRezervacije.Count - 1].ID + 1;
                }

                if (zaposlenikRezervacija == null || zaposlenikRezervacija == false)
                {
                    OsobaSInvaliditetomPopust osobaSInvaliditetomPopust = OsobaSInvaliditetomPopust.getInstance();
                    StalniGostMjesecnoPopust stalniGostMjesecnoPopust = StalniGostMjesecnoPopust.getInstance();
                    StalniGostUzastopnoPopust stalniGostUzastopnoPopust = StalniGostUzastopnoPopust.getInstance();

                    var user = await _userManager.GetUserAsync(User);
                    var trenutniKorisnik = await _context.Users.FirstOrDefaultAsync(usercic => usercic.UserName.Equals(user.UserName));


                    
                    if (stalniGostMjesecnoPopust.dajKriterij() < trenutniKorisnik.ProvedenoVrijeme)
                    {
                        rezervacija.Cijena = rezervacija.Cijena - stalniGostMjesecnoPopust.dajIznos() * rezervacija.Cijena;
                    }
                    if (stalniGostUzastopnoPopust.dajKriterij() < trenutniKorisnik.UzastopnoVrijeme)
                    {
                        rezervacija.Cijena = rezervacija.Cijena - stalniGostUzastopnoPopust.dajIznos() * rezervacija.Cijena;
                    }
                    if (trenutniKorisnik.Invaliditet)
                    {
                        rezervacija.Cijena = rezervacija.Cijena - osobaSInvaliditetomPopust.dajIznos() * rezervacija.Cijena;
                    }

                    if (parking.PocetakJeftinogVremena != new TimeSpan(0, 0, 0) && parking.KrajJeftinogVremena != new TimeSpan(0, 0, 0))
                    {
                        if (rezervacija.VrijemePocetka.TimeOfDay > parking.PocetakJeftinogVremena && rezervacija.VrijemeIsteka.TimeOfDay < parking.KrajJeftinogVremena)
                        {
                            rezervacija.Cijena = rezervacija.Cijena - 0.1 * rezervacija.Cijena;
                        }
                    }
                    rezervacija.KorisnikID = trenutniKorisnik.Id;
                }
                else if(zaposlenikRezervacija==true)
                {
                    rezervacija.KorisnikID = "ZaposlenikRezervacija" + rezervacija.ID.ToString();
                }
                
                _context.Add(rezervacija);
                await _context.SaveChangesAsync();
                _context.Update(mjesto);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Pocetna");
                
                
            }
            return View(rezervacija);

        }
        //[Authorize(Roles = "Administrator,RegistrovaniKorisnik")]
        public async Task<IActionResult> KategorijaVozila()
        {
            IzabranaKategorijaVozila vozilica = await _context.IzabranaKategorijaVozila.FindAsync(1);
            return View(vozilica);
        }
        [HttpPost]
        //[Authorize(Roles = "Administrator,RegistrovaniKorisnik")]
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
        [Authorize(Roles = "Administrator,RegistrovaniKorisnik")]
        public async Task<IActionResult> OdabirParkinga(int id)
        {
            IzabranaKategorijaVozila odabranovoziloKlase = await _context.IzabranaKategorijaVozila.FindAsync(id);
            List<Parking> parkinzi = await _context.Parking.ToListAsync();
            List<Parking> odabraniParkinzi = new List<Parking>();
            List<Mjesto> svaMjesta = await _context.Mjesto.Where(mjesto => mjesto.Discriminator.Equals(odabranovoziloKlase.Vozilo + "Mjesto")).ToListAsync();
            
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
                poruka = "Nema parkinga sa mjestom za vašu vrstu vozila";
            }
            ViewBag.poruka = poruka;
            ViewBag.kategorija = odabranovoziloKlase.Vozilo;
            return View(odabraniParkinzi);
        }
        [Authorize(Roles = "Administrator,RegistrovaniKorisnik")]
        public async Task<IActionResult> PrikazRezervacija()
        {
            var user = await _userManager.GetUserAsync(User);
            var trenutniKorisnik = await _context.Users.FirstOrDefaultAsync(usercic => usercic.UserName.Equals(user.UserName));
            List<Rezervacija> rezervacije = await _context.Rezervacija.Where(rezervacija => rezervacija.KorisnikID.Equals(trenutniKorisnik.Id)).ToListAsync();
            List<Rezervacija> izabraneRezervacija = new List<Rezervacija>();
            List<string> naziviParkinga = new List<string>();

            foreach (Rezervacija rezervacija in rezervacije)
            {
                var mjesto = await _context.Mjesto.FindAsync(rezervacija.MjestoID);
                if (rezervacija.VrijemeIsteka < System.DateTime.Now)
                {
                    
                    mjesto.Zauzeto = false;
                    Korisnik k = await _context.RegistrovaniKorisnik.FindAsync(rezervacija.KorisnikID);
                    k = rezervacija.AzurirajKorisnika(k);
                    _context.Update(k);
                    _context.Update(mjesto);
                    _context.Rezervacija.Remove(rezervacija);
                    _context.SaveChangesAsync();
                }
                else
                {
                    var parking = await _context.Parking.FindAsync(mjesto.ParkingId);
                    naziviParkinga.Add(parking.Naziv);
                    izabraneRezervacija.Add(rezervacija);
                }
            }
           
            ViewBag.naziviParkinga = naziviParkinga;
            ViewBag.rezervacije = izabraneRezervacija;
            return View();
        }
        [Authorize(Roles = "Administrator,RegistrovaniKorisnik")]
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
        [Authorize(Roles = "Administrator,RegistrovaniKorisnik")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rezervacija = await _context.Rezervacija.FindAsync(id);
            var mjesto = await _context.Mjesto.FindAsync(rezervacija.MjestoID);
            List<Rezervacija> rezervacije = await _context.Rezervacija.Where(rezervacija => rezervacija.MjestoID.Equals(id)).ToListAsync();
            if (rezervacije.Count == 1)
            {
                mjesto.Zauzeto = false;
            }
            Korisnik k = await _context.RegistrovaniKorisnik.FindAsync(rezervacija.KorisnikID);
            k = rezervacija.AzurirajKorisnika(k);
            _context.Update(k);
            _context.Update(mjesto);
            _context.Rezervacija.Remove(rezervacija);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index","Pocetna");
        }

        private bool IzabranaKategorijaVozilaExists(int id)
        {
            return _context.IzabranaKategorijaVozila.Any(e => e.ID == id);
        }
        [Authorize(Roles = "Administrator,RegistrovaniKorisnik")]
        public async Task<IActionResult> OdaberiMjesto(int id, string kategorija)
        {
            IEnumerable<Mjesto> mjesta = (IEnumerable<Mjesto>)await _context.Mjesto.Where(mjesto => mjesto.ParkingId.Equals(id) && mjesto.Discriminator.Equals(kategorija + "Mjesto")).ToListAsync();
            ViewBag.mjesta = mjesta;
            return View();
        }
        
    }
}
