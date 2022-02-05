using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFG_Web.Areas.Clinics.Interface.DAO.HistorialClinicoInterfaces;
using TFG_Web.Areas.Clinics.Models;
using TFG_Web.Models.HistorialClinicoCollections;
using TFG_Web.Services;

namespace TFG_Web.Areas.Clinics.DAO.HistorialClinicoFormulario
{
    [Area(Values.AreaClinics)]
    public class DolorPechoServicesDAO : IDolorPechoServicesDAO
    {
        private readonly IMongoCollection<DolorPecho> _DolorPecho;
        private readonly DatabaseSettings _databaseSettings = new DatabaseSettings();

        /// <summary>
        /// Iniciamos las variables para poder conectarnos a MongoDB
        /// </summary>
        /// <param name="settings"></param>
        #region "Constructor"
        public DolorPechoServicesDAO()
        {
            var DatabaseSettingsPacientes = _databaseSettings.GetDatabaseSettings("DolorPecho");

            var client = new MongoClient(DatabaseSettingsPacientes.ConnectionString);
            var database = client.GetDatabase(DatabaseSettingsPacientes.DatabaseName);
            _DolorPecho = database.GetCollection<DolorPecho>(DatabaseSettingsPacientes.Collection);
        }
        #endregion

        public async Task<List<DolorPecho>> GetAllAsync()
        {
            return await _DolorPecho.Find(s => true).ToListAsync();
        }
        public async Task<DolorPecho> GetByIdAsync(string id)
        {
            return await _DolorPecho.Find<DolorPecho>(s => s.Id == id).FirstOrDefaultAsync();
        }
        public async Task<DolorPecho> CreateAsync(DolorPecho historialDolorPecho)
        {
            await _DolorPecho.InsertOneAsync(historialDolorPecho);
            return historialDolorPecho;
        }
        public async Task UpdateAsync(string id, DolorPecho historialDolorPecho)
        {
            await _DolorPecho.ReplaceOneAsync(s => s.Id == id, historialDolorPecho);
        }
        public async Task DeleteAsync(string id)
        {
            await _DolorPecho.DeleteOneAsync(s => s.Id == id);
        }

    }
}
