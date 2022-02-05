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
    public class PectusCarinatumServices : IPectusCarinatumServices
    {
        private PectusCarinatumServicesDAO _pectusCarinatumServices;

        #region "Constructor"
        public PectusCarinatumServices()
        {
            _pectusCarinatumServices = new PectusCarinatumServicesDAO();
        }
        #endregion

        [HttpGet]
        public async Task<IEnumerable<PectusCarinatum>> GetAll()
        {
            var pectusCarinatum = await _pectusCarinatumServices.GetAllAsync();
            return pectusCarinatum;
        }

        [HttpGet]
        public async Task<PectusCarinatum> GetById(string id)
        {
            var pectusCarinatum = await _pectusCarinatumServices.GetByIdAsync(id);
            if (pectusCarinatum == null)
            {
                return null;
            }
            return pectusCarinatum;
        }

        [HttpPost]
        public async Task<string> Create(PectusCarinatum pectusCarinatum)
        {
            var result = await _pectusCarinatumServices.CreateAsync(pectusCarinatum);
            return result.Id;
        }

        [HttpPut]
        public async Task<string> Update(string id, PectusCarinatum updatePectusCarinatum)
        {
            var queriedAnamnesis = await _pectusCarinatumServices.GetByIdAsync(id);
            if (queriedAnamnesis == null)
            {
                return null;
            }
            await _pectusCarinatumServices.UpdateAsync(id, updatePectusCarinatum);
            return queriedAnamnesis.Id;
        }

        [HttpDelete]
        public async Task<string> Delete(string id)
        {
            var pectusCarinatum = await _pectusCarinatumServices.GetByIdAsync(id);
            if (pectusCarinatum == null)
            {
                return null;
            }
            await _pectusCarinatumServices.DeleteAsync(id);
            return pectusCarinatum.Id;
        }
    }
}
