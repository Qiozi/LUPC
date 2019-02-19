using LUComputers.DBProvider;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LUComputers
{
    public partial class addProduct : Form
    {
        tb_product _pm;
        int _ltd_id;

        public addProduct(tb_product pm, int ltdId)
        {
            _pm = pm;
            _ltd_id = ltdId;

            InitializeComponent();

            this.Shown += new EventHandler(addProduct_Shown);
        }

        void addProduct_Shown(object sender, EventArgs e)
        {
            this.textBox_long_name.Text = _pm.product_name_long_en.ToLower().IndexOf(_pm.producter_serial_no.ToLower()) == -1 ? 
                string.Concat(_pm.producter_serial_no, " ", _pm.product_name_long_en) : _pm.product_name_long_en;

            this.textBox_middle_name.Text = _pm.product_name;
            this.textBox_short_name.Text = _pm.product_short_name;
            this.label_cost.Text = _pm.product_current_cost.ToString();
            this.textBox_MFP.Text = _pm.manufacturer_part_number;
            this.label_upc.Text = _pm.UPC;
            this.label_weight.Text = _pm.weight.ToString();

            comboBoxPartSize.DataSource = DBProvider.C.GetPartSize();
            comboBoxPartSize.DisplayMember = "c";
            comboBoxPartSize.ValueMember = "product_size_id";

        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {

            DataTable o = Config.RemoteExecuteDateTable(string.Format(@"
            Insert into tb_product (
            menu_child_serial_no
            ,product_short_name
            ,product_name
            ,product_img_sum
            ,product_name_long_en
            ,is_non
            ,manufacturer_part_number
            ,producter_serial_no
            ,new
            ,other_product_sku
            ,export
            ,product_current_cost
            ,product_current_price
            ,product_current_special_cash_price
            ,split_line
            ,tag
            ,issue
            ,product_size_id
            ,keywords
            ,supplier_sku
            ,last_regdate
            ,regdate
            ,UPC
            ,weight
            )
            values
            ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}',now(),now(), '{20}','{21}');
            
            select last_insert_id();"
                , _pm.menu_child_serial_no
                , textBox_short_name.Text.Trim()
                , textBox_middle_name.Text.Trim()
                , 1
                , textBox_long_name.Text.Trim()
                , 0
                , _pm.manufacturer_part_number
                , _pm.producter_serial_no
                , 1
                , 999999
                , 1
                , _pm.product_current_cost
                , _pm.product_current_price
                , _pm.product_current_special_cash_price
                , 0
                , 1
                , 0
                , comboBoxPartSize.SelectedValue.ToString()
                , "[" + _pm.producter_serial_no + "]"
                , _pm.supplier_sku
                 , _pm.UPC
                 , _pm.weight
                ));

            int luc_sku;
            int.TryParse(o.Rows[0][0].ToString(), out luc_sku);


            Config.RemoteExecuteNonQuery(string.Format(@"insert into tb_other_inc_match_lu_sku 
            	( lu_sku, other_inc_sku, other_inc_type
            	)
            	values
            	( '{0}', '{1}', '{2}'
            	)"
                , luc_sku, _pm.supplier_sku, _ltd_id));

            this.DialogResult = System.Windows.Forms.DialogResult.Yes;
            MessageBox.Show(luc_sku.ToString());
            this.Close();
        }
    }
}
