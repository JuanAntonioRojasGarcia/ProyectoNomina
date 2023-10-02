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
    public class ConfiguracionImpuestosEmpleadoDAO
    {
        ConnectionDB db;

        public ConfiguracionImpuestosEmpleadoDAO()
        {
            db = new ConnectionDB();
        }

        public List<ConfiguracionImpuestosEmpleadoDTO> ObtenerConfiguracionImpuestos(int numeroEmpleado)
        {
            try
            {
                List<ConfiguracionImpuestosEmpleadoDTO> listaConfiguracion = new List<ConfiguracionImpuestosEmpleadoDTO>();
                ConfiguracionImpuestosEmpleadoDTO configuracionDTO = null;
                DataTable dt = new DataTable();
                db.StoredProcedureName = "SP_ObtenerConfiguracionImpuestosEmpleado";
                db.CleanParameters();
                db.AddParameter("@NumeroEmpleado", System.Data.SqlDbType.Int, numeroEmpleado);
                db.OpenConnection();
                db.ExecuteQuery(out dt);

                foreach (DataRow dr in dt.Rows)
                {
                    configuracionDTO = new ConfiguracionImpuestosEmpleadoDTO();
                    configuracionDTO.NumeroEmpleado = (int)dr.ItemArray[0];
                    configuracionDTO.CodigoImpuesto = (int)dr.ItemArray[1];
                    configuracionDTO.Porcentaje = (decimal)dr.ItemArray[2]; 
                    listaConfiguracion.Add(configuracionDTO);
                }

                return listaConfiguracion;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la configuracion de impuestos del empleado", ex);
            }
            finally
            {
                db.CloseConnection();
            }
        }

        public void GuardarConfiguracionImpuestosEmpleado(List<ConfiguracionImpuestosEmpleadoDTO> listaConfiguracionDTO)
        {
            try
            {
                //Borrar la configuracion antes de Guardar
                BorrarConfiguracionSueldosEmpleado(listaConfiguracionDTO[0].NumeroEmpleado);

                foreach (ConfiguracionImpuestosEmpleadoDTO config in listaConfiguracionDTO)
                {
                    db.StoredProcedureName = "SP_GuardarConfiguracionImpuestosEmpleado";
                    db.CleanParameters();
                    db.AddParameter("@NumeroEmpleado", SqlDbType.Int, config.NumeroEmpleado);
                    db.AddParameter("@CodigoImpuesto", SqlDbType.Int, config.CodigoImpuesto);

                    db.OpenConnection();
                    db.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar la configuracion de impuestos del empleado", ex);
            }
            finally
            {
                db.CloseConnection();
            }
        }

        private void BorrarConfiguracionSueldosEmpleado(int numeroEmpleado)
        {
            try
            {
                db.StoredProcedureName = "SP_BorrarConfiguracionImpuestosEmpleado";
                db.CleanParameters();
                db.AddParameter("@NumeroEmpleado", SqlDbType.Int, numeroEmpleado);

                db.OpenConnection();
                db.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw new Exception("Error al borrar la configuracion de impuestos del empleado", ex);
            }
            finally
            {
                db.CloseConnection();
            }
        }

    }
}

