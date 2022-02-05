using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TFG_Web.Areas.Clinics.Interface.Services;
using TFG_Web.Areas.Clinics.Models;
using TFG_Web.Areas.Clinics.Services;
using TFG_Web.Models;
using TFG_Web.Services;

namespace TFG_Web.Areas.Clinics.Controllers
{
    [Area(Values.AreaClinics)]
    [ViewLayout(Values.LayoutClinics)]
    public class AnadirUsuarioController : Controller
    {
        #region Dependencias Interfaces
        private ISession Session => HttpContext.Session;
        private readonly ValidarAcceso _validarAcceso = new ValidarAcceso();
        private readonly ViewModel mymodel = new ViewModel();
        private readonly IGetPacientesServices _getPacientesServices;
        #endregion

        #region Constructor
        public AnadirUsuarioController(IGetPacientesServices getPacientesServices)
        {
            _getPacientesServices = getPacientesServices;
        }
        #endregion

        public async Task<IActionResult> Index()
        {
            if (!_validarAcceso.ValidarAccesoWeb(Session)) return RedirectToAction(Values.Index, Values.LoginUsuario, new { area = Values.AreaVacio });

            mymodel.ListPacientes = await _getPacientesServices.ListPacientesAsync(Session.GetString(Values.Session_usrId));
            return View(Values.Index, mymodel);
        }

        [HttpPost]
        public async Task<IActionResult> formularioAddUsuario(string AñadirUsuarioFormCorreo, string AñadirUsuarioFormContraseña, int AñadirUsuarioFormGrupo)
        {
            if (!_validarAcceso.ValidarAccesoWeb(Session)) return RedirectToAction(Values.Index, Values.LoginRegisterUsuario, new { Values.AreaVacio });

            Usuarios usuario = new Usuarios();

            string cryptedPassword = usuario.cryptedPassword(AñadirUsuarioFormContraseña);

            if (AñadirUsuarioFormContraseña != null && AñadirUsuarioFormCorreo != null)
            {
                usuario.P_Username = AñadirUsuarioFormCorreo.Substring(0, AñadirUsuarioFormCorreo.IndexOf("@"));
                usuario.P_Correo = AñadirUsuarioFormCorreo;
                usuario.P_Password = cryptedPassword;
                usuario.P_Fecha_Inicio = DateTime.UtcNow;
                usuario.P_Grupo = AñadirUsuarioFormGrupo;

                await AddUsuario(usuario);
            }
            else
            {
                //TODO: ADVERTENCIA
            }

            return View(Values.Index);
        }

        /// <summary>
        /// Crear el usuario a partir del formulario
        /// </summary>
        /// <param name="usuarios"></param>
        /// <returns></returns>
        public async Task<IActionResult> AddUsuario(Usuarios usuarios)
        {
            UsuariosServices pacientesController = new UsuariosServices();

            await pacientesController.Create(usuarios);

            return View(Values.Index);
        }

        //TODO:
        private string GenerateToken(string password)
        {
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(password));

            var myIssuer = "http://mysite.com";

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddDays(7),
                Issuer = myIssuer,
                SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}