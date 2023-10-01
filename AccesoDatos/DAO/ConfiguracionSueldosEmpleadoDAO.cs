using AccesoDatos.Conexion;
using Servicios.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.DAO
{
    public class ConfiguracionSueldosEmpleadoDAO
    {
        ConnectionDB db;

        public ConfiguracionSueldosEmpleadoDAO()
        {
            db = new ConnectionDB();
        }

        public ConfiguracionSueldosEmpleadoDTO ObtenerConfiguracionSueldos(int numeroEmpleado)
        {
            try
            {
                ConfiguracionSueldosEmpleadoDTO configuracionDTO = null;
                DataTable dt = new DataTable();
                db.StoredProcedureName = "SP_ObtenerConfiguracionSueldosEmpleado";
                db.CleanParameters();
                db.AddParameter("@NumeroEmpleado", System.Data.SqlDbType.Int, numeroEmpleado);
                db.OpenConnection();
                db.ExecuteQuery(out dt);

                if (dt.Rows.Count > 0)
                {
                    configuracionDTO = new ConfiguracionSueldosEmpleadoDTO();
                    configuracionDTO.NumeroEmpleado = (int)dt.Rows[0].ItemArray[0];
                    configuracionDTO.SueldoBasePorHora = (decimal)dt.Rows[0].ItemArray[1];
                    configuracionDTO.PagoPorEntrega = (decimal)dt.Rows[0].ItemArray[2];
                    configuracionDTO.PorcentajeVales = (decimal)dt.Rows[0].ItemArray[3];
                    configuracionDTO.LimiteSueldoMensual = (decimal)dt.Rows[0].ItemArray[4];
                }

                return configuracionDTO;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la configuracion de sueldos del empleado", ex);
            }
            finally
            {
                db.CloseConnection();
            }
        }

        public void GuardarConfiguracionSueldosEmpleado(ConfiguracionSueldosEmpleadoDTO configuracionDTO)
        {
            try
            {
                db.StoredProcedureName = "SP_GuardarConfiguracionSueldosEmpleado";
                db.CleanParameters();
                db.AddParameter("@NumeroEmpleado", SqlDbType.Int, configuracionDTO.NumeroEmpleado);
                db.AddParameter("@SueldoBasePorHora", SqlDbType.Money, configuracionDTO.SueldoBasePorHora);
                db.AddParameter("@PagoPorEntrega", SqlDbType.Money, configuracionDTO.PagoPorEntrega);
                db.AddParameter("@PorcentajeVales", SqlDbType.Money, configuracionDTO.PorcentajeVales);
                db.AddParameter("@LimiteSueldoMensual", SqlDbType.Money, configuracionDTO.LimiteSueldoMensual);

                db.OpenConnection();
                db.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar la configuracion de sueldos del empleado", ex);
            }
            finally
            {
                db.CloseConnection();
            }
        }

        public void ActualizarConfiguracionSueldosEmpleado(ConfiguracionSueldosEmpleadoDTO configuracionDTO)
        {
            try
            {
                db.StoredProcedureName = "SP_ActualizarConfiguracionSueldosEmpleado";
                db.CleanParameters();
                db.AddParameter("@NumeroEmpleado", SqlDbType.Int, configuracionDTO.NumeroEmpleado);
                db.AddParameter("@SueldoBasePorHora", SqlDbType.Money, configuracionDTO.SueldoBasePorHora);
                db.AddParameter("@PagoPorEntrega", SqlDbType.Money, configuracionDTO.PagoPorEntrega);
                db.AddParameter("@PorcentajeVales", SqlDbType.Money, configuracionDTO.PorcentajeVales);
                db.AddParameter("@LimiteSueldoMensual", SqlDbType.Money, configuracionDTO.LimiteSueldoMensual);

                db.OpenConnection();
                db.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la configuracion de sueldos del empleado", ex);
            }
            finally
            {
                db.CloseConnection();
            }
        }

    }
}
