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
    public class PectusMixtoServices : IPectusMixtoServices
    {
        private PectusMixtoServicesDAO _pectusMixtoServices;

        #region "Constructor"
        public PectusMixtoServices()
        {
            _pectusMixtoServices = new PectusMixtoServicesDAO();
        }
        #endregion

        [HttpGet]
        public async Task<IEnumerable<PectusMixto>> GetAll()
        {
            var cirugiaPrevia = await _pectusMixtoServices.GetAllAsync();
            return cirugiaPrevia;
        }

        [HttpGet]
        public async Task<PectusMixto> GetById(string id)
        {
            var pectusMixto = await _pectusMixtoServices.GetByIdAsync(id);
            if (pectusMixto == null)
            {
                return null;
            }
            return pectusMixto;
        }

        [HttpPost]
        public async Task<string> Create(PectusMixto pectusMixto)
        {
            var result = await _pectusMixtoServices.CreateAsync(pectusMixto);
            return result.Id;
        }

        [HttpPut]
        public async Task<string> Update(string id, PectusMixto updatedPectusMixto)
        {
            var queriedPectusMixto = await _pectusMixtoServices.GetByIdAsync(id);
            if (queriedPectusMixto == null)
            {
                return null;
            }
            await _pectusMixtoServices.UpdateAsync(id, updatedPectusMixto);
            return queriedPectusMixto.Id;
        }

        [HttpDelete]
        public async Task<string> Delete(string id)
        {
            var pectusMixto = await _pectusMixtoServices.GetByIdAsync(id);
            if (pectusMixto == null)
            {
                return null;
            }
            await _pectusMixtoServices.DeleteAsync(id);
            return pectusMixto.Id;
        }
    }
}
