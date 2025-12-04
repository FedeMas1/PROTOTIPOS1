namespace PROTOTIPOS1
{
    partial class FrmBitacora
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
            this.dgvBitacora = new System.Windows.Forms.DataGridView();
            this.bttnActualizar = new System.Windows.Forms.Button();
            this.bttnLimpiar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.bttnFUsuario = new System.Windows.Forms.Button();
            this.txtbUsuario = new System.Windows.Forms.TextBox();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.bttnQuitar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dtp = new System.Windows.Forms.DateTimePicker();
            this.bttnFFecha = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBitacora)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvBitacora
            // 
            this.dgvBitacora.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBitacora.Location = new System.Drawing.Point(56, 173);
            this.dgvBitacora.Name = "dgvBitacora";
            this.dgvBitacora.Size = new System.Drawing.Size(682, 210);
            this.dgvBitacora.TabIndex = 0;
            // 
            // bttnActualizar
            // 
            this.bttnActualizar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.bttnActualizar.Location = new System.Drawing.Point(56, 389);
            this.bttnActualizar.Name = "bttnActualizar";
            this.bttnActualizar.Size = new System.Drawing.Size(107, 49);
            this.bttnActualizar.TabIndex = 1;
            this.bttnActualizar.Text = "Actualizar";
            this.bttnActualizar.UseVisualStyleBackColor = true;
            this.bttnActualizar.Click += new System.EventHandler(this.bttnActualizar_Click);
            // 
            // bttnLimpiar
            // 
            this.bttnLimpiar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.bttnLimpiar.Location = new System.Drawing.Point(632, 389);
            this.bttnLimpiar.Name = "bttnLimpiar";
            this.bttnLimpiar.Size = new System.Drawing.Size(106, 49);
            this.bttnLimpiar.TabIndex = 3;
            this.bttnLimpiar.Text = "Limpiar";
            this.bttnLimpiar.UseVisualStyleBackColor = true;
            this.bttnLimpiar.Click += new System.EventHandler(this.bttnLimpiar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F);
            this.label1.Location = new System.Drawing.Point(323, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 39);
            this.label1.TabIndex = 4;
            this.label1.Text = "Bitacora";
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.button4.Location = new System.Drawing.Point(13, 13);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(104, 54);
            this.button4.TabIndex = 5;
            this.button4.Text = "Back";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.button5.Location = new System.Drawing.Point(673, 12);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(115, 55);
            this.button5.TabIndex = 6;
            this.button5.Text = "Cerrar sesion";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // bttnFUsuario
            // 
            this.bttnFUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.bttnFUsuario.Location = new System.Drawing.Point(536, 71);
            this.bttnFUsuario.Name = "bttnFUsuario";
            this.bttnFUsuario.Size = new System.Drawing.Size(132, 26);
            this.bttnFUsuario.TabIndex = 7;
            this.bttnFUsuario.Text = "Filtrar por Usuario";
            this.bttnFUsuario.UseVisualStyleBackColor = true;
            this.bttnFUsuario.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtbUsuario
            // 
            this.txtbUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtbUsuario.Location = new System.Drawing.Point(358, 77);
            this.txtbUsuario.Name = "txtbUsuario";
            this.txtbUsuario.Size = new System.Drawing.Size(144, 26);
            this.txtbUsuario.TabIndex = 8;
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblUsuario.Location = new System.Drawing.Point(213, 77);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(124, 20);
            this.lblUsuario.TabIndex = 9;
            this.lblUsuario.Text = "Ingresar usuario";
            // 
            // bttnQuitar
            // 
            this.bttnQuitar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.bttnQuitar.Location = new System.Drawing.Point(536, 145);
            this.bttnQuitar.Name = "bttnQuitar";
            this.bttnQuitar.Size = new System.Drawing.Size(132, 25);
            this.bttnQuitar.TabIndex = 10;
            this.bttnQuitar.Text = "Quitar filtros";
            this.bttnQuitar.UseVisualStyleBackColor = true;
            this.bttnQuitar.Click += new System.EventHandler(this.bttnQuitar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label2.Location = new System.Drawing.Point(213, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 20);
            this.label2.TabIndex = 11;
            this.label2.Text = "Ingresar fecha";
            // 
            // dtp
            // 
            this.dtp.Location = new System.Drawing.Point(331, 115);
            this.dtp.Name = "dtp";
            this.dtp.Size = new System.Drawing.Size(200, 20);
            this.dtp.TabIndex = 12;
            // 
            // bttnFFecha
            // 
            this.bttnFFecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.bttnFFecha.Location = new System.Drawing.Point(536, 112);
            this.bttnFFecha.Name = "bttnFFecha";
            this.bttnFFecha.Size = new System.Drawing.Size(132, 27);
            this.bttnFFecha.TabIndex = 14;
            this.bttnFFecha.Text = "Filtrar por fecha";
            this.bttnFFecha.UseVisualStyleBackColor = true;
            this.bttnFFecha.Click += new System.EventHandler(this.bttnFFecha_Click);
            // 
            // FrmBitacora
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.bttnFFecha);
            this.Controls.Add(this.dtp);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.bttnQuitar);
            this.Controls.Add(this.lblUsuario);
            this.Controls.Add(this.txtbUsuario);
            this.Controls.Add(this.bttnFUsuario);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bttnLimpiar);
            this.Controls.Add(this.bttnActualizar);
            this.Controls.Add(this.dgvBitacora);
            this.Name = "FrmBitacora";
            this.Text = "FrmBitacora";
            this.Load += new System.EventHandler(this.FrmBitacora_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBitacora)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvBitacora;
        private System.Windows.Forms.Button bttnActualizar;
        private System.Windows.Forms.Button bttnLimpiar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button bttnFUsuario;
        private System.Windows.Forms.TextBox txtbUsuario;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Button bttnQuitar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtp;
        private System.Windows.Forms.Button bttnFFecha;
    }
}