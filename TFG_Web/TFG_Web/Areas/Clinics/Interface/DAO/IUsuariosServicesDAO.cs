using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFG_Web.Models;

namespace TFG_Web.Areas.Clinics.Interface.DAO
{
    interface IUsuariosServicesDAO
    {
       Task<List<Usuarios>> GetAllAsync();
        Task<List<Usuarios>> GetByGroupAsync(int group);
        Task<Usuarios> GetByIdAsync(string id);
        Task<Usuarios> GetAccesUserAsync(string name, string password);
        Task<Usuarios> CreateAsync(Usuarios usuarios);
        Task UpdateAsync(string id, Usuarios usuarios);
        Task DeleteAsync(string id);
        Task<Usuarios> FindCorreoAsync(string correo);
    }
}
