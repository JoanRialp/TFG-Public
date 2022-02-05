using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;

namespace TFG_Web.Services
{
    public class ReadAppSettings
    {
        private const string DatabaseSettings = "TFGDatabaseSettings";
        private const string appsettings = "appsettings.json";
        private const string _databaseName = "DatabaseName";
        private const string _connectionString = "ConnectionString";
        private const string _email = "Email";

        public string ConnectionString { get; }
        public string DatabaseName { get; }
        public string ControlSistemaCompresorCollectionName { get; }
        public string PacientesName { get; }
        public string UsuariosName { get; }
        public string UsuariosControlCampanaPectusExcavatumName { get; }
        public string TokenContrasenaName { get; }
        public string email_correo { get; }
        public string email_SmtpServer { get; }
        public string email_password { get; }


        /// <summary>
        /// Cogemos la informacion del appsettings.json
        /// </summary>
        public ReadAppSettings()
        {
            var configuration = GetConfiguration();

            DatabaseName = configuration.GetSection(DatabaseSettings).GetSection(_databaseName).Value;
            ConnectionString = configuration.GetSection(DatabaseSettings).GetSection(_connectionString).Value;

            ControlSistemaCompresorCollectionName = configuration.GetSection(DatabaseSettings).GetSection("ControlSistemaCompresorCollectionName").Value;
            PacientesName = configuration.GetSection(DatabaseSettings).GetSection("PacientesName").Value;
            UsuariosName = configuration.GetSection(DatabaseSettings).GetSection("UsuariosName").Value;
            UsuariosControlCampanaPectusExcavatumName = configuration.GetSection(DatabaseSettings).GetSection("ControlCampanaPectusExcavatumName").Value;
            TokenContrasenaName = configuration.GetSection(DatabaseSettings).GetSection("TokenContrasenaName").Value;

            email_correo = configuration.GetSection(_email).GetSection("Correo").Value;
            email_SmtpServer = configuration.GetSection(_email).GetSection("SmtpServer").Value;
            email_password = configuration.GetSection(_email).GetSection("Password").Value;
        }

        /// <summary>
        /// Configuracion para obtener la informacion del appsettings.json
        /// </summary>
        /// <returns></returns>
        private IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(appsettings, optional: true, reloadOnChange: true);
            return builder.Build();
        }
    }
}