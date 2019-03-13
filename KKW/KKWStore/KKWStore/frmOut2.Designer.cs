namespace KKWStore
{
    partial class frmOut2
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
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.labelQty = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button1 = new System.Windows.Forms.Button();
            this.textBoxSN = new System.Windows.Forms.TextBox();
            this.textBoxKeyword = new System.Windows.Forms.TextBox();
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listViewSN = new System.Windows.Forms.ListView();
            this.listViewPList = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button2 = new System.Windows.Forms.Button();
            this.comboBoxWarehouse = new System.Windows.Forms.ComboBox();
            this.button4 = new System.Windows.Forms.Button();
            this.listViewOrder = new System.Windows.Forms.ListView();
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.移除产品ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.labelTotal = new System.Windows.Forms.Label();
            this.labelSnQty = new System.Windows.Forms.Label();
            this.txtFilename = new System.Windows.Forms.TextBox();
            this.buttonUpload = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "进货日期";
            this.columnHeader8.Width = 127;
            // 
            // labelQty
            // 
            this.labelQty.AutoSize = true;
            this.labelQty.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelQty.Location = new System.Drawing.Point(116, 1028);
            this.labelQty.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelQty.Name = "labelQty";
            this.labelQty.Size = new System.Drawing.Size(19, 20);
            this.labelQty.TabIndex = 38;
            this.labelQty.Text = "0";
            // 
            // button3
            // 
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button3.Location = new System.Drawing.Point(18, 1020);
            this.button3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(88, 35);
            this.button3.TabIndex = 37;
            this.button3.TabStop = false;
            this.button3.Text = "数量：";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(766, 1019);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(420, 35);
            this.buttonSave.TabIndex = 36;
            this.buttonSave.Text = "出库";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "单价";
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Location = new System.Drawing.Point(834, 518);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(139, 35);
            this.button1.TabIndex = 35;
            this.button1.TabStop = false;
            this.button1.Text = "输入商品数量";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBoxSN
            // 
            this.textBoxSN.Location = new System.Drawing.Point(981, 518);
            this.textBoxSN.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxSN.Name = "textBoxSN";
            this.textBoxSN.Size = new System.Drawing.Size(205, 26);
            this.textBoxSN.TabIndex = 34;
            this.textBoxSN.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxSN_KeyDown);
            // 
            // textBoxKeyword
            // 
            this.textBoxKeyword.Location = new System.Drawing.Point(452, 37);
            this.textBoxKeyword.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxKeyword.Name = "textBoxKeyword";
            this.textBoxKeyword.Size = new System.Drawing.Size(372, 26);
            this.textBoxKeyword.TabIndex = 31;
            this.textBoxKeyword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "条码";
            this.columnHeader6.Width = 80;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "产品编号";
            this.columnHeader1.Width = 120;
            // 
            // listViewSN
            // 
            this.listViewSN.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8});
            this.listViewSN.FullRowSelect = true;
            this.listViewSN.GridLines = true;
            this.listViewSN.Location = new System.Drawing.Point(834, 78);
            this.listViewSN.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listViewSN.MultiSelect = false;
            this.listViewSN.Name = "listViewSN";
            this.listViewSN.Size = new System.Drawing.Size(352, 432);
            this.listViewSN.TabIndex = 33;
            this.listViewSN.UseCompatibleStateImageBehavior = false;
            this.listViewSN.View = System.Windows.Forms.View.Details;
            this.listViewSN.DoubleClick += new System.EventHandler(this.listViewSN_DoubleClick);
            // 
            // listViewPList
            // 
            this.listViewPList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.listViewPList.FullRowSelect = true;
            this.listViewPList.GridLines = true;
            this.listViewPList.Location = new System.Drawing.Point(18, 78);
            this.listViewPList.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listViewPList.MultiSelect = false;
            this.listViewPList.Name = "listViewPList";
            this.listViewPList.Size = new System.Drawing.Size(805, 432);
            this.listViewPList.TabIndex = 32;
            this.listViewPList.UseCompatibleStateImageBehavior = false;
            this.listViewPList.View = System.Windows.Forms.View.Details;
            this.listViewPList.SelectedIndexChanged += new System.EventHandler(this.listViewPList_SelectedIndexChanged);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "产品名称";
            this.columnHeader2.Width = 220;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "总价";
            this.columnHeader3.Width = 67;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "数量";
            this.columnHeader4.Width = 85;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "平均单价";
            this.columnHeader5.Width = 230;
            // 
            // button2
            // 
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Location = new System.Drawing.Point(18, 34);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(112, 35);
            this.button2.TabIndex = 39;
            this.button2.TabStop = false;
            this.button2.Text = "选择仓库";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // comboBoxWarehouse
            // 
            this.comboBoxWarehouse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxWarehouse.FormattingEnabled = true;
            this.comboBoxWarehouse.Location = new System.Drawing.Point(140, 37);
            this.comboBoxWarehouse.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.comboBoxWarehouse.Name = "comboBoxWarehouse";
            this.comboBoxWarehouse.Size = new System.Drawing.Size(180, 28);
            this.comboBoxWarehouse.TabIndex = 40;
            this.comboBoxWarehouse.SelectedIndexChanged += new System.EventHandler(this.comboBoxWarehouse_SelectedIndexChanged);
            // 
            // button4
            // 
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button4.Location = new System.Drawing.Point(330, 34);
            this.button4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(112, 35);
            this.button4.TabIndex = 41;
            this.button4.TabStop = false;
            this.button4.Text = "关键字";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // listViewOrder
            // 
            this.listViewOrder.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader12,
            this.columnHeader13,
            this.columnHeader14});
            this.listViewOrder.ContextMenuStrip = this.contextMenuStrip1;
            this.listViewOrder.FullRowSelect = true;
            this.listViewOrder.GridLines = true;
            this.listViewOrder.Location = new System.Drawing.Point(18, 597);
            this.listViewOrder.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listViewOrder.MultiSelect = false;
            this.listViewOrder.Name = "listViewOrder";
            this.listViewOrder.Size = new System.Drawing.Size(1168, 412);
            this.listViewOrder.TabIndex = 42;
            this.listViewOrder.UseCompatibleStateImageBehavior = false;
            this.listViewOrder.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "条码";
            this.columnHeader9.Width = 80;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "所属仓库";
            this.columnHeader10.Width = 80;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "价格";
            this.columnHeader11.Width = 67;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "进货日期";
            this.columnHeader12.Width = 80;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "产品编号";
            this.columnHeader13.Width = 100;
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "产品名称";
            this.columnHeader14.Width = 336;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.移除产品ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(231, 34);
            // 
            // 移除产品ToolStripMenuItem
            // 
            this.移除产品ToolStripMenuItem.Name = "移除产品ToolStripMenuItem";
            this.移除产品ToolStripMenuItem.Size = new System.Drawing.Size(230, 30);
            this.移除产品ToolStripMenuItem.Text = "移除选中的产品";
            this.移除产品ToolStripMenuItem.Click += new System.EventHandler(this.移除产品ToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(18, 572);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 20);
            this.label1.TabIndex = 43;
            this.label1.Text = "订单列表";
            // 
            // button5
            // 
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button5.Location = new System.Drawing.Point(276, 1020);
            this.button5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(104, 35);
            this.button5.TabIndex = 44;
            this.button5.TabStop = false;
            this.button5.Text = "成本总价：";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // labelTotal
            // 
            this.labelTotal.AutoSize = true;
            this.labelTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelTotal.Location = new System.Drawing.Point(408, 1028);
            this.labelTotal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTotal.Name = "labelTotal";
            this.labelTotal.Size = new System.Drawing.Size(19, 20);
            this.labelTotal.TabIndex = 45;
            this.labelTotal.Text = "0";
            // 
            // labelSnQty
            // 
            this.labelSnQty.AutoSize = true;
            this.labelSnQty.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelSnQty.ForeColor = System.Drawing.Color.Blue;
            this.labelSnQty.Location = new System.Drawing.Point(834, 48);
            this.labelSnQty.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelSnQty.Name = "labelSnQty";
            this.labelSnQty.Size = new System.Drawing.Size(77, 20);
            this.labelSnQty.TabIndex = 46;
            this.labelSnQty.Text = "条码数量";
            // 
            // txtFilename
            // 
            this.txtFilename.Location = new System.Drawing.Point(18, 518);
            this.txtFilename.Name = "txtFilename";
            this.txtFilename.ReadOnly = true;
            this.txtFilename.Size = new System.Drawing.Size(679, 26);
            this.txtFilename.TabIndex = 47;
            this.txtFilename.Text = "点击上传文件";
            this.txtFilename.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBox1_MouseClick);
            // 
            // buttonUpload
            // 
            this.buttonUpload.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonUpload.Location = new System.Drawing.Point(704, 518);
            this.buttonUpload.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonUpload.Name = "buttonUpload";
            this.buttonUpload.Size = new System.Drawing.Size(119, 35);
            this.buttonUpload.TabIndex = 48;
            this.buttonUpload.TabStop = false;
            this.buttonUpload.Text = "上传出库文件";
            this.buttonUpload.UseVisualStyleBackColor = true;
            this.buttonUpload.Click += new System.EventHandler(this.buttonUpload_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // frmOut2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1214, 1094);
            this.Controls.Add(this.buttonUpload);
            this.Controls.Add(this.txtFilename);
            this.Controls.Add(this.labelSnQty);
            this.Controls.Add(this.labelTotal);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listViewOrder);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.comboBoxWarehouse);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.labelQty);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBoxSN);
            this.Controls.Add(this.textBoxKeyword);
            this.Controls.Add(this.listViewSN);
            this.Controls.Add(this.listViewPList);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmOut2";
            this.Text = "出库【新】";
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.Label labelQty;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBoxSN;
        private System.Windows.Forms.TextBox textBoxKeyword;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ListView listViewSN;
        private System.Windows.Forms.ListView listViewPList;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox comboBoxWarehouse;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ListView listViewOrder;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label labelTotal;
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.Label labelSnQty;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 移除产品ToolStripMenuItem;
        private System.Windows.Forms.TextBox txtFilename;
        private System.Windows.Forms.Button buttonUpload;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}