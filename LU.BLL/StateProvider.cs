using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LU.BLL
{
    public class StateProvider
    {
        public static object GetStates(Data.nicklu2Entities context, string countryCode)
        {
            string code = countryCode;
            if (code.ToUpper() == "CA")
                code= "Canada";
            if (code.ToUpper() == "US")
                code= "United States";
           
            if (code == "Canada")
            {
                var stateList = (from c in context.tb_state_shipping
                                 where c.Country.Equals(code)
                                 && c.system_category_serial_no.HasValue
                                 && c.system_category_serial_no.Value.Equals(1)

                                 orderby c.priority ascending
                                 select new
                                 {
                                     ID = c.state_serial_no,
                                     Code = c.state_code,
                                     Name = c.state_name,
                                     Country = c.Country
                                 }).ToList();
                return stateList;
            }
            else if (code == "United States")
            {
                var stateList = (from c in context.tb_state_shipping
                                 where c.Country.Equals(code)
                                 && c.system_category_serial_no.HasValue
                                 && c.system_category_serial_no.Value.Equals(2)

                                 orderby c.priority ascending
                                 select new
                                 {
                                     ID = c.state_serial_no,
                                     Code = c.state_code,
                                     Name = c.state_name,
                                     Country = c.Country
                                 }).ToList();
                return stateList;
            }
            return string.Empty;
        }
    }
}
