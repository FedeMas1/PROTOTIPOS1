namespace PROTOTIPOS1
{
    partial class Informe_recepcion
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
            this.label2 = new System.Windows.Forms.Label();
            this.txtBReceptor = new System.Windows.Forms.TextBox();
            this.dgvIRM = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.rtxtbObservaciones = new System.Windows.Forms.RichTextBox();
            this.bttnEliminar = new System.Windows.Forms.Button();
            this.bttnGuardar = new System.Windows.Forms.Button();
            this.bttnModificar = new System.Windows.Forms.Button();
            this.dtp1 = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtBOC = new System.Windows.Forms.TextBox();
            this.lblCInforme = new System.Windows.Forms.Label();
            this.bttnBuscar = new System.Windows.Forms.Button();
            this.bttnCSesion = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtbNRemito = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIRM)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(98, 47);
            this.button1.TabIndex = 0;
            this.button1.Text = "Pagina principal";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label1.Location = new System.Drawing.Point(12, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(171, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Codigo de informe";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label2.Location = new System.Drawing.Point(12, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "Receptor";
            // 
            // txtBReceptor
            // 
            this.txtBReceptor.Location = new System.Drawing.Point(108, 122);
            this.txtBReceptor.Name = "txtBReceptor";
            this.txtBReceptor.Size = new System.Drawing.Size(100, 20);
            this.txtBReceptor.TabIndex = 4;
            // 
            // dgvIRM
            // 
            this.dgvIRM.BackgroundColor = System.Drawing.SystemColors.InactiveBorder;
            this.dgvIRM.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvIRM.Location = new System.Drawing.Point(12, 192);
            this.dgvIRM.Name = "dgvIRM";
            this.dgvIRM.Size = new System.Drawing.Size(930, 150);
            this.dgvIRM.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label3.Location = new System.Drawing.Point(12, 345);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(150, 25);
            this.label3.TabIndex = 7;
            this.label3.Text = "Observaciones:";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // rtxtbObservaciones
            // 
            this.rtxtbObservaciones.Location = new System.Drawing.Point(15, 373);
            this.rtxtbObservaciones.Name = "rtxtbObservaciones";
            this.rtxtbObservaciones.Size = new System.Drawing.Size(926, 59);
            this.rtxtbObservaciones.TabIndex = 8;
            this.rtxtbObservaciones.Text = "";
            // 
            // bttnEliminar
            // 
            this.bttnEliminar.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.bttnEliminar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bttnEliminar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.bttnEliminar.Location = new System.Drawing.Point(401, 449);
            this.bttnEliminar.Name = "bttnEliminar";
            this.bttnEliminar.Size = new System.Drawing.Size(111, 40);
            this.bttnEliminar.TabIndex = 9;
            this.bttnEliminar.Text = "Eliminar";
            this.bttnEliminar.UseVisualStyleBackColor = false;
            this.bttnEliminar.Click += new System.EventHandler(this.bttnEliminar_Click);
            // 
            // bttnGuardar
            // 
            this.bttnGuardar.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.bttnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bttnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.bttnGuardar.Location = new System.Drawing.Point(743, 449);
            this.bttnGuardar.Name = "bttnGuardar";
            this.bttnGuardar.Size = new System.Drawing.Size(111, 40);
            this.bttnGuardar.TabIndex = 11;
            this.bttnGuardar.Text = "Guardar";
            this.bttnGuardar.UseVisualStyleBackColor = false;
            this.bttnGuardar.Click += new System.EventHandler(this.bttnGuardar_Click);
            // 
            // bttnModificar
            // 
            this.bttnModificar.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.bttnModificar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bttnModificar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.bttnModificar.Location = new System.Drawing.Point(109, 449);
            this.bttnModificar.Name = "bttnModificar";
            this.bttnModificar.Size = new System.Drawing.Size(111, 40);
            this.bttnModificar.TabIndex = 12;
            this.bttnModificar.Text = "Modificar";
            this.bttnModificar.UseVisualStyleBackColor = false;
            this.bttnModificar.Click += new System.EventHandler(this.bttnModificar_Click);
            // 
            // dtp1
            // 
            this.dtp1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtp1.Location = new System.Drawing.Point(741, 69);
            this.dtp1.Name = "dtp1";
            this.dtp1.Size = new System.Drawing.Size(200, 20);
            this.dtp1.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F);
            this.label4.Location = new System.Drawing.Point(235, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(475, 39);
            this.label4.TabIndex = 15;
            this.label4.Text = "ABM de Informe de recepcion ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label5.Location = new System.Drawing.Point(12, 160);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(254, 25);
            this.label5.TabIndex = 16;
            this.label5.Text = "Codigo de orden de compra";
            // 
            // txtBOC
            // 
            this.txtBOC.Location = new System.Drawing.Point(272, 166);
            this.txtBOC.Name = "txtBOC";
            this.txtBOC.Size = new System.Drawing.Size(100, 20);
            this.txtBOC.TabIndex = 17;
            // 
            // lblCInforme
            // 
            this.lblCInforme.AutoSize = true;
            this.lblCInforme.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.lblCInforme.Location = new System.Drawing.Point(189, 69);
            this.lblCInforme.Name = "lblCInforme";
            this.lblCInforme.Size = new System.Drawing.Size(68, 25);
            this.lblCInforme.TabIndex = 18;
            this.lblCInforme.Text = "XXXX";
            // 
            // bttnBuscar
            // 
            this.bttnBuscar.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.bttnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bttnBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.bttnBuscar.Location = new System.Drawing.Point(390, 160);
            this.bttnBuscar.Name = "bttnBuscar";
            this.bttnBuscar.Size = new System.Drawing.Size(93, 30);
            this.bttnBuscar.TabIndex = 19;
            this.bttnBuscar.Text = "Buscar";
            this.bttnBuscar.UseVisualStyleBackColor = false;
            this.bttnBuscar.Click += new System.EventHandler(this.bttnBuscar_Click);
            // 
            // bttnCSesion
            // 
            this.bttnCSesion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bttnCSesion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.bttnCSesion.Location = new System.Drawing.Point(801, 15);
            this.bttnCSesion.Name = "bttnCSesion";
            this.bttnCSesion.Size = new System.Drawing.Size(94, 39);
            this.bttnCSesion.TabIndex = 20;
            this.bttnCSesion.Text = "Cerrar Sesion";
            this.bttnCSesion.UseVisualStyleBackColor = true;
            this.bttnCSesion.Click += new System.EventHandler(this.bttnCSesion_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label6.Location = new System.Drawing.Point(658, 64);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 25);
            this.label6.TabIndex = 21;
            this.label6.Text = "Fecha";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label7.Location = new System.Drawing.Point(658, 117);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(108, 25);
            this.label7.TabIndex = 22;
            this.label7.Text = "Nro Remito";
            // 
            // txtbNRemito
            // 
            this.txtbNRemito.Location = new System.Drawing.Point(783, 122);
            this.txtbNRemito.Name = "txtbNRemito";
            this.txtbNRemito.Size = new System.Drawing.Size(100, 20);
            this.txtbNRemito.TabIndex = 23;
            // 
            // Informe_recepcion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Aqua;
            this.ClientSize = new System.Drawing.Size(954, 501);
            this.Controls.Add(this.txtbNRemito);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.bttnCSesion);
            this.Controls.Add(this.bttnBuscar);
            this.Controls.Add(this.lblCInforme);
            this.Controls.Add(this.txtBOC);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtp1);
            this.Controls.Add(this.bttnModificar);
            this.Controls.Add(this.bttnGuardar);
            this.Controls.Add(this.bttnEliminar);
            this.Controls.Add(this.rtxtbObservaciones);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dgvIRM);
            this.Controls.Add(this.txtBReceptor);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Name = "Informe_recepcion";
            this.Text = "Informe_recepcion";
            this.Load += new System.EventHandler(this.Informe_recepcion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvIRM)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBReceptor;
        private System.Windows.Forms.DataGridView dgvIRM;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox rtxtbObservaciones;
        private System.Windows.Forms.Button bttnEliminar;
        private System.Windows.Forms.Button bttnGuardar;
        private System.Windows.Forms.Button bttnModificar;
        private System.Windows.Forms.DateTimePicker dtp1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtBOC;
        private System.Windows.Forms.Label lblCInforme;
        private System.Windows.Forms.Button bttnBuscar;
        private System.Windows.Forms.Button bttnCSesion;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtbNRemito;
    }
}