using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CapaEntidad;
using CapaNegocio;

namespace CapaPresentacionTienda.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Registrar()
        {
            return View();
        }

        public ActionResult Reestablecer()
        {
            return View();
        }
        public ActionResult CambiarContra()
        {
            return View();
        }

/***********************      REGISTRAR     ********************************/

        [HttpPost]
        public ActionResult Registrar(Admin objeto)
        {
            string resultado;
            string mensaje = string.Empty;

            //objeto.Opersona.ObTipoPersona.id_tipo.Insert(0, "T002");

            //objeto.ToString().Insert((objeto.ToString().IndexOf("id_tipo = null")), "id_tipo = T002");
            
            ViewData["id_tipo"] = objeto.Opersona.ObTipoPersona.id_tipo = "T002";
            ViewData["Nombre"] = string.IsNullOrWhiteSpace(objeto.Opersona.Nombre) ? "" : objeto.Opersona.Nombre;
            ViewData["Apellido"] = string.IsNullOrWhiteSpace(objeto.Opersona.Apellido) ? "" : objeto.Opersona.Apellido;
            ViewData["correo"] = string.IsNullOrWhiteSpace(objeto.Opersona.correo) ? "" : objeto.Opersona.correo;
            ViewData["Direccion"] = string.IsNullOrWhiteSpace(objeto.Opersona.Direccion) ? "" : objeto.Opersona.Direccion;
            ViewData["Telefono"] = string.IsNullOrWhiteSpace(objeto.Opersona.Telefono) ? "" : objeto.Opersona.Telefono;
            ViewData["usuario"] = string.IsNullOrWhiteSpace(objeto.Ousuario.usuario) ? "" : objeto.Ousuario.usuario;

            if(objeto.Ousuario.contraseña != objeto.Ousuario.ConfirmarContra)
            {
                ViewBag.Error = "Las Contraseña no coinciden";
                return View();
            }

            resultado = new CN_Usuario().Registrar(objeto, out mensaje);
            
            if(resultado == "1")
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

/***********************      LOGEO    ********************************/
        [HttpPost]
        public ActionResult Index(string usuario, string contra)
        {
            Admin oAdmin = new Admin();
            oAdmin = new CN_Usuario().ListarUsuarios().Where(u => u.Ousuario.usuario == usuario && u.Ousuario.contraseña == contra && u.Opersona.ObTipoPersona.id_tipo == "T002      ").FirstOrDefault();

            if (oAdmin == null)
            {
                ViewBag.Error = "CORREO O CONTRASEÑA INCORRECTA";
                return View();
            }
            else
            {
                if (oAdmin.Ousuario.Reestrablecer)
                {
                    TempData["id_usuario"] = oAdmin.Ousuario.id_usuario;
                    return RedirectToAction("CambiarContra", "Acceso");
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(oAdmin.Opersona.Nombre, false);

                    Session["USUARIO"] = oAdmin;
                    ViewBag.Error = null;
                    return RedirectToAction("Index", "Tienda");
                }
            }
        }

 /***********************   Reestablecer   ********************************/
        [HttpPost]
        public ActionResult Reestablecer(string correo)
        {
            Admin oAdmin = new Admin();
            oAdmin = new CN_Usuario().ListarUsuarios().Where(u => u.Opersona.correo == correo && u.Opersona.ObTipoPersona.id_tipo == "T002      ").FirstOrDefault();

            if (oAdmin == null)
            {
                ViewBag.Error = "No se encontro un usuario con este correo";
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

 /***********************   CambiarContraseña   ********************************/
        [HttpPost]
        public ActionResult CambiarContra(string idusuario, string contraActual, string nuevaContra, string ConfrimarContra)
        {
            Admin oAdmin = new Admin();

            oAdmin = new CN_Usuario().ListarUsuarios().Where(u => u.Ousuario.id_usuario == idusuario && u.Opersona.ObTipoPersona.id_tipo == "T002      ").FirstOrDefault();

            if (oAdmin.Ousuario.contraseña != contraActual)
            {
                TempData["id_usuario"] = idusuario;
                ViewData["vclave"] = "";
                ViewBag.Error = "La contraseña actual no es correcta";
                return View();

            }
            else if (nuevaContra != ConfrimarContra)
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

/***********************   Cerrar Sesion   ********************************/
        public ActionResult CerrarSesion()
        {
            Session["USUARIO"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Tienda");
        }

    }
}