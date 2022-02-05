using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFG_Web.Models;
using TFG_Web.Models.HistorialClinicoCollections;

namespace TFG_Web.Areas.Clinics.Interface.DAO.HistorialClinicoInterfaces
{
    interface ICirugiaPreviaServicesDAO
    {
        Task<List<CirugiaPrevia>> GetAllAsync();
        Task<CirugiaPrevia> GetByIdAsync(string id);
        Task<CirugiaPrevia> CreateAsync(CirugiaPrevia historialClinico);
        Task UpdateAsync(string id, CirugiaPrevia historialClinico);
        Task DeleteAsync(string id);
    }
}
