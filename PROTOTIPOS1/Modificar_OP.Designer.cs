namespace PROTOTIPOS1
{
    partial class Modificar_OP
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
            this.btnback = new System.Windows.Forms.Button();
            this.btnsesion = new System.Windows.Forms.Button();
            this.btnbuscar = new System.Windows.Forms.Button();
            this.bttnEliminar = new System.Windows.Forms.Button();
            this.bttnGuardar = new System.Windows.Forms.Button();
            this.txtop = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvop = new System.Windows.Forms.DataGridView();
            this.Realizado = new System.Windows.Forms.CheckBox();
            this.Denegado = new System.Windows.Forms.CheckBox();
            this.Aceptado = new System.Windows.Forms.CheckBox();
            this.Procesando = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvop)).BeginInit();
            this.SuspendLayout();
            // 
            // btnback
            // 
            this.btnback.Location = new System.Drawing.Point(13, 13);
            this.btnback.Name = "btnback";
            this.btnback.Size = new System.Drawing.Size(75, 23);
            this.btnback.TabIndex = 0;
            this.btnback.Text = "back";
            this.btnback.UseVisualStyleBackColor = true;
            this.btnback.Click += new System.EventHandler(this.btnback_Click);
            // 
            // btnsesion
            // 
            this.btnsesion.Location = new System.Drawing.Point(676, 13);
            this.btnsesion.Name = "btnsesion";
            this.btnsesion.Size = new System.Drawing.Size(112, 23);
            this.btnsesion.TabIndex = 1;
            this.btnsesion.Text = "Cerrar sesion";
            this.btnsesion.UseVisualStyleBackColor = true;
            this.btnsesion.Click += new System.EventHandler(this.btnsesion_Click);
            // 
            // btnbuscar
            // 
            this.btnbuscar.Location = new System.Drawing.Point(236, 134);
            this.btnbuscar.Name = "btnbuscar";
            this.btnbuscar.Size = new System.Drawing.Size(75, 23);
            this.btnbuscar.TabIndex = 2;
            this.btnbuscar.Text = "buscar";
            this.btnbuscar.UseVisualStyleBackColor = true;
            this.btnbuscar.Click += new System.EventHandler(this.btnbuscar_Click);
            // 
            // bttnEliminar
            // 
            this.bttnEliminar.Location = new System.Drawing.Point(106, 397);
            this.bttnEliminar.Name = "bttnEliminar";
            this.bttnEliminar.Size = new System.Drawing.Size(75, 23);
            this.bttnEliminar.TabIndex = 3;
            this.bttnEliminar.Text = "Eliminar";
            this.bttnEliminar.UseVisualStyleBackColor = true;
            this.bttnEliminar.Click += new System.EventHandler(this.btneliminar_Click);
            // 
            // bttnGuardar
            // 
            this.bttnGuardar.Location = new System.Drawing.Point(629, 397);
            this.bttnGuardar.Name = "bttnGuardar";
            this.bttnGuardar.Size = new System.Drawing.Size(75, 23);
            this.bttnGuardar.TabIndex = 4;
            this.bttnGuardar.Text = "Actualizar";
            this.bttnGuardar.UseVisualStyleBackColor = true;
            this.bttnGuardar.Click += new System.EventHandler(this.btnactualizar_Click);
            // 
            // txtop
            // 
            this.txtop.Location = new System.Drawing.Point(106, 137);
            this.txtop.Name = "txtop";
            this.txtop.Size = new System.Drawing.Size(100, 20);
            this.txtop.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F);
            this.label1.Location = new System.Drawing.Point(190, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(435, 39);
            this.label1.TabIndex = 6;
            this.label1.Text = "Modificacion orden de pago";
            // 
            // dgvop
            // 
            this.dgvop.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvop.Location = new System.Drawing.Point(106, 179);
            this.dgvop.Name = "dgvop";
            this.dgvop.Size = new System.Drawing.Size(598, 150);
            this.dgvop.TabIndex = 7;
            this.dgvop.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // Realizado
            // 
            this.Realizado.AutoSize = true;
            this.Realizado.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.Realizado.Location = new System.Drawing.Point(605, 354);
            this.Realizado.Name = "Realizado";
            this.Realizado.Size = new System.Drawing.Size(99, 24);
            this.Realizado.TabIndex = 23;
            this.Realizado.Text = "Realizado";
            this.Realizado.UseVisualStyleBackColor = true;
            // 
            // Denegado
            // 
            this.Denegado.AutoSize = true;
            this.Denegado.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.Denegado.Location = new System.Drawing.Point(418, 354);
            this.Denegado.Name = "Denegado";
            this.Denegado.Size = new System.Drawing.Size(103, 24);
            this.Denegado.TabIndex = 22;
            this.Denegado.Text = "Denegado";
            this.Denegado.UseVisualStyleBackColor = true;
            // 
            // Aceptado
            // 
            this.Aceptado.AutoSize = true;
            this.Aceptado.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.Aceptado.Location = new System.Drawing.Point(264, 354);
            this.Aceptado.Name = "Aceptado";
            this.Aceptado.Size = new System.Drawing.Size(97, 24);
            this.Aceptado.TabIndex = 21;
            this.Aceptado.Text = "Aceptado";
            this.Aceptado.UseVisualStyleBackColor = true;
            // 
            // Procesando
            // 
            this.Procesando.AutoSize = true;
            this.Procesando.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.Procesando.Location = new System.Drawing.Point(107, 354);
            this.Procesando.Name = "Procesando";
            this.Procesando.Size = new System.Drawing.Size(113, 24);
            this.Procesando.TabIndex = 20;
            this.Procesando.Text = "Procesando";
            this.Procesando.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label2.Location = new System.Drawing.Point(106, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(172, 20);
            this.label2.TabIndex = 24;
            this.label2.Text = "Numero orden de pago";
            // 
            // Modificar_OP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Realizado);
            this.Controls.Add(this.Denegado);
            this.Controls.Add(this.Aceptado);
            this.Controls.Add(this.Procesando);
            this.Controls.Add(this.dgvop);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtop);
            this.Controls.Add(this.bttnGuardar);
            this.Controls.Add(this.bttnEliminar);
            this.Controls.Add(this.btnbuscar);
            this.Controls.Add(this.btnsesion);
            this.Controls.Add(this.btnback);
            this.Name = "Modificar_OP";
            this.Text = "Modificar_OP";
            ((System.ComponentModel.ISupportInitialize)(this.dgvop)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnback;
        private System.Windows.Forms.Button btnsesion;
        private System.Windows.Forms.Button btnbuscar;
        private System.Windows.Forms.Button bttnEliminar;
        private System.Windows.Forms.Button bttnGuardar;
        private System.Windows.Forms.TextBox txtop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvop;
        private System.Windows.Forms.CheckBox Realizado;
        private System.Windows.Forms.CheckBox Denegado;
        private System.Windows.Forms.CheckBox Aceptado;
        private System.Windows.Forms.CheckBox Procesando;
        private System.Windows.Forms.Label label2;
    }
}