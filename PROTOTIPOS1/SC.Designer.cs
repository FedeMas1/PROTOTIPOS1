namespace PROTOTIPOS1
{
    partial class SC
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
            this.dgvProductos = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.bttnGuardar = new System.Windows.Forms.Button();
            this.cbSolicitado = new System.Windows.Forms.CheckBox();
            this.cbAprobado = new System.Windows.Forms.CheckBox();
            this.cbDenegado = new System.Windows.Forms.CheckBox();
            this.bttnBuscar = new System.Windows.Forms.Button();
            this.bttnEliminar = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.cbCotizado = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbRubros = new System.Windows.Forms.ComboBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.bttnCargar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lblNSolicitud = new System.Windows.Forms.Label();
            this.bttnCSesion = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvProductos
            // 
            this.dgvProductos.BackgroundColor = System.Drawing.SystemColors.InactiveBorder;
            this.dgvProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProductos.Location = new System.Drawing.Point(71, 173);
            this.dgvProductos.Name = "dgvProductos";
            this.dgvProductos.Size = new System.Drawing.Size(678, 150);
            this.dgvProductos.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F);
            this.label1.Location = new System.Drawing.Point(166, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(446, 39);
            this.label1.TabIndex = 1;
            this.label1.Text = "ABM de Solicitud de compra";
            // 
            // bttnGuardar
            // 
            this.bttnGuardar.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.bttnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bttnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.bttnGuardar.Location = new System.Drawing.Point(641, 419);
            this.bttnGuardar.Name = "bttnGuardar";
            this.bttnGuardar.Size = new System.Drawing.Size(108, 43);
            this.bttnGuardar.TabIndex = 2;
            this.bttnGuardar.Text = "Guardar";
            this.bttnGuardar.UseVisualStyleBackColor = false;
            this.bttnGuardar.Click += new System.EventHandler(this.bttnGuardar_Click);
            // 
            // cbSolicitado
            // 
            this.cbSolicitado.AutoSize = true;
            this.cbSolicitado.BackColor = System.Drawing.Color.Aqua;
            this.cbSolicitado.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbSolicitado.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.cbSolicitado.Location = new System.Drawing.Point(71, 337);
            this.cbSolicitado.Name = "cbSolicitado";
            this.cbSolicitado.Size = new System.Drawing.Size(116, 29);
            this.cbSolicitado.TabIndex = 3;
            this.cbSolicitado.Text = "Solicitado";
            this.cbSolicitado.UseVisualStyleBackColor = false;
            // 
            // cbAprobado
            // 
            this.cbAprobado.AutoSize = true;
            this.cbAprobado.BackColor = System.Drawing.Color.Aqua;
            this.cbAprobado.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbAprobado.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.cbAprobado.Location = new System.Drawing.Point(229, 337);
            this.cbAprobado.Name = "cbAprobado";
            this.cbAprobado.Size = new System.Drawing.Size(117, 29);
            this.cbAprobado.TabIndex = 4;
            this.cbAprobado.Text = "Aprobado";
            this.cbAprobado.UseVisualStyleBackColor = false;
            // 
            // cbDenegado
            // 
            this.cbDenegado.AutoSize = true;
            this.cbDenegado.BackColor = System.Drawing.Color.Aqua;
            this.cbDenegado.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbDenegado.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.cbDenegado.Location = new System.Drawing.Point(415, 337);
            this.cbDenegado.Name = "cbDenegado";
            this.cbDenegado.Size = new System.Drawing.Size(122, 29);
            this.cbDenegado.TabIndex = 5;
            this.cbDenegado.Text = "Denegado";
            this.cbDenegado.UseVisualStyleBackColor = false;
            // 
            // bttnBuscar
            // 
            this.bttnBuscar.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.bttnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bttnBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.bttnBuscar.Location = new System.Drawing.Point(73, 419);
            this.bttnBuscar.Name = "bttnBuscar";
            this.bttnBuscar.Size = new System.Drawing.Size(108, 43);
            this.bttnBuscar.TabIndex = 7;
            this.bttnBuscar.Text = "Buscar";
            this.bttnBuscar.UseVisualStyleBackColor = false;
            this.bttnBuscar.Click += new System.EventHandler(this.bttnBuscar_Click);
            // 
            // bttnEliminar
            // 
            this.bttnEliminar.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.bttnEliminar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bttnEliminar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.bttnEliminar.Location = new System.Drawing.Point(354, 419);
            this.bttnEliminar.Name = "bttnEliminar";
            this.bttnEliminar.Size = new System.Drawing.Size(96, 43);
            this.bttnEliminar.TabIndex = 9;
            this.bttnEliminar.Text = "Eliminar";
            this.bttnEliminar.UseVisualStyleBackColor = false;
            this.bttnEliminar.Click += new System.EventHandler(this.bttnEliminar_Click);
            // 
            // button6
            // 
            this.button6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button6.Location = new System.Drawing.Point(0, 2);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(104, 50);
            this.button6.TabIndex = 10;
            this.button6.Text = "Pagina principal";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // cbCotizado
            // 
            this.cbCotizado.AutoSize = true;
            this.cbCotizado.BackColor = System.Drawing.Color.Aqua;
            this.cbCotizado.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbCotizado.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.cbCotizado.Location = new System.Drawing.Point(602, 337);
            this.cbCotizado.Name = "cbCotizado";
            this.cbCotizado.Size = new System.Drawing.Size(109, 29);
            this.cbCotizado.TabIndex = 11;
            this.cbCotizado.Text = "Cotizado";
            this.cbCotizado.UseVisualStyleBackColor = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label3.Location = new System.Drawing.Point(68, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 25);
            this.label3.TabIndex = 13;
            this.label3.Text = "Rubro:";
            // 
            // cmbRubros
            // 
            this.cmbRubros.FormattingEnabled = true;
            this.cmbRubros.Location = new System.Drawing.Point(73, 142);
            this.cmbRubros.Name = "cmbRubros";
            this.cmbRubros.Size = new System.Drawing.Size(121, 21);
            this.cmbRubros.TabIndex = 14;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(611, 66);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 15;
            // 
            // bttnCargar
            // 
            this.bttnCargar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.bttnCargar.Location = new System.Drawing.Point(220, 133);
            this.bttnCargar.Name = "bttnCargar";
            this.bttnCargar.Size = new System.Drawing.Size(108, 34);
            this.bttnCargar.TabIndex = 16;
            this.bttnCargar.Text = "Cargar";
            this.bttnCargar.UseVisualStyleBackColor = true;
            this.bttnCargar.Click += new System.EventHandler(this.bttnCargar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label2.Location = new System.Drawing.Point(71, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(183, 25);
            this.label2.TabIndex = 17;
            this.label2.Text = "Numero de solicitud";
            // 
            // lblNSolicitud
            // 
            this.lblNSolicitud.AutoSize = true;
            this.lblNSolicitud.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.lblNSolicitud.Location = new System.Drawing.Point(298, 80);
            this.lblNSolicitud.Name = "lblNSolicitud";
            this.lblNSolicitud.Size = new System.Drawing.Size(68, 25);
            this.lblNSolicitud.TabIndex = 18;
            this.lblNSolicitud.Text = "XXXX";
            // 
            // bttnCSesion
            // 
            this.bttnCSesion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bttnCSesion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.bttnCSesion.Location = new System.Drawing.Point(702, 15);
            this.bttnCSesion.Name = "bttnCSesion";
            this.bttnCSesion.Size = new System.Drawing.Size(94, 39);
            this.bttnCSesion.TabIndex = 19;
            this.bttnCSesion.Text = "Cerrar Sesion";
            this.bttnCSesion.UseVisualStyleBackColor = true;
            this.bttnCSesion.Click += new System.EventHandler(this.bttnCSesion_Click);
            // 
            // SC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Aqua;
            this.ClientSize = new System.Drawing.Size(823, 479);
            this.Controls.Add(this.bttnCSesion);
            this.Controls.Add(this.lblNSolicitud);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.bttnCargar);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.cmbRubros);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbCotizado);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.bttnEliminar);
            this.Controls.Add(this.bttnBuscar);
            this.Controls.Add(this.cbDenegado);
            this.Controls.Add(this.cbAprobado);
            this.Controls.Add(this.cbSolicitado);
            this.Controls.Add(this.bttnGuardar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvProductos);
            this.Name = "SC";
            this.Text = "SC";
            this.Load += new System.EventHandler(this.SC_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvProductos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bttnGuardar;
        private System.Windows.Forms.CheckBox cbSolicitado;
        private System.Windows.Forms.CheckBox cbAprobado;
        private System.Windows.Forms.CheckBox cbDenegado;
        private System.Windows.Forms.Button bttnBuscar;
        private System.Windows.Forms.Button bttnEliminar;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.CheckBox cbCotizado;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbRubros;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button bttnCargar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblNSolicitud;
        private System.Windows.Forms.Button bttnCSesion;
    }
}