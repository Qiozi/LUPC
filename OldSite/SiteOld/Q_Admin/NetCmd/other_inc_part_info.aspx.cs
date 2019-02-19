using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Q_Admin_NetCmd_other_inc_part_info : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && Request.UserHostAddress .Trim() == Config.ExecuteScalar("select max(ip) from tb_luip").ToString().Trim())
        {
        //{
        //    Response.Write(Cmd + "<br/>");
        //    Response.Write(Sql + "<br/>");
        //    Response.Write(Other_inc_id + "<br/>");
            if (Cmd == "delete")
            {
                Config.ExecuteNonQuery(string.Format(@"delete from tb_other_inc_part_info where other_inc_id='{0}' and other_inc_id in ({1})", Other_inc_id, Config.other_inc_id_watch));
                Config.ExecuteNonQuery(Sql);
            }
            else if (Sql .IndexOf("insert") != -1)
            {
                Config.ExecuteNonQuery(Sql);
                            
            }
            Response.Write("OK");    
            Response.End();
        }
    }

    #region properties
    public string Cmd
    {
        get { return Util.GetStringSafeFromQueryString(Page, "cmd"); }
    }

    public string Sql
    {
        get { return Util.GetStringSafeFromQueryString(Page, "sql"); }
    }

    public string Other_inc_id
    {
        get { return Util.GetStringSafeFromQueryString(Page, "other_inc_id"); }
    }
    #endregion
}
