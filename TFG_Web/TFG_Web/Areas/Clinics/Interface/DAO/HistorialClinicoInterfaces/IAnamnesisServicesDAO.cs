using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFG_Web.Models;
using TFG_Web.Models.HistorialClinicoCollections;

namespace TFG_Web.Areas.Clinics.Interface.DAO.HistorialClinicoInterfaces
{
    interface IAnamnesisServicesDAO
    {
        Task<List<Anamnesis>> GetAllAsync();
        Task<Anamnesis> GetByIdAsync(string id);
        Task<Anamnesis> CreateAsync(Anamnesis anamnesis);
        Task UpdateAsync(string id, Anamnesis anamnesis);
        Task DeleteAsync(string id);
    }
}
