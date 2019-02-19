
// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	11/9/2008 4:53:57 PM
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;
using System.Data;

[ActiveRecord("tb_order_shipping_date")]
[Serializable]
public class OrderShippingDateModel : ActiveRecordBase<OrderShippingDateModel>
{

    public OrderShippingDateModel()
    {

    }

    public static OrderShippingDateModel GetOrderShippingDateModel(int _id)
    {
        OrderShippingDateModel[] models = OrderShippingDateModel.FindAllByProperty("id", _id);
        if (models.Length == 1)
            return models[0];
        else
            return new OrderShippingDateModel();
    }

    int _id;

    [PrimaryKey(PrimaryKeyType.Identity)]
    public int id
    {
        get { return _id; }
        set { _id = value; }
    }

    string _staff_name;

    [Property]
    public string staff_name
    {
        get { return _staff_name; }
        set { _staff_name = value; }
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

    DateTime _finish_date;

    [Property]
    public DateTime finish_date
    {
        get { return _finish_date; }
        set { _finish_date = value; }
    }


}

