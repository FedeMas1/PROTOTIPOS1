namespace PROTOTIPOS1
{
    partial class Backup
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
            this.btnrestore = new System.Windows.Forms.Button();
            this.btnbackup = new System.Windows.Forms.Button();
            this.bttnCSesion = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnrestore
            // 
            this.btnrestore.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnrestore.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.btnrestore.Location = new System.Drawing.Point(429, 134);
            this.btnrestore.Name = "btnrestore";
            this.btnrestore.Size = new System.Drawing.Size(236, 90);
            this.btnrestore.TabIndex = 6;
            this.btnrestore.Text = "Restore";
            this.btnrestore.UseVisualStyleBackColor = true;
            this.btnrestore.Click += new System.EventHandler(this.btnrestore_Click);
            // 
            // btnbackup
            // 
            this.btnbackup.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnbackup.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.btnbackup.Location = new System.Drawing.Point(131, 134);
            this.btnbackup.Name = "btnbackup";
            this.btnbackup.Size = new System.Drawing.Size(236, 90);
            this.btnbackup.TabIndex = 5;
            this.btnbackup.Text = "Backup";
            this.btnbackup.UseVisualStyleBackColor = true;
            this.btnbackup.Click += new System.EventHandler(this.btnbackup_Click);
            // 
            // bttnCSesion
            // 
            this.bttnCSesion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bttnCSesion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.bttnCSesion.Location = new System.Drawing.Point(694, 12);
            this.bttnCSesion.Name = "bttnCSesion";
            this.bttnCSesion.Size = new System.Drawing.Size(94, 39);
            this.bttnCSesion.TabIndex = 7;
            this.bttnCSesion.Text = "Cerrar Sesion";
            this.bttnCSesion.UseVisualStyleBackColor = true;
            this.bttnCSesion.Click += new System.EventHandler(this.bttnCSesion_Click);
            // 
            // button1
            // 
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.button1.Location = new System.Drawing.Point(2, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 39);
            this.button1.TabIndex = 8;
            this.button1.Text = "Back";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F);
            this.label1.Location = new System.Drawing.Point(259, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(285, 39);
            this.label1.TabIndex = 9;
            this.label1.Text = "Backup y Restore";
            // 
            // Backup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Aqua;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.bttnCSesion);
            this.Controls.Add(this.btnrestore);
            this.Controls.Add(this.btnbackup);
            this.Name = "Backup";
            this.Text = "Backup";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnrestore;
        private System.Windows.Forms.Button btnbackup;
        private System.Windows.Forms.Button bttnCSesion;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
    }
}