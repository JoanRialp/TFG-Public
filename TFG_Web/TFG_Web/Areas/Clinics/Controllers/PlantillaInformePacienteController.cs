using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TFG_Web.Models;
using TFG_Web.Services;
using TFG_Web.Areas.Clinics.Models;
using Rotativa.AspNetCore;
using TFG_Web.Areas.Clinics.Interface.Services.IHistorialClinicoServices;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using System.Runtime.InteropServices;
using TFG_Web.Areas.Clinics.Interface.Services;

namespace TFG_Web.Areas.Clinics.Controllers
{
    [Area(Values.AreaClinics)]
    public class PlantillaInformePacienteController : Controller
    {
        #region Dependencias Interfaces
        private ISession Session => HttpContext.Session;
        private readonly ValidarAcceso _validarAcceso = new ValidarAcceso();
        private readonly IHistorialClinicoServices _historialClinicoServices;
        private readonly IPacientesServices _pacientesServices;
        private readonly ViewModel viewModel = new ViewModel();
        private readonly IWebHostEnvironment _environment;
        #endregion

        #region Constructor
        public PlantillaInformePacienteController(IHistorialClinicoServices historialClinicoServices, IPacientesServices pacientesServices, IWebHostEnvironment environment)
        {
            this._historialClinicoServices = historialClinicoServices;
            this._environment = environment;
            this._pacientesServices = pacientesServices;
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">id Paciente</param>
        /// <returns></returns>
        [Route(Values.InformePaciente)]
        public async Task<IActionResult> Index(string id)
        {
            if (!_validarAcceso.ValidarAccesoWeb(Session)) return RedirectToAction(Values.Index, Values.LoginUsuario, new { area = Values.AreaVacio });

            Pacientes paciente = await _pacientesServices.GetById(id);
            viewModel.Pacientes = paciente;

            //TODO: Historial 1 y 2

            HistorialClinico mymodel = await _historialClinicoServices.GetById(paciente.HistorialClinicoId.First());

            viewModel.HistorialClinico = mymodel;

            //Cargar los nombres de las imagenes de la Carptea del Historial Clinico (Pacientes)
            try
            {
                string path = String.Empty;
                ObtenerPathImagenesPaciente(out path);

                var provider = new PhysicalFileProvider(path);
                var contents = provider.GetDirectoryContents(id);
                var objFiles = contents.OrderBy(m => m.LastModified);

                List<string> ImageList = new List<string>();
                foreach (var item in objFiles.ToList())
                {
                    ImageList.Add(item.Name);
                }

                viewModel.ImageList = ImageList;
            }
            catch (Exception ex)
            {
                List<string> ImageList = new List<string>();
                viewModel.ImageList = ImageList;
            }

            return new ViewAsPdf(Values.Index, viewModel)
            {
                //Per descargar directament
                //FileName = mymodel.HC_Nombre+".pdf"
            };
        }

        /// <summary>
        /// Obtenemos las imagenes del paciente, de la ruta local del servidor (Se llama desde el cshtml)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nameImage"></param>
        /// <returns></returns>
        public FileContentResult ObtenerImagenesPaciente(string id, string nameImage)
        {
            bool isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

            string path = String.Empty;
            string path2 = String.Empty;

            ObtenerPathImagenesPaciente(out path);

            if (isWindows)
            {
                path = Path.Combine(path, id);
                path2 = Path.Combine(path, nameImage);
            }
            else
            {
                path2 = path + "/" + id + "/" + nameImage;
            }

            try
            {
                byte[] bytes = System.IO.File.ReadAllBytes(path2);
                return File(bytes, "image/jpg");
            }
            catch
            {
                return File(new byte[] { }, "image/jpg");
            }
        }

        private async Task<IActionResult> DescargarInformePaciente(string id)
        {
            if (!_validarAcceso.ValidarAccesoWeb(Session)) return RedirectToAction(Values.Index, Values.LoginUsuario, new { area = Values.AreaVacio });

            HistorialClinico mymodel = await _historialClinicoServices.GetById(id);

            viewModel.HistorialClinico = mymodel;

            return new ViewAsPdf(Values.Index, viewModel)
            {
                //Per descargar directament
                FileName = "InformePaciente_" + mymodel.HC_Nombre + mymodel.HC_Apellidos + ".pdf"
            };
        }


        /// <summary>
        /// Devolvemos la ruta (path) de donde esta la carpeta de las imaganes del Paciente
        /// </summary>
        /// <param name="path"></param>
        private void ObtenerPathImagenesPaciente(out string path)
        {
            bool isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
            string result_Path = String.Empty;

            if (isWindows)
            {
                result_Path = Path.Combine("C:\\ppt-huav", "Pacientes");
            }
            else
            {
                result_Path = "/ppt-huav/Pacientes";
            }

            path = result_Path;
        }
    }
}