namespace LUComputers
{
    partial class Ltd_DanDh
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button_brower = new System.Windows.Forms.Button();
            this.button_upload = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(502, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(39, 81);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(320, 20);
            this.textBox1.TabIndex = 1;
            // 
            // button_brower
            // 
            this.button_brower.Location = new System.Drawing.Point(365, 78);
            this.button_brower.Name = "button_brower";
            this.button_brower.Size = new System.Drawing.Size(47, 23);
            this.button_brower.TabIndex = 2;
            this.button_brower.Text = "...";
            this.button_brower.UseVisualStyleBackColor = true;
            this.button_brower.Click += new System.EventHandler(this.button_brower_Click);
            // 
            // button_upload
            // 
            this.button_upload.Location = new System.Drawing.Point(418, 78);
            this.button_upload.Name = "button_upload";
            this.button_upload.Size = new System.Drawing.Size(47, 23);
            this.button_upload.TabIndex = 3;
            this.button_upload.Text = "upload";
            this.button_upload.UseVisualStyleBackColor = true;
            this.button_upload.Click += new System.EventHandler(this.button_upload_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Ltd_DanDh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 173);
            this.Controls.Add(this.button_upload);
            this.Controls.Add(this.button_brower);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "Ltd_DanDh";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ltd_DanDh";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button_brower;
        private System.Windows.Forms.Button button_upload;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}