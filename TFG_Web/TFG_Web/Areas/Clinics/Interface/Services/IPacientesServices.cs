using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFG_Web.Models;

namespace TFG_Web.Areas.Clinics.Interface.Services
{
    public interface IPacientesServices
    {
        Task<List<Pacientes>> GetAll();
        Task<List<Pacientes>> GetByUsuariosId(string idUsuario, bool finalizado);
        Task<List<Pacientes>> GetByFinalizado(bool finalizado);
        Task<Pacientes> GetById(string id);
        Task<Pacientes> Create(Pacientes pacientes);
        Task<Pacientes> Update(string id, Pacientes pacientes);
        Task<string> Delete(string id);
    }
}
