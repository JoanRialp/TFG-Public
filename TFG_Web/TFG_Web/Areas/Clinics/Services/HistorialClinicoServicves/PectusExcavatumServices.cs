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
    public class PectusExcavatumServices : IPectusExcavatumServices
    {
        private PectusExcavatumServicesDAO _pectusExcavatumServices;

        #region "Constructor"
        public PectusExcavatumServices()
        {
            _pectusExcavatumServices = new PectusExcavatumServicesDAO();
        }
        #endregion

        [HttpGet]
        public async Task<IEnumerable<PectusExcavatum>> GetAll()
        {
            var pectusExcavatum = await _pectusExcavatumServices.GetAllAsync();
            return pectusExcavatum;
        }

        [HttpGet]
        public async Task<PectusExcavatum> GetById(string id)
        {
            var pectusExcavatum = await _pectusExcavatumServices.GetByIdAsync(id);
            if (pectusExcavatum == null)
            {
                return null;
            }
            return pectusExcavatum;
        }

        [HttpPost]
        public async Task<string> Create(PectusExcavatum pectusExcavatum)
        {
            var result = await _pectusExcavatumServices.CreateAsync(pectusExcavatum);
            return result.Id;
        }

        [HttpPut]
        public async Task<string> Update(string id, PectusExcavatum updatedPectusExcavatum)
        {
            var queriedPectusExcavatum = await _pectusExcavatumServices.GetByIdAsync(id);
            if (queriedPectusExcavatum == null)
            {
                return null;
            }
            await _pectusExcavatumServices.UpdateAsync(id, updatedPectusExcavatum);
            return queriedPectusExcavatum.Id;
        }

        [HttpDelete]
        public async Task<string> Delete(string id)
        {
            var deporte = await _pectusExcavatumServices.GetByIdAsync(id);
            if (deporte == null)
            {
                return null;
            }
            await _pectusExcavatumServices.DeleteAsync(id);
            return deporte.Id;
        }
    }
}
