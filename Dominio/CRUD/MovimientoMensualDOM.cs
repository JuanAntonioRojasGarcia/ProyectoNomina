using AccesoDatos.DAO;
using Servicios.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.CRUD
{
    public class MovimientoMensualDOM
    {
        EmpleadoDAO empleadoDAO;
        ConfiguracionSueldosEmpleadoDAO configuracionSueldosDAO;
        ConfiguracionImpuestosEmpleadoDAO configuracionImpuestosDAO;
        RolDAO rolDAO;
        MovimientoMensualDAO movimientoMensualDAO;

        public MovimientoMensualDOM()
        {
            empleadoDAO = new EmpleadoDAO();
            configuracionSueldosDAO = new ConfiguracionSueldosEmpleadoDAO();
            configuracionImpuestosDAO = new ConfiguracionImpuestosEmpleadoDAO();
            rolDAO = new RolDAO();
            movimientoMensualDAO = new MovimientoMensualDAO();
        }

        public MovimientoMensualDTO ObtenerMovimientoMensual(int numeroEmpleado, int codigoRol, int mes)
        {
            return movimientoMensualDAO.ObtenerMovimientoMensual(numeroEmpleado, codigoRol, mes);
        }

        public RolDTO ObtenerRol(int codigoRol)
        {
            return rolDAO.ObtenerRolPorCodigo(codigoRol);
        }

    }
}
