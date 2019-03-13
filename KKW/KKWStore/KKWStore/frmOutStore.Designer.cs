namespace KKWStore
{
    partial class frmOutStore
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btn_select_user = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.label_total = new System.Windows.Forms.Label();
            this.txt_detail = new System.Windows.Forms.TextBox();
            this.label_total_quantity = new System.Windows.Forms.Label();
            this.label_detail_quantity = new System.Windows.Forms.Label();
            this.button10 = new System.Windows.Forms.Button();
            this.txt_paymethod = new System.Windows.Forms.TextBox();
            this.button9 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.btn_exist = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.SN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProdName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cost = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.button13 = new System.Windows.Forms.Button();
            this.txt_total = new System.Windows.Forms.TextBox();
            this.button14 = new System.Windows.Forms.Button();
            this.txt_address = new System.Windows.Forms.TextBox();
            this.button11 = new System.Windows.Forms.Button();
            this.txt_mobile = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.labelFreeTotal = new System.Windows.Forms.Label();
            this.labelFreeQty = new System.Windows.Forms.Label();
            this.button17 = new System.Windows.Forms.Button();
            this.button16 = new System.Windows.Forms.Button();
            this.listViewFree = new System.Windows.Forms.ListView();
            this.columnHeaderProductName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderProductPrice = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button15 = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_select_user
            // 
            this.btn_select_user.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_select_user.Location = new System.Drawing.Point(599, 16);
            this.btn_select_user.Name = "btn_select_user";
            this.btn_select_user.Size = new System.Drawing.Size(31, 21);
            this.btn_select_user.TabIndex = 2;
            this.btn_select_user.Text = "...";
            this.btn_select_user.UseVisualStyleBackColor = true;
            this.btn_select_user.Click += new System.EventHandler(this.btn_select_user_Click);
            // 
            // button12
            // 
            this.button12.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button12.Location = new System.Drawing.Point(198, 483);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(31, 21);
            this.button12.TabIndex = 12;
            this.button12.Text = "...";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // label_total
            // 
            this.label_total.ForeColor = System.Drawing.Color.Blue;
            this.label_total.Location = new System.Drawing.Point(493, 461);
            this.label_total.Name = "label_total";
            this.label_total.Size = new System.Drawing.Size(80, 12);
            this.label_total.TabIndex = 46;
            this.label_total.Text = "0";
            this.label_total.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_detail
            // 
            this.txt_detail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_detail.Location = new System.Drawing.Point(0, 0);
            this.txt_detail.Multiline = true;
            this.txt_detail.Name = "txt_detail";
            this.txt_detail.Size = new System.Drawing.Size(175, 328);
            this.txt_detail.TabIndex = 9;
            this.txt_detail.TextChanged += new System.EventHandler(this.txt_detail_TextChanged);
            this.txt_detail.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txt_detail_KeyUp);
            this.txt_detail.MouseUp += new System.Windows.Forms.MouseEventHandler(this.txt_detail_MouseUp);
            // 
            // label_total_quantity
            // 
            this.label_total_quantity.ForeColor = System.Drawing.Color.Blue;
            this.label_total_quantity.Location = new System.Drawing.Point(326, 461);
            this.label_total_quantity.Name = "label_total_quantity";
            this.label_total_quantity.Size = new System.Drawing.Size(80, 12);
            this.label_total_quantity.TabIndex = 45;
            this.label_total_quantity.Text = "0";
            this.label_total_quantity.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_detail_quantity
            // 
            this.label_detail_quantity.AutoSize = true;
            this.label_detail_quantity.Location = new System.Drawing.Point(666, 461);
            this.label_detail_quantity.Name = "label_detail_quantity";
            this.label_detail_quantity.Size = new System.Drawing.Size(11, 12);
            this.label_detail_quantity.TabIndex = 44;
            this.label_detail_quantity.Text = "0";
            // 
            // button10
            // 
            this.button10.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button10.Location = new System.Drawing.Point(597, 456);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(63, 21);
            this.button10.TabIndex = 43;
            this.button10.TabStop = false;
            this.button10.Text = "数量：";
            this.button10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button10.UseVisualStyleBackColor = true;
            // 
            // txt_paymethod
            // 
            this.txt_paymethod.Location = new System.Drawing.Point(93, 485);
            this.txt_paymethod.MaxLength = 15;
            this.txt_paymethod.Name = "txt_paymethod";
            this.txt_paymethod.Size = new System.Drawing.Size(99, 21);
            this.txt_paymethod.TabIndex = 11;
            // 
            // button9
            // 
            this.button9.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button9.Location = new System.Drawing.Point(12, 483);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(75, 21);
            this.button9.TabIndex = 42;
            this.button9.TabStop = false;
            this.button9.Text = "收款帐户：";
            this.button9.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            this.button8.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button8.Location = new System.Drawing.Point(412, 456);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(75, 21);
            this.button8.TabIndex = 41;
            this.button8.TabStop = false;
            this.button8.Text = "合计金额：";
            this.button8.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button7.Location = new System.Drawing.Point(235, 456);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 21);
            this.button7.TabIndex = 40;
            this.button7.TabStop = false;
            this.button7.Text = "合计数量：";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(93, 15);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(91, 21);
            this.dateTimePicker1.TabIndex = 1;
            // 
            // btn_exist
            // 
            this.btn_exist.Location = new System.Drawing.Point(594, 483);
            this.btn_exist.Name = "btn_exist";
            this.btn_exist.Size = new System.Drawing.Size(175, 21);
            this.btn_exist.TabIndex = 14;
            this.btn_exist.Text = "保存退出";
            this.btn_exist.UseVisualStyleBackColor = true;
            this.btn_exist.Click += new System.EventHandler(this.btn_exist_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(12, 123);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dataGridView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.txt_detail);
            this.splitContainer1.Size = new System.Drawing.Size(760, 328);
            this.splitContainer1.SplitterDistance = 581;
            this.splitContainer1.TabIndex = 38;
            // 
            // dataGridView1
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SN,
            this.ProdName,
            this.cost});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.Size = new System.Drawing.Size(581, 328);
            this.dataGridView1.TabIndex = 8;
            this.dataGridView1.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridView1_CellBeginEdit);
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            this.dataGridView1.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView1_EditingControlShowing);
            // 
            // SN
            // 
            this.SN.HeaderText = "商品编号";
            this.SN.Name = "SN";
            // 
            // ProdName
            // 
            this.ProdName.HeaderText = "商品全名";
            this.ProdName.Name = "ProdName";
            this.ProdName.Width = 300;
            // 
            // cost
            // 
            this.cost.HeaderText = "单价";
            this.cost.Name = "cost";
            // 
            // button5
            // 
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button5.Location = new System.Drawing.Point(12, 69);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 21);
            this.button5.TabIndex = 34;
            this.button5.TabStop = false;
            this.button5.Text = "附加说明";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // txt_note
            // 
            this.txt_note.Location = new System.Drawing.Point(93, 71);
            this.txt_note.MaxLength = 50;
            this.txt_note.Name = "txt_note";
            this.txt_note.Size = new System.Drawing.Size(679, 21);
            this.txt_note.TabIndex = 6;
            // 
            // txt_summary
            // 
            this.txt_summary.Location = new System.Drawing.Point(93, 98);
            this.txt_summary.MaxLength = 50;
            this.txt_summary.Name = "txt_summary";
            this.txt_summary.Size = new System.Drawing.Size(679, 21);
            this.txt_summary.TabIndex = 7;
            // 
            // button6
            // 
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button6.Location = new System.Drawing.Point(12, 96);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 21);
            this.button6.TabIndex = 36;
            this.button6.TabStop = false;
            this.button6.Text = "摘要";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // txt_staff
            // 
            this.txt_staff.Location = new System.Drawing.Point(493, 18);
            this.txt_staff.Name = "txt_staff";
            this.txt_staff.ReadOnly = true;
            this.txt_staff.Size = new System.Drawing.Size(100, 21);
            this.txt_staff.TabIndex = 33;
            this.txt_staff.TabStop = false;
            // 
            // button4
            // 
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button4.Location = new System.Drawing.Point(412, 16);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 21);
            this.button4.TabIndex = 31;
            this.button4.TabStop = false;
            this.button4.Text = "经手人";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // txt_Supplier
            // 
            this.txt_Supplier.Location = new System.Drawing.Point(93, 44);
            this.txt_Supplier.MaxLength = 50;
            this.txt_Supplier.Name = "txt_Supplier";
            this.txt_Supplier.Size = new System.Drawing.Size(172, 21);
            this.txt_Supplier.TabIndex = 3;
            // 
            // button3
            // 
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button3.Location = new System.Drawing.Point(12, 42);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 21);
            this.button3.TabIndex = 28;
            this.button3.TabStop = false;
            this.button3.Text = "购买单位";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // txt_invoice_code
            // 
            this.txt_invoice_code.Location = new System.Drawing.Point(271, 18);
            this.txt_invoice_code.MaxLength = 20;
            this.txt_invoice_code.Name = "txt_invoice_code";
            this.txt_invoice_code.ReadOnly = true;
            this.txt_invoice_code.Size = new System.Drawing.Size(135, 21);
            this.txt_invoice_code.TabIndex = 24;
            // 
            // button2
            // 
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Location = new System.Drawing.Point(190, 16);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 21);
            this.button2.TabIndex = 25;
            this.button2.TabStop = false;
            this.button2.Text = "订单编码";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Location = new System.Drawing.Point(12, 16);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 21);
            this.button1.TabIndex = 23;
            this.button1.TabStop = false;
            this.button1.Text = "录单日期";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button13
            // 
            this.button13.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button13.Location = new System.Drawing.Point(235, 483);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(75, 21);
            this.button13.TabIndex = 47;
            this.button13.TabStop = false;
            this.button13.Text = "收款金额：";
            this.button13.UseVisualStyleBackColor = true;
            // 
            // txt_total
            // 
            this.txt_total.Location = new System.Drawing.Point(316, 485);
            this.txt_total.MaxLength = 15;
            this.txt_total.Name = "txt_total";
            this.txt_total.Size = new System.Drawing.Size(90, 21);
            this.txt_total.TabIndex = 13;
            this.txt_total.TextChanged += new System.EventHandler(this.txt_total_TextChanged);
            // 
            // button14
            // 
            this.button14.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button14.Location = new System.Drawing.Point(444, 42);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(46, 21);
            this.button14.TabIndex = 48;
            this.button14.TabStop = false;
            this.button14.Text = "地址";
            this.button14.UseVisualStyleBackColor = true;
            // 
            // txt_address
            // 
            this.txt_address.Location = new System.Drawing.Point(496, 44);
            this.txt_address.MaxLength = 50;
            this.txt_address.Name = "txt_address";
            this.txt_address.Size = new System.Drawing.Size(273, 21);
            this.txt_address.TabIndex = 5;
            // 
            // button11
            // 
            this.button11.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button11.Location = new System.Drawing.Point(271, 42);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(39, 21);
            this.button11.TabIndex = 49;
            this.button11.TabStop = false;
            this.button11.Text = "电话";
            this.button11.UseVisualStyleBackColor = true;
            // 
            // txt_mobile
            // 
            this.txt_mobile.Location = new System.Drawing.Point(316, 44);
            this.txt_mobile.MaxLength = 50;
            this.txt_mobile.Name = "txt_mobile";
            this.txt_mobile.Size = new System.Drawing.Size(122, 21);
            this.txt_mobile.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.labelFreeTotal);
            this.groupBox1.Controls.Add(this.labelFreeQty);
            this.groupBox1.Controls.Add(this.button17);
            this.groupBox1.Controls.Add(this.button16);
            this.groupBox1.Controls.Add(this.listViewFree);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.button15);
            this.groupBox1.Location = new System.Drawing.Point(796, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(355, 488);
            this.groupBox1.TabIndex = 50;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "赠品";
            // 
            // label3
            // 
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(6, 472);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(261, 12);
            this.label3.TabIndex = 49;
            this.label3.Text = "*如需删除赠品,请选中商品,然后按右键";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelFreeTotal
            // 
            this.labelFreeTotal.ForeColor = System.Drawing.Color.Blue;
            this.labelFreeTotal.Location = new System.Drawing.Point(269, 445);
            this.labelFreeTotal.Name = "labelFreeTotal";
            this.labelFreeTotal.Size = new System.Drawing.Size(80, 12);
            this.labelFreeTotal.TabIndex = 48;
            this.labelFreeTotal.Text = "0";
            this.labelFreeTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelFreeQty
            // 
            this.labelFreeQty.ForeColor = System.Drawing.Color.Blue;
            this.labelFreeQty.Location = new System.Drawing.Point(106, 445);
            this.labelFreeQty.Name = "labelFreeQty";
            this.labelFreeQty.Size = new System.Drawing.Size(80, 12);
            this.labelFreeQty.TabIndex = 47;
            this.labelFreeQty.Text = "0";
            this.labelFreeQty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button17
            // 
            this.button17.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button17.Location = new System.Drawing.Point(192, 440);
            this.button17.Name = "button17";
            this.button17.Size = new System.Drawing.Size(75, 21);
            this.button17.TabIndex = 41;
            this.button17.TabStop = false;
            this.button17.Text = "合计金额";
            this.button17.UseVisualStyleBackColor = true;
            // 
            // button16
            // 
            this.button16.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button16.Location = new System.Drawing.Point(6, 440);
            this.button16.Name = "button16";
            this.button16.Size = new System.Drawing.Size(75, 21);
            this.button16.TabIndex = 40;
            this.button16.TabStop = false;
            this.button16.Text = "数量";
            this.button16.UseVisualStyleBackColor = true;
            // 
            // listViewFree
            // 
            this.listViewFree.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderProductName,
            this.columnHeaderProductPrice});
            this.listViewFree.FullRowSelect = true;
            this.listViewFree.GridLines = true;
            this.listViewFree.Location = new System.Drawing.Point(6, 53);
            this.listViewFree.Name = "listViewFree";
            this.listViewFree.Size = new System.Drawing.Size(343, 382);
            this.listViewFree.TabIndex = 39;
            this.listViewFree.UseCompatibleStateImageBehavior = false;
            this.listViewFree.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderProductName
            // 
            this.columnHeaderProductName.Text = "商品名称";
            this.columnHeaderProductName.Width = 258;
            // 
            // columnHeaderProductPrice
            // 
            this.columnHeaderProductPrice.Text = "单价";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(87, 29);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(262, 21);
            this.textBox1.TabIndex = 38;
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // button15
            // 
            this.button15.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button15.Location = new System.Drawing.Point(6, 27);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(75, 21);
            this.button15.TabIndex = 37;
            this.button15.TabStop = false;
            this.button15.Text = "请输入条码";
            this.button15.UseVisualStyleBackColor = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.删除ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 26);
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.删除ToolStripMenuItem.Text = "删除";
            this.删除ToolStripMenuItem.Click += new System.EventHandler(this.删除ToolStripMenuItem_Click);
            // 
            // frmOutStore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1163, 519);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txt_mobile);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.txt_address);
            this.Controls.Add(this.button14);
            this.Controls.Add(this.txt_total);
            this.Controls.Add(this.button13);
            this.Controls.Add(this.btn_select_user);
            this.Controls.Add(this.button12);
            this.Controls.Add(this.label_total);
            this.Controls.Add(this.label_total_quantity);
            this.Controls.Add(this.label_detail_quantity);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.txt_paymethod);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.btn_exist);
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
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmOutStore";
            this.Text = "出库";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_select_user;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Label label_total;
        private System.Windows.Forms.TextBox txt_detail;
        private System.Windows.Forms.Label label_total_quantity;
        private System.Windows.Forms.Label label_detail_quantity;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.TextBox txt_paymethod;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button btn_exist;
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
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.TextBox txt_total;
        private System.Windows.Forms.Button button14;
        private System.Windows.Forms.TextBox txt_address;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.TextBox txt_mobile;
        private System.Windows.Forms.DataGridViewTextBoxColumn SN;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProdName;
        private System.Windows.Forms.DataGridViewTextBoxColumn cost;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView listViewFree;
        private System.Windows.Forms.ColumnHeader columnHeaderProductName;
        private System.Windows.Forms.ColumnHeader columnHeaderProductPrice;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button15;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelFreeTotal;
        private System.Windows.Forms.Label labelFreeQty;
        private System.Windows.Forms.Button button17;
        private System.Windows.Forms.Button button16;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
    }
}