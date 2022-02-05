using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFG_Web.Models.HistorialClinicoCollections;

namespace TFG_Web.Areas.Clinics.Interface.Services.IHistorialClinicoServices
{
    public interface ISindromePolandServices
    {
        Task<IEnumerable<SindromePoland>> GetAll();
        Task<SindromePoland> GetById(string id);
        Task<string> Create(SindromePoland sindromePoland);
        Task<string> Update(string id, SindromePoland updatedSindromePoland);
        Task<string> Delete(string id);
    }
}
