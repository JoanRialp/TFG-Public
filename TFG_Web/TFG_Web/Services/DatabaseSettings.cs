using Microsoft.AspNetCore.Mvc;
using TFG_Web.Models;

namespace TFG_Web.Services
{
    public class DatabaseSettings : Controller
    {
        private readonly ReadAppSettings readAppSettings = new ReadAppSettings();
        private TFG_DatabaseSettings tFG_DatabaseSettings = new TFG_DatabaseSettings();

        /// <summary>
        /// Cogemos la informacion de MongoDB para poder hacer la conexion
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public TFG_DatabaseSettings GetDatabaseSettings(string collection)
        {
            tFG_DatabaseSettings.Collection = collection;
            tFG_DatabaseSettings.ConnectionString = readAppSettings.ConnectionString; ;
            tFG_DatabaseSettings.DatabaseName = readAppSettings.DatabaseName;

            return tFG_DatabaseSettings;
        }
    }
}