namespace ProyectoNomina
{
    partial class FrmCapturaMovimientosPorMes
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtCantidadEntregas = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtHorasTrabajadas = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.cboMes = new System.Windows.Forms.ComboBox();
            this.lblApellidoMaterno = new System.Windows.Forms.Label();
            this.lblApellidoPaterno = new System.Windows.Forms.Label();
            this.lblNombre = new System.Windows.Forms.Label();
            this.lblRol = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNumeroEmpleado = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.epError = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.epError)).BeginInit();
            this.SuspendLayout();
            // 
            // btnNuevo
            // 
            this.btnNuevo.Location = new System.Drawing.Point(16, 15);
            this.btnNuevo.Margin = new System.Windows.Forms.Padding(4);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(100, 28);
            this.btnNuevo.TabIndex = 2;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.UseVisualStyleBackColor = true;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtCantidadEntregas);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.txtHorasTrabajadas);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.cboMes);
            this.groupBox1.Controls.Add(this.lblApellidoMaterno);
            this.groupBox1.Controls.Add(this.lblApellidoPaterno);
            this.groupBox1.Controls.Add(this.lblNombre);
            this.groupBox1.Controls.Add(this.lblRol);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtNumeroEmpleado);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(16, 46);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(471, 255);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // txtCantidadEntregas
            // 
            this.txtCantidadEntregas.Location = new System.Drawing.Point(140, 216);
            this.txtCantidadEntregas.Margin = new System.Windows.Forms.Padding(4);
            this.txtCantidadEntregas.MaxLength = 3;
            this.txtCantidadEntregas.Name = "txtCantidadEntregas";
            this.txtCantidadEntregas.Size = new System.Drawing.Size(75, 22);
            this.txtCantidadEntregas.TabIndex = 3;
            this.txtCantidadEntregas.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCantidadEntregas_KeyPress);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(13, 219);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(121, 16);
            this.label12.TabIndex = 19;
            this.label12.Text = "Cantidad Entregas:";
            // 
            // txtHorasTrabajadas
            // 
            this.txtHorasTrabajadas.Location = new System.Drawing.Point(140, 188);
            this.txtHorasTrabajadas.Margin = new System.Windows.Forms.Padding(4);
            this.txtHorasTrabajadas.MaxLength = 3;
            this.txtHorasTrabajadas.Name = "txtHorasTrabajadas";
            this.txtHorasTrabajadas.Size = new System.Drawing.Size(75, 22);
            this.txtHorasTrabajadas.TabIndex = 2;
            this.txtHorasTrabajadas.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtHorasTrabajadas_KeyPress);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(13, 191);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(121, 16);
            this.label11.TabIndex = 17;
            this.label11.Text = "Horas Trabajadas:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(97, 161);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(36, 16);
            this.label10.TabIndex = 15;
            this.label10.Text = "Mes:";
            // 
            // cboMes
            // 
            this.cboMes.FormattingEnabled = true;
            this.cboMes.Location = new System.Drawing.Point(140, 158);
            this.cboMes.Name = "cboMes";
            this.cboMes.Size = new System.Drawing.Size(139, 24);
            this.cboMes.TabIndex = 1;
            this.cboMes.Validating += new System.ComponentModel.CancelEventHandler(this.cboMes_Validating);
            // 
            // lblApellidoMaterno
            // 
            this.lblApellidoMaterno.BackColor = System.Drawing.Color.LightGray;
            this.lblApellidoMaterno.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblApellidoMaterno.Location = new System.Drawing.Point(140, 100);
            this.lblApellidoMaterno.Name = "lblApellidoMaterno";
            this.lblApellidoMaterno.Size = new System.Drawing.Size(297, 23);
            this.lblApellidoMaterno.TabIndex = 13;
            this.lblApellidoMaterno.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblApellidoPaterno
            // 
            this.lblApellidoPaterno.BackColor = System.Drawing.Color.LightGray;
            this.lblApellidoPaterno.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblApellidoPaterno.Location = new System.Drawing.Point(140, 72);
            this.lblApellidoPaterno.Name = "lblApellidoPaterno";
            this.lblApellidoPaterno.Size = new System.Drawing.Size(297, 23);
            this.lblApellidoPaterno.TabIndex = 12;
            this.lblApellidoPaterno.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblNombre
            // 
            this.lblNombre.BackColor = System.Drawing.Color.LightGray;
            this.lblNombre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNombre.Location = new System.Drawing.Point(140, 44);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(297, 23);
            this.lblNombre.TabIndex = 11;
            this.lblNombre.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRol
            // 
            this.lblRol.BackColor = System.Drawing.Color.LightGray;
            this.lblRol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRol.Location = new System.Drawing.Point(140, 129);
            this.lblRol.Name = "lblRol";
            this.lblRol.Size = new System.Drawing.Size(139, 23);
            this.lblRol.TabIndex = 10;
            this.lblRol.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(100, 133);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 16);
            this.label3.TabIndex = 9;
            this.label3.Text = "Rol:";
            // 
            // txtNumeroEmpleado
            // 
            this.txtNumeroEmpleado.Location = new System.Drawing.Point(140, 15);
            this.txtNumeroEmpleado.Margin = new System.Windows.Forms.Padding(4);
            this.txtNumeroEmpleado.MaxLength = 10;
            this.txtNumeroEmpleado.Name = "txtNumeroEmpleado";
            this.txtNumeroEmpleado.Size = new System.Drawing.Size(139, 22);
            this.txtNumeroEmpleado.TabIndex = 0;
            this.txtNumeroEmpleado.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNumeroEmpleado_KeyPress);
            this.txtNumeroEmpleado.Validating += new System.ComponentModel.CancelEventHandler(this.txtNumeroEmpleado_Validating);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 18);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(124, 16);
            this.label5.TabIndex = 8;
            this.label5.Text = "Número Empleado:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 103);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "Apellido Materno:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 75);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Apellido Paterno:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(75, 47);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Nombre:";
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(387, 309);
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(4);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(100, 28);
            this.btnGuardar.TabIndex = 1;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // epError
            // 
            this.epError.ContainerControl = this;
            // 
            // FrmCapturaMovimientosPorMes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(503, 347);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnNuevo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FrmCapturaMovimientosPorMes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Captura de Movimientos por Mes";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.epError)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtNumeroEmpleado;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cboMes;
        private System.Windows.Forms.Label lblApellidoMaterno;
        private System.Windows.Forms.Label lblApellidoPaterno;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Label lblRol;
        private System.Windows.Forms.TextBox txtCantidadEntregas;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtHorasTrabajadas;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ErrorProvider epError;
    }
}