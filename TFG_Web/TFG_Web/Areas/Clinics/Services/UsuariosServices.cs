using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TFG_Web.Models;
using TFG_Web.Areas.Clinics.Models;
using TFG_Web.Areas.Clinics.DAO;

namespace TFG_Web.Areas.Clinics.Services
{
    [Area(Values.AreaClinics)]
    public class UsuariosServices : Controller
    {
        #region Dependencias Interfaces
        private readonly UsuariosServicesDAO _usuarios;
        #endregion

        #region "Constructor"
        public UsuariosServices()
        {
            _usuarios = new UsuariosServicesDAO();
        }
        #endregion

        /// <summary>
        /// Obtenemos todos los usuarios
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<Usuarios>> GetAll()
        {
            var usuarios = await _usuarios.GetAllAsync();
            return usuarios;
        }

        /// <summary>
        /// Obtenemos los usuarios a partir de su grupo
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<Usuarios>> GetByGroup(int group)
        {
            var usuarios = await _usuarios.GetByGroupAsync(group);
            if (usuarios == null)
            {
                return new List<Usuarios>();
            }
            return usuarios;
        }

        [HttpGet]
        /// <summary>
        /// Login del usuario
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<Usuarios> GetAccesUser(string username, string password)
        {
            var usuarios = await _usuarios.GetAccesUserAsync(username, password);
            if (usuarios == null)
            {
                return new Usuarios();
            }

            return usuarios;
        }

        [HttpGet]
        /// <summary>
        /// Obtenemos el usuario por su ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Usuarios> GetById(string id)
        {
            var usuarios = await _usuarios.GetByIdAsync(id);
            if (usuarios == null)
            {
                return new Usuarios();
            }
            return usuarios;
        }

        /// <summary>
        /// Creamos un usuario
        /// </summary>
        /// <param name="pacientes"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(Usuarios pacientes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _usuarios.CreateAsync(pacientes);
            return Ok(pacientes);
        }

        /// <summary>
        /// Actualizamos la informacion de un usuario
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedPacientes"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update(string id, Usuarios updatedPacientes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var queriedPacientes = await _usuarios.GetByIdAsync(id);
            if (queriedPacientes == null)
            {
                return NotFound();
            }
            await _usuarios.UpdateAsync(id, updatedPacientes);
            return Ok(updatedPacientes);
        }

        /// <summary>
        /// Eliminamos un usuario
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var pacientes = await _usuarios.GetByIdAsync(id);
            if (pacientes == null)
            {
                return NotFound();
            }
            await _usuarios.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet]
        /// <summary>
        /// Comprovar si el correo electronico existe en un usuario
        /// </summary>
        /// <param name="correo"></param>
        /// <returns></returns>
        public async Task<Usuarios> FindCorreo(string correo)
        {
            Usuarios usuarios = await _usuarios.FindCorreoAsync(correo);
            if (usuarios == null)
            {
                return new Usuarios();
            }
            return usuarios;
        }
    }
}