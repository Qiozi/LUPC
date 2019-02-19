using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using LU.Data;

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
        var context = new nicklu2Entities();
        List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();

        result.Add(new KeyValuePair<string, string>("Brand", "Custom, Whitebox"));
        result.Add(new KeyValuePair<string, string>("MPN", "LUComputers " + sysSku.ToString()));
        result.Add(new KeyValuePair<string, string>("UPC", "Does not apply"));
        result.Add(new KeyValuePair<string, string>("Warranty", "Standard (1) year parts warranty"));

        // DataTable dt = Config.ExecuteDataTable("Select ItemSpecificsName, ItemSpecificsValue from tb_ebay_system_item_specifics where system_sku='" + sysSku + "'");

        EbayItem ei = new EbayItem();
        //foreach (DataRow dr in dt.Rows)
        //{
        //    if (!string.IsNullOrEmpty(dr["ItemSpecificsValue"].ToString()))
        //    {
        //        if (dr["ItemSpecificsName"].ToString().ToLower() != "brand")
        //        {
        //            result.Add(new KeyValuePair<string, string>(dr["ItemSpecificsName"].ToString(), dr["ItemSpecificsValue"].ToString()));
        //        }
        //    }
        //}
        if (result.Count == 4)
        {
            DataTable commDt = Config.ExecuteDataTable(string.Format(@"select p.short_name_for_sys, comm.comment, p.product_serial_no,p.producter_serial_no from tb_ebay_system_parts s 
	inner join tb_product p on p.product_serial_no = s.luc_sku 
	inner join tb_ebay_system_part_comment comm on comm.id=s.comment_id
	where s.system_sku='{0}' and p.for_sys=1", sysSku));

            string hdd = "";
            var storageType = "";

            foreach (DataRow dr in commDt.Rows)
            {
                var comment = dr["comment"].ToString().Trim();
                var partSku = int.Parse(dr["product_serial_no"].ToString());
                if (comment == "Memory")
                {
                    result.Add(new KeyValuePair<string, string>("Memory", string.IsNullOrEmpty(dr["short_name_for_sys"].ToString()) ? "Not Included" : dr["short_name_for_sys"].ToString()));
                }

                if (comment == "Windows OS")
                {
                    result.Add(new KeyValuePair<string, string>("Operating System", string.IsNullOrEmpty(dr["short_name_for_sys"].ToString()) ? "Not Included" : dr["short_name_for_sys"].ToString()));
                }

                if (comment == "Case")
                {
                    result.Add(new KeyValuePair<string, string>("Case", string.IsNullOrEmpty(dr["producter_serial_no"].ToString()) ? "Not Included" : dr["producter_serial_no"].ToString()));
                }

                if (comment == "CPU")
                {
                    result.Add(new KeyValuePair<string, string>("Processor Type", string.IsNullOrEmpty(dr["short_name_for_sys"].ToString()) ? "Not Included" : dr["short_name_for_sys"].ToString()));
                }

                if (comment == "Optical Drive")
                {
                    result.Add(new KeyValuePair<string, string>("Primary Drive", string.IsNullOrEmpty(dr["short_name_for_sys"].ToString()) ? "Not Included" : dr["short_name_for_sys"].ToString()));
                }

                if (comment == "Video")
                {
                    result.Add(new KeyValuePair<string, string>("Graphics Processing Type", string.IsNullOrEmpty(dr["short_name_for_sys"].ToString()) ? "Not Included" : dr["short_name_for_sys"].ToString()));
                }

                if (comment == "Power Supply")
                {
                    result.Add(new KeyValuePair<string, string>("Power Supply", string.IsNullOrEmpty(dr["short_name_for_sys"].ToString()) ? "Not Included" : dr["short_name_for_sys"].ToString()));
                }

                if (comment.IndexOf("SSD") > -1)
                {
                    if (string.IsNullOrEmpty(storageType)&& !string.IsNullOrEmpty(dr["short_name_for_sys"].ToString()))
                    {
                        storageType = "SSD (Solid State Drive)";
                    }
                    else if (storageType.IndexOf("HDD") > -1)
                    {
                        storageType = "HDD + SSD";
                    }
                    result.Add(new KeyValuePair<string, string>("SSD Capacity", string.IsNullOrEmpty(dr["short_name_for_sys"].ToString()) ? "Not Included" : dr["short_name_for_sys"].ToString()));
                }
                if (comment.IndexOf("Hard Drive") > -1)
                {
                    if (string.IsNullOrEmpty(storageType) && !string.IsNullOrEmpty(dr["short_name_for_sys"].ToString()))
                    {
                        storageType = "HDD";
                    }
                    else if (storageType.IndexOf("SSD") > -1)
                    {
                        storageType = "HDD + SSD";
                    }
                    if (string.IsNullOrEmpty(hdd))
                    {
                        hdd += dr["short_name_for_sys"].ToString();
                    }
                    else
                    {
                        hdd += " + " + dr["short_name_for_sys"].ToString();
                    }
                }
                //if (dr["comment"].ToString().IndexOf("Hard Drive") > -1)
                //{
                //    hdd += dr["short_name_for_sys"].ToString() + ", ";
                //}
                //Hardware Connectivity
                if (comment.IndexOf("Motherboard") > -1)
                {
                    var specificial = context.tb_ebay_system_item_specifics.FirstOrDefault(p => p.system_sku.Equals(partSku) && p.ItemSpecificsName.Equals("Input/Output Ports"));
                    if (specificial != null)
                    {
                        if (!string.IsNullOrEmpty(specificial.ItemSpecificsValue))
                        {
                            result.Add(new KeyValuePair<string, string>("Hardware Connectivity", specificial.ItemSpecificsValue));
                        }
                    }
                }
            }
            if (hdd != "" && hdd.Replace(",", "").Replace(" ", "") != "")
            {
                result.Add(new KeyValuePair<string, string>("HDD Capacity", hdd));
            }
            if (!string.IsNullOrEmpty(storageType))
            {
                result.Add(new KeyValuePair<string, string>("Storage Type", storageType));
            }
        }

        return result;
    }

    public static List<KeyValuePair<string, string>> GetSpecifices(LU.Data.nicklu2Entities context, int sku)
    {
        var query = context.tb_ebay_system_item_specifics.Where(p => p.system_sku == sku).ToList();
        var result = new List<KeyValuePair<string, string>>();
        foreach (var item in query)
        {
            if (!string.IsNullOrEmpty(item.ItemSpecificsValue))
                result.Add(new KeyValuePair<string, string>(item.ItemSpecificsName, item.ItemSpecificsValue));
        }
        return result;
    }

    /// <summary>
    /// 取得零件，笔记本规格
    /// </summary>
    /// <param name="LucSku"></param>
    /// <returns></returns>
    public static List<KeyValuePair<string, string>> GetPartSpecifics(nicklu2Entities context, tb_product pm)
    {
        var querySpecifics = GetSpecifices(context, pm.product_serial_no);
        if (querySpecifics.Count > 0)
        {
            return querySpecifics;
        }

        List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();

        result.Add(new KeyValuePair<string, string>("UPC", string.IsNullOrEmpty(pm.UPC) ? "Does not apply" : pm.UPC));
        result.Add(new KeyValuePair<string, string>("MPN", string.IsNullOrEmpty(pm.manufacturer_part_number) ? "Does not apply" : pm.manufacturer_part_number));

        if (!string.IsNullOrEmpty(pm.producter_serial_no))
            result.Add(new KeyValuePair<string, string>("Brand", pm.producter_serial_no));

        if (ProductCategoryModel.IsNotebook(context, pm.menu_child_serial_no.Value))
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
                result.Add(new KeyValuePair<string, string>("SSD Capacity", "128 GB SSD"));
            }

            if (pm.product_ebay_name.ToLower().IndexOf("120G SSD".ToLower()) > -1)
            {
                result.Add(new KeyValuePair<string, string>("SSD Capacity", "120 GB SSD"));
            }

            if (pm.product_ebay_name.ToLower().IndexOf("240G SSD".ToLower()) > -1)
            {
                result.Add(new KeyValuePair<string, string>("SSD Capacity", "240 GB SSD"));
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