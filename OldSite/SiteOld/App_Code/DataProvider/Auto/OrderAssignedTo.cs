
// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	11/10/2008 4:34:46 PM
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;
using System.Data;

[ActiveRecord("tb_order_assigned_to")]
[Serializable]
public class OrderAssignedToModel : ActiveRecordBase<OrderAssignedToModel>
{

    public OrderAssignedToModel()
    {

    }

    public static OrderAssignedToModel GetOrderAssignedToModel(int _assigned_to_id)
    {
        OrderAssignedToModel[] models = OrderAssignedToModel.FindAllByProperty("assigned_to_id", _assigned_to_id);
        if (models.Length == 1)
            return models[0];
        else
            return new OrderAssignedToModel();
    }

    int _assigned_to_id;

    [PrimaryKey(PrimaryKeyType.Identity)]
    public int assigned_to_id
    {
        get { return _assigned_to_id; }
        set { _assigned_to_id = value; }
    }

    string _assigned_to_staff_name;

    [Property]
    public string assigned_to_staff_name
    {
        get { return _assigned_to_staff_name; }
        set { _assigned_to_staff_name = value; }
    }

    int _assigned_to_staff_id;

    [Property]
    public int assigned_to_staff_id
    {
        get { return _assigned_to_staff_id; }
        set { _assigned_to_staff_id = value; }
    }

    string _assigned_to_comment;

    [Property]
    public string assigned_to_comment
    {
        get { return _assigned_to_comment; }
        set { _assigned_to_comment = value; }
    }

    int _edit_staff_id;

    [Property]
    public int edit_staff_id
    {
        get { return _edit_staff_id; }
        set { _edit_staff_id = value; }
    }

    string _eidt_staff_name;

    [Property]
    public string eidt_staff_name
    {
        get { return _eidt_staff_name; }
        set { _eidt_staff_name = value; }
    }

    int _order_code;

    [Property]
    public int order_code
    {
        get { return _order_code; }
        set { _order_code = value; }
    }

    DateTime _regdate;

    [Property]
    public DateTime regdate
    {
        get { return _regdate; }
        set { _regdate = value; }
    }


}

