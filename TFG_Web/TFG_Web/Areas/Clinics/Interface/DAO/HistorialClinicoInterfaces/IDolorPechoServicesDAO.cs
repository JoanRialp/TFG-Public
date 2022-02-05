using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFG_Web.Models;
using TFG_Web.Models.HistorialClinicoCollections;

namespace TFG_Web.Areas.Clinics.Interface.DAO.HistorialClinicoInterfaces
{
    interface IDolorPechoServicesDAO
    {
        Task<List<DolorPecho>> GetAllAsync();
        Task<DolorPecho> GetByIdAsync(string id);
        Task<DolorPecho> CreateAsync(DolorPecho historialDolorPecho);
        Task UpdateAsync(string id, DolorPecho historialDolorPecho);
        Task DeleteAsync(string id);
    }
}
