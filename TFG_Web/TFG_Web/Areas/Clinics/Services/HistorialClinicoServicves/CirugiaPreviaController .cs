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
    public class CirugiaPreviaServices : ICirugiaPreviaServices
    {
        private CirugiaPreviaServicesDAO _cirugiaPreviaServices;

        #region "Constructor"
        public CirugiaPreviaServices()
        {
            _cirugiaPreviaServices = new CirugiaPreviaServicesDAO();
        }
        #endregion

        [HttpGet]
        public async Task<IEnumerable<CirugiaPrevia>> GetAll()
        {
            var cirugiaPrevia = await _cirugiaPreviaServices.GetAllAsync();
            return cirugiaPrevia;
        }

        [HttpGet]
        public async Task<CirugiaPrevia> GetById(string id)
        {
            var cirugiaPrevia = await _cirugiaPreviaServices.GetByIdAsync(id);
            if (cirugiaPrevia == null)
            {
                return null;
            }
            return cirugiaPrevia;
        }

        [HttpPost]
        public async Task<string> Create(CirugiaPrevia cirugiaPrevia)
        {
            var result = await _cirugiaPreviaServices.CreateAsync(cirugiaPrevia);
            return result.Id;
        }

        [HttpPut]
        public async Task<string> Update(string id, CirugiaPrevia updatedcirugiaPrevia)
        {
            var queriedCirugiaPrevia = await _cirugiaPreviaServices.GetByIdAsync(id);
            if (queriedCirugiaPrevia == null)
            {
                return null;
            }
            try
            {
                await _cirugiaPreviaServices.UpdateAsync(id, updatedcirugiaPrevia);
                return queriedCirugiaPrevia.Id;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpDelete]
        public async Task<string> Delete(string id)
        {
            var cirugiaPrevia = await _cirugiaPreviaServices.GetByIdAsync(id);
            if (cirugiaPrevia == null)
            {
                return null;
            }
            await _cirugiaPreviaServices.DeleteAsync(id);
            return cirugiaPrevia.Id;
        }
    }
}
