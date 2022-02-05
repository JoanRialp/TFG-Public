using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using TFG_Web.Areas.Clinics.Interface.DAO;
using TFG_Web.Areas.Clinics.Interface.Services;
using TFG_Web.Areas.Clinics.Models;
using TFG_Web.Models;
using TFG_Web.Services;

namespace TFG_Web.Areas.Clinics.Controllers
{
    [Area(Values.AreaClinics)]
    [ViewLayout(Values.LayoutForms)]
    public class FormControlCampanaPectusExcavatumController : Controller
    {
        #region Dependencias Interfaces
        private ISession Session => HttpContext.Session;
        private readonly ValidarAcceso _validarAcceso = new ValidarAcceso();
        private ViewModel viewModel = new ViewModel();
        private IMemoryCache _memoryCache;
        private IControlCampanaPectusExcavatumServicesDAO _ControlCampanaPectusExcavatum;
        private IGetPacientesServices _getPacientesServices;
        private IPacientesServices _pacientesServices;
        private IControlCampanaPectusExcavatumServices _controlCampanaPectusExcavatumServices;
        #endregion

        #region Constructor
        public FormControlCampanaPectusExcavatumController(IControlCampanaPectusExcavatumServicesDAO controlCampanaPectusExcavatum,
            IGetPacientesServices getPacientesServices, IPacientesServices pacientesServices,
            IControlCampanaPectusExcavatumServices controlCampanaPectusExcavatumServices, IMemoryCache _memoryCache)
        {
            this._memoryCache = _memoryCache;
            this._ControlCampanaPectusExcavatum = controlCampanaPectusExcavatum;
            this._getPacientesServices = getPacientesServices;
            this._pacientesServices = pacientesServices;
            this._controlCampanaPectusExcavatumServices = controlCampanaPectusExcavatumServices;
        }
        #endregion

        /// <summary>
        /// Index normal
        /// </summary>
        /// <returns></returns>
        [Route(Values.ControlCampanaPectusExcavatum)]
        public async Task<IActionResult> Index()
        {
            if (!_validarAcceso.ValidarAccesoWeb(Session)) return RedirectToAction(Values.Index, Values.LoginUsuario, new { area = Values.AreaVacio });

            viewModel = getCacheNotificacionCrearCampanaPectus();

            viewModel.ListPacientes = await _getPacientesServices.ListPacientesAsync(Session.GetString(Values.Session_usrId));

            return View(Values.Index, viewModel);
        }

