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
    public partial class frmMDI : Form
    {
        public frmMDI()
        {
            InitializeComponent();
        }

        private void empleadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FrmEmpleados frmEmpleados = new FrmEmpleados();
                frmEmpleados.MdiParent = this;  
                frmEmpleados.Show();
            }
            catch (Exception)
            {
                MessageBox.Show("Error al abrir el formulario", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void movimientosPorMesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FrmCapturaMovimientosPorMes frmCapturaMovimientosPorMes = new FrmCapturaMovimientosPorMes();
                frmCapturaMovimientosPorMes.MdiParent = this;
                frmCapturaMovimientosPorMes.Show();
            }
            catch (Exception)
            {
                MessageBox.Show("Error al abrir el formulario", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
