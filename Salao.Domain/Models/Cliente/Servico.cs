﻿using Salao.Domain.Models.Admin;
using Salao.Domain.Service.Admin;
using Salao.Domain.Service.Cliente;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Salao.Domain.Models.Cliente
{
    public class Servico
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [HiddenInput]
        [Range(1,int.MaxValue, ErrorMessage="Selecione a sub área")]
        [Display(Name="Sub área")]
        public int IdSubArea { get; set; }

        [Required]
        [HiddenInput]
        [Range(1,int.MaxValue, ErrorMessage="Selecione o salão")]
        public int IdSalao { get; set; }

        [Required(ErrorMessage="Informe a descrição do serviço")]
        [Display(Name="Descrição", Prompt="Descrição do serviço/tratamento")]
        [StringLength(60, ErrorMessage="Máximo de 60 caracteres")]
        public string Descricao { get; set; }

        [Required(ErrorMessage="Informe mais detalhes sobre o serviço")]
        [Display(Name="Detalhes")]
        public string Detalhe { get; set; }

        [Required(ErrorMessage="Informe o tempo para realização do serviço")]
        [Display(Name="Duração do serviço")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan? Tempo { get; set; }

        [Required(ErrorMessage="Informe o preço sem desconto")]
        [Display(Name="Preço sem desconto")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal PrecoSemDesconto { get; set; }

        [Required(ErrorMessage="Informe o preço final de venda")]
        [Display(Name="Preço final de venda")]
        [DisplayFormat(DataFormatString="{0:c}")]
        public decimal Preco { get; set; }

        [Required]
        [Display(Name="Alterado em")]
        public DateTime AlteradoEm { get; set; }

        public bool Ativo { get; set; }

        [NotMapped]
        [Display(Name="Sub área")]
        public virtual SubArea SubArea
        {
            get
            {
                return new SubAreaService().Find(IdSubArea);
            }
        }

        [NotMapped]
        [Display(Name = "Área")]
        public virtual Area Area
        {
            get
            {
                return new AreaService().Find(SubArea.IdArea);
            }
        }

        [NotMapped]
        [Display(Name = "Salão")]
        public virtual Salao Salao
        {
            get
            {
                return new SalaoService().Find(IdSalao);
            }
        }
    }
}
