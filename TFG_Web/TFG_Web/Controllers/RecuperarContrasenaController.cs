using System;
using System.Security.Authentication;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MimeKit;
using TFG_Web.Areas.Clinics.Interface.Services;
using TFG_Web.Areas.Clinics.Models;
using TFG_Web.Areas.Clinics.Services;
using TFG_Web.Interface;
using TFG_Web.Models;
using TFG_Web.Services;

namespace TFG_Web.Controllers
{
    [ViewLayout(Values.Layout)]
    public class RecuperarContrasenaController : Controller
    {
        #region Dependencias Interfaces
        private ISession Session => HttpContext.Session;
        private readonly ReadAppSettings _readAppSettings = new ReadAppSettings();
        private readonly ILogger<TokenHostedServices> _logger;
        private readonly ITokenContrasenaServices _tokenContrasenaServices;
        private readonly UsuariosServices _usuariosServices = new UsuariosServices();
        private readonly ISMTPServices _smtpServices;
        private readonly ICacheServices _cacheServices;
        #endregion

        #region Constructor
        public RecuperarContrasenaController(ICacheServices cacheServices, ILogger<TokenHostedServices> logger, ITokenContrasenaServices tokenContrasenaServices,
            ISMTPServices smtpServices)
        {
            this._logger = logger;
            this._cacheServices = cacheServices;
            this._tokenContrasenaServices = tokenContrasenaServices;
            this._smtpServices = smtpServices;
        }
        #endregion

