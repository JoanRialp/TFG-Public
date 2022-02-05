using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFG_Web.Models;

namespace TFG_Web.Areas.Clinics.Interface.Services
{
    public interface IGetPacientesServices
    {
        Task<List<Pacientes>> ListPacientesAsync(string idUsuario);
    }
}
