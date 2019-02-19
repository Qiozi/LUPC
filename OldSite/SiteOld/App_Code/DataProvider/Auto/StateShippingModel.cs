// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2007-6-4 14:53:25
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;
using System.Data;

[ActiveRecord("tb_state_shipping")]
[Serializable]
public class StateShippingModel : ActiveRecordBase<StateShippingModel>
{
    int _state_serial_no;
    string _state_name;
    double _state_shipping;
    int _system_category_serial_no;
    string _state_short_name;
    string _state_code;
    byte _gst;
    byte _pst;
    int _priority;
    bool _IsOtherCountry;
    string _Country;

    public StateShippingModel()
    {

    }


    /// <summary>
    /// 
    /// </summary>
    [PrimaryKey(PrimaryKeyType.Identity)]
    public int state_serial_no
    {
        get { return _state_serial_no; }
        set { _state_serial_no = value; }
    }
    public static StateShippingModel GetStateShippingModel(int _state_serial_no)
    {
        StateShippingModel[] models = StateShippingModel.FindAllByProperty("state_serial_no", _state_serial_no);
        if (models.Length == 1)
            return models[0];
        else
            return new StateShippingModel();
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string state_name
    {
        get { return _state_name; }
        set { _state_name = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public double state_shipping
    {
        get { return _state_shipping; }
        set { _state_shipping = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public int system_category_serial_no
    {
        get { return _system_category_serial_no; }
        set { _system_category_serial_no = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string state_short_name
    {
        get { return _state_short_name; }
        set { _state_short_name = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string state_code
    {
        get { return _state_code; }
        set { _state_code = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public byte gst
    {
        get { return _gst; }
        set { _gst = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public byte pst
    {
        get { return _pst; }
        set { _pst = value; }
    }
    [Property]
    public int priority
    {
        get { return _priority; }
        set { _priority = value; }
    }

    [Property]
    public string Country
    {
        get { return _Country; }
        set { _Country = value; }
    }

    [Property]
    public bool IsOtherCountry
    {
        get { return _IsOtherCountry; }
        set { _IsOtherCountry = value; }
    }

    public static StateShippingModel[] GetModelsBySystemCategory(int country_id)
    {
        return StateShippingModel.FindAllByProperty("system_category_serial_no", country_id);
    }

    /// <summary>
    /// 按缩写查找取得ID号
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public static int FindStatIDByCode(string code)
    {
        StateShippingModel[] ssm = StateShippingModel.FindAllByProperty("state_code", code);
        if (ssm.Length > 0)
            return ssm[0].state_serial_no;
        return -1;
    }

    public StateShippingModel FindByCode(string code)
    {
        StateShippingModel[] ssm = StateShippingModel.FindAllByProperty("state_code", code);
        if (ssm.Length > 0)
            return ssm[0];
        return new StateShippingModel();
    }

    public StateShippingModel[] FindAllByPriority()
    {
        NHibernate.Expression.Order o = new NHibernate.Expression.Order("priority", true);
        NHibernate.Expression.Order[] oo = new NHibernate.Expression.Order[] { o };
        return StateShippingModel.FindAll(oo);
    }

    /// <summary>
    /// 如果不存在，就创建新的state 信息
    /// </summary>
    /// <param name="CountryName"></param>
    /// <param name="StateName"></param>
    /// <returns></returns>
    public StateShippingModel SaveNewState(string CountryName, string StateName)
    {
        NHibernate.Expression.EqExpression eq1 = new NHibernate.Expression.EqExpression("state_code", StateName);
        NHibernate.Expression.EqExpression eq2 = new NHibernate.Expression.EqExpression("state_name", StateName);
        NHibernate.Expression.OrExpression or = new NHibernate.Expression.OrExpression(eq1, eq2);
        StateShippingModel[] ssm = StateShippingModel.FindAll(or);
        foreach (var m in ssm)
        {
            if (CountryName == m.Country)
            {
                return m;
            }
        }

        StateShippingModel newModel = new StateShippingModel();
        newModel.Country = CountryName;
        newModel.state_code = StateName;
        newModel.state_name = StateName;
        newModel.state_short_name = "";
        newModel.system_category_serial_no = 3;
        newModel.IsOtherCountry = true;
        newModel.state_shipping = 150;
        newModel.pst = 0;
        newModel.gst = 0;
       
        newModel.Create();
        return newModel;
    }

    /// <summary>
    /// 取各种税列表，用于订单的选择税
    /// </summary>
    /// <returns></returns>
    public DataTable GetAllTaxList()
    {
        DataTable dt = Config.ExecuteDataTable("select distinct ifnull(gst, 0) gst, ifnull(pst, 0) pst, '' stateNames from tb_state_shipping");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow dr = dt.Rows[i];
            if(dr["gst"].ToString() == "0"
                && dr["pst"].ToString() == "0"){

            }
            DataTable subDT = Config.ExecuteDataTable(string.Format("select state_code from  tb_state_shipping where gst='{0}' and pst = '{1}'"
                , dr["gst"].ToString()
                , dr["pst"].ToString()));
            string names = "";
            for (int j = 0; j < subDT.Rows.Count; j++)
            {
                names = names == "" ? names : names + ",";
                names += subDT.Rows[j][0].ToString();
            }
            dr["stateNames"] = names;
        }
        return dt;
    }
}
