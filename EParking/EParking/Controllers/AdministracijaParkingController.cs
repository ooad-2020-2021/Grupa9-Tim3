using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EParking.Data;
using EParking.Models;
using Microsoft.AspNetCore.Authorization;

namespace EParking.Controllers
{
    public class AdministracijaParkingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdministracijaParkingController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Administrator,RegistrovaniKorisnik")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Parking.ToListAsync());
        }

        [Authorize(Roles = "Administrator,RegistrovaniKorisnik")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parking = await _context.Parking
                .FirstOrDefaultAsync(m => m.ID == id);
            if (parking == null)
            {
                return NotFound();
            }

            return View(parking);
        }

        // GET: Administracija/Create
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,RegistrovaniKorisnik")]
        public async Task<IActionResult> Create([Bind("ID,Naziv,PocetakRadnogVremena,KrajRadnogVremena,PocetakJeftinogVremena,KrajJeftinogVremena,Sifra,OdobrenSGMjesecno,OdobrenSGUzastopno,OdobrenOSInvaliditetom,Cijena")] Parking parking)
        {
            if (ModelState.IsValid)
            {
                List<Parking> parkinzi = await _context.Parking.ToListAsync();
                if (parkinzi.Count > 0)
                {
                    parking.ID = parkinzi[parkinzi.Count - 1].ID + 1;
                }
                _context.Add(parking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(parking);
        }

        [Authorize(Roles = "Administrator,RegistrovaniKorisnik")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parking = await _context.Parking.FindAsync(id);
            if (parking == null)
            {
                return NotFound();
            }
            return View(parking);
        }

        // POST: Administracija/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,RegistrovaniKorisnik")]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Naziv,PocetakRadnogVremena,KrajRadnogVremena,PocetakJeftinogVremena,KrajJeftinogVremena,Sifra,OdobrenSGMjesecno,OdobrenSGUzastopno,OdobrenOSInvaliditetom,Cijena")] Parking parking)
        {
            if (id != parking.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParkingExists(parking.ID))
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
            return View(parking);
        }

        [Authorize(Roles = "Administrator,RegistrovaniKorisnik")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parking = await _context.Parking
                .FirstOrDefaultAsync(m => m.ID == id);
            if (parking == null)
            {
                return NotFound();
            }

            return View(parking);
        }

        // POST: Administracija/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,RegistrovaniKorisnik")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var parking = await _context.Parking.FindAsync(id);
            _context.Parking.Remove(parking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParkingExists(int id)
        {
            return _context.Parking.Any(e => e.ID == id);
        }
    }
}
