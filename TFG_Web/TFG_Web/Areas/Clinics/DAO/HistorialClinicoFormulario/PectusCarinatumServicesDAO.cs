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
    public class PectusCarinatumServicesDAO : IPectusCarinatumServicesDAO
    {
        private readonly IMongoCollection<PectusCarinatum> _PectusCarinatum;
        private readonly DatabaseSettings _databaseSettings = new DatabaseSettings();

        /// <summary>
        /// Iniciamos las variables para poder conectarnos a MongoDB
        /// </summary>
        /// <param name="settings"></param>
        #region "Constructor"
        public PectusCarinatumServicesDAO()
        {
            var DatabaseSettingsPacientes = _databaseSettings.GetDatabaseSettings("PectusCarinatum");

            var client = new MongoClient(DatabaseSettingsPacientes.ConnectionString);
            var database = client.GetDatabase(DatabaseSettingsPacientes.DatabaseName);
            _PectusCarinatum = database.GetCollection<PectusCarinatum>(DatabaseSettingsPacientes.Collection);
        }
        #endregion

        public async Task<List<PectusCarinatum>> GetAllAsync()
        {
            return await _PectusCarinatum.Find(s => true).ToListAsync();
        }
        public async Task<PectusCarinatum> GetByIdAsync(string id)
        {
            return await _PectusCarinatum.Find<PectusCarinatum>(s => s.Id == id).FirstOrDefaultAsync();
        }
        public async Task<PectusCarinatum> CreateAsync(PectusCarinatum pectusCarinatum)
        {
            await _PectusCarinatum.InsertOneAsync(pectusCarinatum);
            return pectusCarinatum;
        }
        public async Task UpdateAsync(string id, PectusCarinatum pectusCarinatum)
        {
            await _PectusCarinatum.ReplaceOneAsync(s => s.Id == id, pectusCarinatum);
        }
        public async Task DeleteAsync(string id)
        {
            await _PectusCarinatum.DeleteOneAsync(s => s.Id == id);
        }

    }
}
