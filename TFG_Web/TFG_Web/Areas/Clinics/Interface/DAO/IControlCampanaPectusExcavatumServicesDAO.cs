using System.Collections.Generic;
using System.Threading.Tasks;
using TFG_Web.Models;

namespace TFG_Web.Areas.Clinics.Interface.DAO
{
    public interface IControlCampanaPectusExcavatumServicesDAO
    {
        Task<List<ControlCampanaPectusExcavatum>> GetAllAsync();
        Task<ControlCampanaPectusExcavatum> GetByIdAsync(string id);
        Task<List<ControlCampanaPectusExcavatum>> GetGroupByIdAsync(string[] id);
        Task<ControlCampanaPectusExcavatum> CreateAsync(ControlCampanaPectusExcavatum campana);
        Task UpdateAsync(string id, ControlCampanaPectusExcavatum campana);
        Task DeleteAsync(string id);
    }
}
