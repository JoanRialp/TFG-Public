using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using TFG_Web.Areas.Clinics.Interface.DAO;
using TFG_Web.Areas.Clinics.Models;
using TFG_Web.Models;
using TFG_Web.Services;

namespace TFG_Web.Areas.Clinics.DAO
{
    [Area(Values.AreaClinics)]
    public class ControlSistemaCompresorServicesDAO : IControlSistemaCompresorServicesDAO
    {
        #region Dependencias Interfaces
        private readonly IMongoCollection<ControlSistemaCompresor> _ControlSistemaCompresor;
        private readonly DatabaseSettings _databaseSettings = new DatabaseSettings();
        private readonly ReadAppSettings _readAppSettings = new ReadAppSettings();
        #endregion

        #region "Constructor"
        public ControlSistemaCompresorServicesDAO()
        {
            var DatabaseSettingsCompresor = _databaseSettings.GetDatabaseSettings(_readAppSettings.ControlSistemaCompresorCollectionName);

            var client = new MongoClient(DatabaseSettingsCompresor.ConnectionString);
            var database = client.GetDatabase(DatabaseSettingsCompresor.DatabaseName);
            _ControlSistemaCompresor = database.GetCollection<ControlSistemaCompresor>(DatabaseSettingsCompresor.Collection);
        }
        #endregion

        public async Task<List<ControlSistemaCompresor>> GetAllAsync()
        {
            return await _ControlSistemaCompresor.Find(s => true).ToListAsync();
        }

        public async Task<ControlSistemaCompresor> GetByIdAsync(string id)
        {
            return await _ControlSistemaCompresor.Find<ControlSistemaCompresor>(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<ControlSistemaCompresor>> GetGroupByIdAsync(string[] id)
        {
            var filterDef = new FilterDefinitionBuilder<ControlSistemaCompresor>();
            var filter = filterDef.In(x => x.Id, id);

            var result = await _ControlSistemaCompresor.Find(filter).ToListAsync();
            return result;

        }

        public async Task<ControlSistemaCompresor> CreateAsync(ControlSistemaCompresor compresor)
        {
            await _ControlSistemaCompresor.InsertOneAsync(compresor);
            return compresor;
        }

        public async Task UpdateAsync(string id, ControlSistemaCompresor compresor)
        {
            await _ControlSistemaCompresor.ReplaceOneAsync(s => s.Id == id, compresor);
        }

        public async Task DeleteAsync(string id)
        {
            await _ControlSistemaCompresor.DeleteOneAsync(s => s.Id == id);
        }
    }
}