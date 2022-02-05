using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFG_Web.Models.HistorialClinicoCollections;

namespace TFG_Web.Areas.Clinics.Interface.Services.IHistorialClinicoServices
{
    public interface IClasificacionPectusServices
    {
        Task<IEnumerable<ClasificacionPectus>> GetAll();
        Task<ClasificacionPectus> GetById(string id);
        Task<string> Create(ClasificacionPectus clasificacionPectus);
        Task<string> Update(string id, ClasificacionPectus updatedClasificacionPectus);
        Task<string> Delete(string id);
    }
}
