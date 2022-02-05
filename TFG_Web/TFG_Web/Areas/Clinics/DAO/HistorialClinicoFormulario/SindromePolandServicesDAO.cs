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
    public class SindromePolandServicesDAO : ISindromePolandServicesDAO
    {
        private readonly IMongoCollection<SindromePoland> _SindromePoland;
        private readonly DatabaseSettings _databaseSettings = new DatabaseSettings();

        /// <summary>
        /// Iniciamos las variables para poder conectarnos a MongoDB
        /// </summary>
        /// <param name="settings"></param>
        #region "Constructor"
        public SindromePolandServicesDAO()
        {
            var DatabaseSettingsPacientes = _databaseSettings.GetDatabaseSettings("SindromePoland");

            var client = new MongoClient(DatabaseSettingsPacientes.ConnectionString);
            var database = client.GetDatabase(DatabaseSettingsPacientes.DatabaseName);
            _SindromePoland = database.GetCollection<SindromePoland>(DatabaseSettingsPacientes.Collection);
        }
        #endregion

        public async Task<List<SindromePoland>> GetAllAsync()
        {
            return await _SindromePoland.Find(s => true).ToListAsync();
        }
        public async Task<SindromePoland> GetByIdAsync(string id)
        {
            return await _SindromePoland.Find<SindromePoland>(s => s.Id == id).FirstOrDefaultAsync();
        }
        public async Task<SindromePoland> CreateAsync(SindromePoland sindromePoland)
        {
            await _SindromePoland.InsertOneAsync(sindromePoland);
            return sindromePoland;
        }
        public async Task UpdateAsync(string id, SindromePoland sindromePoland)
        {
            await _SindromePoland.ReplaceOneAsync(s => s.Id == id, sindromePoland);
        }
        public async Task DeleteAsync(string id)
        {
            await _SindromePoland.DeleteOneAsync(s => s.Id == id);
        }

    }
}
