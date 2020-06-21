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
            this.buttonProfirt = new System.Windows.Forms.Button();
            this.buttonUpStock = new System.Windows.Forms.Button();
            this.buttonUpSaleInfo = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonCompWarehouse = new System.Windows.Forms.Button();
            this.buttonReadProfit = new System.Windows.Forms.Button();
            this.buttonProfitStat = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // buttonProfirt
            // 
            this.buttonProfirt.Location = new System.Drawing.Point(31, 49);
            this.buttonProfirt.Name = "buttonProfirt";
            this.buttonProfirt.Size = new System.Drawing.Size(164, 40);
            this.buttonProfirt.TabIndex = 0;
            this.buttonProfirt.Text = "利润表输入";
            this.buttonProfirt.UseVisualStyleBackColor = true;
            this.buttonProfirt.Click += new System.EventHandler(this.buttonProfirt_Click);
            // 
            // buttonUpStock
            // 
            this.buttonUpStock.Location = new System.Drawing.Point(201, 49);
            this.buttonUpStock.Name = "buttonUpStock";
            this.buttonUpStock.Size = new System.Drawing.Size(164, 40);
            this.buttonUpStock.TabIndex = 1;
            this.buttonUpStock.Text = "上传当前秒仓库存";
            this.buttonUpStock.UseVisualStyleBackColor = true;
            this.buttonUpStock.Click += new System.EventHandler(this.buttonUpStock_Click);
            // 
            // buttonUpSaleInfo
            // 
            this.buttonUpSaleInfo.Location = new System.Drawing.Point(371, 49);
            this.buttonUpSaleInfo.Name = "buttonUpSaleInfo";
            this.buttonUpSaleInfo.Size = new System.Drawing.Size(164, 40);
            this.buttonUpSaleInfo.TabIndex = 2;
            this.buttonUpSaleInfo.Text = "上传秒仓销售数量";
            this.buttonUpSaleInfo.UseVisualStyleBackColor = true;
            this.buttonUpSaleInfo.Click += new System.EventHandler(this.buttonUpSaleInfo_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(201, 94);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(164, 37);
            this.button3.TabIndex = 3;
            this.button3.Text = "秒仓库存历史";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(371, 94);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(164, 37);
            this.button4.TabIndex = 4;
            this.button4.Text = "秒仓销售历史";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(29, 184);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(694, 267);
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
            this.columnHeader3.Text = "总库存";
            this.columnHeader3.Width = 150;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "总成本";
            this.columnHeader4.Width = 150;
            // 
            // buttonCompWarehouse
            // 
            this.buttonCompWarehouse.Location = new System.Drawing.Point(559, 92);
            this.buttonCompWarehouse.Name = "buttonCompWarehouse";
            this.buttonCompWarehouse.Size = new System.Drawing.Size(164, 40);
            this.buttonCompWarehouse.TabIndex = 6;
            this.buttonCompWarehouse.Text = "公司仓库";
            this.buttonCompWarehouse.UseVisualStyleBackColor = true;
            this.buttonCompWarehouse.Click += new System.EventHandler(this.buttonCompWarehouse_Click);
            // 
            // buttonReadProfit
            // 
            this.buttonReadProfit.Location = new System.Drawing.Point(31, 92);
            this.buttonReadProfit.Name = "buttonReadProfit";
            this.buttonReadProfit.Size = new System.Drawing.Size(164, 40);
            this.buttonReadProfit.TabIndex = 8;
            this.buttonReadProfit.Text = "查看利润表";
            this.buttonReadProfit.UseVisualStyleBackColor = true;
            this.buttonReadProfit.Click += new System.EventHandler(this.buttonReadProfit_Click);
            // 
            // buttonProfitStat
            // 
            this.buttonProfitStat.Location = new System.Drawing.Point(31, 138);
            this.buttonProfitStat.Name = "buttonProfitStat";
            this.buttonProfitStat.Size = new System.Drawing.Size(164, 40);
            this.buttonProfitStat.TabIndex = 9;
            this.buttonProfitStat.Text = "利润表统计";
            this.buttonProfitStat.UseVisualStyleBackColor = true;
            this.buttonProfitStat.Click += new System.EventHandler(this.buttonProfitStat_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(559, 49);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(164, 40);
            this.button1.TabIndex = 10;
            this.button1.Text = "上传公司仓库";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "最后更新时间";
            this.columnHeader5.Width = 180;
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(761, 475);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonProfitStat);
            this.Controls.Add(this.buttonReadProfit);
            this.Controls.Add(this.buttonCompWarehouse);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.buttonUpSaleInfo);
            this.Controls.Add(this.buttonUpStock);
            this.Controls.Add(this.buttonProfirt);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "秒仓数据备份";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonProfirt;
        private System.Windows.Forms.Button buttonUpStock;
        private System.Windows.Forms.Button buttonUpSaleInfo;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button buttonCompWarehouse;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Button buttonReadProfit;
        private System.Windows.Forms.Button buttonProfitStat;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ColumnHeader columnHeader5;
    }
}

