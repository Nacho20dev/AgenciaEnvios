﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgenciaEnvios.DTOs.DTOs.DTOSeguimiento
{
    public class DTOSeguimiento
    {
        public int? IdEnvio { get; set; }

        public string? comentario { get; set; }

        public int? IdLogueado { get; set; }

        public DateTime? Fecha { get; set; }

    }
}
