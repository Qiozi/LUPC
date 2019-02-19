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

[ActiveRecord("tb_order_system_note")]
[Serializable]
public class OrderSystemNoteModel : ActiveRecordBase<OrderSystemNoteModel>
{
	public OrderSystemNoteModel()
	{
		//
		// TODO: Add constructor logic here
		//
	}int _id;
    int _order_code;
    int _system_code;
    DateTime _regdate;
    string _note;

    /// <summary>
    /// 
    /// </summary>
    [PrimaryKey(PrimaryKeyType.Identity)]
    public int id
    {
        get { return _id; }
        set { _id = value; }
    }
    public static OrderSystemNoteModel GetOrderSystemNoteModel(int _id)
    {
        OrderSystemNoteModel[] models = OrderSystemNoteModel.FindAllByProperty("id", _id);
        if (models.Length == 1)
            return models[0];
        else
            return new OrderSystemNoteModel();
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
    public string note
    {
        get { return _note; }
        set { _note = value; }
    }
    [Property]
    public DateTime regdate
    {
        get { return _regdate; }
        set { _regdate = value; }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="system_code"></param>
    /// <returns></returns>
    public DataTable FindNoteBySystemCode(int system_code)
    {
        return Config.ExecuteDataTable(string.Format(@"select note, regdate from tb_order_system_note where system_code='{0}'", system_code));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="order_code"></param>
    /// <returns></returns>
    public DataTable FindModelByOrderCode(int order_code)
    {
        return Config.ExecuteDataTable(string.Format(@"select note, regdate from tb_order_system_note where order_code='{0}'", order_code));
   
    }
}
