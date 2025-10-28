namespace PROTOTIPOS1
{
    partial class Login
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.txtbCorreo = new System.Windows.Forms.TextBox();
            this.txtbContraseña = new System.Windows.Forms.TextBox();
            this.Registrarse = new System.Windows.Forms.LinkLabel();
            this.bttnVer = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.button1.Location = new System.Drawing.Point(247, 260);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(142, 61);
            this.button1.TabIndex = 0;
            this.button1.Text = "Iniciar sesion";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label1.Location = new System.Drawing.Point(242, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(163, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Ingrese su correo";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label2.Location = new System.Drawing.Point(225, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(205, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "Ingrese su contraseña";
            // 
            // linkLabel1
            // 
            this.linkLabel1.ActiveLinkColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.linkLabel1.LinkColor = System.Drawing.Color.Purple;
            this.linkLabel1.Location = new System.Drawing.Point(242, 188);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(189, 25);
            this.linkLabel1.TabIndex = 3;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Olvide mi contaseña";
            this.linkLabel1.VisitedLinkColor = System.Drawing.Color.Indigo;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // txtbCorreo
            // 
            this.txtbCorreo.Location = new System.Drawing.Point(247, 72);
            this.txtbCorreo.Name = "txtbCorreo";
            this.txtbCorreo.Size = new System.Drawing.Size(158, 20);
            this.txtbCorreo.TabIndex = 4;
            // 
            // txtbContraseña
            // 
            this.txtbContraseña.Location = new System.Drawing.Point(247, 144);
            this.txtbContraseña.Name = "txtbContraseña";
            this.txtbContraseña.Size = new System.Drawing.Size(158, 20);
            this.txtbContraseña.TabIndex = 5;
            this.txtbContraseña.UseSystemPasswordChar = true;
            // 
            // Registrarse
            // 
            this.Registrarse.ActiveLinkColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Registrarse.AutoSize = true;
            this.Registrarse.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Registrarse.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.Registrarse.LinkColor = System.Drawing.Color.Purple;
            this.Registrarse.Location = new System.Drawing.Point(242, 222);
            this.Registrarse.Name = "Registrarse";
            this.Registrarse.Size = new System.Drawing.Size(110, 25);
            this.Registrarse.TabIndex = 6;
            this.Registrarse.TabStop = true;
            this.Registrarse.Text = "Registrarse";
            this.Registrarse.VisitedLinkColor = System.Drawing.Color.Indigo;
            this.Registrarse.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Registrarse_LinkClicked);
            // 
            // bttnVer
            // 
            this.bttnVer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.bttnVer.Location = new System.Drawing.Point(436, 138);
            this.bttnVer.Name = "bttnVer";
            this.bttnVer.Size = new System.Drawing.Size(63, 30);
            this.bttnVer.TabIndex = 8;
            this.bttnVer.Text = "Ver";
            this.bttnVer.UseVisualStyleBackColor = true;
            this.bttnVer.Click += new System.EventHandler(this.bttnVer_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::PROTOTIPOS1.Properties.Resources.Gemini_Generated_Image_gga20pgga20pgga2;
            this.pictureBox2.Location = new System.Drawing.Point(21, 128);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(198, 148);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 9;
            this.pictureBox2.TabStop = false;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Aqua;
            this.ClientSize = new System.Drawing.Size(637, 425);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.bttnVer);
            this.Controls.Add(this.Registrarse);
            this.Controls.Add(this.txtbContraseña);
            this.Controls.Add(this.txtbCorreo);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Name = "Login";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.TextBox txtbCorreo;
        private System.Windows.Forms.TextBox txtbContraseña;
        private System.Windows.Forms.LinkLabel Registrarse;
        private System.Windows.Forms.Button bttnVer;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}

