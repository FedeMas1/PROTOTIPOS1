namespace PROTOTIPOS1
{
    partial class PR
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
            this.cmbRubros = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dgvProductos = new System.Windows.Forms.DataGridView();
            this.cbSolicitado = new System.Windows.Forms.CheckBox();
            this.cbAprobado = new System.Windows.Forms.CheckBox();
            this.cbDenegado = new System.Windows.Forms.CheckBox();
            this.bttnEliminar = new System.Windows.Forms.Button();
            this.bttnGuardar = new System.Windows.Forms.Button();
            this.bttnBuscar = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.cbCotizado = new System.Windows.Forms.CheckBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.bttnCargar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblNPedido = new System.Windows.Forms.Label();
            this.bttnCSesion = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbRubros
            // 
            this.cmbRubros.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cmbRubros.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbRubros.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cmbRubros.FormattingEnabled = true;
            this.cmbRubros.Location = new System.Drawing.Point(91, 141);
            this.cmbRubros.Name = "cmbRubros";
            this.cmbRubros.Size = new System.Drawing.Size(121, 21);
            this.cmbRubros.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label2.Location = new System.Drawing.Point(88, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "Rubro:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F);
            this.label3.Location = new System.Drawing.Point(131, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(617, 39);
            this.label3.TabIndex = 3;
            this.label3.Text = "ABM de Pedido de reaprovisionamiento";
            // 
            // dgvProductos
            // 
            this.dgvProductos.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProductos.GridColor = System.Drawing.Color.Black;
            this.dgvProductos.Location = new System.Drawing.Point(91, 181);
            this.dgvProductos.Name = "dgvProductos";
            this.dgvProductos.Size = new System.Drawing.Size(742, 203);
            this.dgvProductos.TabIndex = 4;
            this.dgvProductos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // cbSolicitado
            // 
            this.cbSolicitado.AutoSize = true;
            this.cbSolicitado.BackColor = System.Drawing.Color.Aqua;
            this.cbSolicitado.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbSolicitado.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.cbSolicitado.Location = new System.Drawing.Point(91, 406);
            this.cbSolicitado.Name = "cbSolicitado";
            this.cbSolicitado.Size = new System.Drawing.Size(116, 29);
            this.cbSolicitado.TabIndex = 5;
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
            this.cbAprobado.Location = new System.Drawing.Point(283, 406);
            this.cbAprobado.Name = "cbAprobado";
            this.cbAprobado.Size = new System.Drawing.Size(117, 29);
            this.cbAprobado.TabIndex = 6;
            this.cbAprobado.Text = "Aprobado";
            this.cbAprobado.UseVisualStyleBackColor = false;
            this.cbAprobado.CheckedChanged += new System.EventHandler(this.cbAprobado_CheckedChanged);
            // 
            // cbDenegado
            // 
            this.cbDenegado.AutoSize = true;
            this.cbDenegado.BackColor = System.Drawing.Color.Aqua;
            this.cbDenegado.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbDenegado.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.cbDenegado.Location = new System.Drawing.Point(473, 406);
            this.cbDenegado.Name = "cbDenegado";
            this.cbDenegado.Size = new System.Drawing.Size(122, 29);
            this.cbDenegado.TabIndex = 7;
            this.cbDenegado.Text = "Denegado";
            this.cbDenegado.UseVisualStyleBackColor = false;
            this.cbDenegado.CheckedChanged += new System.EventHandler(this.cbDenegado_CheckedChanged);
            // 
            // bttnEliminar
            // 
            this.bttnEliminar.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.bttnEliminar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bttnEliminar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.bttnEliminar.Location = new System.Drawing.Point(402, 441);
            this.bttnEliminar.Name = "bttnEliminar";
            this.bttnEliminar.Size = new System.Drawing.Size(116, 40);
            this.bttnEliminar.TabIndex = 8;
            this.bttnEliminar.Text = "Eliminar";
            this.bttnEliminar.UseVisualStyleBackColor = false;
            this.bttnEliminar.Click += new System.EventHandler(this.bttnEliminar_Click);
            // 
            // bttnGuardar
            // 
            this.bttnGuardar.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.bttnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bttnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.bttnGuardar.Location = new System.Drawing.Point(717, 441);
            this.bttnGuardar.Name = "bttnGuardar";
            this.bttnGuardar.Size = new System.Drawing.Size(116, 40);
            this.bttnGuardar.TabIndex = 9;
            this.bttnGuardar.Text = "Guardar";
            this.bttnGuardar.UseVisualStyleBackColor = false;
            this.bttnGuardar.Click += new System.EventHandler(this.bttnGuardar_Click);
            // 
            // bttnBuscar
            // 
            this.bttnBuscar.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.bttnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bttnBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.bttnBuscar.Location = new System.Drawing.Point(91, 441);
            this.bttnBuscar.Name = "bttnBuscar";
            this.bttnBuscar.Size = new System.Drawing.Size(116, 40);
            this.bttnBuscar.TabIndex = 10;
            this.bttnBuscar.Text = "Buscar";
            this.bttnBuscar.UseVisualStyleBackColor = false;
            this.bttnBuscar.Click += new System.EventHandler(this.bttnBuscar_Click);
            // 
            // button5
            // 
            this.button5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button5.Location = new System.Drawing.Point(2, 12);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(112, 51);
            this.button5.TabIndex = 12;
            this.button5.Text = "Pagina principal";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // cbCotizado
            // 
            this.cbCotizado.AutoSize = true;
            this.cbCotizado.BackColor = System.Drawing.Color.Aqua;
            this.cbCotizado.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbCotizado.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.cbCotizado.Location = new System.Drawing.Point(681, 406);
            this.cbCotizado.Name = "cbCotizado";
            this.cbCotizado.Size = new System.Drawing.Size(109, 29);
            this.cbCotizado.TabIndex = 13;
            this.cbCotizado.Text = "Cotizado";
            this.cbCotizado.UseVisualStyleBackColor = false;
            this.cbCotizado.CheckedChanged += new System.EventHandler(this.cbCotizado_CheckedChanged);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dateTimePicker1.Location = new System.Drawing.Point(726, 79);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 14;
            // 
            // bttnCargar
            // 
            this.bttnCargar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.bttnCargar.Location = new System.Drawing.Point(243, 132);
            this.bttnCargar.Name = "bttnCargar";
            this.bttnCargar.Size = new System.Drawing.Size(108, 34);
            this.bttnCargar.TabIndex = 17;
            this.bttnCargar.Text = "Cargar";
            this.bttnCargar.UseVisualStyleBackColor = true;
            this.bttnCargar.Click += new System.EventHandler(this.bttnCargar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label1.Location = new System.Drawing.Point(91, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(174, 25);
            this.label1.TabIndex = 18;
            this.label1.Text = "Numero de Pedido";
            // 
            // lblNPedido
            // 
            this.lblNPedido.AutoSize = true;
            this.lblNPedido.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.lblNPedido.Location = new System.Drawing.Point(271, 79);
            this.lblNPedido.Name = "lblNPedido";
            this.lblNPedido.Size = new System.Drawing.Size(68, 25);
            this.lblNPedido.TabIndex = 19;
            this.lblNPedido.Text = "XXXX";
            // 
            // bttnCSesion
            // 
            this.bttnCSesion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bttnCSesion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.bttnCSesion.Location = new System.Drawing.Point(811, 17);
            this.bttnCSesion.Name = "bttnCSesion";
            this.bttnCSesion.Size = new System.Drawing.Size(94, 39);
            this.bttnCSesion.TabIndex = 20;
            this.bttnCSesion.Text = "Cerrar Sesion";
            this.bttnCSesion.UseVisualStyleBackColor = true;
            this.bttnCSesion.Click += new System.EventHandler(this.bttnCSesion_Click);
            // 
            // PR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Aqua;
            this.ClientSize = new System.Drawing.Size(981, 499);
            this.Controls.Add(this.bttnCSesion);
            this.Controls.Add(this.lblNPedido);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bttnCargar);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.cbCotizado);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.bttnBuscar);
            this.Controls.Add(this.bttnGuardar);
            this.Controls.Add(this.bttnEliminar);
            this.Controls.Add(this.cbDenegado);
            this.Controls.Add(this.cbAprobado);
            this.Controls.Add(this.cbSolicitado);
            this.Controls.Add(this.dgvProductos);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbRubros);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Name = "PR";
            this.Text = "Pedido de reaprovisionamiento";
            this.Load += new System.EventHandler(this.PR_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbRubros;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvProductos;
        private System.Windows.Forms.CheckBox cbSolicitado;
        private System.Windows.Forms.CheckBox cbAprobado;
        private System.Windows.Forms.CheckBox cbDenegado;
        private System.Windows.Forms.Button bttnEliminar;
        private System.Windows.Forms.Button bttnGuardar;
        private System.Windows.Forms.Button bttnBuscar;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.CheckBox cbCotizado;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button bttnCargar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblNPedido;
        private System.Windows.Forms.Button bttnCSesion;
    }
}