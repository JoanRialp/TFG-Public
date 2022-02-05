using System;
using System.Collections.Generic;
using TFG_Web.Models.HistorialClinicoCollections;

namespace TFG_Web.Models
{
    public class ViewModel
    {
        public string id_Paciente { get; set; }
        public Usuarios Usuarios { get; set; }

        /// <summary>
        /// Mensaje de eliminacion (Notificacion) - Cache
        /// </summary>
        public bool Notificaciones { get; set; }
        public bool NotificacionesControl { get; set; }
        public string NotificacionesMensaje { get; set; }
        public string NotificacionesPaciente { get; set; }

        /// <summary>
        /// HistorialClinico
        /// </summary>
        public HistorialClinico HistorialClinico { get; set; }
        public HistorialClinico HistorialClinico2 { get; set; }
        public Pacientes Pacientes { get; set; }

        /// <summary>
        /// Listado de pacientes (Search)
        /// </summary>
        public List<Pacientes> ListPacientes { get; set; }

        /// <summary>
        /// Listado Sistema Compresor
        /// </summary>
        public List<ControlSistemaCompresor> ListSistemaCompresor { get; set; }

        /// <summary>
        /// Listado Campana Pectus Excavatum
        /// </summary>
        public List<ControlCampanaPectusExcavatum> ListCampanaPectusExcavatum { get; set; }

        /// <summary>
        /// Historial Clinico
        /// </summary>
        public List<CirugiaPrevia> CirugiaPrevia { get; set; }
        public List<Anamnesis> Anamnesis { get; set; }
        public List<EnfermdedadPreexistente> EnfermdedadPreexistente { get; set; }
        public List<Deporte> Deporte { get; set; }
        public List<SignosSintomasClinicos> SignosSintomasClinicos { get; set; }
        public List<PectusCarinatum> PectusCarinatum { get; set; }
        public List<PectusExcavatum> PectusExcavatum { get; set; }
        public List<PectusMixto> PectusMixto { get; set; }
        public List<SindromePoland> SindromePoland { get; set; }
        public ControlSistemaCompresor ControlSistemaCompresor { get; set; }
        public ControlCampanaPectusExcavatum ControlCampanaPectusExcavatum { get; set; }

        /// <summary>
        /// Listado de Imagenes Historial Clinico Paciente
        /// </summary>
        public List<string> ImageList { get; set; }

    }
}
