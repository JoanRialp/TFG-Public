using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TFG_Web.Areas.Clinics.Models;

namespace TFG_Web.Services
{
    public class ValidarAcceso : Controller
    {
        /// <summary>
        /// Validamos el Acceso del usuario a la vista
        /// Comprobamos si es null
        /// Comprobamos a que Grupo pertenece
        /// </summary>
        /// <param name="_session">Parametro con la Session actual del usuario</param>
        /// <returns></returns>
        public bool ValidarAccesoWeb(ISession _session)
        {
            if (_session.GetString(Values.Session_usrId) != null && (_session.GetInt32(Values.Session_usrArea) == 1 || _session.GetInt32(Values.Session_usrArea) == 0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}