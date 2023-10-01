using AccesoDatos.DAO;
using Servicios.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.CRUD
{
    public class EmpleadoDOM
    {
        EmpleadoDAO empleadoDAO;
        public EmpleadoDOM()
        {
            empleadoDAO = new EmpleadoDAO();
        }
        public EmpleadoDTO ObtenerEmpleado(int numeroEmpleado)
        {
            return empleadoDAO.ObtenerEmpleadoPorNumero(numeroEmpleado);
        }

        public void GuardarEmpleado(EmpleadoDTO empleadoDTO)
        {
            empleadoDAO.GuardarEmpleado(empleadoDTO);
        }

        public void ActualizarEmpleado(EmpleadoDTO empleadoDTO)
        {
            empleadoDAO.ActualizarEmpleado(empleadoDTO);
        }

    }
}
