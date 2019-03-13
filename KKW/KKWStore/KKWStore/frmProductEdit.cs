using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace KKWStore
{
    public partial class frmProductEdit : Form
    {
        decimal oldCost = 0M;
        db.qstoreEntities context = new db.qstoreEntities();
        int ProductId = -1;
        string Code = "";
        db.tb_product PM = null;
        public frmProductEdit(int id, string code)
        {
            ProductId = id;
            Code = code;
            InitializeComponent();
            this.Shown += new EventHandler(frmProductEdit_Shown);
        }

        void frmProductEdit_Shown(object sender, EventArgs e)
        {
            //if (Helper.Config.IsAdmin)
            //{
            //    this.txtCost.Enabled = true;
            //    this.cbIsToAll.Enabled = true;
            //}
            //else
            //{
                this.txtCost.Enabled = false;
                this.cbIsToAll.Enabled = false;
                this.txtCost.Visible = false;
                this.cbIsToAll.Visible = false;
            //}
            if (ProductId == -1)
            {
                this.btn_save.Text = "添加(&A)";
                if (Code != "")
                {
                    this.txt_prod_code.Text = Code;
                    this.txt_prod_code.Enabled = false;
                }
                this.txtCost.Enabled = false;
                this.cbIsToAll.Enabled = false;
            }
            else
            {
                this.btn_save.Text = "修改(&M)";
                PM = context.tb_product.Single(p => p.id.Equals(ProductId));
                BindBrand();
                if (PM != null)
                {
                    this.txt_prod_code.Text = PM.p_code;
                    this.txt_prod_name.Text = PM.p_name;
                    this.txt_taobao_url.Text = PM.p_taobao_url;
                    this.textBox_wholesalerCode.Text = PM.WholesalerCode;
                    this.textBox_wholesalerUrl.Text = PM.WholesalerUrl;
                    this.numericUpDown1.Value = (decimal)PM.WarnQty;
                    this.comboBox1.Text = PM.brand;
                    this.txtCost.Text = PM.p_cost.Value.ToString("0.00");
                    this.cbIsToAll.Checked = true;
                    this.textBoxYunCode.Text = PM.yun_code;
                    oldCost = PM.p_cost.Value;
                }
            }
        }
        void BindBrand()
        {
            comboBox1.Items.Clear();
            List<string> list = db.ProductModel_p.GetBrand();

            for (int i = 0; i < list.Count; i++)
            {
                comboBox1.Items.Add(list[i]);
            }
        }
        bool ValidateCode(string code, int pid)
        {            
            var prod = context.tb_product.Where(p => p.p_code.Equals(code)).ToList();
            if (prod.Count == 0)
            {
                return true;
            }
            else if (prod.Count == 1)
            {
                if (prod[0].id == pid)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        bool ValidateYunCode(string code, int prodId)
        {
            if (string.IsNullOrEmpty(code.Trim()))
            {
                return true;
            }

            var prod = context.tb_product.Where(p => p.yun_code.Equals(code)).ToList();
            if (prod.Count == 0)
            {
                return true;
            }
            else if (prod.Count == 1)
            {
                if (prod[0].id == prodId)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            var pmNew = new db.tb_product();
            if (ProductId > 0)
            {
                pmNew = context.tb_product.Single(p => p.id.Equals(ProductId));
                Code = pmNew.p_code;

                if (Helper.Config.IsAdmin)
                {

                }
            }
            else
            {
                pmNew.p_price = 0M;
                pmNew.p_cost = 0M;
                pmNew.p_cate_id = 0;
            }

            pmNew.p_name = this.txt_prod_name.Text.Trim();
            pmNew.p_code = this.txt_prod_code.Text.Trim();
            pmNew.p_taobao_url = this.txt_taobao_url.Text.Trim();
            pmNew.WholesalerCode = this.textBox_wholesalerCode.Text.Trim();
            pmNew.WholesalerUrl = this.textBox_wholesalerUrl.Text.Trim();
            pmNew.WarnQty = (int)this.numericUpDown1.Value;
            pmNew.brand = comboBox1.Text.Trim();
            pmNew.yun_code = textBoxYunCode.Text.Trim();

            if (!ValidateCode(this.txt_prod_code.Text.Trim(), ProductId))
            {
                MessageBox.Show("编号已存在，请重新输入", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.txt_prod_code.Text = PM.p_code;
                return;
            }

            if(!ValidateYunCode(textBoxYunCode.Text, ProductId))
            {
                MessageBox.Show("云仓编号已存在，请重新输入", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //this.textBoxYunCode.Text = PM.yun_code;
                this.textBoxYunCode.Focus();
                return;
            }

            if (ProductId < 1)
            {
                pmNew.showit = true;
                pmNew.regdate = DateTime.Now;
                context.tb_product.Add(pmNew);
                context.SaveChanges();
            }
            else
            {
                context.SaveChanges();
                if (pmNew.p_code != Code)
                {
                    db.ProductModel_p.ChangeProductCode(pmNew.id, pmNew.p_code);
                }
            }

            this.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.Close();
        }

        private void txt_prod_code_TextChanged(object sender, EventArgs e)
        {
            //TextBox TB = (TextBox)sender;
            //string text = TB.Text;
            //TB.Text = Helper.CharacterHelper.ToDBC(text);
        }

        private void txt_prod_name_TextChanged(object sender, EventArgs e)
        {
            TextBox TB = (TextBox)sender;
            string text = TB.Text;
            TB.Text = Helper.CharacterHelper.ToDBC(text);
        }
    }
}
