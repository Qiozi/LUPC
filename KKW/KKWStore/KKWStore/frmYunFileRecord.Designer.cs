﻿namespace KKWStore
{
    partial class frmYunFileRecord
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
            this.listViewHistory2 = new System.Windows.Forms.ListView();
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader15 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader16 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader17 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader18 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader19 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // listViewHistory2
            // 
            this.listViewHistory2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader13,
            this.columnHeader14,
            this.columnHeader15,
            this.columnHeader16,
            this.columnHeader17,
            this.columnHeader18,
            this.columnHeader19});
            this.listViewHistory2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewHistory2.GridLines = true;
            this.listViewHistory2.Location = new System.Drawing.Point(0, 0);
            this.listViewHistory2.Name = "listViewHistory2";
            this.listViewHistory2.Size = new System.Drawing.Size(740, 487);
            this.listViewHistory2.TabIndex = 3;
            this.listViewHistory2.UseCompatibleStateImageBehavior = false;
            this.listViewHistory2.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "商品编码";
            this.columnHeader13.Width = 124;
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "商品名称";
            this.columnHeader14.Width = 77;
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "商品条码";
            this.columnHeader15.Width = 104;
            // 
            // columnHeader16
            // 
            this.columnHeader16.Text = "商品规格";
            this.columnHeader16.Width = 111;
            // 
            // columnHeader17
            // 
            this.columnHeader17.Text = "总量";
            // 
            // columnHeader18
            // 
            this.columnHeader18.Text = "订单占用量";
            this.columnHeader18.Width = 85;
            // 
            // columnHeader19
            // 
            this.columnHeader19.Text = "可用量";
            // 
            // frmYunFileRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(740, 487);
            this.Controls.Add(this.listViewHistory2);
            this.Name = "frmYunFileRecord";
            this.Text = "云仓库存 文件纪录";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewHistory2;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.ColumnHeader columnHeader15;
        private System.Windows.Forms.ColumnHeader columnHeader16;
        private System.Windows.Forms.ColumnHeader columnHeader17;
        private System.Windows.Forms.ColumnHeader columnHeader18;
        private System.Windows.Forms.ColumnHeader columnHeader19;
    }
}