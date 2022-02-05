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
    public class AnamnesisServices : IAnamnesisServices
    {
        private AnamnesisServicesDAO _anamnesisServices;

        #region "Constructor"
        public AnamnesisServices()
        {
            _anamnesisServices = new AnamnesisServicesDAO();
        }
        #endregion

        [HttpGet]
        public async Task<IEnumerable<Anamnesis>> GetAll()
        {
            var cirugiaPrevia = await _anamnesisServices.GetAllAsync();
            return cirugiaPrevia;
        }

        [HttpGet]
        public async Task<Anamnesis> GetById(string id)
        {
            var anamnesis = await _anamnesisServices.GetByIdAsync(id);
            if (anamnesis == null)
            {
                return null;
            }
            return anamnesis;
        }

        [HttpPost]
        public async Task<string> Create(Anamnesis anamnesis)
        {
            var result = await _anamnesisServices.CreateAsync(anamnesis);
            return result.Id;
        }

        [HttpPut]
        public async Task<string> Update(string id, Anamnesis updatedanamnesis)
        {
            var queriedAnamnesis = await _anamnesisServices.GetByIdAsync(id);
            if (queriedAnamnesis == null)
            {
                return null;
            }
            await _anamnesisServices.UpdateAsync(id, updatedanamnesis);
            return queriedAnamnesis.Id;
        }

        [HttpDelete]
        public async Task<string> Delete(string id)
        {
            var anamnesis = await _anamnesisServices.GetByIdAsync(id);
            if (anamnesis == null)
            {
                return null;
            }
            await _anamnesisServices.DeleteAsync(id);
            return anamnesis.Id;
        }
    }
}
