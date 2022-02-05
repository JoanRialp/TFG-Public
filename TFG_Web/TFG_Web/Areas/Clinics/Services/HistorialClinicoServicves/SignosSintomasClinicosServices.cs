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
    public class SignosSintomasClinicosServices : ISignosSintomasClinicosServices
    {
        private SignosSintomasClinicosServicesDAO _signosSintomasClinicosServices;

        #region "Constructor"
        public SignosSintomasClinicosServices()
        {
            _signosSintomasClinicosServices = new SignosSintomasClinicosServicesDAO();
        }
        #endregion

        [HttpGet]
        public async Task<IEnumerable<SignosSintomasClinicos>> GetAll()
        {
            var signosSintomasClinicos = await _signosSintomasClinicosServices.GetAllAsync();
            return signosSintomasClinicos;
        }

        [HttpGet]
        public async Task<SignosSintomasClinicos> GetById(string id)
        {
            var signosSintomasClinicos = await _signosSintomasClinicosServices.GetByIdAsync(id);
            if (signosSintomasClinicos == null)
            {
                return null;
            }
            return signosSintomasClinicos;
        }

        [HttpPost]
        public async Task<string> Create(SignosSintomasClinicos signosSintomasClinicos)
        {
            var result = await _signosSintomasClinicosServices.CreateAsync(signosSintomasClinicos);
            return result.Id;
        }

        [HttpPut]
        public async Task<string> Update(string id, SignosSintomasClinicos updatedsignosSintomasClinicos)
        {
            var queriedDeporte = await _signosSintomasClinicosServices.GetByIdAsync(id);
            if (queriedDeporte == null)
            {
                return null;
            }
            await _signosSintomasClinicosServices.UpdateAsync(id, updatedsignosSintomasClinicos);
            return queriedDeporte.Id;
        }

        [HttpDelete]
        public async Task<string> Delete(string id)
        {
            var deporte = await _signosSintomasClinicosServices.GetByIdAsync(id);
            if (deporte == null)
            {
                return null;
            }
            await _signosSintomasClinicosServices.DeleteAsync(id);
            await _signosSintomasClinicosServices.DeleteAsync(id);
            return deporte.Id;
        }
    }
}
