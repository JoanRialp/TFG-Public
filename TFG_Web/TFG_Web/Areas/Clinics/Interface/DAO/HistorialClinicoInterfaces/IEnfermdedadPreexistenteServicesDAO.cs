using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFG_Web.Models;
using TFG_Web.Models.HistorialClinicoCollections;

namespace TFG_Web.Areas.Clinics.Interface.DAO.HistorialClinicoInterfaces
{
    interface IEnfermdedadPreexistenteServicesDAO
    {
        Task<List<EnfermdedadPreexistente>> GetAllAsync();
        Task<EnfermdedadPreexistente> GetByIdAsync(string id);
        Task<EnfermdedadPreexistente> CreateAsync(EnfermdedadPreexistente historialClinico);
        Task UpdateAsync(string id, EnfermdedadPreexistente historialClinico);
        Task DeleteAsync(string id);
    }
}
