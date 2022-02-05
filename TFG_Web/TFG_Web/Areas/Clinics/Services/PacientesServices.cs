using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TFG_Web.Models;
using TFG_Web.Areas.Clinics.Models;
using Microsoft.AspNetCore.Http;
using TFG_Web.Areas.Clinics.Interface.Services;
using TFG_Web.Areas.Clinics.Interface.DAO;

namespace TFG_Web.Areas.Clinics.Services
{
    [Area(Values.AreaClinics)]
    public class PacientesServices : IPacientesServices
    {
        #region Dependencias Interfaces
        private IPacientesServicesDAO _pacientesServicesDAO;
        #endregion

        #region "Constructor"
        public PacientesServices(IPacientesServicesDAO pacientes)
        {
            _pacientesServicesDAO = pacientes;
        }
        #endregion

        /// <summary>
        /// Obtenemos todos los pacientes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<Pacientes>> GetAll()
        {
            var pacientes = await _pacientesServicesDAO.GetAllAsync();
            return pacientes;
        }

        /// <summary>
        /// Obtenemos todos los pacientes a partir del idUsario
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<Pacientes>> GetByUsuariosId(string idUsuario, bool finalizado)
        {
            var pacientes = await _pacientesServicesDAO.GetByUsuariosIdAsync(idUsuario, finalizado);
            return pacientes;
        }

        /// <summary>
        /// Obtenemos todos los Pacientes a partir del campo Finalizado
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<Pacientes>> GetByFinalizado(bool finalizado)
        {
            var pacientes = await _pacientesServicesDAO.GetByFinalizadoAsync(finalizado);
            return pacientes;
        }

        /// <summary>
        /// Obtenemos el usuario por su ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Pacientes> GetById(string id)
        {
            var pacientes = await _pacientesServicesDAO.GetByIdAsync(id);
            if (pacientes == null)
            {
                return new Pacientes();
            }

            return pacientes;
        }

        /// <summary>
        /// Creamos un usuario
        /// </summary>
        /// <param name="pacientes"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Pacientes> Create(Pacientes pacientes)
        {
            var result = await _pacientesServicesDAO.CreateAsync(pacientes);
            return result;
        }

        /// <summary>
        /// Actualizamos la informacion de un usuario
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedPacientes"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<Pacientes> Update(string id, Pacientes updatedPacientes)
        {
            var queriedPacientes = await _pacientesServicesDAO.GetByIdAsync(id);
            if (queriedPacientes == null)
            {
                return null;
            }
            await _pacientesServicesDAO.UpdateAsync(id, updatedPacientes);
            return updatedPacientes;
        }

        /// <summary>
        /// Eliminamos un usuario
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<string> Delete(string id)
        {
            await _pacientesServicesDAO.DeleteAsync(id);
            return id;
        }
    }
}