namespace KKWStore
{
    partial class frmReturnEdit
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_select_user = new System.Windows.Forms.Button();
            this.txt_staff = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_select_user);
            this.groupBox1.Controls.Add(this.txt_staff);
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(348, 150);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btn_select_user
            // 
            this.btn_select_user.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_select_user.Location = new System.Drawing.Point(193, 101);
            this.btn_select_user.Name = "btn_select_user";
            this.btn_select_user.Size = new System.Drawing.Size(31, 23);
            this.btn_select_user.TabIndex = 9;
            this.btn_select_user.Text = "...";
            this.btn_select_user.UseVisualStyleBackColor = true;
            this.btn_select_user.Click += new System.EventHandler(this.btn_select_user_Click);
            // 
            // txt_staff
            // 
            this.txt_staff.Location = new System.Drawing.Point(87, 103);
            this.txt_staff.Name = "txt_staff";
            this.txt_staff.ReadOnly = true;
            this.txt_staff.Size = new System.Drawing.Size(100, 20);
            this.txt_staff.TabIndex = 10;
            this.txt_staff.TabStop = false;
            // 
            // button4
            // 
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button4.Location = new System.Drawing.Point(6, 101);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 8;
            this.button4.TabStop = false;
            this.button4.Text = "经手人";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(245, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 104);
            this.button1.TabIndex = 1;
            this.button1.Text = "提交";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(6, 19);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(218, 76);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // frmReturnEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 174);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmReturnEdit";
            this.Text = "退货::扫描输入SN";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button btn_select_user;
        private System.Windows.Forms.TextBox txt_staff;
    }
}