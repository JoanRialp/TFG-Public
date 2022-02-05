using System;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using TFG_Web.Interface;
using TFG_Web.Models;

namespace TFG_Web.Services
{
    public class CacheServices : ICacheServices
    {
        #region Dependencias Interfaces
        private readonly ILogger<TokenHostedServices> _logger;
        private readonly IMemoryCache _memoryCache;
        private MemoryCacheEntryOptions _cacheEntryoptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(2));
        #endregion

        #region Constructor
        public CacheServices(ILogger<TokenHostedServices> logger, IMemoryCache _memoryCache)
        {
            this._logger = logger;
            this._memoryCache = _memoryCache;
        }
        #endregion

        /// <summary>
        /// Añadimos a la cache
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="result">Resultado de la operacion</param>
        /// <param name="mensaje">Mensaje a mostrar</param>
        public void SetCacheNotificacionRecuperarContrasena(string cacheKey, bool? result, string mensaje)
        {
            bool AlreadyExitsEnviarCorreo = _memoryCache.TryGetValue(cacheKey, out string respuesta);

            if (!AlreadyExitsEnviarCorreo)
            {
                if (result != null)
                {
                    _memoryCache.Set(cacheKey, result, _cacheEntryoptions);
                }

                else if (mensaje != null)
                {
                    _memoryCache.Set(cacheKey, mensaje, _cacheEntryoptions);
                }
            }
        }

        /// <summary>
        /// Obtenemos de la cache
        /// </summary>
        /// <param name="model"> ViewModel</param>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        public ViewModel GetCacheNotificacionRecuperarContrasena(ViewModel model, string cacheKey)
        {
            ViewModel mymodel = model;
            object result_Cache = _memoryCache.Get(cacheKey);

            if (result_Cache != null)
            {
                if (result_Cache.GetType() == typeof(string))
                {
                    mymodel.NotificacionesMensaje = result_Cache.ToString();
                }

                else if (result_Cache.GetType() == typeof(bool))
                {
                    mymodel.Notificaciones = bool.Parse(result_Cache.ToString());
                }

                mymodel.NotificacionesControl = true;
            }

            return mymodel;
        }
    }
}