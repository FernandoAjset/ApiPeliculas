﻿using System.ComponentModel.DataAnnotations;

namespace Peliculas.Entidades
{
    public class EntidadAuditable
    {
        [StringLength(150)]
        public string UsuarioCreacion { get; set; }
        [StringLength(150)]
        public string UsuarioModificacion { get; set; }
    }
}
