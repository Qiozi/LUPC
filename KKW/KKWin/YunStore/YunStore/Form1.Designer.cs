namespace YunStore
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonProdList = new System.Windows.Forms.Button();
            this.buttonUpStock = new System.Windows.Forms.Button();
            this.buttonUpSaleInfo = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonProdList
            // 
            this.buttonProdList.Location = new System.Drawing.Point(50, 50);
            this.buttonProdList.Margin = new System.Windows.Forms.Padding(2);
            this.buttonProdList.Name = "buttonProdList";
            this.buttonProdList.Size = new System.Drawing.Size(109, 78);
            this.buttonProdList.TabIndex = 0;
            this.buttonProdList.Text = "产品列表";
            this.buttonProdList.UseVisualStyleBackColor = true;
            this.buttonProdList.Click += new System.EventHandler(this.buttonProdList_Click);
            // 
            // buttonUpStock
            // 
            this.buttonUpStock.Location = new System.Drawing.Point(180, 50);
            this.buttonUpStock.Margin = new System.Windows.Forms.Padding(2);
            this.buttonUpStock.Name = "buttonUpStock";
            this.buttonUpStock.Size = new System.Drawing.Size(109, 38);
            this.buttonUpStock.TabIndex = 1;
            this.buttonUpStock.Text = "上传当前云仓库存";
            this.buttonUpStock.UseVisualStyleBackColor = true;
            this.buttonUpStock.Click += new System.EventHandler(this.buttonUpStock_Click);
            // 
            // buttonUpSaleInfo
            // 
            this.buttonUpSaleInfo.Location = new System.Drawing.Point(292, 50);
            this.buttonUpSaleInfo.Margin = new System.Windows.Forms.Padding(2);
            this.buttonUpSaleInfo.Name = "buttonUpSaleInfo";
            this.buttonUpSaleInfo.Size = new System.Drawing.Size(109, 38);
            this.buttonUpSaleInfo.TabIndex = 2;
            this.buttonUpSaleInfo.Text = "上传云仓销售数量";
            this.buttonUpSaleInfo.UseVisualStyleBackColor = true;
            this.buttonUpSaleInfo.Click += new System.EventHandler(this.buttonUpSaleInfo_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(180, 91);
            this.button3.Margin = new System.Windows.Forms.Padding(2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(109, 38);
            this.button3.TabIndex = 3;
            this.button3.Text = "上传云仓库存历史";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(292, 91);
            this.button4.Margin = new System.Windows.Forms.Padding(2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(109, 38);
            this.button4.TabIndex = 4;
            this.button4.Text = "上传云仓库存历史";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(50, 164);
            this.listView1.Margin = new System.Windows.Forms.Padding(2);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(464, 162);
            this.listView1.TabIndex = 5;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "仓库名称";
            this.columnHeader1.Width = 80;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "总数量";
            this.columnHeader2.Width = 80;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "总金额（计算方式1）";
            this.columnHeader3.Width = 150;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "总金额（计算方式1）";
            this.columnHeader4.Width = 150;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(404, 50);
            this.button5.Margin = new System.Windows.Forms.Padding(2);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(109, 38);
            this.button5.TabIndex = 6;
            this.button5.Text = "导入软件数据";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(404, 91);
            this.button6.Margin = new System.Windows.Forms.Padding(2);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(109, 38);
            this.button6.TabIndex = 7;
            this.button6.Text = "导出软件数据";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(572, 370);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.buttonUpSaleInfo);
            this.Controls.Add(this.buttonUpStock);
            this.Controls.Add(this.buttonProdList);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "云仓数据备份";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonProdList;
        private System.Windows.Forms.Button buttonUpStock;
        private System.Windows.Forms.Button buttonUpSaleInfo;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
    }
}

