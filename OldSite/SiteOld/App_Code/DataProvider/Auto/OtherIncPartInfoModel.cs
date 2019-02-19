
// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	11/26/2008 1:34:17 PM
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;
using System.Data;

[ActiveRecord("tb_other_inc_part_info")]
[Serializable]
public class OtherIncPartInfoModel : ActiveRecordBase<OtherIncPartInfoModel>
{

    public OtherIncPartInfoModel()
    {

    }

    public static OtherIncPartInfoModel GetOtherIncPartInfoModel(int _id)
    {
        OtherIncPartInfoModel[] models = OtherIncPartInfoModel.FindAllByProperty("id", _id);
        if (models.Length == 1)
            return models[0];
        else
            return new OtherIncPartInfoModel();
    }

    int _id;

    [PrimaryKey(PrimaryKeyType.Identity)]
    public int id
    {
        get { return _id; }
        set { _id = value; }
    } 
    int _other_inc_id;

    [Property]
    public int other_inc_id
    {
        get { return _other_inc_id; }
        set { _other_inc_id = value; }
    }

    string _other_inc_sku;

    [Property]
    public string other_inc_sku
    {
        get { return _other_inc_sku; }
        set { _other_inc_sku = value; }
    }

    string _manufacture_part_number;

    [Property]
    public string manufacture_part_number
    {
        get { return _manufacture_part_number; }
        set { _manufacture_part_number = value; }
    }

    decimal _other_inc_price;

    [Property]
    public decimal other_inc_price
    {
        get { return _other_inc_price; }
        set { _other_inc_price = value; }
    }

    int _other_inc_store_sum;

    [Property]
    public int other_inc_store_sum
    {
        get { return _other_inc_store_sum; }
        set { _other_inc_store_sum = value; }
    }

    bool _tag;
    [Property]
    public bool tag
    {
        get { return _tag; }
        set { _tag = value; }
    }


    DateTime _last_regdate;
    [Property]
    public DateTime last_regdate
    {
        get { return _last_regdate; }
        set { _last_regdate = value; }
    }

    int _luc_sku;
    [Property]
    public int luc_sku
    {
        get { return _luc_sku; }
        set { _luc_sku = value; }
    }


    public static OtherIncPartInfoModel[] FindBySKUAndLtd(string sku, int ltd)
    {
        NHibernate.Expression.EqExpression eq1 = new NHibernate.Expression.EqExpression("other_inc_sku", sku);
        NHibernate.Expression.EqExpression eq2 = new NHibernate.Expression.EqExpression("other_inc_id", ltd);
        NHibernate.Expression.AndExpression a = new NHibernate.Expression.AndExpression(eq1, eq2);
        OtherIncPartInfoModel[] psss = OtherIncPartInfoModel.FindAll(a);
        return psss;
    }

    public void RunAmount()
    {
        LtdHelper LH = new LtdHelper();
        DataTable wholesalerDT = LH.LtdHelperWholesalerToDT();
        DataTable RivalDT = LH.LtdHelperRivalToDT();

        Config.ExecuteNonQuery(@"delete from tb_other_inc_match_lu_sku where other_inc_type not in (select id from tb_other_inc where id <> 50);
delete from tb_other_inc_part_info where other_inc_id not in (select id from tb_other_inc where id <> 50);");

        for (int i = 0; i < wholesalerDT.Rows.Count; i++)
        {
            Config.ExecuteNonQuery(string.Format(@"
update tb_other_inc set
inc_record=(select count(id) c from tb_other_inc_part_info where other_inc_id='{0}')

,inc_record_valid=(select count(id) c from tb_other_inc_part_info where tag=1 and other_inc_id='{0}')

,inc_record_match=(select count(id) c from tb_other_inc_match_lu_sku ol inner join tb_other_inc_part_info oi
 on oi.other_inc_sku=ol.other_inc_sku and ol.other_inc_type=oi.other_inc_id and other_inc_id='{0}')

,bigger_than_lu=(select count(ol.id) from tb_other_inc_match_lu_sku ol inner join tb_other_inc_part_info oi
 on oi.other_inc_sku=ol.other_inc_sku 
 inner join tb_product p on p.product_serial_no=ol.lu_sku and ol.other_inc_type=oi.other_inc_id where other_inc_id='{0}' and p.product_current_cost < oi.other_inc_price)

,less_than_lu=(select count(ol.id) from tb_other_inc_match_lu_sku ol inner join tb_other_inc_part_info oi
 on oi.other_inc_sku=ol.other_inc_sku 
 inner join tb_product p on p.product_serial_no=ol.lu_sku and ol.other_inc_type=oi.other_inc_id where other_inc_id='{0}' and p.product_current_cost > oi.other_inc_price)

,equal_than_lu=(select count(ol.id) from tb_other_inc_match_lu_sku ol inner join tb_other_inc_part_info oi
 on oi.other_inc_sku=ol.other_inc_sku 
 inner join tb_product p on p.product_serial_no=ol.lu_sku and ol.other_inc_type=oi.other_inc_id where other_inc_id='{0}' and p.product_current_cost = oi.other_inc_price)

where id='{0}'", wholesalerDT.Rows[i]["id"].ToString()));
        }

        for (int i = 0; i < RivalDT.Rows.Count; i++)
        {
            Config.ExecuteNonQuery(string.Format(@"update tb_other_inc set
inc_record=(select count(id) c from tb_other_inc_part_info where other_inc_id='{0}')

,inc_record_valid=(select count(id) c from tb_other_inc_part_info where tag=1 and other_inc_id='{0}')

,inc_record_match=(select count(ol.id) c from tb_other_inc_match_lu_sku ol inner join tb_other_inc_part_info oi
 on oi.other_inc_sku=ol.other_inc_sku and ol.other_inc_type=oi.other_inc_id and other_inc_id='{0}')

,bigger_than_lu=(select count(ol.id) from tb_other_inc_match_lu_sku ol inner join tb_other_inc_part_info oi
 on oi.other_inc_sku=ol.other_inc_sku 
 inner join tb_product p on p.product_serial_no=ol.lu_sku and ol.other_inc_type=oi.other_inc_id where other_inc_id='{0}' and p.product_current_price-p.product_current_discount < oi.other_inc_price)

,less_than_lu=(select count(ol.id) from tb_other_inc_match_lu_sku ol inner join tb_other_inc_part_info oi
 on oi.other_inc_sku=ol.other_inc_sku 
 inner join tb_product p on p.product_serial_no=ol.lu_sku and ol.other_inc_type=oi.other_inc_id where other_inc_id='{0}' and p.product_current_price-p.product_current_discount > oi.other_inc_price)

,equal_than_lu=(select count(ol.id) from tb_other_inc_match_lu_sku ol inner join tb_other_inc_part_info oi
 on oi.other_inc_sku=ol.other_inc_sku 
 inner join tb_product p on p.product_serial_no=ol.lu_sku and ol.other_inc_type=oi.other_inc_id where other_inc_id='{0}' and p.product_current_price-p.product_current_discount = oi.other_inc_price)

where id='{0}'", RivalDT.Rows[i]["id"].ToString()));
        }
    }
}

