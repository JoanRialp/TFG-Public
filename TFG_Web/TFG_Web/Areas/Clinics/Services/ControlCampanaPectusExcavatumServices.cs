using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TFG_Web.Models;
using TFG_Web.Areas.Clinics.Models;
using System;
using Microsoft.Extensions.Caching.Memory;
using TFG_Web.Areas.Clinics.Interface.DAO;
using TFG_Web.Areas.Clinics.Interface.Services;

namespace TFG_Web.Areas.Clinics.Services
{
    [Area(Values.AreaClinics)]
    public class ControlCampanaPectusExcavatumServices : IControlCampanaPectusExcavatumServices
    {
        #region Dependencias Interfaces
        private IControlCampanaPectusExcavatumServicesDAO _controlCampanaPectusExcavatum;
        private readonly IMemoryCache _memoryCache;
        private readonly IPacientesServices _pacientesServices;
        #endregion

        #region "Constructor"
        public ControlCampanaPectusExcavatumServices(IControlCampanaPectusExcavatumServicesDAO controlCampanaPectusExcavatum, IPacientesServices pacientesServices, IMemoryCache _memoryCache)
        {
            this._memoryCache = _memoryCache;
            this._controlCampanaPectusExcavatum = controlCampanaPectusExcavatum;
            this._pacientesServices = pacientesServices;
        }
        #endregion

        [HttpPost]
        public async Task addControlCampanaExcavatumAsync(ControlCampanaPectusExcavatum controlCampanaExcavatum, string pacienteId)
        {
            string notificacionMensaje = string.Empty;
            bool notificacion = false;

            if (controlCampanaExcavatum != null && pacienteId != null)
            {
                try
                {
                    var idCampanaExcavatum = await Create(controlCampanaExcavatum);
                    notificacionMensaje = Mensajes.SuccessCrearCampanaExcavatum;
                    notificacion = true;

                    try
                    {
                        Pacientes paciente = await _pacientesServices.GetById(pacienteId);
                        paciente.ControlCampanaPectusExcavatum.Add(idCampanaExcavatum);

                        await _pacientesServices.Update(pacienteId, paciente);

                        notificacionMensaje = Mensajes.SuccessCrearCampanaExcavatum;
                        notificacion = true;
                    }
                    catch
                    {
                        notificacionMensaje = Mensajes.ErrorCrearCampanaExcavatum;
                        notificacion = false;
                    }
                }
                catch
                {
                    notificacionMensaje = Mensajes.ErrorCrearCampanaExcavatum;
                    notificacion = false;
                }
            }
            else
            {
                notificacionMensaje = Mensajes.ErrorCrearCampanaExcavatum;
                notificacion = false;
            }

            //Cache - Notificacions creacion
            bool AlreadyExitCrearCampanaExcavatum = _memoryCache.TryGetValue("crearCampanaExcavatum", out string respuesta);
            bool AlreadyExitCrearCampanaExcavatumMensaje = _memoryCache.TryGetValue("crearCampanaExcavatumMensaje", out string respuesta2);

            if (!AlreadyExitCrearCampanaExcavatum && !AlreadyExitCrearCampanaExcavatumMensaje)
            {
                var cacheEntryoptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(2));
                _memoryCache.Set("crearCampanaExcavatum", notificacion, cacheEntryoptions);
                _memoryCache.Set("crearCampanaExcavatumMensaje", notificacionMensaje, cacheEntryoptions);
            }
        }

        [HttpGet]
        public async Task<IEnumerable<ControlCampanaPectusExcavatum>> GetAll()
        {
            var controlCampanaPectusExcavatum = await _controlCampanaPectusExcavatum.GetAllAsync();
            return controlCampanaPectusExcavatum;
        }

        [HttpGet]
        public async Task<ControlCampanaPectusExcavatum> GetById(string id)
        {
            var controlCampanaPectusExcavatum = await _controlCampanaPectusExcavatum.GetByIdAsync(id);
            if (controlCampanaPectusExcavatum == null)
            {
                return null;
            }
            return controlCampanaPectusExcavatum;
        }

        [HttpGet]
        public async Task<List<ControlCampanaPectusExcavatum>> GetGroupById(string[] id)
        {
            var controlCampanaPectusExcavatum = await _controlCampanaPectusExcavatum.GetGroupByIdAsync(id);
            if (controlCampanaPectusExcavatum == null)
            {
                return null;
            }
            return controlCampanaPectusExcavatum;

        }

        [HttpPost]
        public async Task<string> Create(ControlCampanaPectusExcavatum campana)
        {
            var result = await _controlCampanaPectusExcavatum.CreateAsync(campana);
            return result.Id;
        }

        [HttpPut]
        public async Task<string> Update(string id, ControlCampanaPectusExcavatum updatedCampana)
        {
            var queriedCompresor = await _controlCampanaPectusExcavatum.GetByIdAsync(id);
            if (queriedCompresor == null)
            {
                return null;
            }
            await _controlCampanaPectusExcavatum.UpdateAsync(id, updatedCampana);
            return queriedCompresor.Id;
        }

        [HttpDelete]
        public async Task<string> Delete(string id)
        {
            var compresor = await _controlCampanaPectusExcavatum.GetByIdAsync(id);
            if (compresor == null)
            {
                return null;
            }
            await _controlCampanaPectusExcavatum.DeleteAsync(id);
            return compresor.Id;
        }
    }
}