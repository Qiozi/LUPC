using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class ChangeOnSalePriceToProduct : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dt = Config.ExecuteDataTable(@"select 	current_datetime 
	from 
	tb_execute_day ");
        if(dt.Rows.Count == 1)
        {
            if (DateTime.Parse(dt.Rows[0][0].ToString()).ToString("yyyy-MM-dd") != DateTime.Now.ToString("yyyy-MM-dd"))
            {
                if (Request.QueryString["QioziCommand"].ToString() == "update")
                {
                    //
                    // end adjustment price.
                    //
                    Config.ExecuteNonQuery("update tb_product set adjustment = 0, adjustment_enddate='1971-01-01' where  date_format(adjustment_enddate,'%Y-%m-%d')= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' or date_format(adjustment_enddate,'%Y-%m-%d')= '" + DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + "'");
                    //
                    // change on sale price
                    //
                    Config.ExecuteNonQuery(@"	
    update tb_on_sale set save_price_bak=save_price;
    update tb_product p set product_current_discount=0 where product_serial_no not in (select product_serial_no from tb_on_sale where date_format(end_datetime,'%Y%j')>=date_format(now(),'%Y%j'));
    update tb_product p set product_current_discount=0, product_current_price = (select max(sale_price) from tb_on_sale os where os.product_serial_no=p.product_serial_no)
	where product_serial_no in (
	select product_serial_no from tb_on_sale where date_format(end_datetime,'%Y%j')=date_format(date_sub(current_date, interval 1 day), '%Y%j')
	);
    update tb_product set product_current_special_cash_price=round((product_current_price-product_current_discount)/1.022, 2);
	insert into tb_execute_history 
	(execute_datetime, execute_comment)
	values
	(now(), 'Change On Sale Price To Product DataTable');");

                    //
                    // no change price , end.
                    Config.ExecuteNonQuery("delete from tb_part_not_change_price where date_format(endDate, '%Y%j') < date_format(now(), '%Y%j')");

                    //
                    // etc compare
                    //
                    // EtcLuModel elm = new EtcLuModel();
                    // elm.SetEtcResult();
                    
                    //
                    // cancel New Product Tag.
                    // 
                    Config.ExecuteNonQuery("update tb_product set new=0 where  date_format(now(),'%Y%j')-date_format(regdate,'%Y%j')>15");

                    Config.ExecuteNonQuery("Delete from tb_on_sale where product_serial_no not in (Select product_serial_no from tb_product where product_current_discount >0)");

                    //
                    //
                    //
                    Response.Write("OK-----------" + DateTime.Now.ToString() + "\r\n");
                    Config.ExecuteNonQuery("update tb_execute_day set current_datetime=now()");
                    //
                    //  generate price file
                    //
                    //  Server.Execute("/generate/generatelist.aspx?cmd=onsale");
                    //return;
                }
            }
        }

        Config.ExecuteNonQuery("Delete from tb_on_sale where product_serial_no not in (Select product_serial_no from tb_product where product_current_discount >0)");

        Response.Write("end<script> this.close();</script>");
    }
}
