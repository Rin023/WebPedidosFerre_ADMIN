using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Mail;
using System.Net;


namespace CapaNegocio
{
    public class CN_Recursos
    {
        //Generar Clava
        public static string GenerarClave()
        {
            string contra = Guid.NewGuid().ToString("N").Substring(0,8); //generar clave de 0 a 6 dig
            return contra;
        }

        //Mandar clava el correo
        public static bool EnviarCorreo(string correo, string asunto, string mensaje)
        {
            bool resultado = false;

            try
            {

                MailMessage mail = new MailMessage();
                mail.To.Add(correo);
                mail.From = new MailAddress("adrianafajardo0203@gmail.com");
                mail.Subject = asunto;
                mail.Body = mensaje;
                mail.IsBodyHtml = true;

                var smtp = new SmtpClient() //servidor cliente
                {
                    Credentials = new NetworkCredential("adrianafajardo0203@gmail.com", "emqgnkevruutpuxm"),
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true

                };

                smtp.Send(mail);
                resultado = true;

            }catch(Exception ex)
            {
                resultado = false;
                mensaje = ex.Message;
            }
            return resultado;
        }



        public static string ConvertirBase64(string ruta,out bool conversion)
        {
            string textoBase64 = string.Empty;
            conversion = true;

            try
            {
                byte[] bytes = File.ReadAllBytes(ruta);
                textoBase64 = Convert.ToBase64String(bytes);
            }
            catch
            {
                conversion = false;
            }
            return textoBase64;
        }
    }
}
