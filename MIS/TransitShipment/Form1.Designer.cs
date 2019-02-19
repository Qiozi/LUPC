namespace TransitShipment
{
    partial class Form1
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
            this.button_dandh = new System.Windows.Forms.Button();
            this.button_synnex = new System.Windows.Forms.Button();
            this.button_asi = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(413, 61);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button_dandh
            // 
            this.button_dandh.Location = new System.Drawing.Point(41, 105);
            this.button_dandh.Name = "button_dandh";
            this.button_dandh.Size = new System.Drawing.Size(75, 23);
            this.button_dandh.TabIndex = 2;
            this.button_dandh.Text = "Dandh";
            this.button_dandh.UseVisualStyleBackColor = true;
            this.button_dandh.Click += new System.EventHandler(this.button_dandh_Click);
            // 
            // button_synnex
            // 
            this.button_synnex.Enabled = false;
            this.button_synnex.Location = new System.Drawing.Point(41, 151);
            this.button_synnex.Name = "button_synnex";
            this.button_synnex.Size = new System.Drawing.Size(75, 23);
            this.button_synnex.TabIndex = 3;
            this.button_synnex.Text = "Synnex";
            this.button_synnex.UseVisualStyleBackColor = true;
            this.button_synnex.Click += new System.EventHandler(this.button_synnex_Click);
            // 
            // button_asi
            // 
            this.button_asi.Location = new System.Drawing.Point(41, 61);
            this.button_asi.Name = "button_asi";
            this.button_asi.Size = new System.Drawing.Size(75, 23);
            this.button_asi.TabIndex = 1;
            this.button_asi.Text = "asi";
            this.button_asi.UseVisualStyleBackColor = true;
            this.button_asi.Click += new System.EventHandler(this.button_asi_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(638, 403);
            this.Controls.Add(this.button_synnex);
            this.Controls.Add(this.button_dandh);
            this.Controls.Add(this.button_asi);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button_dandh;
        private System.Windows.Forms.Button button_synnex;
        private System.Windows.Forms.Button button_asi;
    }
}

