using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using TFG_Web.Areas.Clinics.Interface.Services;
using TFG_Web.Areas.Clinics.Interface.Services.IHistorialClinicoServices;
using TFG_Web.Areas.Clinics.Models;
using TFG_Web.Areas.Clinics.Services;
using TFG_Web.Models;
using TFG_Web.Models.HistorialClinicoCollections;
using TFG_Web.Services;

namespace TFG_Web.Areas.Clinics.Controllers
{
    [Area(Values.AreaClinics)]
    [ViewLayout(Values.LayoutClinics)]
    public class PerfilPacienteController : Controller
    {
        #region Dependencias Interfaces
        private ISession Session => HttpContext.Session;
        private readonly ValidarAcceso _validarAcceso = new ValidarAcceso();
        private readonly IMemoryCache _memoryCache;
        private readonly ViewModel mymodel = new ViewModel();
        private readonly UsuariosServices usuariosServices = new UsuariosServices();
        private readonly IGetPacientesServices _getPacientesServices;
        private readonly IHistorialClinicoServices _historialClinicoServices;
        private readonly ISignosSintomasClinicosServices _signosSintomasClinicosServices;
        private readonly IPacientesServices _pacientesServices;
        private readonly IControlSistemaCompresorServices _controlSistemaCompresorServices;
        private readonly IControlCampanaPectusExcavatumServices _controlCampanaPectusExcavatumServices;
        private readonly ICirugiaPreviaServices _cirugiaPreviaServices;
        private readonly IAnamnesisServices _anamnesisServices;
        private readonly IClasificacionPectusServices _clasificacionPectusServices;
        private readonly IDeporteServices _deporteServices;
        private readonly IDolorPechoServices _dolorPechoServices;
        private readonly IEnfermdedadPreexistenteServices _enfermedadPreexistenteServices;
        private readonly IPectusCarinatumServices _pectusCarinatumServices;
        private readonly IPectusExcavatumServices _pectusExavatumServices;
        private readonly IPectusMixtoServices _pectusMixtoServices;
        private readonly ISindromePolandServices _sindromePolandServices;
        private readonly IWebHostEnvironment _environment;
        private HistorialClinico historial = new HistorialClinico();
        private HistorialClinico historial2 = new HistorialClinico();
        #endregion

        #region Constructor
        public PerfilPacienteController(IGetPacientesServices getPacientesServices, IHistorialClinicoServices historialClinicoServices,
            ISignosSintomasClinicosServices signosSintomasClinicosServices, IControlSistemaCompresorServices controlSistemaCompresorServices,
            IPacientesServices pacientesServices, ICirugiaPreviaServices cirugiaPreviaServices, IAnamnesisServices anamnesisServices,
            IClasificacionPectusServices clasificacionPectusServices, IDeporteServices deporteServices, IDolorPechoServices dolorPechoServices,
            IEnfermdedadPreexistenteServices enfermedadPreexistenteServices, IPectusCarinatumServices pectusCarinatumServices,
            IPectusExcavatumServices pectusExavatumServices, IPectusMixtoServices pectusMixtoServices, ISindromePolandServices sindromePolandServices,
            IControlCampanaPectusExcavatumServices controlCampanaPectusExcavatumServices, IMemoryCache _memoryCache, IWebHostEnvironment environment)
        {
            this._memoryCache = _memoryCache;
            this._getPacientesServices = getPacientesServices;
            this._historialClinicoServices = historialClinicoServices;
            this._controlSistemaCompresorServices = controlSistemaCompresorServices;
            this._controlCampanaPectusExcavatumServices = controlCampanaPectusExcavatumServices;
            this._pacientesServices = pacientesServices;
            this._cirugiaPreviaServices = cirugiaPreviaServices;
            this._anamnesisServices = anamnesisServices;
            this._clasificacionPectusServices = clasificacionPectusServices;
            this._deporteServices = deporteServices;
            this._dolorPechoServices = dolorPechoServices;
            this._enfermedadPreexistenteServices = enfermedadPreexistenteServices;
            this._pectusCarinatumServices = pectusCarinatumServices;
            this._pectusExavatumServices = pectusExavatumServices;
            this._signosSintomasClinicosServices = signosSintomasClinicosServices;
            this._pectusMixtoServices = pectusMixtoServices;
            this._sindromePolandServices = sindromePolandServices;
            this._environment = environment;
        }
        #endregion

