﻿using Salao.Domain.Abstract;
using Salao.Domain.Abstract.Admin;
using Salao.Domain.Models.Cliente;
using Salao.Domain.Service.Admin;
using Salao.Domain.Service.Cliente;
using Salao.Domain.Service.Endereco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Salao.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class EmpresaController : Controller
    {
        IBaseService<Empresa> serviceEmpresa;
        ICadastroEmpresa serviceCadastro;
        ILogin login;

        public EmpresaController()
        {
            serviceEmpresa = new EmpresaService();
            serviceCadastro = new CadastroEmpresaService();
            login = new UsuarioService();
        }
        //
        // GET: /Admin/Empresa/
        public ActionResult Index(string fantasia = "")
        {
            fantasia = fantasia.ToUpper().Trim();

            var empresas = serviceEmpresa.Listar()
                .Where(x => fantasia == "" || x.Fantasia.Contains(fantasia))
                .OrderBy(x => x.Fantasia);

            return View(empresas);
        }

        //
        // GET: /Admin/Empresa/Details/5
        public ActionResult Details(int id)
        {
            var cadastro = serviceCadastro.Find(id);

            if (cadastro == null)
            {
                return HttpNotFound();
            }

            return View(cadastro);
        }

        //
        // GET: /Admin/Empresa/Create
        public ActionResult Create()
        {
            // TODO: desconto, descontocarencia -> not hard code            
            var cadastro = new CadastroEmpresa { Desconto = 100, DescontoCarencia = 3, Cortesia = true };

            ViewBag.TipoPessoa = GetTipoPessoa(1);
            ViewBag.TipoEndereco = GetTipoEndereco();
            ViewBag.Estados = GetEstados();

            return View(cadastro);
        }

        //
        // POST: /Admin/Empresa/Create
        [HttpPost]
        public ActionResult Create(CadastroEmpresa cadastro)
        {
            try
            {
                cadastro.CadastradoPor = login.GetIdUsuario(System.Web.HttpContext.Current.User.Identity.Name);
                
                if (ModelState.IsValid)
                {
                    serviceCadastro.Incluir(cadastro);
                    // TODO - inclusao do salao
                    return RedirectToAction("Index");
                }

                ViewBag.TipoPessoa = GetTipoPessoa(1);
                ViewBag.TipoEndereco = GetTipoEndereco();
                ViewBag.Estados = GetEstados();
                return View(cadastro);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                ViewBag.TipoPessoa = GetTipoPessoa(1);
                ViewBag.TipoEndereco = GetTipoEndereco();
                ViewBag.Estados = GetEstados();
                return View(cadastro);
            }
        }

        //
        // GET: /Admin/Empresa/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Empresa/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/Empresa/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var cadastro = serviceCadastro.Find((int)id);
            
            if (cadastro == null)
	        {
                return HttpNotFound();
	        }

            return View(cadastro);
        }

        //
        // POST: /Admin/Empresa/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                serviceCadastro.Excluir(id);
                return RedirectToAction("Index");
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                var cadastro = serviceCadastro.Find(id);
                if (cadastro == null)
                {
                    return HttpNotFound();
                }
                return View(cadastro);
            }
        }

        private List<SelectListItem> GetTipoPessoa(int tipo = 1)
        {
            var tipos = new List<SelectListItem>();
            tipos.Add(new SelectListItem { Text = "FÍSICA", Value = "1", Selected = (tipo == 1) });
            tipos.Add(new SelectListItem { Text = "JURÍDICA", Value = "2", Selected = (tipo == 1) });
            return tipos;
        }

        private List<SelectListItem> GetTipoEndereco(int id = 0)
        {
            var tipos = new TipoEnderecoService().Listar()
                .Where(x => x.Ativo == true).OrderBy(x => x.Descricao);

            var lista = new List<SelectListItem>();
            foreach (var item in tipos)
            {
                lista.Add(new SelectListItem { Text = item.Descricao, Value = item.Id.ToString(), Selected = (item.Id == id) });
            }

            return lista;
        }

        private List<SelectListItem> GetEstados(int id = 0)
        {
            var estados = new EstadoService().Listar()
                .Where(x => x.Ativo == true)
                .OrderBy(x => x.UF);

            var lista = new List<SelectListItem>();
            foreach (var item in estados)
            {
                lista.Add(new SelectListItem { Text = item.UF, Value = item.Id.ToString(), Selected = (item.Id == id) });
            }
            return lista;
        }
    }
}
