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
        RolDTO rol;

        public FrmSueldoDetalles(EmpleadoDTO empleado, MovimientoMensualDTO movimiento, RolDTO rol)
        {
            InitializeComponent();
            this.empleado = empleado;
            this.movimiento = movimiento;
            this.rol = rol;
        }

        private string ObtenerNombreMes(int mes)
        {
            string nombreMes = "";
            switch (mes)
            {
                case 1:
                    nombreMes= "Enero";
                    break;
                case 2:
                    nombreMes = "Febrero";
                    break;
                case 3:
                    nombreMes = "Marzo";
                    break;
                case 4:
                    nombreMes = "Abril";
                    break;
                case 5:
                    nombreMes = "Mayo";
                    break;
                case 6:
                    nombreMes = "Junio";
                    break;
                case 7:
                    nombreMes = "Julio";
                    break;
                case 8:
                    nombreMes = "Agosto";
                    break;
                case 9:
                    nombreMes = "Septiembre";
                    break;
                case 10:
                    nombreMes = "Octubre";
                    break;
                case 11:
                    nombreMes = "Noviembre";
                    break;
                case 12:
                    nombreMes = "Diciembre";
                    break;
            }
            return nombreMes;
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
                lblMes.Text = ObtenerNombreMes(movimiento.Mes);
                lblRol.Text = rol.Descripcion;
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
