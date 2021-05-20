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

namespace EParking.Controllers
{
    public enum Popust
    {
        StalniGostUzastopno = 10,
        StalniGostMjesecno = 15,
        OsobaSInvaliditetom = 40,
        PopustIgrica = 15
    }

    public class RezervacijasController : Controller
    {
        private readonly ApplicationDbContext _context;
        /*
        static List<Mjesto> mjesta = new List<Mjesto>()
        {
            new Mjesto(1,1,1,false,KategorijaVozila.Automobil),
            new Mjesto(3,2,1,true,KategorijaVozila.Kamion),
            new Mjesto(1,4,3,false,KategorijaVozila.Kombi),
            new Mjesto(2,1,3,true,KategorijaVozila.Kamion)
        };

        static RegistrovaniKorisnik korisnik = new RegistrovaniKorisnik("Muharem Kapo", "muharemkapo@yahoo.com", "Musija",100, 20);
        */
        public RezervacijasController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        

        public async Task<IActionResult> Index()
        {
            IEnumerable<Mjesto> mjesta = _context.Mjesto.Where(mjesto => mjesto.Zauzeto.Equals(false)).ToList();

            return View(new Tuple<Rezervacija, IEnumerable<Mjesto>> (null,mjesta));
        }
       

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Index([Bind("ID,KorisnikID,MjestoID,VrijemeIsteka,VrijemePocetka")] Rezervacija rezervacija)
        {
            IEnumerable<Mjesto> mjesta = _context.Mjesto.Where(mjesto => mjesto.Zauzeto.Equals(false)).ToList();
            if (ModelState.IsValid)
            {
        
                _context.Add(rezervacija);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(new Tuple<Rezervacija, IEnumerable<Mjesto>> (rezervacija,mjesta));
        }

        /*[HttpPost]
        public HttpResponseMessage provjeriKorisnika(string popust)
        {/*
            if (popust.Equals("StalniGostMjesecno"))
            {

            }
            if (popust.Equals("StalniGostUzastopno"))
            {

            }
            if (popust.Equals("OsobaSInvaliditetom") && korisnik.Invaliditet)
            {
                
            }
            var response = new HttpResponseMessage(HttpStatusCode.Created);
            if (!korisnik.addPopust(popust))
            {
                response.Content = new StringContent("Korisnik nema pravo na taj popust");
            }
            else {
                response.Content = new StringContent("Korisnik ima pravo na taj popust");
            }

            response.Headers.Location =      
                new Uri(Url.Link("DefaultApi", new { action = "status" }));
            return response;

        }
         */
        // GET: Rezervacijas
        /*public async Task<IActionResult> Index()
        {
            return View(await _context.Rezervacija.ToListAsync());
        }*/
        /*       public Task<IActionResult> izaberiKategoriju()
               {

                   return View();
               } 
               public Task<IActionResult> potvrdiRezervaciju()
               {

               }
               public async Task<IActionResult> zavrsiRezervaciju()
               {

               }
               public void obavijestiKorisnika()
               {

               } 
               public Task<IActionResult> odaberiPopust()
               {

               }
               public void provjeriKorisnikovoStanje()
               {

               }
               //public async Task<IActionResult> pokreniIgricu() { }
       */









        

        // GET: Rezervacijas/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: Rezervacijas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rezervacijas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,VrijemeIsteka")] Rezervacija rezervacija)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rezervacija);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rezervacija);
        }

        // GET: Rezervacijas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rezervacija = await _context.Rezervacija.FindAsync(id);
            if (rezervacija == null)
            {
                return NotFound();
            }
            return View(rezervacija);
        }
/*
        // POST: Rezervacijas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,VrijemeIsteka")] Rezervacija rezervacija)
        {
            if (id != rezervacija.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rezervacija);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RezervacijaExists(rezervacija.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(rezervacija);
        }

        // GET: Rezervacijas/Delete/5
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

        // POST: Rezervacijas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rezervacija = await _context.Rezervacija.FindAsync(id);
            _context.Rezervacija.Remove(rezervacija);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RezervacijaExists(int id)
        {
            return _context.Rezervacija.Any(e => e.ID == id);
        }*/
    }
}
