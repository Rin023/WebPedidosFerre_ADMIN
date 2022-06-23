using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CapaEntidad;
using CapaNegocio;


namespace CapaPresentacionAdmin.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Reestablecer()
        {
            return View();
        }
        public ActionResult CambiarContraseña()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Index(string usuario, string contra)
        {
            Admin oAdmin = new Admin();
            oAdmin = new CN_Usuario().ListarUsuarios().Where(u => u.Ousuario.usuario == usuario && u.Ousuario.contraseña == contra && u.Opersona.ObTipoPersona.id_tipo == "T001      ").FirstOrDefault();
            
            if(oAdmin == null)
            {
                ViewBag.Error = "CORREO O CONTRASEÑA INCORRECTA";
                return View();
            }
            else
            {
                if (oAdmin.Ousuario.Reestrablecer)
                {
                    TempData["id_usuario"] = oAdmin.Ousuario.id_usuario;
                    return RedirectToAction("CambiarContraseña", "Acceso");
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(oAdmin.Opersona.Nombre, false);

                    Session["ADMIN"] = oAdmin;
                    ViewBag.Error = null;
                    return RedirectToAction("Index", "Home");
                }
               
            }
         
        }
        [HttpPost]
        public ActionResult Reestablecer(string correo)
        {
            Admin oAdmin = new Admin();
            oAdmin = new CN_Usuario().ListarUsuarios().Where(u => u.Opersona.correo == correo && u.Opersona.ObTipoPersona.id_tipo == "T001      ").FirstOrDefault();

            if (oAdmin == null)
            {
                ViewBag.Error = "No se encontro un Administrador con este correo";
                return View();
            }

            string mensaje = string.Empty;
            bool respuesta = new CN_Usuario().ReestablecerContra(oAdmin.Ousuario.id_usuario, correo, out mensaje);

            if (respuesta)
            {
                ViewBag.Error = null;
                return RedirectToAction("Index", "Acceso");

            }
            else
            {
                ViewBag.Error = mensaje;
                return View();
            }

        }
        
        [HttpPost]
        public ActionResult CambiarContraseña(string idusuario, string contraActual,string nuevaContra, string ConfrimarContra )
        {
            Admin oAdmin = new Admin();

            oAdmin = new CN_Usuario().ListarUsuarios().Where(u => u.Ousuario.id_usuario == idusuario && u.Opersona.ObTipoPersona.id_tipo == "T001      ").FirstOrDefault();

            if(oAdmin.Ousuario.contraseña != contraActual)
            {
                TempData["id_usuario"] = idusuario;
                ViewData["vclave"] = "";
                ViewBag.Error = "La contraseña actual no es correcta";
                return View();

            }else if(nuevaContra != ConfrimarContra)
            {
                TempData["id_usuario"] = idusuario;
                ViewData["vclave"] = contraActual;
                ViewBag.Error = "La contraseña actual no coinciden";
                return View();
            }
            ViewData["vclave"] = "";

            string mensaje = string.Empty;

            bool respuesta = new CN_Usuario().CambiarContra(idusuario, nuevaContra, out mensaje);

            if (respuesta)
            {
                return RedirectToAction("Index");
            }
            else
            {
                TempData["id_usuario"] = idusuario;

                ViewBag.Error = mensaje;
                return View();
            }

        }

        public ActionResult CerrarSesion()
        {
            Session["ADMIN"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Acceso");
        }

    }
}
