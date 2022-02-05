using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using TFG_Web.Areas.Clinics.Interface.Services;
using TFG_Web.Areas.Clinics.Models;
using TFG_Web.Models;
using TFG_Web.Services;

namespace TFG_Web.Areas.Clinics.Controllers
{
    [Area(Values.AreaClinics)]
    [ViewLayout(Values.LayoutForms)]
    public class FormControlSistemaCompresorController : Controller
    {
        #region Dependencias Interfaces
        private ISession Session => HttpContext.Session;
        private readonly ValidarAcceso _validarAcceso = new ValidarAcceso();
        private readonly IMemoryCache _memoryCache;
        private ViewModel viewModel = new ViewModel();
        private readonly IGetPacientesServices _getPacientesServices;
        private readonly IPacientesServices _pacientesServices;
        private readonly IControlSistemaCompresorServices _controlSistemaCompresorServices;
        #endregion

        #region Constructor
        public FormControlSistemaCompresorController(IMemoryCache _memoryCache, IGetPacientesServices getPacientesServices,
            IControlSistemaCompresorServices controlSistemaCompresorServices, IPacientesServices pacientesServices)
        {
            this._memoryCache = _memoryCache;
            this._getPacientesServices = getPacientesServices;
            this._pacientesServices = pacientesServices;
            this._controlSistemaCompresorServices = controlSistemaCompresorServices;
        }
        #endregion

        [Route(Values.ControlSistemaCompresor)]
        public async Task<IActionResult> Index()
        {
            if (!_validarAcceso.ValidarAccesoWeb(Session)) return RedirectToAction(Values.Index, Values.LoginUsuario, new { area = Values.AreaVacio });

            viewModel = getCacheNotificacionCrearSistemaCompresor();

            viewModel.ListPacientes = await _getPacientesServices.ListPacientesAsync(Session.GetString(Values.Session_usrId));

            return View(Values.Index, viewModel);
        }

        /// <summary>
        /// Notificaciones de Crear Control Sistema Compresor
        /// </summary>
        /// <returns></returns>
        private ViewModel getCacheNotificacionCrearSistemaCompresor()
        {
            ViewModel mymodel = new ViewModel();
            var result_CrearSistemaCompresor = _memoryCache.Get("crearSistemaCompresor");
            var result_CrearSistemaCompresorMensaje = _memoryCache.Get("crearSistemaCompresorMensaje");
            if (result_CrearSistemaCompresor != null)
            {
                mymodel.Notificaciones = bool.Parse(result_CrearSistemaCompresor.ToString());
                mymodel.NotificacionesControl = true;
            }
            if (result_CrearSistemaCompresorMensaje != null)
            {
                mymodel.NotificacionesMensaje = result_CrearSistemaCompresorMensaje.ToString();
                mymodel.NotificacionesControl = true;
            }
            return mymodel;
        }

