using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DownloadEBayOrder.BLL
{
    public class StateShippingHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="stateName"></param>
        /// <param name="countryName"></param>
        /// <returns></returns>
        public static tb_state_shipping GetStateID(nicklu2Entities context, string stateName, string countryName)
        {
            if (countryName.Equals("CA"))
            {
                countryName = "Canada";
            }
            if (countryName.Equals("US"))
            {
                countryName = "United States";
            }

            var ss = context.tb_state_shipping.FirstOrDefault(s => (s.state_code.Equals(stateName) || s.state_name.Equals(stateName))
                && (string.IsNullOrEmpty(countryName) ? true : s.Country.Equals(countryName)));

            if (ss == null)
            {
                //ss = tb_state_shipping.Createtb_state_shipping(0);
                ss = new tb_state_shipping();
                ss.state_code = stateName.Trim().Length > 5 ? stateName.Trim().Substring(0, 5) : stateName.Trim();
                ss.state_name = stateName;
                ss.IsOtherCountry = true;
                ss.is_paypal = true;
                ss.state_shipping = 150;
                ss.system_category_serial_no = 3;
                ss.Country = countryName;
                // context.AddTotb_state_shipping(ss);
                context.tb_state_shipping.Add(ss);
                context.SaveChanges();

                return ss;
            }
            else
                return ss;
        }
    }
}
