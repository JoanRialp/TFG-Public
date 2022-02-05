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
    public class ControlCampanaPectusExcavatumServicesDAO : IControlCampanaPectusExcavatumServicesDAO
    {
        #region Dependencias Interfaces
        private readonly IMongoCollection<ControlCampanaPectusExcavatum> _ControlCampanaPectusExcavatum;
        private readonly DatabaseSettings _databaseSettings = new DatabaseSettings();
        private readonly ReadAppSettings _readAppSettings = new ReadAppSettings();
        #endregion
        
        #region "Constructor"
        public ControlCampanaPectusExcavatumServicesDAO()
        {
            var DatabaseSettingsCompresor = _databaseSettings.GetDatabaseSettings(_readAppSettings.UsuariosControlCampanaPectusExcavatumName);

            var client = new MongoClient(DatabaseSettingsCompresor.ConnectionString);
            var database = client.GetDatabase(DatabaseSettingsCompresor.DatabaseName);
            _ControlCampanaPectusExcavatum = database.GetCollection<ControlCampanaPectusExcavatum>(DatabaseSettingsCompresor.Collection);
        }
        #endregion

        public async Task<List<ControlCampanaPectusExcavatum>> GetAllAsync()
        {
            return await _ControlCampanaPectusExcavatum.Find(s => true).ToListAsync();
        }

        public async Task<ControlCampanaPectusExcavatum> GetByIdAsync(string id)
        {
            return await _ControlCampanaPectusExcavatum.Find<ControlCampanaPectusExcavatum>(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<ControlCampanaPectusExcavatum>> GetGroupByIdAsync(string[] id)
        {
            var filterDef = new FilterDefinitionBuilder<ControlCampanaPectusExcavatum>();
            var filter = filterDef.In(x => x.Id, id);

            var result = await _ControlCampanaPectusExcavatum.Find(filter).ToListAsync();
            return result;
        }

        public async Task<ControlCampanaPectusExcavatum> CreateAsync(ControlCampanaPectusExcavatum campana)
        {
            await _ControlCampanaPectusExcavatum.InsertOneAsync(campana);
            return campana;
        }

        public async Task UpdateAsync(string id, ControlCampanaPectusExcavatum campana)
        {
            await _ControlCampanaPectusExcavatum.ReplaceOneAsync(s => s.Id == id, campana);
        }

        public async Task DeleteAsync(string id)
        {
            await _ControlCampanaPectusExcavatum.DeleteOneAsync(s => s.Id == id);
        }
    }
}
