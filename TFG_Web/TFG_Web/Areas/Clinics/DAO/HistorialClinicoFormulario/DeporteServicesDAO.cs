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
    public class DeporteServicesDAO : IDeporteServicesDAO
    {
        private readonly IMongoCollection<Deporte> _Deporte;
        private readonly DatabaseSettings _databaseSettings = new DatabaseSettings();

        /// <summary>
        /// Iniciamos las variables para poder conectarnos a MongoDB
        /// </summary>
        /// <param name="settings"></param>
        #region "Constructor"
        public DeporteServicesDAO()
        {
            var DatabaseSettingsPacientes = _databaseSettings.GetDatabaseSettings("Deporte");

            var client = new MongoClient(DatabaseSettingsPacientes.ConnectionString);
            var database = client.GetDatabase(DatabaseSettingsPacientes.DatabaseName);
            _Deporte = database.GetCollection<Deporte>(DatabaseSettingsPacientes.Collection);
        }
        #endregion

        public async Task<List<Deporte>> GetAllAsync()
        {
            return await _Deporte.Find(s => true).ToListAsync();
        }
        public async Task<Deporte> GetByIdAsync(string id)
        {
            return await _Deporte.Find<Deporte>(s => s.Id == id).FirstOrDefaultAsync();
        }
        public async Task<Deporte> CreateAsync(Deporte historialClinico)
        {
            await _Deporte.InsertOneAsync(historialClinico);
            return historialClinico;
        }
        public async Task UpdateAsync(string id, Deporte historialDeporte)
        {
            await _Deporte.ReplaceOneAsync(s => s.Id == id, historialDeporte);
        }
        public async Task DeleteAsync(string id)
        {
            await _Deporte.DeleteOneAsync(s => s.Id == id);
        }

    }
}
