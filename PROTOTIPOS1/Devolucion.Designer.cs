namespace PROTOTIPOS1
{
    partial class Devolucion
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
            this.label1 = new System.Windows.Forms.Label();
            this.dgvDevolucion = new System.Windows.Forms.DataGridView();
            this.rtbDescripcion = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.bttnEliminar = new System.Windows.Forms.Button();
            this.bttnModificar = new System.Windows.Forms.Button();
            this.bttnBOC = new System.Windows.Forms.Button();
            this.bttnGuardar = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblCUIT = new System.Windows.Forms.Label();
            this.lblNDev = new System.Windows.Forms.Label();
            this.txtbOC = new System.Windows.Forms.TextBox();
            this.txtbIRM = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.bttnBIRM = new System.Windows.Forms.Button();
            this.bttnCSesion = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDevolucion)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label1.Location = new System.Drawing.Point(116, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "N devolucion";
            // 
            // dgvDevolucion
            // 
            this.dgvDevolucion.BackgroundColor = System.Drawing.Color.White;
            this.dgvDevolucion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDevolucion.Location = new System.Drawing.Point(119, 197);
            this.dgvDevolucion.Name = "dgvDevolucion";
            this.dgvDevolucion.Size = new System.Drawing.Size(690, 150);
            this.dgvDevolucion.TabIndex = 4;
            // 
            // rtbDescripcion
            // 
            this.rtbDescripcion.Location = new System.Drawing.Point(119, 386);
            this.rtbDescripcion.Name = "rtbDescripcion";
            this.rtbDescripcion.Size = new System.Drawing.Size(690, 96);
            this.rtbDescripcion.TabIndex = 6;
            this.rtbDescripcion.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label3.Location = new System.Drawing.Point(116, 360);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 25);
            this.label3.TabIndex = 7;
            this.label3.Text = "Descripcion:";
            // 
            // button1
            // 
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button1.Location = new System.Drawing.Point(12, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(97, 51);
            this.button1.TabIndex = 8;
            this.button1.Text = "Pagina principal";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // bttnEliminar
            // 
            this.bttnEliminar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bttnEliminar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.bttnEliminar.Location = new System.Drawing.Point(400, 505);
            this.bttnEliminar.Name = "bttnEliminar";
            this.bttnEliminar.Size = new System.Drawing.Size(102, 47);
            this.bttnEliminar.TabIndex = 9;
            this.bttnEliminar.Text = "Eliminar";
            this.bttnEliminar.UseVisualStyleBackColor = true;
            this.bttnEliminar.Click += new System.EventHandler(this.bttnEliminar_Click);
            // 
            // bttnModificar
            // 
            this.bttnModificar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bttnModificar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.bttnModificar.Location = new System.Drawing.Point(119, 505);
            this.bttnModificar.Name = "bttnModificar";
            this.bttnModificar.Size = new System.Drawing.Size(102, 47);
            this.bttnModificar.TabIndex = 10;
            this.bttnModificar.Text = "Modificar";
            this.bttnModificar.UseVisualStyleBackColor = true;
            this.bttnModificar.Click += new System.EventHandler(this.bttnModificar_Click);
            // 
            // bttnBOC
            // 
            this.bttnBOC.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bttnBOC.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.bttnBOC.Location = new System.Drawing.Point(409, 121);
            this.bttnBOC.Name = "bttnBOC";
            this.bttnBOC.Size = new System.Drawing.Size(93, 32);
            this.bttnBOC.TabIndex = 12;
            this.bttnBOC.Text = "Buscar";
            this.bttnBOC.UseVisualStyleBackColor = true;
            this.bttnBOC.Click += new System.EventHandler(this.bttnBOC_Click);
            // 
            // bttnGuardar
            // 
            this.bttnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bttnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.bttnGuardar.Location = new System.Drawing.Point(707, 505);
            this.bttnGuardar.Name = "bttnGuardar";
            this.bttnGuardar.Size = new System.Drawing.Size(102, 47);
            this.bttnGuardar.TabIndex = 13;
            this.bttnGuardar.Text = "Guardar";
            this.bttnGuardar.UseVisualStyleBackColor = true;
            this.bttnGuardar.Click += new System.EventHandler(this.bttnGuardar_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F);
            this.label4.Location = new System.Drawing.Point(263, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(353, 39);
            this.label4.TabIndex = 14;
            this.label4.Text = "ABM de Devoluciones";
            // 
            // dtpFecha
            // 
            this.dtpFecha.Location = new System.Drawing.Point(668, 73);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(200, 20);
            this.dtpFecha.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label2.Location = new System.Drawing.Point(116, 128);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(164, 25);
            this.label2.TabIndex = 16;
            this.label2.Text = "Orden de compra";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label5.Location = new System.Drawing.Point(116, 91);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(179, 25);
            this.label5.TabIndex = 17;
            this.label5.Text = "CUIT de proveedor";
            // 
            // lblCUIT
            // 
            this.lblCUIT.AutoSize = true;
            this.lblCUIT.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.lblCUIT.Location = new System.Drawing.Point(301, 91);
            this.lblCUIT.Name = "lblCUIT";
            this.lblCUIT.Size = new System.Drawing.Size(68, 25);
            this.lblCUIT.TabIndex = 18;
            this.lblCUIT.Text = "XXXX";
            // 
            // lblNDev
            // 
            this.lblNDev.AutoSize = true;
            this.lblNDev.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.lblNDev.Location = new System.Drawing.Point(247, 55);
            this.lblNDev.Name = "lblNDev";
            this.lblNDev.Size = new System.Drawing.Size(68, 25);
            this.lblNDev.TabIndex = 19;
            this.lblNDev.Text = "XXXX";
            // 
            // txtbOC
            // 
            this.txtbOC.Location = new System.Drawing.Point(286, 134);
            this.txtbOC.Name = "txtbOC";
            this.txtbOC.Size = new System.Drawing.Size(100, 20);
            this.txtbOC.TabIndex = 20;
            // 
            // txtbIRM
            // 
            this.txtbIRM.Location = new System.Drawing.Point(316, 169);
            this.txtbIRM.Name = "txtbIRM";
            this.txtbIRM.Size = new System.Drawing.Size(100, 20);
            this.txtbIRM.TabIndex = 23;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label8.Location = new System.Drawing.Point(116, 163);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(194, 25);
            this.label8.TabIndex = 22;
            this.label8.Text = "Informe de recepcion";
            // 
            // bttnBIRM
            // 
            this.bttnBIRM.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bttnBIRM.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.bttnBIRM.Location = new System.Drawing.Point(422, 159);
            this.bttnBIRM.Name = "bttnBIRM";
            this.bttnBIRM.Size = new System.Drawing.Size(93, 32);
            this.bttnBIRM.TabIndex = 21;
            this.bttnBIRM.Text = "Buscar";
            this.bttnBIRM.UseVisualStyleBackColor = true;
            this.bttnBIRM.Click += new System.EventHandler(this.bttnBIRM_Click);
            // 
            // bttnCSesion
            // 
            this.bttnCSesion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bttnCSesion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.bttnCSesion.Location = new System.Drawing.Point(748, 12);
            this.bttnCSesion.Name = "bttnCSesion";
            this.bttnCSesion.Size = new System.Drawing.Size(94, 39);
            this.bttnCSesion.TabIndex = 24;
            this.bttnCSesion.Text = "Cerrar Sesion";
            this.bttnCSesion.UseVisualStyleBackColor = true;
            this.bttnCSesion.Click += new System.EventHandler(this.bttnCSesion_Click);
            // 
            // Devolucion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Aqua;
            this.ClientSize = new System.Drawing.Size(880, 552);
            this.Controls.Add(this.bttnCSesion);
            this.Controls.Add(this.txtbIRM);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.bttnBIRM);
            this.Controls.Add(this.txtbOC);
            this.Controls.Add(this.lblNDev);
            this.Controls.Add(this.lblCUIT);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpFecha);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.bttnGuardar);
            this.Controls.Add(this.bttnBOC);
            this.Controls.Add(this.bttnModificar);
            this.Controls.Add(this.bttnEliminar);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.rtbDescripcion);
            this.Controls.Add(this.dgvDevolucion);
            this.Controls.Add(this.label1);
            this.Name = "Devolucion";
            this.Text = "Devolucion";
            this.Load += new System.EventHandler(this.Devolucion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDevolucion)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvDevolucion;
        private System.Windows.Forms.RichTextBox rtbDescripcion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button bttnEliminar;
        private System.Windows.Forms.Button bttnModificar;
        private System.Windows.Forms.Button bttnBOC;
        private System.Windows.Forms.Button bttnGuardar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblCUIT;
        private System.Windows.Forms.Label lblNDev;
        private System.Windows.Forms.TextBox txtbOC;
        private System.Windows.Forms.TextBox txtbIRM;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button bttnBIRM;
        private System.Windows.Forms.Button bttnCSesion;
    }
}