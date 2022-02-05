using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFG_Web.Models.HistorialClinicoCollections;

namespace TFG_Web.Areas.Clinics.Interface.Services.IHistorialClinicoServices
{
    public interface IDolorPechoServices
    {
        Task<IEnumerable<DolorPecho>> GetAll();
        Task<DolorPecho> GetById(string id);
        Task<string> Create(DolorPecho dolorPecho);
        Task<string> Update(string id, DolorPecho updateddolorPecho);
        Task<string> Delete(string id);
    }
}
