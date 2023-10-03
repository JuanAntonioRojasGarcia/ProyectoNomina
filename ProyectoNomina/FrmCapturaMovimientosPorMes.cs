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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace ProyectoNomina
{
    public partial class FrmCapturaMovimientosPorMes : Form
    {
        EmpleadoDOM empleadoDOM;
        MovimientoMensualDOM movimientoDOM;
        EmpleadoDTO empleadoDTO;
        ConfiguracionSueldosEmpleadoDTO configuracionSueldosDTO;
        List<ConfiguracionImpuestosEmpleadoDTO> listaConfiguracionImpuestosDTO;
        RolDTO rolDTO;
        MovimientoMensualDTO movimientoDTO;
        bool esActualizacion;

        public FrmCapturaMovimientosPorMes()
        {
            InitializeComponent();

            empleadoDOM = new EmpleadoDOM();
            movimientoDOM = new MovimientoMensualDOM();
            empleadoDTO = new EmpleadoDTO();
            configuracionSueldosDTO = new ConfiguracionSueldosEmpleadoDTO();
            listaConfiguracionImpuestosDTO = new List<ConfiguracionImpuestosEmpleadoDTO>();
            rolDTO = new RolDTO();
            movimientoDTO = new MovimientoMensualDTO();
            esActualizacion = false;

            cboMes.DataSource = LlenarComboMes();
            cboMes.DisplayMember = "Descripcion";
            cboMes.ValueMember = "NumeroMes";
        }

        private void Nuevo()
        {
            try
            {
                LimpiarErrorProvider();
                this.empleadoDTO = new EmpleadoDTO();
                this.configuracionSueldosDTO = new ConfiguracionSueldosEmpleadoDTO();

                listaConfiguracionImpuestosDTO.Clear();
                rolDTO = new RolDTO();
                movimientoDTO = new MovimientoMensualDTO();

                txtNumeroEmpleado.Text = string.Empty;
                txtNumeroEmpleado.Enabled = true;
                lblNombre.Text = string.Empty;
                lblApellidoPaterno.Text = string.Empty;
                lblApellidoMaterno.Text = string.Empty;
                lblRol.Text = string.Empty;
                cboMes.Text = string.Empty;
                txtHorasTrabajadas.Text = string.Empty;
                txtCantidadEntregas.Text = string.Empty;

                //esActualizacion = false;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al limpiar." + ex.Message, ex);
            }
        }

        private List<Mes> LlenarComboMes()
        {
            List<Mes> listaMeses = new List<Mes>();

            listaMeses.Add(new Mes { NumeroMes = 1, Descripcion = "Enero" });
            listaMeses.Add(new Mes { NumeroMes = 2, Descripcion = "Febrero" });
            listaMeses.Add(new Mes { NumeroMes = 3, Descripcion = "Marzo" });
            listaMeses.Add(new Mes { NumeroMes = 4, Descripcion = "Abril" });
            listaMeses.Add(new Mes { NumeroMes = 5, Descripcion = "Mayo" });
            listaMeses.Add(new Mes { NumeroMes = 6, Descripcion = "Junio" });
            listaMeses.Add(new Mes { NumeroMes = 7, Descripcion = "Julio" });
            listaMeses.Add(new Mes { NumeroMes = 8, Descripcion = "Agosto" });
            listaMeses.Add(new Mes { NumeroMes = 9, Descripcion = "Septiembre" });
            listaMeses.Add(new Mes { NumeroMes = 10, Descripcion = "Octubre" });
            listaMeses.Add(new Mes { NumeroMes = 11, Descripcion = "Noviembre" });
            listaMeses.Add(new Mes { NumeroMes = 12, Descripcion = "Diciembre" });

            return listaMeses;
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

                if (txtNumeroEmpleado.Text.Trim() == string.Empty || lblNombre.Text.Trim() == string.Empty)
                {
                    resultado = false;
                    epError.SetError(txtNumeroEmpleado, "Capture un Número de Empleado por favor...");
                }

                if (cboMes.SelectedValue == null)
                {
                    resultado = false;
                    epError.SetError(cboMes, "Seleccione un Mes por favor...");
                }

                if (txtHorasTrabajadas.Text == string.Empty)
                {
                    resultado = false;
                    epError.SetError(txtHorasTrabajadas, "Capture la horas trabajadas en el Mes por favor...");
                }

                if (txtCantidadEntregas.Text == string.Empty)
                {
                    resultado = false;
                    epError.SetError(txtCantidadEntregas, "Capture la cantidad de entregas en el Mes por favor...");
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
                        //Guardar registro del movimiento de sueldo mensual
                        this.Cursor = Cursors.WaitCursor;

                        movimientoDTO.NumeroEmpleado = int.Parse(txtNumeroEmpleado.Text.Trim());
                        movimientoDTO.CodigoRol = rolDTO.CodigoRol;
                        movimientoDTO.Mes = int.Parse(cboMes.SelectedValue.ToString());
                        movimientoDTO.HorasTrabajadas = int.Parse(txtHorasTrabajadas.Text.Trim());
                        movimientoDTO.CantidadEntregas = int.Parse(txtCantidadEntregas.Text.Trim());
                        movimientoDTO.SueldoBase = movimientoDOM.CalcularSueldoBaseMensual(int.Parse(txtHorasTrabajadas.Text.Trim()), configuracionSueldosDTO.SueldoBasePorHora);
                        movimientoDTO.ImportePagoPorEntregas = movimientoDOM.CalcularPagoPorEntregas(int.Parse(txtCantidadEntregas.Text.Trim()), configuracionSueldosDTO.PagoPorEntrega);
                        movimientoDTO.ImportePagoPorBono = movimientoDOM.CalcularPagoPorBonos(int.Parse(txtHorasTrabajadas.Text.Trim()), rolDTO.BonoPorHora);

                        decimal sueldoSubtotal = movimientoDOM.ObtenerSubTotalSueldo(int.Parse(txtHorasTrabajadas.Text.Trim()),
                                                               int.Parse(txtCantidadEntregas.Text.Trim()),
                                                               configuracionSueldosDTO, rolDTO);


                        movimientoDTO.ISR = movimientoDOM.CalcularISR(sueldoSubtotal, listaConfiguracionImpuestosDTO[0].Porcentaje);
                        movimientoDTO.ISRAdicional = 0;

                        if (sueldoSubtotal > configuracionSueldosDTO.LimiteSueldoMensual)
                        {
                            //Agregar ISR Adicional
                            sueldoSubtotal -= movimientoDTO.ISR;
                            movimientoDTO.ISRAdicional = movimientoDOM.CalcularISRAdicional(sueldoSubtotal, listaConfiguracionImpuestosDTO[1].Porcentaje);
                            sueldoSubtotal -= movimientoDTO.ISRAdicional;
                        }
                        else
                        {
                            sueldoSubtotal -= movimientoDTO.ISR;
                        }

                        movimientoDTO.ImporteVales = movimientoDOM.CalcularImporteVales(sueldoSubtotal, configuracionSueldosDTO.PorcentajeVales);

                        movimientoDOM.GuardarMovimientoSueldo(movimientoDTO);

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
                        movimientoDTO.CodigoRol = rolDTO.CodigoRol;
                        movimientoDTO.Mes = int.Parse(cboMes.SelectedValue.ToString());
                        movimientoDTO.HorasTrabajadas = int.Parse(txtHorasTrabajadas.Text.Trim());
                        movimientoDTO.CantidadEntregas = int.Parse(txtCantidadEntregas.Text.Trim());
                        movimientoDTO.SueldoBase = movimientoDOM.CalcularSueldoBaseMensual(int.Parse(txtHorasTrabajadas.Text.Trim()), configuracionSueldosDTO.SueldoBasePorHora);
                        movimientoDTO.ImportePagoPorEntregas = movimientoDOM.CalcularPagoPorEntregas(int.Parse(txtCantidadEntregas.Text.Trim()), configuracionSueldosDTO.PagoPorEntrega);
                        movimientoDTO.ImportePagoPorBono = movimientoDOM.CalcularPagoPorBonos(int.Parse(txtHorasTrabajadas.Text.Trim()), rolDTO.BonoPorHora);

                        decimal sueldoSubtotal = movimientoDOM.ObtenerSubTotalSueldo(int.Parse(txtHorasTrabajadas.Text.Trim()),
                                                               int.Parse(txtCantidadEntregas.Text.Trim()),
                                                               configuracionSueldosDTO, rolDTO);


                        movimientoDTO.ISR = movimientoDOM.CalcularISR(sueldoSubtotal, listaConfiguracionImpuestosDTO[0].Porcentaje);
                        movimientoDTO.ISRAdicional = 0;

                        if (sueldoSubtotal > configuracionSueldosDTO.LimiteSueldoMensual)
                        {
                            //Agregar ISR Adicional
                            sueldoSubtotal -= movimientoDTO.ISR;
                            movimientoDTO.ISRAdicional = movimientoDOM.CalcularISRAdicional(sueldoSubtotal, listaConfiguracionImpuestosDTO[1].Porcentaje);
                            sueldoSubtotal -= movimientoDTO.ISRAdicional;
                        }
                        else
                        {
                            sueldoSubtotal -= movimientoDTO.ISR;
                        }

                        movimientoDTO.ImporteVales = movimientoDOM.CalcularImporteVales(sueldoSubtotal, configuracionSueldosDTO.PorcentajeVales);


                        movimientoDOM.ActualizarMovimientoSueldo(movimientoDTO);

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
                    RolDTO rolDTO = movimientoDOM.ObtenerRol(empleadoDTO.CodigoRol);
                    if (rolDTO != null)
                    {
                        lblRol.Text = rolDTO.Descripcion;
                    }
                    else
                    {
                        MessageBox.Show("El empleado deber tener configurado un rol.", "Proyecto Nomina", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    txtNumeroEmpleado.Text = empleadoDTO.NumeroEmpleado.ToString();
                    lblNombre.Text = empleadoDTO.Nombre;
                    lblApellidoPaterno.Text = empleadoDTO.ApellidoPaterno;
                    lblApellidoMaterno.Text = empleadoDTO.ApellidoMaterno;
                    txtNumeroEmpleado.Enabled = false;
                    this.empleadoDTO = empleadoDTO;
                    this.configuracionSueldosDTO = configuracionSueldosDTO;
                    this.listaConfiguracionImpuestosDTO = listaConfiguracionImpuestosDTO;
                    this.rolDTO = rolDTO;
                }
                else
                {
                    //esActualizacion = false;
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("No se encontró el registro para numero de empleado capturado.", "Proyecto Nomina", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNumeroEmpleado.Focus();
                }
                this.Cursor = Cursors.Default;

            }
            catch (Exception)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error al obtener el registro", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboMes_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (txtNumeroEmpleado.Text.Trim() == string.Empty || lblNombre.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("Capture un numero de empleado válido", "Proyecto Nomina", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNumeroEmpleado.Focus();
                    return;
                }
                if (cboMes.SelectedValue == null)
                {
                    MessageBox.Show("Seleccione un mes válido", "Proyecto Nomina", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;
                    return;
                }

                this.Cursor = Cursors.WaitCursor;
                MovimientoMensualDTO movimientoDTO = movimientoDOM.ObtenerMovimientoSueldo(int.Parse(txtNumeroEmpleado.Text.Trim()),
                                                            empleadoDTO.CodigoRol, int.Parse(cboMes.SelectedValue.ToString()));

                if (movimientoDTO != null)
                {
                    txtHorasTrabajadas.Text = movimientoDTO.HorasTrabajadas.ToString();
                    txtCantidadEntregas.Text = movimientoDTO.CantidadEntregas.ToString();
                    esActualizacion = true;
                    //txtNumeroEmpleado.Enabled = false;
                    this.movimientoDTO = movimientoDTO;

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
                MessageBox.Show("Error al obtener el registro del movimiento mensual", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void txtHorasTrabajadas_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtCantidadEntregas_KeyPress(object sender, KeyPressEventArgs e)
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

        private void btnVerDetalle_Click(object sender, EventArgs e)
        {
            try
            {
                if (!esActualizacion)
                {
                    MessageBox.Show("No hay detalle para mostrar", "Proyecto Nomina", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                FrmSueldoDetalles frmSueldoDetalles = new FrmSueldoDetalles(this.empleadoDTO, this.movimientoDTO, this.rolDTO);
                frmSueldoDetalles.ShowDialog();
            }
            catch (Exception)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error al mostrar el detalle", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    public class Mes
    {
        public int NumeroMes { get; set; }
        public string Descripcion { get; set; }
    }
}
