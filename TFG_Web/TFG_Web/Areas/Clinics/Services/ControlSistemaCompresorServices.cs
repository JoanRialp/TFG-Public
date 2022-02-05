using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TFG_Web.Models;
using TFG_Web.Areas.Clinics.Models;
using System;
using Microsoft.Extensions.Caching.Memory;
using TFG_Web.Areas.Clinics.Interface.Services;
using TFG_Web.Areas.Clinics.Interface.DAO;

namespace TFG_Web.Areas.Clinics.Services
{
    [Area(Values.AreaClinics)]
    public class ControlSistemaCompresorServices : IControlSistemaCompresorServices
    {
        #region Dependencias Interfaces
        private readonly IControlSistemaCompresorServicesDAO _controlSistemaCompresor;
        private readonly IMemoryCache _memoryCache;
        private readonly IPacientesServices _pacientesServices;
        #endregion

        #region Constructor
        public ControlSistemaCompresorServices(IPacientesServices pacientesServices, IControlSistemaCompresorServicesDAO controlSistemaCompresor, IMemoryCache _memoryCache)
        {
            this._memoryCache = _memoryCache;
            this._controlSistemaCompresor = controlSistemaCompresor;
            this._pacientesServices = pacientesServices;
        }
        #endregion

        /// <summary>
        /// Añadimos el formulario del Control Sistema Compresor a su Collection y a la array del Usuario
        /// </summary>
        /// <param name="controlSistemaCompresor"></param>
        /// <param name="usuarioId"></param>
        /// <returns></returns>
        public async Task addControlSistemaCompresorAsync(ControlSistemaCompresor controlSistemaCompresor, string pacienteId)
        {
            string notificacionMensaje = string.Empty;
            bool notificacion = false;

            if (controlSistemaCompresor != null && pacienteId != null)
            {
                try
                {
                    var idSistemaCompresor = await Create(controlSistemaCompresor);

                    try
                    {
                        Pacientes paciente = await _pacientesServices.GetById(pacienteId);
                        paciente.ControlSistemaCompresor.Add(idSistemaCompresor);

                        await _pacientesServices.Update(pacienteId, paciente);

                        notificacionMensaje = Mensajes.SuccessCrearSistemaCompresor;
                        notificacion = true;
                    }
                    catch
                    {
                        notificacionMensaje = Mensajes.ErrorCrearSistemaCompresor;
                        notificacion = false;
                    }
                }
                catch
                {
                    notificacionMensaje = Mensajes.ErrorCrearSistemaCompresor;
                    notificacion = false;
                }
            }
            else
            {
                notificacionMensaje = Mensajes.ErrorCrearSistemaCompresor;
                notificacion = false;
            }

            //Cache - Notificacions creacion
            bool AlreadyExitCrearSistemaCompresor = _memoryCache.TryGetValue("crearSistemaCompresor", out string respuesta);
            bool AlreadyExitCrearSistemaCompresorMensaje = _memoryCache.TryGetValue("crearSistemaCompresorMensaje", out string respuesta2);

            if (!AlreadyExitCrearSistemaCompresor && !AlreadyExitCrearSistemaCompresorMensaje)
            {
                var cacheEntryoptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(2));
                _memoryCache.Set("crearSistemaCompresor", notificacion, cacheEntryoptions);
                _memoryCache.Set("crearSistemaCompresorMensaje", notificacionMensaje, cacheEntryoptions);
            }
        }

        [HttpGet]
        public async Task<IEnumerable<ControlSistemaCompresor>> GetAll()
        {
            var ControlSistemaCompresor = await _controlSistemaCompresor.GetAllAsync();
            return ControlSistemaCompresor;
        }

        [HttpGet]
        public async Task<ControlSistemaCompresor> GetById(string id)
        {
            var ControlSistemaCompresor = await _controlSistemaCompresor.GetByIdAsync(id);
            if (ControlSistemaCompresor == null)
            {
                return null;
            }
            return ControlSistemaCompresor;
        }

        [HttpGet]
        public async Task<List<ControlSistemaCompresor>> GetGroupById(string[] id)
        {
            var ControlSistemaCompresor = await _controlSistemaCompresor.GetGroupByIdAsync(id);
            if (ControlSistemaCompresor == null)
            {
                return null;
            }
            return ControlSistemaCompresor;
        }

        [HttpPost]
        public async Task<string> Create(ControlSistemaCompresor compresor)
        {
            var result = await _controlSistemaCompresor.CreateAsync(compresor);
            return result.Id;
        }

        [HttpPut]
        public async Task<string> Update(string id, ControlSistemaCompresor updatedCompresor)
        {
            var queriedCompresor = await _controlSistemaCompresor.GetByIdAsync(id);
            if (queriedCompresor == null)
            {
                return null;
            }
            await _controlSistemaCompresor.UpdateAsync(id, updatedCompresor);
            return queriedCompresor.Id;
        }

        [HttpDelete]
        public async Task<string> Delete(string id)
        {
            var compresor = await _controlSistemaCompresor.GetByIdAsync(id);
            if (compresor == null)
            {
                return null;
            }
            await _controlSistemaCompresor.DeleteAsync(id);
            return compresor.Id;
        }
    }
}