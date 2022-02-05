using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFG_Web.Models;

namespace TFG_Web.Areas.Clinics.Interface.DAO
{
    public interface IPacientesServicesDAO
    {
       Task<List<Pacientes>> GetAllAsync();
        Task<List<Pacientes>> GetByUsuariosIdAsync(string idUsuario, bool finalizado);
       Task<List<Pacientes>> GetByFinalizadoAsync(bool finalizado);
       Task<Pacientes> GetByIdAsync(string id);
       Task<Pacientes> CreateAsync(Pacientes pacientes);
       Task UpdateAsync(string id, Pacientes pacientes);
       Task DeleteAsync(string id);
    }
}
