using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for eBaySysTitle
/// </summary>
public class eBaySysTitle
{
    public eBaySysTitle()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    /// <summary>
    /// 取得系统 自动生成的标题 
    /// 
    /// </summary>
    /// <param name="sysSku"></param>
    /// <returns></returns>
    public static string GetSystemAutoTitle(int sysSku, ref string cpuShortName)
    {
        DataTable dt = Config.ExecuteDataTable(string.Format(@"select p.short_name_for_sys, sys.is_barebone,p.product_current_price, es.is_cpu, es.is_video
from tb_product p inner join tb_ebay_system_parts sp on sp.luc_sku=p.product_serial_no
inner join tb_ebay_system_part_comment es on es.id=sp.comment_id 
inner join tb_ebay_system sys on sys.id=sp.system_sku
where p.short_name_for_sys <> '' and sp.system_sku='{0}'
order by priority asc ", sysSku));

        string titleEnd = " Desktop PC SKU#" + sysSku;
        bool isgaming = false;

        if (dt.Rows.Count > 0)
        {
            string title = "";
            bool isbarebone = dt.Rows[0]["is_barebone"].ToString() == "1";
            if (isbarebone)
                titleEnd = " Barebone PC SKU#" + sysSku;
            FilterSysKeywork(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                if ((title + " " + dr["short_name_for_sys"].ToString().Trim() + " " + titleEnd).Length <= 80)
                {
                    title += " " + dr["short_name_for_sys"].ToString().Trim();
                }

                if (dr["is_cpu"].ToString() == "1")
                    cpuShortName = dr["short_name_for_sys"].ToString().Trim();
                if (dr["is_video"].ToString() == "1" && decimal.Parse(dr["product_current_price"].ToString()) > 100M)
                    isgaming = true;
            }
            if (isgaming && (title + " " + titleEnd.Trim()).Trim().Length <= 73)
                titleEnd = titleEnd.Replace("PC", "Gaming PC");
            return (title + " " + titleEnd.Trim()).Trim();
        }
        return titleEnd.Trim();
    }

    static void FilterSysKeywork(DataTable dt)
    {
        string pre_name = "";
        int count = 1;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            pre_name = dt.Rows[i]["short_name_for_sys"].ToString();
            if (i < dt.Rows.Count - 1)
            {
                if (pre_name == dt.Rows[i+1]["short_name_for_sys"].ToString())
                {
                    count += 1;
                    dt.Rows.RemoveAt(i + 1);
                    i--;

                }
                else
                {
                    dt.Rows[i]["short_name_for_sys"] = count > 1 ? count + "x" + dt.Rows[i]["short_name_for_sys"].ToString() : dt.Rows[i]["short_name_for_sys"].ToString();
                    count = 1;
                }
            }
        }
    }
}