using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.DTO
{
    public class ConfiguracionSueldosEmpleadoDTO
    {
        public int NumeroEmpleado { get; set; }
        public decimal SueldoBasePorHora { get; set; }
        public decimal PagoPorEntrega { get; set; }
        public decimal PorcentajeVales { get; set; }
        public decimal LimiteSueldoMensual { get; set; }

    }
}