        /// <summary>
        /// Mostrar el perfil del paciente
        /// </summary>
        /// <param name="id">Id del Paciente</param>
        /// <returns></returns>
        [Route(Values.PerfilPaciente)]
        public async Task<IActionResult> Index(string id)
        {
            if (!_validarAcceso.ValidarAccesoWeb(Session)) return RedirectToAction(Values.Index, Values.LoginUsuario, new { area = Values.AreaVacio });

            if (id != null)
            {
                ViewModel mymodel = new ViewModel();
                mymodel = getCacheNotificacionCrearPaciente();

                try
                {
                    mymodel.Usuarios = usuariosServices.GetById(Session.GetString(Values.Session_usrId)).GetAwaiter().GetResult();
                }
                catch (Exception ex)
                {
                    mymodel.Usuarios = null;
                }

                try
                {
                    mymodel.Pacientes = await _pacientesServices.GetById(id);

                    //Comprobamos que el paciente que queremos mostrar es el del usuario que ha iniciado sesion (Session)
                    if (mymodel.Pacientes.usuarioId != Session.GetString(Values.Session_usrId))
                    {
                        return RedirectToAction(Values.Index, Values.Inicio, new { area = Values.AreaVacio });
                    }

                    mymodel.ListPacientes = await _getPacientesServices.ListPacientesAsync(Session.GetString(Values.Session_usrId));

                    try
                    {
                        //Comprobamos cuantos historiales de cliente tiene este paciente
                        if (mymodel.Pacientes.HistorialClinicoId.Count() > 1)
                        {
                            string idHistorial = mymodel.Pacientes.HistorialClinicoId.First();
                            string idHistorial2 = mymodel.Pacientes.HistorialClinicoId[1];
                            this.historial = await this._historialClinicoServices.GetById(idHistorial);
                            mymodel.HistorialClinico = historial;
                            this.historial2 = await this._historialClinicoServices.GetById(idHistorial2);
                            mymodel.HistorialClinico2 = historial2;
                        }
                        else
                        {
                            string idHistorial = mymodel.Pacientes.HistorialClinicoId.First();
                            this.historial = await this._historialClinicoServices.GetById(idHistorial);
                            mymodel.HistorialClinico = historial;
                        }
                    }
                    catch (Exception ex)
                    {
                        return RedirectToAction(Values.Index, Values.Inicio, new { area = Values.AreaVacio });
                    }
                }
                catch (Exception ex)
                {
                    return RedirectToAction(Values.Index, Values.Inicio, new { area = Values.AreaVacio });
                }

                //Cogemos las Collections del HistorialClinico
                try
                {
                    //Comprobar si puede tener dos historiales
                    if (historial.HC_TipoPectus != null)
                    {
                        if (historial.HC_TipoPectus == Values.PectusMixto || historial.HC_TipoPectus == Values.SindromePoland)
                        {
                            historial.HC_TipoPectus = "1";
                        }
                        else
                        {
                            historial.HC_TipoPectus = "0";
                        }
                    }

                    //Cirugia Previa
                    #region Cirugia Previa
                    List<CirugiaPrevia> listCirugiaPrevias = new List<CirugiaPrevia>();

                    if (historial.CirugiaPrevia != null)
                    {
                        //Historial 1 - CirugiaPrevia
                        if (historial.CirugiaPrevia.Count > 0)
                        {
                            try
                            {
                                var cirugia = await getCirugiaPacienteIdAsync(historial.CirugiaPrevia.First());
                                listCirugiaPrevias.Add(cirugia);
                            }
                            catch
                            {
                                listCirugiaPrevias.Add(null);
                            }
                        }
                        else
                        {
                            listCirugiaPrevias.Add(null);
                        }

                        //Historial 2 - CirugiaPrevia
                        if (historial2.CirugiaPrevia != null && historial2.CirugiaPrevia.Count > 0)
                        {
                            try
                            {
                                var cirugia = await getCirugiaPacienteIdAsync(historial2.CirugiaPrevia.First());
                                listCirugiaPrevias.Add(cirugia);
                            }
                            catch
                            {
                                listCirugiaPrevias.Add(null);
                            }
                        }
                        else
                        {
                            listCirugiaPrevias.Add(null);
                        }

                        mymodel.CirugiaPrevia = listCirugiaPrevias;
                    }
                    else
                    {
                        listCirugiaPrevias.Add(null);
                        listCirugiaPrevias.Add(null);
                        mymodel.CirugiaPrevia = listCirugiaPrevias;
                    }
                    #endregion

                    //Anamesis
                    #region Anamesis
                    List<Anamnesis> listAnamnesis = new List<Anamnesis>();

                    if (historial.Anamnesis != null)
                    {
                        //Historial 1 - Anamnesis
                        if (historial.Anamnesis.Count > 0)
                        {
                            try
                            {
                                var anamnesis = await getAnamnesisPacienteIdAsync(historial.Anamnesis.First());
                                listAnamnesis.Add(anamnesis);
                            }
                            catch
                            {
                                listAnamnesis.Add(null);
                            }
                        }
                        else
                        {
                            listAnamnesis.Add(null);
                        }

                        //Historial 2 - Anamnesis
                        if (historial2.Anamnesis != null && historial2.Anamnesis.Count > 0)
                        {
                            try
                            {
                                var anamnesis = await getAnamnesisPacienteIdAsync(historial2.Anamnesis.First());
                                listAnamnesis.Add(anamnesis);
                            }
                            catch
                            {
                                listAnamnesis.Add(null);
                            }
                        }
                        else
                        {
                            listAnamnesis.Add(null);
                        }

                        mymodel.Anamnesis = listAnamnesis;
                    }
                    else
                    {
                        listAnamnesis.Add(null);
                        listAnamnesis.Add(null);
                        mymodel.Anamnesis = listAnamnesis;
                    }
                    #endregion

                    //EnfermdedadPreexistente
                    #region EnfermdedadPreexistente
                    List<EnfermdedadPreexistente> listEnfermedadPreexistente = new List<EnfermdedadPreexistente>();

                    if (historial.EnfermdedadPreexistente != null)
                    {
                        if (historial.EnfermdedadPreexistente.Count > 0)
                        {
                            try
                            {
                                var enfermdedadPreexistente = await getEnfermdedadPreexistentePacienteIdAsync(historial.EnfermdedadPreexistente.First());
                                listEnfermedadPreexistente.Add(enfermdedadPreexistente);
                            }
                            catch
                            {
                                listEnfermedadPreexistente.Add(null);
                            }
                        }
                        else
                        {
                            listEnfermedadPreexistente.Add(null);
                        }

                        //Historial 2 - EnfermdedadPreexistente
                        if (historial2.EnfermdedadPreexistente != null && historial2.EnfermdedadPreexistente.Count > 0)
                        {
                            try
                            {
                                var enfermdedadPreexistente = await getEnfermdedadPreexistentePacienteIdAsync(historial2.EnfermdedadPreexistente.First());
                                listEnfermedadPreexistente.Add(enfermdedadPreexistente);
                            }
                            catch
                            {
                                listEnfermedadPreexistente.Add(null);
                            }
                        }
                        else
                        {
                            listEnfermedadPreexistente.Add(null);
                        }

                        mymodel.EnfermdedadPreexistente = listEnfermedadPreexistente;
                    }
                    else
                    {
                        listEnfermedadPreexistente.Add(null);
                        listEnfermedadPreexistente.Add(null);
                        mymodel.EnfermdedadPreexistente = listEnfermedadPreexistente;
                    }
                    #endregion

                    //Deporte
                    #region Deporte
                    List<Deporte> listDeporte = new List<Deporte>();

                    if (historial.Deporte != null)
                    {
                        if (historial.Deporte.Count > 0)
                        {
                            try
                            {
                                var deporte = await getDeportePacienteIdAsync(historial.Deporte.First());
                                listDeporte.Add(deporte);
                            }
                            catch (Exception ex)
                            {
                                listDeporte.Add(null);
                            }
                        }
                        else
                        {
                            listDeporte.Add(null);
                        }

                        //Historial 2 - Deporte
                        if (historial2.Deporte != null && historial2.Deporte.Count > 0)
                        {
                            try
                            {
                                var deporte = await getDeportePacienteIdAsync(historial2.Deporte.First());
                                listDeporte.Add(deporte);
                            }
                            catch
                            {
                                listDeporte.Add(null);
                            }
                        }
                        else
                        {
                            listDeporte.Add(null);
                        }

                        mymodel.Deporte = listDeporte;
                    }
                    else
                    {
                        listDeporte.Add(null);
                        listDeporte.Add(null);
                        mymodel.Deporte = listDeporte;
                    }
                    #endregion

                    //SignosSintomasClinicos
                    #region SignosSintomasClinicos
                    List<SignosSintomasClinicos> listSignosSintomasClinicos = new List<SignosSintomasClinicos>();

                    if (historial.SignosSintomasClinicos != null)
                    {
                        if (historial.SignosSintomasClinicos.Count > 0)
                        {
                            try
                            {
                                var signosSintomasClinicos = await getSignosSintomasClinicosPacienteIdAsync(historial.SignosSintomasClinicos.First());
                                listSignosSintomasClinicos.Add(signosSintomasClinicos);

                                try
                                {
                                    if(listSignosSintomasClinicos[0] != null)
                                    {
                                        if (listSignosSintomasClinicos[0].SSC_TipoPectusNombre == "PectusCarinatum")
                                        {
                                            mymodel.PectusCarinatum = new List<PectusCarinatum>();
                                            mymodel.PectusCarinatum.Add(await _pectusCarinatumServices.GetById(listSignosSintomasClinicos[0].SSC_TipoPectus.First()));
                                        }
                                        else if (listSignosSintomasClinicos[0].SSC_TipoPectusNombre == "PectusExcavatum")
                                        {
                                            mymodel.PectusExcavatum = new List<PectusExcavatum>();
                                            mymodel.PectusExcavatum.Add(await _pectusExavatumServices.GetById(listSignosSintomasClinicos[0].SSC_TipoPectus.First()));
                                        }
                                        else if (listSignosSintomasClinicos[0].SSC_TipoPectusNombre == "PectusMixto")
                                        {
                                            mymodel.PectusMixto = new List<PectusMixto>();
                                            mymodel.PectusMixto.Add(await _pectusMixtoServices.GetById(listSignosSintomasClinicos[0].SSC_TipoPectus.First()));
                                        }
                                        else if (listSignosSintomasClinicos[0].SSC_TipoPectusNombre == "SindromePoland")
                                        {
                                            mymodel.SindromePoland = new List<SindromePoland>();
                                            mymodel.SindromePoland.Add(await _sindromePolandServices.GetById(listSignosSintomasClinicos[0].SSC_TipoPectus.First()));
                                        }
                                        else if (listSignosSintomasClinicos[0].SSC_TipoPectusNombre == "PectusMixto")
                                        {
                                            mymodel.PectusMixto = new List<PectusMixto>();
                                            mymodel.PectusMixto.Add(await _pectusMixtoServices.GetById(listSignosSintomasClinicos[0].SSC_TipoPectus.First()));
                                        }
                                    }  
                                }
                                catch
                                {
                                    mymodel.PectusCarinatum.Add(null);
                                    mymodel.PectusExcavatum.Add(null);
                                    mymodel.PectusMixto.Add(null);
                                    mymodel.SindromePoland.Add(null);
                                }
                            }
                            catch
                            {
                                listSignosSintomasClinicos.Add(null);
                            }
                        }
                        else
                        {
                            listSignosSintomasClinicos.Add(null);
                        }

                        //Historial 2
                        if (historial2.SignosSintomasClinicos != null)
                        {
                            if (historial2.SignosSintomasClinicos.Count > 0)
                            {
                                try
                                {
                                    var signosSintomasClinicos = await getSignosSintomasClinicosPacienteIdAsync(historial2.SignosSintomasClinicos.First());
                                    listSignosSintomasClinicos.Add(signosSintomasClinicos);

                                    try
                                    {
                                        if (listSignosSintomasClinicos[1].SSC_TipoPectusNombre == "PectusCarinatum")
                                        {
                                            mymodel.PectusCarinatum.Add(await _pectusCarinatumServices.GetById(listSignosSintomasClinicos[1].SSC_TipoPectus.First()));
                                        }
                                        else if (listSignosSintomasClinicos[1].SSC_TipoPectusNombre == "PectusExcavatum")
                                        {
                                            mymodel.PectusExcavatum.Add(await _pectusExavatumServices.GetById(listSignosSintomasClinicos[1].SSC_TipoPectus.First()));
                                        }
                                        else if (listSignosSintomasClinicos[1].SSC_TipoPectusNombre == "PectusMixto")
                                        {
                                            mymodel.PectusMixto.Add(await _pectusMixtoServices.GetById(listSignosSintomasClinicos[1].SSC_TipoPectus.First()));
                                        }
                                        else if (listSignosSintomasClinicos[1].SSC_TipoPectusNombre == "SindromePoland")
                                        {
                                            mymodel.SindromePoland.Add(await _sindromePolandServices.GetById(listSignosSintomasClinicos[1].SSC_TipoPectus.First()));
                                        }
                                        else if (listSignosSintomasClinicos[1].SSC_TipoPectusNombre == "PectusMixto")
                                        {
                                            mymodel.PectusMixto.Add(await _pectusMixtoServices.GetById(listSignosSintomasClinicos[1].SSC_TipoPectus.First()));
                                        }
                                    }
                                    catch
                                    {
                                        mymodel.PectusCarinatum.Add(null);
                                        mymodel.PectusExcavatum.Add(null);
                                        mymodel.PectusMixto.Add(null);
                                        mymodel.SindromePoland.Add(null);
                                    }
                                }
                                catch
                                {
                                    listSignosSintomasClinicos.Add(null);
                                }
                            }
                            else
                            {
                                listSignosSintomasClinicos.Add(null);
                            }
                        }
                        else
                        {
                            listSignosSintomasClinicos.Add(null);
                        }

                        mymodel.SignosSintomasClinicos = listSignosSintomasClinicos;
                    }
                    else
                    {
                        listSignosSintomasClinicos.Add(null);
                        listSignosSintomasClinicos.Add(null);
                        mymodel.SignosSintomasClinicos = listSignosSintomasClinicos;
                    }
                    #endregion

                    //
                    try
                    {
                        var control = await _controlSistemaCompresorServices.GetGroupById(mymodel.Pacientes.ControlSistemaCompresor.ToArray());
                        mymodel.ListSistemaCompresor = control;
                    }
                    catch
                    {
                        mymodel.ListSistemaCompresor = null;
                    }

                    try
                    {
                        var control = await _controlCampanaPectusExcavatumServices.GetGroupById(mymodel.Pacientes.ControlCampanaPectusExcavatum.ToArray());
                        mymodel.ListCampanaPectusExcavatum = control;
                    }
                    catch
                    {
                        mymodel.ListCampanaPectusExcavatum = null;
                    }

                    //Cargar los nombres de las imagenes de la Carptea del Historial Clinico
                    try
                    {
                        ViewModel view = new ViewModel();
                        ObtenerImagenesPaciente(mymodel, id, out view);

                        mymodel = view;
                    }
                    catch
                    {
                        List<string> ImageList = new List<string>();
                        mymodel.ImageList = ImageList;
                    }

                    return View(Values.Index, mymodel);
                }
                catch
                {
                    return RedirectToAction(Values.Index, Values.Inicio, new { area = Values.AreaVacio });
                }
            }
            return RedirectToAction(Values.Index, Values.Inicio, new { area = Values.AreaVacio });
        }

