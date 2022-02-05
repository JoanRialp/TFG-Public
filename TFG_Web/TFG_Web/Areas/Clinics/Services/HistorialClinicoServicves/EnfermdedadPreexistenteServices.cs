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
    public class EnfermdedadPreexistenteServices : IEnfermdedadPreexistenteServices
    {
        private EnfermdedadPreexistenteServicesDAO _enfermdedadPreexistenteServices;

        #region "Constructor"
        public EnfermdedadPreexistenteServices()
        {
            _enfermdedadPreexistenteServices = new EnfermdedadPreexistenteServicesDAO();
        }
        #endregion

        [HttpGet]
        public async Task<IEnumerable<EnfermdedadPreexistente>> GetAll()
        {
            var cirugiaPrevia = await _enfermdedadPreexistenteServices.GetAllAsync();
            return cirugiaPrevia;
        }

        [HttpGet]
        public async Task<EnfermdedadPreexistente> GetById(string id)
        {
            var cirugiaPrevia = await _enfermdedadPreexistenteServices.GetByIdAsync(id);
            if (cirugiaPrevia == null)
            {
                return null;
            }
            return cirugiaPrevia;
        }

        [HttpPost]
        public async Task<string> Create(EnfermdedadPreexistente enfermdedadPreexistente)
        {
            var result = await _enfermdedadPreexistenteServices.CreateAsync(enfermdedadPreexistente);
            return result.Id;
        }

        [HttpPut]
        public async Task<string> Update(string id, EnfermdedadPreexistente updatedEnfermdedadPreexistente)
        {
            var queriedEnfermdedadPreexistente = await _enfermdedadPreexistenteServices.GetByIdAsync(id);
            if (queriedEnfermdedadPreexistente == null)
            {
                return null;
            }
            await _enfermdedadPreexistenteServices.UpdateAsync(id, updatedEnfermdedadPreexistente);
            return queriedEnfermdedadPreexistente.Id;
        }

        [HttpDelete]
        public async Task<string> Delete(string id)
        {
            var enfermdedadPreexistente = await _enfermdedadPreexistenteServices.GetByIdAsync(id);
            if (enfermdedadPreexistente == null)
            {
                return null;
            }
            await _enfermdedadPreexistenteServices.DeleteAsync(id);
            return enfermdedadPreexistente.Id;
        }
    }
}
