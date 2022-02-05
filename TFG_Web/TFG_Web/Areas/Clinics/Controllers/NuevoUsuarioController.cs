using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TFG_Web.Models;
using TFG_Web.Models.HistorialClinicoCollections;
using TFG_Web.Services;
using TFG_Web.Areas.Clinics.Services;
using TFG_Web.Areas.Clinics.Models;
using Microsoft.Extensions.Caching.Memory;
using TFG_Web.Areas.Clinics.Interface.Services;
using TFG_Web.Areas.Clinics.Interface.Services.IHistorialClinicoServices;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Runtime.InteropServices;

namespace TFG_Web.Areas.Clinics.Controllers
{
    [Area(Values.AreaClinics)]
    [ViewLayout(Values.LayoutForms)]
    public class NuevoUsuarioController : Controller
    {
        #region Dependencias Interfaces
        private ISession Session => HttpContext.Session;
        private readonly ValidarAcceso _validarAcceso = new ValidarAcceso();
        private readonly IMemoryCache _memoryCache;
        private readonly ViewModel viewModel = new ViewModel();
        private readonly IGetPacientesServices _getPacientesServices;
        private readonly IPacientesServices _pacientesServices;
        private readonly IHistorialClinicoServices _historialClinicoServices;
        private readonly IAnamnesisServices _anamnesisServices;
        private readonly ICirugiaPreviaServices _cirurgiaPreviaServices;
        private readonly IClasificacionPectusServices _clasificacionPectusServices;
        private readonly IDeporteServices _deporteServices;
        private readonly IDolorPechoServices _dolorPechoServices;
        private readonly IEnfermdedadPreexistenteServices _enfermedadPreexistenteServices;
        private readonly IPectusCarinatumServices _pectusCarinatumServices;
        private readonly IPectusExcavatumServices _pectusExcavatumServices;
        private readonly IPectusMixtoServices _pectusMixtoServices;
        private readonly ISignosSintomasClinicosServices _signosSintomasClinicosServices;
        private readonly ISindromePolandServices _sindromePolandServices;
        private readonly IWebHostEnvironment _environment;
        #endregion

        #region Constructor
        public NuevoUsuarioController(IGetPacientesServices getPacientesServices, IPacientesServices pacientesServices, IHistorialClinicoServices historialClinicoServices,
            IAnamnesisServices anamnesisServices, ICirugiaPreviaServices cirurgiaPreviaServices, IClasificacionPectusServices clasificacionPectusServices,
            IDeporteServices deporteServices, IDolorPechoServices dolorPechoServices, IEnfermdedadPreexistenteServices enfermedadPreexistenteServices,
            IPectusCarinatumServices pectusCarinatumServices, IPectusExcavatumServices pectusExcavatumServices, IPectusMixtoServices pectusMixtoServices,
            ISignosSintomasClinicosServices signosSintomasClinicosServices, ISindromePolandServices sindromePolandServices, IMemoryCache _memoryCache,
            IWebHostEnvironment environment)
        {
            this._memoryCache = _memoryCache;
            this._getPacientesServices = getPacientesServices;
            this._pacientesServices = pacientesServices;
            this._historialClinicoServices = historialClinicoServices;
            this._anamnesisServices = anamnesisServices;
            this._cirurgiaPreviaServices = cirurgiaPreviaServices;
            this._clasificacionPectusServices = clasificacionPectusServices;
            this._deporteServices = deporteServices;
            this._dolorPechoServices = dolorPechoServices;
            this._enfermedadPreexistenteServices = enfermedadPreexistenteServices;
            this._pectusCarinatumServices = pectusCarinatumServices;
            this._pectusExcavatumServices = pectusExcavatumServices;
            this._pectusMixtoServices = pectusMixtoServices;
            this._signosSintomasClinicosServices = signosSintomasClinicosServices;
            this._sindromePolandServices = sindromePolandServices;
            this._environment = environment;

        }
        #endregion

        [Route(Values.NuevoPaciente)]
        public async Task<IActionResult> IndexAsync()
        {
            if (!_validarAcceso.ValidarAccesoWeb(Session)) return RedirectToAction(Values.Index, Values.LoginUsuario, new { area = Values.AreaVacio });

            ViewModel mymodel = new ViewModel();
            mymodel = getCacheNotificacionCrearPaciente();

            mymodel.ListPacientes = await _getPacientesServices.ListPacientesAsync(Session.GetString(Values.Session_usrId));

            return View(Values.Index, mymodel);
        }

        /// <summary>
        /// Añadir otro historial clinico a un paciente
        /// </summary>
        /// <param name="id">id Paciente</param>
        /// <returns></returns>
        [Route(Values.NuevoPaciente + "/{id}")]
        public async Task<IActionResult> IndexIdPacienteAsync(string id)
        {
            if (!_validarAcceso.ValidarAccesoWeb(Session)) return RedirectToAction(Values.Index, Values.LoginUsuario, new { area = Values.AreaVacio });

            ViewModel mymodel = new ViewModel();
            mymodel = getCacheNotificacionCrearPaciente();

            try
            {
                var paciente = await _pacientesServices.GetById(id);

                if (paciente.HistorialClinicoId.Count() == 1)
                {
                    mymodel.Pacientes = paciente;
                }
            }
            catch
            {
                return RedirectToAction(Values.Index, Values.NuevoPaciente, new { area = Values.AreaVacio });
            }

            mymodel.ListPacientes = await _getPacientesServices.ListPacientesAsync(Session.GetString(Values.Session_usrId));

            return View(Values.Index, mymodel);
        }

