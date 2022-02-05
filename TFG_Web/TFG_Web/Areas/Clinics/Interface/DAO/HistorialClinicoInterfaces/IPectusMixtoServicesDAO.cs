using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFG_Web.Models;
using TFG_Web.Models.HistorialClinicoCollections;

namespace TFG_Web.Areas.Clinics.Interface.DAO.HistorialClinicoInterfaces
{
    interface IPectusMixtoServicesDAO
    {
        Task<List<PectusMixto>> GetAllAsync();
        Task<PectusMixto> GetByIdAsync(string id);
        Task<PectusMixto> CreateAsync(PectusMixto pectusMixto);
        Task UpdateAsync(string id, PectusMixto pectusMixto);
        Task DeleteAsync(string id);
    }
}
