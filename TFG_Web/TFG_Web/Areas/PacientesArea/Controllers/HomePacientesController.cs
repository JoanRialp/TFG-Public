using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TFG_Web.Areas.Clinics.Services;
using TFG_Web.Models;
using TFG_Web.Services;

namespace TFG_Web.Areas.PacientesArea.Controllers
{
    [Area("PacientesArea")]
    [ViewLayout("_LayoutPacientes")]
    public class HomePacientesController : Controller
    {
        public IActionResult IndexAsync()
        {
            return View();
        }
    }
}
