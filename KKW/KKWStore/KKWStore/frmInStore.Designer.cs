﻿namespace KKWStore
{
    partial class frmInStore
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.txt_invoice_code = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.txt_Supplier = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.txt_staff = new System.Windows.Forms.TextBox();
            this.txt_note = new System.Windows.Forms.TextBox();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.txt_summary = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.txt_code_input = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button_current_parent_code = new System.Windows.Forms.Button();
            this.txt_detail = new System.Windows.Forms.TextBox();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.txt_paymethod = new System.Windows.Forms.TextBox();
            this.button10 = new System.Windows.Forms.Button();
            this.label_detail_quantity = new System.Windows.Forms.Label();
            this.label_total_quantity = new System.Windows.Forms.Label();
            this.label_total = new System.Windows.Forms.Label();
            this.button11 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.btn_select_user = new System.Windows.Forms.Button();
            this.btn_exist = new System.Windows.Forms.Button();
            this.labelWarehouse = new System.Windows.Forms.Label();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(93, 17);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(91, 20);
            this.dateTimePicker1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Location = new System.Drawing.Point(12, 18);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.TabStop = false;
            this.button1.Text = "录单日期";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Location = new System.Drawing.Point(190, 18);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.TabStop = false;
            this.button2.Text = "单据编码";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // txt_invoice_code
            // 
            this.txt_invoice_code.Location = new System.Drawing.Point(271, 20);
            this.txt_invoice_code.MaxLength = 20;
            this.txt_invoice_code.Name = "txt_invoice_code";
            this.txt_invoice_code.Size = new System.Drawing.Size(135, 20);
            this.txt_invoice_code.TabIndex = 2;
            // 
            // button3
            // 
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button3.Location = new System.Drawing.Point(12, 47);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 5;
            this.button3.TabStop = false;
            this.button3.Text = "供货单位";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // txt_Supplier
            // 
            this.txt_Supplier.Location = new System.Drawing.Point(93, 49);
            this.txt_Supplier.MaxLength = 50;
            this.txt_Supplier.Name = "txt_Supplier";
            this.txt_Supplier.Size = new System.Drawing.Size(500, 20);
            this.txt_Supplier.TabIndex = 4;
            // 
            // button4
            // 
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button4.Location = new System.Drawing.Point(412, 18);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 7;
            this.button4.TabStop = false;
            this.button4.Text = "经手人";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // txt_staff
            // 
            this.txt_staff.Location = new System.Drawing.Point(493, 20);
            this.txt_staff.Name = "txt_staff";
            this.txt_staff.ReadOnly = true;
            this.txt_staff.Size = new System.Drawing.Size(100, 20);
            this.txt_staff.TabIndex = 8;
            this.txt_staff.TabStop = false;
            // 
            // txt_note
            // 
            this.txt_note.Location = new System.Drawing.Point(93, 78);
            this.txt_note.MaxLength = 50;
            this.txt_note.Name = "txt_note";
            this.txt_note.Size = new System.Drawing.Size(500, 20);
            this.txt_note.TabIndex = 6;
            // 
            // button5
            // 
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button5.Location = new System.Drawing.Point(12, 76);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 10;
            this.button5.TabStop = false;
            this.button5.Text = "附加说明";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button6.Location = new System.Drawing.Point(12, 105);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 11;
            this.button6.TabStop = false;
            this.button6.Text = "摘要";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // txt_summary
            // 
            this.txt_summary.Location = new System.Drawing.Point(93, 107);
            this.txt_summary.MaxLength = 50;
            this.txt_summary.Name = "txt_summary";
            this.txt_summary.Size = new System.Drawing.Size(500, 20);
            this.txt_summary.TabIndex = 7;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(12, 134);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.txt_code_input);
            this.splitContainer1.Panel1.Controls.Add(this.dataGridView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.button_current_parent_code);
            this.splitContainer1.Panel2.Controls.Add(this.txt_detail);
            this.splitContainer1.Size = new System.Drawing.Size(760, 355);
            this.splitContainer1.SplitterDistance = 581;
            this.splitContainer1.TabIndex = 13;
            // 
            // txt_code_input
            // 
            this.txt_code_input.Location = new System.Drawing.Point(3, 3);
            this.txt_code_input.Name = "txt_code_input";
            this.txt_code_input.Size = new System.Drawing.Size(339, 20);
            this.txt_code_input.TabIndex = 8;
            this.txt_code_input.TextChanged += new System.EventHandler(this.txt_code_input_TextChanged);
            this.txt_code_input.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_code_input_KeyDown);
            this.txt_code_input.Validating += new System.ComponentModel.CancelEventHandler(this.txt_code_input_Validating);
            // 
            // dataGridView1
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView1.Location = new System.Drawing.Point(0, 28);
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridView1.Size = new System.Drawing.Size(581, 327);
            this.dataGridView1.TabIndex = 10;
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            this.dataGridView1.CellStateChanged += new System.Windows.Forms.DataGridViewCellStateChangedEventHandler(this.dataGridView1_CellStateChanged);
            this.dataGridView1.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValidated);
            this.dataGridView1.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_RowHeaderMouseClick);
            // 
            // button_current_parent_code
            // 
            this.button_current_parent_code.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_current_parent_code.Location = new System.Drawing.Point(3, 3);
            this.button_current_parent_code.Name = "button_current_parent_code";
            this.button_current_parent_code.Size = new System.Drawing.Size(169, 23);
            this.button_current_parent_code.TabIndex = 3;
            this.button_current_parent_code.TabStop = false;
            this.button_current_parent_code.UseVisualStyleBackColor = true;
            // 
            // txt_detail
            // 
            this.txt_detail.Location = new System.Drawing.Point(0, 28);
            this.txt_detail.Multiline = true;
            this.txt_detail.Name = "txt_detail";
            this.txt_detail.Size = new System.Drawing.Size(175, 327);
            this.txt_detail.TabIndex = 9;
            this.txt_detail.TextChanged += new System.EventHandler(this.txt_detail_TextChanged);
            this.txt_detail.Leave += new System.EventHandler(this.txt_detail_Leave);
            // 
            // button7
            // 
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button7.Location = new System.Drawing.Point(424, 495);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 23);
            this.button7.TabIndex = 14;
            this.button7.TabStop = false;
            this.button7.Text = "合计数量：";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            this.button8.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button8.Location = new System.Drawing.Point(424, 524);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(75, 23);
            this.button8.TabIndex = 15;
            this.button8.TabStop = false;
            this.button8.Text = "合计金额：";
            this.button8.UseVisualStyleBackColor = true;
            // 
            // button9
            // 
            this.button9.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button9.Location = new System.Drawing.Point(12, 524);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(75, 23);
            this.button9.TabIndex = 16;
            this.button9.TabStop = false;
            this.button9.Text = "付款帐户：";
            this.button9.UseVisualStyleBackColor = true;
            // 
            // txt_paymethod
            // 
            this.txt_paymethod.Location = new System.Drawing.Point(93, 526);
            this.txt_paymethod.MaxLength = 15;
            this.txt_paymethod.Name = "txt_paymethod";
            this.txt_paymethod.Size = new System.Drawing.Size(160, 20);
            this.txt_paymethod.TabIndex = 11;
            // 
            // button10
            // 
            this.button10.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button10.Location = new System.Drawing.Point(597, 495);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(63, 23);
            this.button10.TabIndex = 18;
            this.button10.TabStop = false;
            this.button10.Text = "数量：";
            this.button10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button10.UseVisualStyleBackColor = true;
            // 
            // label_detail_quantity
            // 
            this.label_detail_quantity.AutoSize = true;
            this.label_detail_quantity.Location = new System.Drawing.Point(666, 500);
            this.label_detail_quantity.Name = "label_detail_quantity";
            this.label_detail_quantity.Size = new System.Drawing.Size(13, 13);
            this.label_detail_quantity.TabIndex = 19;
            this.label_detail_quantity.Text = "0";
            // 
            // label_total_quantity
            // 
            this.label_total_quantity.ForeColor = System.Drawing.Color.Blue;
            this.label_total_quantity.Location = new System.Drawing.Point(505, 500);
            this.label_total_quantity.Name = "label_total_quantity";
            this.label_total_quantity.Size = new System.Drawing.Size(80, 13);
            this.label_total_quantity.TabIndex = 20;
            this.label_total_quantity.Text = "0";
            this.label_total_quantity.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_total
            // 
            this.label_total.ForeColor = System.Drawing.Color.Blue;
            this.label_total.Location = new System.Drawing.Point(505, 529);
            this.label_total.Name = "label_total";
            this.label_total.Size = new System.Drawing.Size(80, 13);
            this.label_total.TabIndex = 21;
            this.label_total.Text = "0";
            this.label_total.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // button11
            // 
            this.button11.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button11.Location = new System.Drawing.Point(599, 47);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(31, 23);
            this.button11.TabIndex = 5;
            this.button11.Text = "...";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // button12
            // 
            this.button12.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button12.Location = new System.Drawing.Point(259, 524);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(31, 23);
            this.button12.TabIndex = 12;
            this.button12.Text = "...";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // btn_select_user
            // 
            this.btn_select_user.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_select_user.Location = new System.Drawing.Point(599, 18);
            this.btn_select_user.Name = "btn_select_user";
            this.btn_select_user.Size = new System.Drawing.Size(31, 23);
            this.btn_select_user.TabIndex = 3;
            this.btn_select_user.Text = "...";
            this.btn_select_user.UseVisualStyleBackColor = true;
            this.btn_select_user.Click += new System.EventHandler(this.btn_select_user_Click);
            // 
            // btn_exist
            // 
            this.btn_exist.Location = new System.Drawing.Point(597, 524);
            this.btn_exist.Name = "btn_exist";
            this.btn_exist.Size = new System.Drawing.Size(175, 23);
            this.btn_exist.TabIndex = 13;
            this.btn_exist.Text = "保存退出";
            this.btn_exist.UseVisualStyleBackColor = true;
            this.btn_exist.Click += new System.EventHandler(this.btn_exist_Click);
            // 
            // labelWarehouse
            // 
            this.labelWarehouse.AutoSize = true;
            this.labelWarehouse.ForeColor = System.Drawing.Color.Blue;
            this.labelWarehouse.Location = new System.Drawing.Point(15, 495);
            this.labelWarehouse.Name = "labelWarehouse";
            this.labelWarehouse.Size = new System.Drawing.Size(84, 13);
            this.labelWarehouse.TabIndex = 22;
            this.labelWarehouse.Text = "labelWarehouse";
            // 
            // frmInStore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.labelWarehouse);
            this.Controls.Add(this.btn_exist);
            this.Controls.Add(this.btn_select_user);
            this.Controls.Add(this.button12);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.label_total);
            this.Controls.Add(this.label_total_quantity);
            this.Controls.Add(this.label_detail_quantity);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.txt_paymethod);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.txt_summary);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.txt_note);
            this.Controls.Add(this.txt_staff);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.txt_Supplier);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.txt_invoice_code);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dateTimePicker1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmInStore";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "入库";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txt_invoice_code;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox txt_Supplier;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox txt_staff;
        private System.Windows.Forms.TextBox txt_note;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.TextBox txt_summary;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txt_detail;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.TextBox txt_paymethod;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Label label_detail_quantity;
        private System.Windows.Forms.Label label_total_quantity;
        private System.Windows.Forms.Label label_total;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Button btn_select_user;
        private System.Windows.Forms.Button btn_exist;
        private System.Windows.Forms.TextBox txt_code_input;
        private System.Windows.Forms.Button button_current_parent_code;
        private System.Windows.Forms.Label labelWarehouse;

    }
}