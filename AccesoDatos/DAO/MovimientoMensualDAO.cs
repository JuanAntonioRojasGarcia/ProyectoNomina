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
    public class MovimientoMensualDAO
    {
        ConnectionDB db;

        public MovimientoMensualDAO()
        {
            db = new ConnectionDB();
        }
        public MovimientoMensualDTO ObtenerMovimientoSueldo(int numeroEmpleado, int codigoRol, int mes)
        {
            try
            {
                MovimientoMensualDTO movimientoDTO = null;
                DataTable dt = new DataTable();
                db.StoredProcedureName = "SP_ObtenerMovimientosSueldos";
                db.CleanParameters();
                db.AddParameter("@NumeroEmpleado", System.Data.SqlDbType.Int, numeroEmpleado);
                db.AddParameter("@CodigoRol", System.Data.SqlDbType.Int, codigoRol);
                db.AddParameter("@Mes", System.Data.SqlDbType.Int, mes);
                db.OpenConnection();
                db.ExecuteQuery(out dt);

                if (dt.Rows.Count > 0)
                {
                    movimientoDTO = new MovimientoMensualDTO();
                    movimientoDTO.NumeroEmpleado = (int)dt.Rows[0].ItemArray[0];
                    movimientoDTO.CodigoRol = (int)dt.Rows[0].ItemArray[1];
                    movimientoDTO.Mes = (int)dt.Rows[0].ItemArray[2];
                    movimientoDTO.HorasTrabajadas = (int)dt.Rows[0].ItemArray[3];
                    movimientoDTO.CantidadEntregas = (int)dt.Rows[0].ItemArray[4];
                    movimientoDTO.SueldoBase = (decimal)dt.Rows[0].ItemArray[5];
                    movimientoDTO.ImportePagoPorEntregas = (decimal)dt.Rows[0].ItemArray[6];
                    movimientoDTO.ImportePagoPorBono = (decimal)dt.Rows[0].ItemArray[7];
                    movimientoDTO.ImporteVales = (decimal)dt.Rows[0].ItemArray[8];
                    movimientoDTO.ISR = (decimal)dt.Rows[0].ItemArray[9];
                    movimientoDTO.ISRAdicional = (decimal)dt.Rows[0].ItemArray[10];

                }

                return movimientoDTO;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el movimiento mensual", ex);
            }
            finally
            {
                db.CloseConnection();
            }
        }

        public void GuardarMovimientoSueldo(MovimientoMensualDTO movimientoDTO)
        {
            try
            {
                db.StoredProcedureName = "SP_GuardarMovimientosSueldos";
                db.CleanParameters();
                db.AddParameter("@NumeroEmpleado", SqlDbType.Int, movimientoDTO.NumeroEmpleado);
                db.AddParameter("@CodigoRol", SqlDbType.Int, movimientoDTO.CodigoRol);
                db.AddParameter("@Mes", SqlDbType.Int, movimientoDTO.Mes);
                db.AddParameter("@HorasTrabajadas", SqlDbType.Int, movimientoDTO.HorasTrabajadas);
                db.AddParameter("@CantidadEntregas", SqlDbType.Int, movimientoDTO.CantidadEntregas);
                db.AddParameter("@SueldoBase", SqlDbType.Money, movimientoDTO.SueldoBase);
                db.AddParameter("@ImportePagoPorEntregas", SqlDbType.Money, movimientoDTO.ImportePagoPorEntregas);
                db.AddParameter("@ImportePagoPorBono", SqlDbType.Money, movimientoDTO.ImportePagoPorBono);
                db.AddParameter("@ImporteVales", SqlDbType.Money, movimientoDTO.ImporteVales);
                db.AddParameter("@ISR", SqlDbType.Money, movimientoDTO.ISR);
                db.AddParameter("@ISRAdicional", SqlDbType.Money, movimientoDTO.ISRAdicional);

                db.OpenConnection();
                db.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar el movimiento mensual", ex);
            }
            finally
            {
                db.CloseConnection();
            }
        }

        public void ActualizarMovimientoSueldo(MovimientoMensualDTO movimientoDTO)
        {
            try
            {
                db.StoredProcedureName = "SP_ActualizarMovimientosSueldos";
                db.CleanParameters();
                db.AddParameter("@NumeroEmpleado", SqlDbType.Int, movimientoDTO.NumeroEmpleado);
                db.AddParameter("@CodigoRol", SqlDbType.Int, movimientoDTO.CodigoRol);
                db.AddParameter("@Mes", SqlDbType.Int, movimientoDTO.Mes);
                db.AddParameter("@HorasTrabajadas", SqlDbType.Int, movimientoDTO.HorasTrabajadas);
                db.AddParameter("@CantidadEntregas", SqlDbType.Int, movimientoDTO.CantidadEntregas);
                db.AddParameter("@SueldoBase", SqlDbType.Money, movimientoDTO.SueldoBase);
                db.AddParameter("@ImportePagoPorEntregas", SqlDbType.Money, movimientoDTO.ImportePagoPorEntregas);
                db.AddParameter("@ImportePagoPorBono", SqlDbType.Money, movimientoDTO.ImportePagoPorBono);
                db.AddParameter("@ImporteVales", SqlDbType.Money, movimientoDTO.ImporteVales);
                db.AddParameter("@ISR", SqlDbType.Money, movimientoDTO.ISR);
                db.AddParameter("@ISRAdicional", SqlDbType.Money, movimientoDTO.ISRAdicional);

                db.OpenConnection();
                db.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el movimiento mensual", ex);
            }
            finally
            {
                db.CloseConnection();
            }
        }

    }
}
