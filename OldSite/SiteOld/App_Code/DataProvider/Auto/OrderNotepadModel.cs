
// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	6/12/2012 2:45:49 AM
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;
using System.Data;

[ActiveRecord("tb_order_notepad")]
[Serializable]
public class OrderNotepadModel : ActiveRecordBase<OrderNotepadModel>
{

    public OrderNotepadModel()
    {

    }

    public static OrderNotepadModel GetOrderNotepadModel(int _ID)
    {
        OrderNotepadModel[] models = OrderNotepadModel.FindAllByProperty("ID", _ID);
        if (models.Length == 1)
            return models[0];
        else
            return new OrderNotepadModel();
    }

    int _ID;

    [PrimaryKey(PrimaryKeyType.Identity)]
    public int ID
    {
        get { return _ID; }
        set { _ID = value; }
    }

    string _Msg;

    [Property]
    public string Msg
    {
        get { return _Msg; }
        set { _Msg = value; }
    }

    int _OrderCode;

    [Property]
    public int OrderCode
    {
        get { return _OrderCode; }
        set { _OrderCode = value; }
    }

    string _Author;

    [Property]
    public string Author
    {
        get { return _Author; }
        set { _Author = value; }
    }
}

