using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFG_Web.Models;
using TFG_Web.Models.HistorialClinicoCollections;

namespace TFG_Web.Areas.Clinics.Interface.DAO.HistorialClinicoInterfaces
{
    interface IPectusExcavatumServicesDAO
    {
        Task<List<PectusExcavatum>> GetAllAsync();
        Task<PectusExcavatum> GetByIdAsync(string id);
        Task<PectusExcavatum> CreateAsync(PectusExcavatum pectusExcavatum);
        Task UpdateAsync(string id, PectusExcavatum pectusExcavatum);
        Task DeleteAsync(string id);
    }
}
