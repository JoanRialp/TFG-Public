using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TFG_Web.Areas.Clinics.Models;
using TFG_Web.Areas.Clinics.Services;
using TFG_Web.Models;
using TFG_Web.Services;

namespace TFG_Web.Controllers
{
    [ViewLayout(Values.Layout)]
    public class LoginUsuarioController : Controller
    {
        private ISession Session => HttpContext.Session;

        [Route(Values.IniciarSesion)]
        public IActionResult Index()
        {
            //Comprobamos si el usuario ya ha iniciado sesion a la web, si es que si le redireccionamos al Inicio, sino seguimos en el LoginUsuario
            if (Session.GetString(Values.Session_usrId) != null)
            {
                return RedirectToAction(Values.Index, Values.HomeClinics, new { area = Values.AreaClinics });
            }

            UserLoginView userLoginView = new UserLoginView();
            userLoginView.ErrorMessage = false;
            return View(Values.Index, userLoginView);
        }

        /// <summary>
        /// Iniciar Sesión del usuario
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        [Route(Values.IniciarSesion + "Error")]
        public async Task<IActionResult> LogInUser(UserLoginView model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            //Controla que les propietats vinculades entre model - vista siguin correctes
            if (ModelState.IsValid)
            {
                UsuariosServices usuariosController = new UsuariosServices();
                Usuarios modelUsuarios = new Usuarios();

                string cryptedPassword = modelUsuarios.cryptedPassword(model.Input.PasswordBD);

                try
                {
                    Usuarios usuario = await usuariosController.GetAccesUser(model.Input.UserBD.Substring(0, model.Input.UserBD.IndexOf("@")), cryptedPassword);

                    if (usuario.Id != null)
                    {
                        if (usuario.P_Grupo == 0)
                        {
                            Session.SetString(Values.Session_usrId, usuario.Id);
                            Session.SetInt32(Values.Session_usrArea, usuario.P_Grupo);
                            Session.SetString(Values.Session_usrUsername, usuario.P_Username);
                            Session.SetString(Values.Session_usrPassword, model.Input.PasswordBD);
                            return RedirectToAction(Values.Index, Values.HomeClinics, new { area = Values.AreaClinics });
                        }
                        else if (usuario.P_Grupo == 1)
                        {
                            Session.SetString(Values.Session_usrId, usuario.Id);
                            Session.SetInt32(Values.Session_usrArea, usuario.P_Grupo);
                            Session.SetString(Values.Session_usrUsername, usuario.P_Username);
                            Session.SetString(Values.Session_usrPassword, model.Input.PasswordBD);
                            return RedirectToAction(Values.Index, Values.HomeClinics, new { area = Values.AreaClinics });
                        }
                        else if (usuario.P_Grupo == 2)
                        {
                            return RedirectToAction(Values.Index, Values.HomePacientes, new { area = Values.PacientesArea });
                        }
                        else
                        {
                            return RedirectToAction(Values.Index, Values.Home, new { area = Values.AreaVacio });
                        }
                    }
                    else
                    {
                        model.ErrorMessage = true;
                        return RedirectToAction(Values.Index, Values.IniciarSesion, new { area = Values.AreaVacio });
                    }
                }
                catch (Exception ex)
                {
                    model.ErrorMessage = true;
                    return View(Values.Index, model);
                }
            }
            else
            {
                model.ErrorMessage = true;
                return View(Values.Index, model);
            }
        }

        /// <summary>
        /// Cerrar sesión del usuario
        /// </summary>
        /// <returns></returns>
        public IActionResult LogOutUser()
        {
            try
            {
                Session.Remove(Values.Session_usrId);
                Session.Remove(Values.Session_usrArea);
                Session.Remove(Values.Session_usrUsername);
            }
            catch
            {
                Session.Remove(Values.Session_usrUsername);
                Session.Remove(Values.Session_usrArea);
                Session.Remove(Values.Session_usrId);
            }
            return RedirectToAction(Values.Index, Values.IniciarSesion, new { area = Values.AreaVacio });
        }
    }
}
