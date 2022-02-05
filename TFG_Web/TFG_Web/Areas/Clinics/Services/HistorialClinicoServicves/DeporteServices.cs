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
    public class DeporteServices : IDeporteServices
    {
        private DeporteServicesDAO _deporteServices;

        #region "Constructor"
        public DeporteServices()
        {
            _deporteServices = new DeporteServicesDAO();
        }
        #endregion

        [HttpGet]
        public async Task<IEnumerable<Deporte>> GetAll()
        {
            var cirugiaPrevia = await _deporteServices.GetAllAsync();
            return cirugiaPrevia;
        }

        [HttpGet]
        public async Task<Deporte> GetById(string id)
        {
            var cirugiaPrevia = await _deporteServices.GetByIdAsync(id);
            if (cirugiaPrevia == null)
            {
                return null;
            }
            return cirugiaPrevia;
        }

        [HttpPost]
        public async Task<string> Create(Deporte deporte)
        {
            var result = await _deporteServices.CreateAsync(deporte);
            return result.Id;
        }

        [HttpPut]
        public async Task<string> Update(string id, Deporte updatedDeporte)
        {
            var queriedDeporte = await _deporteServices.GetByIdAsync(id);
            if (queriedDeporte == null)
            {
                return null;
            }
            await _deporteServices.UpdateAsync(id, updatedDeporte);
            return queriedDeporte.Id;
        }

        [HttpDelete]
        public async Task<string> Delete(string id)
        {
            var deporte = await _deporteServices.GetByIdAsync(id);
            if (deporte == null)
            {
                return null;
            }
            await _deporteServices.DeleteAsync(id);
            return deporte.Id;
        }
    }
}
