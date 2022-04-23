using DownloadEBayOrder.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadEBayOrder.BLL
{
    public class eBaySuggest
    {
        public bool GetMayWeAlsoSuggest(nicklu2Entities context, int partSku, int partCateId, int sysSku)
        {
            List<eBayProdInfo> parts = new List<eBayProdInfo>();
            if (sysSku > 0)
            {
                parts = GetSystemSuggest(context, sysSku);
            }
            if (partSku > 0)
            {
                switch (partCateId)
                {
                    case 350: // 笔记本电脑               
                        parts = GetMyWeAlsoSuggestNotebook(partSku);
                        break;
                    case 31: // 主板
                        parts = GetMyWeAlsoSuggestMotherboard(partSku); break;
                    case 41: //显卡
                        parts = GetMyWeAlsoSuggestVideoCard(partSku); break;
                    case 22: // CPU
                        parts = GetMyWeAlsoSuggestCPU(partSku); break;
                    case 36:// 电源
                        parts = GetMyWeAlsoSuggestPowerSupply(partSku); break;
                    case 25:// hard drive
                        parts = GetMyWeAlsoSuggestHDD(partSku); break;
                    default:
                        parts = GetMyWeAlsoSuggestEbayCateId(partSku); break;
                }
                //return string.Empty;
            }

            var filePath = Config.SysDocuments + "\\eBaySuggest\\";

            if (!System.IO.Directory.Exists(filePath))
            {
                System.IO.Directory.CreateDirectory(filePath);
            }
            filePath += (partSku > 0 ? partSku : sysSku) + ".json";


            System.IO.StreamWriter sw = new System.IO.StreamWriter(filePath, false, Encoding.UTF8);
            sw.Write(Newtonsoft.Json.JsonConvert.SerializeObject(parts));
            sw.Close();
            return true;
        }

        List<eBayProdInfo> GetSystemSuggest(nicklu2Entities context, int sysSku)
        {
            var query = context.Database.SqlQuery<eBayProdInfo>(string.Format(@"select 
            ebay_system_name as Title,
            ebay.BuyItNowPrice as eBayPrice,
            ebay.ItemID as eBayItemId,
            es.id as LUCSku
            from tb_ebay_system es inner
            join tb_ebay_selling ebay on ebay.sys_sku = es.id
            where es.id in (select system_sku from tb_ebay_system_parts where ebayShowIt = 1 and luc_sku = (select max(luc_sku) from tb_ebay_system_parts where system_sku = '{0}' and comment_id = 1))
            order by ebay.BuyItNowPrice asc", sysSku)).ToList();
            for (var i = 0; i < query.Count; i++)
            {
                var title = GetSystemSuggestTitle(context, query[i].LUCSku);

                if (!string.IsNullOrEmpty(title))
                {
                    query[i].Title = title;
                }
            }
            return query;
        }

        /// <summary>
        /// 获取系统建议的产品标题
        /// </summary>
        /// <param name="sysSku"></param>
        /// <returns></returns>
        string GetSystemSuggestTitle(nicklu2Entities context, int sysSku)
        {
            var sql = string.Format(@"select p.short_name_for_sys
from tb_product p inner join tb_ebay_system_parts sp on sp.luc_sku=p.product_serial_no
inner join tb_ebay_system_part_comment es on es.id=sp.comment_id 
inner join tb_ebay_system sys on sys.id=sp.system_sku
where p.short_name_for_sys <> '' and sp.system_sku='" + sysSku + @"'
order by priority asc");
            using (nicklu2Entities DB = new nicklu2Entities())
            {
                var query = DB.Database.SqlQuery<string>(sql).ToList();// Config.ExecuteDataTable(sql);
                var res = string.Join(",", query);


                res = res.Replace(",,", "");
                res = res.TrimEnd(',');

                return res;
            }
        }


        /// <summary>
        /// 笔记本电脑
        /// </summary>
        /// <param name="pm"></param>
        /// <returns></returns>
        List<eBayProdInfo> GetMyWeAlsoSuggestNotebook(int sku)
        {
            using (nicklu2Entities db = new nicklu2Entities())
            {
                var cpuString = db.Database.SqlQuery<string>(string.Format(@"
                SELECT max(replace(replace(replace(lower(ItemSpecificsValue),'intel',''),'amd',''),'core','')) 
                FROM tb_ebay_system_item_specifics where length(system_sku)=5 and ItemSpecificsName='Processor Type' and system_sku={0};", sku))
                    .ToList();
                if (cpuString != null && cpuString.Count > 0 && (cpuString[0] ?? "").Trim() != "")
                {
                    var query = string.Format(@"
                            select distinct es.Title, es.ItemID as eBayItemId, es.BuyItNowPrice as eBayPrice,es.luc_sku as LUCSku, case when p.other_product_sku>0 then p.other_product_sku else p.product_serial_no end ImgSku
                            from 
	                            tb_ebay_system_item_specifics sp 
                                inner join tb_product p on sp.system_sku = p.product_serial_no
                                inner join tb_ebay_selling es on es.luc_sku=sp.system_sku

                            where ItemSpecificsValue like '%{0}%' order by es.BuyItNowPrice asc;
                            ", cpuString[0].Trim());


                    var res = db.Database.SqlQuery<eBayProdInfo>(query).ToList();
                    return res;

                    //var res = new List<eBayProdInfo>();
                    //foreach (DataRow dr in query.Rows)
                    //{
                    //    res.Add(new eBayProdInfo
                    //    {
                    //        eBayItemId = dr["ItemID"].ToString(),
                    //        eBayPrice = decimal.Parse(dr["BuyItNowPrice"].ToString()),
                    //        LUCSku = int.Parse(dr["luc_sku"].ToString()),
                    //        Title = dr["Title"].ToString(),
                    //        ImgSku = int.Parse(dr["ImgSku"].ToString())
                    //    });
                    //}
                    //return res;
                }
                else
                {
                    return new List<eBayProdInfo>();
                }
            }
        }

        /// <summary>
        /// hard drive
        /// </summary>
        /// <param name="pm"></param>
        /// <returns></returns>
        List<eBayProdInfo> GetMyWeAlsoSuggestHDD(int sku)
        {
            using (nicklu2Entities db = new nicklu2Entities())
            {
                var cpuString = db.Database.SqlQuery<string>(string.Format(@"
                SELECT max(ItemSpecificsValue) 
                FROM tb_ebay_system_item_specifics where length(system_sku)=5 and ItemSpecificsName='Brand' and system_sku={0};", sku))
                    .ToList();

                if (cpuString != null && cpuString.Count > 0 && (cpuString[0] ?? "").Trim() != "")
                {
                    var query = string.Format(@"
                            select distinct es.Title, es.ItemID as eBayItemId, es.BuyItNowPrice as eBayPrice,es.luc_sku as LUCSku, case when p.other_product_sku>0 then p.other_product_sku else p.product_serial_no end ImgSku
                            from 
	                            tb_ebay_system_item_specifics sp 
                                inner join tb_product p on sp.system_sku = p.product_serial_no
                                inner join tb_ebay_selling es on es.luc_sku=sp.system_sku

                            where ItemSpecificsValue like '%{0}%' and p.menu_child_serial_no=25 order by es.BuyItNowPrice asc;
                            ", cpuString[0].Trim());


                    var res = db.Database.SqlQuery<eBayProdInfo>(query).ToList();
                    return res;

                    //var res = new List<eBayProdInfo>();
                    //foreach (DataRow dr in query.Rows)
                    //{
                    //    res.Add(new eBayProdInfo
                    //    {
                    //        eBayItemId = dr["ItemID"].ToString(),
                    //        eBayPrice = decimal.Parse(dr["BuyItNowPrice"].ToString()),
                    //        LUCSku = int.Parse(dr["luc_sku"].ToString()),
                    //        Title = dr["Title"].ToString(),
                    //        ImgSku = int.Parse(dr["ImgSku"].ToString())
                    //    });
                    //}
                    //return res;
                }
                else
                {
                    return new List<eBayProdInfo>();
                }
            }
        }

        /// <summary>
        /// 主板
        /// </summary>
        /// <param name="pm"></param>
        /// <returns></returns>
        List<eBayProdInfo> GetMyWeAlsoSuggestMotherboard(int sku)
        {
            using (nicklu2Entities db = new nicklu2Entities())
            {
                var cpuString = db.Database.SqlQuery<string>(string.Format(@"
                SELECT max(ItemSpecificsValue) as ItemSpecificsValue
                FROM tb_ebay_system_item_specifics where length(system_sku)=5 and ItemSpecificsName='Socket Type' and system_sku={0};", sku))
                    .ToList();
                if (cpuString != null && cpuString.Count > 0 && (cpuString[0] ?? "").Trim() != "")
                {
                    var query = string.Format(@"
                            select distinct es.Title, es.ItemID as eBayItemId, es.BuyItNowPrice as eBayPrice,es.luc_sku as LUCSku, case when p.other_product_sku>0 then p.other_product_sku else p.product_serial_no end ImgSku
                            from 
	                            tb_ebay_system_item_specifics sp 
                                inner join tb_product p on sp.system_sku = p.product_serial_no
                                inner join tb_ebay_selling es on es.luc_sku=sp.system_sku

                            where ItemSpecificsValue like '%{0}%' order by es.BuyItNowPrice asc;
                            ", cpuString[0].Trim());


                    var res = db.Database.SqlQuery<eBayProdInfo>(query).ToList();
                    return res;


                    //var res = new List<eBayProdInfo>();
                    //foreach (DataRow dr in query.Rows)
                    //{
                    //    res.Add(new eBayProdInfo
                    //    {
                    //        eBayItemId = dr["ItemID"].ToString(),
                    //        eBayPrice = decimal.Parse(dr["BuyItNowPrice"].ToString()),
                    //        LUCSku = int.Parse(dr["luc_sku"].ToString()),
                    //        Title = dr["Title"].ToString(),
                    //        ImgSku = int.Parse(dr["ImgSku"].ToString())
                    //    });
                    //}
                    //return res;
                }
                else
                {
                    return new List<eBayProdInfo>();
                }
            }
        }

        /// <summary>
        /// 显卡
        /// </summary>
        /// <param name="pm"></param>
        /// <returns></returns>
        List<eBayProdInfo> GetMyWeAlsoSuggestVideoCard(int sku)
        {
            using (nicklu2Entities db = new nicklu2Entities())
            {
                var cpuString = db.Database.SqlQuery<string>(string.Format(@"
                SELECT max(ItemSpecificsValue) as ItemSpecificsValue
                FROM tb_ebay_system_item_specifics where length(system_sku)=5 and ItemSpecificsName='Chipset/GPU Model' and system_sku={0};", sku))
                    .ToList();
                if (cpuString != null && cpuString.Count > 0 && (cpuString[0] ?? "").Trim() != "")
                {
                    var query = string.Format(@"
                            select distinct es.Title, es.ItemID as eBayItemId, es.BuyItNowPrice as eBayPrice,es.luc_sku as LUCSku, case when p.other_product_sku>0 then p.other_product_sku else p.product_serial_no end ImgSku
                            from 
	                            tb_ebay_system_item_specifics sp 
                                inner join tb_product p on sp.system_sku = p.product_serial_no
                                inner join tb_ebay_selling es on es.luc_sku=sp.system_sku

                            where ItemSpecificsValue like '%{0}%' order by es.BuyItNowPrice asc;
                            ", cpuString[0].Trim());

                    var res = db.Database.SqlQuery<eBayProdInfo>(query).ToList();
                    return res;


                    //var res = new List<eBayProdInfo>();
                    //foreach (DataRow dr in query.Rows)
                    //{
                    //    res.Add(new eBayProdInfo
                    //    {
                    //        eBayItemId = dr["ItemID"].ToString(),
                    //        eBayPrice = decimal.Parse(dr["BuyItNowPrice"].ToString()),
                    //        LUCSku = int.Parse(dr["luc_sku"].ToString()),
                    //        Title = dr["Title"].ToString(),
                    //        ImgSku = int.Parse(dr["ImgSku"].ToString())
                    //    });
                    //}
                    //return res;
                }
                else
                {
                    return new List<eBayProdInfo>();
                }
            }
        }

        /// <summary>
        /// CPU
        /// </summary>
        /// <param name="pm"></param>
        /// <returns></returns>
        List<eBayProdInfo> GetMyWeAlsoSuggestCPU(int sku)
        {
            using (nicklu2Entities db = new nicklu2Entities())
            {
                var cpuString = db.Database.SqlQuery<string>(string.Format(@"
                SELECT max(ItemSpecificsValue) as ItemSpecificsValue
                FROM tb_ebay_system_item_specifics where length(system_sku)=5 and ItemSpecificsName='Brand' and system_sku={0};", sku))
                    .ToList();
                if (cpuString != null && cpuString.Count > 0 && (cpuString[0] ?? "").Trim() != "")
                {
                    var query = string.Format(@"
                            select distinct es.Title, es.ItemID as eBayItemId, es.BuyItNowPrice as eBayPrice,es.luc_sku as LUCSku, case when p.other_product_sku>0 then p.other_product_sku else p.product_serial_no end ImgSku
                            from 
	                            tb_ebay_system_item_specifics sp 
                                inner join tb_product p on sp.system_sku = p.product_serial_no
                                inner join tb_ebay_selling es on es.luc_sku=sp.system_sku

                            where ItemSpecificsValue like '%{0}%' and p.menu_child_serial_no=22 order by es.BuyItNowPrice asc;
                            ", cpuString[0].Trim());

                    var res = db.Database.SqlQuery<eBayProdInfo>(query).ToList();
                    return res;


                    //var res = new List<eBayProdInfo>();
                    //foreach (DataRow dr in query.Rows)
                    //{
                    //    res.Add(new eBayProdInfo
                    //    {
                    //        eBayItemId = dr["ItemID"].ToString(),
                    //        eBayPrice = decimal.Parse(dr["BuyItNowPrice"].ToString()),
                    //        LUCSku = int.Parse(dr["luc_sku"].ToString()),
                    //        Title = dr["Title"].ToString(),
                    //        ImgSku = int.Parse(dr["ImgSku"].ToString())
                    //    });
                    //}
                    //return res;
                }
                else
                {
                    return new List<eBayProdInfo>();
                }
            }

        }

        /// <summary>
        /// 电源 
        /// </summary>
        /// <param name="pm"></param>
        /// <returns></returns>
        List<eBayProdInfo> GetMyWeAlsoSuggestPowerSupply(int sku)
        {
            using (nicklu2Entities db = new nicklu2Entities())
            {
                System.IO.File.WriteAllText("C:\\Workspaces\\tt.txt", sku.ToString());
                var cpuString = db.Database.SqlQuery<string>(string.Format(@"
                SELECT max(ItemSpecificsValue) as ItemSpecificsValue
                FROM tb_ebay_system_item_specifics where length(system_sku)=5 and ItemSpecificsName='Max. Output Power' and system_sku={0};", sku))
                    .ToList();
                if (cpuString != null && cpuString.Count > 0 && (cpuString[0] ?? "").Trim() != "")
                {
                    var query = string.Format(@"
                            select distinct es.Title, es.ItemID as eBayItemId, es.BuyItNowPrice as eBayPrice,es.luc_sku as LUCSku, case when p.other_product_sku>0 then p.other_product_sku else p.product_serial_no end ImgSku
                            from 
	                            tb_ebay_system_item_specifics sp 
                                inner join tb_product p on sp.system_sku = p.product_serial_no
                                inner join tb_ebay_selling es on es.luc_sku=sp.system_sku

                            where ItemSpecificsValue like '%{0}%' order by es.BuyItNowPrice asc;
                            ", (cpuString[0] ?? "").Trim());


                    var res = db.Database.SqlQuery<eBayProdInfo>(query).ToList();
                    return res;


                    //var res = new List<eBayProdInfo>();
                    //foreach (DataRow dr in query.Rows)
                    //{
                    //    res.Add(new eBayProdInfo
                    //    {
                    //        eBayItemId = dr["ItemID"].ToString(),
                    //        eBayPrice = decimal.Parse(dr["BuyItNowPrice"].ToString()),
                    //        LUCSku = int.Parse(dr["luc_sku"].ToString()),
                    //        Title = dr["Title"].ToString(),
                    //        ImgSku = int.Parse(dr["ImgSku"].ToString())
                    //    });
                    //}
                    //return res;
                }
                else
                {
                    return new List<eBayProdInfo>();
                }
            }
        }


        /// <summary>
        ///  按ebay cateid 分
        /// </summary>
        /// <param name="pm"></param>
        /// <returns></returns>
        List<eBayProdInfo> GetMyWeAlsoSuggestEbayCateId(int sku)
        {
            //var cpuString = Config.ExecuteScalar(string.Format(@"
            //        SELECT max(ItemSpecificsValue) as ItemSpecificsValue
            //        FROM tb_ebay_system_item_specifics where length(system_sku)=5 and ItemSpecificsName='Max. Output Power' and system_sku={0};", pm.product_serial_no))
            //        .ToString();
            if (true)
            {
                var sql = string.Format(@"
                            select distinct es.Title, es.ItemID as eBayItemId, es.BuyItNowPrice as eBayPrice,es.luc_sku as LUCSku, case when p.other_product_sku>0 then p.other_product_sku else p.product_serial_no end ImgSku
                            from 
	                            tb_ebay_system_item_specifics sp 
                                inner join tb_product p on sp.system_sku = p.product_serial_no
                                inner join tb_ebay_selling es on es.luc_sku=sp.system_sku
                                inner join (select sku from tb_ebay_category_and_product where eBayCateID_1 in (SELECT eBayCateID_1 FROM nicklu2.tb_ebay_category_and_product where sku={0})) as sku on sku.sku=sp.system_sku
                            where 1=1 order by es.BuyItNowPrice asc;
                            ", sku);
                using (nicklu2Entities DB = new nicklu2Entities())
                {
                    var query = DB.Database.SqlQuery<eBayProdInfo>(sql).ToList();

                    ////var res = new List<eBayProdInfo>();
                    //foreach (DataRow dr in query.Rows)
                    //////{
                    //    res.Add(new eBayProdInfo
                    //    {
                    //        eBayItemId = dr["ItemID"].ToString(),
                    //        eBayPrice = decimal.Parse(dr["BuyItNowPrice"].ToString()),
                    //        LUCSku = int.Parse(dr["luc_sku"].ToString()),
                    //        Title = dr["Title"].ToString(),
                    //        ImgSku = int.Parse(dr["ImgSku"].ToString())
                    //    });
                    //}

                    return query;
                }
            }
            //else
            //{
            //    return new List<eBayProdInfo>();
            //}
        }

    }
}
