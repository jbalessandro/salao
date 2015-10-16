﻿using Salao.Domain.Repository;
using System;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace Salao.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer<EFDbContext>(null);

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            if (FormsAuthentication.CookiesSupported == true)
            {
                if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
                {
                    try
                    {
                        // usuario autenticado
                        string username = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
                        string roles = string.Empty;

                        if (!username.Contains("@"))
                        {
                            // usuario administrativo
                            // roles Usuario.tab
                            var usuario = new Salao.Domain.Service.Admin.UsuarioService().Listar().FirstOrDefault(x => x.Login == username);
                            if (usuario != null)
                            {
                                roles = usuario.Roles;
                            }
                        }
                        else
                        {
                            var usuario = new Salao.Domain.Service.Cliente.CliUsuarioService().Listar().FirstOrDefault(x => x.Email == username);
                            if (usuario != null)
                            {
                                roles = usuario.Roles;
                            }
                        }

                        // atribui roles a identidade Principal
                        HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(
                          new System.Security.Principal.GenericIdentity(username, "Forms"), roles.Split(';'));
                    }
                    catch (Exception)
                    {
                        //somehting went wrong
                    }
                }
            }
        }
    }
}