        [Route("ControlSistemaCompresor/{id}")]
        public async Task<IActionResult> IndexId(string id)
        {
            if (!_validarAcceso.ValidarAccesoWeb(Session)) return RedirectToAction(Values.Index, Values.LoginUsuario, new { area = Values.AreaVacio });

            viewModel.ListPacientes = await _getPacientesServices.ListPacientesAsync(Session.GetString(Values.Session_usrId));

            if (id != null)
            {
                try
                {
                    viewModel.Pacientes = await this._pacientesServices.GetById(id);
                    viewModel.id_Paciente = id;
                }
                catch (Exception ex)
                {
                    viewModel.HistorialClinico = null;
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                viewModel.id_Paciente = null;
            }

            return View(Values.Index, viewModel);
        }

        /// <summary>
        /// Añadir formulario Control Sistema Compresor
        /// </summary>
        /// <param name="SistemaCompresorIdPaciente"></param>
        /// <param name="SistemaCompresorForm1Fecha"></param>
        /// <param name="SistemaCompresorForm1HorasDia"></param>
        /// <param name="SistemaCompresorForm1HorasNoche"></param>
        /// <param name="SistemaCompresorForm1PC"></param>
        /// <param name="customRadioSistemaCompresorForm1Grupo"></param>
        /// <param name="SistemaCompresorForm1PTPre"></param>
        /// <param name="SistemaCompresorForm1PTPost"></param>
        /// <param name="customRadioSistemaCompresorForm1AjustesDinamico"></param>
        /// <param name="customRadioForm2SistemaCompresorF1"></param>
        /// <param name="customRadioForm2SistemaCompresorF2"></param>
        /// <param name="customRadioForm2SistemaCompresorF3"></param>
        /// <param name="customRadioForm2SistemaCompresorF4"></param>
        /// <param name="customRadioForm2SistemaCompresorF5"></param>
        /// <param name="customCheckboxForm2SistemaCompresorDoblador"></param>
        /// <param name="customCheckboxForm2SistemaCompresorRecambio"></param>
        /// <param name="customRadioForm2SistemaCompresorPAD"></param>
        /// <param name="customRadioForm2SistemaCompresorPADOther"></param>
        /// <param name="Form3SistemaCompresorComplicaciones"></param>
        /// <param name="customRadioForm3SistemaCompresorFotos"></param>
        /// <param name="Form3SistemaCompresorObservaciones"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddFormularioControlSistemaCompresor(string SistemaCompresorIdPaciente, DateTime SistemaCompresorForm1Fecha, int SistemaCompresorForm1HorasDia,
            int SistemaCompresorForm1HorasNoche, string SistemaCompresorForm1PC, string customRadioSistemaCompresorForm1Grupo, string SistemaCompresorForm1PTPre,
            string SistemaCompresorForm1PTPost, string customRadioSistemaCompresorForm1AjustesDinamico, string customRadioForm2SistemaCompresorF1,
            string customRadioForm2SistemaCompresorF2, string customRadioForm2SistemaCompresorF3, string customRadioForm2SistemaCompresorF4,
            string customRadioForm2SistemaCompresorF5, string customCheckboxForm2SistemaCompresorDoblador, string customCheckboxForm2SistemaCompresorRecambio,
            string customRadioForm2SistemaCompresorPAD, string customRadioForm2SistemaCompresorPADOther, string Form3SistemaCompresorComplicaciones,
            string Form3SistemaCompresorObservaciones)
        {
            ControlSistemaCompresor controlSistemaCompresor = new ControlSistemaCompresor();
            var result_CrearSistemaCompresor = _memoryCache.Get("crearSistemaCompresor");

            if (SistemaCompresorIdPaciente != null && result_CrearSistemaCompresor == null)
            {
                DateTime fechaNull = new DateTime(0001, 01, 01);
                if (SistemaCompresorForm1Fecha != fechaNull && SistemaCompresorForm1PC != null)
                {
                    controlSistemaCompresor.CSC_Fecha = SistemaCompresorForm1Fecha;
                    controlSistemaCompresor.CSC_HorasUsoDia = SistemaCompresorForm1HorasDia;
                    controlSistemaCompresor.CSC_HorasUsoNoche = SistemaCompresorForm1HorasNoche;
                    controlSistemaCompresor.CSC_PC = SistemaCompresorForm1PC;

                    if (customRadioSistemaCompresorForm1Grupo != null)
                    {
                        controlSistemaCompresor.CSC_Grupo = Int32.Parse(customRadioSistemaCompresorForm1Grupo);
                    }

                    controlSistemaCompresor.CSC_PTPreAjustes = SistemaCompresorForm1PTPre;
                    controlSistemaCompresor.CSC_PTPostAjustes = SistemaCompresorForm1PTPost;
                    controlSistemaCompresor.CSC_AjustesSistemaCompresorDinamico = customRadioSistemaCompresorForm1AjustesDinamico;
                    controlSistemaCompresor.CSC_F1 = customRadioForm2SistemaCompresorF1;
                    controlSistemaCompresor.CSC_F2 = customRadioForm2SistemaCompresorF2;
                    controlSistemaCompresor.CSC_F3 = customRadioForm2SistemaCompresorF3;
                    controlSistemaCompresor.CSC_F4 = customRadioForm2SistemaCompresorF4;
                    controlSistemaCompresor.CSC_F5 = customRadioForm2SistemaCompresorF5;
                    controlSistemaCompresor.CSC_Doblador = customCheckboxForm2SistemaCompresorDoblador;
                    controlSistemaCompresor.CSC_RecambioPieza = customCheckboxForm2SistemaCompresorRecambio;

                    if (customRadioForm2SistemaCompresorPAD == "Other")
                    {
                        controlSistemaCompresor.CSC_PAD = customRadioForm2SistemaCompresorPADOther;
                    }
                    else
                    {
                        controlSistemaCompresor.CSC_PAD = customRadioForm2SistemaCompresorPAD;
                    }

                    controlSistemaCompresor.CSC_Compilaciones = Form3SistemaCompresorComplicaciones;
                    controlSistemaCompresor.CSC_Observaciones = Form3SistemaCompresorObservaciones;

                    try
                    {
                        //Llama el ControlSistemaCompresorController para añadir el formulario a MongoDB
                        await _controlSistemaCompresorServices.addControlSistemaCompresorAsync(controlSistemaCompresor, SistemaCompresorIdPaciente);
                    }
                    catch
                    {
                        NotificacionCacheCrearSistemaCompresor(false, Mensajes.ErrorCrearSistemaCompresor);
                    }
                }
                else
                {
                    NotificacionCacheCrearSistemaCompresor(false, Mensajes.ErrorCrearSistemaCompresor);
                }
            }
            else
            {
                NotificacionCacheCrearSistemaCompresor(false, Mensajes.ErrorCrearSistemaCompresor);
            }

            return RedirectToAction(Values.Index, Values.ControlSistemaCompresor, new { area = Values.AreaVacio });
        }

        /// <summary>
        /// Cache - Notificacion de la creacion del formulario Control Sistema Compresor
        /// </summary>
        /// <param name="resultado"></param>
        /// <param name="mensaje"></param>
        private void NotificacionCacheCrearSistemaCompresor(bool resultado, string mensaje)
        {
            //Cache - Notificacions creacion
            bool AlreadyExitCrearSistemaCompresor = _memoryCache.TryGetValue("crearSistemaCompresor", out string respuesta);
            bool AlreadyExitCrearSistemaCompresorMensaje = _memoryCache.TryGetValue("crearSistemaCompresorMensaje", out string respuesta2);

            if (!AlreadyExitCrearSistemaCompresor && !AlreadyExitCrearSistemaCompresorMensaje)
            {
                var cacheEntryoptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(2));
                _memoryCache.Set("crearSistemaCompresor", resultado, cacheEntryoptions);
                _memoryCache.Set("crearSistemaCompresorMensaje", mensaje, cacheEntryoptions);
            }
        }
    }
}