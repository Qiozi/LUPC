// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2007-6-5 13:35:59
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;
using System.Data;

[ActiveRecord("tb_sp_tmp")]
[Serializable]
public class SpTmpModel : ActiveRecordBase<SpTmpModel>
{
    int _sys_tmp_serial_no;
    string _sys_tmp_code;
    decimal _sys_tmp_price;
    DateTime _create_datetime;
    byte _tag;
    string _ip;
    int _system_templete_serial_no;
    string _email;
    byte _system_category_serial_no;


    public SpTmpModel()
    {

    }


    /// <summary>
    /// 
    /// </summary>
    [PrimaryKey(PrimaryKeyType.Identity)]
    public int sys_tmp_serial_no
    {
        get { return _sys_tmp_serial_no; }
        set { _sys_tmp_serial_no = value; }
    }
    public static SpTmpModel GetSpTmpModel(int _sys_tmp_serial_no)
    {
        SpTmpModel[] models = SpTmpModel.FindAllByProperty("sys_tmp_serial_no", _sys_tmp_serial_no);
        if (models.Length == 1)
            return models[0];
        else
            return new SpTmpModel();
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string sys_tmp_code
    {
        get { return _sys_tmp_code; }
        set { _sys_tmp_code = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public decimal sys_tmp_price
    {
        get { return _sys_tmp_price; }
        set { _sys_tmp_price = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public DateTime create_datetime
    {
        get { return _create_datetime; }
        set { _create_datetime = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public byte tag
    {
        get { return _tag; }
        set { _tag = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string ip
    {
        get { return _ip; }
        set { _ip = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public int system_templete_serial_no
    {
        get { return _system_templete_serial_no; }
        set { _system_templete_serial_no = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string email
    {
        get { return _email; }
        set { _email = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public byte system_category_serial_no
    {
        get { return _system_category_serial_no; }
        set { _system_category_serial_no = value; }
    }

    public static DataTable GetModelsByTmpCode(string sys_tmp_code)
    {
        string sql = "select st.*,sp.sys_tmp_cost,sp.sys_tmp_price,sp.sys_tmp_price,sp.sys_tmp_product_name from tb_sp_tmp sp inner join tb_ebay_system st on st.id=sp.system_templete_serial_no where sys_tmp_code ='" + sys_tmp_code + "'";
        return Config.ExecuteDataTable(sql);
    }
    public static DataTable GetModelsByTmpCodeNotConationSystemTemplete(string sys_tmp_code)
    {
        string sql = "select sp.* from tb_sp_tmp sp where sys_tmp_code ='" + sys_tmp_code + "'";
        return Config.ExecuteDataTable(sql);
    }
}
