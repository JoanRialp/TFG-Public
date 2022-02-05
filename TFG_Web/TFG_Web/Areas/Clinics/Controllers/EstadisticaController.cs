using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TFG_Web.Models;
using TFG_Web.Services;
using TFG_Web.Areas.Clinics.Models;
using TFG_Web.Areas.Clinics.Interface.Services;

namespace TFG_Web.Areas.Clinics.Controllers
{
    [Area(Values.AreaClinics)]
    [ViewLayout(Values.LayoutEstadistica)]
    public class EstadisticaController : Controller
    {
        #region Dependencias Interfaces
        private ISession Session => HttpContext.Session;
        private readonly ValidarAcceso _validarAcceso = new ValidarAcceso();
        private readonly ViewModel mymodel = new ViewModel();
        private readonly IGetPacientesServices _getPacientesServices;
        private readonly IPacientesServices _pacienteServices;
        #endregion

        #region Controlador
        public EstadisticaController(IGetPacientesServices getPacientesServices, IPacientesServices historialClinicoServices)
        {
            this._getPacientesServices = getPacientesServices;
            this._pacienteServices = historialClinicoServices;
        }
        #endregion

        public async Task<IActionResult> Index()
        {
            if (!_validarAcceso.ValidarAccesoWeb(Session)) return RedirectToAction(Values.Index, Values.LoginUsuario, new { area = Values.AreaVacio });

            mymodel.ListPacientes = await _getPacientesServices.ListPacientesAsync(Session.GetString(Values.Session_usrId));

            return View(Values.Index, mymodel);
        }

        /// <summary>
        /// Mostrar la estadistica de paciente
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> estadisticaUsuario(string id)
        {
            mymodel.ListPacientes = await _getPacientesServices.ListPacientesAsync(Session.GetString(Values.Session_usrId));
            mymodel.Pacientes = await _pacienteServices.GetById(id);

            return View(Values.EstadisticaPacientes, mymodel);
        }
    }
}