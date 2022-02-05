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
using TFG_Web.Areas.Clinics.Interface.Services.IHistorialClinicoServices;

namespace TFG_Web.Areas.Clinics.Controllers
{
    [Area(Values.AreaClinics)]
    [ViewLayout(Values.LayoutClinics)]
    public class PacienteFinalizadoController : Controller
    {
        #region Dependencias Interfaces
        private ISession Session => HttpContext.Session;
        private readonly ValidarAcceso _validarAcceso = new ValidarAcceso();
        private readonly ViewModel mymodel = new ViewModel();
        private readonly IGetPacientesServices _getPacientesServices;
        private readonly IPacientesServices _pacienteServices;
        private readonly IHistorialClinicoServices _historialClinicoServices;
        #endregion

        #region Cosntructor
        public PacienteFinalizadoController(IGetPacientesServices getPacientesServices, IPacientesServices pacienteServices,
            IHistorialClinicoServices historialClinicoServices)
        {
            this._getPacientesServices = getPacientesServices;
            this._pacienteServices = pacienteServices;
            this._historialClinicoServices = historialClinicoServices;

        }
        #endregion

        [Route(Values.FinalizarTratamiento)]
        public async Task<IActionResult> Index()
        {
            if (!_validarAcceso.ValidarAccesoWeb(Session)) return RedirectToAction(Values.Index, Values.LoginUsuario, new { area = Values.AreaVacio });

            mymodel.ListPacientes = await _pacienteServices.GetByUsuariosId(Session.GetString(Values.Session_usrId), true);

            return View(Values.Index, mymodel);
        }

        /// <summary>
        /// Finalizar el tratamiento del Paciente
        /// </summary>
        /// <param name="id">Id del paciente a finalizar</param>
        /// <param name="finalizar"></param>
        /// <param name="idHistorialClinico"></param>
        /// <returns></returns>
        public async Task<IActionResult> FinalizarTratamientoPaciente(string id, bool finalizar, string idHistorialClinico, string idHistorialClinico2)
        {
            if (!_validarAcceso.ValidarAccesoWeb(Session)) return RedirectToAction(Values.Index, Values.LoginUsuario, new { area = Values.AreaVacio });

            if (id != null)
            {
                try
                {
                    Pacientes paciente = await _pacienteServices.GetById(id);
                    HistorialClinico historialClinico = await _historialClinicoServices.GetById(idHistorialClinico);
                    HistorialClinico historialClinico2 = new HistorialClinico();

                    if (idHistorialClinico2 != null && idHistorialClinico2 != "null")
                    {
                        historialClinico2 = await _historialClinicoServices.GetById(idHistorialClinico2);
                    }

                    try
                    {
                        paciente.P_Finalizado = finalizar;
                        await _pacienteServices.Update(id, paciente);
                    }
                    catch
                    {
                        return RedirectToAction(Values.Index, Values.Inicio, new { area = Values.AreaVacio });
                    }

                    if (!finalizar)
                    {
                        try
                        {
                            historialClinico.HC_FechaUltimaConsulta = null;
                            await _historialClinicoServices.Update(idHistorialClinico, historialClinico);

                            if (idHistorialClinico2 != "null" && idHistorialClinico2 != null)
                            {
                                historialClinico2.HC_FechaUltimaConsulta = null;
                                await _historialClinicoServices.Update(idHistorialClinico2, historialClinico2);
                            }
                        }
                        catch
                        {
                            return RedirectToAction(Values.Index, Values.Inicio, new { area = Values.AreaVacio });
                        }
                    }
                    if (finalizar)
                    {
                        try
                        {
                            historialClinico.HC_FechaUltimaConsulta = DateTime.Now;
                            await _historialClinicoServices.Update(idHistorialClinico, historialClinico);

                            if (idHistorialClinico2 != "null")
                            {
                                historialClinico2.HC_FechaUltimaConsulta = DateTime.Now;
                                await _historialClinicoServices.Update(idHistorialClinico2, historialClinico2);
                            }
                        }
                        catch
                        {
                            return RedirectToAction(Values.Index, Values.Inicio, new { area = Values.AreaVacio });
                        }
                    }
                }
                catch
                {
                    return RedirectToAction(Values.Index, Values.Inicio, new { area = Values.AreaVacio });
                }
            }

            mymodel.ListPacientes = await _pacienteServices.GetByFinalizado(true);

            return RedirectToAction(Values.Index, Values.FinalizarTratamiento, new { area = Values.AreaVacio });
        }
    }
}