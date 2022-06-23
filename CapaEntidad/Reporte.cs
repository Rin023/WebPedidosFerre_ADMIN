using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Reporte
    {
        public string FECHA { get; set; }
        public string CLIENTE { get; set; }
        public string USUARIO { get; set; }
        public string PRODUCTO { get; set; }
        public float PRECIO { get; set; }
        public int CANTIDAD { get; set; }
        public float TOT_POR_UNIDADES { get; set; }
        public string IDVENTA { get; set; }
        public string ADMINISTRADOR { get; set; }
       
    
    }
}
