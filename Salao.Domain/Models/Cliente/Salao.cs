﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Salao.Domain.Models.Cliente
{
    public class Salao
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(0, int.MaxValue,ErrorMessage="Selecione a empresa")]
        public int IdEmpresa { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Endereço inválido")]
        public int IdEndereco { get; set; }

        public string Fantasia { get; set; }

        [Required]
        [Range(1, 2, ErrorMessage = "Selecione o tipo de pessoa (física/jurídica)")]
        [Display(Name = "Tipo de pessoa")]
        [HiddenInput(DisplayValue = false)]
        public Int16 TipoPessoa { get; set; }

        [Display(Name = "CNPJ")]
        public string Cnpj { get; set; }

        [Display(Name = "CPF")]
        public string Cpf { get; set; }

        public bool Aprovado { get; set; }

        [Display(Name="Exibir site")]
        public bool Exibir { get; set; }

        public bool Ativo { get; set; }

        [Required(ErrorMessage = "Informe o nome para contato na empresa")]
        [StringLength(60, ErrorMessage = "O nome do contato é composto por no máximo 60 caracteres")]
        public string Contato { get; set; }

        public string Sobre { get; set; }

        [Display(Name = "Observações")]
        [DataType(DataType.MultilineText)]
        public string Observ { get; set; }

        [Required]
        [Display(Name = "Cadastrado em")]
        public DateTime CadastradoEm { get; set; }

        public bool Cortesia { get; set; }

        public decimal Desconto { get; set; }

        [Display(Name = "Carência (meses)")]
        public int DescontoCarencia { get; set; }

        [DisplayFormat(DataFormatString="{0:N6}")]
        [Range(-90, 90, ErrorMessage="A latitude varia entre -90 e 90 graus")]
        public double Latitude { get; set; }

        [DisplayFormat(DataFormatString = "{0:N6}")]
        [Range(-179, 180, ErrorMessage="A longitude varia entre -179 e 180 graus")]
        public double Longitude { get; set; }

        [Required]
        [Display(Name = "Alterado em")]
        public DateTime AlteradoEm { get; set; }

        [NotMapped]
        public virtual Empresa Empresa
        {
            get
            {
                return new Service.Cliente.EmpresaService().Find(IdEmpresa);
            }
        }

        [NotMapped]
        public virtual Endereco.Endereco Endereco
        {
            get
            {
                return new Service.Endereco.EnderecoService().Find(IdEndereco);
            }
        }
    }
}
