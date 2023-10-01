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
    public partial class FrmEmpleados : Form
    {
        public FrmEmpleados()
        {
            InitializeComponent();
        }

        private void Nuevo()
        {
            try
            {
                LimpiarErrorProvider();
                txtNombre.Text = string.Empty;
                txtApellidoPaterno.Text = string.Empty;
                txtApellidoMaterno.Text = string.Empty;
                rbChofer.Checked = false;
                rbCargador.Checked = false;
                rbAuxiliar.Checked = false;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al limpiar." + ex.Message, ex);
            }
        }
        private void LimpiarErrorProvider()
        {
            epError.Clear();
        }
        private bool ValidarInformacion()
        {
            try
            {
                bool resultado = true;
                LimpiarErrorProvider();

                if (txtNumeroEmpleado.Text.Trim() == string.Empty)
                {
                    resultado = false;
                    epError.SetError(txtNumeroEmpleado, "Capture un Número de Empleado por favor...");
                }

                if (txtNombre.Text.Trim() == string.Empty)
                {
                    resultado = false;
                    epError.SetError(txtNombre, "Capture un Nombre por favor...");
                }

                if (txtApellidoPaterno.Text.Trim() == string.Empty)
                {
                    resultado = false;
                    epError.SetError(txtApellidoPaterno, "Capture un Apellido Paterno por favor...");
                }

                if (txtApellidoMaterno.Text.Trim() == string.Empty)
                {
                    resultado = false;
                    epError.SetError(txtApellidoMaterno, "Capture un Apellido Materno por favor...");
                }

                if (!(rbChofer.Checked || rbCargador.Checked || rbAuxiliar.Checked))
                {
                    resultado = false;
                    epError.SetError(groupBox2, "Capture un Rol para el Empleado por favor...");
                }

                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void ValidarCapturaSoloNumeros(KeyPressEventArgs e)
        {
            Clipboard.Clear(); //--> es para que no copie textos.

            if (!(char.IsDigit(e.KeyChar) || char.IsNumber(e.KeyChar)) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private int ObtenerCodigoRol()
        {
            int resultado = 0;
            if (rbChofer.Checked)
            {
                resultado = 1;
            }
            else if (rbCargador.Checked)
            {
                resultado = 2;
            }
            else if (rbAuxiliar.Checked)
            {
                resultado = 3;
            }
            return resultado;
        }

        private void EstablecerRol(int codigoRol)
        {
            switch (codigoRol)
            {
                case 1:
                    rbChofer.Checked = true;
                    break;
                case 2:
                    rbCargador.Checked = true;
                    break;
                case 3:
                    rbAuxiliar.Checked = true;
                    break;
            }
        }

        private void txtNumeroEmpleado_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                ValidarCapturaSoloNumeros(e);
            }
            catch (Exception)
            {
                MessageBox.Show("Error al presionar la tecla", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                Nuevo();
            }
            catch (Exception)
            {
                MessageBox.Show("Error al limpiar el formulario", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarInformacion())
                {
                    //Guardar o Actualizar informacion
                    this.Cursor = Cursors.WaitCursor;
                    
                    /*empleadoDTO.NumeroEmpleado = int.Parse(txtNumeroEmpleado.Text.Trim());
                    empleadoDTO.Nombre = txtNombre.Text.Trim();
                    empleadoDTO.ApellidoPaterno = txtApellidoPaterno.Text.Trim();
                    empleadoDTO.ApellidoMaterno = txtApellidoMaterno.Text.Trim();
                    empleadoDTO.CodigoRol = ObtenerCodigoRol();
                    empleadoDTO.LimiteSueldoMensual = 10000m;
                    empleado.GuardarEmpleado(empleadoDTO);*/

                    Nuevo();
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error al guardar la información", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
