using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFG_Web.Models;

namespace TFG_Web.Areas.Clinics.Interface.Services
{
    public interface IControlCampanaPectusExcavatumServices
    {
        Task addControlCampanaExcavatumAsync(ControlCampanaPectusExcavatum controlCampanaExcavatum, string pacienteId);
        Task<IEnumerable<ControlCampanaPectusExcavatum>> GetAll();
        Task<ControlCampanaPectusExcavatum> GetById(string id);
        Task<List<ControlCampanaPectusExcavatum>> GetGroupById(string[] id);
        Task<string> Update(string id, ControlCampanaPectusExcavatum updatedCampana);
        Task<string> Delete(string id);
    }
}
