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
    public class UsuariosServicesDAO : IUsuariosServicesDAO
    {
        #region Dependencias Interfaces
        private readonly IMongoCollection<Usuarios> _Usuarios;
        private readonly DatabaseSettings _databaseSettings = new DatabaseSettings();
        private readonly ReadAppSettings _readAppSettings = new ReadAppSettings();
        #endregion

        #region "Constructor"
        /// <summary>
        /// Iniciamos las variables para poder conectarnos a MongoDB
        /// </summary>
        /// <param name="settings"></param>
        public UsuariosServicesDAO()
        {
            var DatabaseSettingsPacientes = _databaseSettings.GetDatabaseSettings(_readAppSettings.UsuariosName);

            var client = new MongoClient(DatabaseSettingsPacientes.ConnectionString);
            var database = client.GetDatabase(DatabaseSettingsPacientes.DatabaseName);
            _Usuarios = database.GetCollection<Usuarios>(DatabaseSettingsPacientes.Collection);
        }
        #endregion

        public async Task<List<Usuarios>> GetAllAsync()
        {
            return await _Usuarios.Find(s => true).ToListAsync();
        }

        public async Task<List<Usuarios>> GetByGroupAsync(int group)
        {
            return await _Usuarios.Find<Usuarios>(s => s.P_Grupo == group).ToListAsync();
        }

        public async Task<Usuarios> GetByIdAsync(string id)
        {
            return await _Usuarios.Find<Usuarios>(s => s.Id == id).FirstOrDefaultAsync();
        }
        public async Task<Usuarios> GetAccesUserAsync(string username, string password)
        {
            return await _Usuarios.Find<Usuarios>(s => s.P_Password == password && s.P_Username == username).FirstOrDefaultAsync();
        }
        public async Task<Usuarios> CreateAsync(Usuarios usuarios)
        {
            await _Usuarios.InsertOneAsync(usuarios);
            return usuarios;
        }
        public async Task UpdateAsync(string id, Usuarios usuarios)
        {
            await _Usuarios.ReplaceOneAsync(s => s.Id == id, usuarios);
        }
        public async Task DeleteAsync(string id)
        {
            await _Usuarios.DeleteOneAsync(s => s.Id == id);
        }

        public async Task<Usuarios> FindCorreoAsync(string correo)
        {
            return await _Usuarios.Find<Usuarios>(u => u.P_Correo == correo).FirstOrDefaultAsync();
        }
    }
}