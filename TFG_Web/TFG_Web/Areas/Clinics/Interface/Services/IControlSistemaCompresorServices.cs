using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFG_Web.Models;

namespace TFG_Web.Areas.Clinics.Interface.Services
{
    public interface IControlSistemaCompresorServices
    {
        Task addControlSistemaCompresorAsync(ControlSistemaCompresor controlSistemaCompresor, string pacienteId);
        Task<IEnumerable<ControlSistemaCompresor>> GetAll();
        Task<ControlSistemaCompresor> GetById(string id);
        Task<List<ControlSistemaCompresor>> GetGroupById(string[] id);
        Task<string> Create(ControlSistemaCompresor compresor);
        Task<string> Update(string id, ControlSistemaCompresor updatedCompresor);
        Task<string> Delete(string id);

    }
}
