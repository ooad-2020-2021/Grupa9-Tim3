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

        
        

        public async Task<IActionResult> OdaberiParking()
        {
            IEnumerable<Parking> parkinzi = await _context.Parking.ToListAsync();
            return View(parkinzi);
        }
        
        public async Task<IActionResult> UnesiSifru(int id)
        {
            Parking parking = await _context.Parking.FindAsync(id);
            return View(parking);
        }
        [HttpPost]
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

        



        // GET: HomeController1
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Index(int id)
        {
            Parking parking = await _context.Parking.FindAsync(id);
            List<Mjesto> mjesta = await _context.Mjesto.Where(mjesto => mjesto.ParkingId.Equals(id)).ToListAsync();
            List<Rezervacija> rezervacije = await _context.Rezervacija.ToListAsync();
            List<Rezervacija> izabraneRezervacije = rezervacije.Where(rezervacija => mjesta.First(mjesto => mjesto.ID == rezervacija.MjestoID) != null).ToList();
            ViewBag.mjesta = mjesta;
            ViewBag.rezervacije = izabraneRezervacije;
            return View(parking);
        }
        [HttpPost]
        public async Task<IActionResult> Index(int id, [Bind("ID,Naziv,PocetakRadnogVremena,KrajRadnogVremena,PocetakJeftinogVremena,KrajJeftinogVremena,Sifra,OdobrenSGMjesecno,OdobrenSGUzastopno,OdobrenOSInvaliditetom,Cijena")] Parking parking)
        {
            if (id != parking.ID)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                _context.Update(parking);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index","Pocetna");
            }
            List<Mjesto> mjesta = await _context.Mjesto.Where(mjesto => mjesto.ParkingId.Equals(id)).ToListAsync();
            List<Rezervacija> rezervacije = await _context.Rezervacija.Where(rezervacija => mjesta.First(mjesto => mjesto.ID == rezervacija.MjestoID)!=null).ToListAsync();
            ViewBag.mjesta = mjesta;
            ViewBag.rezervacije = rezervacije;
            return View(parking);
        }

        public async Task<IActionResult> PromijeniSifru(int id)
        {
            Parking parking = await _context.Parking.FindAsync(id);
            ViewBag.sifra = parking.Sifra;
            return View(parking);
        }

        [HttpPost]
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
        //[Authorize(Roles = "Administrator")]
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
        public async Task<IActionResult> IzbrisiMjesto(int id)
        {
            var mjesto = await _context.Mjesto.FindAsync(id);
            _context.Mjesto.Remove(mjesto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