        /// <summary>
        /// Notificaciones de Crear Control Campana Pectus Excavatum
        /// </summary>
        /// <returns></returns>
        private ViewModel getCacheNotificacionCrearCampanaPectus()
        {
            ViewModel mymodel = new ViewModel();

            var result_CrearSistemaCompresor = _memoryCache.Get("crearCampanaExcavatum");
            var result_CrearSistemaCompresorMensaje = _memoryCache.Get("crearCampanaExcavatumMensaje");
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

        /// <summary>
        /// Index con el paramentro del id Paciente
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("ControlCampanaPectusExcavatum/{id}")]
        public async Task<IActionResult> IndexId(string id)
        {
            if (!_validarAcceso.ValidarAccesoWeb(Session)) return RedirectToAction(Values.Index, Values.LoginUsuario, new { area = Values.AreaVacio });

            viewModel.ListPacientes = await _getPacientesServices.ListPacientesAsync(Session.GetString(Values.Session_usrId));

            if (id != null)
            {
                try
                {
                    viewModel.Pacientes = this._pacientesServices.GetById(id).GetAwaiter().GetResult();
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
        /// Añadir formulario Control Campana Pectus Excavatum a Paciente
        /// </summary>
        /// <param name="campanaPectusExcavatumIdPaciente"></param>
        /// <param name="campanaPectusExcavatumForm1Fecha"></param>
        /// <param name="campanaPectusExcavatumForm1HorasDia"></param>
        /// <param name="campanaPectusExcavatumForm1HorasNoche"></param>
        /// <param name="campanaPectusExcavatumPumpsUso"></param>
        /// <param name="campanaPectusExcavatumForm1PresionTratamiento"></param>
        /// <param name="campanaPectusExcavatumForm1EnUso"></param>
        /// <param name="campanaPectusExcavatumForm1Indicada"></param>
        /// <param name="campanaPectusExcavatumForm1NPumpsIndicados"></param>
        /// <param name="campanaPectusExcavatumForm2Complicaciones"></param>
        /// <param name="customRadioForm2PectusExcavatumFotos"></param>
        /// <param name="campanaPectusExcavatumForm2Observaciones"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddFormularioControlCampanaExcavatum(string campanaPectusExcavatumIdPaciente, DateTime campanaPectusExcavatumForm1Fecha,
            int campanaPectusExcavatumForm1HorasDia, int campanaPectusExcavatumForm1HorasNoche, string campanaPectusExcavatumPumpsUso,
            string campanaPectusExcavatumForm1PresionTratamiento, int campanaPectusExcavatumForm1EnUso, string campanaPectusExcavatumForm1Indicada,
            string campanaPectusExcavatumForm1NPumpsIndicados, string campanaPectusExcavatumForm2Complicaciones, string customRadioForm2PectusExcavatumFotos,
            string campanaPectusExcavatumForm2Observaciones)
        {
            ControlCampanaPectusExcavatum controlCampanaPectusExcavatum = new ControlCampanaPectusExcavatum();

            //Cache notificacion crear Form
            var result_CrearSistemaCompresor = _memoryCache.Get("crearCampanaExcavatum");

            if (campanaPectusExcavatumIdPaciente != null && result_CrearSistemaCompresor == null)
            {
                DateTime fechaNull = new DateTime(0001, 01, 01);
                if (campanaPectusExcavatumForm1Fecha != fechaNull)
                {
                    controlCampanaPectusExcavatum.CCPE_Fecha = campanaPectusExcavatumForm1Fecha;
                    controlCampanaPectusExcavatum.CCPE_HorasUsoDia = campanaPectusExcavatumForm1HorasDia;
                    controlCampanaPectusExcavatum.CCPE_HorasUsoNoche = campanaPectusExcavatumForm1HorasNoche;
                    controlCampanaPectusExcavatum.CCPE_NumeroPumpsUso = campanaPectusExcavatumPumpsUso;
                    controlCampanaPectusExcavatum.CCPE_PresionTratamiento = campanaPectusExcavatumForm1PresionTratamiento;
                    controlCampanaPectusExcavatum.CCPE_Uso = campanaPectusExcavatumForm1EnUso;
                    controlCampanaPectusExcavatum.CCPE_Indicada = campanaPectusExcavatumForm1Indicada;
                    controlCampanaPectusExcavatum.CCPE_NumeroPumpsIndicados = campanaPectusExcavatumForm1NPumpsIndicados;
                    controlCampanaPectusExcavatum.CCPE_Complicaciones = campanaPectusExcavatumForm2Complicaciones;
                    controlCampanaPectusExcavatum.CCPE_Fotos = customRadioForm2PectusExcavatumFotos;
                    controlCampanaPectusExcavatum.CCPE_Observaciones = campanaPectusExcavatumForm2Observaciones;

                    try
                    {
                        await _controlCampanaPectusExcavatumServices.addControlCampanaExcavatumAsync(controlCampanaPectusExcavatum, campanaPectusExcavatumIdPaciente);
                    }
                    catch
                    {
                        NotificacionCacheCrearControlCampanaPectusExcavatum(false, Mensajes.ErrorCrearCampanaExcavatum);
                    }
                }
                else
                {
                    NotificacionCacheCrearControlCampanaPectusExcavatum(false, Mensajes.ErrorCrearCampanaExcavatum);
                }
            }
            else
            {
                NotificacionCacheCrearControlCampanaPectusExcavatum(false, Mensajes.ErrorCrearCampanaExcavatum);
            }

            return RedirectToAction(Values.Index, Values.ControlCampanaPectusExcavatum, new { area = Values.AreaVacio });
        }

        /// <summary>
        /// Cache - Notificacion de la creacion del formulario Control Campana Pectus Excavatum
        /// </summary>
        /// <param name="resultado">Si ha ido bien o no</param>
        /// <param name="mensaje">Mensaje de respuesta</param>
        private void NotificacionCacheCrearControlCampanaPectusExcavatum(bool resultado, string mensaje)
        {
            //Cache - Notificacions creacion
            bool AlreadyExitCrearCampanaExcavatum = _memoryCache.TryGetValue("crearCampanaExcavatum", out string respuesta);
            bool AlreadyExitCrearCampanaExcavatumMensaje = _memoryCache.TryGetValue("crearCampanaExcavatumMensaje", out string respuesta2);

            if (!AlreadyExitCrearCampanaExcavatum && !AlreadyExitCrearCampanaExcavatumMensaje)
            {
                var cacheEntryoptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(2));
                _memoryCache.Set("crearCampanaExcavatum", resultado, cacheEntryoptions);
                _memoryCache.Set("crearCampanaExcavatumMensaje", mensaje, cacheEntryoptions);
            }
        }
    }
}