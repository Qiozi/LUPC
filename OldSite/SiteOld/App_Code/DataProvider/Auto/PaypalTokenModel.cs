using System;
using Castle.ActiveRecord;
/// <summary>
/// KeywordModel 的摘要说明
/// </summary>
[ActiveRecord("tb_paypal_token")]
[Serializable]
public class PaypalTokenModel : ActiveRecordBase<PaypalTokenModel>
{
    public PaypalTokenModel()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    // int _serial_no;
    string _order_code;
    string _token;
    DateTime _create_datetime;
    string _ip;
    string _return_response;
    int _customer_serial_no;
    decimal _amt;



    /// <summary>
    /// 
    /// </summary>
    [PrimaryKey(PrimaryKeyType.Identity)]
    public int serial_no
    {
        get { return serial_no; }
        set { serial_no = value; }
    }
    public static PaypalTokenModel GetKeywordModelModel(int _serial_no)
    {
        PaypalTokenModel[] models = PaypalTokenModel.FindAllByProperty("serial_no", _serial_no);
        if (models.Length == 1)
            return models[0];
        else
            return new PaypalTokenModel();
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string order_code
    {
        get { return _order_code; }
        set { _order_code = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string token
    {
        get { return _token; }
        set { _token = value; }
    }
    /// <summary>
    /// 
    /// </summary>
    [Property]
    public decimal amt
    {
        get { return _amt; }
        set { _amt = value; }
    }   
    /// <summary>
    /// 
    /// </summary>
    [Property]
    public int customer_serial_no
    {
        get { return _customer_serial_no; }
        set { _customer_serial_no = value; }
    }   
    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string return_response
    {
        get { return _return_response; }
        set { _return_response = value; }
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
    public string ip
    {
        get { return _ip; }
        set { _ip = value; }
    }
    public static void SaveToken(string token,string tokensss, string order_code, int customer_id, decimal amt)
    {
        try
        {
            PaypalTokenModel p = new PaypalTokenModel();
            p.amt = amt;
            p.create_datetime = DateTime.Now;
            p.customer_serial_no = customer_id;
            p.ip = System.Web.HttpContext.Current.Request.UserHostAddress;
            p.order_code = order_code;
            p.return_response = tokensss;
            p.token = token;
            p.Create();
        }
        catch (Exception ex)
        {
            TrackModel.InsertInfo(ex.Message, customer_id);
        }
    }
}

