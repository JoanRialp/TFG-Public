using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFG_Web.Models;
using TFG_Web.Models.HistorialClinicoCollections;

namespace TFG_Web.Areas.Clinics.Interface.DAO.HistorialClinicoInterfaces
{
    interface IDeporteServicesDAO
    {
        Task<List<Deporte>> GetAllAsync();
        Task<Deporte> GetByIdAsync(string id);
        Task<Deporte> CreateAsync(Deporte historialClinico);
        Task UpdateAsync(string id, Deporte historialClinico);
        Task DeleteAsync(string id);
    }
}
