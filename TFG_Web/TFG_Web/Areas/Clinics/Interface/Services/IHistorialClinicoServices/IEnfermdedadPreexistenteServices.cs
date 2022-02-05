using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFG_Web.Models.HistorialClinicoCollections;

namespace TFG_Web.Areas.Clinics.Interface.Services.IHistorialClinicoServices
{
    public interface IEnfermdedadPreexistenteServices
    {
        Task<IEnumerable<EnfermdedadPreexistente>> GetAll();
        Task<EnfermdedadPreexistente> GetById(string id);
        Task<string> Create(EnfermdedadPreexistente enfermdedadPreexistente);
        Task<string> Update(string id, EnfermdedadPreexistente updatedEnfermdedadPreexistente);
        Task<string> Delete(string id);
    }
}
