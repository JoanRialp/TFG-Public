using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFG_Web.Models;
using TFG_Web.Models.HistorialClinicoCollections;

namespace TFG_Web.Areas.Clinics.Interface.DAO.HistorialClinicoInterfaces
{
    interface IClasificacionPectusServicesDAO
    {
        Task<List<ClasificacionPectus>> GetAllAsync();
        Task<ClasificacionPectus> GetByIdAsync(string id);
        Task<ClasificacionPectus> CreateAsync(ClasificacionPectus clasificacionPectus);
        Task UpdateAsync(string id, ClasificacionPectus lasificacionPectus);
        Task DeleteAsync(string id);
    }
}
