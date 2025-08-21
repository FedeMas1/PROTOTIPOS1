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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.cbSolicitado = new System.Windows.Forms.CheckBox();
            this.cbAprobado = new System.Windows.Forms.CheckBox();
            this.cbDenegado = new System.Windows.Forms.CheckBox();
            this.bttnEliminar = new System.Windows.Forms.Button();
            this.bttnGuardar = new System.Windows.Forms.Button();
            this.bttnBuscar = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.cbCotizado = new System.Windows.Forms.CheckBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbRubros
            // 
            this.cmbRubros.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cmbRubros.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbRubros.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cmbRubros.FormattingEnabled = true;
            this.cmbRubros.Location = new System.Drawing.Point(94, 114);
            this.cmbRubros.Name = "cmbRubros";
            this.cmbRubros.Size = new System.Drawing.Size(121, 21);
            this.cmbRubros.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label2.Location = new System.Drawing.Point(91, 86);
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
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.GridColor = System.Drawing.Color.Black;
            this.dataGridView1.Location = new System.Drawing.Point(94, 154);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(742, 203);
            this.dataGridView1.TabIndex = 4;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // cbSolicitado
            // 
            this.cbSolicitado.AutoSize = true;
            this.cbSolicitado.BackColor = System.Drawing.Color.Aqua;
            this.cbSolicitado.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbSolicitado.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.cbSolicitado.Location = new System.Drawing.Point(94, 379);
            this.cbSolicitado.Name = "cbSolicitado";
            this.cbSolicitado.Size = new System.Drawing.Size(116, 29);
            this.cbSolicitado.TabIndex = 5;
            this.cbSolicitado.Text = "Solicitado";
            this.cbSolicitado.UseVisualStyleBackColor = false;
            // 
            // cbAprobado
            // 
            this.cbAprobado.AutoSize = true;
            this.cbAprobado.BackColor = System.Drawing.Color.Aqua;
            this.cbAprobado.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbAprobado.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.cbAprobado.Location = new System.Drawing.Point(286, 379);
            this.cbAprobado.Name = "cbAprobado";
            this.cbAprobado.Size = new System.Drawing.Size(117, 29);
            this.cbAprobado.TabIndex = 6;
            this.cbAprobado.Text = "Aprobado";
            this.cbAprobado.UseVisualStyleBackColor = false;
            // 
            // cbDenegado
            // 
            this.cbDenegado.AutoSize = true;
            this.cbDenegado.BackColor = System.Drawing.Color.Aqua;
            this.cbDenegado.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbDenegado.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.cbDenegado.Location = new System.Drawing.Point(476, 379);
            this.cbDenegado.Name = "cbDenegado";
            this.cbDenegado.Size = new System.Drawing.Size(122, 29);
            this.cbDenegado.TabIndex = 7;
            this.cbDenegado.Text = "Denegado";
            this.cbDenegado.UseVisualStyleBackColor = false;
            // 
            // bttnEliminar
            // 
            this.bttnEliminar.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.bttnEliminar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bttnEliminar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.bttnEliminar.Location = new System.Drawing.Point(405, 414);
            this.bttnEliminar.Name = "bttnEliminar";
            this.bttnEliminar.Size = new System.Drawing.Size(116, 40);
            this.bttnEliminar.TabIndex = 8;
            this.bttnEliminar.Text = "Eliminar";
            this.bttnEliminar.UseVisualStyleBackColor = false;
            // 
            // bttnGuardar
            // 
            this.bttnGuardar.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.bttnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bttnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.bttnGuardar.Location = new System.Drawing.Point(720, 414);
            this.bttnGuardar.Name = "bttnGuardar";
            this.bttnGuardar.Size = new System.Drawing.Size(116, 40);
            this.bttnGuardar.TabIndex = 9;
            this.bttnGuardar.Text = "Guardar";
            this.bttnGuardar.UseVisualStyleBackColor = false;
            // 
            // bttnBuscar
            // 
            this.bttnBuscar.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.bttnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bttnBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.bttnBuscar.Location = new System.Drawing.Point(94, 414);
            this.bttnBuscar.Name = "bttnBuscar";
            this.bttnBuscar.Size = new System.Drawing.Size(116, 40);
            this.bttnBuscar.TabIndex = 10;
            this.bttnBuscar.Text = "Buscar";
            this.bttnBuscar.UseVisualStyleBackColor = false;
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
            this.cbCotizado.Location = new System.Drawing.Point(684, 379);
            this.cbCotizado.Name = "cbCotizado";
            this.cbCotizado.Size = new System.Drawing.Size(109, 29);
            this.cbCotizado.TabIndex = 13;
            this.cbCotizado.Text = "Cotizado";
            this.cbCotizado.UseVisualStyleBackColor = false;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dateTimePicker1.Location = new System.Drawing.Point(769, 43);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 14;
            // 
            // PR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Aqua;
            this.ClientSize = new System.Drawing.Size(981, 472);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.cbCotizado);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.bttnBuscar);
            this.Controls.Add(this.bttnGuardar);
            this.Controls.Add(this.bttnEliminar);
            this.Controls.Add(this.cbDenegado);
            this.Controls.Add(this.cbAprobado);
            this.Controls.Add(this.cbSolicitado);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbRubros);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Name = "PR";
            this.Text = "Pedido de reaprovisionamiento";
            this.Load += new System.EventHandler(this.PR_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbRubros;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.CheckBox cbSolicitado;
        private System.Windows.Forms.CheckBox cbAprobado;
        private System.Windows.Forms.CheckBox cbDenegado;
        private System.Windows.Forms.Button bttnEliminar;
        private System.Windows.Forms.Button bttnGuardar;
        private System.Windows.Forms.Button bttnBuscar;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.CheckBox cbCotizado;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
    }
}