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
    public class PectusMixtoServicesDAO : IPectusMixtoServicesDAO
    {
        private readonly IMongoCollection<PectusMixto> _PectusMixto;
        private readonly DatabaseSettings _databaseSettings = new DatabaseSettings();

        /// <summary>
        /// Iniciamos las variables para poder conectarnos a MongoDB
        /// </summary>
        /// <param name="settings"></param>
        #region "Constructor"
        public PectusMixtoServicesDAO()
        {
            var DatabaseSettingsPacientes = _databaseSettings.GetDatabaseSettings("PectusMixto");

            var client = new MongoClient(DatabaseSettingsPacientes.ConnectionString);
            var database = client.GetDatabase(DatabaseSettingsPacientes.DatabaseName);
            _PectusMixto = database.GetCollection<PectusMixto>(DatabaseSettingsPacientes.Collection);
        }
        #endregion

        public async Task<List<PectusMixto>> GetAllAsync()
        {
            return await _PectusMixto.Find(s => true).ToListAsync();
        }
        public async Task<PectusMixto> GetByIdAsync(string id)
        {
            return await _PectusMixto.Find<PectusMixto>(s => s.Id == id).FirstOrDefaultAsync();
        }
        public async Task<PectusMixto> CreateAsync(PectusMixto pectusMixto)
        {
            await _PectusMixto.InsertOneAsync(pectusMixto);
            return pectusMixto;
        }
        public async Task UpdateAsync(string id, PectusMixto pectusMixto)
        {
            await _PectusMixto.ReplaceOneAsync(s => s.Id == id, pectusMixto);
        }
        public async Task DeleteAsync(string id)
        {
            await _PectusMixto.DeleteOneAsync(s => s.Id == id);
        }

    }
}
