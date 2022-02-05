using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFG_Web.Models;
using TFG_Web.Models.HistorialClinicoCollections;

namespace TFG_Web.Areas.Clinics.Interface.DAO.HistorialClinicoInterfaces
{
    interface IPectusCarinatumServicesDAO
    {
        Task<List<PectusCarinatum>> GetAllAsync();
        Task<PectusCarinatum> GetByIdAsync(string id);
        Task<PectusCarinatum> CreateAsync(PectusCarinatum pectusCarinatum);
        Task UpdateAsync(string id, PectusCarinatum pectusCarinatum);
        Task DeleteAsync(string id);
    }
}