        private ViewModel getCacheNotificacionCrearPaciente()
        {
            ViewModel mymodel = new ViewModel();
            var result_crearPaciente = _memoryCache.Get("crearPaciente");
            var result_crearPacienteMensaje = _memoryCache.Get("crearPacienteMensaje");
            var result_crearPacientePaciente = _memoryCache.Get("crearPacientePaciente");

            if (result_crearPaciente != null)
            {
                mymodel.Notificaciones = bool.Parse(result_crearPaciente.ToString());
                mymodel.NotificacionesControl = true;
            }
            if (result_crearPacienteMensaje != null)
            {
                mymodel.NotificacionesMensaje = result_crearPacienteMensaje.ToString();
            }
            if (result_crearPacientePaciente != null)
            {
                mymodel.NotificacionesPaciente = result_crearPacientePaciente.ToString();
            }

            return mymodel;
        }

        /// <summary>
        /// Añadimos el formulario Nuevo Usuario a la BD
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddFormularioNuevoUsuario(string id_pacienteNuevoUsuarioForm, string NuevoUsuarioForm1Nombre, string NuevoUsuarioForm1Apellidos, string NuevoUsuarioForm1Correo, DateTime NuevoUsuarioForm1Fecha,
            DateTime NuevoUsuarioForm1Edad, string customRadioFormNuevoUsuarioGenero, string customRadioFormNuevoUsuarioMotivo,
            string NuevoUsuarioForm1MotivoOther, string NuevoUsuarioForm1EdadNotaron, string customRadioFormNuevoUsuarioEmpeorado, string NuevoUsuarioForm1Cuando,
            string customRadioFormNuevoUsuarioHermanos, string[] checkboxForm1NuevoUsuarioAntecedentesCarinatum, string[] checkboxForm1NuevoUsuarioAntecedentesExcavatum,
            string[] checkboxForm1NuevoUsuarioAntecedentesOtras, string[] checkboxForm1NuevoUsuarioAntecedentesEscoliosis, string NuevoUsuarioForm1OtrosFamiliares,
            string customRadioForm1NuevoUsuarioConsultado, string inputForm1NuevoUsuarioConsultadoCuando, string inputForm1NuevoUsuarioConsultadoDonde, string customRadioForm1NuevoUsuarioCirugia, string NuevoUsuarioForm2Cirugia, string NuevoUsuarioForm2Edad,
            string customRadioForm3NuevoUsuarioEnfermedadPre, string NuevoUsuarioForm4TipoEnfermedad, string NuevoUsuarioForm4EdadDiagnostico, string customRadioForm4NuevoUsuarioOtro,
            string NuevoUsuarioForm4Especificar, string customRadioForm5NuevoUsuarioAlergia, string inputForm5NuevoUsuarioAlergiaCual, string NuevoUsaurioForm5Observaciones, string NuevoUsaurioForm5OtrasAlergias,
            string customRadioForm5NuevoUsuarioDeportes, string[] customRadioForm6NuevoUsuarioDeportesCual, string InputForm6NuevoUsuarioDeportesCualOther, string customRadioForm6NuevoUsuarioFrecuencia,
            string customRadioForm6NuevoUsuarioDejado, string customRadioForm6NuevoUsuarioPorque, string customRadioForm7NuevoUsuarioFuma, string customRadioForm7NuevoUsuarioMedicacion,
            string NuevoUsuarioForm7CualEs, string customRadioForm7NuevoUsuarioDolor, string[] checkboxForm8NuevoUsuarioTipoDolorReeposo, string[] checkboxForm8NuevoUsuarioTipoDolorActividadFisica,
            string selectOptionForm8NuevoUsuarioFrecuenciaDolorReposo, string selectOptionForm8NuevoUsuarioFrecuenciaDolorActividadFisica, string customRadioForm9NuevoUsuarioDificultadReposo,
            string customRadioForm9NuevoUsuarioDificultadActividad, string customRadioForm9NuevoUsuarioRepeticion,
            string customRadioForm9NuevoUsuarioRepeticion2, string customRadioForm9NuevoUsuarioEspalda, string customRadioForm9NuevoUsuarioPalpitaciones, string NuevoUsuarioForm9Otros, string[] customRadioForm9NuevoUsuarioPsicosocialesIncomodidad,
            string[] customRadioForm9NuevoUsuarioPsicosocialesVerguenza, string[] customRadioForm9NuevoUsuarioPsicosocialesAnsiedad, string[] customRadioForm9NuevoUsuarioPsicosocialesAngustia,
            string[] customRadioForm9NuevoUsuarioPsicosocialesTristeza, string[] customRadioForm9NuevoUsuarioPsicosocialesSentimientos, string[] customRadioForm9NuevoUsuarioPsicosocialesDificultad,
            string[] customRadioForm9NuevoUsuarioPsicosocialesOtros, string customRadioForm9NuevoUsuarioPectus, string customRadioForm11NuevoUsuarioPectusCarinatumTipo,
            string customRadioForm11NuevoUsuarioPectusCarinatumSimetria, string customRadioForm11NuevoUsuarioPectusCarinatumRotacion, string NuevoUsuarioForm11PresionCorreccion,
            string customRadioForm12NuevoUsuarioExcavatumSimetria, string NuevoUsuarioForm12HundimientoParado, string NuevoUsuarioForm12HundimientoAcostado, string customRadioForm12NuevoUsuarioExcavatumRotacion,
            string customRadioForm13NuevoUsuarioMixtoUbicacion, string NuevoUsuarioForm13PresionCorreccion, string customRadioForm13NuevoUsuarioMixtoRotacion, string customRadioForm14NuevoUsuarioPolandToracica,
            string customRadioForm14NuevoUsuarioPolandMamaria, string customRadioForm14NuevoUsuarioPolandPezon, string customRadioForm14NuevoUsuarioPolandT, string NuevoUsuarioForm14PC,
            string customRadioForm10NuevoUsuarioClasificacionForma, string customRadioForm10NuevoUsuarioClasificacionAlerones, string customRadioForm10NuevoUsuarioClasificacionHombros,
            string customRadioForm10NuevoUsuarioClasificacionAntepulsion, string customRadioForm10NuevoUsuarioClasificacionAsimetria, string customRadioForm10NuevoUsuarioClasificacionPosicion,
            string customRadioForm10NuevoUsuarioClasificacionEstrias, string NuevoUsuarioForm10Donde, string customRadioForm10NuevoUsuarioClasificacionTratamiento,
            string InputForm10NuevoUsuarioClasificacionTratamiento, string NuevoUsuarioForm10Anotaciones)
        {
            if (!_validarAcceso.ValidarAccesoWeb(Session)) return RedirectToAction(Values.Index, Values.LoginUsuario, new { area = Values.AreaVacio });

            //Cache Notificacion - Paciente Creado
            bool AlreadyExitCrearPaciente = _memoryCache.TryGetValue("crearPaciente", out string respuesta1);
            bool AlreadyExitCrearPacienteMensaje = _memoryCache.TryGetValue("crearPacienteMensaje", out string respuesta2);
            bool AlreadyExitCrearPacientePaciente = _memoryCache.TryGetValue("crearPacientePaciente", out string respuesta3);

            //Control para no poder enviar muchos formularios seguidos (Cache)
            bool AlreadyExitNuevoUsuario = _memoryCache.TryGetValue("nuevoUsuarioEnviar", out string respuesta);

            if (!AlreadyExitNuevoUsuario)
            {
                var cacheEntryoptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(2));
                _memoryCache.Set("nuevoUsuarioEnviar", "1", cacheEntryoptions);
            }
            else
            {
                if (!AlreadyExitCrearPaciente && !AlreadyExitCrearPacienteMensaje && !AlreadyExitCrearPacientePaciente)
                {
                    var cacheEntryoptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(2));
                    _memoryCache.Set("crearPaciente", true, cacheEntryoptions);
                    _memoryCache.Set("crearPacienteMensaje", Mensajes.SuccessUsuarioCreado, cacheEntryoptions);
                    _memoryCache.Set("crearPacientePaciente", NuevoUsuarioForm1Nombre + NuevoUsuarioForm1Apellidos, cacheEntryoptions);
                }
                return RedirectToAction(Values.Index, Values.NuevoPaciente, new { area = Values.AreaVacio });
            }

