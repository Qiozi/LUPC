// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2008-09-22 10:34:30
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;
using System.Data;

/// <summary>
/// Summary description for OrderSystemFinishDateModel
/// </summary>

[ActiveRecord("tb_order_system_finish_date")]
[Serializable]
public class OrderSystemFinishDateModel : ActiveRecordBase<OrderSystemFinishDateModel>
{
    int _id;
    int _order_code;
    int _system_code;
    DateTime _regdate;

    DateTime _finish_date;



    public OrderSystemFinishDateModel()
    {

    }


    /// <summary>
    /// 
    /// </summary>
    [PrimaryKey(PrimaryKeyType.Identity)]
    public int id
    {
        get { return _id; }
        set { _id = value; }
    }
    public static OrderSystemFinishDateModel GetOrderSystemFinishDateModel(int _id)
    {
        OrderSystemFinishDateModel[] models = OrderSystemFinishDateModel.FindAllByProperty("id", _id);
        if (models.Length == 1)
            return models[0];
        else
            return new OrderSystemFinishDateModel();
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public int order_code
    {
        get { return _order_code; }
        set { _order_code = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public int system_code
    {
        get { return _system_code; }
        set { _system_code = value; }
    }
    [Property]
    public DateTime finish_date
    {
        get { return _finish_date; }
        set { _finish_date = value; }
    }
    [Property]
    public DateTime regdate
    {
        get { return _regdate; }
        set { _regdate = value; }
    }


    public string FindDateBySystemCode(int system_code)
    {
        DataTable dt = Config.ExecuteDataTable(string.Format(@"select date_format(finish_date,'%Y-%m-%d') from tb_order_system_finish_date 
where system_code='{0}' order by regdate desc limit 0,1", system_code));
        if (dt.Rows.Count == 1)
            return dt.Rows[0][0].ToString();
        else
            return "";
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="order_code"></param>
    /// <returns></returns>
    public DataTable FindModelByOrderCode(int order_code)
    {
        return Config.ExecuteDataTable(string.Format(@"select date_format(finish_date,'%Y-%m-%d') finish_date, regdate from tb_order_system_finish_date
 where order_code='{0}' ", order_code));
    }
}

