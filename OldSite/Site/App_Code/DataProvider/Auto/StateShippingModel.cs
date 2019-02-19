// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2007-6-4 14:53:25
//
// // // // // // // // // // // // // // // //
using LU.Data;
using System;
using System.Linq;
using System.Data;

[Serializable]
public class StateShippingModel 
{
   public static tb_state_shipping GetStateShippingModel(nicklu2Entities context, int customer_card_state)
    {
        var query = context.tb_state_shipping.FirstOrDefault(me => me.state_serial_no.Equals(customer_card_state));
        return query;
    }
    public static tb_state_shipping[] GetModelsBySystemCategory(nicklu2Entities context, int country_id)
    {
        // return StateShippingModel.FindAllByProperty("system_category_serial_no", country_id);
        return context.tb_state_shipping.Where(me => me.system_category_serial_no.Value.Equals(country_id)).ToList().ToArray();
           
    }

    /// <summary>
    /// 按缩写查找取得ID号
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public static int FindStatIDByCode(nicklu2Entities context, string code)
    {
        //StateShippingModel[] ssm = StateShippingModel.FindAllByProperty("state_code", code);
        //if (ssm.Length > 0)
        //    return ssm[0].state_serial_no;
        //return -1;

        var query = context.tb_state_shipping.FirstOrDefault(me => me.state_code.Equals(code));
        if (query == null)
            return -1;
        else
            return query.state_serial_no;
    }

    public tb_state_shipping FindByCode(nicklu2Entities context, string code)
    {
        //StateShippingModel[] ssm = StateShippingModel.FindAllByProperty("state_code", code);
        //if (ssm.Length > 0)
        //    return ssm[0];
        //return new StateShippingModel();
        var query = context.tb_state_shipping.FirstOrDefault(me => me.state_code.Equals(code));
        if (query == null)
            return new tb_state_shipping();
        else
            return query;
    }

    public tb_state_shipping[] FindAllByPriority(nicklu2Entities context)
    {
        //NHibernate.Expression.Order o = new NHibernate.Expression.Order("priority", true);
        //NHibernate.Expression.Order[] oo = new NHibernate.Expression.Order[] { o };
        //return StateShippingModel.FindAll(oo);
        var query = context.tb_state_shipping.OrderBy(me => me.priority.Value).ToList();
        return query.ToArray();
    }

    /// <summary>
    /// 如果不存在，就创建新的state 信息
    /// </summary>
    /// <param name="CountryName"></param>
    /// <param name="StateName"></param>
    /// <returns></returns>
    public tb_state_shipping SaveNewState(nicklu2Entities context, string CountryName, string StateName)
    {
        //NHibernate.Expression.EqExpression eq1 = new NHibernate.Expression.EqExpression("state_code", StateName);
        //NHibernate.Expression.EqExpression eq2 = new NHibernate.Expression.EqExpression("state_name", StateName);
        //NHibernate.Expression.OrExpression or = new NHibernate.Expression.OrExpression(eq1, eq2);
        //StateShippingModel[] ssm = StateShippingModel.FindAll(or);
        //foreach (var m in ssm)
        //{
        //    if (CountryName == m.Country)
        //    {
        //        return m;
        //    }
        //}

        ////StateShippingModel newModel = new StateShippingModel();
        ////newModel.Country = CountryName;
        ////newModel.state_code = StateName;
        ////newModel.state_name = StateName;
        ////newModel.state_short_name = "";
        ////newModel.system_category_serial_no = 3;
        ////newModel.IsOtherCountry = true;
        ////newModel.state_shipping = 150;
        ////newModel.pst = 0;
        ////newModel.gst = 0;

        ////newModel.Create();
        ////return newModel;

        var query = context.tb_state_shipping.FirstOrDefault(me => (me.state_code.Equals(StateName) || me.state_name.Equals(StateName)) && me.Country.Equals(CountryName));
        if (query != null)
            return query;


        var newModel = new tb_state_shipping();
        newModel.Country = CountryName;
        newModel.state_code = StateName;
        newModel.state_name = StateName;
        newModel.state_short_name = "";
        newModel.system_category_serial_no = 3;
        newModel.IsOtherCountry = true;
        newModel.state_shipping = 150;
        newModel.pst = 0;
        newModel.gst = 0;
        context.tb_state_shipping.Add(newModel);
        context.SaveChanges();
       
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
