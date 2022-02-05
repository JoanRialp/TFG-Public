using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TFG_Web.Areas.Clinics.Interface.Services;
using TFG_Web.Areas.Clinics.Models;
using TFG_Web.Models;

namespace TFG_Web.Areas.Clinics.Services
{
    [Area(Values.AreaClinics)]
    public class GetPacientesServices : IGetPacientesServices
    {
        #region Dependencias Interfaces
        private readonly IPacientesServices _pacientesServices;
        #endregion

        #region Constructor
        public GetPacientesServices(IPacientesServices pacientesServices)
        {
            _pacientesServices = pacientesServices;
        }
        #endregion

        /// <summary>
        /// Obtenemos el listado de pacientes de la Base de Datos
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<Pacientes>> ListPacientesAsync(string idUsuario)
        {
            List<Pacientes> listPacientes = await getPaceinteOfHistorialClinicoAsync(idUsuario);

            return listPacientes;
        }

        /// <summary>
        /// Llamamos el controlador para obtener el listado de pacientes de la Base de Datos
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        private async Task<List<Pacientes>> getPaceinteOfHistorialClinicoAsync(string idUsuario)
        {
            //var listPacientes = await _pacientesServices.GetByFinalizado(false);

            var listPacientes = await _pacientesServices.GetByUsuariosId(idUsuario, false);

            return listPacientes;
        }
    }
}