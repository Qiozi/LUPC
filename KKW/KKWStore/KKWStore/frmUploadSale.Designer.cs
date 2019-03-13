namespace KKWStore
{
    partial class frmUploadSale
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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.buttonStoreName = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.numericUpDownTianMao = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownCDian = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownJinDong = new System.Windows.Forms.NumericUpDown();
            this.btnSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.button6 = new System.Windows.Forms.Button();
            this.numericUpDownOther1 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownReturn = new System.Windows.Forms.NumericUpDown();
            this.textBoxOther1 = new System.Windows.Forms.TextBox();
            this.textBoxOther2 = new System.Windows.Forms.TextBox();
            this.numericUpDownOther2 = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTianMao)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCDian)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownJinDong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOther1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownReturn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOther2)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(12, 25);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(150, 21);
            this.comboBox1.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(168, 25);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(332, 20);
            this.textBox1.TabIndex = 1;
            this.textBox1.Click += new System.EventHandler(this.textBox1_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(425, 51);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "上传";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 80);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 20;
            this.dataGridView1.Size = new System.Drawing.Size(488, 150);
            this.dataGridView1.TabIndex = 3;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // buttonStoreName
            // 
            this.buttonStoreName.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonStoreName.Location = new System.Drawing.Point(12, 51);
            this.buttonStoreName.Name = "buttonStoreName";
            this.buttonStoreName.Size = new System.Drawing.Size(75, 23);
            this.buttonStoreName.TabIndex = 4;
            this.buttonStoreName.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Location = new System.Drawing.Point(12, 247);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(115, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "天猫";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button3.Location = new System.Drawing.Point(12, 276);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(115, 23);
            this.button3.TabIndex = 6;
            this.button3.Text = "C店";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button4.Location = new System.Drawing.Point(12, 305);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(115, 23);
            this.button4.TabIndex = 7;
            this.button4.Text = "京东";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // numericUpDownTianMao
            // 
            this.numericUpDownTianMao.DecimalPlaces = 2;
            this.numericUpDownTianMao.Location = new System.Drawing.Point(133, 247);
            this.numericUpDownTianMao.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDownTianMao.Name = "numericUpDownTianMao";
            this.numericUpDownTianMao.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownTianMao.TabIndex = 8;
            // 
            // numericUpDownCDian
            // 
            this.numericUpDownCDian.DecimalPlaces = 2;
            this.numericUpDownCDian.Location = new System.Drawing.Point(133, 276);
            this.numericUpDownCDian.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDownCDian.Name = "numericUpDownCDian";
            this.numericUpDownCDian.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownCDian.TabIndex = 9;
            // 
            // numericUpDownJinDong
            // 
            this.numericUpDownJinDong.DecimalPlaces = 2;
            this.numericUpDownJinDong.Location = new System.Drawing.Point(133, 305);
            this.numericUpDownJinDong.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDownJinDong.Name = "numericUpDownJinDong";
            this.numericUpDownJinDong.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownJinDong.TabIndex = 10;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(425, 244);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 178);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(12, 436);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "label1";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(259, 276);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(160, 20);
            this.dateTimePicker1.TabIndex = 13;
            // 
            // button6
            // 
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button6.Location = new System.Drawing.Point(12, 402);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(115, 23);
            this.button6.TabIndex = 15;
            this.button6.Text = "退款";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // numericUpDownOther1
            // 
            this.numericUpDownOther1.DecimalPlaces = 2;
            this.numericUpDownOther1.Location = new System.Drawing.Point(133, 334);
            this.numericUpDownOther1.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDownOther1.Name = "numericUpDownOther1";
            this.numericUpDownOther1.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownOther1.TabIndex = 16;
            // 
            // numericUpDownReturn
            // 
            this.numericUpDownReturn.DecimalPlaces = 2;
            this.numericUpDownReturn.Location = new System.Drawing.Point(133, 402);
            this.numericUpDownReturn.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDownReturn.Name = "numericUpDownReturn";
            this.numericUpDownReturn.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownReturn.TabIndex = 17;
            // 
            // textBoxOther1
            // 
            this.textBoxOther1.Location = new System.Drawing.Point(12, 334);
            this.textBoxOther1.Name = "textBoxOther1";
            this.textBoxOther1.Size = new System.Drawing.Size(115, 20);
            this.textBoxOther1.TabIndex = 18;
            // 
            // textBoxOther2
            // 
            this.textBoxOther2.Location = new System.Drawing.Point(12, 360);
            this.textBoxOther2.Name = "textBoxOther2";
            this.textBoxOther2.Size = new System.Drawing.Size(115, 20);
            this.textBoxOther2.TabIndex = 19;
            // 
            // numericUpDownOther2
            // 
            this.numericUpDownOther2.DecimalPlaces = 2;
            this.numericUpDownOther2.Location = new System.Drawing.Point(133, 361);
            this.numericUpDownOther2.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDownOther2.Name = "numericUpDownOther2";
            this.numericUpDownOther2.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownOther2.TabIndex = 20;
            // 
            // frmUploadSale
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 490);
            this.Controls.Add(this.numericUpDownOther2);
            this.Controls.Add(this.textBoxOther2);
            this.Controls.Add(this.textBoxOther1);
            this.Controls.Add(this.numericUpDownReturn);
            this.Controls.Add(this.numericUpDownOther1);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.numericUpDownJinDong);
            this.Controls.Add(this.numericUpDownCDian);
            this.Controls.Add(this.numericUpDownTianMao);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.buttonStoreName);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.comboBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmUploadSale";
            this.Text = "上传销售金额";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTianMao)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCDian)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownJinDong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOther1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownReturn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOther2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button buttonStoreName;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.NumericUpDown numericUpDownTianMao;
        private System.Windows.Forms.NumericUpDown numericUpDownCDian;
        private System.Windows.Forms.NumericUpDown numericUpDownJinDong;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.NumericUpDown numericUpDownOther1;
        private System.Windows.Forms.NumericUpDown numericUpDownReturn;
        private System.Windows.Forms.TextBox textBoxOther1;
        private System.Windows.Forms.TextBox textBoxOther2;
        private System.Windows.Forms.NumericUpDown numericUpDownOther2;
    }
}