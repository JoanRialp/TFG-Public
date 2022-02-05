using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFG_Web.Models;

namespace TFG_Web.Areas.Clinics.Interface.DAO.HistorialClinicoInterfaces
{
    interface IHistorialClinicoServicesDAO
    {
        Task<List<HistorialClinico>> GetAllAsync();
        Task<HistorialClinico> GetByIdAsync(string id);
        Task<HistorialClinico> CreateAsync(HistorialClinico historialClinico);
        Task UpdateAsync(string id, HistorialClinico historialClinico);
        Task DeleteAsync(string id);
    }
}
