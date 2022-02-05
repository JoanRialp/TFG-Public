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
    public class ClasificacionPectusServicesDAO : IClasificacionPectusServicesDAO
    {
        private readonly IMongoCollection<ClasificacionPectus> _ClasificacionPectus;
        private readonly DatabaseSettings _databaseSettings = new DatabaseSettings();

        /// <summary>
        /// Iniciamos las variables para poder conectarnos a MongoDB
        /// </summary>
        /// <param name="settings"></param>
        #region "Constructor"
        public ClasificacionPectusServicesDAO()
        {
            var DatabaseSettingsPacientes = _databaseSettings.GetDatabaseSettings("ClasificacionPectus");

            var client = new MongoClient(DatabaseSettingsPacientes.ConnectionString);
            var database = client.GetDatabase(DatabaseSettingsPacientes.DatabaseName);
            _ClasificacionPectus = database.GetCollection<ClasificacionPectus>(DatabaseSettingsPacientes.Collection);
        }
        #endregion

        public async Task<List<ClasificacionPectus>> GetAllAsync()
        {
            return await _ClasificacionPectus.Find(s => true).ToListAsync();
        }
        public async Task<ClasificacionPectus> GetByIdAsync(string id)
        {
            return await _ClasificacionPectus.Find<ClasificacionPectus>(s => s.Id == id).FirstOrDefaultAsync();
        }
        public async Task<ClasificacionPectus> CreateAsync(ClasificacionPectus slasificacionPectus)
        {
            await _ClasificacionPectus.InsertOneAsync(slasificacionPectus);
            return slasificacionPectus;
        }
        public async Task UpdateAsync(string id, ClasificacionPectus slasificacionPectus)
        {
            await _ClasificacionPectus.ReplaceOneAsync(s => s.Id == id, slasificacionPectus);
        }
        public async Task DeleteAsync(string id)
        {
            await _ClasificacionPectus.DeleteOneAsync(s => s.Id == id);
        }

    }
}
