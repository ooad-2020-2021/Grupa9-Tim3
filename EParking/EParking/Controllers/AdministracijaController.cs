using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EParking.Controllers
{
    public class AdministracijaController : Controller
    {
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
