// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2008-10-01 15:00:26
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;
using System.Data;


[ActiveRecord("tb_ebay_store")]
[Serializable]
public class EbayStoreModel : ActiveRecordBase<EbayStoreModel>
{
	public EbayStoreModel()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    [PrimaryKey(PrimaryKeyType.Identity)]
    public int id
    {
        get { return _id; }
        set { _id = value; }
    }
    public static EbayStoreModel GetEbayStoreModel(int _id)
    {
        EbayStoreModel[] models = EbayStoreModel.FindAllByProperty("id", _id);
        if (models.Length == 1)
            return models[0];
        else
            return new EbayStoreModel();
    }

    int _id;
    string _ebay_store_comment;
    [Property]
    public string ebay_store_comment
    {
        get { return _ebay_store_comment; }
        set { _ebay_store_comment = value; }
    }
    string _ebay_code;
    [Property]
    public string ebay_code
    {
        get { return _ebay_code; }
        set { _ebay_code = value; }
    }
    decimal _price;
    [Property]
    public decimal price
    {
        get { return _price; }
        set { _price = value; }
    }
    decimal _is_modify_price;
    [Property]
    public decimal is_modify_price
    {
        get { return _is_modify_price; }
        set { _is_modify_price = value; }
    }
    int _ebay_store_type;
    [Property]
    public int ebay_store_type
    {
        get { return _ebay_store_type; }
        set { _ebay_store_type = value; }
    }
    bool _is_templete;
    [Property]
    public bool is_templete
    {
        get { return _is_templete; }
        set { _is_templete = value; }
    }
    DateTime _regdate;
    [Property]
    public DateTime regdate
    {
        get { return _regdate; }
        set { _regdate = value; }
    }
    DateTime _last_regdate;
    [Property]
    public DateTime last_regdate
    {
        get { return _last_regdate; }
        set { _last_regdate = value; }
    }
    int _lu_ebay_sku;
    [Property]
    public int lu_ebay_sku
    {
        get { return _lu_ebay_sku; }
        set { _lu_ebay_sku = value; }
    }


    public DataTable FindModelsBySystem(int pagesize, int startrecord, string keyword, ref int count,int store_type)
    {
        // system 
        if (store_type == 2)
        {
            string sql_search = "";
            if (keyword != string.Empty)
            {
                sql_search += string.Format(" and ( e.ebay_code='{0}' or e.id={0})", keyword);

            }
            count = int.Parse(Config.ExecuteScalar(string.Format(@"select count(e.id)
 from tb_ebay_store e
left join 
(select case when other_product_sku>0 then p.other_product_sku else p.product_serial_no end img_sku,  es.ebay_store_id from tb_ebay_store_detail es inner join tb_product p on p.product_serial_no=es.lu_sku
		inner join tb_product_category pc on pc.menu_child_serial_no=p.menu_child_serial_no 
		inner join (select max(computer_case_category) ccc from tb_computer_case) c on c.ccc=pc.menu_child_serial_no or pc.menu_pre_serial_no=c.ccc
		) img
on img.ebay_store_id=e.id where ebay_store_type=2 {0}", sql_search)).ToString());
            return Config.ExecuteDataTable(string.Format(@"select e.*,ifnull(img.img_sku,999999) img_sku
,pr.price lu_price, pr.cost lu_cost
 from tb_ebay_store e
left join 
(select case when other_product_sku>0 then p.other_product_sku else p.product_serial_no end img_sku,  es.ebay_store_id from tb_ebay_store_detail es inner join tb_product p on p.product_serial_no=es.lu_sku
		inner join tb_product_category pc on pc.menu_child_serial_no=p.menu_child_serial_no 
		inner join (select max(computer_case_category) ccc from tb_computer_case) c on c.ccc=pc.menu_child_serial_no or pc.menu_pre_serial_no=c.ccc
		) img
on img.ebay_store_id=e.id 
inner join (
select distinct ebay_store_id, sum(product_current_price) price, sum(product_current_cost) cost from tb_ebay_store_detail esd inner join tb_product p on p.product_serial_no=esd.lu_sku
group by ebay_store_id
) pr on pr.ebay_store_id=e.id

where ebay_store_type=2 {2} limit {0},{1}", startrecord, pagesize, sql_search));
        }
        else
        {
            string sql_search = "";
            if (keyword != string.Empty)
            {
                sql_search += string.Format(" and ( e.ebay_code='{0}' or e.lu_ebay_sku='{0}' or e.id={0})", keyword);

            }

            // part 
            count = int.Parse(Config.ExecuteScalar(string.Format(@"select count(e.id)
 from tb_ebay_store e
where ebay_store_type=1 {0}", sql_search)).ToString());

            return Config.ExecuteDataTable(string.Format(@"select e.*,case when p.other_product_sku > 0 then p.other_product_sku else p.product_serial_no end as img_sku
,p.product_current_price lu_price, p.product_current_cost lu_cost
 from tb_ebay_store e inner join tb_product p on p.product_serial_no = e.lu_ebay_sku
where ebay_store_type=1 {2} limit {0},{1}", startrecord, pagesize, sql_search));

        }
    }
}
