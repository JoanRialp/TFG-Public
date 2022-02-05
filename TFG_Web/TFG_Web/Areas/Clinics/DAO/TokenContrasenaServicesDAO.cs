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
    public class TokenContrasenaServicesDAO : ITokenContrasenaServicesDAO
    {
        #region Dependencias Interfaces
        private readonly IMongoCollection<TokenContrasena> _TokenContrasena;
        private readonly DatabaseSettings _databaseSettings = new DatabaseSettings();
        private readonly ReadAppSettings _readAppSettings = new ReadAppSettings();
        #endregion

        #region "Constructor"
        /// <summary>
        /// Iniciamos las variables para poder conectarnos a MongoDB
        /// </summary>
        /// <param name="settings"></param>
        public TokenContrasenaServicesDAO()
        {
            var DatabaseSettingsTokenContrasena = _databaseSettings.GetDatabaseSettings(_readAppSettings.TokenContrasenaName);

            var client = new MongoClient(DatabaseSettingsTokenContrasena.ConnectionString);
            var database = client.GetDatabase(DatabaseSettingsTokenContrasena.DatabaseName);
            _TokenContrasena = database.GetCollection<TokenContrasena>(DatabaseSettingsTokenContrasena.Collection);
        }
        #endregion

        public async Task<List<TokenContrasena>> GetAllAsync()
        {
            return await _TokenContrasena.Find(s => true).ToListAsync();
        }

        public async Task<TokenContrasena> GetByIdAsync(string id)
        {
            return await _TokenContrasena.Find<TokenContrasena>(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task<TokenContrasena> GetByTokenAsync(string idToken)
        {
            return await _TokenContrasena.Find<TokenContrasena>(t => t.TC_Token == idToken).FirstOrDefaultAsync();
        }

        public async Task<TokenContrasena> CreateAsync(TokenContrasena token)
        {
            await _TokenContrasena.InsertOneAsync(token);
            return token;
        }

        public async Task UpdateAsync(string id, TokenContrasena token)
        {
            await _TokenContrasena.ReplaceOneAsync(s => s.Id == id, token);
        }

        public async Task DeleteAsync(string id)
        {
            await _TokenContrasena.DeleteOneAsync(t => t.Id == id);
        }

        public async Task DeleteAllAsync()
        {
            await _TokenContrasena.DeleteManyAsync(t => true);
        }
    }
}