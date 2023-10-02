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
    public class RolDAO
    {
        ConnectionDB db;
        public RolDAO()
        {
            db = new ConnectionDB();
        }

        public RolDTO ObtenerRolPorCodigo(int CodigoRol)
        {
            try
            {
                RolDTO rolDTO = null;
                DataTable dt = new DataTable();
                db.StoredProcedureName = "SP_ObtenerRol";
                db.CleanParameters();
                db.AddParameter("@CodigoRol", System.Data.SqlDbType.Int, CodigoRol);
                db.OpenConnection();
                db.ExecuteQuery(out dt);

                if (dt.Rows.Count > 0)
                {
                    rolDTO = new RolDTO();
                    rolDTO.CodigoRol = (int)dt.Rows[0].ItemArray[0];
                    rolDTO.Descripcion = dt.Rows[0].ItemArray[1].ToString();
                    rolDTO.BonoPorHora = (decimal)dt.Rows[0].ItemArray[2];

                }

                return rolDTO;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el rol", ex);
            }
            finally
            {
                db.CloseConnection();
            }
        }

    }
}
