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
using TFG_Web.Areas.Clinics.Interface.Services;
using System.IO;
using TFG_Web.Areas.Clinics.Interface.Services.IHistorialClinicoServices;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Hosting;
using System.Runtime.InteropServices;

namespace TFG_Web.Areas.Clinics.Controllers
{
    [Area(Values.AreaClinics)]
    [ViewLayout(Values.LayoutClinics)]
    public class ImagenesEvolucionPaciente : Controller
    {
        #region Dependencias Interfaces
        private ISession Session => HttpContext.Session;
        private readonly ValidarAcceso _validarAcceso = new ValidarAcceso();
        private readonly ViewModel mymodel = new ViewModel();
        private readonly IGetPacientesServices _getPacientesServices;
        private readonly IPacientesServices _pacienteServices;
        private readonly IHistorialClinicoServices _historialClinicoServices;
        private readonly IWebHostEnvironment _environment;
        #endregion

        #region Cosntructor
        public ImagenesEvolucionPaciente(IGetPacientesServices getPacientesServices, IPacientesServices pacienteServices,
            IHistorialClinicoServices historialClinicoServices, IWebHostEnvironment environment)
        {
            this._getPacientesServices = getPacientesServices;
            this._pacienteServices = pacienteServices;
            this._historialClinicoServices = historialClinicoServices;
            this._environment = environment;

        }
        #endregion

        /// <summary>
        /// CampanaPectusExcavatum table Informacion
        /// </summary>
        /// <param name="id">idPaciente</param>
        /// <returns></returns>
        [Route(Values.ImagenesPaciente)]
        public async Task<IActionResult> Index(string id)
        {
            if (!_validarAcceso.ValidarAccesoWeb(Session)) return RedirectToAction(Values.Index, Values.LoginUsuario, new { area = Values.AreaVacio });

            mymodel.ListPacientes = await _getPacientesServices.ListPacientesAsync(Session.GetString(Values.Session_usrId));

            try
            {
                mymodel.Pacientes = await _pacienteServices.GetById(id);
            }
            catch
            {
                return RedirectToAction(Values.Index, Values.Inicio, new { area = Values.AreaVacio });
            }

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

                mymodel.ImageList = ImageList;
            }
            catch
            {
                return RedirectToAction(Values.Index, Values.Inicio, new { area = Values.AreaVacio });
            }

            return View(Values.Index, mymodel);
        }

        /// <summary>
        /// Desde el form input file, guardamos las imagenes UPLOAD a la carpeta de wwwroot/img/Pacientes/id
        /// </summary>
        /// <param name="postedFiles"></param>
        /// <param name="idHistorialClinico"></param>
        /// <param name="idPaciente"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CargarImagenesPaciente(List<IFormFile> postedFiles, string idPaciente)
        {
            if (postedFiles != null)
            {
                bool isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

                string path = String.Empty;
                string path2 = String.Empty;

                ObtenerPathImagenesPaciente(out path);

                if(isWindows)
                {
                    path2 = Path.Combine(path, idPaciente);
                }
                else
                {
                    path2 = path + "/" + idPaciente;
                }

                List<string> uploadedFiles = new List<string>();
                foreach (IFormFile postedFile in postedFiles)
                {
                    if (postedFile.ContentType == "image/jpeg")
                    {
                        try
                        {
                            string fileName = Path.GetFileName(postedFile.FileName);
                            if(isWindows)
                            {
                                using (FileStream stream = new FileStream(Path.Combine(path2, fileName), FileMode.Create))
                                {
                                    postedFile.CopyTo(stream);
                                    uploadedFiles.Add(fileName);
                                }
                            }
                            else
                            {
                                using (FileStream stream = new FileStream(path2 + "/" + fileName, FileMode.Create))
                                {
                                    postedFile.CopyTo(stream);
                                    uploadedFiles.Add(fileName);
                                }
                            } 
                        }
                        catch
                        {
                            return RedirectToAction(Values.Index, Values.Inicio, new { area = Values.AreaVacio });
                        }
                    }
                }
            }

            return RedirectToAction(Values.Index, Values.ImagenesPaciente, new { area = Values.AreaVacio, id = idPaciente });
        }

        /// <summary>
        /// Eliminamos la imagen de la carptea wwwroot/img/Pacientes/id/...
        /// </summary>
        /// <param name="nombreImagen"></param>
        /// <param name="idHistorialClinico"></param>
        /// <param name="idPaciente"></param>
        /// <returns></returns>
        public IActionResult EliminarImagenPaciente(string nombreImagen, string idPaciente)
        {
            bool isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

            string path = String.Empty;
            string path3 = String.Empty;

            ObtenerPathImagenesPaciente(out path);

            if (isWindows)
            {
                string path2 = Path.Combine(path, idPaciente);
                path3 = Path.Combine(path2, nombreImagen);
            }
            else
            {
                path3 = path + "/" + idPaciente + "/" + nombreImagen;
            }

            try
            {
                if (System.IO.File.Exists(path3))
                {
                    System.IO.File.Delete(path3);
                }
            }
            catch
            {
                return RedirectToAction(Values.Index, Values.ImagenesPaciente, new { area = Values.AreaVacio, id = idPaciente });
            }

            return RedirectToAction(Values.Index, Values.ImagenesPaciente, new { area = Values.AreaVacio, id = idPaciente });
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

            if(isWindows)
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
                return File(new byte[]{}, "image/jpg");
            }
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