using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Usuario
    {
        private CD_Usuario objCapaDato = new CD_Usuario();

        public List<Admin> ListarUsuarios()
        {
            return objCapaDato.ListarUsuarios();
        }

        public string Registrar(Admin obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Opersona.Nombre) || string.IsNullOrWhiteSpace(obj.Opersona.Nombre))
            {
                Mensaje = "El Nombre no puede estar vacio";
            }
            if (string.IsNullOrEmpty(obj.Opersona.Apellido) || string.IsNullOrWhiteSpace(obj.Opersona.Apellido))
            {
                Mensaje = "El Apellido no puede estar vacio";
            }
            if (string.IsNullOrEmpty(obj.Opersona.Direccion) || string.IsNullOrWhiteSpace(obj.Opersona.Direccion))
            {
                Mensaje = "La Direccion no puede estar vacio";
            }
            if (string.IsNullOrEmpty(obj.Opersona.Telefono) || string.IsNullOrWhiteSpace(obj.Opersona.Telefono))
            {
                Mensaje = "El número de teléfono no puede estar vacio";
            }
            if (string.IsNullOrEmpty(obj.Opersona.correo) || string.IsNullOrWhiteSpace(obj.Opersona.correo))
            {
                Mensaje = "El correo no puede estar vacio";
            }
            if (string.IsNullOrEmpty(obj.Ousuario.usuario) || string.IsNullOrWhiteSpace(obj.Ousuario.usuario))
            {
                Mensaje = "El nombre de usuario no puede estar vacio";
            }
            if (string.IsNullOrEmpty(obj.Ousuario.contraseña) || string.IsNullOrWhiteSpace(obj.Ousuario.contraseña))
            {
                Mensaje = "La contraseña  no puede estar vacio";
            }
            if (string.IsNullOrEmpty(Mensaje))
            {
                return objCapaDato.Registrar(obj, out Mensaje);
            }
            else {

                return Mensaje;

            }

           
        }


        public bool Editar(Admin obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Opersona.Nombre) || string.IsNullOrWhiteSpace(obj.Opersona.Nombre))
            {
                Mensaje = "El Nombre no puede estar vacio";
            }
            if (string.IsNullOrEmpty(obj.Opersona.Apellido) || string.IsNullOrWhiteSpace(obj.Opersona.Apellido))
            {
                Mensaje = "El Apellido no puede estar vacio";
            }
            if (string.IsNullOrEmpty(obj.Opersona.Direccion) || string.IsNullOrWhiteSpace(obj.Opersona.Direccion))
            {
                Mensaje = "La Direccion no puede estar vacio";
            }
            if (string.IsNullOrEmpty(obj.Opersona.Telefono) || string.IsNullOrWhiteSpace(obj.Opersona.Telefono))
            {
                Mensaje = "El número de teléfono no puede estar vacio";
            }
            if (string.IsNullOrEmpty(obj.Opersona.correo) || string.IsNullOrWhiteSpace(obj.Opersona.correo))
            {
                Mensaje = "El correo no puede estar vacio";
            }
            if (string.IsNullOrEmpty(obj.Ousuario.usuario) || string.IsNullOrWhiteSpace(obj.Ousuario.usuario))
            {
                Mensaje = "El nombre de usuario no puede estar vacio";
            }
            if (string.IsNullOrEmpty(obj.Ousuario.contraseña) || string.IsNullOrWhiteSpace(obj.Ousuario.contraseña))
            {
                Mensaje = "La contraseña  no puede estar vacio";
            }
            
            if (string.IsNullOrEmpty(Mensaje))
            {
                return objCapaDato.Editar(obj, out Mensaje);
            }
            else
            {
                return false;
            }
        }

        public bool ReestablecerContra(string id_usuario, string correo, out string Mensaje)
        {
            Mensaje = string.Empty;
            string nuevacontra = CN_Recursos.GenerarClave();
            bool resultado = objCapaDato.ReestablecerContra(id_usuario, nuevacontra, out Mensaje);

            if (resultado)
            {
                string asunto = "CONTRASEÑA REESTABLECIDA";
                string mensaje_correo = "<h3>Su contraseña fue reestablecida</h3></br><p>Su contraseña para acceder es: !clave!</p>";
                mensaje_correo = mensaje_correo.Replace("!clave!", nuevacontra);

                bool respuesta = CN_Recursos.EnviarCorreo(correo, asunto, mensaje_correo);

                if (respuesta)
                {
                    return true;
                }
                else
                {
                    Mensaje = "No se pudo enviar el correo";
                    return false;
                }
            }
            else
            {
                Mensaje = "NO SE PUDO REESTABLECER LA CONTRASEÑA";
                return false;
            }

        }

        public bool CambiarContra(string id_usuario, string nuevaContra, out string Mensaje)
        {
            return objCapaDato.CambiarContra(id_usuario, nuevaContra, out Mensaje);
        }

    }
}