        [Route(Values.RecuperarContrasena)]
        public async Task<IActionResult> IndexAsync(string token)
        {
            //Comprobamos si el usuario ya ha iniciado sesion a la web, si es que si le redireccionamos al Inicio, sino seguimos en el LoginUsuario
            if (Session.GetString(Values.Session_usrId) != null)
            {
                return RedirectToAction(Values.Index, Values.HomeClinics, new { area = Values.AreaClinics });
            }

            ViewModel viewModel = new ViewModel();

            if (token != null)
            {
                try
                {
                    TokenContrasena tokenContrasena = await _tokenContrasenaServices.GetByToken(token);

                    if (tokenContrasena.Id != null)
                    {
                        Usuarios usuarios = new Usuarios();
                        usuarios.Id = tokenContrasena.TC_IdUsuario;
                        usuarios.P_Password = tokenContrasena.TC_Token;
                        viewModel.Usuarios = usuarios;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("RecuperarContrasenaController: " + ex.Message);
                }
            }

            viewModel = _cacheServices.GetCacheNotificacionRecuperarContrasena(viewModel, "recuperarContrasenaEnviarCorreo");
            viewModel = _cacheServices.GetCacheNotificacionRecuperarContrasena(viewModel, "recuperarContrasenaEnviarCorreoMensaje");
            viewModel = _cacheServices.GetCacheNotificacionRecuperarContrasena(viewModel, "recuperarContrasenaFormatoCorreo");
            viewModel = _cacheServices.GetCacheNotificacionRecuperarContrasena(viewModel, "recuperarContrasenaFormatoCorreoMensaje");

            viewModel = _cacheServices.GetCacheNotificacionRecuperarContrasena(viewModel, "recuperarContrasenaCambiarContrasena");
            viewModel = _cacheServices.GetCacheNotificacionRecuperarContrasena(viewModel, "recuperarContrasenaCambiarContrasenaMensaje");

            return View(Values.Index, viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EnviarCorreoRecuperacionAsync(string correo)
        {
            bool formatoCorreo = Regex.IsMatch(correo, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));

            if (formatoCorreo)
            {
                Usuarios usuarios = new Usuarios();

                try
                {
                    usuarios = await _usuariosServices.FindCorreo(correo);
                }
                catch (Exception ex)
                {
                    _logger.LogError("RecuperarContrasenaController: " + ex.Message);
                }

                //Comprobar que existe el usuario con el correo electronico
                if (usuarios.Id != null)
                {
                    try
                    {
                        MimeMessage message = new MimeMessage();

                        MailboxAddress from = new MailboxAddress("ppt-huav", _readAppSettings.email_correo);
                        message.From.Add(from);

                        MailboxAddress to = new MailboxAddress("Usuario", correo);
                        message.To.Add(to);

                        message.Subject = "Recuperar contraseña de ppt-huav";

                        //Generar el token de recuperacion
                        string token = Guid.NewGuid().ToString();

                        //Guardamos el token en BDD
                        await GuardarTokenBD(usuarios.Id, token);

                        BodyBuilder bodyBuilder = new BodyBuilder();
                        bodyBuilder.TextBody = @"Para recuperar la contraseña accede el link: https://ppt-huav.udl.cat/RecuperarContrasena?token=" + token;

                        message.Body = bodyBuilder.ToMessageBody();

                        //Enviar correo electronico
                        _smtpServices.SMPTServer(message);

                        _cacheServices.SetCacheNotificacionRecuperarContrasena("recuperarContrasenaEnviarCorreo", true, null);
                        _cacheServices.SetCacheNotificacionRecuperarContrasena("recuperarContrasenaEnviarCorreoMensaje", null, Mensajes.SuccessEnviarCorreoPacienteRecuperarContrasena);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError("RecuperarContrasenaController: " + ex.Message);

                        //Notificacion Cache
                        _cacheServices.SetCacheNotificacionRecuperarContrasena("recuperarContrasenaEnviarCorreo", false, null);
                        _cacheServices.SetCacheNotificacionRecuperarContrasena("recuperarContrasenaEnviarCorreoMensaje", null, Mensajes.ErrorEnviarCorreoPacienteRecuperarContrasena);

                        return RedirectToAction(Values.Index, Values.RecuperarContrasena, new { area = Values.AreaVacio });
                    }
                }
                else
                {
                    //Notificacion Cache
                    _cacheServices.SetCacheNotificacionRecuperarContrasena("recuperarContrasenaFormatoCorreo", false, null);
                    _cacheServices.SetCacheNotificacionRecuperarContrasena("recuperarContrasenaFormatoCorreoMensaje", null, Mensajes.ErrorCorreoPacienteRecuperarContrasena);
                }
            }

            return RedirectToAction(Values.Index, Values.RecuperarContrasena, new { area = Values.AreaVacio });
        }


        [HttpPost]
        public async Task<IActionResult> CambiarContrasenaAsync(string contrasena, string token)
        {
            string mensaje = Mensajes.ErrorCambiarContrasenaRecuperarContrasena;
            bool result = false;

            //Control Token
            if (token != null)
            {
                try
                {
                    TokenContrasena tokenContrasena = await _tokenContrasenaServices.GetByToken(token);

                    if (tokenContrasena.Id != null)
                    {
                        Usuarios usuarios = new Usuarios();
                        usuarios = await _usuariosServices.GetById(tokenContrasena.TC_IdUsuario);

                        string cryptedPassword = usuarios.cryptedPassword(contrasena);
                        usuarios.Id = tokenContrasena.TC_IdUsuario;
                        usuarios.P_Password = cryptedPassword;

                        try
                        {
                            await _usuariosServices.Update(tokenContrasena.TC_IdUsuario, usuarios);
                            result = true;
                            mensaje = Mensajes.SuccessCambiarContrasenaRecuperarContrasena;
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError("RecuperarContrasenaController: " + ex.Message);
                            result = false;
                            mensaje = Mensajes.ErrorCambiarContrasenaRecuperarContrasena;
                        }

                        try
                        {
                            await _tokenContrasenaServices.Delete(tokenContrasena.Id);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError("RecuperarContrasenaController: " + ex.Message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("RecuperarContrasenaController: " + ex.Message);
                }
            }

            //Notificacion Cache
            _cacheServices.SetCacheNotificacionRecuperarContrasena("recuperarContrasenaCambiarContrasena", result, null);
            _cacheServices.SetCacheNotificacionRecuperarContrasena("recuperarContrasenaCambiarContrasenaMensaje", null, mensaje);

            return RedirectToAction(Values.Index, Values.RecuperarContrasena, new { area = Values.AreaVacio });
        }

        /// <summary>
        /// Guardamos el nuevo token con el id del Usuario en la Base de datos
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private async Task GuardarTokenBD(string idUsuario, string token)
        {
            TokenContrasena tokenContrasena = new TokenContrasena();
            tokenContrasena.TC_IdUsuario = idUsuario;
            tokenContrasena.TC_Token = token;

            await _tokenContrasenaServices.Create(tokenContrasena);
        }
    }
}
