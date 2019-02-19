// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2007-6-5 10:42:04
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;
using System.Data;

[ActiveRecord("tb_sp_tmp_detail")]
[Serializable]
public class SpTmpDetailModel : ActiveRecordBase<SpTmpDetailModel>
{

    public SpTmpDetailModel()
    {

    }

    public static SpTmpDetailModel GetSpTmpDetailModel(int _sys_tmp_detail)
    {
        SpTmpDetailModel[] models = SpTmpDetailModel.FindAllByProperty("sys_tmp_detail", _sys_tmp_detail);
        if (models.Length == 1)
            return models[0];
        else
            return new SpTmpDetailModel();
    }

    int _sys_tmp_detail;

    [PrimaryKey(PrimaryKeyType.Identity)]
    public int sys_tmp_detail
    {
        get { return _sys_tmp_detail; }
        set { _sys_tmp_detail = value; }
    }

    string _sys_tmp_code;

    [Property]
    public string sys_tmp_code
    {
        get { return _sys_tmp_code; }
        set { _sys_tmp_code = value; }
    }

    int _product_serial_no;

    [Property]
    public int product_serial_no
    {
        get { return _product_serial_no; }
        set { _product_serial_no = value; }
    }

    string _product_name;

    [Property]
    public string product_name
    {
        get { return _product_name; }
        set { _product_name = value; }
    }

    string _cate_name;

    [Property]
    public string cate_name
    {
        get { return _cate_name; }
        set { _cate_name = value; }
    }

    int _part_quantity;

    [Property]
    public int part_quantity
    {
        get { return _part_quantity; }
        set { _part_quantity = value; }
    }

    int _part_max_quantity;

    [Property]
    public int part_max_quantity
    {
        get { return _part_max_quantity; }
        set { _part_max_quantity = value; }
    }

    decimal _product_current_price;

    [Property]
    public decimal product_current_price
    {
        get { return _product_current_price; }
        set { _product_current_price = value; }
    }

    decimal _product_current_cost;

    [Property]
    public decimal product_current_cost
    {
        get { return _product_current_cost; }
        set { _product_current_cost = value; }
    }

    int _product_order;

    [Property]
    public int product_order
    {
        get { return _product_order; }
        set { _product_order = value; }
    }

    int _system_templete_serial_no;

    [Property]
    public int system_templete_serial_no
    {
        get { return _system_templete_serial_no; }
        set { _system_templete_serial_no = value; }
    }

    int _system_product_serial_no;

    [Property]
    public int system_product_serial_no
    {
        get { return _system_product_serial_no; }
        set { _system_product_serial_no = value; }
    }

    int _part_group_id;

    [Property]
    public int part_group_id
    {
        get { return _part_group_id; }
        set { _part_group_id = value; }
    }

    decimal _save_price;

    [Property]
    public decimal save_price
    {
        get { return _save_price; }
        set { _save_price = value; }
    }

    decimal _old_price;

    [Property]
    public decimal old_price
    {
        get { return _old_price; }
        set { _old_price = value; }
    }

    int _re_sys_tmp_detail;

    [Property]
    public int re_sys_tmp_detail
    {
        get { return _re_sys_tmp_detail; }
        set { _re_sys_tmp_detail = value; }
    }

    decimal _product_current_price_rate;

    [Property]
    public decimal product_current_price_rate
    {
        get { return _product_current_price_rate; }
        set { _product_current_price_rate = value; }
    }

    decimal _product_current_sold;

    [Property]
    public decimal product_current_sold
    {
        get { return _product_current_sold; }
        set { _product_current_sold = value; }
    }

    bool _is_lock;

    [Property]
    public bool is_lock
    {
        get { return _is_lock; }
        set { _is_lock = value; }
    }

    string _ebay_number;

    [Property]
    public string ebay_number
    {
        get { return _ebay_number; }
        set { _ebay_number = value; }
    }  

    
    public static SpTmpDetailModel[] GetModelsBySysTmpCode(string sys_tmp_code)
    {
        return SpTmpDetailModel.FindAllByProperty("sys_tmp_code", sys_tmp_code);

    }

    public static DataTable GetModelsBySysCode(string sys_tmp_code)
    {
        return Config.ExecuteDataTable(@"select part_quantity,p.product_serial_no,p.product_name,p.product_short_name,(p.product_current_price-p.product_current_discount) product_current_price ,sp.sys_tmp_detail, p.is_non,p.product_current_cost from tb_sp_tmp_detail sp inner join tb_product p on p.product_serial_no=sp.product_serial_no 
inner join tb_product_category pc on pc.menu_child_serial_no=p.menu_child_serial_no where sys_tmp_code='" + sys_tmp_code + "' order by sp.product_order asc");
    }


    public static decimal GetPriceSUM(int sys_tem_code)
    {
        string sql = "select ifnull(sum(product_current_price), 0) from tb_sp_tmp_detail where sys_tmp_code='" + sys_tem_code + "'";
        DataTable dt = Config.ExecuteDataTable(sql);
        if (dt.Rows.Count == 1)
            return decimal.Parse(dt.Rows[0][0].ToString());
        return 0M;
    }

    public static decimal GetPriceCostSUM(int sys_tem_code)
    {
        string sql = "select sum(product_current_cost) from tb_sp_tmp_detail where sys_tmp_code='" + sys_tem_code + "'";
        DataTable dt = Config.ExecuteDataTable(sql);
        if (dt.Rows.Count == 1)
            return decimal.Parse(dt.Rows[0][0].ToString());
        return -1;
    }
    public static decimal GetPriceSaveCostSUM(int sys_tem_code)
    {
        string sql = "select sum(save_price) from tb_sp_tmp_detail where sys_tmp_code='" + sys_tem_code + "'";
        DataTable dt = Config.ExecuteDataTable(sql);
        if (dt.Rows.Count == 1)
            return decimal.Parse(dt.Rows[0][0].ToString());
        return -1;
    }

    public static int GetNewCode(int SysID)
    {
        //string code = Config.eightCode.ToString();
        //DataTable dt = Config.ExecuteDataTable("select system_code from tb_system_code_store where system_code='" + code + "'");
        //if (dt.Rows.Count > 0)
        //    code = GetNewCode().ToString();
        //return int.Parse(code);
        int code = Code.NewSysCode();
        SystemCodeStoreModel scsm = new SystemCodeStoreModel();
        scsm.create_datetime = DateTime.Now;
        scsm.is_buy = true;
        scsm.system_code = code;
        scsm.ip = "127.0.0.1";
        scsm.system_templete_serial_no = SysID;
        scsm.Create();
        return code;
    }

    public static void DeleteBySysCode(int sys_temp_code)
    {
        string sql = "delete from tb_sp_tmp_detail where sys_tmp_code='" + sys_temp_code + "'";
        Config.ExecuteNonQuery(sql);
    }
}
