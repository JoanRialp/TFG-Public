using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFG_Web.Models;
using TFG_Web.Models.HistorialClinicoCollections;

namespace TFG_Web.Areas.Clinics.Interface.DAO.HistorialClinicoInterfaces
{
    interface ISignosSintomasClinicosServicesDAO
    {
        Task<List<SignosSintomasClinicos>> GetAllAsync();
        Task<SignosSintomasClinicos> GetByIdAsync(string id);
        Task<SignosSintomasClinicos> CreateAsync(SignosSintomasClinicos signosSintomasClinicos);
        Task UpdateAsync(string id, SignosSintomasClinicos signosSintomasClinicos);
        Task DeleteAsync(string id);
    }
}
