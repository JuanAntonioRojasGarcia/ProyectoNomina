using Dominio.CRUD;
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
    public partial class FrmEmpleados : Form
    {
        EmpleadoDOM empleadoDOM;
        EmpleadoDTO empleadoDTO;
        ConfiguracionSueldosEmpleadoDTO configuracionSueldosDTO;
        List<ConfiguracionImpuestosEmpleadoDTO> listaConfiguracionImpuestosDTO;
        bool esActualizacion;

        public FrmEmpleados()
        {
            InitializeComponent();

            empleadoDOM = new EmpleadoDOM();
            empleadoDTO = new EmpleadoDTO();
            configuracionSueldosDTO = new ConfiguracionSueldosEmpleadoDTO();
            listaConfiguracionImpuestosDTO = new List<ConfiguracionImpuestosEmpleadoDTO>();
            esActualizacion = false;
        }

        private void Nuevo()
        {
            try
            {
                LimpiarErrorProvider();
                this.empleadoDTO = new EmpleadoDTO();
                this.configuracionSueldosDTO = new ConfiguracionSueldosEmpleadoDTO();
                listaConfiguracionImpuestosDTO.Clear();

                txtNumeroEmpleado.Text = string.Empty;
                txtNumeroEmpleado.Enabled = true;
                txtNombre.Text = string.Empty;
                txtApellidoPaterno.Text = string.Empty;
                txtApellidoMaterno.Text = string.Empty;
                rbChofer.Checked = false;
                rbCargador.Checked = false;
                rbAuxiliar.Checked = false;
                esActualizacion = false;
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
                    epError.SetError(groupBox2, "Seleccione un Rol para el Empleado por favor...");
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
                    if (!esActualizacion)
                    {
                        //Guardar registro de Empleado
                        this.Cursor = Cursors.WaitCursor;

                        empleadoDTO.NumeroEmpleado = int.Parse(txtNumeroEmpleado.Text.Trim());
                        empleadoDTO.Nombre = txtNombre.Text.Trim();
                        empleadoDTO.ApellidoPaterno = txtApellidoPaterno.Text.Trim();
                        empleadoDTO.ApellidoMaterno = txtApellidoMaterno.Text.Trim();
                        empleadoDTO.CodigoRol = ObtenerCodigoRol();

                        configuracionSueldosDTO.NumeroEmpleado = int.Parse(txtNumeroEmpleado.Text.Trim());
                        configuracionSueldosDTO.SueldoBasePorHora = 30.00m;
                        configuracionSueldosDTO.PagoPorEntrega = 5.00m;
                        configuracionSueldosDTO.PorcentajeVales = 4.00m;
                        configuracionSueldosDTO.LimiteSueldoMensual = 10000.00m;


                        ConfiguracionImpuestosEmpleadoDTO configuracionImpuestoDTO = new ConfiguracionImpuestosEmpleadoDTO();
                        configuracionImpuestoDTO.NumeroEmpleado = int.Parse(txtNumeroEmpleado.Text.Trim());
                        configuracionImpuestoDTO.CodigoImpuesto = 1;
                        listaConfiguracionImpuestosDTO.Add(configuracionImpuestoDTO);

                        configuracionImpuestoDTO = new ConfiguracionImpuestosEmpleadoDTO();
                        configuracionImpuestoDTO.NumeroEmpleado = int.Parse(txtNumeroEmpleado.Text.Trim());
                        configuracionImpuestoDTO.CodigoImpuesto = 2;
                        listaConfiguracionImpuestosDTO.Add(configuracionImpuestoDTO);

                        empleadoDOM.GuardarEmpleado(empleadoDTO, configuracionSueldosDTO, listaConfiguracionImpuestosDTO);

                        Nuevo();
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("Registro guardado.", "Proyecto Nomina", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtNumeroEmpleado.Focus();
                    }
                    else
                    {
                        //Actualizar registro de Empleado
                        this.Cursor = Cursors.WaitCursor;

                        empleadoDTO.NumeroEmpleado = int.Parse(txtNumeroEmpleado.Text.Trim());
                        empleadoDTO.Nombre = txtNombre.Text.Trim();
                        empleadoDTO.ApellidoPaterno = txtApellidoPaterno.Text.Trim();
                        empleadoDTO.ApellidoMaterno = txtApellidoMaterno.Text.Trim();
                        empleadoDTO.CodigoRol = ObtenerCodigoRol();

                        empleadoDOM.ActualizarEmpleado(empleadoDTO);

                        Nuevo();
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("Registro actualizado.", "Proyecto Nomina", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtNumeroEmpleado.Focus();
                    }
                }
            }
            catch (Exception)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error al guardar la información", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtNumeroEmpleado_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (txtNumeroEmpleado.Text.Trim() == string.Empty)
                    return;


                this.Cursor = Cursors.WaitCursor;
                EmpleadoDTO empleadoDTO = empleadoDOM.ObtenerEmpleado(int.Parse(txtNumeroEmpleado.Text.Trim()));
                ConfiguracionSueldosEmpleadoDTO configuracionSueldosDTO = empleadoDOM.ObtenerConfiguracionSueldos(int.Parse(txtNumeroEmpleado.Text.Trim()));
                List<ConfiguracionImpuestosEmpleadoDTO> listaConfiguracionImpuestosDTO = empleadoDOM.ObtenerConfiguracionImpuestos(int.Parse(txtNumeroEmpleado.Text.Trim()));

                if (empleadoDTO != null)
                {
                    txtNumeroEmpleado.Text = empleadoDTO.NumeroEmpleado.ToString();
                    txtNombre.Text = empleadoDTO.Nombre;
                    txtApellidoPaterno.Text = empleadoDTO.ApellidoPaterno;
                    txtApellidoMaterno.Text = empleadoDTO.ApellidoMaterno;
                    EstablecerRol(empleadoDTO.CodigoRol);
                    esActualizacion = true;
                    txtNumeroEmpleado.Enabled = false;
                    this.empleadoDTO = empleadoDTO;
                    this.configuracionSueldosDTO = configuracionSueldosDTO;
                    this.listaConfiguracionImpuestosDTO = listaConfiguracionImpuestosDTO;
                }
                else
                {
                    esActualizacion = false;
                    //MessageBox.Show("No se encontró el registro", "Proyecto Nomina", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                this.Cursor = Cursors.Default;

            }
            catch (Exception)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error al obtener el registro", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
