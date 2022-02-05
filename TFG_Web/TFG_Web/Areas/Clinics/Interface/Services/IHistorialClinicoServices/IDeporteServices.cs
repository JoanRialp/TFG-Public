using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFG_Web.Models.HistorialClinicoCollections;

namespace TFG_Web.Areas.Clinics.Interface.Services.IHistorialClinicoServices
{
    public interface IDeporteServices
    {
        Task<IEnumerable<Deporte>> GetAll();
        Task<Deporte> GetById(string id);
        Task<string> Create(Deporte deporte);
        Task<string> Update(string id, Deporte updatedDeporte);
        Task<string> Delete(string id);
    }
}
