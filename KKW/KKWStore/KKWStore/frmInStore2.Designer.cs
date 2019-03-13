namespace KKWStore
{
    partial class frmInStore2
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btn_exist = new System.Windows.Forms.Button();
            this.btn_select_user = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.label_total = new System.Windows.Forms.Label();
            this.label_total_quantity = new System.Windows.Forms.Label();
            this.label_detail_quantity = new System.Windows.Forms.Label();
            this.button10 = new System.Windows.Forms.Button();
            this.txt_paymethod = new System.Windows.Forms.TextBox();
            this.button9 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.labelWarehouse = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.txt_code_input_Text = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.删除选中行ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button5 = new System.Windows.Forms.Button();
            this.txt_note = new System.Windows.Forms.TextBox();
            this.txt_summary = new System.Windows.Forms.TextBox();
            this.button6 = new System.Windows.Forms.Button();
            this.txt_staff = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.txt_Supplier = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.txt_invoice_code = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_exist
            // 
            this.btn_exist.Location = new System.Drawing.Point(787, 489);
            this.btn_exist.Name = "btn_exist";
            this.btn_exist.Size = new System.Drawing.Size(175, 52);
            this.btn_exist.TabIndex = 39;
            this.btn_exist.Text = "保存退出";
            this.btn_exist.UseVisualStyleBackColor = true;
            this.btn_exist.Click += new System.EventHandler(this.btn_exist_Click);
            // 
            // btn_select_user
            // 
            this.btn_select_user.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_select_user.Location = new System.Drawing.Point(599, 12);
            this.btn_select_user.Name = "btn_select_user";
            this.btn_select_user.Size = new System.Drawing.Size(31, 23);
            this.btn_select_user.TabIndex = 27;
            this.btn_select_user.Text = "...";
            this.btn_select_user.UseVisualStyleBackColor = true;
            this.btn_select_user.Click += new System.EventHandler(this.btn_select_user_Click);
            // 
            // button12
            // 
            this.button12.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button12.Location = new System.Drawing.Point(259, 518);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(31, 23);
            this.button12.TabIndex = 38;
            this.button12.Text = "...";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // button11
            // 
            this.button11.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button11.Location = new System.Drawing.Point(599, 41);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(31, 23);
            this.button11.TabIndex = 29;
            this.button11.Text = "...";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // label_total
            // 
            this.label_total.ForeColor = System.Drawing.Color.Blue;
            this.label_total.Location = new System.Drawing.Point(505, 523);
            this.label_total.Name = "label_total";
            this.label_total.Size = new System.Drawing.Size(80, 13);
            this.label_total.TabIndex = 47;
            this.label_total.Text = "0";
            this.label_total.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_total_quantity
            // 
            this.label_total_quantity.ForeColor = System.Drawing.Color.Blue;
            this.label_total_quantity.Location = new System.Drawing.Point(505, 494);
            this.label_total_quantity.Name = "label_total_quantity";
            this.label_total_quantity.Size = new System.Drawing.Size(80, 13);
            this.label_total_quantity.TabIndex = 46;
            this.label_total_quantity.Text = "0";
            this.label_total_quantity.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_detail_quantity
            // 
            this.label_detail_quantity.AutoSize = true;
            this.label_detail_quantity.Location = new System.Drawing.Point(666, 494);
            this.label_detail_quantity.Name = "label_detail_quantity";
            this.label_detail_quantity.Size = new System.Drawing.Size(13, 13);
            this.label_detail_quantity.TabIndex = 45;
            this.label_detail_quantity.Text = "0";
            // 
            // button10
            // 
            this.button10.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button10.Location = new System.Drawing.Point(597, 489);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(63, 23);
            this.button10.TabIndex = 44;
            this.button10.TabStop = false;
            this.button10.Text = "数量：";
            this.button10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button10.UseVisualStyleBackColor = true;
            // 
            // txt_paymethod
            // 
            this.txt_paymethod.Location = new System.Drawing.Point(93, 520);
            this.txt_paymethod.MaxLength = 15;
            this.txt_paymethod.Name = "txt_paymethod";
            this.txt_paymethod.Size = new System.Drawing.Size(160, 20);
            this.txt_paymethod.TabIndex = 37;
            // 
            // button9
            // 
            this.button9.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button9.Location = new System.Drawing.Point(12, 518);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(75, 23);
            this.button9.TabIndex = 43;
            this.button9.TabStop = false;
            this.button9.Text = "付款帐户：";
            this.button9.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            this.button8.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button8.Location = new System.Drawing.Point(424, 518);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(75, 23);
            this.button8.TabIndex = 42;
            this.button8.TabStop = false;
            this.button8.Text = "合计金额：";
            this.button8.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button7.Location = new System.Drawing.Point(424, 489);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 23);
            this.button7.TabIndex = 41;
            this.button7.TabStop = false;
            this.button7.Text = "合计数量：";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(93, 11);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(91, 20);
            this.dateTimePicker1.TabIndex = 23;
            // 
            // labelWarehouse
            // 
            this.labelWarehouse.AutoSize = true;
            this.labelWarehouse.ForeColor = System.Drawing.Color.Blue;
            this.labelWarehouse.Location = new System.Drawing.Point(15, 489);
            this.labelWarehouse.Name = "labelWarehouse";
            this.labelWarehouse.Size = new System.Drawing.Size(84, 13);
            this.labelWarehouse.TabIndex = 48;
            this.labelWarehouse.Text = "labelWarehouse";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(12, 128);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.listView1);
            this.splitContainer1.Panel1.Controls.Add(this.txt_code_input_Text);
            this.splitContainer1.Panel1.Controls.Add(this.dataGridView1);
            this.splitContainer1.Panel2MinSize = 1;
            this.splitContainer1.Size = new System.Drawing.Size(960, 355);
            this.splitContainer1.SplitterDistance = 940;
            this.splitContainer1.TabIndex = 40;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(284, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(313, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "输入产品编号或者名称，即可从下面的列表选中产品(双击)";
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(6, 29);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(400, 323);
            this.listView1.TabIndex = 12;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "编号";
            this.columnHeader1.Width = 121;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "产品名称";
            this.columnHeader2.Width = 260;
            // 
            // txt_code_input_Text
            // 
            this.txt_code_input_Text.Location = new System.Drawing.Point(6, 3);
            this.txt_code_input_Text.Name = "txt_code_input_Text";
            this.txt_code_input_Text.Size = new System.Drawing.Size(272, 20);
            this.txt_code_input_Text.TabIndex = 11;
            this.txt_code_input_Text.TextChanged += new System.EventHandler(this.txt_code_input_TextChanged);
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
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView1.Location = new System.Drawing.Point(412, 28);
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridView1.Size = new System.Drawing.Size(538, 323);
            this.dataGridView1.TabIndex = 10;
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            this.dataGridView1.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValidated);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.删除选中行ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(140, 26);
            // 
            // 删除选中行ToolStripMenuItem
            // 
            this.删除选中行ToolStripMenuItem.Name = "删除选中行ToolStripMenuItem";
            this.删除选中行ToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.删除选中行ToolStripMenuItem.Text = "删除选中行";
            this.删除选中行ToolStripMenuItem.Click += new System.EventHandler(this.删除选中行ToolStripMenuItem_Click);
            // 
            // button5
            // 
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button5.Location = new System.Drawing.Point(12, 70);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 35;
            this.button5.TabStop = false;
            this.button5.Text = "附加说明";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // txt_note
            // 
            this.txt_note.Location = new System.Drawing.Point(93, 72);
            this.txt_note.MaxLength = 50;
            this.txt_note.Name = "txt_note";
            this.txt_note.Size = new System.Drawing.Size(500, 20);
            this.txt_note.TabIndex = 31;
            // 
            // txt_summary
            // 
            this.txt_summary.Location = new System.Drawing.Point(93, 101);
            this.txt_summary.MaxLength = 50;
            this.txt_summary.Name = "txt_summary";
            this.txt_summary.Size = new System.Drawing.Size(500, 20);
            this.txt_summary.TabIndex = 32;
            // 
            // button6
            // 
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button6.Location = new System.Drawing.Point(12, 99);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 36;
            this.button6.TabStop = false;
            this.button6.Text = "摘要";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // txt_staff
            // 
            this.txt_staff.Location = new System.Drawing.Point(493, 14);
            this.txt_staff.Name = "txt_staff";
            this.txt_staff.ReadOnly = true;
            this.txt_staff.Size = new System.Drawing.Size(100, 20);
            this.txt_staff.TabIndex = 34;
            this.txt_staff.TabStop = false;
            // 
            // button4
            // 
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button4.Location = new System.Drawing.Point(412, 12);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 33;
            this.button4.TabStop = false;
            this.button4.Text = "经手人";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // txt_Supplier
            // 
            this.txt_Supplier.Location = new System.Drawing.Point(93, 43);
            this.txt_Supplier.MaxLength = 50;
            this.txt_Supplier.Name = "txt_Supplier";
            this.txt_Supplier.Size = new System.Drawing.Size(500, 20);
            this.txt_Supplier.TabIndex = 28;
            // 
            // button3
            // 
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button3.Location = new System.Drawing.Point(12, 41);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 30;
            this.button3.TabStop = false;
            this.button3.Text = "供货单位";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // txt_invoice_code
            // 
            this.txt_invoice_code.Location = new System.Drawing.Point(271, 14);
            this.txt_invoice_code.MaxLength = 20;
            this.txt_invoice_code.Name = "txt_invoice_code";
            this.txt_invoice_code.Size = new System.Drawing.Size(135, 20);
            this.txt_invoice_code.TabIndex = 25;
            // 
            // button2
            // 
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Location = new System.Drawing.Point(190, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 26;
            this.button2.TabStop = false;
            this.button2.Text = "单据编码";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 24;
            this.button1.TabStop = false;
            this.button1.Text = "录单日期";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // frmInStore2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(986, 562);
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
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.labelWarehouse);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.txt_note);
            this.Controls.Add(this.txt_summary);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.txt_staff);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.txt_Supplier);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.txt_invoice_code);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "frmInStore2";
            this.Text = "采购编辑";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_exist;
        private System.Windows.Forms.Button btn_select_user;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Label label_total;
        private System.Windows.Forms.Label label_total_quantity;
        private System.Windows.Forms.Label label_detail_quantity;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.TextBox txt_paymethod;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label labelWarehouse;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox txt_note;
        private System.Windows.Forms.TextBox txt_summary;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.TextBox txt_staff;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox txt_Supplier;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox txt_invoice_code;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.TextBox txt_code_input_Text;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 删除选中行ToolStripMenuItem;
    }
}