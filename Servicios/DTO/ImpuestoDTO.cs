using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.DTO
{
    public class ImpuestoDTO
    {
        public int CodigoImpuesto { get; set; }
        public string Descripcion { get; set; }
        public decimal Porcentaje { get; set; }
    }
}
