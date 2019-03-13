namespace KKWStore
{
    partial class frmCheckStoreEdit
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
            this.txt_input = new System.Windows.Forms.TextBox();
            this.button_report = new System.Windows.Forms.Button();
            this.button_save = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.txt_staff = new System.Windows.Forms.TextBox();
            this.txtProdName = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label_no_in_check_5 = new System.Windows.Forms.Label();
            this.richTextBox_no_in_check_5 = new System.Windows.Forms.RichTextBox();
            this.label_no_in_store_4 = new System.Windows.Forms.Label();
            this.richTextBox_no_in_store_4 = new System.Windows.Forms.RichTextBox();
            this.label_Normal_3 = new System.Windows.Forms.Label();
            this.richTextBox_check_nomal_3 = new System.Windows.Forms.RichTextBox();
            this.label_CheckAll_2 = new System.Windows.Forms.Label();
            this.richTextBox_check_all_2 = new System.Windows.Forms.RichTextBox();
            this.label_all_store_1 = new System.Windows.Forms.Label();
            this.richTextBox_all_store_1 = new System.Windows.Forms.RichTextBox();
            this.label_status = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.buttonAutoRun = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txt_input
            // 
            this.txt_input.Location = new System.Drawing.Point(18, 18);
            this.txt_input.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_input.Multiline = true;
            this.txt_input.Name = "txt_input";
            this.txt_input.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_input.Size = new System.Drawing.Size(1185, 159);
            this.txt_input.TabIndex = 1;
            this.txt_input.TextChanged += new System.EventHandler(this.txt_input_TextChanged);
            // 
            // button_report
            // 
            this.button_report.Location = new System.Drawing.Point(806, 189);
            this.button_report.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_report.Name = "button_report";
            this.button_report.Size = new System.Drawing.Size(112, 74);
            this.button_report.TabIndex = 2;
            this.button_report.Text = "生成报表";
            this.button_report.UseVisualStyleBackColor = true;
            this.button_report.Click += new System.EventHandler(this.button_report_Click);
            // 
            // button_save
            // 
            this.button_save.Enabled = false;
            this.button_save.Location = new System.Drawing.Point(927, 189);
            this.button_save.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(132, 74);
            this.button_save.TabIndex = 3;
            this.button_save.Text = "保存";
            this.button_save.UseVisualStyleBackColor = true;
            this.button_save.Click += new System.EventHandler(this.button_save_exist_Click);
            // 
            // button_cancel
            // 
            this.button_cancel.Enabled = false;
            this.button_cancel.Location = new System.Drawing.Point(1069, 189);
            this.button_cancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(134, 74);
            this.button_cancel.TabIndex = 4;
            this.button_cancel.Text = "取消";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.button_exist_no_save_Click);
            // 
            // txt_staff
            // 
            this.txt_staff.Location = new System.Drawing.Point(140, 192);
            this.txt_staff.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_staff.Name = "txt_staff";
            this.txt_staff.ReadOnly = true;
            this.txt_staff.Size = new System.Drawing.Size(531, 26);
            this.txt_staff.TabIndex = 11;
            this.txt_staff.TabStop = false;
            // 
            // txtProdName
            // 
            this.txtProdName.Location = new System.Drawing.Point(140, 238);
            this.txtProdName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtProdName.Name = "txtProdName";
            this.txtProdName.Size = new System.Drawing.Size(531, 26);
            this.txtProdName.TabIndex = 3;
            // 
            // button4
            // 
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button4.Location = new System.Drawing.Point(18, 189);
            this.button4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(112, 35);
            this.button4.TabIndex = 10;
            this.button4.TabStop = false;
            this.button4.Text = "经手人";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Location = new System.Drawing.Point(18, 234);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 35);
            this.button1.TabIndex = 12;
            this.button1.TabStop = false;
            this.button1.Text = "商品名称";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label_no_in_check_5);
            this.groupBox1.Controls.Add(this.richTextBox_no_in_check_5);
            this.groupBox1.Controls.Add(this.label_no_in_store_4);
            this.groupBox1.Controls.Add(this.richTextBox_no_in_store_4);
            this.groupBox1.Controls.Add(this.label_Normal_3);
            this.groupBox1.Controls.Add(this.richTextBox_check_nomal_3);
            this.groupBox1.Controls.Add(this.label_CheckAll_2);
            this.groupBox1.Controls.Add(this.richTextBox_check_all_2);
            this.groupBox1.Controls.Add(this.label_all_store_1);
            this.groupBox1.Controls.Add(this.richTextBox_all_store_1);
            this.groupBox1.Location = new System.Drawing.Point(232, 277);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(971, 469);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            // 
            // label_no_in_check_5
            // 
            this.label_no_in_check_5.AutoSize = true;
            this.label_no_in_check_5.Location = new System.Drawing.Point(772, 41);
            this.label_no_in_check_5.Name = "label_no_in_check_5";
            this.label_no_in_check_5.Size = new System.Drawing.Size(89, 20);
            this.label_no_in_check_5.TabIndex = 33;
            this.label_no_in_check_5.Text = "不在盘点中";
            // 
            // richTextBox_no_in_check_5
            // 
            this.richTextBox_no_in_check_5.Location = new System.Drawing.Point(776, 66);
            this.richTextBox_no_in_check_5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.richTextBox_no_in_check_5.Name = "richTextBox_no_in_check_5";
            this.richTextBox_no_in_check_5.Size = new System.Drawing.Size(176, 380);
            this.richTextBox_no_in_check_5.TabIndex = 32;
            this.richTextBox_no_in_check_5.Text = "";
            // 
            // label_no_in_store_4
            // 
            this.label_no_in_store_4.AutoSize = true;
            this.label_no_in_store_4.Location = new System.Drawing.Point(582, 41);
            this.label_no_in_store_4.Name = "label_no_in_store_4";
            this.label_no_in_store_4.Size = new System.Drawing.Size(89, 20);
            this.label_no_in_store_4.TabIndex = 31;
            this.label_no_in_store_4.Text = "不在仓库中";
            // 
            // richTextBox_no_in_store_4
            // 
            this.richTextBox_no_in_store_4.Location = new System.Drawing.Point(586, 66);
            this.richTextBox_no_in_store_4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.richTextBox_no_in_store_4.Name = "richTextBox_no_in_store_4";
            this.richTextBox_no_in_store_4.Size = new System.Drawing.Size(182, 380);
            this.richTextBox_no_in_store_4.TabIndex = 30;
            this.richTextBox_no_in_store_4.Text = "";
            // 
            // label_Normal_3
            // 
            this.label_Normal_3.AutoSize = true;
            this.label_Normal_3.Location = new System.Drawing.Point(392, 41);
            this.label_Normal_3.Name = "label_Normal_3";
            this.label_Normal_3.Size = new System.Drawing.Size(41, 20);
            this.label_Normal_3.TabIndex = 29;
            this.label_Normal_3.Text = "正常";
            // 
            // richTextBox_check_nomal_3
            // 
            this.richTextBox_check_nomal_3.Location = new System.Drawing.Point(396, 66);
            this.richTextBox_check_nomal_3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.richTextBox_check_nomal_3.Name = "richTextBox_check_nomal_3";
            this.richTextBox_check_nomal_3.Size = new System.Drawing.Size(182, 380);
            this.richTextBox_check_nomal_3.TabIndex = 28;
            this.richTextBox_check_nomal_3.Text = "";
            // 
            // label_CheckAll_2
            // 
            this.label_CheckAll_2.AutoSize = true;
            this.label_CheckAll_2.Location = new System.Drawing.Point(202, 41);
            this.label_CheckAll_2.Name = "label_CheckAll_2";
            this.label_CheckAll_2.Size = new System.Drawing.Size(73, 20);
            this.label_CheckAll_2.TabIndex = 27;
            this.label_CheckAll_2.Text = "盘点输入";
            // 
            // richTextBox_check_all_2
            // 
            this.richTextBox_check_all_2.Location = new System.Drawing.Point(206, 66);
            this.richTextBox_check_all_2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.richTextBox_check_all_2.Name = "richTextBox_check_all_2";
            this.richTextBox_check_all_2.Size = new System.Drawing.Size(182, 380);
            this.richTextBox_check_all_2.TabIndex = 26;
            this.richTextBox_check_all_2.Text = "";
            // 
            // label_all_store_1
            // 
            this.label_all_store_1.AutoSize = true;
            this.label_all_store_1.Location = new System.Drawing.Point(12, 41);
            this.label_all_store_1.Name = "label_all_store_1";
            this.label_all_store_1.Size = new System.Drawing.Size(73, 20);
            this.label_all_store_1.TabIndex = 25;
            this.label_all_store_1.Text = "库中所有";
            // 
            // richTextBox_all_store_1
            // 
            this.richTextBox_all_store_1.Location = new System.Drawing.Point(16, 66);
            this.richTextBox_all_store_1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.richTextBox_all_store_1.Name = "richTextBox_all_store_1";
            this.richTextBox_all_store_1.Size = new System.Drawing.Size(182, 380);
            this.richTextBox_all_store_1.TabIndex = 24;
            this.richTextBox_all_store_1.Text = "";
            // 
            // label_status
            // 
            this.label_status.AutoSize = true;
            this.label_status.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_status.Location = new System.Drawing.Point(1122, 761);
            this.label_status.Name = "label_status";
            this.label_status.Size = new System.Drawing.Size(81, 37);
            this.label_status.TabIndex = 34;
            this.label_status.Text = "正常";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkedListBox1);
            this.groupBox2.Location = new System.Drawing.Point(18, 277);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 469);
            this.groupBox2.TabIndex = 35;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "仓库";
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.HorizontalExtent = 3;
            this.checkedListBox1.Location = new System.Drawing.Point(6, 45);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(188, 403);
            this.checkedListBox1.TabIndex = 13;
            // 
            // buttonAutoRun
            // 
            this.buttonAutoRun.Enabled = false;
            this.buttonAutoRun.Location = new System.Drawing.Point(678, 189);
            this.buttonAutoRun.Name = "buttonAutoRun";
            this.buttonAutoRun.Size = new System.Drawing.Size(121, 74);
            this.buttonAutoRun.TabIndex = 36;
            this.buttonAutoRun.Text = "执行";
            this.buttonAutoRun.UseVisualStyleBackColor = true;
            this.buttonAutoRun.Click += new System.EventHandler(this.buttonAutoRun_Click);
            // 
            // frmCheckStoreEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1219, 816);
            this.Controls.Add(this.buttonAutoRun);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label_status);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtProdName);
            this.Controls.Add(this.txt_staff);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_save);
            this.Controls.Add(this.button_report);
            this.Controls.Add(this.txt_input);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCheckStoreEdit";
            this.Text = "输入盘点数据";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_input;
        private System.Windows.Forms.Button button_report;
        private System.Windows.Forms.Button button_save;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.TextBox txt_staff;
        private System.Windows.Forms.TextBox txtProdName;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label_no_in_check_5;
        private System.Windows.Forms.RichTextBox richTextBox_no_in_check_5;
        private System.Windows.Forms.Label label_no_in_store_4;
        private System.Windows.Forms.RichTextBox richTextBox_no_in_store_4;
        private System.Windows.Forms.Label label_Normal_3;
        private System.Windows.Forms.RichTextBox richTextBox_check_nomal_3;
        private System.Windows.Forms.Label label_CheckAll_2;
        private System.Windows.Forms.RichTextBox richTextBox_check_all_2;
        private System.Windows.Forms.Label label_all_store_1;
        private System.Windows.Forms.RichTextBox richTextBox_all_store_1;
        private System.Windows.Forms.Label label_status;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Button buttonAutoRun;
    }
}