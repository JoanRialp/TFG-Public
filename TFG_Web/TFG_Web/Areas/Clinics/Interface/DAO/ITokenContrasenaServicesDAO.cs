using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFG_Web.Models;

namespace TFG_Web.Areas.Clinics.Interface.DAO
{
    public interface ITokenContrasenaServicesDAO
    {
       Task<List<TokenContrasena>> GetAllAsync();
       Task<TokenContrasena> GetByIdAsync(string id);
        Task<TokenContrasena> GetByTokenAsync(string idToken);
       Task<TokenContrasena> CreateAsync(TokenContrasena token);
       Task UpdateAsync(string id, TokenContrasena token);
       Task DeleteAsync(string id);
       Task DeleteAllAsync();
    }
}
