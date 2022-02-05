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
    public class CirugiaPreviaServicesDAO : ICirugiaPreviaServicesDAO
    {
        private readonly IMongoCollection<CirugiaPrevia> _CirugiaPreviao;
        private readonly DatabaseSettings _databaseSettings = new DatabaseSettings();

        /// <summary>
        /// Iniciamos las variables para poder conectarnos a MongoDB
        /// </summary>
        /// <param name="settings"></param>
        #region "Constructor"
        public CirugiaPreviaServicesDAO()
        {
            var DatabaseSettingsPacientes = _databaseSettings.GetDatabaseSettings("CirugiaPrevia");

            var client = new MongoClient(DatabaseSettingsPacientes.ConnectionString);
            var database = client.GetDatabase(DatabaseSettingsPacientes.DatabaseName);
            _CirugiaPreviao = database.GetCollection<CirugiaPrevia>(DatabaseSettingsPacientes.Collection);
        }
        #endregion

        public async Task<List<CirugiaPrevia>> GetAllAsync()
        {
            return await _CirugiaPreviao.Find(s => true).ToListAsync();
        }
        public async Task<CirugiaPrevia> GetByIdAsync(string id)
        {
            return await _CirugiaPreviao.Find<CirugiaPrevia>(s => s.Id == id).FirstOrDefaultAsync();
        }
        public async Task<CirugiaPrevia> CreateAsync(CirugiaPrevia historialClinico)
        {
            await _CirugiaPreviao.InsertOneAsync(historialClinico);
            return historialClinico;
        }
        public async Task UpdateAsync(string id, CirugiaPrevia historialClinico)
        {
            await _CirugiaPreviao.ReplaceOneAsync(s => s.Id == id, historialClinico);
        }
        public async Task DeleteAsync(string id)
        {
            await _CirugiaPreviao.DeleteOneAsync(s => s.Id == id);
        }

    }
}
