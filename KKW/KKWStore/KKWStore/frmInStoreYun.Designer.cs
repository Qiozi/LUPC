namespace KKWStore
{
    partial class frmInStoreYun
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
            this.labelWarehouse = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.labelCurrProductName = new System.Windows.Forms.Label();
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listViewSN = new System.Windows.Forms.ListView();
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listViewPList = new System.Windows.Forms.ListView();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.buttonSubmitQty = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownRemoveQty = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRemoveQty)).BeginInit();
            this.SuspendLayout();
            // 
            // labelWarehouse
            // 
            this.labelWarehouse.AutoSize = true;
            this.labelWarehouse.ForeColor = System.Drawing.Color.Blue;
            this.labelWarehouse.Location = new System.Drawing.Point(18, 26);
            this.labelWarehouse.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelWarehouse.Name = "labelWarehouse";
            this.labelWarehouse.Size = new System.Drawing.Size(51, 20);
            this.labelWarehouse.TabIndex = 39;
            this.labelWarehouse.Text = "label2";
            // 
            // button3
            // 
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button3.Location = new System.Drawing.Point(18, 801);
            this.button3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(148, 35);
            this.button3.TabIndex = 37;
            this.button3.TabStop = false;
            this.button3.Text = "当前选中产品：";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(832, 803);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(420, 35);
            this.buttonSave.TabIndex = 36;
            this.buttonSave.Text = "保存";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Location = new System.Drawing.Point(834, 60);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(173, 35);
            this.button1.TabIndex = 35;
            this.button1.TabStop = false;
            this.button1.Text = "输入选中商品数量";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "匹配";
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "单价";
            // 
            // labelCurrProductName
            // 
            this.labelCurrProductName.AutoSize = true;
            this.labelCurrProductName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelCurrProductName.Location = new System.Drawing.Point(176, 809);
            this.labelCurrProductName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelCurrProductName.Name = "labelCurrProductName";
            this.labelCurrProductName.Size = new System.Drawing.Size(59, 20);
            this.labelCurrProductName.TabIndex = 38;
            this.labelCurrProductName.Text = "label1";
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "临时条码";
            this.columnHeader6.Width = 104;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "平均单价";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "数量";
            this.columnHeader4.Width = 42;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "总价";
            this.columnHeader3.Width = 67;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "产品名称";
            this.columnHeader2.Width = 220;
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
            this.columnHeader8,
            this.columnHeader9});
            this.listViewSN.FullRowSelect = true;
            this.listViewSN.GridLines = true;
            this.listViewSN.Location = new System.Drawing.Point(834, 100);
            this.listViewSN.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listViewSN.MultiSelect = false;
            this.listViewSN.Name = "listViewSN";
            this.listViewSN.Size = new System.Drawing.Size(416, 690);
            this.listViewSN.TabIndex = 33;
            this.listViewSN.UseCompatibleStateImageBehavior = false;
            this.listViewSN.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "新条码";
            this.columnHeader9.Width = 100;
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
            this.listViewPList.Location = new System.Drawing.Point(18, 100);
            this.listViewPList.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listViewPList.MultiSelect = false;
            this.listViewPList.Name = "listViewPList";
            this.listViewPList.Size = new System.Drawing.Size(805, 690);
            this.listViewPList.TabIndex = 32;
            this.listViewPList.UseCompatibleStateImageBehavior = false;
            this.listViewPList.View = System.Windows.Forms.View.Details;
            this.listViewPList.SelectedIndexChanged += new System.EventHandler(this.listViewPList_SelectedIndexChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(18, 60);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(325, 26);
            this.textBox1.TabIndex = 31;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(1014, 65);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(160, 26);
            this.numericUpDown1.TabIndex = 40;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // buttonSubmitQty
            // 
            this.buttonSubmitQty.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonSubmitQty.Location = new System.Drawing.Point(1181, 60);
            this.buttonSubmitQty.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonSubmitQty.Name = "buttonSubmitQty";
            this.buttonSubmitQty.Size = new System.Drawing.Size(69, 35);
            this.buttonSubmitQty.TabIndex = 41;
            this.buttonSubmitQty.TabStop = false;
            this.buttonSubmitQty.Text = "确定";
            this.buttonSubmitQty.UseVisualStyleBackColor = true;
            this.buttonSubmitQty.Click += new System.EventHandler(this.buttonSubmitQty_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(524, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 20);
            this.label1.TabIndex = 42;
            this.label1.Text = "删除数量";
            this.label1.Visible = false;
            // 
            // numericUpDownRemoveQty
            // 
            this.numericUpDownRemoveQty.Location = new System.Drawing.Point(615, 61);
            this.numericUpDownRemoveQty.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownRemoveQty.Name = "numericUpDownRemoveQty";
            this.numericUpDownRemoveQty.ReadOnly = true;
            this.numericUpDownRemoveQty.Size = new System.Drawing.Size(120, 26);
            this.numericUpDownRemoveQty.TabIndex = 43;
            this.numericUpDownRemoveQty.Visible = false;
            // 
            // frmInStoreYun
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1270, 865);
            this.Controls.Add(this.numericUpDownRemoveQty);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonSubmitQty);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.labelWarehouse);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.labelCurrProductName);
            this.Controls.Add(this.listViewSN);
            this.Controls.Add(this.listViewPList);
            this.Controls.Add(this.textBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmInStoreYun";
            this.Text = "frmInStoreYun";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRemoveQty)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelWarehouse;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.Label labelCurrProductName;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ListView listViewSN;
        private System.Windows.Forms.ListView listViewPList;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.Button buttonSubmitQty;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownRemoveQty;
    }
}