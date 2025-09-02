namespace PROTOTIPOS1
{
    partial class Modulos
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.bttnCSesion = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lblNivel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F);
            this.label1.Location = new System.Drawing.Point(324, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 39);
            this.label1.TabIndex = 0;
            this.label1.Text = "Modulos";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.button1.Location = new System.Drawing.Point(168, 111);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(161, 56);
            this.button1.TabIndex = 1;
            this.button1.Text = "Compras";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.button2.Location = new System.Drawing.Point(168, 249);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(161, 57);
            this.button2.TabIndex = 2;
            this.button2.Text = "Pagos";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.button3.Location = new System.Drawing.Point(484, 111);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(161, 56);
            this.button3.TabIndex = 3;
            this.button3.Text = "Ventas";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.button4.Location = new System.Drawing.Point(484, 250);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(161, 56);
            this.button4.TabIndex = 4;
            this.button4.Text = "Cobros";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // bttnCSesion
            // 
            this.bttnCSesion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bttnCSesion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.bttnCSesion.Location = new System.Drawing.Point(12, 12);
            this.bttnCSesion.Name = "bttnCSesion";
            this.bttnCSesion.Size = new System.Drawing.Size(94, 39);
            this.bttnCSesion.TabIndex = 5;
            this.bttnCSesion.Text = "Cerrar Sesion";
            this.bttnCSesion.UseVisualStyleBackColor = true;
            this.bttnCSesion.Click += new System.EventHandler(this.bttnCSesion_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label2.Location = new System.Drawing.Point(579, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "Nivel de usuario:";
            // 
            // lblNivel
            // 
            this.lblNivel.AutoSize = true;
            this.lblNivel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblNivel.Location = new System.Drawing.Point(716, 27);
            this.lblNivel.Name = "lblNivel";
            this.lblNivel.Size = new System.Drawing.Size(53, 20);
            this.lblNivel.TabIndex = 7;
            this.lblNivel.Text = "XXXX";
            // 
            // Modulos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Aqua;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblNivel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.bttnCSesion);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Name = "Modulos";
            this.Text = "Modulos";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button bttnCSesion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblNivel;
    }
}