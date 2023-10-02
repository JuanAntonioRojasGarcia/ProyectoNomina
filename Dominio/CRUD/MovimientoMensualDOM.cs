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

        public void GuardarMovimientoMensual(MovimientoMensualDTO movimientoDTO)
        {
            movimientoMensualDAO.GuardarMovimientoMensual(movimientoDTO);
        }

        public void ActualizarMovimientoMensual(MovimientoMensualDTO movimientoDTO)
        {
            movimientoMensualDAO.ActualizarMovimientoMensual(movimientoDTO);
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
            return Convert.ToDecimal(horasTrabajadas * sueldoBasePorHora);
        }
        public decimal CalcularPagoPorEntregas(int cantidadEntregas, decimal pagoPorEntrega)
        {
            return Convert.ToDecimal(cantidadEntregas * pagoPorEntrega);
        }

        public decimal CalcularPagoPorBonos(int horasTrabajadas, decimal bonoPorHora)
        {
            return Convert.ToDecimal(horasTrabajadas * bonoPorHora);
        }

        public decimal CalcularImporteVales(decimal sueldo, decimal porcentajeVales)
        {
            return Convert.ToDecimal(sueldo * (porcentajeVales / 100));
        }

        public decimal CalcularISR(decimal sueldo, decimal porcentajeISR)
        {
            return Convert.ToDecimal(sueldo * (porcentajeISR / 100));
        }
        public decimal CalcularISRAdicional(decimal sueldo, decimal porcentajeISRAdicional)
        {
            return Convert.ToDecimal(sueldo * (porcentajeISRAdicional / 100));
        }
    }
}
