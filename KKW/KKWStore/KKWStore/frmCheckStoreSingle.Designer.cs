namespace KKWStore
{
    partial class frmCheckStoreSingle
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btn_select_user = new System.Windows.Forms.Button();
            this.txt_staff = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button_exist_no_save = new System.Windows.Forms.Button();
            this.button_save_exist = new System.Windows.Forms.Button();
            this.button_report = new System.Windows.Forms.Button();
            this.richTextBox_result = new System.Windows.Forms.RichTextBox();
            this.txt_input = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 151);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(340, 20);
            this.textBox1.TabIndex = 17;
            // 
            // btn_select_user
            // 
            this.btn_select_user.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_select_user.Location = new System.Drawing.Point(199, 123);
            this.btn_select_user.Name = "btn_select_user";
            this.btn_select_user.Size = new System.Drawing.Size(31, 23);
            this.btn_select_user.TabIndex = 15;
            this.btn_select_user.Text = "...";
            this.btn_select_user.UseVisualStyleBackColor = true;
            // 
            // txt_staff
            // 
            this.txt_staff.Location = new System.Drawing.Point(93, 125);
            this.txt_staff.Name = "txt_staff";
            this.txt_staff.ReadOnly = true;
            this.txt_staff.Size = new System.Drawing.Size(100, 20);
            this.txt_staff.TabIndex = 20;
            this.txt_staff.TabStop = false;
            // 
            // button4
            // 
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button4.Location = new System.Drawing.Point(12, 123);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 19;
            this.button4.TabStop = false;
            this.button4.Text = "经手人";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button_exist_no_save
            // 
            this.button_exist_no_save.Enabled = false;
            this.button_exist_no_save.Location = new System.Drawing.Point(533, 123);
            this.button_exist_no_save.Name = "button_exist_no_save";
            this.button_exist_no_save.Size = new System.Drawing.Size(89, 48);
            this.button_exist_no_save.TabIndex = 18;
            this.button_exist_no_save.Text = "退出不保存";
            this.button_exist_no_save.UseVisualStyleBackColor = true;
            // 
            // button_save_exist
            // 
            this.button_save_exist.Enabled = false;
            this.button_save_exist.Location = new System.Drawing.Point(439, 123);
            this.button_save_exist.Name = "button_save_exist";
            this.button_save_exist.Size = new System.Drawing.Size(88, 48);
            this.button_save_exist.TabIndex = 16;
            this.button_save_exist.Text = "保存，退出";
            this.button_save_exist.UseVisualStyleBackColor = true;
            // 
            // button_report
            // 
            this.button_report.Location = new System.Drawing.Point(358, 123);
            this.button_report.Name = "button_report";
            this.button_report.Size = new System.Drawing.Size(75, 48);
            this.button_report.TabIndex = 14;
            this.button_report.Text = "生成报表";
            this.button_report.UseVisualStyleBackColor = true;
            this.button_report.Click += new System.EventHandler(this.button_report_Click);
            // 
            // richTextBox_result
            // 
            this.richTextBox_result.Location = new System.Drawing.Point(12, 177);
            this.richTextBox_result.Name = "richTextBox_result";
            this.richTextBox_result.Size = new System.Drawing.Size(610, 266);
            this.richTextBox_result.TabIndex = 12;
            this.richTextBox_result.Text = "";
            // 
            // txt_input
            // 
            this.txt_input.Location = new System.Drawing.Point(12, 12);
            this.txt_input.Multiline = true;
            this.txt_input.Name = "txt_input";
            this.txt_input.Size = new System.Drawing.Size(610, 105);
            this.txt_input.TabIndex = 13;
            // 
            // frmCheckStoreSingle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 455);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btn_select_user);
            this.Controls.Add(this.txt_staff);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button_exist_no_save);
            this.Controls.Add(this.button_save_exist);
            this.Controls.Add(this.button_report);
            this.Controls.Add(this.richTextBox_result);
            this.Controls.Add(this.txt_input);
            this.Name = "frmCheckStoreSingle";
            this.Text = "盘点单个产品";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btn_select_user;
        private System.Windows.Forms.TextBox txt_staff;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button_exist_no_save;
        private System.Windows.Forms.Button button_save_exist;
        private System.Windows.Forms.Button button_report;
        private System.Windows.Forms.RichTextBox richTextBox_result;
        private System.Windows.Forms.TextBox txt_input;
    }
}