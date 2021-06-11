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
    public class AdministracijaUserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdministracijaUserController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Administrator,RegistrovaniKorisnik")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }
        [Authorize(Roles = "Administrator,RegistrovaniKorisnik")]
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var korisnik = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (korisnik == null)
            {
                return NotFound();
            }

            return View(korisnik);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,RegistrovaniKorisnik")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var korisnik = await _context.Users.FindAsync(id);
            var rezervacije = await _context.Rezervacija.Where(rez => rez.KorisnikID.Equals(id)).ToListAsync();
            foreach (Rezervacija item in rezervacije)
            {
                var zaizbirsatRezervacija = await _context.Rezervacija.FirstOrDefaultAsync(rez => rez.ID == item.ID);
                if (zaizbirsatRezervacija != null)
                {
                    var mjesto = await _context.Mjesto.FindAsync(zaizbirsatRezervacija.MjestoID);
                    mjesto.Zauzeto = false;
                    _context.Update(mjesto);
                    _context.Rezervacija.Remove(zaizbirsatRezervacija);
                    _context.SaveChangesAsync();
                }
            }
            _context.Users.Remove(korisnik);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        
    }
}
