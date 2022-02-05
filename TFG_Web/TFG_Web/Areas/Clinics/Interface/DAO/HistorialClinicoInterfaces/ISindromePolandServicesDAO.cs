using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFG_Web.Models.HistorialClinicoCollections;

namespace TFG_Web.Areas.Clinics.Interface.DAO.HistorialClinicoInterfaces
{
    interface ISindromePolandServicesDAO
    {
        Task<List<SindromePoland>> GetAllAsync();
        Task<SindromePoland> GetByIdAsync(string id);
        Task<SindromePoland> CreateAsync(SindromePoland sindromePoland);
        Task UpdateAsync(string id, SindromePoland sindromePoland);
        Task DeleteAsync(string id);
    }
}
