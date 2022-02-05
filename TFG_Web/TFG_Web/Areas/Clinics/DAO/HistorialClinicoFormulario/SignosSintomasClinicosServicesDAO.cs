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
    public class SignosSintomasClinicosServicesDAO : ISignosSintomasClinicosServicesDAO
    {
        private readonly IMongoCollection<SignosSintomasClinicos> _SignosSintomasClinicos;
        private readonly DatabaseSettings _databaseSettings = new DatabaseSettings();

        /// <summary>
        /// Iniciamos las variables para poder conectarnos a MongoDB
        /// </summary>
        /// <param name="settings"></param>
        #region "Constructor"
        public SignosSintomasClinicosServicesDAO()
        {
            var DatabaseSettingsPacientes = _databaseSettings.GetDatabaseSettings("SignosSintomasClinicos");

            var client = new MongoClient(DatabaseSettingsPacientes.ConnectionString);
            var database = client.GetDatabase(DatabaseSettingsPacientes.DatabaseName);
            _SignosSintomasClinicos = database.GetCollection<SignosSintomasClinicos>(DatabaseSettingsPacientes.Collection);
        }
        #endregion

        public async Task<List<SignosSintomasClinicos>> GetAllAsync()
        {
            return await _SignosSintomasClinicos.Find(s => true).ToListAsync();
        }
        public async Task<SignosSintomasClinicos> GetByIdAsync(string id)
        {
            return await _SignosSintomasClinicos.Find<SignosSintomasClinicos>(s => s.Id == id).FirstOrDefaultAsync();
        }
        public async Task<SignosSintomasClinicos> CreateAsync(SignosSintomasClinicos signosSintomasClinicos)
        {
            await _SignosSintomasClinicos.InsertOneAsync(signosSintomasClinicos);
            return signosSintomasClinicos;
        }
        public async Task UpdateAsync(string id, SignosSintomasClinicos signosSintomasClinicos)
        {
            await _SignosSintomasClinicos.ReplaceOneAsync(s => s.Id == id, signosSintomasClinicos);
        }
        public async Task DeleteAsync(string id)
        {
            await _SignosSintomasClinicos.DeleteOneAsync(s => s.Id == id);
        }

    }
}
