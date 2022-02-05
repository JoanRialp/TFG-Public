using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFG_Web.Models;

namespace TFG_Web.Areas.Clinics.Interface.Services
{
    public interface ITokenContrasenaServices
    {
        Task<List<TokenContrasena>> GetAll();
        Task<TokenContrasena> GetById(string id);
        Task<TokenContrasena> GetByToken(string idToken);
        Task<TokenContrasena> Create(TokenContrasena token);
        Task<TokenContrasena> Update(string id, TokenContrasena token);
        Task<string> Delete(string id);
        Task DeleteAll();
    }
}
