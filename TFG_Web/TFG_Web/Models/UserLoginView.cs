using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TFG_Web.Models
{
    public class UserLoginView
    {
        [BindProperty]
        public InputModelView Input { get; set; }
        [TempData]
        public bool ErrorMessage { get; set; }

        //Modal View
        public class InputModelView
        {
            [Required(ErrorMessage = "El correo del usuario es obligatorio")]
            public string UserBD { get; set; }

            [Required(ErrorMessage = "La contraseña es obligatoria")]
            [DataType(DataType.Password)]
            public string PasswordBD { get; set; }

            public int AreaBD { get; set; }
        }
    }
}
