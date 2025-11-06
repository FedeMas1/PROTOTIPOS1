namespace PROTOTIPOS1
{
    partial class Pago
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
            this.dgvOrdenes = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpfecha = new System.Windows.Forms.DateTimePicker();
            this.lblnpago = new System.Windows.Forms.Label();
            this.lbltotal = new System.Windows.Forms.Label();
            this.bttnEliminar = new System.Windows.Forms.Button();
            this.bttnGuardar = new System.Windows.Forms.Button();
            this.bttnCSesion = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.comboBoxBancos = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdenes)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvOrdenes
            // 
            this.dgvOrdenes.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvOrdenes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrdenes.Location = new System.Drawing.Point(82, 141);
            this.dgvOrdenes.Name = "dgvOrdenes";
            this.dgvOrdenes.Size = new System.Drawing.Size(661, 150);
            this.dgvOrdenes.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.label1.Location = new System.Drawing.Point(375, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 31);
            this.label1.TabIndex = 1;
            this.label1.Text = "Pago";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label2.Location = new System.Drawing.Point(77, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(157, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "Numero de pago";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label3.Location = new System.Drawing.Point(77, 325);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(161, 25);
            this.label3.TabIndex = 3;
            this.label3.Text = "Codigo de banco";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label4.Location = new System.Drawing.Point(77, 369);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(118, 25);
            this.label4.TabIndex = 4;
            this.label4.Text = "Importe total";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label5.Location = new System.Drawing.Point(77, 95);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(170, 25);
            this.label5.TabIndex = 5;
            this.label5.Text = "Ordenes de pago:";
            // 
            // dtpfecha
            // 
            this.dtpfecha.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtpfecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.dtpfecha.Location = new System.Drawing.Point(507, 62);
            this.dtpfecha.Name = "dtpfecha";
            this.dtpfecha.Size = new System.Drawing.Size(281, 26);
            this.dtpfecha.TabIndex = 6;
            // 
            // lblnpago
            // 
            this.lblnpago.AutoSize = true;
            this.lblnpago.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.lblnpago.Location = new System.Drawing.Point(240, 43);
            this.lblnpago.Name = "lblnpago";
            this.lblnpago.Size = new System.Drawing.Size(68, 25);
            this.lblnpago.TabIndex = 7;
            this.lblnpago.Text = "XXXX";
            // 
            // lbltotal
            // 
            this.lbltotal.AutoSize = true;
            this.lbltotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.lbltotal.Location = new System.Drawing.Point(253, 369);
            this.lbltotal.Name = "lbltotal";
            this.lbltotal.Size = new System.Drawing.Size(68, 25);
            this.lbltotal.TabIndex = 8;
            this.lbltotal.Text = "XXXX";
            // 
            // bttnEliminar
            // 
            this.bttnEliminar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bttnEliminar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.bttnEliminar.Location = new System.Drawing.Point(77, 440);
            this.bttnEliminar.Name = "bttnEliminar";
            this.bttnEliminar.Size = new System.Drawing.Size(118, 36);
            this.bttnEliminar.TabIndex = 10;
            this.bttnEliminar.Text = "Eliminar";
            this.bttnEliminar.UseVisualStyleBackColor = true;
            this.bttnEliminar.Click += new System.EventHandler(this.bttnEliminar_Click);
            // 
            // bttnGuardar
            // 
            this.bttnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bttnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.bttnGuardar.Location = new System.Drawing.Point(625, 440);
            this.bttnGuardar.Name = "bttnGuardar";
            this.bttnGuardar.Size = new System.Drawing.Size(118, 36);
            this.bttnGuardar.TabIndex = 12;
            this.bttnGuardar.Text = "Pagar";
            this.bttnGuardar.UseVisualStyleBackColor = true;
            this.bttnGuardar.Click += new System.EventHandler(this.bttnGuardar_Click);
            // 
            // bttnCSesion
            // 
            this.bttnCSesion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bttnCSesion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.bttnCSesion.Location = new System.Drawing.Point(2, 1);
            this.bttnCSesion.Name = "bttnCSesion";
            this.bttnCSesion.Size = new System.Drawing.Size(94, 39);
            this.bttnCSesion.TabIndex = 13;
            this.bttnCSesion.Text = "Back";
            this.bttnCSesion.UseVisualStyleBackColor = true;
            this.bttnCSesion.Click += new System.EventHandler(this.bttnCSesion_Click);
            // 
            // button4
            // 
            this.button4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.button4.Location = new System.Drawing.Point(625, 297);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(118, 36);
            this.button4.TabIndex = 14;
            this.button4.Text = "Seleccionar";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label8.Location = new System.Drawing.Point(437, 325);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(68, 25);
            this.label8.TabIndex = 15;
            this.label8.Text = "XXXX";
            // 
            // button5
            // 
            this.button5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.button5.Location = new System.Drawing.Point(678, 12);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(94, 39);
            this.button5.TabIndex = 16;
            this.button5.Text = "Cerrar Sesion";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // comboBoxBancos
            // 
            this.comboBoxBancos.FormattingEnabled = true;
            this.comboBoxBancos.Location = new System.Drawing.Point(245, 328);
            this.comboBoxBancos.Name = "comboBoxBancos";
            this.comboBoxBancos.Size = new System.Drawing.Size(121, 21);
            this.comboBoxBancos.TabIndex = 17;
            // 
            // Pago
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Aqua;
            this.ClientSize = new System.Drawing.Size(800, 488);
            this.Controls.Add(this.comboBoxBancos);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.bttnCSesion);
            this.Controls.Add(this.bttnGuardar);
            this.Controls.Add(this.bttnEliminar);
            this.Controls.Add(this.lbltotal);
            this.Controls.Add(this.lblnpago);
            this.Controls.Add(this.dtpfecha);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvOrdenes);
            this.Name = "Pago";
            this.Text = "Pago";
            this.Load += new System.EventHandler(this.Pago_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdenes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvOrdenes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpfecha;
        private System.Windows.Forms.Label lblnpago;
        private System.Windows.Forms.Label lbltotal;
        private System.Windows.Forms.Button bttnEliminar;
        private System.Windows.Forms.Button bttnGuardar;
        private System.Windows.Forms.Button bttnCSesion;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.ComboBox comboBoxBancos;
    }
}