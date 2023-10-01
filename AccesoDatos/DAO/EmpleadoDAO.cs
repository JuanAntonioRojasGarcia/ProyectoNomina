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

    public class EmpleadoDAO
    {
        ConnectionDB db;

        public EmpleadoDAO()
        {
            db = new ConnectionDB();
        }

        public EmpleadoDTO ObtenerEmpleadoPorNumero(int numeroEmpleado)
        {
            try
            {
                EmpleadoDTO empleadoDTO = null;
                DataTable dt = new DataTable();
                db.StoredProcedureName = "SP_ObtenerEmpleado";
                db.CleanParameters();
                db.AddParameter("@NumeroEmpleado", System.Data.SqlDbType.Int, numeroEmpleado);
                db.OpenConnection();
                db.ExecuteQuery(out dt);

                if (dt.Rows.Count > 0)
                {
                    empleadoDTO = new EmpleadoDTO();
                    empleadoDTO.NumeroEmpleado = (int)dt.Rows[0].ItemArray[0];
                    empleadoDTO.Nombre = dt.Rows[0].ItemArray[1].ToString();
                    empleadoDTO.ApellidoPaterno = dt.Rows[0].ItemArray[2].ToString();
                    empleadoDTO.ApellidoMaterno = dt.Rows[0].ItemArray[3].ToString();
                    empleadoDTO.CodigoRol = (int)dt.Rows[0].ItemArray[4];
                }

                return empleadoDTO;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el empleado", ex);
            }
            finally
            {
                db.CloseConnection();
            }
        }

        public void GuardarEmpleado(EmpleadoDTO empleadoDTO)
        {
            try
            {
                db.StoredProcedureName = "SP_GuardarEmpleado";
                db.CleanParameters();
                db.AddParameter("@NumeroEmpleado", SqlDbType.Int, empleadoDTO.NumeroEmpleado);
                db.AddParameter("@Nombre", SqlDbType.NVarChar, 50, empleadoDTO.Nombre);
                db.AddParameter("@ApellidoPaterno", SqlDbType.NVarChar, 50, empleadoDTO.ApellidoPaterno);
                db.AddParameter("@ApellidoMaterno", SqlDbType.NVarChar, 50, empleadoDTO.ApellidoMaterno);
                db.AddParameter("@CodigoRol", SqlDbType.Int, empleadoDTO.CodigoRol);

                db.OpenConnection();
                db.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar el empleado", ex);
            }
            finally
            {
                db.CloseConnection();
            }
        }

        public void ActualizarEmpleado(EmpleadoDTO empleadoDTO)
        {
            try
            {
                db.StoredProcedureName = "SP_ActualizarEmpleado";
                db.CleanParameters();
                db.AddParameter("@NumeroEmpleado", SqlDbType.Int, empleadoDTO.NumeroEmpleado);
                db.AddParameter("@Nombre", SqlDbType.NVarChar, 50, empleadoDTO.Nombre);
                db.AddParameter("@ApellidoPaterno", SqlDbType.NVarChar, 50, empleadoDTO.ApellidoPaterno);
                db.AddParameter("@ApellidoMaterno", SqlDbType.NVarChar, 50, empleadoDTO.ApellidoMaterno);
                db.AddParameter("@CodigoRol", SqlDbType.Int, empleadoDTO.CodigoRol);

                db.OpenConnection();
                db.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el registro del empleado", ex);
            }
            finally
            {
                db.CloseConnection();
            }
        }

    }
}
