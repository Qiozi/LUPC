namespace KKWStore
{
    partial class frmIP
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
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.textBoxPwd = new System.Windows.Forms.TextBox();
            this.button5 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Location = new System.Drawing.Point(30, 33);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 21);
            this.button1.TabIndex = 1;
            this.button1.TabStop = false;
            this.button1.Text = "用户名：";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(205, 107);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(84, 21);
            this.button3.TabIndex = 3;
            this.button3.Text = "取消（&C）";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button4.Location = new System.Drawing.Point(30, 60);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 21);
            this.button4.TabIndex = 5;
            this.button4.TabStop = false;
            this.button4.Text = "密码：";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // textBoxPwd
            // 
            this.textBoxPwd.Location = new System.Drawing.Point(114, 60);
            this.textBoxPwd.Name = "textBoxPwd";
            this.textBoxPwd.Size = new System.Drawing.Size(176, 21);
            this.textBoxPwd.TabIndex = 4;
            this.textBoxPwd.UseSystemPasswordChar = true;
            this.textBoxPwd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxPwd_KeyDown);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(114, 107);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(85, 21);
            this.button5.TabIndex = 6;
            this.button5.Text = "登入";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(114, 33);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(176, 20);
            this.comboBox1.TabIndex = 7;
            // 
            // frmIP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(343, 169);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.textBoxPwd);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmIP";
            this.Text = "库存管理-旧版";
            this.Load += new System.EventHandler(this.frmIP_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmIP_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox textBoxPwd;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}