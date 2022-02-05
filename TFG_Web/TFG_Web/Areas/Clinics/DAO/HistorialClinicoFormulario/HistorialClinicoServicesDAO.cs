using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFG_Web.Areas.Clinics.Interface.DAO.HistorialClinicoInterfaces;
using TFG_Web.Areas.Clinics.Models;
using TFG_Web.Models;
using TFG_Web.Services;

namespace TFG_Web.Areas.Clinics.DAO.HistorialClinicoFormulario
{
    [Area(Values.AreaClinics)]
    public class HistorialClinicoServicesDAO : IHistorialClinicoServicesDAO
    {
        private readonly IMongoCollection<HistorialClinico> _HistorialClinico;
        private readonly DatabaseSettings _databaseSettings = new DatabaseSettings();

        /// <summary>
        /// Iniciamos las variables para poder conectarnos a MongoDB
        /// </summary>
        /// <param name="settings"></param>
        #region "Constructor"
        public HistorialClinicoServicesDAO()
        {
            var DatabaseSettingsPacientes = _databaseSettings.GetDatabaseSettings("HistorialClinico");

            var client = new MongoClient(DatabaseSettingsPacientes.ConnectionString);
            var database = client.GetDatabase(DatabaseSettingsPacientes.DatabaseName);
            _HistorialClinico = database.GetCollection<HistorialClinico>(DatabaseSettingsPacientes.Collection);
        }
        #endregion

        public async Task<List<HistorialClinico>> GetAllAsync()
        {
            return await _HistorialClinico.Find(s => true).ToListAsync();
        }
        public async Task<HistorialClinico> GetByIdAsync(string id)
        {
            return await _HistorialClinico.Find<HistorialClinico>(s => s.Id == id).FirstOrDefaultAsync();
        }
        public async Task<HistorialClinico> CreateAsync(HistorialClinico historialClinico)
        {
            await _HistorialClinico.InsertOneAsync(historialClinico);
            return historialClinico;
        }
        public async Task UpdateAsync(string id, HistorialClinico historialClinico)
        {
            await _HistorialClinico.ReplaceOneAsync(s => s.Id == id, historialClinico);
        }
        public async Task DeleteAsync(string id)
        {
            await _HistorialClinico.DeleteOneAsync(s => s.Id == id);
        }

    }
}
