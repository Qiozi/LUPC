using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Castle.ActiveRecord;

/// <summary>
/// Summary description for OnSaleModel
/// </summary>
[ActiveRecord("tb_on_sale")]
[Serializable]
public class OnSaleModel:ActiveRecordBase<OnSaleModel>
{
	public OnSaleModel()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    //CREATE TABLE `tb_on_sale` (                    
    //          `serial_no` int(6) NOT NULL auto_increment,  
    //          `product_serial_no` int(11) default NULL,    
    //          `begin_datetime` datetime default NULL,      
    //          `end_datetime` datetime default NULL,        
    //          `save_price` decimal(8,2) default '0.00',    
    //          `price` decimal(8,2) default '0.00',         
    //          `cost` decimal(8,2) default '0.00',          
    //          `sale_price` decimal(8,2) default '0.00',    
    //          `comment` varchar(255) default NULL,         
    //          `modify_datetime` datetime default NULL,     
    //          PRIMARY KEY  (`serial_no`)                   
    //        )
    int _serial_no;
    int _product_serial_no;
    DateTime _begin_datetime;
    DateTime _end_datetime;
    decimal _save_price;
    decimal _price;
    decimal _cost;
    decimal _sale_price;
    string _comment;
    DateTime _modify_datetime;

    [PrimaryKey(PrimaryKeyType.Identity)]
    public int serial_no
    {
        get { return _serial_no; }
        set { _serial_no = value; }
    }
    public static OnSaleModel GetOnSaleModel(int _serial_no)
    {
        OnSaleModel[] models = OnSaleModel.FindAllByProperty("serial_no", _serial_no);
        if (models.Length == 1)
            return models[0];
        else
            return new OnSaleModel();
    }

    [Property]
    public int product_serial_no
    {
        get { return _product_serial_no; }
        set { _product_serial_no = value; }
    }

    [Property]
    public DateTime begin_datetime
    {
        get { return _begin_datetime; }
        set { _begin_datetime = value; }
    }

    [Property]
    public DateTime end_datetime
    {
        get { return _end_datetime; }
        set { _end_datetime = value; }
    }

    
    [Property]
    public decimal save_price
    {
        get { return _save_price; }
        set { _save_price = value; }
    }
    [Property]
    public decimal price
    {
        get { return _price; }
        set { _price = value; }
    }

    [Property]
    public decimal cost
    {
        get { return _cost; }
        set { _cost = value; }
    }

    [Property]
    public decimal sale_price
    {
        get { return _sale_price; }
        set { _sale_price = value; }
    }

    [Property]
    public string comment
    {
        get { return _comment; }
        set { _comment = value; }
    }


    [Property]
    public DateTime modify_datetime
    {
        get { return _modify_datetime; }
        set { _modify_datetime = value; }
    }

    public DataTable FindAllOnSale()
    {
        return Config.ExecuteDataTable(@"select p.product_current_price, p.product_name, p.product_current_cost, pc.menu_child_name, os.*
from tb_on_sale os inner join tb_product p on p.product_serial_no=os.product_serial_no 
inner join tb_product_category pc on p.menu_child_serial_no=pc.menu_child_serial_no where p.tag=1 order by pc.menu_child_order, p.product_order asc ");
    }

    public bool IsExist(int product_serial_no)
    {
        OnSaleModel[] osm = OnSaleModel.FindAllByProperty("product_serial_no", product_serial_no);
        if (osm.Length > 0)
            return true;
        else
            return false;
    }

    public OnSaleModel[] FindModelByProductSerialNo(int product_serial_no)
    {
        return OnSaleModel.FindAllByProperty("product_serial_no", product_serial_no);
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


    public static void SaveDiscountByPartID(int part_id, decimal price, decimal cost, decimal save_cost)
    {
        OnSaleModel[] osm = OnSaleModel.FindAllByProperty("product_serial_no", part_id.ToString());
        for (int i = 0; i < osm.Length; i++)
        {
            osm[i].cost = cost;
            osm[i].price = price;
            osm[i].save_price = save_cost;
            osm[i].sale_price = price - save_cost;
            osm[i].Update();
        }
    }

}
