using LU.Data;
using System;
using System.Data;
using System.Linq;

/// <summary>
/// Summary description for OnSaleModel
/// </summary>
[Serializable]
public class OnSaleModel
{

    public static tb_on_sale GetOnSaleModel(nicklu2Entities context, int id)
    {
        return context.tb_on_sale.Single(me => me.serial_no.Equals(id));
    }

    public DataTable FindAllOnSale()
    {
        return Config.ExecuteDataTable(@"select p.product_current_price, p.product_name, p.product_current_cost, pc.menu_child_name, os.*
from tb_on_sale os inner join tb_product p on p.product_serial_no=os.product_serial_no 
inner join tb_product_category pc on p.menu_child_serial_no=pc.menu_child_serial_no where p.tag=1 order by pc.menu_child_order, p.product_order asc ");
    }

    public bool IsExist(nicklu2Entities context, int product_serial_no)
    {
        //var osm = OnSaleModel.FindAllByProperty("product_serial_no", product_serial_no);
        return context.tb_on_sale.Count(me => me.product_serial_no.Value.Equals(product_serial_no)) > 0;
        //    (
        //if (osm.Length > 0)
        //    return true;
        //else
        //    return false;
    }

    public tb_on_sale[] FindModelByProductSerialNo(nicklu2Entities context, int product_serial_no)
    {
        //return OnSaleModel.FindAllByProperty("product_serial_no", product_serial_no);
        return context.tb_on_sale.Where(me => me.product_serial_no.Equals(product_serial_no)).ToList().ToArray();
    }

    public DataTable FindYestodayAndTodaySum()
    {
        return Config.ExecuteDataTable(@"
select * from (
select count(serial_no) yestoday from tb_on_sale 
where date_format(end_datetime,'%Y%j') =date_format(date_sub(current_date, interval 1 day), '%Y%j') 
) y1 , (
select count(serial_no) today from tb_on_sale 
where date_format(end_datetime,'%Y%j') =date_format(date_sub(current_date, interval 0 day), '%Y%j') ) t1");
    }

    public static void ChangeOldPrice(string begin_datetime, string end_datetime)
    {
        Config.ExecuteNonQuery(@"
update tb_product p 
	set 
		product_current_discount= (select max(save_price_bak) from tb_on_sale os where os.product_serial_no=p.product_serial_no)
		,product_current_price = (select max(sale_price+save_price_bak) from tb_on_sale os where os.product_serial_no=p.product_serial_no)
		,Is_Modify=1
			where product_serial_no in (select product_serial_no from tb_on_sale where date_format( end_datetime,'%y%j') < date_format(now(),'%y%j')
								);
update tb_on_sale set begin_datetime='" + begin_datetime.ToString() + "',end_datetime='" + end_datetime.ToString() + @"' where date_format( end_datetime,'%y%j') < date_format(now(),'%y%j');
");
    }


    public static void SaveDiscountByPartID(nicklu2Entities context, int part_id, decimal price, decimal cost, decimal save_cost)
    {
        //OnSaleModel[] osm = OnSaleModel.FindAllByProperty("product_serial_no", part_id.ToString());
        //for (int i = 0; i < osm.Length; i++)
        //{
        //    osm[i].cost = cost;
        //    osm[i].price = price;
        //    osm[i].save_price = save_cost;
        //    osm[i].sale_price = price - save_cost;
        //    osm[i].Update();
        //}

        var query = context.tb_on_sale.Where(me => me.product_serial_no.Value.Equals(part_id)).ToList();
        foreach (var item in query)
        {
            item.cost = cost;
            item.price = price;
            item.sale_price = save_cost;
            item.sale_price = price - save_cost;
        }
        context.SaveChanges();
    }

}
