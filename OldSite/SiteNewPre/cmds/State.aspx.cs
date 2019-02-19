using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class cmds_State : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            switch (ReqCmd)
            {
                case "getStates":

                    if (ReqCountryCode == "Canada")
                    {
                        var stateList = (from c in db.tb_state_shipping
                                         where c.Country.Equals(ReqCountryCode)
                                         && c.system_category_serial_no.HasValue
                                         && c.system_category_serial_no.Value.Equals( 1)

                                         orderby c.priority ascending
                                         select new
                                         {
                                             ID = c.state_serial_no,
                                             Code = c.state_code,
                                             Name = c.state_name,
                                             Country = c.Country
                                         }).ToList();
                        var json = Newtonsoft.Json.JsonConvert.SerializeObject(stateList);
                        Response.Write(json);
                    }
                    else if (ReqCountryCode == "United States")
                    {
                        var stateList = (from c in db.tb_state_shipping
                                         where c.Country.Equals(ReqCountryCode)
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
                        var json = Newtonsoft.Json.JsonConvert.SerializeObject(stateList);
                        Response.Write(json);
                    }
                    break;
            }
        }
        Response.End();
    }

    /// <summary>
    /// 国家缩写
    /// </summary>
    string ReqCountryCode
    {
        get
        {
            string code = Util.GetStringSafeFromQueryString(Page, "cc");
            if (code.ToUpper() == "CA")
                return "Canada";
            if (code.ToUpper() == "US")
                return "United States";
            return "";
        }
    }
}