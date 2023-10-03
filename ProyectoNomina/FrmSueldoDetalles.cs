using Servicios.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoNomina
{
    public partial class FrmSueldoDetalles : Form
    {
        EmpleadoDTO empleado;
        MovimientoMensualDTO movimiento;

        public FrmSueldoDetalles(EmpleadoDTO empleado, MovimientoMensualDTO movimiento)
        {
            InitializeComponent();
            this.empleado = empleado;
            this.movimiento = movimiento;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmSueldoDetalles_Load(object sender, EventArgs e)
        {
            try
            {
                lblNombre.Text = empleado.Nombre + " " + empleado.ApellidoPaterno + " " + empleado.ApellidoMaterno;
                lblMes.Text = "";
                lblHorasTrabajadas.Text = movimiento.HorasTrabajadas.ToString();
                lblCantidadEntregas.Text = movimiento.CantidadEntregas.ToString();
                lblSueldoBase.Text = movimiento.SueldoBase.ToString("C2");
                lblPagoPorEntregas.Text = movimiento.ImportePagoPorEntregas.ToString("C2");
                lblPagoPorBono.Text = movimiento.ImportePagoPorBono.ToString("C2");
                lblISR.Text = movimiento.ISR.ToString("C2");
                lblISRAdicional.Text = movimiento.ISRAdicional.ToString("C2");
                lblVales.Text = movimiento.ImporteVales.ToString("C2");
                decimal sueldoTotal = movimiento.SueldoBase + movimiento.ImportePagoPorEntregas +
                                      movimiento.ImportePagoPorBono - (movimiento.ISR + movimiento.ISRAdicional) +
                                      movimiento.ImporteVales;
                lblSueldoTotal.Text = sueldoTotal.ToString("C2");
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar la informacion para el detalle","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
