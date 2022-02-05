using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFG_Web.Models.HistorialClinicoCollections;

namespace TFG_Web.Areas.Clinics.Interface.Services.IHistorialClinicoServices
{
    public interface IPectusMixtoServices
    {
        Task<IEnumerable<PectusMixto>> GetAll();
        Task<PectusMixto> GetById(string id);
        Task<string> Create(PectusMixto pectusMixto);
        Task<string> Update(string id, PectusMixto updatedPectusMixto);
        Task<string> Delete(string id);
    }
}
