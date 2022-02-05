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
    public class SindromePolandServices : ISindromePolandServices
    {
        private SindromePolandServicesDAO _sindromePolandServices;

        #region "Constructor"
        public SindromePolandServices()
        {
            _sindromePolandServices = new SindromePolandServicesDAO();
        }
        #endregion

        [HttpGet]
        public async Task<IEnumerable<SindromePoland>> GetAll()
        {
            var sindromePoland = await _sindromePolandServices.GetAllAsync();
            return sindromePoland;
        }

        [HttpGet]
        public async Task<SindromePoland> GetById(string id)
        {
            var sindromePoland = await _sindromePolandServices.GetByIdAsync(id);
            if (sindromePoland == null)
            {
                return null;
            }
            else
            {
                return sindromePoland;
            }
        }

        [HttpPost]
        public async Task<string> Create(SindromePoland sindromePoland)
        {
            var result = await _sindromePolandServices.CreateAsync(sindromePoland);
            return result.Id;
        }

        [HttpPut]
        public async Task<string> Update(string id, SindromePoland updatedSindromePoland)
        {
            var queriedSindromePoland = await _sindromePolandServices.GetByIdAsync(id);
            if (queriedSindromePoland == null)
            {
                return null;
            }
            await _sindromePolandServices.UpdateAsync(id, updatedSindromePoland);
            return queriedSindromePoland.Id;
        }

        [HttpDelete]
        public async Task<string> Delete(string id)
        {
            var sindromePoland = await _sindromePolandServices.GetByIdAsync(id);
            if (sindromePoland == null)
            {
                return null;
            }
            await _sindromePolandServices.DeleteAsync(id);
            return sindromePoland.Id;
        }
    }
}
