using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.DTO
{
    public class MovimientoMensualDTO
    {
        public int NumeroEmpleado { get; set; }
        public int CodigoRol { get; set; }
        public int Mes { get; set; }
        public int HorasTrabajadas { get; set; }
        public int CantidadEntregas { get; set; }
        public decimal SueldoBase { get; set; }
        public decimal ImportePagoPorEntregas { get; set; }
        public decimal ImportePagoPorBono { get; set; }
        public decimal ImporteVales { get; set; }
        public decimal ISR { get; set; }
        public decimal ISRAdicional { get; set; }

    }
}
