#region using's
using System;
using MimeKit;
using MailKit.Net.Smtp;
using TFG_Web.Services;
using System.Security.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TFG_Web.Models;
using TFG_Web.Areas.Clinics.Models;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using System.Text.RegularExpressions;
#endregion

namespace TFG_Web.Areas.Clinics.Controllers
{
    [Area(Values.AreaClinics)]
    public class EnviarCorreoPacienteController : Controller
    {
        #region Dependencias Interfaces
        private ISession Session => HttpContext.Session;
        private readonly ValidarAcceso _validarAcceso = new ValidarAcceso();
        private readonly ViewModel viewModel = new ViewModel();
        private readonly ReadAppSettings _readAppSettings = new ReadAppSettings();
        private readonly IMemoryCache _memoryCache;
        #endregion

        #region Constructor
        public EnviarCorreoPacienteController(IMemoryCache _memoryCache)
        {
            this._memoryCache = _memoryCache;
        }
        #endregion

        [Route(Values.EnviarInformePaciente)]
        public IActionResult Index()
        {
            if (!_validarAcceso.ValidarAccesoWeb(Session)) return RedirectToAction(Values.Index, Values.LoginUsuario, new { area = Values.AreaVacio });

            return RedirectToAction(Values.Index, Values.Inicio, new { area = Values.AreaVacio });
        }

        /// <summary>
        /// Creacion del correo a enviar
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> emailMessage(IFormFile postedFiles, string correoPara, string correoAsunto, string correoDescripcion, string idPaciente)
        {
            if (!_validarAcceso.ValidarAccesoWeb(Session)) return RedirectToAction(Values.Index, Values.LoginUsuario, new { area = Values.AreaVacio });
            
            var formatoCorreo = Regex.IsMatch(correoPara, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));

            if (correoPara != null && formatoCorreo)
            {
                try
                {
                    MimeMessage message = new MimeMessage();

                    MailboxAddress from = new MailboxAddress("Clinico", _readAppSettings.email_correo);
                    message.From.Add(from);

                    MailboxAddress to = new MailboxAddress("Paciente", correoPara);
                    message.To.Add(to);

                    if(correoAsunto != null)
                    {
                        message.Subject = correoAsunto;
                    }

                    BodyBuilder bodyBuilder = new BodyBuilder();
                    //bodyBuilder.HtmlBody = "<h1>Hello World!</h1>";
                    bodyBuilder.TextBody = correoDescripcion;

                    if (postedFiles != null)
                    {
                        byte[] fileBytes;
                        using (var ms = new MemoryStream())
                        {
                            await postedFiles.CopyToAsync(ms);
                            fileBytes = ms.ToArray();
                            string stream = Convert.ToBase64String(fileBytes);
                        }

                        bodyBuilder.Attachments.Add(postedFiles.FileName, fileBytes, new ContentType("application", "pdf"));
                    }

                    message.Body = bodyBuilder.ToMessageBody();

                    SMPTServer(message);
                }
                catch
                {
                    setCacheNotificacion(false, Mensajes.ErrorEnviarCorreoPacienteRecuperarContrasena);

                    return RedirectToAction(Values.Index, Values.PerfilPaciente, new { area = Values.AreaVacio, id = idPaciente });
                }
            }
            else
            {
                setCacheNotificacion(false, Mensajes.ErrorEnviarCorreoPacienteRecuperarContrasena);

                return RedirectToAction(Values.Index, Values.PerfilPaciente, new { area = Values.AreaVacio, id = idPaciente });
            }

            setCacheNotificacion(true, Mensajes.SuccessEnviarCorreoPaciente);

            return RedirectToAction(Values.Index, Values.PerfilPaciente, new { area = Values.AreaVacio, id = idPaciente });
        }

        /// <summary>
        /// Conexion el servidor SMPT de gmail y envio correo
        /// </summary>
        /// <param name="message"></param>
        private void SMPTServer(MimeMessage message)
        {
            //Cuenta Gmail: Acesso de aplicaciones poca seguras (Activado) y Acceso IMAP Habilitado
            try
            {
                SmtpClient client = new SmtpClient();
                client.Connect(_readAppSettings.email_SmtpServer, 465, true);
                client.SslProtocols = SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12;
                client.Authenticate(_readAppSettings.email_correo, _readAppSettings.email_password);

                client.Send(message);
                client.Disconnect(true);
                client.Dispose();
            }
            catch{}
        }

        /// <summary>
        /// Ponemos en la cache el mensaje de respuesta del Enviar Correo
        /// </summary>
        /// <param name="resultado"></param>
        /// <param name="message"></param>
        private void setCacheNotificacion(bool resultado, string message)
        {
            var cacheEntryoptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(2));

            _memoryCache.Set("enviarCorreoPaciente", resultado, cacheEntryoptions);
            _memoryCache.Set("enviarCorreoPacienteMensaje", message, cacheEntryoptions);
        }
    }
}