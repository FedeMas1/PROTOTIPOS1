namespace PROTOTIPOS1
{
    partial class Orden_de_Pago
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblnnt = new System.Windows.Forms.Label();
            this.txtfact = new System.Windows.Forms.TextBox();
            this.dtpfecha = new System.Windows.Forms.DateTimePicker();
            this.lblrazon = new System.Windows.Forms.Label();
            this.lblimportefact = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.chknt = new System.Windows.Forms.CheckBox();
            this.Procesando = new System.Windows.Forms.CheckBox();
            this.Aceptado = new System.Windows.Forms.CheckBox();
            this.Denegado = new System.Windows.Forms.CheckBox();
            this.Realizado = new System.Windows.Forms.CheckBox();
            this.bttnModificar = new System.Windows.Forms.Button();
            this.bttnGuardar = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblop = new System.Windows.Forms.Label();
            this.txtnt = new System.Windows.Forms.TextBox();
            this.lbltotal = new System.Windows.Forms.Label();
            this.lblnt = new System.Windows.Forms.Label();
            this.bttnCSesion = new System.Windows.Forms.Button();
            this.txtnnt = new System.Windows.Forms.TextBox();
            this.lblcuit = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label1.Location = new System.Drawing.Point(21, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nro ord. pago";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label2.Location = new System.Drawing.Point(21, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Nro factura";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label3.Location = new System.Drawing.Point(21, 153);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(152, 25);
            this.label3.TabIndex = 2;
            this.label3.Text = "CUIT proveedor";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label4.Location = new System.Drawing.Point(21, 206);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(168, 25);
            this.label4.TabIndex = 3;
            this.label4.Text = "Importe de factura";
            // 
            // lblnnt
            // 
            this.lblnnt.AutoSize = true;
            this.lblnnt.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.lblnnt.Location = new System.Drawing.Point(21, 297);
            this.lblnnt.Name = "lblnnt";
            this.lblnnt.Size = new System.Drawing.Size(176, 25);
            this.lblnnt.TabIndex = 4;
            this.lblnnt.Text = "Nro nota de credito";
            // 
            // txtfact
            // 
            this.txtfact.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtfact.Location = new System.Drawing.Point(231, 103);
            this.txtfact.Name = "txtfact";
            this.txtfact.Size = new System.Drawing.Size(136, 26);
            this.txtfact.TabIndex = 6;
            // 
            // dtpfecha
            // 
            this.dtpfecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.dtpfecha.Location = new System.Drawing.Point(506, 64);
            this.dtpfecha.Name = "dtpfecha";
            this.dtpfecha.Size = new System.Drawing.Size(282, 26);
            this.dtpfecha.TabIndex = 10;
            // 
            // lblrazon
            // 
            this.lblrazon.AutoSize = true;
            this.lblrazon.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.lblrazon.Location = new System.Drawing.Point(437, 155);
            this.lblrazon.Name = "lblrazon";
            this.lblrazon.Size = new System.Drawing.Size(100, 25);
            this.lblrazon.TabIndex = 11;
            this.lblrazon.Text = "________";
            this.lblrazon.Click += new System.EventHandler(this.label6_Click);
            // 
            // lblimportefact
            // 
            this.lblimportefact.AutoSize = true;
            this.lblimportefact.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.lblimportefact.Location = new System.Drawing.Point(226, 206);
            this.lblimportefact.Name = "lblimportefact";
            this.lblimportefact.Size = new System.Drawing.Size(111, 25);
            this.lblimportefact.TabIndex = 12;
            this.lblimportefact.Text = "_________";
            this.lblimportefact.Click += new System.EventHandler(this.label7_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label8.Location = new System.Drawing.Point(21, 258);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(143, 25);
            this.label8.TabIndex = 13;
            this.label8.Text = "Nota de credito";
            // 
            // chknt
            // 
            this.chknt.AutoSize = true;
            this.chknt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.chknt.Location = new System.Drawing.Point(231, 261);
            this.chknt.Name = "chknt";
            this.chknt.Size = new System.Drawing.Size(15, 14);
            this.chknt.TabIndex = 14;
            this.chknt.UseVisualStyleBackColor = true;
            this.chknt.CheckedChanged += new System.EventHandler(this.chknt_CheckedChanged);
            // 
            // Procesando
            // 
            this.Procesando.AutoSize = true;
            this.Procesando.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.Procesando.Location = new System.Drawing.Point(26, 400);
            this.Procesando.Name = "Procesando";
            this.Procesando.Size = new System.Drawing.Size(113, 24);
            this.Procesando.TabIndex = 15;
            this.Procesando.Text = "Procesando";
            this.Procesando.UseVisualStyleBackColor = true;
            // 
            // Aceptado
            // 
            this.Aceptado.AutoSize = true;
            this.Aceptado.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.Aceptado.Location = new System.Drawing.Point(197, 400);
            this.Aceptado.Name = "Aceptado";
            this.Aceptado.Size = new System.Drawing.Size(97, 24);
            this.Aceptado.TabIndex = 17;
            this.Aceptado.Text = "Aceptado";
            this.Aceptado.UseVisualStyleBackColor = true;
            // 
            // Denegado
            // 
            this.Denegado.AutoSize = true;
            this.Denegado.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.Denegado.Location = new System.Drawing.Point(358, 400);
            this.Denegado.Name = "Denegado";
            this.Denegado.Size = new System.Drawing.Size(103, 24);
            this.Denegado.TabIndex = 18;
            this.Denegado.Text = "Denegado";
            this.Denegado.UseVisualStyleBackColor = true;
            // 
            // Realizado
            // 
            this.Realizado.AutoSize = true;
            this.Realizado.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.Realizado.Location = new System.Drawing.Point(540, 400);
            this.Realizado.Name = "Realizado";
            this.Realizado.Size = new System.Drawing.Size(99, 24);
            this.Realizado.TabIndex = 19;
            this.Realizado.Text = "Realizado";
            this.Realizado.UseVisualStyleBackColor = true;
            // 
            // bttnModificar
            // 
            this.bttnModificar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.bttnModificar.Location = new System.Drawing.Point(27, 452);
            this.bttnModificar.Name = "bttnModificar";
            this.bttnModificar.Size = new System.Drawing.Size(170, 36);
            this.bttnModificar.TabIndex = 21;
            this.bttnModificar.Text = "Modificar";
            this.bttnModificar.UseVisualStyleBackColor = true;
            this.bttnModificar.Click += new System.EventHandler(this.bttnModificar_Click);
            // 
            // bttnGuardar
            // 
            this.bttnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.bttnGuardar.Location = new System.Drawing.Point(588, 452);
            this.bttnGuardar.Name = "bttnGuardar";
            this.bttnGuardar.Size = new System.Drawing.Size(170, 36);
            this.bttnGuardar.TabIndex = 22;
            this.bttnGuardar.Text = "Guardar";
            this.bttnGuardar.UseVisualStyleBackColor = true;
            this.bttnGuardar.Click += new System.EventHandler(this.bttnGuardar_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.label9.Location = new System.Drawing.Point(298, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(193, 31);
            this.label9.TabIndex = 23;
            this.label9.Text = "Orden de pago";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label10.Location = new System.Drawing.Point(590, 360);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 25);
            this.label10.TabIndex = 24;
            this.label10.Text = "Total";
            // 
            // lblop
            // 
            this.lblop.AutoSize = true;
            this.lblop.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.lblop.Location = new System.Drawing.Point(226, 65);
            this.lblop.Name = "lblop";
            this.lblop.Size = new System.Drawing.Size(68, 25);
            this.lblop.TabIndex = 25;
            this.lblop.Text = "XXXX";
            // 
            // txtnt
            // 
            this.txtnt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtnt.Location = new System.Drawing.Point(237, 337);
            this.txtnt.Name = "txtnt";
            this.txtnt.Size = new System.Drawing.Size(136, 26);
            this.txtnt.TabIndex = 26;
            this.txtnt.TextChanged += new System.EventHandler(this.txtnt_TextChanged);
            // 
            // lbltotal
            // 
            this.lbltotal.AutoSize = true;
            this.lbltotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.lbltotal.Location = new System.Drawing.Point(658, 360);
            this.lbltotal.Name = "lbltotal";
            this.lbltotal.Size = new System.Drawing.Size(100, 25);
            this.lbltotal.TabIndex = 27;
            this.lbltotal.Text = "________";
            // 
            // lblnt
            // 
            this.lblnt.AutoSize = true;
            this.lblnt.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.lblnt.Location = new System.Drawing.Point(21, 338);
            this.lblnt.Name = "lblnt";
            this.lblnt.Size = new System.Drawing.Size(210, 25);
            this.lblnt.TabIndex = 28;
            this.lblnt.Text = "Importe nota de credito";
            // 
            // bttnCSesion
            // 
            this.bttnCSesion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bttnCSesion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.bttnCSesion.Location = new System.Drawing.Point(2, 4);
            this.bttnCSesion.Name = "bttnCSesion";
            this.bttnCSesion.Size = new System.Drawing.Size(94, 39);
            this.bttnCSesion.TabIndex = 30;
            this.bttnCSesion.Text = "Back";
            this.bttnCSesion.UseVisualStyleBackColor = true;
            this.bttnCSesion.Click += new System.EventHandler(this.bttnCSesion_Click);
            // 
            // txtnnt
            // 
            this.txtnnt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtnnt.Location = new System.Drawing.Point(237, 296);
            this.txtnnt.Name = "txtnnt";
            this.txtnnt.Size = new System.Drawing.Size(136, 26);
            this.txtnnt.TabIndex = 31;
            // 
            // lblcuit
            // 
            this.lblcuit.AutoSize = true;
            this.lblcuit.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.lblcuit.Location = new System.Drawing.Point(232, 164);
            this.lblcuit.Name = "lblcuit";
            this.lblcuit.Size = new System.Drawing.Size(100, 25);
            this.lblcuit.TabIndex = 32;
            this.lblcuit.Text = "________";
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.button4.Location = new System.Drawing.Point(410, 102);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(81, 27);
            this.button4.TabIndex = 33;
            this.button4.Text = "Buscar";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.button5.Location = new System.Drawing.Point(694, 10);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(94, 39);
            this.button5.TabIndex = 34;
            this.button5.Text = "Cerrar Sesion";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // Orden_de_Pago
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Aqua;
            this.ClientSize = new System.Drawing.Size(800, 500);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.lblcuit);
            this.Controls.Add(this.txtnnt);
            this.Controls.Add(this.bttnCSesion);
            this.Controls.Add(this.lblnt);
            this.Controls.Add(this.lbltotal);
            this.Controls.Add(this.txtnt);
            this.Controls.Add(this.lblop);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.bttnGuardar);
            this.Controls.Add(this.bttnModificar);
            this.Controls.Add(this.Realizado);
            this.Controls.Add(this.Denegado);
            this.Controls.Add(this.Aceptado);
            this.Controls.Add(this.Procesando);
            this.Controls.Add(this.chknt);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lblimportefact);
            this.Controls.Add(this.lblrazon);
            this.Controls.Add(this.dtpfecha);
            this.Controls.Add(this.txtfact);
            this.Controls.Add(this.lblnnt);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Orden_de_Pago";
            this.Text = "Orden_de_Pago";
            this.Load += new System.EventHandler(this.Orden_de_Pago_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblnnt;
        private System.Windows.Forms.TextBox txtfact;
        private System.Windows.Forms.DateTimePicker dtpfecha;
        private System.Windows.Forms.Label lblrazon;
        private System.Windows.Forms.Label lblimportefact;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox chknt;
        private System.Windows.Forms.CheckBox Procesando;
        private System.Windows.Forms.CheckBox Aceptado;
        private System.Windows.Forms.CheckBox Denegado;
        private System.Windows.Forms.CheckBox Realizado;
        private System.Windows.Forms.Button bttnModificar;
        private System.Windows.Forms.Button bttnGuardar;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblop;
        private System.Windows.Forms.TextBox txtnt;
        private System.Windows.Forms.Label lbltotal;
        private System.Windows.Forms.Label lblnt;
        private System.Windows.Forms.Button bttnCSesion;
        private System.Windows.Forms.TextBox txtnnt;
        private System.Windows.Forms.Label lblcuit;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
    }
}