using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaEntidad;
using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class CD_Categoria
    {
        public List<Categoria> Listar()
        {

            List<Categoria> lista = new List<Categoria>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    string query = "select id_categoria,id_catalogo,nombre_categoria,estado from CATEGORIA";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Categoria()
                            {

                                id_categoria = dr["id_categoria"].ToString(),
                                oCatalogo = new Catalogo() { id_catalogo = dr["id_catalogo"].ToString() },
                                nombre_categoria = dr["nombre_categoria"].ToString(),
                                estado = dr["estado"].ToString()
                            });
                        }
                    }
                }
            }
            catch { 
                lista = new List<Categoria>();
            }

            return lista;
        }

        public string Registrar(Categoria obj, out string Mensaje)
        {
            string id = " ";
            Mensaje = string.Empty;

            try
            {
                using(SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("NUEVA_CATEGORIA", oconexion);
                    cmd.Parameters.AddWithValue("IDCATALOGO", obj.oCatalogo.id_catalogo);
                    cmd.Parameters.AddWithValue("NOMBRE", obj.nombre_categoria);
                    cmd.Parameters.AddWithValue("ESTADO", obj.estado);

                    cmd.Parameters.Add("SALIDA",SqlDbType.VarChar,7).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("MENSAJE", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    id = Convert.ToString(cmd.Parameters["SALIDA"].Value);
                    Mensaje = Convert.ToString(cmd.Parameters["MENSAJE"].Value);
                }
            }
            catch(Exception ex)
            {
                id = " ";
                Mensaje = ex.Message;
            }
            return id;
        }

        public bool Editar(Categoria obj, out string Mensaje)
        {
            bool result = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("ACTUALIZAR_CATEGORIA", oconexion);
                    cmd.Parameters.AddWithValue("IDCATEGORIA", obj.id_categoria);
                    cmd.Parameters.AddWithValue("NEWNOMBRE", obj.nombre_categoria);
                    cmd.Parameters.AddWithValue("NEWESTADO", obj.estado);
                    cmd.Parameters.Add("SALIDA", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("MENSAJE", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    result = Convert.ToBoolean(cmd.Parameters["SALIDA"].Value);
                    Mensaje = Convert.ToString(cmd.Parameters["MENSAJE"].Value);
                    
                }
            }
            catch (Exception ex)
            {
                result = false;
                Mensaje = ex.Message;
            }
            return result;
        }


        public bool Eliminar(string id_categoria)
        {           
            using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("ELIMINAR_CATEGORIA", oconexion);
                    cmd.Parameters.AddWithValue("IDCATE", id_categoria);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    cmd.ExecuteNonQuery();
                }
            return true;  
        }


    }

}
