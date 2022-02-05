using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFG_Web.Models;

namespace TFG_Web.Areas.Clinics.Interface.DAO
{
    public interface IControlSistemaCompresorServicesDAO
    {
        Task<List<ControlSistemaCompresor>> GetAllAsync();
        Task<ControlSistemaCompresor> GetByIdAsync(string id);
        Task<List<ControlSistemaCompresor>> GetGroupByIdAsync(string[] id);
        Task<ControlSistemaCompresor> CreateAsync(ControlSistemaCompresor compresor);
        Task UpdateAsync(string id, ControlSistemaCompresor compresor);
        Task DeleteAsync(string id);
    }
}
