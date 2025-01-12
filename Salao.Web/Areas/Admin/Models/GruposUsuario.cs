﻿using System.ComponentModel.DataAnnotations;

namespace Salao.Web.Areas.Admin.Models
{
    public class GruposUsuario
    {
        public int Id { get; set; }
        
        [Display(Name="Grupo")]
        public string Descricao { get; set; }

        public bool Selecionado { get; set; }
    }
}