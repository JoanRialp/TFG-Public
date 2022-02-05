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
    [ViewLayout(Values.LayoutClinics)]
    public class CampanaPectusExcavatumController : Controller
    {
        #region Dependencias Interfaces
        private ISession Session => HttpContext.Session;
        private readonly ValidarAcceso _validarAcceso = new ValidarAcceso();
        private readonly ViewModel mymodel = new ViewModel();
        private readonly IGetPacientesServices _getPacientesServices;
        private readonly IPacientesServices _pacienteServices;
        private readonly IControlCampanaPectusExcavatumServices _controlCampanaPectusExcavatumServices;
        #endregion

        #region Cosntructor
        public CampanaPectusExcavatumController(IGetPacientesServices getPacientesServices, IPacientesServices historialClinicoServices,
            IControlCampanaPectusExcavatumServices controlSistemaCompresorServices)
        {
            this._getPacientesServices = getPacientesServices;
            this._pacienteServices = historialClinicoServices;
            this._controlCampanaPectusExcavatumServices = controlSistemaCompresorServices;
        }
        #endregion

        /// <summary>
        /// CampanaPectusExcavatum table Informacion
        /// </summary>
        /// <param name="idSistemaCompresor"></param>
        /// <param name="idPaciente"></param>
        /// <returns></returns>
        [Route(Values.CampanaPectusExcavatum)]
        public async Task<IActionResult> Index(string idCampanaPectusExcavatum, string idPaciente)
        {
            if (!_validarAcceso.ValidarAccesoWeb(Session)) return RedirectToAction(Values.Index, Values.LoginUsuario, new { area = Values.AreaVacio });

            if (idCampanaPectusExcavatum != null)
            {
                try
                {
                    mymodel.Pacientes = await _pacienteServices.GetById(idPaciente);
                    mymodel.ControlCampanaPectusExcavatum = await _controlCampanaPectusExcavatumServices.GetById(idCampanaPectusExcavatum);
                }
                catch
                {
                    return RedirectToAction(Values.Index, Values.Inicio, new { area = Values.AreaVacio });
                }
            }
            else
            {
                return RedirectToAction(Values.Index, Values.Inicio, new { area = Values.AreaVacio });
            }

            mymodel.ListPacientes = await _getPacientesServices.ListPacientesAsync(Session.GetString(Values.Session_usrId));

            return View(Values.Index, mymodel);
        }

        /// <summary>
        /// Pectus Excavatum del paciente - Editar (Update)
        /// </summary>
        /// <param name="pacienteId"></param>
        /// <param name="pectusExcavatumId"></param>
        /// <param name="formCampanaPectusExcavatumComplicaciones"></param>
        /// <param name="formCampanaPectusExcavatumFecha"></param>
        /// <param name="formCampanaPectusExcavatumFotos"></param>
        /// <param name="formCampanaPectusExcavatumHorasDia"></param>
        /// <param name="formCampanaPectusExcavatumHorasNoche"></param>
        /// <param name="formCampanaPectusExcavatumIndicada"></param>
        /// <param name="formCampanaPectusExcavatumNumeroPumpsIndicados"></param>
        /// <param name="formCampanaPectusExcavatumNumeroPumpsUso"></param>
        /// <param name="formCampanaPectusExcavatumObservaciones"></param>
        /// <param name="formCampanaPectusExcavatumPresionTratamiento"></param>
        /// <param name="formCampanaPectusExcavatumUso"></param>
        /// <returns></returns>
        public async Task<IActionResult> CambiarPectusExcavatum(string pacienteId, string pectusExcavatumId, string formCampanaPectusExcavatumComplicaciones,
            DateTime formCampanaPectusExcavatumFecha, string formCampanaPectusExcavatumFotos, int formCampanaPectusExcavatumHorasDia,
            int formCampanaPectusExcavatumHorasNoche, string formCampanaPectusExcavatumIndicada, string formCampanaPectusExcavatumNumeroPumpsIndicados,
            string formCampanaPectusExcavatumNumeroPumpsUso, string formCampanaPectusExcavatumObservaciones, string formCampanaPectusExcavatumPresionTratamiento,
            int formCampanaPectusExcavatumUso)
        {
            if (!_validarAcceso.ValidarAccesoWeb(Session)) return RedirectToAction(Values.Index, Values.LoginUsuario, new { area = Values.AreaVacio });

            if (pectusExcavatumId != null)
            {
                try
                {
                    mymodel.Pacientes = await _pacienteServices.GetById(pacienteId);

                    ControlCampanaPectusExcavatum controlCampanaPectusExcavatum = new ControlCampanaPectusExcavatum();
                    controlCampanaPectusExcavatum.Id = pectusExcavatumId;
                    controlCampanaPectusExcavatum.CCPE_Complicaciones = formCampanaPectusExcavatumComplicaciones;
                    controlCampanaPectusExcavatum.CCPE_Fecha = formCampanaPectusExcavatumFecha.AddHours(23);
                    controlCampanaPectusExcavatum.CCPE_Fotos = formCampanaPectusExcavatumFotos;
                    controlCampanaPectusExcavatum.CCPE_HorasUsoDia = formCampanaPectusExcavatumHorasDia;
                    controlCampanaPectusExcavatum.CCPE_HorasUsoNoche = formCampanaPectusExcavatumHorasNoche;
                    controlCampanaPectusExcavatum.CCPE_Indicada = formCampanaPectusExcavatumIndicada;
                    controlCampanaPectusExcavatum.CCPE_NumeroPumpsIndicados = formCampanaPectusExcavatumNumeroPumpsIndicados;
                    controlCampanaPectusExcavatum.CCPE_NumeroPumpsUso = formCampanaPectusExcavatumNumeroPumpsUso;
                    controlCampanaPectusExcavatum.CCPE_Observaciones = formCampanaPectusExcavatumObservaciones;
                    controlCampanaPectusExcavatum.CCPE_PresionTratamiento = formCampanaPectusExcavatumPresionTratamiento;
                    controlCampanaPectusExcavatum.CCPE_Uso = formCampanaPectusExcavatumUso;

                    try
                    {
                        await _controlCampanaPectusExcavatumServices.Update(pectusExcavatumId, controlCampanaPectusExcavatum);
                    }
                    catch
                    {
                        return RedirectToAction(Values.Index, Values.Inicio, new { area = Values.AreaVacio });
                    }
                }
                catch
                {
                    return RedirectToAction(Values.Index, Values.Inicio, new { area = Values.AreaVacio });
                }
            }
            else
            {
                return RedirectToAction(Values.Index, Values.Inicio, new { area = Values.AreaVacio });
            }

            return RedirectToAction(Values.Index, Values.CampanaPectusExcavatum, new { area = Values.AreaVacio, idCampanaPectusExcavatum = pectusExcavatumId, idPaciente = pacienteId });
        }

        /// <summary>
        /// Eliminar CampanaPectusExcavatum
        /// ControlCampanaPectusExcavatum
        /// Paciente.ControlCampanaPectusExcavatum
        /// </summary>
        /// <param name="pacienteId"></param>
        /// <param name="campanaPectusId"></param>
        /// <returns></returns>
        public async Task<IActionResult> eliminarCampanaPectusExcavatum(string pacienteId, string campanaPectusId)
        {
            try
            {
                await _controlCampanaPectusExcavatumServices.Delete(campanaPectusId);
            }
            catch
            {
                return RedirectToAction(Values.Index, Values.PerfilPaciente, new { area = Values.AreaVacio, id = pacienteId });
            }

            try
            {
                Pacientes paciente = await _pacienteServices.GetById(pacienteId);

                for (int i = 0; i < paciente.ControlCampanaPectusExcavatum.Count; i++)
                {
                    if (paciente.ControlCampanaPectusExcavatum[i] == campanaPectusId)
                    {
                        paciente.ControlCampanaPectusExcavatum.RemoveAt(i);
                    }
                }

                await _pacienteServices.Update(pacienteId, paciente);
            }
            catch
            {
                return RedirectToAction(Values.Index, Values.PerfilPaciente, new { area = Values.AreaVacio, id = pacienteId });
            }

            return RedirectToAction(Values.Index, Values.PerfilPaciente, new { area = Values.AreaVacio, id = pacienteId });
        }
    }
}