using EParking.Data;
using EParking.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EParking.Controllers
{
    public class ParkingManagementController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ParkingManagementController(ApplicationDbContext context)
        {
            _context = context;
        }
        //[Authorize(Roles="Administrator")]



        [Authorize(Roles = "Administrator,RegistrovaniKorisnik")]
        public async Task<IActionResult> OdaberiParking()
        {
            IEnumerable<Parking> parkinzi = await _context.Parking.ToListAsync();
            return View(parkinzi);
        }
        [Authorize(Roles = "Administrator,RegistrovaniKorisnik")]
        public async Task<IActionResult> UnesiSifru(int id)
        {
            Parking parking = await _context.Parking.FindAsync(id);
            return View(parking);
        }
        [HttpPost]
        [Authorize(Roles = "Administrator,RegistrovaniKorisnik")]
        public async Task<IActionResult> UnesiSifru(int id, [Bind("ID,Naziv,PocetakRadnogVremena,KrajRadnogVremena,PocetakJeftinogVremena,KrajJeftinogVremena,Sifra,OdobrenSGMjesecno,OdobrenSGUzastopno,OdobrenOSInvaliditetom,Cijena")] Parking parking)
        {
            if (id != parking.ID)
            {
                return NotFound();
            }
            System.Diagnostics.Debug.WriteLine(id);
            int ovajid = id;
            return RedirectToAction("Index", new { id = ovajid });

        }





        [Authorize(Roles = "Administrator,RegistrovaniKorisnik")]
        public async Task<IActionResult> Index(int id)
        {
            Parking parking = await _context.Parking.FindAsync(id);
            List<Mjesto> mjesta = await _context.Mjesto.Where(mjesto => mjesto.ParkingId.Equals(id)).ToListAsync();
            List<Rezervacija> rezervacije = await _context.Rezervacija.ToListAsync();
            List<Rezervacija> krajnjeRezervacije = new List<Rezervacija>();
            foreach (Rezervacija rezervacija in rezervacije)
            {
                if (rezervacija.VrijemeIsteka < System.DateTime.Now)
                {
                    var mjesto = await _context.Mjesto.FindAsync(rezervacija.MjestoID);
                    List<Rezervacija> rezervacijeMjesta = await _context.Rezervacija.Where(rezervacija => rezervacija.MjestoID.Equals(id)).ToListAsync();
                    if (rezervacijeMjesta.Count == 1)
                    {
                        mjesto.Zauzeto = false;
                    }
                    _context.Update(mjesto);
                    Korisnik k = await _context.RegistrovaniKorisnik.FindAsync(rezervacija.KorisnikID);
                    k = rezervacija.AzurirajKorisnika(k);
                    _context.Update(k);
                    _context.Rezervacija.Remove(rezervacija);
                    _context.SaveChangesAsync();
                    
                }
                else
                {
                    krajnjeRezervacije.Add(rezervacija);
                }
            }

            List<Rezervacija> izabraneRezervacije = new List<Rezervacija>();
            try
            {
                izabraneRezervacije = krajnjeRezervacije.Where(rezervacija => mjesta.First(mjesto => mjesto.ID == rezervacija.MjestoID) != null).ToList();
            }
            catch (InvalidOperationException e){

            }
            ViewBag.poruka = "";
            ViewBag.mjesta = mjesta;
            ViewBag.rezervacije = izabraneRezervacije;
            return View(parking);
        }
        [HttpPost]
        [Authorize(Roles = "Administrator,RegistrovaniKorisnik")]
        public async Task<IActionResult> Index(int id, [Bind("ID,Naziv,PocetakRadnogVremena,KrajRadnogVremena,PocetakJeftinogVremena,KrajJeftinogVremena,Sifra,OdobrenSGMjesecno,OdobrenSGUzastopno,OdobrenOSInvaliditetom,Cijena")] Parking parking)
        {
            if (id != parking.ID)
            {
                return NotFound();
            }
            List<Mjesto> mjesta = await _context.Mjesto.Where(mjesto => mjesto.ParkingId.Equals(id)).ToListAsync();
            List<Rezervacija> rezervacije = new List<Rezervacija>();
            try
            {
                rezervacije = await _context.Rezervacija.Where(rezervacija => mjesta.First(mjesto => mjesto.ID == rezervacija.MjestoID) != null).ToListAsync();
            }
            catch
            {

            }
            ViewBag.poruka = "";
            ViewBag.mjesta = mjesta;
            ViewBag.rezervacije = rezervacije;


            if (ModelState.IsValid)
            {
                
                TimeSpan testni = new TimeSpan(0, 0, 0);
                if (!parking.PocetakJeftinogVremena.Equals(testni) && !parking.KrajJeftinogVremena.Equals(testni) && (parking.PocetakJeftinogVremena> parking.KrajRadnogVremena || parking.KrajJeftinogVremena< parking.PocetakRadnogVremena))
                {
                    ViewBag.poruka = "Jeftino vrijeme mora biti unutar radnog vremena";
                    return View(parking);

                }

                _context.Update(parking);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index","Pocetna");
            }
            
            return View(parking);
        }
        [Authorize(Roles = "Administrator,RegistrovaniKorisnik")]
        public async Task<IActionResult> PromijeniSifru(int id)
        {
            Parking parking = await _context.Parking.FindAsync(id);
            ViewBag.sifra = parking.Sifra;
            return View(parking);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator,RegistrovaniKorisnik")]
        public async Task<IActionResult> PromijeniSifru(int id, [Bind("ID,Naziv,PocetakRadnogVremena,KrajRadnogVremena,PocetakJeftinogVremena,KrajJeftinogVremena,Sifra,OdobrenSGMjesecno,OdobrenSGUzastopno,OdobrenOSInvaliditetom,Cijena")] Parking parking)
        {
            
            if (ModelState.IsValid)
            {
                _context.Update(parking);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", new { id = id });
            }
            ViewBag.sifra = parking.Sifra;
            return View(parking);
        }

        public async Task<IActionResult> DodajMjesto(int id)
        {
            List<SelectListItem> kategorije = new List<SelectListItem>
            {
                new SelectListItem {Text="Automobil" , Value="AutomobilMjesto"},
                new SelectListItem {Text="Autobus" , Value="AutobusMjesto"},
                new SelectListItem {Text="Motocikl" , Value="MotociklMjesto"},
                new SelectListItem {Text="Kombi" , Value="KombiMjesto"},
                new SelectListItem {Text="Kamp kucica" , Value="KampKucicaMjesto"},
                new SelectListItem {Text="Kamion" , Value="KamionMjesto"},
                new SelectListItem {Text="Biciklo" , Value="BicikloMjesto"}
            };
            ViewBag.poruka = "";
            ViewBag.kategorije = kategorije;
            ViewBag.parking = id;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,RegistrovaniKorisnik")]
        public async Task<IActionResult> DodajMjesto(int id, [Bind("ID,Sprat,Red,Kolona,Zauzeto,Discriminator,ParkingId")] AutomobilMjesto mjesto)
        {


            mjesto.ParkingId = id;
            if (ModelState.IsValid)
            {
                Mjesto mjestasce = mjesto;
                List<Mjesto> svaMjesta = await _context.Mjesto.ToListAsync();
                int maxiID = svaMjesta[svaMjesta.Count - 1].ID;
                mjesto.ID = maxiID + 1;

                if (svaMjesta.Find(mjesto => mjesto.Sprat == mjestasce.Sprat && mjesto.Red == mjestasce.Red && mjesto.Kolona == mjestasce.Kolona) == null)
                {
                    _context.Add(mjestasce);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", new { id = id });
                }
            }
            List<SelectListItem> kategorije = new List<SelectListItem>
            {
                new SelectListItem {Text="Automobil" , Value="AutomobilMjesto"},
                new SelectListItem {Text="Autobus" , Value="AutobusMjesto"},
                new SelectListItem {Text="Motocikl" , Value="MotociklMjesto"},
                new SelectListItem {Text="Kombi" , Value="KombiMjesto"},
                new SelectListItem {Text="Kamp kucica" , Value="KampKucicaMjesto"},
                new SelectListItem {Text="Kamion" , Value="KamionMjesto"},
                new SelectListItem {Text="Biciklo" , Value="BicikloMjesto"}
            };
            ViewBag.poruka = "To mjesto već postoji";
            ViewBag.kategorije = kategorije;
            ViewBag.parking = id;
            return View(mjesto);
        }
        [Authorize(Roles = "Administrator,RegistrovaniKorisnik")]
        public async Task<IActionResult> IzbrisiMjesto(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var mjesto = await _context.Mjesto
                .FirstAsync(m => m.ID == id);
            if (mjesto == null)
            {
                return NotFound();
            }

            return View(mjesto);
        }

        // POST: Rezervacijas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,RegistrovaniKorisnik")]
        public async Task<IActionResult> IzbrisiMjesto(int id)
        {
            var mjesto = await _context.Mjesto.FindAsync(id);
            _context.Mjesto.Remove(mjesto);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new { id = id });
        }


    }
}
