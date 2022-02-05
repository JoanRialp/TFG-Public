using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFG_Web.Models;

namespace TFG_Web.Areas.Clinics.Interface.Services.IHistorialClinicoServices
{
    public interface IHistorialClinicoServices
    {
        Task<ActionResult<IEnumerable<HistorialClinico>>> GetAll();
        Task<HistorialClinico> GetById(string id);
        Task<string> Create(HistorialClinico historialClinico);
        Task<string> Update(string id, HistorialClinico updatedHistorialClinico);
        Task<string> Delete(string id);
    }
}
