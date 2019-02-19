using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Q_Admin_NetCmd_export_other_inc_sku : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Util.GetStringSafeFromQueryString(Page, "cmd").ToLower() != "qiozi@msn.com")
        {
            Response.End();
            return;
        }

        if (Util.GetStringSafeFromQueryString(Page,"SubCmd").ToLower() == "xml")
        {
            Response.ClearContent();//
            Response.ContentType = "text/xml";
            Response.Write(@"<?xml version=""1.0"" encoding=""utf-8"" ?>
        <data>");
            DataTable dt = Config.ExecuteDataTable(string.Format(@"select * from tb_other_inc_match_lu_sku where other_inc_type in ({0})", Config.other_inc_id_watch));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Response.Write(string.Format("<row><lu_sku>{0}</lu_sku><other_inc_sku>{1}</other_inc_sku><other_inc_type>{2}</other_inc_type></row>"
                    , dt.Rows[i]["lu_sku"].ToString()
                    , dt.Rows[i]["other_inc_sku"].ToString()
                    , dt.Rows[i]["other_inc_type"].ToString()));
            }
            Response.Write(@"</data>");
            Response.End();
        }
        else
        {
            Response.ClearContent();//
            Response.ContentType = "text/html";
           
            DataTable dt = Config.ExecuteDataTable(string.Format(@"select * from tb_other_inc_match_lu_sku where other_inc_type in ({0})", Config.other_inc_id_watch));
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //Response.Write(string.Format("<row><lu_sku>{0}</lu_sku><other_inc_sku>{1}</other_inc_sku><other_inc_type>{2}</other_inc_type></row>"
                //    , dt.Rows[i]["lu_sku"].ToString()
                //    , dt.Rows[i]["other_inc_sku"].ToString()
                //    , dt.Rows[i]["other_inc_type"].ToString()));
               sb.Append(","+string.Format(" ({0},'{1}',{2})"
                    , dt.Rows[i]["lu_sku"].ToString()
                    , dt.Rows[i]["other_inc_sku"].ToString()
                    , dt.Rows[i]["other_inc_type"].ToString()
                    ));
            }
            if (sb.ToString().Length > 0)
            {
                Response.Write(@"insert  into `tb_other_inc_match_lu_sku`(`lu_sku`,`other_inc_sku`,`other_inc_type`) values ");
                Response.Write(sb.ToString().Substring(1)+";");
            }

            Response.End();
        
        }
       
    }
}
