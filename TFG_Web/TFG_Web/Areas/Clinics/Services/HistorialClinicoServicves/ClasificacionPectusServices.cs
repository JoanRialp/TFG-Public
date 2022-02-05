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
    public class ClasificacionPectusServices : IClasificacionPectusServices
    {
        private ClasificacionPectusServicesDAO _clasificacionPectusServices;

        #region "Constructor"
        public ClasificacionPectusServices()
        {
            _clasificacionPectusServices = new ClasificacionPectusServicesDAO();
        }
        #endregion

        [HttpGet]
        public async Task<IEnumerable<ClasificacionPectus>> GetAll()
        {
            var cirugiaPrevia = await _clasificacionPectusServices.GetAllAsync();
            return cirugiaPrevia;
        }

        [HttpGet]
        public async Task<ClasificacionPectus> GetById(string id)
        {
            var cirugiaPrevia = await _clasificacionPectusServices.GetByIdAsync(id);
            if (cirugiaPrevia == null)
            {
                return null;
            }
            return cirugiaPrevia;
        }

        [HttpPost]
        public async Task<string> Create(ClasificacionPectus clasificacionPectus)
        {
            var result = await _clasificacionPectusServices.CreateAsync(clasificacionPectus);
            return result.Id;
        }

        [HttpPut]
        public async Task<string> Update(string id, ClasificacionPectus updatedClasificacionPectus)
        {
            var clasificacionPectus = await _clasificacionPectusServices.GetByIdAsync(id);
            if (clasificacionPectus == null)
            {
                return null;
            }
            await _clasificacionPectusServices.UpdateAsync(id, updatedClasificacionPectus);
            return clasificacionPectus.Id;
        }

        [HttpDelete]
        public async Task<string> Delete(string id)
        {
            var deporte = await _clasificacionPectusServices.GetByIdAsync(id);
            if (deporte == null)
            {
                return null;
            }
            await _clasificacionPectusServices.DeleteAsync(id);
            return deporte.Id;
        }
    }
}
