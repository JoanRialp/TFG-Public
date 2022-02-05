using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TFG_Web.Areas.Clinics.Models;
using TFG_Web.Areas.Clinics.Services;
using TFG_Web.Areas.Clinics.DAO.HistorialClinicoFormulario;
using TFG_Web.Models;
using TFG_Web.Areas.Clinics.Interface.Services.IHistorialClinicoServices;

namespace TFG_Web.Areas.Clinics.Services.HistorialClinicoServices
{
    [Area(Values.AreaClinics)]
    public class HistorialClinicoServices : IHistorialClinicoServices
    {
        private HistorialClinicoServicesDAO _historialClinicoServices;

        #region "Constructor"
        public HistorialClinicoServices()
        {
            _historialClinicoServices = new HistorialClinicoServicesDAO();
        }
        #endregion

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HistorialClinico>>> GetAll()
        {
            var HistorialClinico = await _historialClinicoServices.GetAllAsync();
            return HistorialClinico;
        }

        [HttpGet]
        public async Task<HistorialClinico> GetById(string id)
        {
            var HistorialClinico = await _historialClinicoServices.GetByIdAsync(id);
            if (HistorialClinico == null)
            {
                return null;
            }
            return HistorialClinico;
        }

        [HttpPost]
        public async Task<string> Create(HistorialClinico historialClinico)
        {
            var result = await _historialClinicoServices.CreateAsync(historialClinico);
            return result.Id;
        }

        [HttpPut]
        public async Task<string> Update(string id, HistorialClinico updatedHistorialClinico)
        {
            var queriedHistorialClinico = await _historialClinicoServices.GetByIdAsync(id);
            if (queriedHistorialClinico == null)
            {
                return null;
            }
            await _historialClinicoServices.UpdateAsync(id, updatedHistorialClinico);
            return queriedHistorialClinico.Id;
        }

        [HttpDelete]
        public async Task<string> Delete(string id)
        {
            var historialClinico = await _historialClinicoServices.GetByIdAsync(id);
            if (historialClinico == null)
            {
                return null;
            }
            await _historialClinicoServices.DeleteAsync(id);
            return historialClinico.Id;
        }
    }
}
