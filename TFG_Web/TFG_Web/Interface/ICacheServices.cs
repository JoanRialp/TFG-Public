using TFG_Web.Models;

namespace TFG_Web.Interface
{
    public interface ICacheServices
    {
        void SetCacheNotificacionRecuperarContrasena(string cacheKey, bool? result, string mensaje);
        ViewModel GetCacheNotificacionRecuperarContrasena(ViewModel model, string cacheKey);
    }
}