            //Controlar el submit del formulario con el id del paciente seleccionado
            UsuariosServices usuariosController = new UsuariosServices();
            var pacienteIdControl = await usuariosController.GetById(Session.GetString(Values.Session_usrId));
            if (pacienteIdControl.Id == null)
            {
                var cacheEntryoptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(2));
                _memoryCache.Set("crearPaciente", false, cacheEntryoptions);
                _memoryCache.Set("crearPacienteMensaje", Mensajes.ErrorUsuarioNoCreado, cacheEntryoptions);
                _memoryCache.Set("crearPacientePaciente", NuevoUsuarioForm1Nombre + NuevoUsuarioForm1Apellidos, cacheEntryoptions);

                return RedirectToAction(Values.Index, Values.NuevoPaciente, new { area = Values.AreaVacio });
            }

            //Validar campos obligatorios
            if (NuevoUsuarioForm1Nombre == null || NuevoUsuarioForm1Apellidos == null || NuevoUsuarioForm1Correo == null)
            {
                var cacheEntryoptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(2));
                _memoryCache.Set("crearPaciente", false, cacheEntryoptions);
                _memoryCache.Set("crearPacienteMensaje", Mensajes.ErrorUsuarioNoCreado, cacheEntryoptions);
                _memoryCache.Set("crearPacientePaciente", NuevoUsuarioForm1Nombre + NuevoUsuarioForm1Apellidos, cacheEntryoptions);

                return RedirectToAction(Values.Index, Values.NuevoPaciente, new { area = Values.AreaVacio });
            }

            HistorialClinico historialClinico = new HistorialClinico();
            CirugiaPrevia cirugiaPrevia = new CirugiaPrevia();
            Anamnesis anamnesis = new Anamnesis();
            DolorPecho dolorPecho = new DolorPecho();
            EnfermdedadPreexistente enfermdedadPreexistente = new EnfermdedadPreexistente();
            Deporte deporte = new Deporte();
            SignosSintomasClinicos signosSintomasClinicos = new SignosSintomasClinicos();
            ClasificacionPectus clasificacionPectus = new ClasificacionPectus();
            PectusCarinatum pectusCarinatum = new PectusCarinatum();
            PectusExcavatum pectusExcavatum = new PectusExcavatum();
            PectusMixto pectusMixto = new PectusMixto();
            SindromePoland sindromePoland = new SindromePoland();

            List<string> listado_CirugiaPrevia = new List<string>();
            List<string> listado_Anamnesis = new List<string>();
            List<string> listado_DolorPecho = new List<string>();
            List<string> listado_EnfermdedadPreexistente = new List<string>();
            List<string> listado_Deporte = new List<string>();
            List<string> listado_SignosSintomasClinicos = new List<string>();
            List<string> listado_TipoPectus = new List<string>();
            List<string> listado_ClasificacionPectus = new List<string>();
            List<string> listado_HistorialClinico = new List<string>();

            //HistorialClinico Form1
            historialClinico.HC_Nombre = NuevoUsuarioForm1Nombre;
            historialClinico.HC_Apellidos = NuevoUsuarioForm1Apellidos;
            historialClinico.HC_FechaPrimeraConsulta = NuevoUsuarioForm1Fecha;
            historialClinico.HC_FechaUltimaConsulta = null;
            historialClinico.HC_Edad = NuevoUsuarioForm1Edad;
            historialClinico.HC_Genero = customRadioFormNuevoUsuarioGenero;

            //CustomGroupRadio con opcion de Other: texto
            if (customRadioForm9NuevoUsuarioPectus != null)
            {
                historialClinico.HC_MotivoConsulta = customRadioForm9NuevoUsuarioPectus;
            }
            else
            {
                historialClinico.HC_MotivoConsulta = NuevoUsuarioForm1MotivoOther;
            }

            historialClinico.HC_EdadNotaronDeformidadToracica = NuevoUsuarioForm1EdadNotaron;
            historialClinico.HC_EmpeoradoUltimamente = customRadioFormNuevoUsuarioEmpeorado;
            historialClinico.HC_Cuando = NuevoUsuarioForm1Cuando;
            historialClinico.HC_HermanosGemelos = customRadioFormNuevoUsuarioHermanos;

            //Guardamos los checkboxs como una array[,]
            historialClinico.HC_AntecendentesFamilia = grupoCheckboxToArray(checkboxForm1NuevoUsuarioAntecedentesCarinatum, checkboxForm1NuevoUsuarioAntecedentesExcavatum,
                checkboxForm1NuevoUsuarioAntecedentesOtras, checkboxForm1NuevoUsuarioAntecedentesEscoliosis, 4, 4);

            historialClinico.HC_OtrosFamiliares = NuevoUsuarioForm1OtrosFamiliares;
            historialClinico.HC_ConsultadoPectus = customRadioForm1NuevoUsuarioConsultado;
            historialClinico.HC_ConsultadoPectusCuando = inputForm1NuevoUsuarioConsultadoCuando;
            historialClinico.HC_ConsultadoPectusDonde = inputForm1NuevoUsuarioConsultadoDonde;
            historialClinico.HC_CirurgiaPrevia = customRadioForm1NuevoUsuarioCirugia;

            //CirugiaPrevia Form2
            if (customRadioForm1NuevoUsuarioCirugia != null && customRadioForm1NuevoUsuarioCirugia != "No")
            {
                cirugiaPrevia.CP_TipoCirugia = NuevoUsuarioForm2Cirugia;
                cirugiaPrevia.CP_Edad = NuevoUsuarioForm2Edad;

                var cirugiaPrevia_Id = createCirugiaPreviaAsync(cirugiaPrevia);

                if (cirugiaPrevia_Id != null)
                {
                    listado_CirugiaPrevia.Add(cirugiaPrevia_Id.GetAwaiter().GetResult());
                }
            }

            //Anamnesis Form3
            anamnesis.A_EnfermedadPreexistente = customRadioForm3NuevoUsuarioEnfermedadPre;

            //EnfermdedadPreexistente Form4
            if (customRadioForm3NuevoUsuarioEnfermedadPre != null && customRadioForm3NuevoUsuarioEnfermedadPre != "No")
            {
                enfermdedadPreexistente.A_Tipo = NuevoUsuarioForm4TipoEnfermedad;
                enfermdedadPreexistente.A_Edad = NuevoUsuarioForm4EdadDiagnostico;
                enfermdedadPreexistente.A_Otros = customRadioForm4NuevoUsuarioOtro;
                enfermdedadPreexistente.A_Especificar = NuevoUsuarioForm4Especificar;

                var enfermedadPreexistente_Id = createEnfermedadPreexistente(enfermdedadPreexistente);

                if (enfermedadPreexistente_Id != null)
                {
                    listado_EnfermdedadPreexistente.Add(enfermedadPreexistente_Id.GetAwaiter().GetResult());
                }
            }

            //Anamnesis Form5 Alergias
            anamnesis.A_Alergia = customRadioForm5NuevoUsuarioAlergia;
            anamnesis.A_AlergiaCual = inputForm5NuevoUsuarioAlergiaCual;

            //Anamnesis Form5
            anamnesis.A_Observaciones = NuevoUsaurioForm5Observaciones;
            anamnesis.A_OtrasAlergias = NuevoUsaurioForm5OtrasAlergias;
            anamnesis.A_Deporte = customRadioForm5NuevoUsuarioDeportes;

            //Deporte Form6
            if (customRadioForm5NuevoUsuarioDeportes != null && customRadioForm5NuevoUsuarioDeportes != "No")
            {
                deporte.D_Cual = customRadioForm6NuevoUsuarioDeportesCual;

                if (customRadioForm6NuevoUsuarioDeportesCual.Contains("Other"))
                {
                    deporte.D_Cual[customRadioForm6NuevoUsuarioDeportesCual.Length - 1] = "Other: " + InputForm6NuevoUsuarioDeportesCualOther;
                }

                deporte.D_Frecuencia = customRadioForm6NuevoUsuarioFrecuencia;
                deporte.D_DejoDeporte = customRadioForm6NuevoUsuarioDejado;
                deporte.D_Porque = customRadioForm6NuevoUsuarioPorque;

                var createDeporte_Id = createDeporte(deporte);

                if (createDeporte_Id != null)
                {
                    listado_Deporte.Add(createDeporte_Id.GetAwaiter().GetResult());
                }
            }

            //Anamnesis Form7
            anamnesis.A_Fuma = customRadioForm7NuevoUsuarioFuma;
            anamnesis.A_Medicacion = customRadioForm7NuevoUsuarioMedicacion;
            anamnesis.A_Cual = NuevoUsuarioForm7CualEs;

            //DolorPecho Form8
            if (customRadioForm7NuevoUsuarioDolor != null && customRadioForm7NuevoUsuarioDolor != "No")
            {
                dolorPecho.DP_Tipo = grupoCheckboxToArray(checkboxForm8NuevoUsuarioTipoDolorReeposo, checkboxForm8NuevoUsuarioTipoDolorActividadFisica, 2, 4);

                string[,] arrayFrecuencia = new string[2, 1];
                arrayFrecuencia[0, 0] = selectOptionForm8NuevoUsuarioFrecuenciaDolorReposo;
                arrayFrecuencia[1, 0] = selectOptionForm8NuevoUsuarioFrecuenciaDolorActividadFisica;
                dolorPecho.DP_Frecuencia = arrayFrecuencia;

                var dolorPecho_Id = createDolorPecho(dolorPecho);

                if (dolorPecho_Id != null)
                {
                    listado_DolorPecho.Add(dolorPecho_Id.GetAwaiter().GetResult());
                }
            }

            //Anamnesis add BBDD
            anamnesis.DolorPecho = listado_DolorPecho;

            var anamnesis_Id = createAnamnesis(anamnesis);

            if (anamnesis_Id != null)
            {
                listado_Anamnesis.Add(anamnesis_Id.GetAwaiter().GetResult());
            }

            //SignosSintomasClinicos Form9
            signosSintomasClinicos.SSC_DificultadRespiratoriaReposo = customRadioForm9NuevoUsuarioDificultadReposo;
            signosSintomasClinicos.SSC_DificultadRespiratoriaActividadFisica = customRadioForm9NuevoUsuarioDificultadActividad;
            signosSintomasClinicos.SSC_NeumoniasRepeticion = customRadioForm9NuevoUsuarioRepeticion;
            signosSintomasClinicos.SSC_OtrasNeumoniasRepeticion = customRadioForm9NuevoUsuarioRepeticion2;
            signosSintomasClinicos.SSC_DolorEspalda = customRadioForm9NuevoUsuarioEspalda;
            signosSintomasClinicos.SSC_PalpitacionesTaquicardia = customRadioForm9NuevoUsuarioPalpitaciones;
            signosSintomasClinicos.SSC_Otros = NuevoUsuarioForm9Otros;

            signosSintomasClinicos.SSC_SignosSintomas = grupoCheckboxToArray(customRadioForm9NuevoUsuarioPsicosocialesIncomodidad, customRadioForm9NuevoUsuarioPsicosocialesVerguenza, customRadioForm9NuevoUsuarioPsicosocialesAnsiedad,
                customRadioForm9NuevoUsuarioPsicosocialesAngustia, customRadioForm9NuevoUsuarioPsicosocialesTristeza, customRadioForm9NuevoUsuarioPsicosocialesSentimientos,
                customRadioForm9NuevoUsuarioPsicosocialesDificultad, customRadioForm9NuevoUsuarioPsicosocialesOtros, 8, 2);

            //PectusCarinatum Form11
            if (customRadioForm9NuevoUsuarioPectus == "Pectus Carinatum")
            {
                pectusCarinatum.PC_Tipo = customRadioForm11NuevoUsuarioPectusCarinatumTipo;
                pectusCarinatum.PC_Simetria = customRadioForm11NuevoUsuarioPectusCarinatumSimetria;
                pectusCarinatum.PC_RotacionEsternal = customRadioForm11NuevoUsuarioPectusCarinatumRotacion;
                pectusCarinatum.PC_PresionCorreccion = NuevoUsuarioForm11PresionCorreccion;

                var pectusCarinatum_Id = createPectusCarinatum(pectusCarinatum);
                if (pectusCarinatum_Id != null)
                {
                    listado_TipoPectus.Add(pectusCarinatum_Id.GetAwaiter().GetResult());
                }
                signosSintomasClinicos.SSC_TipoPectusNombre = "PectusCarinatum";
                signosSintomasClinicos.SSC_TipoPectus = listado_TipoPectus;
            }

            //PectusExcavatum Form12
            if (customRadioForm9NuevoUsuarioPectus == "Pectus Excavatum")
            {
                pectusExcavatum.PE_Simetria = customRadioForm12NuevoUsuarioExcavatumSimetria;
                pectusExcavatum.PE_HundimientoToracicoParado = NuevoUsuarioForm12HundimientoParado;
                pectusExcavatum.PE_HundimientoToracicoAcostado = NuevoUsuarioForm12HundimientoAcostado;
                pectusExcavatum.PE_RotacionEsternal = customRadioForm12NuevoUsuarioExcavatumRotacion;

                var pectusExcavatum_id = createPectusExcavatum(pectusExcavatum);
                if (pectusExcavatum_id != null)
                {
                    listado_TipoPectus.Add(pectusExcavatum_id.GetAwaiter().GetResult());
                }
                signosSintomasClinicos.SSC_TipoPectusNombre = "PectusExcavatum";
                signosSintomasClinicos.SSC_TipoPectus = listado_TipoPectus;
            }

            //PectusMixto Form13
            if (customRadioForm9NuevoUsuarioPectus == "Pectus Mixto")
            {
                pectusMixto.PM_UbicacionDefecto = customRadioForm13NuevoUsuarioMixtoUbicacion;
                pectusMixto.PM_PresionCorreccionPectusCarinatum = NuevoUsuarioForm13PresionCorreccion;
                pectusMixto.PM_RotacionEsternal = customRadioForm13NuevoUsuarioMixtoRotacion;

                var pectusMixto_Id = createPectusMixto(pectusMixto);
                if (pectusMixto_Id != null)
                {
                    listado_TipoPectus.Add(pectusMixto_Id.GetAwaiter().GetResult());
                }
                signosSintomasClinicos.SSC_TipoPectusNombre = "PectusMixto";
                signosSintomasClinicos.SSC_TipoPectus = listado_TipoPectus;
            }

            //SindromePoland Form14
            if (customRadioForm9NuevoUsuarioPectus == "Síndrome de Poland")
            {
                sindromePoland.SP_AnomaliaToracica = customRadioForm14NuevoUsuarioPolandToracica;
                sindromePoland.SP_AnomaliaMamaria = customRadioForm14NuevoUsuarioPolandMamaria;
                sindromePoland.SP_AnomaliaComplejoPezonAreola = customRadioForm14NuevoUsuarioPolandPezon;
                sindromePoland.SP_T2T3T4 = customRadioForm14NuevoUsuarioPolandT;
                sindromePoland.SP_PCPSIPectusCarinatum = NuevoUsuarioForm14PC;

                var sindromePoland_Id = createSindromePoland(sindromePoland);
                if (sindromePoland_Id != null)
                {
                    listado_TipoPectus.Add(sindromePoland_Id.GetAwaiter().GetResult());
                }
                signosSintomasClinicos.SSC_TipoPectusNombre = "SindromePoland";
                signosSintomasClinicos.SSC_TipoPectus = listado_TipoPectus;
            }

            //Other Form13
            if (customRadioForm9NuevoUsuarioPectus == "Other:")
            {
                pectusMixto.PM_UbicacionDefecto = customRadioForm13NuevoUsuarioMixtoUbicacion;
                pectusMixto.PM_PresionCorreccionPectusCarinatum = NuevoUsuarioForm13PresionCorreccion;
                pectusMixto.PM_RotacionEsternal = customRadioForm13NuevoUsuarioMixtoRotacion;

                var pectusMixto_Id = createPectusMixto(pectusMixto);
                if (pectusMixto_Id != null)
                {
                    listado_TipoPectus.Add(pectusMixto_Id.GetAwaiter().GetResult());
                }
                signosSintomasClinicos.SSC_TipoPectusNombre = "PectusMixto";
                signosSintomasClinicos.SSC_TipoPectus = listado_TipoPectus;
            }

            var signosSintomasClinicos_Id = createSignosSintomasClinicos(signosSintomasClinicos);
            if (signosSintomasClinicos_Id != null)
            {
                listado_SignosSintomasClinicos.Add(signosSintomasClinicos_Id.GetAwaiter().GetResult());
            }

            //ClasificacionPectus Form10
            clasificacionPectus.CP_FormaCostillas = customRadioForm10NuevoUsuarioClasificacionForma;
            clasificacionPectus.CP_Alerones = customRadioForm10NuevoUsuarioClasificacionAlerones;
            clasificacionPectus.CP_Hombros = customRadioForm10NuevoUsuarioClasificacionHombros;
            clasificacionPectus.CP_AntepulsionHombros = customRadioForm10NuevoUsuarioClasificacionAntepulsion;
            clasificacionPectus.CP_AsimetriaPosterior = customRadioForm10NuevoUsuarioClasificacionAsimetria;
            clasificacionPectus.CP_PosicionCifotica = customRadioForm10NuevoUsuarioClasificacionPosicion;
            clasificacionPectus.CP_Estrias = customRadioForm10NuevoUsuarioClasificacionEstrias;
            clasificacionPectus.CP_Donde = NuevoUsuarioForm10Donde;

            if (customRadioForm10NuevoUsuarioClasificacionTratamiento == "Other:")
            {
                clasificacionPectus.CP_Tratamiento = InputForm10NuevoUsuarioClasificacionTratamiento;
            }
            else
            {
                clasificacionPectus.CP_Tratamiento = customRadioForm10NuevoUsuarioClasificacionTratamiento;
            }
            clasificacionPectus.CP_Anotaciones = NuevoUsuarioForm10Anotaciones;

            var clasificacionPectus_Id = createClasificacionPectus(clasificacionPectus);
            if (clasificacionPectus_Id != null)
            {
                listado_ClasificacionPectus.Add(clasificacionPectus_Id.GetAwaiter().GetResult());
            }

            //HistorialClinico Listados ID
            historialClinico.HC_TipoPectus = customRadioForm9NuevoUsuarioPectus;
            historialClinico.CirugiaPrevia = listado_CirugiaPrevia;
            historialClinico.Anamnesis = listado_Anamnesis;
            historialClinico.EnfermdedadPreexistente = listado_EnfermdedadPreexistente;
            historialClinico.Deporte = listado_Deporte;
            historialClinico.SignosSintomasClinicos = listado_SignosSintomasClinicos;
            historialClinico.ClasificacionPectus = listado_ClasificacionPectus;

            //Add HistorialClinico a BBDD
            var historialClinico_Id = createHistorialClinico(historialClinico);

            //Añadir Paciente el Historial Clinico
            List<string> listadoHistorialClinico = new List<string>();
            listadoHistorialClinico.Add(historialClinico_Id.GetAwaiter().GetResult());
            var pacienteId = "";
            if (id_pacienteNuevoUsuarioForm != null)
            {
                pacienteId = createPaciente(NuevoUsuarioForm1Nombre, NuevoUsuarioForm1Apellidos, NuevoUsuarioForm1Correo, NuevoUsuarioForm1Fecha, listadoHistorialClinico, id_pacienteNuevoUsuarioForm).GetAwaiter().GetResult();
            }
            else
            {
                pacienteId = createPaciente(NuevoUsuarioForm1Nombre, NuevoUsuarioForm1Apellidos, NuevoUsuarioForm1Correo, NuevoUsuarioForm1Fecha, listadoHistorialClinico, null).GetAwaiter().GetResult();
            }

            //Control de la respuesta si ha funcionada o no
            bool notificacion = false;
            string notificacionMensaje = string.Empty;

            if (historialClinico_Id != null && pacienteId != "")
            {
                notificacion = true;
                notificacionMensaje = Mensajes.SuccessUsuarioCreado;
            }
            else
            {
                notificacion = false;
                notificacionMensaje = Mensajes.ErrorUsuarioNoCreado;
            }

            //Crear la Carpeta para guardar las imagenes
            crearCarpetaImagenes(pacienteId);

            if (!AlreadyExitCrearPaciente && !AlreadyExitCrearPacienteMensaje && !AlreadyExitCrearPacientePaciente)
            {
                var cacheEntryoptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(2));
                _memoryCache.Set("crearPaciente", notificacion, cacheEntryoptions);
                _memoryCache.Set("crearPacienteMensaje", notificacionMensaje, cacheEntryoptions);
                _memoryCache.Set("crearPacientePaciente", NuevoUsuarioForm1Nombre + " " + NuevoUsuarioForm1Apellidos, cacheEntryoptions);
            }

            return RedirectToAction(Values.Index, Values.NuevoPaciente, new { area = Values.AreaVacio });
        }

        /// <summary>
        /// Creamos una CirugiaPrevia en la BD y devolvemos el id del Object para gaurdarlo en el HistorialClinico
        /// </summary>
        /// <param name="cirugiaPrevia"></param>
        /// <returns></returns>
        private async Task<string> createCirugiaPreviaAsync(CirugiaPrevia cirugiaPrevia)
        {
            try
            {
                var id = await _cirurgiaPreviaServices.Create(cirugiaPrevia);
                return id;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Creamos una EnfermedadPreexistente en la BD y devolvemos el id del Object para gaurdarlo en el HistorialClinico
        /// </summary>
        /// <param name="enfermdedadPreexistente"></param>
        /// <returns></returns>
        private async Task<string> createEnfermedadPreexistente(EnfermdedadPreexistente enfermdedadPreexistente)
        {
            try
            {
                var id = await _enfermedadPreexistenteServices.Create(enfermdedadPreexistente);
                return id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Creamos una DolorPecho en la BD y devolvemos el id del Object para gaurdarlo en el HistorialClinico
        /// </summary>
        /// <param name="dolorPecho"></param>
        /// <returns></returns>
        private async Task<string> createDolorPecho(DolorPecho dolorPecho)
        {
            try
            {
                var id = await _dolorPechoServices.Create(dolorPecho);
                return id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Creamos una Deporte en la BD y devolvemos el id del Object para gaurdarlo en el HistorialClinico
        /// </summary>
        /// <param name="deporte"></param>
        /// <returns></returns>
        private async Task<string> createDeporte(Deporte deporte)
        {
            try
            {
                var id = await _deporteServices.Create(deporte);
                return id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Creamos una Anamnesis en la BD y devolvemos el id del Object para gaurdarlo en el HistorialClinico
        /// </summary>
        /// <param name="anamnesis"></param>
        /// <returns></returns>
        private async Task<string> createAnamnesis(Anamnesis anamnesis)
        {
            try
            {
                var id = await _anamnesisServices.Create(anamnesis);
                return id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Creamos una PectusCarinatum en la BD y devolvemos el id del Object para gaurdarlo en el HistorialClinico
        /// </summary>
        /// <param name="pectusCarinatum"></param>
        /// <returns></returns>
        private async Task<string> createPectusCarinatum(PectusCarinatum pectusCarinatum)
        {
            try
            {
                var id = await _pectusCarinatumServices.Create(pectusCarinatum);
                return id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Creamos una PectusExcavatum en la BD y devolvemos el id del Object para gaurdarlo en el HistorialClinico
        /// </summary>
        /// <param name="pectusExcavatum"></param>
        /// <returns></returns>
        private async Task<string> createPectusExcavatum(PectusExcavatum pectusExcavatum)
        {
            try
            {
                var id = await _pectusExcavatumServices.Create(pectusExcavatum);
                return id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Creamos una PectusMixto en la BD y devolvemos el id del Object para gaurdarlo en el HistorialClinico
        /// </summary>
        /// <param name="pectusMixto"></param>
        /// <returns></returns>
        private async Task<string> createPectusMixto(PectusMixto pectusMixto)
        {
            try
            {
                var id = await _pectusMixtoServices.Create(pectusMixto);
                return id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Creamos una SindromePoland en la BD y devolvemos el id del Object para gaurdarlo en el HistorialClinico
        /// </summary>
        /// <param name="sindromePoland"></param>
        /// <returns></returns>
        private async Task<string> createSindromePoland(SindromePoland sindromePoland)
        {
            try
            {
                var id = await _sindromePolandServices.Create(sindromePoland);
                return id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Creamos una SignosSintomasClinicos en la BD y devolvemos el id del Object para gaurdarlo en el HistorialClinico
        /// </summary>
        /// <param name="signosSintomasClinicos"></param>
        /// <returns></returns>
        private async Task<string> createSignosSintomasClinicos(SignosSintomasClinicos signosSintomasClinicos)
        {
            try
            {
                var id = await _signosSintomasClinicosServices.Create(signosSintomasClinicos);
                return id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Creamos una ClasificacionPectus en la BD y devolvemos el id del Object para gaurdarlo en el HistorialClinico
        /// </summary>
        /// <param name="clasificacionPectus"></param>
        /// <returns></returns>
        private async Task<string> createClasificacionPectus(ClasificacionPectus clasificacionPectus)
        {
            try
            {
                var id = await _clasificacionPectusServices.Create(clasificacionPectus);
                return id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Creamos un Paciente en la BD y devolvemos el id del Object para gaurdarlo en el HistorialClinico
        /// </summary>
        /// <param name="clasificacionPectus"></param>
        /// <returns></returns>
        private async Task<string> createPaciente(string nombre, string apellidos, string correo, DateTime P_PrimeraConsulta, List<string> historialClinicoId, string idPaciente)
        {
            Pacientes pacientes = new Pacientes();
            List<string> listado_id = new List<string>();
            try
            {
                if (idPaciente != null)
                {
                    try
                    {
                        pacientes = await _pacientesServices.GetById(idPaciente);
                        pacientes.HistorialClinicoId.Add(historialClinicoId[0]);

                        pacientes = await _pacientesServices.Update(idPaciente, pacientes);
                    }
                    catch
                    {
                        return null;
                    }
                }
                else
                {
                    pacientes.usuarioId = Session.GetString(Values.Session_usrId);
                    pacientes.P_Nombre = nombre;
                    pacientes.P_Apellidos = apellidos;
                    pacientes.P_PrimeraConsulta = P_PrimeraConsulta;
                    pacientes.HistorialClinicoId = historialClinicoId;
                    pacientes.ControlSistemaCompresor = listado_id;
                    pacientes.ControlCampanaPectusExcavatum = listado_id;
                    pacientes.P_Correo = correo;
                    pacientes.P_Finalizado = false;

                    pacientes = await _pacientesServices.Create(pacientes);
                }

                return pacientes.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Creamos una HistorialClinico en la BD y devolvemos el id del Object para gaurdarlo en el HistorialClinico
        /// </summary>
        /// <param name="historialClinico"></param>
        /// <returns></returns>
        private async Task<string> createHistorialClinico(HistorialClinico historialClinico)
        {
            try
            {
                var id = await _historialClinicoServices.Create(historialClinico);
                return id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Llamamos el metodo grupoCheckboxToArray con solo 2 parametros
        /// </summary>
        /// <param name="checkboxForm1"></param>
        /// <param name="checkboxForm2"></param>
        /// <param name="count"></param>
        /// <param name="count2"></param>
        /// <returns></returns>
        private string[,] grupoCheckboxToArray(string[] checkboxForm1, string[] checkboxForm2, int count, int count2)
        {
            return this.grupoCheckboxToArray(checkboxForm1, checkboxForm2, null, null, null, null, null, null, count, count2);
        }

        /// <summary>
        /// Llamamos el metodo grupoCheckboxToArray con solo 4 parametros
        /// </summary>
        /// <param name="checkboxForm1"></param>
        /// <param name="checkboxForm2"></param>
        /// <param name="count"></param>
        /// <param name="count2"></param>
        /// <returns></returns>
        private string[,] grupoCheckboxToArray(string[] checkboxForm1, string[] checkboxForm2, string[] checkboxForm3, string[] checkboxForm4, int count, int count2)
        {
            return this.grupoCheckboxToArray(checkboxForm1, checkboxForm2, checkboxForm3, checkboxForm4, null, null, null, null, count, count2);
        }


        /// <summary>
        /// Obtener los valores del checkbox del formulario y ponerlos el model HistorialClinico
        /// </summary>
        /// <param name="checkboxForm1"></param>
        /// <param name="checkboxForm2"></param>
        /// <param name="checkboxForm3"></param>
        /// <param name="checkboxForm4"></param>
        /// <returns></returns>
        private string[,] grupoCheckboxToArray(string[] checkboxForm1, string[] checkboxForm2,
            string[] checkboxForm3, string[] checkboxForm4, string[] checkboxForm5, string[] checkboxForm6, string[] checkboxForm7, string[] checkboxForm8, int count, int count2)
        {
            string[,] array = new string[count, count2];

            if (checkboxForm1 != null)
            {
                for (int e = 0; e < checkboxForm1.Length; e++)
                {
                    array[0, e] = checkboxForm1[e];
                }
            }
            if (checkboxForm2 != null)
            {
                for (int e = 0; e < checkboxForm2.Length; e++)
                {
                    array[1, e] = checkboxForm2[e];
                }
            }
            if (checkboxForm3 != null)
            {
                for (int e = 0; e < checkboxForm3.Length; e++)
                {
                    array[2, e] = checkboxForm3[e];
                }
            }
            if (checkboxForm4 != null)
            {
                for (int e = 0; e < checkboxForm4.Length; e++)
                {
                    array[3, e] = checkboxForm4[e];
                }
            }
            if (checkboxForm5 != null)
            {
                for (int e = 0; e < checkboxForm5.Length; e++)
                {
                    array[4, e] = checkboxForm5[e];
                }
            }
            if (checkboxForm6 != null)
            {
                for (int e = 0; e < checkboxForm6.Length; e++)
                {
                    array[5, e] = checkboxForm6[e];
                }
            }
            if (checkboxForm7 != null)
            {
                for (int e = 0; e < checkboxForm7.Length; e++)
                {
                    array[6, e] = checkboxForm7[e];
                }
            }
            if (checkboxForm8 != null)
            {
                for (int e = 0; e < checkboxForm8.Length; e++)
                {
                    array[7, e] = checkboxForm8[e];
                }
            }

            return array;
        }

        /// <summary>
        /// Crear la carpeta en el path correspondiente
        /// </summary>
        /// <param name="idHistorialClino">Nombre de la carpeta</param>
        private void crearCarpetaImagenes(string pacienteId)
        {
            bool isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
            string path = String.Empty;
            string path2 = String.Empty;

            if (isWindows)
            {
                path = Path.Combine("C:\\ppt-huav", "Pacientes");
                path2 = Path.Combine(path, pacienteId);
            }
            else
            {
                path2 = "/ppt-huav/Pacientes/" + pacienteId;
            }

            if (!Directory.Exists(path2))
            {
                Directory.CreateDirectory(path2);
            }
        }
    }
}