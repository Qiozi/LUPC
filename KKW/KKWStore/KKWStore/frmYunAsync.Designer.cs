namespace KKWStore
{
    partial class frmYunAsync
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
            this.button1 = new System.Windows.Forms.Button();
            this.textBoxFile = new System.Windows.Forms.TextBox();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.listViewHistory3 = new System.Windows.Forms.ListView();
            this.columnHeader23 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader21 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader22 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.查看明细ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.listViewHistory1 = new System.Windows.Forms.ListView();
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.查看明细ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.buttonGenerateNew = new System.Windows.Forms.Button();
            this.buttonReadFile = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(12, 20);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(84, 21);
            this.button1.TabIndex = 1;
            this.button1.Text = "Excel 文件名";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // textBoxFile
            // 
            this.textBoxFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxFile.Location = new System.Drawing.Point(102, 20);
            this.textBoxFile.Name = "textBoxFile";
            this.textBoxFile.Size = new System.Drawing.Size(268, 23);
            this.textBoxFile.TabIndex = 2;
            this.textBoxFile.Click += new System.EventHandler(this.textBoxFile_Click);
            this.textBoxFile.TextChanged += new System.EventHandler(this.TextBoxFile_TextChanged);
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Location = new System.Drawing.Point(483, 20);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(84, 21);
            this.buttonUpdate.TabIndex = 3;
            this.buttonUpdate.Text = "上传";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listView1);
            this.groupBox1.Location = new System.Drawing.Point(12, 66);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(764, 226);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "匹配结果";
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(6, 18);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(752, 204);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "云仓编码";
            this.columnHeader1.Width = 292;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "云仓库存";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "编码";
            this.columnHeader3.Width = 120;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "库存";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "操作";
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "状态";
            this.columnHeader6.Width = 74;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.listViewHistory3);
            this.groupBox2.Controls.Add(this.listViewHistory1);
            this.groupBox2.Location = new System.Drawing.Point(12, 297);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(764, 215);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "历史纪录";
            // 
            // listViewHistory3
            // 
            this.listViewHistory3.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader23,
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader12,
            this.columnHeader21,
            this.columnHeader22});
            this.listViewHistory3.ContextMenuStrip = this.contextMenuStrip2;
            this.listViewHistory3.FullRowSelect = true;
            this.listViewHistory3.GridLines = true;
            this.listViewHistory3.Location = new System.Drawing.Point(402, 18);
            this.listViewHistory3.Name = "listViewHistory3";
            this.listViewHistory3.Size = new System.Drawing.Size(356, 192);
            this.listViewHistory3.TabIndex = 1;
            this.listViewHistory3.UseCompatibleStateImageBehavior = false;
            this.listViewHistory3.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader23
            // 
            this.columnHeader23.Text = "日期";
            this.columnHeader23.Width = 80;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "云仓编码";
            this.columnHeader10.Width = 106;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "云仓库存";
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "编码";
            this.columnHeader12.Width = 75;
            // 
            // columnHeader21
            // 
            this.columnHeader21.Text = "库存";
            // 
            // columnHeader22
            // 
            this.columnHeader22.Text = "操作";
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.查看明细ToolStripMenuItem1});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(125, 26);
            // 
            // 查看明细ToolStripMenuItem1
            // 
            this.查看明细ToolStripMenuItem1.Name = "查看明细ToolStripMenuItem1";
            this.查看明细ToolStripMenuItem1.Size = new System.Drawing.Size(124, 22);
            this.查看明细ToolStripMenuItem1.Text = "查看明细";
            this.查看明细ToolStripMenuItem1.Click += new System.EventHandler(this.查看明细ToolStripMenuItem1_Click);
            // 
            // listViewHistory1
            // 
            this.listViewHistory1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9});
            this.listViewHistory1.ContextMenuStrip = this.contextMenuStrip1;
            this.listViewHistory1.FullRowSelect = true;
            this.listViewHistory1.GridLines = true;
            this.listViewHistory1.Location = new System.Drawing.Point(6, 18);
            this.listViewHistory1.Name = "listViewHistory1";
            this.listViewHistory1.Size = new System.Drawing.Size(390, 192);
            this.listViewHistory1.TabIndex = 1;
            this.listViewHistory1.UseCompatibleStateImageBehavior = false;
            this.listViewHistory1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "文件名称";
            this.columnHeader7.Width = 158;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "日期";
            this.columnHeader8.Width = 110;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "操作人";
            this.columnHeader9.Width = 103;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.查看明细ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 26);
            // 
            // 查看明细ToolStripMenuItem
            // 
            this.查看明细ToolStripMenuItem.Name = "查看明细ToolStripMenuItem";
            this.查看明细ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.查看明细ToolStripMenuItem.Text = "查看明细";
            this.查看明细ToolStripMenuItem.Click += new System.EventHandler(this.查看明细ToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // buttonGenerateNew
            // 
            this.buttonGenerateNew.Location = new System.Drawing.Point(574, 20);
            this.buttonGenerateNew.Name = "buttonGenerateNew";
            this.buttonGenerateNew.Size = new System.Drawing.Size(194, 21);
            this.buttonGenerateNew.TabIndex = 6;
            this.buttonGenerateNew.Text = "未匹配的产品生成为新产品";
            this.buttonGenerateNew.UseVisualStyleBackColor = true;
            this.buttonGenerateNew.Click += new System.EventHandler(this.buttonGenerateNew_Click);
            // 
            // buttonReadFile
            // 
            this.buttonReadFile.Location = new System.Drawing.Point(375, 20);
            this.buttonReadFile.Name = "buttonReadFile";
            this.buttonReadFile.Size = new System.Drawing.Size(84, 21);
            this.buttonReadFile.TabIndex = 7;
            this.buttonReadFile.Text = "读excel";
            this.buttonReadFile.UseVisualStyleBackColor = true;
            this.buttonReadFile.Click += new System.EventHandler(this.buttonReadFile_Click);
            // 
            // frmYunAsync
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(788, 524);
            this.Controls.Add(this.buttonReadFile);
            this.Controls.Add(this.buttonGenerateNew);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonUpdate);
            this.Controls.Add(this.textBoxFile);
            this.Controls.Add(this.button1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmYunAsync";
            this.Text = "云仓同步";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBoxFile;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ListView listViewHistory1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ListView listViewHistory3;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ColumnHeader columnHeader21;
        private System.Windows.Forms.ColumnHeader columnHeader22;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader23;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem 查看明细ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 查看明细ToolStripMenuItem;
        private System.Windows.Forms.Button buttonGenerateNew;
        private System.Windows.Forms.Button buttonReadFile;
    }
}