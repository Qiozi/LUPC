using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for GetItemSpecifics
/// </summary>
public class GetItemSpecifics
{
    public GetItemSpecifics()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    /// <summary>
    /// 取得系统规格
    /// </summary>
    /// <returns></returns>
    public static List<KeyValuePair<string, string>> GetSystemSpecifics(int sysSku)
    {
        List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();

        result.Add(new KeyValuePair<string, string>("Brand", "Custom, Whitebox"));
        result.Add(new KeyValuePair<string, string>("MPN", "Does not apply"));
        result.Add(new KeyValuePair<string, string>("UPC", "Does not apply"));

        DataTable dt = Config.ExecuteDataTable("Select ItemSpecificsName, ItemSpecificsValue from tb_ebay_system_item_specifics where system_sku='" + sysSku + "'");

        EbayItem ei = new EbayItem();
        foreach (DataRow dr in dt.Rows)
        {
            if (!string.IsNullOrEmpty(dr["ItemSpecificsValue"].ToString()))
            {
                if (dr["ItemSpecificsName"].ToString().ToLower() != "brand")
                {
                    result.Add(new KeyValuePair<string, string>(dr["ItemSpecificsName"].ToString(), dr["ItemSpecificsValue"].ToString()));
                }
            }
        }
        if (result.Count == 0)
        {
            DataTable commDt = Config.ExecuteDataTable(string.Format(@"select p.short_name_for_sys, comm.comment from tb_ebay_system_parts s 
	inner join tb_product p on p.product_serial_no = s.luc_sku 
	inner join tb_ebay_system_part_comment comm on comm.id=s.comment_id
	where s.system_sku='{0}' and p.for_sys=1", sysSku));

            string hdd = "";
            foreach (DataRow dr in commDt.Rows)
            {
                if (dr["comment"].ToString() == "Memory")
                {
                    result.Add(new KeyValuePair<string, string>("Memory", string.IsNullOrEmpty(dr["short_name_for_sys"].ToString()) ? "Not Included" : dr["short_name_for_sys"].ToString()));
                }

                if (dr["comment"].ToString() == "Windows OS")
                {
                    result.Add(new KeyValuePair<string, string>("Operating System", string.IsNullOrEmpty(dr["short_name_for_sys"].ToString()) ? "Not Included" : dr["short_name_for_sys"].ToString()));
                }

                if (dr["comment"].ToString() == "Windows OS")
                {
                    result.Add(new KeyValuePair<string, string>("Hard Drive Capacity", ""));
                }

                if (dr["comment"].ToString() == "CPU")
                {
                    result.Add(new KeyValuePair<string, string>("Processor Type", string.IsNullOrEmpty(dr["short_name_for_sys"].ToString()) ? "Not Included" : dr["short_name_for_sys"].ToString()));
                }

                if (dr["comment"].ToString() == "Optical Drive")
                {
                    result.Add(new KeyValuePair<string, string>("Primary Drive", string.IsNullOrEmpty(dr["short_name_for_sys"].ToString()) ? "Not Included" : dr["short_name_for_sys"].ToString()));
                }

                if (dr["comment"].ToString() == "Video")
                {
                    result.Add(new KeyValuePair<string, string>("Graphics Processing Type", string.IsNullOrEmpty(dr["short_name_for_sys"].ToString()) ? "Not Included" : dr["short_name_for_sys"].ToString()));
                }

                if (dr["comment"].ToString() == "Power Supply")
                {
                    result.Add(new KeyValuePair<string, string>("Power Supply", string.IsNullOrEmpty(dr["short_name_for_sys"].ToString()) ? "Not Included" : dr["short_name_for_sys"].ToString()));
                }

                if (dr["comment"].ToString() == "SSD")
                {
                    result.Add(new KeyValuePair<string, string>("SSD", string.IsNullOrEmpty(dr["short_name_for_sys"].ToString()) ? "Not Included" : dr["short_name_for_sys"].ToString()));
                }
                if (dr["comment"].ToString().IndexOf("Hard Drive") > -1)
                {
                    hdd += dr["short_name_for_sys"].ToString() + " ";
                }
            }
            if (hdd != "")
            {
                result.Add(new KeyValuePair<string, string>("Hard Drive", hdd));
            }
        }
       
        return result;
    }

    /// <summary>
    /// 取得零件，笔记本规格
    /// </summary>
    /// <param name="LucSku"></param>
    /// <returns></returns>
    public static List<KeyValuePair<string, string>> GetPartSpecifics(ProductModel pm)
    {
        List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();

        result.Add(new KeyValuePair<string, string>("UPC", string.IsNullOrEmpty(pm.UPC) ? "Does not apply" : pm.UPC));
        result.Add(new KeyValuePair<string, string>("MPN", string.IsNullOrEmpty(pm.manufacturer_part_number) ? "Does not apply" : pm.manufacturer_part_number));
        if (!string.IsNullOrEmpty(pm.producter_serial_no))
            result.Add(new KeyValuePair<string, string>("Brand", pm.producter_serial_no));

        if (ProductCategoryModel.IsNotebook(pm.menu_child_serial_no))
        {
            if (pm.screen_size > 0M)
            {
                result.Add(new KeyValuePair<string, string>("Screen Size", pm.screen_size.ToString().TrimEnd(new char[] { '0', '.' }) + "\""));
            }

            if (pm.product_ebay_name.ToLower().IndexOf("ultrabook") > -1)
            {
                result.Add(new KeyValuePair<string, string>("Type", "Ultrabook"));
            }

            if (pm.product_ebay_name.ToLower().IndexOf("notebook") > -1 ||
                pm.product_ebay_name.ToLower().IndexOf("laptop") > -1)
            {
                result.Add(new KeyValuePair<string, string>("Type", "Notebook"));
            }

            if (pm.product_ebay_name.ToLower().IndexOf("tablet") > -1)
            {
                result.Add(new KeyValuePair<string, string>("Type", "Tablet"));
            }
            if (pm.product_ebay_name.ToLower().IndexOf("notbook") > -1)
            {
                result.Add(new KeyValuePair<string, string>("Type", "Netbook"));

            }
            if (pm.product_ebay_name.ToLower().IndexOf("w8") > -1 ||
                pm.product_ebay_name.ToLower().IndexOf("win8") > -1)
            {
                result.Add(new KeyValuePair<string, string>("Operating", "Windows 8"));
            }

            if (pm.product_ebay_name.ToLower().IndexOf("w10") > -1 ||
                pm.product_ebay_name.ToLower().IndexOf("win10") > -1)
            {
                result.Add(new KeyValuePair<string, string>("Operating", "Windows 10"));
            }

            if (pm.product_ebay_name.ToLower().IndexOf("w7") > -1
                || pm.product_ebay_name.ToLower().IndexOf("win7") > -1)
            {
                result.Add(new KeyValuePair<string, string>("Operating", "Windows 7"));
            }

            if (pm.product_ebay_name.ToLower().IndexOf("4GB RAM".ToLower()) > -1)
            {
                result.Add(new KeyValuePair<string, string>("Memory", "4 GB"));
            }

            if (pm.product_ebay_name.ToLower().IndexOf("4G RAM".ToLower()) > -1)
            {
                result.Add(new KeyValuePair<string, string>("Memory", "4 GB"));
            }

            if (pm.product_ebay_name.ToLower().IndexOf("6GB RAM".ToLower()) > -1)
            {
                result.Add(new KeyValuePair<string, string>("Memory", "6 GB"));
            }

            if (pm.product_ebay_name.ToLower().IndexOf("6G RAM".ToLower()) > -1)
            {
                result.Add(new KeyValuePair<string, string>("Memory", "6 GB"));
            }

            if (pm.product_ebay_name.ToLower().IndexOf("12GB RAM".ToLower()) > -1)
            {
                result.Add(new KeyValuePair<string, string>("Memory", "12 GB"));
            }

            if (pm.product_ebay_name.ToLower().IndexOf("12G RAM".ToLower()) > -1)
            {
                result.Add(new KeyValuePair<string, string>("Memory", "12 GB"));
            }

            if (pm.product_ebay_name.ToLower().IndexOf("16GB RAM".ToLower()) > -1)
            {
                result.Add(new KeyValuePair<string, string>("Memory", "16 GB"));
            }

            if (pm.product_ebay_name.ToLower().IndexOf("16G RAM".ToLower()) > -1)
            {
                result.Add(new KeyValuePair<string, string>("Memory", "16 GB"));
            }

            if (pm.product_ebay_name.ToLower().IndexOf("8GB RAM".ToLower()) > -1)
            {
                result.Add(new KeyValuePair<string, string>("Memory", "8 GB"));
            }

            if (pm.product_ebay_name.ToLower().IndexOf("8G RAM".ToLower()) > -1)
            {
                result.Add(new KeyValuePair<string, string>("Memory", "8 GB"));
            }

            if (pm.product_ebay_name.ToLower().IndexOf("24GB RAM".ToLower()) > -1)
            {
                result.Add(new KeyValuePair<string, string>("Memory", "24 GB"));
            }

            if (pm.product_ebay_name.ToLower().IndexOf("24G RAM".ToLower()) > -1)
            {
                result.Add(new KeyValuePair<string, string>("Memory", "24 GB"));
            }

            if (pm.product_ebay_name.ToLower().IndexOf("32GB RAM".ToLower()) > -1)
            {
                result.Add(new KeyValuePair<string, string>("Memory", "32 GB"));
            }

            if (pm.product_ebay_name.ToLower().IndexOf("32G RAM".ToLower()) > -1)
            {
                result.Add(new KeyValuePair<string, string>("Memory", "32 GB"));
            }

            if (pm.product_ebay_name.ToLower().IndexOf("128G SSD".ToLower()) > -1)
            {
                result.Add(new KeyValuePair<string, string>("Hard Drive Capacity", "128 GB SSD"));
            }

            if (pm.product_ebay_name.ToLower().IndexOf("120G SSD".ToLower()) > -1)
            {
                result.Add(new KeyValuePair<string, string>("Hard Drive Capacity", "120 GB SSD"));
            }

            if (pm.product_ebay_name.ToLower().IndexOf("240G SSD".ToLower()) > -1)
            {
                result.Add(new KeyValuePair<string, string>("Hard Drive Capacity", "240 GB SSD"));
            }

            if (pm.product_ebay_name.ToLower().IndexOf("512G SSD".ToLower()) > -1)
            {
                result.Add(new KeyValuePair<string, string>("Hard Drive Capacity", "512 GB SSD"));
            }

            if (pm.product_ebay_name.ToLower().IndexOf("1TB SSD".ToLower()) > -1)
            {
                result.Add(new KeyValuePair<string, string>("Hard Drive Capacity", "1TB SSD"));
            }

            if (pm.product_ebay_name.ToLower().IndexOf("320G HDD".ToLower()) > -1)
            {
                result.Add(new KeyValuePair<string, string>("Hard Drive Capacity", "320 GB"));
            }
            if (pm.product_ebay_name.ToLower().IndexOf("500G HDD".ToLower()) > -1)
            {
                result.Add(new KeyValuePair<string, string>("Hard Drive Capacity", "500 GB"));
            }
            if (pm.product_ebay_name.ToLower().IndexOf("640G HDD".ToLower()) > -1)
            {
                result.Add(new KeyValuePair<string, string>("Hard Drive Capacity", "640 GB"));
            }
            if (pm.product_ebay_name.ToLower().IndexOf("750G HDD".ToLower()) > -1)
            {
                result.Add(new KeyValuePair<string, string>("Hard Drive Capacity", "750 GB"));
            }
            if (pm.product_ebay_name.ToLower().IndexOf("1TB HDD".ToLower()) > -1)
            {
                result.Add(new KeyValuePair<string, string>("Hard Drive Capacity", "1 TB"));
            }
            if (pm.product_ebay_name.ToLower().IndexOf("1.5TB HDD".ToLower()) > -1)
            {
                result.Add(new KeyValuePair<string, string>("Hard Drive Capacity", "1.5 TB"));
            }
            if (pm.product_ebay_name.ToLower().IndexOf(" i3".ToLower()) > -1)
            {
                result.Add(new KeyValuePair<string, string>("Processor Type", "Intel Core i3"));
            }
            if (pm.product_ebay_name.ToLower().IndexOf(" i5".ToLower()) > -1)
            {
                result.Add(new KeyValuePair<string, string>("Processor Type", "Intel Core i5"));
            }
            if (pm.product_ebay_name.ToLower().IndexOf(" i7".ToLower()) > -1)
            {
                result.Add(new KeyValuePair<string, string>("Processor Type", "Intel Core i7"));
            }
        }
        return result;
    }
}