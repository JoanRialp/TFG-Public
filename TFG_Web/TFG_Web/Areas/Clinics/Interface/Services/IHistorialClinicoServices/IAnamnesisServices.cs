﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFG_Web.Models;
using TFG_Web.Models.HistorialClinicoCollections;

namespace TFG_Web.Areas.Clinics.Interface.Services.IHistorialClinicoServices
{
    public interface IAnamnesisServices
    {
        Task<IEnumerable<Anamnesis>> GetAll();
        Task<Anamnesis> GetById(string id);
        Task<string> Create(Anamnesis anamnesis);
        Task<string> Update(string id, Anamnesis updatedanamnesis);
        Task<string> Delete(string id);

    }
}