        private ViewModel getCacheNotificacionCrearPaciente()
        {
            ViewModel mymodel = new ViewModel();
            var resultenviarCorreoPaciente = _memoryCache.Get("enviarCorreoPaciente");
            var result_enviarCorreoPacienteMensaje = _memoryCache.Get("enviarCorreoPacienteMensaje");

            if (resultenviarCorreoPaciente != null)
            {
                mymodel.Notificaciones = bool.Parse(resultenviarCorreoPaciente.ToString());
                mymodel.NotificacionesControl = true;
            }
            if (result_enviarCorreoPacienteMensaje != null)
            {
                mymodel.NotificacionesMensaje = result_enviarCorreoPacienteMensaje.ToString();
            }

            return mymodel;
        }

        /// <summary>
        /// Eliminamos el Paciente y el Historial Clinico del paciente
        /// </summary>
        /// <param name="id">Paciente Id</param> 
        [Route("EliminarPaciente")]
        public async Task<IActionResult> eliminarPacienteHistorialAsync(string id)
        {
            if (!_validarAcceso.ValidarAccesoWeb(Session)) return RedirectToAction(Values.Index, Values.LoginUsuario, new { area = Values.AreaVacio });

            bool _eliminarPaciente = false;
            string _eliminarPaciente_Mensaje = string.Empty;
            string _eliminarPaciente_Paciente = string.Empty;

            //Comprobar que hay valor en el Id
            if (id != null)
            {
                //Get Pacientes y Historials Clinicos
                try
                {
                    var paciente = await _pacientesServices.GetById(id);
                    if (paciente.HistorialClinicoId.Count > 1)
                    {
                        this.historial = await _historialClinicoServices.GetById(paciente.HistorialClinicoId.First());
                        this.historial2 = await _historialClinicoServices.GetById(paciente.HistorialClinicoId.First());
                    }
                    else
                    {
                        this.historial = await _historialClinicoServices.GetById(paciente.HistorialClinicoId.First());
                    }

                    //Delete Cirugia Previa
                    try
                    {
                        if (paciente.HistorialClinicoId.Count > 1)
                        {
                            await _cirugiaPreviaServices.Delete(historial.CirugiaPrevia.First());
                            await _cirugiaPreviaServices.Delete(historial2.CirugiaPrevia.First());
                        }
                        else
                        {
                            await _cirugiaPreviaServices.Delete(historial.CirugiaPrevia.First());
                        }

                    }
                    catch (Exception ex)
                    {
                        _eliminarPaciente_Mensaje = Mensajes.ErrorEliminarPaciente;
                        _eliminarPaciente = false;
                        Console.WriteLine(ex.Message);
                    }

                    //Delete DolorPecho
                    try
                    {
                        var result = await _anamnesisServices.GetById(historial.Anamnesis.First());
                        if(result != null)
                        {
                            if (paciente.HistorialClinicoId.Count > 1)
                            {
                                await _dolorPechoServices.Delete(result.DolorPecho.First());
                                await _dolorPechoServices.Delete(result.DolorPecho.First());
                            }
                            else
                            {
                                await _dolorPechoServices.Delete(result.DolorPecho.First());
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _eliminarPaciente_Mensaje = Mensajes.ErrorEliminarPaciente;
                        _eliminarPaciente = false;
                        Console.WriteLine(ex.Message);
                    }

                    //Delete Anamnesis
                    try
                    {
                        if (paciente.HistorialClinicoId.Count > 1)
                        {
                            await _anamnesisServices.Delete(historial.Anamnesis.First());
                            await _anamnesisServices.Delete(historial2.Anamnesis.First());
                        }
                        else
                        {
                            await _anamnesisServices.Delete(historial.Anamnesis.First());
                        }
                    }
                    catch (Exception ex)
                    {
                        _eliminarPaciente_Mensaje = Mensajes.ErrorEliminarPaciente;
                        _eliminarPaciente = false;
                        Console.WriteLine(ex.Message);
                    }

                    //Delete EnfermedadPreexistente
                    try
                    {
                        if (paciente.HistorialClinicoId.Count > 1)
                        {
                            await _enfermedadPreexistenteServices.Delete(historial.EnfermdedadPreexistente.First());
                            await _enfermedadPreexistenteServices.Delete(historial2.EnfermdedadPreexistente.First());
                        }
                        else
                        {
                            await _enfermedadPreexistenteServices.Delete(historial.EnfermdedadPreexistente.First());
                        }
                    }
                    catch (Exception ex)
                    {
                        _eliminarPaciente_Mensaje = Mensajes.ErrorEliminarPaciente;
                        _eliminarPaciente = false;
                        Console.WriteLine(ex.Message);
                    }

                    //Delete Deporte
                    try
                    {
                        if (paciente.HistorialClinicoId.Count > 1)
                        {
                            await _deporteServices.Delete(historial.Deporte.First());
                            await _deporteServices.Delete(historial2.Deporte.First());
                        }
                        else
                        {
                            await _deporteServices.Delete(historial.Deporte.First());
                        }
                    }
                    catch (Exception ex)
                    {
                        _eliminarPaciente_Mensaje = Mensajes.ErrorEliminarPaciente;
                        _eliminarPaciente = false;
                        Console.WriteLine(ex.Message);
                    }

                    //Delete ClasificacionPectus
                    try
                    {
                        if (paciente.HistorialClinicoId.Count > 1)
                        {
                            await _clasificacionPectusServices.Delete(historial.ClasificacionPectus.First());
                            await _clasificacionPectusServices.Delete(historial2.ClasificacionPectus.First());
                        }
                        else
                        {
                            await _clasificacionPectusServices.Delete(historial.ClasificacionPectus.First());
                        }
                    }
                    catch (Exception ex)
                    {
                        _eliminarPaciente_Mensaje = Mensajes.ErrorEliminarPaciente;
                        _eliminarPaciente = false;
                        Console.WriteLine(ex.Message);
                    }

                    //Delete TipoPectus
                    try
                    {
                        var signsoSintomasClinicos = await _signosSintomasClinicosServices.GetById(historial.SignosSintomasClinicos.First());

                        if(signsoSintomasClinicos != null)
                        {
                            var tipoPectusId = signsoSintomasClinicos.SSC_TipoPectus.First();

                            if (signsoSintomasClinicos.SSC_TipoPectusNombre == "PectusCarinatum")
                            {
                                await _pectusCarinatumServices.Delete(tipoPectusId);
                            }
                            else if (signsoSintomasClinicos.SSC_TipoPectusNombre == "PectusExcavatum")
                            {
                                await _pectusExavatumServices.Delete(tipoPectusId);
                            }
                            else if (signsoSintomasClinicos.SSC_TipoPectusNombre == "PectusMixto")
                            {
                                await _pectusMixtoServices.Delete(tipoPectusId);
                            }
                            else if (signsoSintomasClinicos.SSC_TipoPectusNombre == "SindromePoland")
                            {
                                await _sindromePolandServices.Delete(tipoPectusId);
                            }
                            else if (signsoSintomasClinicos.SSC_TipoPectusNombre == "PectusMixto")
                            {
                                await _pectusMixtoServices.Delete(tipoPectusId);
                            }
                        }  
                    }
                    catch (Exception ex)
                    {
                        _eliminarPaciente_Mensaje = Mensajes.ErrorEliminarPaciente;
                        _eliminarPaciente = false;
                        Console.WriteLine(ex.Message);
                    }

                    //Delete SignosSintomasClinicos
                    try
                    {
                        if (paciente.HistorialClinicoId.Count > 1)
                        {
                            await _signosSintomasClinicosServices.Delete(historial.SignosSintomasClinicos.First());
                            await _signosSintomasClinicosServices.Delete(historial2.SignosSintomasClinicos.First());
                        }
                        else
                        {
                            await _signosSintomasClinicosServices.Delete(historial.SignosSintomasClinicos.First());
                        }
                    }
                    catch (Exception ex)
                    {
                        _eliminarPaciente_Mensaje = Mensajes.ErrorEliminarPaciente;
                        _eliminarPaciente = false;
                        Console.WriteLine(ex.Message);
                    }

                    //Delete Historial Cliniso
                    try
                    {
                        if (paciente.HistorialClinicoId.Count > 1)
                        {
                            await _historialClinicoServices.Delete(paciente.HistorialClinicoId.First());
                            await _historialClinicoServices.Delete(paciente.HistorialClinicoId[0]);
                        }
                        else
                        {
                            await _historialClinicoServices.Delete(paciente.HistorialClinicoId.First());
                        }
                        _eliminarPaciente_Paciente = historial.HC_Nombre + " " + historial.HC_Apellidos;
                        _eliminarPaciente_Mensaje = Mensajes.SuccessEliminarPaciente;
                        _eliminarPaciente = true;
                    }
                    catch (Exception ex)
                    {
                        _eliminarPaciente_Mensaje = Mensajes.ErrorEliminarPaciente;
                        _eliminarPaciente = false;
                        Console.WriteLine(ex.Message);
                    }

                    //Delete Paciente
                    try
                    {
                        await _pacientesServices.Delete(id);
                        _eliminarPaciente_Paciente = historial.HC_Nombre + " " + historial.HC_Apellidos;
                        _eliminarPaciente_Mensaje = Mensajes.SuccessEliminarPaciente;
                        _eliminarPaciente = true;
                    }
                    catch (Exception ex)
                    {
                        _eliminarPaciente_Mensaje = Mensajes.ErrorEliminarPaciente;
                        _eliminarPaciente = false;
                        Console.WriteLine(ex.Message);
                    }

                    try
                    {
                        //TODO: Historial 1 y 2
                        string path = Path.Combine(this._environment.WebRootPath, "img\\Pacientes");
                        string path2 = Path.Combine(path, historial.Id);

                        if (System.IO.Directory.Exists(path2))
                        {
                            System.IO.Directory.Delete(path2);
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
                catch (Exception ex)
                {
                    _eliminarPaciente_Mensaje = Mensajes.ErrorEliminarPaciente;
                    _eliminarPaciente = false;
                    Console.WriteLine(ex.Message);
                }
            }

            //Cache - Notificacions eliminacion
            bool AlreadyExitPacienteEliminar = _memoryCache.TryGetValue("pacienteEliminado", out string respuesta);
            bool AlreadyExitPacienteEliminarMensaje = _memoryCache.TryGetValue("pacienteEliminadoMensaje", out string respuesta2);
            bool AlreadyExitPacienteEliminarPaciente = _memoryCache.TryGetValue("pacienteEliminadoPaciente", out string respuesta3);

            if (!AlreadyExitPacienteEliminar && !AlreadyExitPacienteEliminarMensaje && !AlreadyExitPacienteEliminarPaciente)
            {
                var cacheEntryoptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(2));
                _memoryCache.Set("pacienteEliminado", _eliminarPaciente.ToString(), cacheEntryoptions);
                _memoryCache.Set("pacienteEliminadoMensaje", _eliminarPaciente_Mensaje, cacheEntryoptions);
                _memoryCache.Set("pacienteEliminadoPaciente", _eliminarPaciente_Paciente, cacheEntryoptions);
            }

            return RedirectToAction(Values.Index, Values.Inicio, new { area = Values.AreaVacio });
        }

        /// <summary>
        /// Guardamos los nuevos valores del Form1 Historial Clinico - Editar
        /// </summary>
        /// <param name="idPaciente"></param>
        /// <param name="form1PerfilPacienteId"></param>
        /// <param name="form1PerfilPacienteNombre"></param>
        /// <param name="form1PerfilPacienteApellido"></param>
        /// <param name="form1PerfilPacienteFechaPrimeraConsulta"></param>
        /// <param name="form1PerfilPacienteEdad"></param>
        /// <param name="form1PerfilPacienteTipoEdad"></param>
        /// <param name="form1PerfilPacienteGenero"></param>
        /// <param name="form1PerfilPacienteMotivoConsulta"></param>
        /// <param name="form1PerfilPacienteNotaronDeformidad"></param>
        /// <param name="form1PerfilPacienteEmpeorado"></param>
        /// <param name="form1PerfilPacienteCuando"></param>
        /// <param name="form1PerfilPacienteHermanosGemelos"></param>
        /// <param name="form1PerfilPacienteOtrosFamiliares"></param>
        /// <param name="form1PerfilPacienteAntecedentesFamilia1"></param>
        /// <param name="form1PerfilPacienteAntecedentesFamilia2"></param>
        /// <param name="form1PerfilPacienteAntecedentesFamilia3"></param>
        /// <param name="form1PerfilPacienteAntecedentesFamilia4"></param>
        /// <returns></returns>
        public async Task<IActionResult> form1PerfilPacientesGuardarInputs(string idPaciente, string form1PerfilPacienteId, string form1PerfilPacienteNombre,
            string form1PerfilPacienteApellido, DateTime form1PerfilPacienteFechaPrimeraConsulta, DateTime form1PerfilPacienteEdad,
            string form1PerfilPacienteTipoEdad, string form1PerfilPacienteGenero, string form1PerfilPacienteMotivoConsulta,
            string form1PerfilPacienteNotaronDeformidad, string form1PerfilPacienteEmpeorado, string form1PerfilPacienteCuando,
            string form1PerfilPacienteHermanosGemelos, string form1PerfilPacienteOtrosFamiliares, string[] form1PerfilPacienteAntecedentesFamilia1,
            string[] form1PerfilPacienteAntecedentesFamilia2, string[] form1PerfilPacienteAntecedentesFamilia3, string[] form1PerfilPacienteAntecedentesFamilia4)
        {
            if (!_validarAcceso.ValidarAccesoWeb(Session)) return RedirectToAction(Values.Index, Values.LoginUsuario, new { area = Values.AreaVacio });

            if (form1PerfilPacienteId != null)
            {
                HistorialClinico historialClinico = new HistorialClinico();
                try
                {
                    historialClinico = await _historialClinicoServices.GetById(form1PerfilPacienteId);

                }
                catch (Exception ex)
                {
                    return RedirectToAction(Values.Index, Values.PerfilPaciente, new { area = Values.AreaVacio });
                }

                historialClinico.Id = form1PerfilPacienteId;
                historialClinico.HC_Nombre = form1PerfilPacienteNombre;
                historialClinico.HC_Apellidos = form1PerfilPacienteApellido;
                historialClinico.HC_FechaPrimeraConsulta = form1PerfilPacienteFechaPrimeraConsulta.AddHours(23);
                historialClinico.HC_Edad = form1PerfilPacienteEdad;
                //historialClinico.HC_TipoEdad = form1PerfilPacienteTipoEdad;
                historialClinico.HC_Genero = form1PerfilPacienteGenero;
                historialClinico.HC_MotivoConsulta = form1PerfilPacienteMotivoConsulta;
                historialClinico.HC_EdadNotaronDeformidadToracica = form1PerfilPacienteNotaronDeformidad;
                historialClinico.HC_EmpeoradoUltimamente = form1PerfilPacienteEmpeorado;
                historialClinico.HC_Cuando = form1PerfilPacienteCuando;
                historialClinico.HC_HermanosGemelos = form1PerfilPacienteHermanosGemelos;
                historialClinico.HC_AntecendentesFamilia = AntecendentesFamiliaArray(form1PerfilPacienteAntecedentesFamilia1, form1PerfilPacienteAntecedentesFamilia2,
                        form1PerfilPacienteAntecedentesFamilia3, form1PerfilPacienteAntecedentesFamilia4);
                historialClinico.HC_OtrosFamiliares = form1PerfilPacienteOtrosFamiliares;

                try
                {
                    await _historialClinicoServices.Update(form1PerfilPacienteId, historialClinico);
                }
                catch (Exception ex)
                {
                    return RedirectToAction(Values.Index, Values.PerfilPaciente, new { area = Values.AreaVacio });
                }

                return RedirectToAction(Values.Index, Values.PerfilPaciente, new { area = Values.AreaVacio, id = idPaciente });
            }
            else
            {
                return RedirectToAction(Values.Index, Values.PerfilPaciente, new { area = Values.AreaVacio });
            }
        }

        /// <summary>
        /// Guardamos los nuevos valores del Form2 Historial Clinico - Editar
        /// </summary>
        /// <param name="idPaciente"></param>
        /// <param name="form2PerfilPacienteId"></param>
        /// <param name="form2PerfilPacienteTipoCirugia"></param>
        /// <param name="form2PerfilPacienteEdad"></param>
        /// <returns></returns>
        public async Task<IActionResult> form2PerfilPacientesGuardarInputs(string idPaciente, string form2PerfilPacienteId,
            string form2PerfilPacienteTipoCirugia, string form2PerfilPacienteEdad)
        {
            if (!_validarAcceso.ValidarAccesoWeb(Session)) return RedirectToAction(Values.Index, Values.LoginUsuario, new { area = Values.AreaVacio });

            if (form2PerfilPacienteId != null)
            {
                CirugiaPrevia cirugiaPrevia = new CirugiaPrevia();

                try
                {
                    cirugiaPrevia = await _cirugiaPreviaServices.GetById(form2PerfilPacienteId);
                }
                catch
                {
                    return RedirectToAction(Values.Index, Values.PerfilPaciente, new { area = Values.AreaVacio });
                }

                cirugiaPrevia.CP_TipoCirugia = form2PerfilPacienteTipoCirugia;
                cirugiaPrevia.CP_Edad = form2PerfilPacienteEdad;

                try
                {
                    await _cirugiaPreviaServices.Update(form2PerfilPacienteId, cirugiaPrevia);
                }
                catch
                {
                    return RedirectToAction(Values.Index, Values.PerfilPaciente, new { area = Values.AreaVacio });
                }

                return RedirectToAction(Values.Index, Values.PerfilPaciente, new { area = Values.AreaVacio, id = idPaciente });
            }
            else
            {
                return RedirectToAction(Values.Index, Values.PerfilPaciente, new { area = Values.AreaVacio });
            }
        }

        /// <summary>
        /// Guardamos los nuevos valores del Form3 Historial Clinico - Editar
        /// </summary>
        /// <param name="idPaciente"></param>
        /// <param name="form3PerfilPacienteId"></param>
        /// <param name="form3PerfilPacienteEnfermedadPreexistente"></param>
        /// <param name="form3PerfilPacienteAlergia"></param>
        /// <param name="form3PerfilPacienteObservaciones"></param>
        /// <param name="form3PerfilPacienteOtrasAlergias"></param>
        /// <param name="form3PerfilPacienteDeporte"></param>
        /// <param name="form3PerfilPacienteFuma"></param>
        /// <param name="form3PerfilPacienteMedicacion"></param>
        /// <param name="form3PerfilPacienteCual"></param>
        /// <returns></returns>
        public async Task<IActionResult> form3PerfilPacientesGuardarInputs(string idPaciente, string form3PerfilPacienteId,
            string form3PerfilPacienteEnfermedadPreexistente, string form3PerfilPacienteAlergia, string form3PerfilPacienteObservaciones,
            string form3PerfilPacienteOtrasAlergias, string form3PerfilPacienteDeporte, string form3PerfilPacienteFuma,
            string form3PerfilPacienteMedicacion, string form3PerfilPacienteCual)
        {
            if (!_validarAcceso.ValidarAccesoWeb(Session)) return RedirectToAction(Values.Index, Values.LoginUsuario, new { area = Values.AreaVacio });

            if (form3PerfilPacienteId != null)
            {
                Anamnesis anamnesis = new Anamnesis();

                try
                {
                    anamnesis = await _anamnesisServices.GetById(form3PerfilPacienteId);
                }
                catch
                {
                    return RedirectToAction(Values.Index, Values.PerfilPaciente, new { area = Values.AreaVacio });
                }

                anamnesis.A_EnfermedadPreexistente = form3PerfilPacienteEnfermedadPreexistente;
                anamnesis.A_Alergia = form3PerfilPacienteAlergia;
                anamnesis.A_Observaciones = form3PerfilPacienteObservaciones;
                anamnesis.A_OtrasAlergias = form3PerfilPacienteOtrasAlergias;
                anamnesis.A_Deporte = form3PerfilPacienteDeporte;
                anamnesis.A_Fuma = form3PerfilPacienteFuma;
                anamnesis.A_Medicacion = form3PerfilPacienteMedicacion;
                anamnesis.A_Cual = form3PerfilPacienteCual;

                try
                {
                    await _anamnesisServices.Update(form3PerfilPacienteId, anamnesis);
                }
                catch
                {
                    return RedirectToAction(Values.Index, Values.PerfilPaciente, new { area = Values.AreaVacio });
                }

                return RedirectToAction(Values.Index, Values.PerfilPaciente, new { area = Values.AreaVacio, id = idPaciente });
            }
            else
            {
                return RedirectToAction(Values.Index, Values.PerfilPaciente, new { area = Values.AreaVacio });
            }
        }

        /// <summary>
        ///  Guardamos los nuevos valores del Form4 Historial Clinico - Editar
        /// </summary>
        /// <param name="idPaciente"></param>
        /// <param name="form4PerfilPacienteId"></param>
        /// <param name="form4PerfilPacienteTipoEnfermedad"></param>
        /// <param name="form4PerfilPacienteEdadDiagnostico"></param>
        /// <param name="form4PerfilPacienteOtros"></param>
        /// <param name="form4PerfilPacienteEspecificar"></param>
        /// <returns></returns>
        public async Task<IActionResult> form4PerfilPacientesGuardarInputs(string idPaciente, string form4PerfilPacienteId, string form4PerfilPacienteTipoEnfermedad,
            string form4PerfilPacienteEdadDiagnostico, string form4PerfilPacienteOtros, string form4PerfilPacienteEspecificar)
        {
            if (!_validarAcceso.ValidarAccesoWeb(Session)) return RedirectToAction(Values.Index, Values.LoginUsuario, new { area = Values.AreaVacio });

            if (form4PerfilPacienteId != null)
            {
                EnfermdedadPreexistente enfermdedadPreexistente = new EnfermdedadPreexistente();

                try
                {
                    enfermdedadPreexistente = await _enfermedadPreexistenteServices.GetById(form4PerfilPacienteId);
                }
                catch
                {
                    return RedirectToAction(Values.Index, Values.PerfilPaciente, new { area = Values.AreaVacio });
                }

                enfermdedadPreexistente.A_Tipo = form4PerfilPacienteTipoEnfermedad;
                enfermdedadPreexistente.A_Edad = form4PerfilPacienteEdadDiagnostico;
                enfermdedadPreexistente.A_Otros = form4PerfilPacienteOtros;
                enfermdedadPreexistente.A_Especificar = form4PerfilPacienteEspecificar;

                try
                {
                    await _enfermedadPreexistenteServices.Update(form4PerfilPacienteId, enfermdedadPreexistente);
                }
                catch
                {
                    return RedirectToAction(Values.Index, Values.PerfilPaciente, new { area = Values.AreaVacio, id = idPaciente });
                }

                return RedirectToAction(Values.Index, Values.PerfilPaciente, new { area = Values.AreaVacio, id = idPaciente });
            }
            else
            {
                return RedirectToAction(Values.Index, Values.PerfilPaciente, new { area = Values.AreaVacio, id = idPaciente });
            }
        }

        /// <summary>
        ///  Guardamos los nuevos valores del Form5 Historial Clinico - Editar
        /// </summary>
        /// <param name="idPaciente"></param>
        /// <param name="form5PerfilPacienteId"></param>
        /// <param name="form5PerfilPacienteCual"></param>
        /// <param name="form5PerfilPacienteFrecuencia"></param>
        /// <param name="form5PerfilPacienteDejoDeporte"></param>
        /// <param name="form5PerfilPacientePorque"></param>
        /// <returns></returns>
        public async Task<IActionResult> form5PerfilPacientesGuardarInputs(string idPaciente, string form5PerfilPacienteId,
            string[] form5PerfilPacienteCual, string form5PerfilPacienteFrecuencia, string form5PerfilPacienteDejoDeporte,
            string form5PerfilPacientePorque)
        {
            if (!_validarAcceso.ValidarAccesoWeb(Session)) return RedirectToAction(Values.Index, Values.LoginUsuario, new { area = Values.AreaVacio });

            if (form5PerfilPacienteId != null)
            {
                Deporte deporte = new Deporte();

                try
                {
                    deporte = await _deporteServices.GetById(form5PerfilPacienteId);
                }
                catch
                {
                    return RedirectToAction(Values.Index, Values.PerfilPaciente, new { area = Values.AreaVacio });
                }

                deporte.D_Cual = DeporteCual(form5PerfilPacienteCual);
                deporte.D_Frecuencia = form5PerfilPacienteFrecuencia;
                deporte.D_DejoDeporte = form5PerfilPacienteDejoDeporte;
                deporte.D_Porque = form5PerfilPacientePorque;

                try
                {
                    await _deporteServices.Update(form5PerfilPacienteId, deporte);
                }
                catch
                {
                    return RedirectToAction(Values.Index, Values.PerfilPaciente, new { area = Values.AreaVacio });
                }

                return RedirectToAction(Values.Index, Values.PerfilPaciente, new { area = Values.AreaVacio, id = idPaciente });
            }
            else
            {
                return RedirectToAction(Values.Index, Values.PerfilPaciente, new { area = Values.AreaVacio });
            }
        }

        /// <summary>
        /// Guardamos los nuevos valores del Form6 Historial Clinico - Editar
        /// </summary>
        /// <param name="idPaciente"></param>
        /// <param name="form6PerfilPacienteId"></param>
        /// <param name="form6PerfilPacienteDificultadReposo"></param>
        /// <param name="form6PerfilPacienteDificultadFisica"></param>
        /// <param name="form6PerfilPacienteIntolerancia"></param>
        /// <param name="form6PerfilPacienteNeumonias"></param>
        /// <param name="form6PerfilPacienteOtrasNeumonias"></param>
        /// <param name="form6PerfilPacienteAsma"></param>
        /// <param name="form6PerfilPacienteBroncoespasmos"></param>
        /// <param name="form6PerfilPacienteEscoliosis"></param>
        /// <param name="form6PerfilPacienteDolorEspalda"></param>
        /// <param name="form6PerfilPacientePalpitaciones"></param>
        /// <param name="form6PerfilPacienteOtros"></param>
        /// <param name="form6PerfilPacienteSignosSintomas"></param>
        /// <returns></returns>
        public async Task<IActionResult> form6PerfilPacientesGuardarInputs(string idPaciente, string form6PerfilPacienteId,
            string form6PerfilPacienteDificultadReposo, string form6PerfilPacienteDificultadFisica,
            string form6PerfilPacienteNeumonias, string form6PerfilPacienteOtrasNeumonias, string form6PerfilPacienteDolorEspalda,
            string form6PerfilPacientePalpitaciones, string form6PerfilPacienteOtros, string[] form6PerfilPacienteSignosSintomas)
        {
            if (!_validarAcceso.ValidarAccesoWeb(Session)) return RedirectToAction(Values.Index, Values.LoginUsuario, new { area = Values.AreaVacio });

            if (form6PerfilPacienteId != null)
            {
                SignosSintomasClinicos signosSintomasClinicos = new SignosSintomasClinicos();

                try
                {
                    signosSintomasClinicos = await _signosSintomasClinicosServices.GetById(form6PerfilPacienteId);
                }
                catch
                {
                    return RedirectToAction(Values.Index, Values.PerfilPaciente, new { area = Values.AreaVacio });
                }

                signosSintomasClinicos.SSC_DificultadRespiratoriaReposo = form6PerfilPacienteDificultadReposo;
                signosSintomasClinicos.SSC_DificultadRespiratoriaActividadFisica = form6PerfilPacienteDificultadFisica;
                signosSintomasClinicos.SSC_NeumoniasRepeticion = form6PerfilPacienteNeumonias;
                signosSintomasClinicos.SSC_OtrasNeumoniasRepeticion = form6PerfilPacienteOtrasNeumonias;
                signosSintomasClinicos.SSC_DolorEspalda = form6PerfilPacienteDolorEspalda;
                signosSintomasClinicos.SSC_PalpitacionesTaquicardia = form6PerfilPacientePalpitaciones;
                signosSintomasClinicos.SSC_Otros = form6PerfilPacienteOtros;
                signosSintomasClinicos.SSC_SignosSintomas = SignosSintomasArray(signosSintomasClinicos.SSC_SignosSintomas, form6PerfilPacienteSignosSintomas);

                try
                {
                    await _signosSintomasClinicosServices.Update(form6PerfilPacienteId, signosSintomasClinicos);
                }
                catch
                {
                    return RedirectToAction(Values.Index, Values.PerfilPaciente, new { area = Values.AreaVacio });
                }

                return RedirectToAction(Values.Index, Values.PerfilPaciente, new { area = Values.AreaVacio, id = idPaciente });
            }
            else
            {
                return RedirectToAction(Values.Index, Values.PerfilPaciente, new { area = Values.AreaVacio });
            }
        }

        /// <summary>
        /// Guardamos los nuevos valores del FormTipoPetus (PectusCarinatum) Historial Clinico - Editar
        /// </summary>
        /// <param name="idPaciente"></param>
        /// <param name="formTipoPectusPerfilPacienteId"></param>
        /// <param name="formPectusCarinatumPerfilPacienteTipo"></param>
        /// <param name="formPectusCarinatumPerfilPacienteSimetria"></param>
        /// <param name="formPectusCarinatumPerfilPacienteRotacion"></param>
        /// <param name="formPectusCarinatumPerfilPacientePresion"></param>
        /// <returns></returns>
        public async Task<IActionResult> formPectusCarinatumPerfilPacientesGuardarInputs(string idPaciente, string formTipoPectusPerfilPacienteId,
            string formPectusCarinatumPerfilPacienteTipo, string formPectusCarinatumPerfilPacienteSimetria, string formPectusCarinatumPerfilPacienteRotacion,
            string formPectusCarinatumPerfilPacientePresion)
        {
            if (!_validarAcceso.ValidarAccesoWeb(Session)) return RedirectToAction(Values.Index, Values.LoginUsuario, new { area = Values.AreaVacio });

            if (formTipoPectusPerfilPacienteId != null)
            {
                PectusCarinatum pectusCarinatum = new PectusCarinatum();

                try
                {
                    pectusCarinatum = await _pectusCarinatumServices.GetById(formTipoPectusPerfilPacienteId);
                }
                catch
                {
                    return RedirectToAction(Values.Index, Values.PerfilPaciente, new { area = Values.AreaVacio });
                }

                pectusCarinatum.PC_Tipo = formPectusCarinatumPerfilPacienteTipo;
                pectusCarinatum.PC_Simetria = formPectusCarinatumPerfilPacienteSimetria;
                pectusCarinatum.PC_RotacionEsternal = formPectusCarinatumPerfilPacienteRotacion;
                pectusCarinatum.PC_PresionCorreccion = formPectusCarinatumPerfilPacientePresion;

                try
                {
                    await _pectusCarinatumServices.Update(formTipoPectusPerfilPacienteId, pectusCarinatum);
                }
                catch
                {
                    return RedirectToAction(Values.Index, Values.PerfilPaciente, new { area = Values.AreaVacio });
                }

                return RedirectToAction(Values.Index, Values.PerfilPaciente, new { area = Values.AreaVacio, id = idPaciente });
            }
            else
            {
                return RedirectToAction(Values.Index, Values.PerfilPaciente, new { area = Values.AreaVacio });
            }
        }

        /// <summary>
        /// Guardamos los nuevos valores del FormTipoPetus (PectusExcavatum) Historial Clinico - Editar
        /// </summary>
        /// <param name="idPaciente"></param>
        /// <param name="formTipoPectusPerfilPacienteId"></param>
        /// <param name="formPectusExcavatumPerfilPacienteSimetria"></param>
        /// <param name="formPectusExcavatumPerfilPacienteHundimientoParado"></param>
        /// <param name="formPectusExcavatumPerfilPacienteHundimientoAcostado"></param>
        /// <param name="formPectusExcavatumPerfilPacienteRotacionEsternal"></param>
        /// <returns></returns>
        public async Task<IActionResult> formPectusExcavatumPerfilPacientesGuardarInputs(string idPaciente, string formTipoPectusPerfilPacienteId,
            string formPectusExcavatumPerfilPacienteSimetria, string formPectusExcavatumPerfilPacienteHundimientoParado,
            string formPectusExcavatumPerfilPacienteHundimientoAcostado, string formPectusExcavatumPerfilPacienteRotacionEsternal)
        {
            if (!_validarAcceso.ValidarAccesoWeb(Session)) return RedirectToAction(Values.Index, Values.LoginUsuario, new { area = Values.AreaVacio });

            if (formTipoPectusPerfilPacienteId != null)
            {
                PectusExcavatum pectusExcavatum = new PectusExcavatum();

                try
                {
                    pectusExcavatum = await _pectusExavatumServices.GetById(formTipoPectusPerfilPacienteId);
                }
                catch
                {
                    return RedirectToAction(Values.Index, Values.PerfilPaciente, new { area = Values.AreaVacio });
                }

                pectusExcavatum.PE_Simetria = formPectusExcavatumPerfilPacienteSimetria;
                pectusExcavatum.PE_HundimientoToracicoParado = formPectusExcavatumPerfilPacienteHundimientoParado;
                pectusExcavatum.PE_HundimientoToracicoAcostado = formPectusExcavatumPerfilPacienteHundimientoAcostado;
                pectusExcavatum.PE_RotacionEsternal = formPectusExcavatumPerfilPacienteRotacionEsternal;

                try
                {
                    await _pectusExavatumServices.Update(formTipoPectusPerfilPacienteId, pectusExcavatum);
                }
                catch
                {
                    return RedirectToAction(Values.Index, Values.PerfilPaciente, new { area = Values.AreaVacio });
                }

                return RedirectToAction(Values.Index, Values.PerfilPaciente, new { area = Values.AreaVacio, id = idPaciente });
            }
            else
            {
                return RedirectToAction(Values.Index, Values.PerfilPaciente, new { area = Values.AreaVacio });
            }
        }

        /// <summary>
        /// Guardamos los nuevos valores del FormTipoPetus (PectusMixto) Historial Clinico - Editar
        /// </summary>
        /// <param name="idPaciente"></param>
        /// <param name="formTipoPectusPerfilPacienteId"></param>
        /// <param name="formPectusMixtoPerfilPacienteUbicacion"></param>
        /// <param name="formPectusMixtoPerfilPacientePresion"></param>
        /// <param name="formPectusMixtoPerfilPacienteRotacion"></param>
        /// <returns></returns>
        public async Task<IActionResult> formPectusMixtoPerfilPacientesGuardarInputs(string idPaciente, string formTipoPectusPerfilPacienteId,
            string formPectusMixtoPerfilPacienteUbicacion, string formPectusMixtoPerfilPacientePresion, string formPectusMixtoPerfilPacienteRotacion)
        {
            if (!_validarAcceso.ValidarAccesoWeb(Session)) return RedirectToAction(Values.Index, Values.LoginUsuario, new { area = Values.AreaVacio });

            if (formTipoPectusPerfilPacienteId != null)
            {
                PectusMixto pectusMixto = new PectusMixto();

                try
                {
                    pectusMixto = await _pectusMixtoServices.GetById(formTipoPectusPerfilPacienteId);
                }
                catch
                {
                    return RedirectToAction(Values.Index, Values.PerfilPaciente, new { area = Values.AreaVacio });
                }

                pectusMixto.PM_UbicacionDefecto = formPectusMixtoPerfilPacienteUbicacion;
                pectusMixto.PM_PresionCorreccionPectusCarinatum = formPectusMixtoPerfilPacientePresion;
                pectusMixto.PM_RotacionEsternal = formPectusMixtoPerfilPacienteRotacion;

                try
                {
                    await _pectusMixtoServices.Update(formTipoPectusPerfilPacienteId, pectusMixto);
                }
                catch
                {
                    return RedirectToAction(Values.Index, Values.PerfilPaciente, new { area = Values.AreaVacio });
                }

                return RedirectToAction(Values.Index, Values.PerfilPaciente, new { area = Values.AreaVacio, id = idPaciente });
            }
            else
            {
                return RedirectToAction(Values.Index, Values.PerfilPaciente, new { area = Values.AreaVacio });
            }
        }

        /// <summary>
        /// Guardamos los nuevos valores del FormTipoPetus (SindromePoland) Historial Clinico - Editar
        /// </summary>
        /// <param name="idPaciente"></param>
        /// <param name="formTipoPectusPerfilPacienteId"></param>
        /// <param name="formSindromePolandPerfilPacienteToracica"></param>
        /// <param name="formSindromePolandPerfilPacienteMamaria"></param>
        /// <param name="formSindromePolandPerfilPacienteAnomaliaCompleja"></param>
        /// <param name="formSindromePolandPerfilPacienteT2T3T4"></param>
        /// <param name="formSindromePolandPerfilPacientePCPSI"></param>
        /// <returns></returns>
        public async Task<IActionResult> formSindromePolandPerfilPacientesGuardarInputs(string idPaciente, string formTipoPectusPerfilPacienteId,
            string formSindromePolandPerfilPacienteToracica, string formSindromePolandPerfilPacienteMamaria, string formSindromePolandPerfilPacienteAnomaliaCompleja,
            string formSindromePolandPerfilPacienteT2T3T4, string formSindromePolandPerfilPacientePCPSI)
        {
            if (!_validarAcceso.ValidarAccesoWeb(Session)) return RedirectToAction(Values.Index, Values.LoginUsuario, new { area = Values.AreaVacio });

            if (formTipoPectusPerfilPacienteId != null)
            {
                SindromePoland sindromePoland = new SindromePoland();

                try
                {
                    sindromePoland = await _sindromePolandServices.GetById(formTipoPectusPerfilPacienteId);
                }
                catch
                {
                    return RedirectToAction(Values.Index, Values.PerfilPaciente, new { area = Values.AreaVacio });
                }

                sindromePoland.SP_AnomaliaToracica = formSindromePolandPerfilPacienteToracica;
                sindromePoland.SP_AnomaliaMamaria = formSindromePolandPerfilPacienteMamaria;
                sindromePoland.SP_AnomaliaComplejoPezonAreola = formSindromePolandPerfilPacienteAnomaliaCompleja;
                sindromePoland.SP_T2T3T4 = formSindromePolandPerfilPacienteT2T3T4;
                sindromePoland.SP_PCPSIPectusCarinatum = formSindromePolandPerfilPacientePCPSI;

                try
                {
                    await _sindromePolandServices.Update(formTipoPectusPerfilPacienteId, sindromePoland);
                }
                catch
                {
                    return RedirectToAction(Values.Index, Values.PerfilPaciente, new { area = Values.AreaVacio });
                }

                return RedirectToAction(Values.Index, Values.PerfilPaciente, new { area = Values.AreaVacio, id = idPaciente });
            }
            else
            {
                return RedirectToAction(Values.Index, Values.PerfilPaciente, new { area = Values.AreaVacio });
            }
        }

        /// <summary>
        /// Guardar los nuevos Signos Sintomas - Editar
        /// </summary>
        /// <param name="sintomas"></param>
        /// <param name="sintomasNuevos"></param>
        /// <returns></returns>
        private string[,] SignosSintomasArray(string[,] sintomas, string[] sintomasNuevos)
        {
            for (int i = 0; i < sintomasNuevos.Length; i++)
            {
                sintomas[i, 0] = sintomasNuevos[i];
                sintomas[i, 1] = null;
            }

            return sintomas;
        }

        /// <summary>
        /// Cogemos solos los inputs que tengar valor
        /// </summary>
        /// <param name="arrayDeportes"></param>
        /// <returns></returns>
        private string[] DeporteCual(string[] arrayDeportes)
        {
            List<string> nuevoArray = new List<string>();

            for (int i = 0; i < arrayDeportes.Length; i++)
            {
                if (arrayDeportes[i] != null && arrayDeportes[i] != "")
                {
                    nuevoArray.Add(arrayDeportes[i]);
                }
            }
            return nuevoArray.ToArray();
        }

        /// <summary>
        /// Guardar valores editar form1 AntecedentesFamilia to array
        /// </summary>
        /// <param name="antecedente0"></param>
        /// <param name="antecedente1"></param>
        /// <param name="antecedente2"></param>
        /// <param name="antecedente3"></param>
        /// <returns></returns>
        private string[,] AntecendentesFamiliaArray(string[] antecedente0, string[] antecedente1, string[] antecedente2, string[] antecedente3)
        {
            string[,] result = new string[4, 4];

            for (int i = 0; i < antecedente0.Length; i++)
            {
                result[0, i] = antecedente0[i];
            }

            for (int i = 0; i < antecedente1.Length; i++)
            {
                result[1, i] = antecedente1[i];
            }

            for (int i = 0; i < antecedente2.Length; i++)
            {
                result[2, i] = antecedente2[i];
            }

            for (int i = 0; i < antecedente3.Length; i++)
            {
                result[3, i] = antecedente3[i];
            }

            return result;
        }

        /// <summary>
        /// Cogemos la Collection Anamnesis a partir del id del Paciente
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task<Anamnesis> getAnamnesisPacienteIdAsync(string id)
        {
            if (id != null)
            {
                var result = await _anamnesisServices.GetById(id);
                return result;
            }
            else { return null; }
        }

        /// <summary>
        /// Cogemos la Collection CirugiaPrevia a partir del id del Paciente
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task<CirugiaPrevia> getCirugiaPacienteIdAsync(string id)
        {
            if (id != null)
            {
                var result = await _cirugiaPreviaServices.GetById(id);
                return result;
            }
            else { return null; }
        }

        /// <summary>
        /// Cogemos la Collection EnfermdedadPreexistente a partir del id del Paciente
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task<EnfermdedadPreexistente> getEnfermdedadPreexistentePacienteIdAsync(string id)
        {
            if (id != null)
            {
                var result = await _enfermedadPreexistenteServices.GetById(id);
                return result;
            }
            else { return null; }
        }

        /// <summary>
        /// Cogemos la Collection Deporte a partir del id del Paciente
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task<Deporte> getDeportePacienteIdAsync(string id)
        {
            if (id != null)
            {
                var result = await _deporteServices.GetById(id);
                return result;
            }
            else { return null; }
        }

        /// <summary>
        /// Cogemos la Collection Deporte a partir del id del Paciente
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task<SignosSintomasClinicos> getSignosSintomasClinicosPacienteIdAsync(string id)
        {
            if (id != null)
            {
                var result = await _signosSintomasClinicosServices.GetById(id);
                return result;
            }
            else { return null; }
        }

        /// <summary>
        /// Obtenemos las imagenes del paciente, de la ruta local del servidor
        /// </summary>
        /// <param name="view">ViewModel actual</param>
        /// <param name="viewModel">Devolvemos el nuevo ViewModel con la informacion del viejo</param>
        private void ObtenerImagenesPaciente(ViewModel view, string id, out ViewModel viewModel)
        {
            bool isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
            string path = String.Empty;

            if (isWindows)
            {
                path = Path.Combine("C:\\ppt-huav", "Pacientes");
            }
            else
            {
                path = Path.Combine("/ppt-huav", "Pacientes");
            }

            var provider = new PhysicalFileProvider(path);
            var contents = provider.GetDirectoryContents(id);
            var objFiles = contents.OrderBy(m => m.LastModified);

            List<string> ImageList = new List<string>();
            foreach (var item in objFiles.ToList())
            {
                ImageList.Add(item.Name);
            }

            view.ImageList = ImageList;
            viewModel = view;
        }
    }
}