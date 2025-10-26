namespace PROTOTIPOS1
{
    partial class Ped_cotizacion
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
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbRubros = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvPCot = new System.Windows.Forms.DataGridView();
            this.cbSolicitado = new System.Windows.Forms.CheckBox();
            this.cbAprobado = new System.Windows.Forms.CheckBox();
            this.cbDeenegado = new System.Windows.Forms.CheckBox();
            this.bttnEliminar = new System.Windows.Forms.Button();
            this.bttnGuardar = new System.Windows.Forms.Button();
            this.bttnBuscar = new System.Windows.Forms.Button();
            this.bttnModificar = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.dgvTemporal = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cbCotizado = new System.Windows.Forms.CheckBox();
            this.bttnCSesion = new System.Windows.Forms.Button();
            this.bttnCargar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPCot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTemporal)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button1.Location = new System.Drawing.Point(13, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(97, 48);
            this.button1.TabIndex = 0;
            this.button1.Text = "Pagina principal";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label1.Location = new System.Drawing.Point(124, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(232, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nro pedido de cotizacion:";
            // 
            // cmbRubros
            // 
            this.cmbRubros.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbRubros.FormattingEnabled = true;
            this.cmbRubros.Location = new System.Drawing.Point(200, 119);
            this.cmbRubros.Name = "cmbRubros";
            this.cmbRubros.Size = new System.Drawing.Size(121, 21);
            this.cmbRubros.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label2.Location = new System.Drawing.Point(124, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "Rubro:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label4.Location = new System.Drawing.Point(124, 487);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 25);
            this.label4.TabIndex = 6;
            this.label4.Text = "Estado:";
            // 
            // dgvPCot
            // 
            this.dgvPCot.BackgroundColor = System.Drawing.SystemColors.InactiveBorder;
            this.dgvPCot.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPCot.Location = new System.Drawing.Point(127, 327);
            this.dgvPCot.Name = "dgvPCot";
            this.dgvPCot.Size = new System.Drawing.Size(702, 150);
            this.dgvPCot.TabIndex = 8;
            this.dgvPCot.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPCot_CellContentClick);
            // 
            // cbSolicitado
            // 
            this.cbSolicitado.AutoSize = true;
            this.cbSolicitado.BackColor = System.Drawing.Color.Aqua;
            this.cbSolicitado.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbSolicitado.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.cbSolicitado.Location = new System.Drawing.Point(209, 487);
            this.cbSolicitado.Name = "cbSolicitado";
            this.cbSolicitado.Size = new System.Drawing.Size(116, 29);
            this.cbSolicitado.TabIndex = 10;
            this.cbSolicitado.Text = "Solicitado";
            this.cbSolicitado.UseVisualStyleBackColor = false;
            this.cbSolicitado.CheckedChanged += new System.EventHandler(this.cbSolicitado_CheckedChanged);
            // 
            // cbAprobado
            // 
            this.cbAprobado.AutoSize = true;
            this.cbAprobado.BackColor = System.Drawing.Color.Aqua;
            this.cbAprobado.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbAprobado.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.cbAprobado.Location = new System.Drawing.Point(367, 483);
            this.cbAprobado.Name = "cbAprobado";
            this.cbAprobado.Size = new System.Drawing.Size(117, 29);
            this.cbAprobado.TabIndex = 11;
            this.cbAprobado.Text = "Aprobado";
            this.cbAprobado.UseVisualStyleBackColor = false;
            this.cbAprobado.CheckedChanged += new System.EventHandler(this.cbAprobado_CheckedChanged);
            // 
            // cbDeenegado
            // 
            this.cbDeenegado.AutoSize = true;
            this.cbDeenegado.BackColor = System.Drawing.Color.Aqua;
            this.cbDeenegado.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbDeenegado.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.cbDeenegado.Location = new System.Drawing.Point(516, 483);
            this.cbDeenegado.Name = "cbDeenegado";
            this.cbDeenegado.Size = new System.Drawing.Size(122, 29);
            this.cbDeenegado.TabIndex = 12;
            this.cbDeenegado.Text = "Denegado";
            this.cbDeenegado.UseVisualStyleBackColor = false;
            this.cbDeenegado.CheckedChanged += new System.EventHandler(this.cbDeenegado_CheckedChanged);
            // 
            // bttnEliminar
            // 
            this.bttnEliminar.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.bttnEliminar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bttnEliminar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.bttnEliminar.Location = new System.Drawing.Point(538, 525);
            this.bttnEliminar.Name = "bttnEliminar";
            this.bttnEliminar.Size = new System.Drawing.Size(100, 40);
            this.bttnEliminar.TabIndex = 13;
            this.bttnEliminar.Text = "Eliminar";
            this.bttnEliminar.UseVisualStyleBackColor = false;
            this.bttnEliminar.Click += new System.EventHandler(this.bttnEliminar_Click);
            // 
            // bttnGuardar
            // 
            this.bttnGuardar.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.bttnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bttnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.bttnGuardar.Location = new System.Drawing.Point(721, 523);
            this.bttnGuardar.Name = "bttnGuardar";
            this.bttnGuardar.Size = new System.Drawing.Size(110, 40);
            this.bttnGuardar.TabIndex = 14;
            this.bttnGuardar.Text = "Guardar";
            this.bttnGuardar.UseVisualStyleBackColor = false;
            this.bttnGuardar.Click += new System.EventHandler(this.bttnGuardar_Click);
            // 
            // bttnBuscar
            // 
            this.bttnBuscar.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.bttnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bttnBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.bttnBuscar.Location = new System.Drawing.Point(129, 525);
            this.bttnBuscar.Name = "bttnBuscar";
            this.bttnBuscar.Size = new System.Drawing.Size(100, 38);
            this.bttnBuscar.TabIndex = 16;
            this.bttnBuscar.Text = "Buscar";
            this.bttnBuscar.UseVisualStyleBackColor = false;
            this.bttnBuscar.Click += new System.EventHandler(this.bttnBuscar_Click);
            // 
            // bttnModificar
            // 
            this.bttnModificar.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.bttnModificar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bttnModificar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.bttnModificar.Location = new System.Drawing.Point(320, 525);
            this.bttnModificar.Name = "bttnModificar";
            this.bttnModificar.Size = new System.Drawing.Size(110, 38);
            this.bttnModificar.TabIndex = 17;
            this.bttnModificar.Text = "Modificar";
            this.bttnModificar.UseVisualStyleBackColor = false;
            this.bttnModificar.Click += new System.EventHandler(this.bttnModificar_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dateTimePicker1.Location = new System.Drawing.Point(692, 76);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 18;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F);
            this.label5.Location = new System.Drawing.Point(207, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(462, 39);
            this.label5.TabIndex = 19;
            this.label5.Text = "ABM de Pedido de cotizacion";
            // 
            // dgvTemporal
            // 
            this.dgvTemporal.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dgvTemporal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTemporal.Location = new System.Drawing.Point(127, 196);
            this.dgvTemporal.Name = "dgvTemporal";
            this.dgvTemporal.Size = new System.Drawing.Size(702, 116);
            this.dgvTemporal.TabIndex = 20;
            this.dgvTemporal.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTemporal_CellContentClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label3.Location = new System.Drawing.Point(124, 168);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(296, 25);
            this.label3.TabIndex = 21;
            this.label3.Text = "Pedidos de reaprovisionamiento:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label6.Location = new System.Drawing.Point(362, 76);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 25);
            this.label6.TabIndex = 22;
            this.label6.Text = "XXXX";
            // 
            // cbCotizado
            // 
            this.cbCotizado.AutoSize = true;
            this.cbCotizado.BackColor = System.Drawing.Color.Aqua;
            this.cbCotizado.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbCotizado.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.cbCotizado.Location = new System.Drawing.Point(671, 483);
            this.cbCotizado.Name = "cbCotizado";
            this.cbCotizado.Size = new System.Drawing.Size(109, 29);
            this.cbCotizado.TabIndex = 23;
            this.cbCotizado.Text = "Cotizado";
            this.cbCotizado.UseVisualStyleBackColor = false;
            this.cbCotizado.CheckedChanged += new System.EventHandler(this.cbCotizado_CheckedChanged);
            // 
            // bttnCSesion
            // 
            this.bttnCSesion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bttnCSesion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.bttnCSesion.Location = new System.Drawing.Point(779, 19);
            this.bttnCSesion.Name = "bttnCSesion";
            this.bttnCSesion.Size = new System.Drawing.Size(94, 39);
            this.bttnCSesion.TabIndex = 24;
            this.bttnCSesion.Text = "Cerrar Sesion";
            this.bttnCSesion.UseVisualStyleBackColor = true;
            this.bttnCSesion.Click += new System.EventHandler(this.bttnCSesion_Click);
            // 
            // bttnCargar
            // 
            this.bttnCargar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.bttnCargar.Location = new System.Drawing.Point(340, 113);
            this.bttnCargar.Name = "bttnCargar";
            this.bttnCargar.Size = new System.Drawing.Size(127, 33);
            this.bttnCargar.TabIndex = 25;
            this.bttnCargar.Text = "Cargar";
            this.bttnCargar.UseVisualStyleBackColor = true;
            this.bttnCargar.Click += new System.EventHandler(this.bttnCargar_Click);
            // 
            // Ped_cotizacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Aqua;
            this.ClientSize = new System.Drawing.Size(950, 574);
            this.Controls.Add(this.bttnCargar);
            this.Controls.Add(this.bttnCSesion);
            this.Controls.Add(this.cbCotizado);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dgvTemporal);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.bttnModificar);
            this.Controls.Add(this.bttnBuscar);
            this.Controls.Add(this.bttnGuardar);
            this.Controls.Add(this.bttnEliminar);
            this.Controls.Add(this.cbDeenegado);
            this.Controls.Add(this.cbAprobado);
            this.Controls.Add(this.cbSolicitado);
            this.Controls.Add(this.dgvPCot);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbRubros);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Name = "Ped_cotizacion";
            this.Text = "Ped_cotizacion";
            this.Load += new System.EventHandler(this.Ped_cotizacion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPCot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTemporal)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbRubros;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgvPCot;
        private System.Windows.Forms.CheckBox cbSolicitado;
        private System.Windows.Forms.CheckBox cbAprobado;
        private System.Windows.Forms.CheckBox cbDeenegado;
        private System.Windows.Forms.Button bttnEliminar;
        private System.Windows.Forms.Button bttnGuardar;
        private System.Windows.Forms.Button bttnBuscar;
        private System.Windows.Forms.Button bttnModificar;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dgvTemporal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox cbCotizado;
        private System.Windows.Forms.Button bttnCSesion;
        private System.Windows.Forms.Button bttnCargar;
    }
}