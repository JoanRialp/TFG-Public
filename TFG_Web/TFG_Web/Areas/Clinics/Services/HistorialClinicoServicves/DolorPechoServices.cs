using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TFG_Web.Areas.Clinics.Models;
using TFG_Web.Areas.Clinics.DAO.HistorialClinicoFormulario;
using TFG_Web.Models.HistorialClinicoCollections;
using TFG_Web.Areas.Clinics.Interface.Services.IHistorialClinicoServices;

namespace TFG_Web.Areas.Clinics.Controllers.HistorialClinicoServices
{
    [Area(Values.AreaClinics)]
    public class DolorPechoServices : IDolorPechoServices
    {
        private DolorPechoServicesDAO _dolorPechoServices;

        #region "Constructor"
        public DolorPechoServices()
        {
            _dolorPechoServices = new DolorPechoServicesDAO();
        }
        #endregion

        [HttpGet]
        public async Task<IEnumerable<DolorPecho>> GetAll()
        {
            var dolorPecho = await _dolorPechoServices.GetAllAsync();
            return dolorPecho;
        }

        [HttpGet]
        public async Task<DolorPecho> GetById(string id)
        {
            var dolorPecho = await _dolorPechoServices.GetByIdAsync(id);
            if (dolorPecho == null)
            {
                return null;
            }
            return dolorPecho;
        }

        [HttpPost]
        public async Task<string> Create(DolorPecho dolorPecho)
        {
            var result = await _dolorPechoServices.CreateAsync(dolorPecho);
            return result.Id;
        }

        [HttpPut]
        public async Task<string> Update(string id, DolorPecho updateddolorPecho)
        {
            var queriedDolorPecho = await _dolorPechoServices.GetByIdAsync(id);
            if (queriedDolorPecho == null)
            {
                return null;
            }
            await _dolorPechoServices.UpdateAsync(id, updateddolorPecho);
            return queriedDolorPecho.Id;
        }

        [HttpDelete]
        public async Task<string> Delete(string id)
        {
            var dolorPecho = await _dolorPechoServices.GetByIdAsync(id);
            if (dolorPecho == null)
            {
                return null;
            }
            await _dolorPechoServices.DeleteAsync(id);
            return dolorPecho.Id;
        }
    }
}
