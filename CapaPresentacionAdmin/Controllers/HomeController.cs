using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaEntidad;
using CapaNegocio;
using ClosedXML.Excel;
using CapaPresentacionAdmin.Permisos;

namespace CapaPresentacionAdmin.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {    
       
        public ActionResult Index()
        {
            return View();
        }
        [PermisosRol(CapaEntidad.Rol.General)]
        public ActionResult Usuarios()
        {
         
            return View();
        }
              
       
        public ActionResult Pedidos()
        {
           return View();
        }
       
        public ActionResult Ventas()
        {
            return View();
        }
        public ActionResult SinPermiso()
        {
          
            return View();
        }

        //USUARIO//
        [HttpGet,PermisosRol(CapaEntidad.Rol.General)]
        public JsonResult ListarUsuarios()
        {
            List<Admin> olista = new List<Admin>();
            olista = new CN_Usuario().ListarUsuarios();

            return Json(new { data = olista }, JsonRequestBehavior.AllowGet);
        }

        //LISTAR USUARIOS
        [HttpGet]
        public JsonResult Listar()
        {
            List<TipoPersona> olista = new List<TipoPersona>();
            olista = new CN_TipoPersona().Listar();

            return Json(new { data = olista }, JsonRequestBehavior.AllowGet);
        }

        //GUARDAR Y EDITAR USUARIOS
        [HttpPost]
        public JsonResult GuardarUsuario(Admin objeto)
        {
            object resultado="0";
            bool resEditar = false;
            string mensaje = string.Empty;

            if(objeto.Ousuario.id_usuario == " ")
            {
                resultado = new CN_Usuario().Registrar(objeto, out mensaje);
            }
            else
            {
                resEditar = new CN_Usuario().Editar(objeto, out mensaje);
            }

            return Json(new { resultado = resultado ,mensaje = mensaje, resEditar = resEditar}, JsonRequestBehavior.AllowGet);

        }

        //DASHBOAR Y REPORTE

        //LISTAR VENTAS
        [HttpGet]
        public JsonResult ListaReporte(string fechaini, string fechafin, string idventa)
        {
            List<Reporte> oLista = new List<Reporte>();
            oLista = new CN_Reporte().Ventas(fechaini, fechafin, idventa);
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }
        //VER TOTALES
        [HttpGet]
        public JsonResult VistaDashBoard()
        {
            DashBoard objeto = new CN_Reporte().verDashBoard();
            return Json(new { resultado = objeto }, JsonRequestBehavior.AllowGet);
        }
        //REPORTE
        [HttpPost]
        public FileResult ExportarVenta(string fechaini, string fechafin, string idventa)
        {
            List<Reporte> oLista = new List<Reporte>();
            oLista = new CN_Reporte().Ventas(fechaini, fechafin, idventa);

            DataTable dt = new DataTable();

            dt.Locale = new System.Globalization.CultureInfo("es-NI");
            dt.Columns.Add("Fecha de venta", typeof(String));
            dt.Columns.Add("Cliente", typeof(String));
            dt.Columns.Add("Usuario", typeof(String));
            dt.Columns.Add("Producto", typeof(String));
            dt.Columns.Add("Precio", typeof(String));
            dt.Columns.Add("Cantidad", typeof(String));
            dt.Columns.Add("Total por unidades", typeof(String));
            dt.Columns.Add("ID de venta", typeof(String));
            dt.Columns.Add("Administrador de venta", typeof(String));

            foreach (Reporte rp in oLista)
            {
                dt.Rows.Add(new object[]
                {
                    rp.FECHA,
                    rp.CLIENTE,
                    rp.USUARIO,
                    rp.PRODUCTO,
                    rp.PRECIO,
                    rp.CANTIDAD,
                    rp.TOT_POR_UNIDADES,
                    rp.IDVENTA,
                    rp.ADMINISTRADOR
                });
            }
            dt.TableName = "Datos";
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ReporteVenta" + DateTime.Now.ToString() + ".xlsx");
                }
            }
        }

    }
}


