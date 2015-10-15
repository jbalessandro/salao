﻿using Salao.Domain.Abstract.Cliente;
using Salao.Domain.Service.Cliente;
using Salao.Web.Areas.Empresa.Models;
using System.Web.Mvc;
using System.Web.Security;

namespace Salao.Web.Areas.Empresa.Controllers
{
    public class LoginController : Controller
    {
        private ILogin service;

        public LoginController()
        {
            service = new CliUsuarioService();
        }

        // GET: Empresa/Login
        public ActionResult Index(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View(new LoginUsuario());
        }

        [HttpPost]
        public ActionResult Index(LoginUsuario loginUsuario, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var usuario = service.ValidaLogin(loginUsuario.Email, loginUsuario.Senha);

                if (usuario != null)
                {
                    FormsAuthentication.SetAuthCookie(usuario.Email, false);
                    Session["IdUsuario"] = usuario.Id;
                    if (Url.IsLocalUrl(returnUrl)
                        && returnUrl.Length > 1
                        && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//")
                        && !returnUrl.StartsWith(@"\//"))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index", "HomeAdm");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Usuário inválido");
                }
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(loginUsuario);
        }
        
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}