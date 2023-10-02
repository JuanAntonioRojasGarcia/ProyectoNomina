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

        public MovimientoMensualDTO ObtenerMovimientoSueldo(int numeroEmpleado, int codigoRol, int mes)
        {
            return movimientoMensualDAO.ObtenerMovimientoSueldo(numeroEmpleado, codigoRol, mes);
        }

        public void GuardarMovimientoSueldo(MovimientoMensualDTO movimientoDTO)
        {
            movimientoMensualDAO.GuardarMovimientoSueldo(movimientoDTO);
        }

        public void ActualizarMovimientoSueldo(MovimientoMensualDTO movimientoDTO)
        {
            movimientoMensualDAO.ActualizarMovimientoSueldo(movimientoDTO);
        }

        public RolDTO ObtenerRol(int codigoRol)
        {
            return rolDAO.ObtenerRolPorCodigo(codigoRol);
        }

        public decimal ObtenerSubTotalSueldo(int horasTrabajadas, int cantidadEntregas, ConfiguracionSueldosEmpleadoDTO configuracionSueldos, RolDTO rolDTO)
        {
            decimal subtotal = 0m;
            subtotal += CalcularSueldoBaseMensual(horasTrabajadas, configuracionSueldos.SueldoBasePorHora);
            subtotal += CalcularPagoPorEntregas(cantidadEntregas, configuracionSueldos.PagoPorEntrega);
            subtotal += CalcularPagoPorBonos(horasTrabajadas, rolDTO.BonoPorHora);

            return subtotal;
        }

        public decimal CalcularSueldoBaseMensual(int horasTrabajadas, decimal sueldoBasePorHora)
        {
            return Math.Round(Convert.ToDecimal(horasTrabajadas * sueldoBasePorHora), 2);
        }
        public decimal CalcularPagoPorEntregas(int cantidadEntregas, decimal pagoPorEntrega)
        {
            return Math.Round(Convert.ToDecimal(cantidadEntregas * pagoPorEntrega),2);
        }

        public decimal CalcularPagoPorBonos(int horasTrabajadas, decimal bonoPorHora)
        {
            return Math.Round(Convert.ToDecimal(horasTrabajadas * bonoPorHora),2);
        }

        public decimal CalcularImporteVales(decimal sueldo, decimal porcentajeVales)
        {
            return Math.Round(Convert.ToDecimal(sueldo * (porcentajeVales / 100)),2);
        }

        public decimal CalcularISR(decimal sueldo, decimal porcentajeISR)
        {
            return Math.Round(Convert.ToDecimal(sueldo * (porcentajeISR / 100)),2);
        }
        public decimal CalcularISRAdicional(decimal sueldo, decimal porcentajeISRAdicional)
        {
            return Math.Round(Convert.ToDecimal(sueldo * (porcentajeISRAdicional / 100)),2);
        }
    }
}
