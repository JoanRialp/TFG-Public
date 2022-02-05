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
    public class SistemaCompresorController : Controller
    {
        #region Dependencias Interfaces
        private ISession Session => HttpContext.Session;
        private readonly ValidarAcceso _validarAcceso = new ValidarAcceso();
        private readonly ViewModel mymodel = new ViewModel();
        private readonly IGetPacientesServices _getPacientesServices;
        private readonly IPacientesServices _pacienteServices;
        private readonly IControlSistemaCompresorServices _controlSistemaCompresorServices;
        #endregion

        #region Cosntructor
        public SistemaCompresorController(IGetPacientesServices getPacientesServices, IPacientesServices historialClinicoServices,
            IControlSistemaCompresorServices controlSistemaCompresorServices)
        {
            this._getPacientesServices = getPacientesServices;
            this._pacienteServices = historialClinicoServices;
            this._controlSistemaCompresorServices = controlSistemaCompresorServices;
        }
        #endregion

        /// <summary>
        /// SistemaCompresor table Informacion
        /// </summary>
        /// <param name="idSistemaCompresor"></param>
        /// <param name="idPaciente"></param>
        /// <returns></returns>
        [Route(Values.SistemaCompresor)]
        public async Task<IActionResult> Index(string idSistemaCompresor, string idPaciente)
        {
            if (!_validarAcceso.ValidarAccesoWeb(Session)) return RedirectToAction(Values.Index, Values.LoginUsuario, new { area = Values.AreaVacio });

            if (idSistemaCompresor != null)
            {
                try
                {
                    mymodel.ControlSistemaCompresor = await _controlSistemaCompresorServices.GetById(idSistemaCompresor);
                    mymodel.Pacientes = await _pacienteServices.GetById(idPaciente);
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
        /// Sistema Compresor del paciente - Editar (Update)
        /// </summary>
        /// <param name="pacienteId"></param>
        /// <param name="sistemaCompresorId"></param>
        /// <param name="formSistemaCompresorAjustes"></param>
        /// <param name="formSistemaCompresorCompilaciones"></param>
        /// <param name="formSistemaCompresorDoblador"></param>
        /// <param name="formSistemaCompresorF1"></param>
        /// <param name="formSistemaCompresorF2"></param>
        /// <param name="formSistemaCompresorF3"></param>
        /// <param name="formSistemaCompresorF4"></param>
        /// <param name="formSistemaCompresorF5"></param>
        /// <param name="formSistemaCompresorFecha"></param>
        /// <param name="formSistemaCompresorFotos"></param>
        /// <param name="formSistemaCompresorGrupo"></param>
        /// <param name="formSistemaCompresorHorasDia"></param>
        /// <param name="formSistemaCompresorHorasNoche"></param>
        /// <param name="formSistemaCompresorObservaciones"></param>
        /// <param name="formSistemaCompresorPAD"></param>
        /// <param name="formSistemaCompresorPC"></param>
        /// <param name="formSistemaCompresorPTPostAjustes"></param>
        /// <param name="formSistemaCompresorPTPreAjustes"></param>
        /// <param name="formSistemaCompresorRecambioPieza"></param>
        /// <returns></returns>
        public async Task<IActionResult> CambiarSistemaCompresor(string pacienteId, string sistemaCompresorId, string formSistemaCompresorAjustes,
            string formSistemaCompresorCompilaciones, string formSistemaCompresorDoblador, string formSistemaCompresorF1, string formSistemaCompresorF2,
            string formSistemaCompresorF3, string formSistemaCompresorF4, string formSistemaCompresorF5, DateTime formSistemaCompresorFecha,
            int formSistemaCompresorGrupo, int formSistemaCompresorHorasDia, int formSistemaCompresorHorasNoche,
            string formSistemaCompresorObservaciones, string formSistemaCompresorPAD, string formSistemaCompresorPC, string formSistemaCompresorPTPostAjustes,
            string formSistemaCompresorPTPreAjustes, string formSistemaCompresorRecambioPieza)
        {
            if (!_validarAcceso.ValidarAccesoWeb(Session)) return RedirectToAction(Values.Index, Values.LoginUsuario, new { area = Values.AreaVacio });

            if (sistemaCompresorId != null)
            {
                try
                {
                    mymodel.Pacientes = await _pacienteServices.GetById(pacienteId);

                    ControlSistemaCompresor controlSistemaCompresor = new ControlSistemaCompresor();
                    controlSistemaCompresor.Id = sistemaCompresorId;
                    controlSistemaCompresor.CSC_AjustesSistemaCompresorDinamico = formSistemaCompresorAjustes;
                    controlSistemaCompresor.CSC_Compilaciones = formSistemaCompresorCompilaciones;
                    controlSistemaCompresor.CSC_Doblador = formSistemaCompresorDoblador;
                    controlSistemaCompresor.CSC_F1 = formSistemaCompresorF1;
                    controlSistemaCompresor.CSC_F2 = formSistemaCompresorF2;
                    controlSistemaCompresor.CSC_F3 = formSistemaCompresorF3;
                    controlSistemaCompresor.CSC_F4 = formSistemaCompresorF4;
                    controlSistemaCompresor.CSC_F5 = formSistemaCompresorF5;
                    controlSistemaCompresor.CSC_Fecha = formSistemaCompresorFecha.AddHours(23);
                    controlSistemaCompresor.CSC_Grupo = formSistemaCompresorGrupo;
                    controlSistemaCompresor.CSC_HorasUsoDia = formSistemaCompresorHorasDia;
                    controlSistemaCompresor.CSC_HorasUsoNoche = formSistemaCompresorHorasNoche;
                    controlSistemaCompresor.CSC_Observaciones = formSistemaCompresorObservaciones;
                    controlSistemaCompresor.CSC_PAD = formSistemaCompresorPAD;
                    controlSistemaCompresor.CSC_PC = formSistemaCompresorPC;
                    controlSistemaCompresor.CSC_PTPostAjustes = formSistemaCompresorPTPostAjustes;
                    controlSistemaCompresor.CSC_PTPreAjustes = formSistemaCompresorPTPreAjustes;
                    controlSistemaCompresor.CSC_RecambioPieza = formSistemaCompresorRecambioPieza;

                    try
                    {
                        await _controlSistemaCompresorServices.Update(sistemaCompresorId, controlSistemaCompresor);
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

            return RedirectToAction(Values.Index, Values.SistemaCompresor, new { area = Values.AreaVacio, idSistemaCompresor = sistemaCompresorId, idPaciente = pacienteId });
        }

        /// <summary>
        /// Eliminar el Sistema Compresor
        /// ControlSistemaCompresor
        /// Paciente.ControlSistemaCompresor
        /// </summary>
        /// <param name="pacienteId"></param>
        /// <param name="sistemaCompresorId"></param>
        /// <returns></returns>
        public async Task<IActionResult> eliminarSistemaCompresor(string pacienteId, string sistemaCompresorId)
        {
            try
            {
                await _controlSistemaCompresorServices.Delete(sistemaCompresorId);
            }
            catch
            {
                return RedirectToAction(Values.Index, Values.PerfilPaciente, new { area = Values.AreaVacio, id = pacienteId });
            }

            try
            {
                Pacientes paciente = await _pacienteServices.GetById(pacienteId);

                for (int i = 0; i < paciente.ControlSistemaCompresor.Count; i++)
                {
                    if (paciente.ControlSistemaCompresor[i] == sistemaCompresorId)
                    {
                        paciente.ControlSistemaCompresor.RemoveAt(i);
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