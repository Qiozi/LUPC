using LUComputers.DBProvider;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LUComputers
{
    public partial class EditPriceToRemote : Form
    {
        int _luc_sku = 0;
        Ltd _ltd ;
        decimal _new_cost;
        public EditPriceToRemote(int luc_sku, Ltd ltd, decimal new_cost)
        {
            InitializeComponent();

            _luc_sku = luc_sku;
            _ltd = ltd;
            _new_cost = new_cost;

            WinLoad(true);
        }

        public EditPriceToRemote() { }

        public void WinLoad(bool view_other_inc)
        {
            if (_luc_sku > 0)
            {
                DataTable dt = Config.RemoteExecuteDateTable(string.Format(@"select 
manufacturer_part_number
,product_current_price
,product_current_cost
,product_current_discount
,product_current_price - product_current_discount sold
,case when product_name_long_en<>'' then product_name_long_en
else product_name end as product_name   from tb_product where product_serial_no='{0}'", _luc_sku));

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];

                    string table_name = new LtdHelper().GetLastStoreTableName(_ltd);

                  
                        this.label_mfp.Text = dr["manufacturer_part_number"].ToString();
                        this.label_name.Text = dr["product_name"].ToString();
                        this.label_sku.Text = _luc_sku.ToString();

                        this.textBox_old_price.Text = dr["product_current_price"].ToString();
                        this.textBox_old_sold.Text = dr["sold"].ToString();
                        this.textBox_old_discount.Text = dr["product_current_discount"].ToString();
                        this.textBox_old_cost.Text = dr["product_current_cost"].ToString();


                        this.textBox_new_cost.Text = _new_cost.ToString();

                        decimal new_special_price = GetNewSpecial(_new_cost,_luc_sku,0 );
                        //if (_new_cost <= 50)

                        //    new_special_price = _new_cost + 5;
                        //else if (_new_cost > 50 && _new_cost < 100)
                        //    new_special_price = _new_cost + 8;
                        //else if (_new_cost >= 100 && _new_cost < 150)
                        //    new_special_price = _new_cost + 9;
                        //else if (_new_cost > 150 && _new_cost < 200)
                        //    new_special_price = _new_cost + 10;
                        //else if(_new_cost >=200 && _new_cost < 2500)
                        //    new_special_price = _new_cost * 1.08M;
                        //else
                        //    new_special_price = _new_cost * 1.09M; 

                        this.textBox_new_special.Text = new_special_price.ToString("###.00");

                        decimal new_price = GetNewPrice(new_special_price);// decimal.Parse((new_special_price * 1.022M).ToString("###")) - 0.01M;

                        this.textBox_new_price.Text = new_price.ToString("###.00");

                        this.label_special_cost.Text = (new_special_price - _new_cost).ToString("###.00");
                        if (this.textBox_new_price.Text == this.textBox_old_price.Text)
                        {
                            this.button1.Text = "OK";
                           
                        }
                        if (view_other_inc)
                        {
                            dataGridView1.DataSource = Config.RemoteExecuteDateTable(string.Format(@"select other_inc_name Name, other_inc_store_sum quantity, other_inc_price price from tb_other_inc_part_info oi 
left join tb_other_inc c on c.id=oi.other_inc_id
 where oi.luc_sku='{0}' order by other_inc_price asc ", _luc_sku));
                            PriceComp(new_special_price);
                        }


                } 
            }
        }

        public void PriceComp(decimal special)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                decimal price;
                decimal.TryParse(dataGridView1.Rows[i].Cells["price"].ToString(), out price);

                if (price > special)
                    dataGridView1.Rows[i].Cells["price"].Style.ForeColor = System.Drawing.Color.Red;
            }
            dataGridView1.Update();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            decimal new_price;
            decimal.TryParse(this.textBox_new_price.Text, out new_price);

            decimal new_cost;
            decimal.TryParse(this.textBox_new_cost.Text, out new_cost);

            decimal special;
            decimal.TryParse(this.textBox_new_special.Text, out special);

            ExecRemote(new_price, new_cost, special, _luc_sku);
            this.Close();
            //this.WinLoad(false);

        }

        #region Methods

        public static void ExecRemote(decimal new_price, decimal new_cost, decimal special, int lu_sku)
        {
            Config.RemoteExecuteNonQuery(string.Format(@"Update tb_product set product_current_price='{0}', product_current_cost='{1}', product_current_special_cash_price = round({0}/1.022, 2), last_regdate=now() where product_serial_no='{2}'", new_price, new_cost, lu_sku));

        }


        public static decimal GetNewSpecial(decimal new_cost, int luc_sku, int categoryID)
        {
            decimal new_special_price;
            //if (new_cost < 20)
            //    new_special_price = new_cost + 5;
            //else if (new_cost >= 20 && new_cost < 30)
            //    new_special_price = new_cost * (1M + 0.15M);
            //else if (new_cost >= 30 && new_cost < 60)
            //    new_special_price = new_cost * (1M + 0.12M);
            //else if (new_cost >= 60 && new_cost < 90)
            //    new_special_price = new_cost * (1M + 0.11M);
            //else if (new_cost >= 90 && new_cost < 150)
            //    new_special_price = new_cost * (1M + 0.10M);
            //else if (new_cost >= 150 && new_cost < 200)
            //    new_special_price = new_cost * (1M + 0.09M);
            //else if (new_cost >= 200 && new_cost < 500)
            //    new_special_price = new_cost * (1M + 0.08M);
            //else if (new_cost >= 500 && new_cost < 1000)
            //    new_special_price = new_cost * (1M + 0.08M);
            //else if (new_cost >= 1000 && new_cost < 1500)
            //    new_special_price = new_cost * (1M + 0.08M);
            //else if (new_cost >= 1500 && new_cost < 2500)
            //    new_special_price = new_cost * (1M + 0.08M);
            //else if (new_cost >= 2500 && new_cost < 9999999)
            //    new_special_price = new_cost * (1M + 0.08M);
            //else
            //    new_special_price = new_cost * 1.09M;

            DataTable dt ;
            if (luc_sku > 0)
            {
                dt = Config.ExecuteDateTable(string.Format(@"select pp.*, pc.adjustment from tb_part_price_change_setting pp 
inner join tb_other_inc_valid_lu_sku  pc
on pc.menu_child_serial_no=pp.category_id where pc.lu_sku='{0}'
 and ({1}+pc.adjustment)>cost_min and ({1}+pc.adjustment)<=cost_max"
                    , luc_sku
                    , new_cost));
            }
            else
            {
                dt = Config.ExecuteDateTable(string.Format(@"select pp.*, '0' adjustment from tb_part_price_change_setting pp 
 where pp.category_id='{0}'
 and ({1})>cost_min and ({1})<=cost_max"
                    , categoryID
                    , new_cost));
            }
            if (dt.Rows.Count < 1)
                throw new ArgumentNullException("Part Price Change Settings isn't find.");
            else
            {
                decimal adjustment;
                decimal.TryParse(dt.Rows[0]["adjustment"].ToString(), out adjustment);

                int is_percent = dt.Rows[0]["is_percent"].ToString().ToLower()=="false"?0:1;
                if (is_percent==1)
                {
                    new_special_price = (new_cost+ adjustment) * decimal.Parse(dt.Rows[0]["rate"].ToString()) / 100M;
                }
                else
                {
                    new_special_price = (new_cost + adjustment) + decimal.Parse(dt.Rows[0]["rate"].ToString());
                }
            }
  
            return new_special_price;
        }

        public static decimal GetNewPrice(decimal new_special)
        {
            return decimal.Parse((new_special * 1.022M).ToString("###")) - 0.01M;
        }
        #endregion

        private void EditPriceToRemote_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)27)
            {
                this.Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="current_price"></param>
        /// <param name="rate"></param>
        /// <returns></returns>
        public static decimal ChangePriceToNotCard(decimal current_price, decimal rate)
        {
            if (current_price == 0M)
                return 0M;
            return decimal.Parse((current_price / rate).ToString("###.00"));
        }
    }
}
