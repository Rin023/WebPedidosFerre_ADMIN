using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Admin
    {
        public string id_admin { get; set; }
        public Persona Opersona { get; set; }
        public Usuario Ousuario { get; set; }
        public CLIENTE_USER OclienteUser { get; set; }
        public  Rol IdRol{ get; set; }

    }
}
