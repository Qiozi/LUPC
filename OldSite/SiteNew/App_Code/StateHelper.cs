using LU.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for StateHelper
/// </summary>
public class StateHelper
{
    public StateHelper()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    /// <summary>
    /// 获取洲ID，　如果是外国不存在的，，添加它。
    /// </summary>
    /// <param name="CountryText"></param>
    /// <param name="StateText"></param>
    /// <param name="db"></param>
    /// <returns></returns>
    public static LU.Data.tb_state_shipping GetState(string CountryText
        , string StateText
        , nicklu2Entities db)
    {
        var state = db.tb_state_shipping.FirstOrDefault(p => p.Country.Equals(CountryText)
                               && p.state_code.Equals(StateText));
        if (state == null)
        {
            state = new tb_state_shipping
            {
                Country = CountryText,
                gst = 0,
                is_paypal = false,
                IsOtherCountry = true,
                priority = 0,
                pst = 0,
                state_code = StateText,
                state_name = StateText,
                state_shipping = 100,
                system_category_serial_no = 3,
            };

            db.tb_state_shipping.Add(state);
            db.SaveChanges();
        }
        return state;
    }
}