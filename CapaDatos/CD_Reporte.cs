using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaEntidad;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace CapaDatos
{
    public class CD_Reporte
    {
        public List<Reporte> Ventas(string fechainicio,string fechafin,string idventa)
        {

            List<Reporte> lista = new List<Reporte>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {                  
                    SqlCommand cmd = new SqlCommand("REPORTE_VENTAS", oconexion);
                    cmd.Parameters.AddWithValue("FECHAINI", fechainicio);
                    cmd.Parameters.AddWithValue("FECHAFIN", fechafin);
                    cmd.Parameters.AddWithValue("IDVENTA", idventa);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Reporte()
                            {
                                FECHA = dr["FECHA"].ToString(),
                                CLIENTE = dr["CLIENTE"].ToString(),
                                USUARIO = dr["USUARIO"].ToString(),
                                PRODUCTO = dr["PRODUCTO"].ToString(),
                                PRECIO = (float)Convert.ToDecimal(dr["PRECIO"], new CultureInfo("es-NI")),
                                CANTIDAD = Convert.ToInt32(dr["CANTIDAD"]),
                                TOT_POR_UNIDADES = (float)Convert.ToDecimal(dr["TOT_POR_UNIDADES"], new CultureInfo("es-NI")),
                                IDVENTA = dr["IDVENTA"].ToString(),
                                ADMINISTRADOR = dr["ADMINISTRADOR"].ToString(),
                               

                            });
                        }
                    }
                }
            }
            catch
            {
                lista = new List<Reporte>();
            }

            return lista;
        }

        public DashBoard verDashBoard()
        {

            DashBoard objeto = new DashBoard();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                   
                    SqlCommand cmd = new SqlCommand("REPORTE_DASHBOARD", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            objeto = new DashBoard()
                            {
                                TotalUsuarios = Convert.ToInt32(dr["TotalUsuarios"]),
                                TotalProductos = Convert.ToInt32(dr["TotalProductos"]),
                                TotalVentas = Convert.ToInt32(dr["TotalVentas"]),
                                
                            };
                        }
                    }
                }
            }
            catch
            {
                objeto = new DashBoard();
            }

            return objeto;
        }

    }
}
