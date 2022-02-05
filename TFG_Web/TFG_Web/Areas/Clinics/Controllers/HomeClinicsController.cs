using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TFG_Web.Services;
using System.Threading.Tasks;
using TFG_Web.Models;
using TFG_Web.Areas.Clinics.Models;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Caching.Memory;
using TFG_Web.Areas.Clinics.Interface.Services;

namespace TFG_Web.Areas.Clinics.Controllers
{
    [Area(Values.AreaClinics)]
    [ViewLayout(Values.LayoutClinics)]
    public class HomeClinicsController : Controller
    {
        #region Dependencias Interfaces
        private ISession Session => HttpContext.Session;
        private readonly ValidarAcceso _validarAcceso = new ValidarAcceso();
        private readonly IMemoryCache _memoryCache;
        private readonly IGetPacientesServices _getPacientesServices;
        #endregion

        #region Constructor
        public HomeClinicsController(IMemoryCache _memoryCache, IGetPacientesServices getPacientesServices)
        {
            this._memoryCache = _memoryCache;
            _getPacientesServices = getPacientesServices;
        }
        #endregion

        [Route(Values.Inicio)]
        public async Task<IActionResult> Index()
        {
            if (!_validarAcceso.ValidarAccesoWeb(Session)) return RedirectToAction(Values.Index, Values.LoginUsuario, new { area = Values.AreaVacio });

            ViewModel mymodel = new ViewModel();
            mymodel.ListPacientes = await _getPacientesServices.ListPacientesAsync(Session.GetString(Values.Session_usrId));

            mymodel = getCacheSaved(mymodel);

            return View(Values.Index, mymodel);
        }

        /// <summary>
        /// Cogemos los values (cache) de la eliminacion del paciente para guardarlo en el ViewModel
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private ViewModel getCacheSaved(ViewModel model)
        {
            var result_PacienteEliminado = _memoryCache.Get("pacienteEliminado");
            if (result_PacienteEliminado == null)
            {
                return model;
            }

            var result_PacienteEliminadoMensaje = _memoryCache.Get("pacienteEliminadoMensaje");
            var result_PacienteEliminadoPaciente = _memoryCache.Get("pacienteEliminadoPaciente");

            if (result_PacienteEliminado != null && result_PacienteEliminadoMensaje != null && result_PacienteEliminadoPaciente != null)
            {
                model.Notificaciones = bool.Parse(result_PacienteEliminado.ToString());
                model.NotificacionesMensaje = result_PacienteEliminadoMensaje.ToString();
                model.NotificacionesPaciente = result_PacienteEliminadoPaciente.ToString();
                model.NotificacionesControl = true;
            }

            return model;
        }
    }
}