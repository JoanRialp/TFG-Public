using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFG_Web.Models.HistorialClinicoCollections;

namespace TFG_Web.Areas.Clinics.Interface.Services.IHistorialClinicoServices
{
    public interface ISignosSintomasClinicosServices
    {
        Task<IEnumerable<SignosSintomasClinicos>> GetAll();
        Task<SignosSintomasClinicos> GetById(string id);
        Task<string> Create(SignosSintomasClinicos signosSintomasClinicos);
        Task<string> Update(string id, SignosSintomasClinicos updatedsignosSintomasClinicos);
        Task<string> Delete(string id);
    }
}
