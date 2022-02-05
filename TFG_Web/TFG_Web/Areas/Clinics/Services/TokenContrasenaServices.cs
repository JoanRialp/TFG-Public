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
    public class TokenContrasenaServices : ITokenContrasenaServices
    {
        #region Dependencias Interfaces
        private ITokenContrasenaServicesDAO _tokenContrasenaServicesDAO;
        #endregion

        #region "Constructor"
        public TokenContrasenaServices(ITokenContrasenaServicesDAO tokenContrasenaServicesDAO)
        {
            _tokenContrasenaServicesDAO = tokenContrasenaServicesDAO;
        }
        #endregion

        /// <summary>
        /// Obtenemos todos los tokens
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<TokenContrasena>> GetAll()
        {
            var token = await _tokenContrasenaServicesDAO.GetAllAsync();
            return token;
        }

        /// <summary>
        /// Obtenemos el token por su ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<TokenContrasena> GetById(string id)
        {
            var token = await _tokenContrasenaServicesDAO.GetByIdAsync(id);
            if (token == null)
            {
                return new TokenContrasena();
            }

            return token;
        }

        /// <summary>
        /// Obtenemos el token por su TokenID
        /// </summary>
        /// <param name="idToken"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<TokenContrasena> GetByToken(string idToken)
        {
            var token = await _tokenContrasenaServicesDAO.GetByTokenAsync(idToken);
            if (token == null)
            {
                return new TokenContrasena();
            }

            return token;
        }

        /// <summary>
        /// Creamos un usuario
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<TokenContrasena> Create(TokenContrasena token)
        {
            var result = await _tokenContrasenaServicesDAO.CreateAsync(token);
            return result;
        }

        /// <summary>
        /// Actualizamos la informacion de un token
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedToken"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<TokenContrasena> Update(string id, TokenContrasena updatedToken)
        {
            var queriedToken = await _tokenContrasenaServicesDAO.GetByIdAsync(id);
            if (queriedToken == null)
            {
                return null;
            }
            await _tokenContrasenaServicesDAO.UpdateAsync(id, updatedToken);
            return updatedToken;
        }

        /// <summary>
        /// Eliminamos un token
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<string> Delete(string id)
        {
            await _tokenContrasenaServicesDAO.DeleteAsync(id);
            return id;
        }

        /// <summary>
        /// Eliminamos todos los tokens
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task DeleteAll()
        {
            await _tokenContrasenaServicesDAO.DeleteAllAsync();
        }
    }
}