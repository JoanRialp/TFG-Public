using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFG_Web.Models.HistorialClinicoCollections;

namespace TFG_Web.Areas.Clinics.Interface.Services.IHistorialClinicoServices
{
    public interface IPectusCarinatumServices
    {
        Task<IEnumerable<PectusCarinatum>> GetAll();
        Task<PectusCarinatum> GetById(string id);
        Task<string> Create(PectusCarinatum pectusCarinatum);
        Task<string> Update(string id, PectusCarinatum updatePectusCarinatum);
        Task<string> Delete(string id);
    }
}
