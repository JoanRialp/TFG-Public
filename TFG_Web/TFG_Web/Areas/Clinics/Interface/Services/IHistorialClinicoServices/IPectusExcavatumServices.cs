using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFG_Web.Models.HistorialClinicoCollections;

namespace TFG_Web.Areas.Clinics.Interface.Services.IHistorialClinicoServices
{
    public interface IPectusExcavatumServices
    {
        Task<IEnumerable<PectusExcavatum>> GetAll();
        Task<PectusExcavatum> GetById(string id);
        Task<string> Create(PectusExcavatum pectusExcavatum);
        Task<string> Update(string id, PectusExcavatum updatedPectusExcavatum);
        Task<string> Delete(string id);
    }
}
