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
    public class EnfermdedadPreexistenteServicesDAO : IEnfermdedadPreexistenteServicesDAO
    {
        private readonly IMongoCollection<EnfermdedadPreexistente> _EnfermdedadPreexistente;
        private readonly DatabaseSettings _databaseSettings = new DatabaseSettings();

        /// <summary>
        /// Iniciamos las variables para poder conectarnos a MongoDB
        /// </summary>
        /// <param name="settings"></param>
        #region "Constructor"
        public EnfermdedadPreexistenteServicesDAO()
        {
            var DatabaseSettingsPacientes = _databaseSettings.GetDatabaseSettings("EnfermdedadPreexistente");

            var client = new MongoClient(DatabaseSettingsPacientes.ConnectionString);
            var database = client.GetDatabase(DatabaseSettingsPacientes.DatabaseName);
            _EnfermdedadPreexistente = database.GetCollection<EnfermdedadPreexistente>(DatabaseSettingsPacientes.Collection);
        }
        #endregion

        public async Task<List<EnfermdedadPreexistente>> GetAllAsync()
        {
            return await _EnfermdedadPreexistente.Find(s => true).ToListAsync();
        }
        public async Task<EnfermdedadPreexistente> GetByIdAsync(string id)
        {
            return await _EnfermdedadPreexistente.Find<EnfermdedadPreexistente>(s => s.Id == id).FirstOrDefaultAsync();
        }
        public async Task<EnfermdedadPreexistente> CreateAsync(EnfermdedadPreexistente historialClinico)
        {
            await _EnfermdedadPreexistente.InsertOneAsync(historialClinico);
            return historialClinico;
        }
        public async Task UpdateAsync(string id, EnfermdedadPreexistente enfermdedadPreexistente)
        {
            await _EnfermdedadPreexistente.ReplaceOneAsync(s => s.Id == id, enfermdedadPreexistente);
        }
        public async Task DeleteAsync(string id)
        {
            await _EnfermdedadPreexistente.DeleteOneAsync(s => s.Id == id);
        }

    }
}
