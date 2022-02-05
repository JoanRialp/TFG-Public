using System;
using System.Security.Authentication;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MimeKit;
using TFG_Web.Interface;

namespace TFG_Web.Services
{
    public class SMTPServices : ISMTPServices
    {
        #region Dependencias Interfaces
        private readonly ReadAppSettings _readAppSettings = new ReadAppSettings();
        private readonly ILogger<TokenHostedServices> _logger;
        #endregion

        #region Constructor
        public SMTPServices(ILogger<TokenHostedServices> logger)
        {
            this._logger = logger;
        }
        #endregion

        /// <summary>
        /// Conexion el servidor SMPT de gmail y envio correo
        /// </summary>
        /// <param name="message"></param>
        public void SMPTServer(MimeMessage message)
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
            catch (Exception ex)
            {
                _logger.LogError("RecuperarContrasenaController: " + ex.Message);
            }
        }
    }
}