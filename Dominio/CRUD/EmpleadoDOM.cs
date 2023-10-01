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
        ConfiguracionSueldosEmpleadoDAO configuracionSueldosDAO;
        ConfiguracionImpuestosEmpleadoDAO configuracionImpuestosDAO;

        public EmpleadoDOM()
        {
            empleadoDAO = new EmpleadoDAO();
            configuracionSueldosDAO = new ConfiguracionSueldosEmpleadoDAO();
            configuracionImpuestosDAO = new ConfiguracionImpuestosEmpleadoDAO();
        }
        public EmpleadoDTO ObtenerEmpleado(int numeroEmpleado)
        {
            return empleadoDAO.ObtenerEmpleadoPorNumero(numeroEmpleado);
        }

        public void GuardarEmpleado(EmpleadoDTO empleadoDTO, ConfiguracionSueldosEmpleadoDTO configuracionSueldosDTO,
                                    List<ConfiguracionImpuestosEmpleadoDTO> listaConfiguracionImpuestosDTO)
        {
            empleadoDAO.GuardarEmpleado(empleadoDTO);
            configuracionSueldosDAO.GuardarConfiguracionSueldosEmpleado(configuracionSueldosDTO);
            configuracionImpuestosDAO.GuardarConfiguracionImpuestosEmpleado(listaConfiguracionImpuestosDTO);

        }

        public void ActualizarEmpleado(EmpleadoDTO empleadoDTO)
        {
            empleadoDAO.ActualizarEmpleado(empleadoDTO);
        }

        public ConfiguracionSueldosEmpleadoDTO ObtenerConfiguracionSueldos(int numeroEmpleado)
        {
            return configuracionSueldosDAO.ObtenerConfiguracionSueldos(numeroEmpleado);
        }
        public List<ConfiguracionImpuestosEmpleadoDTO> ObtenerConfiguracionImpuestos(int numeroEmpleado)
        {
            return configuracionImpuestosDAO.ObtenerConfiguracionImpuestos(numeroEmpleado);
        }

    }
}
