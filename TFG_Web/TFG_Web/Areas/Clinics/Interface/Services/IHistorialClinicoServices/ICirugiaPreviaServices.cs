using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFG_Web.Models;
using TFG_Web.Models.HistorialClinicoCollections;

namespace TFG_Web.Areas.Clinics.Interface.Services.IHistorialClinicoServices
{
    public interface ICirugiaPreviaServices
    {
        Task<IEnumerable<CirugiaPrevia>> GetAll();
        Task<CirugiaPrevia> GetById(string id);
        Task<string> Create(CirugiaPrevia cirugiaPrevia);
        Task<string> Update(string id, CirugiaPrevia updatedcirugiaPrevia);
        Task<string> Delete(string id);

    }
}
