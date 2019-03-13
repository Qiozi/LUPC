namespace KKWStore
{
    partial class frmStockTakeClear
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
            this.button_store = new System.Windows.Forms.Button();
            this.txtFolderPath = new System.Windows.Forms.TextBox();
            this.btnBrowser = new System.Windows.Forms.Button();
            this.txtResultNormal = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSureSave = new System.Windows.Forms.Button();
            this.btnReadFile = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.txtResultNoInStore = new System.Windows.Forms.TextBox();
            this.txtResultOutStore = new System.Windows.Forms.TextBox();
            this.txtResultNoExist = new System.Windows.Forms.TextBox();
            this.label_normal = new System.Windows.Forms.Label();
            this.label_no_in_store = new System.Windows.Forms.Label();
            this.label_out_store = new System.Windows.Forms.Label();
            this.label_no_exist = new System.Windows.Forms.Label();
            this.label_allSNCount = new System.Windows.Forms.Label();
            this.label_no_use = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button_store
            // 
            this.button_store.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_store.Location = new System.Drawing.Point(13, 14);
            this.button_store.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_store.Name = "button_store";
            this.button_store.Size = new System.Drawing.Size(153, 33);
            this.button_store.TabIndex = 5;
            this.button_store.Text = "文件夹所在路径：";
            this.button_store.UseVisualStyleBackColor = true;
            // 
            // txtFolderPath
            // 
            this.txtFolderPath.Location = new System.Drawing.Point(173, 17);
            this.txtFolderPath.Name = "txtFolderPath";
            this.txtFolderPath.Size = new System.Drawing.Size(387, 26);
            this.txtFolderPath.TabIndex = 6;
            // 
            // btnBrowser
            // 
            this.btnBrowser.Location = new System.Drawing.Point(566, 14);
            this.btnBrowser.Name = "btnBrowser";
            this.btnBrowser.Size = new System.Drawing.Size(75, 33);
            this.btnBrowser.TabIndex = 7;
            this.btnBrowser.Text = "浏览";
            this.btnBrowser.UseVisualStyleBackColor = true;
            this.btnBrowser.Click += new System.EventHandler(this.btnBrowser_Click);
            // 
            // txtResultNormal
            // 
            this.txtResultNormal.Location = new System.Drawing.Point(13, 102);
            this.txtResultNormal.Multiline = true;
            this.txtResultNormal.Name = "txtResultNormal";
            this.txtResultNormal.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResultNormal.Size = new System.Drawing.Size(322, 521);
            this.txtResultNormal.TabIndex = 8;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(1246, 639);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 33);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSureSave
            // 
            this.btnSureSave.Enabled = false;
            this.btnSureSave.Location = new System.Drawing.Point(1165, 639);
            this.btnSureSave.Name = "btnSureSave";
            this.btnSureSave.Size = new System.Drawing.Size(75, 33);
            this.btnSureSave.TabIndex = 10;
            this.btnSureSave.Text = "确认库存";
            this.btnSureSave.UseVisualStyleBackColor = true;
            this.btnSureSave.Click += new System.EventHandler(this.btnSureSave_Click);
            // 
            // btnReadFile
            // 
            this.btnReadFile.Location = new System.Drawing.Point(647, 14);
            this.btnReadFile.Name = "btnReadFile";
            this.btnReadFile.Size = new System.Drawing.Size(108, 33);
            this.btnReadFile.TabIndex = 10;
            this.btnReadFile.Text = "分析文件";
            this.btnReadFile.UseVisualStyleBackColor = true;
            this.btnReadFile.Click += new System.EventHandler(this.btnReadFile_Click);
            // 
            // txtResultNoInStore
            // 
            this.txtResultNoInStore.Location = new System.Drawing.Point(341, 102);
            this.txtResultNoInStore.Multiline = true;
            this.txtResultNoInStore.Name = "txtResultNoInStore";
            this.txtResultNoInStore.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResultNoInStore.Size = new System.Drawing.Size(322, 521);
            this.txtResultNoInStore.TabIndex = 11;
            // 
            // txtResultOutStore
            // 
            this.txtResultOutStore.Location = new System.Drawing.Point(669, 102);
            this.txtResultOutStore.Multiline = true;
            this.txtResultOutStore.Name = "txtResultOutStore";
            this.txtResultOutStore.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResultOutStore.Size = new System.Drawing.Size(322, 521);
            this.txtResultOutStore.TabIndex = 12;
            // 
            // txtResultNoExist
            // 
            this.txtResultNoExist.Location = new System.Drawing.Point(997, 102);
            this.txtResultNoExist.Multiline = true;
            this.txtResultNoExist.Name = "txtResultNoExist";
            this.txtResultNoExist.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResultNoExist.Size = new System.Drawing.Size(322, 521);
            this.txtResultNoExist.TabIndex = 13;
            // 
            // label_normal
            // 
            this.label_normal.AutoSize = true;
            this.label_normal.Location = new System.Drawing.Point(12, 79);
            this.label_normal.Name = "label_normal";
            this.label_normal.Size = new System.Drawing.Size(67, 20);
            this.label_normal.TabIndex = 14;
            this.label_normal.Text = " 正常SN";
            // 
            // label_no_in_store
            // 
            this.label_no_in_store.AutoSize = true;
            this.label_no_in_store.Location = new System.Drawing.Point(337, 79);
            this.label_no_in_store.Name = "label_no_in_store";
            this.label_no_in_store.Size = new System.Drawing.Size(67, 20);
            this.label_no_in_store.TabIndex = 15;
            this.label_no_in_store.Text = " 正常SN";
            // 
            // label_out_store
            // 
            this.label_out_store.AutoSize = true;
            this.label_out_store.Location = new System.Drawing.Point(665, 79);
            this.label_out_store.Name = "label_out_store";
            this.label_out_store.Size = new System.Drawing.Size(67, 20);
            this.label_out_store.TabIndex = 16;
            this.label_out_store.Text = " 正常SN";
            // 
            // label_no_exist
            // 
            this.label_no_exist.AutoSize = true;
            this.label_no_exist.Location = new System.Drawing.Point(993, 79);
            this.label_no_exist.Name = "label_no_exist";
            this.label_no_exist.Size = new System.Drawing.Size(67, 20);
            this.label_no_exist.TabIndex = 17;
            this.label_no_exist.Text = " 正常SN";
            // 
            // label_allSNCount
            // 
            this.label_allSNCount.AutoSize = true;
            this.label_allSNCount.Location = new System.Drawing.Point(12, 645);
            this.label_allSNCount.Name = "label_allSNCount";
            this.label_allSNCount.Size = new System.Drawing.Size(67, 20);
            this.label_allSNCount.TabIndex = 18;
            this.label_allSNCount.Text = " 正常SN";
            // 
            // label_no_use
            // 
            this.label_no_use.AutoSize = true;
            this.label_no_use.Location = new System.Drawing.Point(337, 645);
            this.label_no_use.Name = "label_no_use";
            this.label_no_use.Size = new System.Drawing.Size(67, 20);
            this.label_no_use.TabIndex = 19;
            this.label_no_use.Text = " 正常SN";
            // 
            // frmStockTakeClear
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1337, 697);
            this.Controls.Add(this.label_no_use);
            this.Controls.Add(this.label_allSNCount);
            this.Controls.Add(this.label_no_exist);
            this.Controls.Add(this.label_out_store);
            this.Controls.Add(this.label_no_in_store);
            this.Controls.Add(this.label_normal);
            this.Controls.Add(this.txtResultNoExist);
            this.Controls.Add(this.txtResultOutStore);
            this.Controls.Add(this.txtResultNoInStore);
            this.Controls.Add(this.btnReadFile);
            this.Controls.Add(this.btnSureSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtResultNormal);
            this.Controls.Add(this.btnBrowser);
            this.Controls.Add(this.txtFolderPath);
            this.Controls.Add(this.button_store);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmStockTakeClear";
            this.Text = "盘点（excel）";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_store;
        private System.Windows.Forms.TextBox txtFolderPath;
        private System.Windows.Forms.Button btnBrowser;
        private System.Windows.Forms.TextBox txtResultNormal;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSureSave;
        private System.Windows.Forms.Button btnReadFile;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TextBox txtResultNoInStore;
        private System.Windows.Forms.TextBox txtResultOutStore;
        private System.Windows.Forms.TextBox txtResultNoExist;
        private System.Windows.Forms.Label label_normal;
        private System.Windows.Forms.Label label_no_in_store;
        private System.Windows.Forms.Label label_out_store;
        private System.Windows.Forms.Label label_no_exist;
        private System.Windows.Forms.Label label_allSNCount;
        private System.Windows.Forms.Label label_no_use;
    }
}