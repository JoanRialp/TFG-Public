using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFG_Web.Areas.Clinics.Interface.DAO;
using TFG_Web.Areas.Clinics.Models;
using TFG_Web.Models;
using TFG_Web.Services;

namespace TFG_Web.Areas.Clinics.DAO
{
    [Area(Values.AreaClinics)]
    public class PacientesServicesDAO : IPacientesServicesDAO
    {
        #region Dependencias Interfaces
        private readonly IMongoCollection<Pacientes> _PacientesMongoDB;
        private readonly DatabaseSettings _databaseSettings = new DatabaseSettings();
        private readonly ReadAppSettings _readAppSettings = new ReadAppSettings();
        #endregion

        #region "Constructor"
        /// <summary>
        /// Iniciamos las variables para poder conectarnos a MongoDB
        /// </summary>
        /// <param name="settings"></param>
        public PacientesServicesDAO()
        {
            var DatabaseSettingsPacientes = _databaseSettings.GetDatabaseSettings(_readAppSettings.PacientesName);

            var client = new MongoClient(DatabaseSettingsPacientes.ConnectionString);
            var database = client.GetDatabase(DatabaseSettingsPacientes.DatabaseName);
            _PacientesMongoDB = database.GetCollection<Pacientes>(DatabaseSettingsPacientes.Collection);
        }
        #endregion

        public async Task<List<Pacientes>> GetAllAsync()
        {
            return await _PacientesMongoDB.Find(s => true).ToListAsync();
        }

        public async Task<List<Pacientes>> GetByUsuariosIdAsync(string idUsuario, bool finalizado)
        {
            return await _PacientesMongoDB.Find(s => s.P_Finalizado == finalizado && s.usuarioId == idUsuario).ToListAsync();
        }

        public async Task<List<Pacientes>> GetByFinalizadoAsync(bool finalizado)
        {
            return await _PacientesMongoDB.Find(s => s.P_Finalizado == finalizado).ToListAsync();
        }

        public async Task<Pacientes> GetByIdAsync(string id)
        {
            return await _PacientesMongoDB.Find<Pacientes>(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Pacientes> CreateAsync(Pacientes usuarios)
        {
            await _PacientesMongoDB.InsertOneAsync(usuarios);
            return usuarios;
        }
        public async Task UpdateAsync(string id, Pacientes usuarios)
        {
            await _PacientesMongoDB.ReplaceOneAsync(s => s.Id == id, usuarios);
        }

        public async Task DeleteAsync(string id)
        {
            await _PacientesMongoDB.DeleteOneAsync(s => s.Id == id);
        }
    }
}