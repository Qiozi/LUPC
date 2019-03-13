namespace KKWStore
{
    partial class frmModifySNOfProd
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
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.labelCurrProductName = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listViewPList = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listViewSN = new System.Windows.Forms.ListView();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.labelWarehouse = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.labelNewProd = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "进货日期";
            this.columnHeader8.Width = 70;
            // 
            // labelCurrProductName
            // 
            this.labelCurrProductName.AutoSize = true;
            this.labelCurrProductName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelCurrProductName.Location = new System.Drawing.Point(120, 394);
            this.labelCurrProductName.Name = "labelCurrProductName";
            this.labelCurrProductName.Size = new System.Drawing.Size(41, 13);
            this.labelCurrProductName.TabIndex = 38;
            this.labelCurrProductName.Text = "label1";
            // 
            // button3
            // 
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button3.Location = new System.Drawing.Point(15, 389);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(99, 23);
            this.button3.TabIndex = 37;
            this.button3.TabStop = false;
            this.button3.Text = "当前选中产品：";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(309, 472);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(280, 37);
            this.buttonSave.TabIndex = 36;
            this.buttonSave.Text = "修改";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "单价";
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
            this.listViewPList.Location = new System.Drawing.Point(12, 28);
            this.listViewPList.MultiSelect = false;
            this.listViewPList.Name = "listViewPList";
            this.listViewPList.Size = new System.Drawing.Size(538, 355);
            this.listViewPList.TabIndex = 32;
            this.listViewPList.UseCompatibleStateImageBehavior = false;
            this.listViewPList.View = System.Windows.Forms.View.Details;
            this.listViewPList.SelectedIndexChanged += new System.EventHandler(this.listViewPList_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "产品编号";
            this.columnHeader1.Width = 120;
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
            this.columnHeader4.Width = 42;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "平均单价";
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "临时条码";
            this.columnHeader6.Width = 104;
            // 
            // listViewSN
            // 
            this.listViewSN.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8});
            this.listViewSN.FullRowSelect = true;
            this.listViewSN.GridLines = true;
            this.listViewSN.Location = new System.Drawing.Point(556, 28);
            this.listViewSN.MultiSelect = false;
            this.listViewSN.Name = "listViewSN";
            this.listViewSN.Size = new System.Drawing.Size(279, 355);
            this.listViewSN.TabIndex = 33;
            this.listViewSN.UseCompatibleStateImageBehavior = false;
            this.listViewSN.View = System.Windows.Forms.View.Details;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(123, 420);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(218, 20);
            this.textBox1.TabIndex = 31;
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // labelWarehouse
            // 
            this.labelWarehouse.AutoSize = true;
            this.labelWarehouse.ForeColor = System.Drawing.Color.Blue;
            this.labelWarehouse.Location = new System.Drawing.Point(12, 12);
            this.labelWarehouse.Name = "labelWarehouse";
            this.labelWarehouse.Size = new System.Drawing.Size(35, 13);
            this.labelWarehouse.TabIndex = 39;
            this.labelWarehouse.Text = "label2";
            // 
            // button2
            // 
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Location = new System.Drawing.Point(15, 418);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(99, 23);
            this.button2.TabIndex = 40;
            this.button2.TabStop = false;
            this.button2.Text = "新的产品SKU：";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // labelNewProd
            // 
            this.labelNewProd.AutoSize = true;
            this.labelNewProd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelNewProd.Location = new System.Drawing.Point(347, 423);
            this.labelNewProd.Name = "labelNewProd";
            this.labelNewProd.Size = new System.Drawing.Size(19, 13);
            this.labelNewProd.TabIndex = 41;
            this.labelNewProd.Text = "...";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(120, 443);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(269, 13);
            this.label2.TabIndex = 42;
            this.label2.Text = "输入新产品SKU后，按回车，确认是否有产品存在";
            // 
            // frmModifySNOfProd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(853, 521);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelNewProd);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.labelCurrProductName);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.listViewPList);
            this.Controls.Add(this.listViewSN);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.labelWarehouse);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmModifySNOfProd";
            this.Text = "修正条码所属";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.Label labelCurrProductName;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ListView listViewPList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ListView listViewSN;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label labelWarehouse;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label labelNewProd;
        private System.Windows.Forms.Label label2;

    }
}