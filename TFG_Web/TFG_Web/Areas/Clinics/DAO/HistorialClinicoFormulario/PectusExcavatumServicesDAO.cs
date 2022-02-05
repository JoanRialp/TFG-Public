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
    public class PectusExcavatumServicesDAO : IPectusExcavatumServicesDAO
    {
        private readonly IMongoCollection<PectusExcavatum> _PectusExcavatum;
        private readonly DatabaseSettings _databaseSettings = new DatabaseSettings();

        /// <summary>
        /// Iniciamos las variables para poder conectarnos a MongoDB
        /// </summary>
        /// <param name="settings"></param>
        #region "Constructor"
        public PectusExcavatumServicesDAO()
        {
            var DatabaseSettingsPacientes = _databaseSettings.GetDatabaseSettings("PectusExcavatum");

            var client = new MongoClient(DatabaseSettingsPacientes.ConnectionString);
            var database = client.GetDatabase(DatabaseSettingsPacientes.DatabaseName);
            _PectusExcavatum = database.GetCollection<PectusExcavatum>(DatabaseSettingsPacientes.Collection);
        }
        #endregion

        public async Task<List<PectusExcavatum>> GetAllAsync()
        {
            return await _PectusExcavatum.Find(s => true).ToListAsync();
        }
        public async Task<PectusExcavatum> GetByIdAsync(string id)
        {
            return await _PectusExcavatum.Find<PectusExcavatum>(s => s.Id == id).FirstOrDefaultAsync();
        }
        public async Task<PectusExcavatum> CreateAsync(PectusExcavatum pectusExcavatum)
        {
            await _PectusExcavatum.InsertOneAsync(pectusExcavatum);
            return pectusExcavatum;
        }
        public async Task UpdateAsync(string id, PectusExcavatum pectusExcavatum)
        {
            await _PectusExcavatum.ReplaceOneAsync(s => s.Id == id, pectusExcavatum);
        }
        public async Task DeleteAsync(string id)
        {
            await _PectusExcavatum.DeleteOneAsync(s => s.Id == id);
        }

    }
}
