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
    public class AnamnesisServicesDAO : IAnamnesisServicesDAO
    {
        private readonly IMongoCollection<Anamnesis> _Anamnesis;
        private readonly DatabaseSettings _databaseSettings = new DatabaseSettings();

        /// <summary>
        /// Iniciamos las variables para poder conectarnos a MongoDB
        /// </summary>
        /// <param name="settings"></param>
        #region "Constructor"
        public AnamnesisServicesDAO()
        {
            var DatabaseSettingsPacientes = _databaseSettings.GetDatabaseSettings("Anamnesis");

            var client = new MongoClient(DatabaseSettingsPacientes.ConnectionString);
            var database = client.GetDatabase(DatabaseSettingsPacientes.DatabaseName);
            _Anamnesis = database.GetCollection<Anamnesis>(DatabaseSettingsPacientes.Collection);
        }
        #endregion

        public async Task<List<Anamnesis>> GetAllAsync()
        {
            return await _Anamnesis.Find(s => true).ToListAsync();
        }
        public async Task<Anamnesis> GetByIdAsync(string id)
        {
            return await _Anamnesis.Find<Anamnesis>(s => s.Id == id).FirstOrDefaultAsync();
        }
        public async Task<Anamnesis> CreateAsync(Anamnesis anamnesis)
        {
            await _Anamnesis.InsertOneAsync(anamnesis);
            return anamnesis;
        }
        public async Task UpdateAsync(string id, Anamnesis anamnesis)
        {
            await _Anamnesis.ReplaceOneAsync(s => s.Id == id, anamnesis);
        }
        public async Task DeleteAsync(string id)
        {
            await _Anamnesis.DeleteOneAsync(s => s.Id == id);
        }

    }
}
