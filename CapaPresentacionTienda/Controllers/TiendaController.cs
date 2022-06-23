using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaEntidad;
using CapaNegocio;
using System.IO;

namespace CapaPresentacionTienda.Controllers
{
    public class TiendaController : Controller
    {
        // GET: Tienda
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public JsonResult ListarCategoria()
        {
            List<Categoria> lista = new List<Categoria>();
            lista = new CN_Categoria().Listar();

            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarProducto(string idcategoria)
        {
            List<Producto> lista = new List<Producto>();

            bool conversion;

            lista = new CN_Producto().Listar().Select(p => new Producto()
            {
                id_prod = p.id_prod,
                nombre = p.nombre,
                descripcion = p.descripcion,
                oCategoria = p.oCategoria,
                precio = p.precio,
                stock = p.stock,
                rutafoto = p.rutafoto,
                Base64 = CN_Recursos.ConvertirBase64(Path.Combine(p.rutafoto, p.foto), out conversion),
                extension = Path.GetExtension(p.foto),
                estado = p.estado
            }).Where(p =>
                p.oCategoria.id_categoria == (idcategoria == " " ? p.oCategoria.id_categoria : idcategoria) &&
                p.stock > 0 && p.estado == "Habilitado"
            ).ToList();

            var jsonresult = Json(new { data = lista }, JsonRequestBehavior.AllowGet);
            jsonresult.MaxJsonLength = int.MaxValue;

            return jsonresult;

        }

    }
}
