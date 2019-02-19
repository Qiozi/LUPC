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
    public static nicklu2Model.tb_state_shipping GetState(string CountryText
        , string StateText
        , nicklu2Model.nicklu2Entities db)
    {
        var state = db.tb_state_shipping.FirstOrDefault(p => p.Country.Equals(CountryText)
                               && p.state_code.Equals(StateText));
        if (state == null)
        {
            state = nicklu2Model.tb_state_shipping.Createtb_state_shipping(0);
            state.Country = CountryText;
            state.gst = 0;
            state.is_paypal = false;
            state.IsOtherCountry = true;
            state.priority = 0;
            state.pst = 0;
            state.state_code = StateText;
            state.state_name = StateText;
            state.state_shipping = 100;
            state.system_category_serial_no = 3;
            db.AddTotb_state_shipping(state);
            db.SaveChanges();
        }
        return state;
    }
}