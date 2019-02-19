using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace DownloadEBayOrder.BLL
{
    public class eBayHelper : Events.EventBase
    {
        public eBayHelper() { }

        /// <summary>
        /// 修改所有产品描述
        /// </summary>
        /// <param name="context"></param>
        /// <param name="log"></param>
        public void ModifyAllDesc(nicklu2Entities context, Logs log)
        {
            ModifyAllPartsDesc(context, log);
            ModifyAllSysDesc(context, log);
        }



        public void ModifyAllPartsDesc(nicklu2Entities context, Logs log)
        {
            var suggest = new eBaySuggest();
            var items = new List<ebayModifyPriceItem>();
            var list = (from s in context.tb_ebay_selling
                        join p in context.tb_product on s.luc_sku.Value equals p.product_serial_no
                        where s.luc_sku.HasValue && s.luc_sku.Value > 0
                        && s.BuyItNowPrice.HasValue
                        orderby s.BuyItNowPrice ascending
                        select new
                        {
                            SKU = p.product_serial_no,
                            ItemId = s.ItemID,
                            OldSold = s.BuyItNowPrice.Value,
                            Cost = p.product_current_cost.Value,
                            Screen = p.screen_size.HasValue ? p.screen_size.Value : 0M,
                            Adjustment = p.adjustment.HasValue ? p.adjustment.Value : 0M,
                            Qty = p.ltd_stock.HasValue ? p.ltd_stock.Value : 0,
                            CateId = p.menu_child_serial_no
                        }).ToList();
            int count = 0;
            foreach (var part in list)
            {
                count++;
                var neweBay = NewEbayPrice(part.Cost, part.Screen, part.Adjustment, part.SKU, log);
                ModifyEbayDesc(part.Cost, neweBay.profit, neweBay.ebay_fee, neweBay.shipping_fee, neweBay.ebayPrice, part.ItemId, false);
                suggest.GetMayWeAlsoSuggest(context, part.SKU, part.CateId.HasValue ? part.CateId.Value : 0, 0);
                SetStatus("modify ebay part desc:" + part.SKU + "(" + count.ToString() + "/" + list.Count.ToString() + ")");
            }
        }

        public void ModifyAllSysDesc(nicklu2Entities context, Logs log)
        {
            var suggest = new eBaySuggest();
            var items = new List<ebayModifyPriceItem>();
            var list = (from s in context.tb_ebay_selling
                        join p in context.tb_ebay_system on s.sys_sku.Value equals p.id
                        where s.sys_sku.HasValue && s.sys_sku.Value > 0
                        && s.BuyItNowPrice.HasValue
                        && p.is_shrink.HasValue && p.is_shrink.Value.Equals(false)
                        select new
                        {
                            SKU = p.id,
                            ItemId = s.ItemID,
                            OldSold = s.BuyItNowPrice.Value,
                            Adjustment = p.adjustment.HasValue ? p.adjustment.Value : 0M,
                            isShrink = p.is_shrink.Value
                        }).ToList();

            int count = 0;
            foreach (var part in list)
            {
                count++;
                var neweBay = NewEbaySystemPrice(part.isShrink, part.Adjustment, part.SKU);
                ModifyEbayDesc(neweBay.cost, neweBay.profit, neweBay.ebay_fee, neweBay.shipping_fee, neweBay.ebayPrice, part.ItemId, true);
                suggest.GetMayWeAlsoSuggest(context, 0, 0, part.SKU);
                SetStatus("modify ebay system desc:" + part.SKU + "(" + count.ToString() + "/" + list.Count.ToString() + ")");
            }
        }

        /// <summary>
        /// 修改零件价格
        /// </summary>
        public void ModifyPartEbayPrice(nicklu2Entities context, Logs log)
        {
            var items = new List<ebayModifyPriceItem>();
            var list = (from s in context.tb_ebay_selling
                        join p in context.tb_product on s.luc_sku.Value equals p.product_serial_no
                        where s.luc_sku.HasValue && s.luc_sku.Value > 0
                        && s.BuyItNowPrice.HasValue
                        select new
                        {
                            SKU = p.product_serial_no,
                            ItemId = s.ItemID,
                            OldSold = s.BuyItNowPrice.Value,
                            Cost = p.product_current_cost.Value,
                            Screen = p.screen_size.HasValue ? p.screen_size.Value : 0M,
                            Adjustment = p.adjustment.HasValue ? p.adjustment.Value : 0M,
                            Qty = p.ltd_stock.HasValue ? p.ltd_stock.Value : 0

                        }).ToList();

            var updateCount = 0;
            var endCount = 0;
            var warnCount = 0;
            foreach (var part in list)
            {
                SetStatus("modify ebay part price:" + part.SKU);
                var type = eBayModifyPriceType.None;
                var neweBayPrice = 0M;
                try
                {
                    var neweBay = NewEbayPrice(part.Cost, part.Screen, part.Adjustment, part.SKU, log);
                    neweBayPrice = neweBay.ebayPrice;
                    var diffPrice = neweBayPrice - part.OldSold;
                    bool upPrice = false;
                    if (part.Qty < 1)
                    {
                        var partComment = context.tb_ebay_part_comment.Count(p => p.part_sku.HasValue && p.part_sku.Value.Equals(part.SKU)
                                  && !string.IsNullOrEmpty(p.ebay_note) && !p.ebay_note.Equals("ebay on sale"));
                        if (partComment == 0) // 有备注，不更新。
                        {
                            DeleteEbayItem(part.ItemId);
                            type = eBayModifyPriceType.Delete;
                            endCount += 1;
                        }
                    }
                    else if (diffPrice > 0)
                    {
                        upPrice = true;
                    }
                    else if (diffPrice < 0 && part.Qty > 2)
                    {
                        if (diffPrice > -200) // 降价超过200， 
                        {
                            upPrice = true;
                        }
                        else
                        {
                            warnCount += 1;
                        }
                    }

                    if (upPrice)
                    {
                        var partComment = context.tb_ebay_part_comment.Count(p => p.part_sku.HasValue && p.part_sku.Value.Equals(part.SKU)
                              && !string.IsNullOrEmpty(p.ebay_note) && !p.ebay_note.Equals("ebay on sale"));
                        if (partComment == 0) // 有备注，不更新。
                        {
                            ModifyEbayPrice(part.Cost, neweBay.profit, neweBay.ebay_fee, neweBay.shipping_fee, neweBay.ebayPrice, part.ItemId);
                            type = eBayModifyPriceType.Modify;
                            updateCount += 1;
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.WriteErrorLog(ex);
                }

                if (type != eBayModifyPriceType.None)
                {
                    var item = new ebayModifyPriceItem()
                    {
                        Cost = part.Cost,
                        eBayItemId = part.ItemId,
                        LuSku = part.SKU,
                        NewSold = neweBayPrice,
                        OldSold = part.OldSold,
                        Qty = part.Qty,
                        Type = type.ToString()
                    };
                    WriteLog(item, log);
                }
            }
            var title = string.Format("part: delete:{0}, warning:{1}, modify:{2}", endCount, warnCount, updateCount);

            EmailHelper.Send("terryeah@gmail.com", title, title);
            //SetStatus(title);
        }

        static void WriteLog(ebayModifyPriceItem item, Logs log)
        {
            string json = JsonConvert.SerializeObject(item);
            log.WriteLog(json);
        }

        /// <summa
        /// <summary>
        /// 修改系统价格
        /// </summary>
        public void ModifySystemEbayPrice(nicklu2Entities context, Logs log)
        {
            // 把没有库存的零件掦除
            RemovePartQuantityIsZone(context, log);

            #region run
            var items = new List<ebayModifyPriceItem>();
            var list = (from s in context.tb_ebay_selling
                        join p in context.tb_ebay_system on s.sys_sku.Value equals p.id
                        where s.sys_sku.HasValue && s.sys_sku.Value > 0
                        && s.BuyItNowPrice.HasValue
                        && p.is_shrink.HasValue && p.is_shrink.Value.Equals(false)
                        select new
                        {
                            SKU = p.id,
                            ItemId = s.ItemID,
                            OldSold = s.BuyItNowPrice.Value,
                            Adjustment = p.adjustment.HasValue ? p.adjustment.Value : 0M,
                            isShrink = p.is_shrink.Value
                        }).ToList();

            var updateCount = 0;
            var endCount = 0;
            var warnCount = 0;
            foreach (var part in list)
            {
                SetStatus("modify system ebay price. " + part.SKU.ToString() + "  eBay ItemId:" + part.ItemId);

                var type = eBayModifyPriceType.None;
                var neweBayPrice = 0M;
                try
                {
                    var neweBay = NewEbaySystemPrice(part.isShrink, part.Adjustment, part.SKU);
                    neweBayPrice = neweBay.ebayPrice;
                    var diffPrice = neweBayPrice - part.OldSold;
                    if (diffPrice != 0)
                    {
                        if (diffPrice > -200) // 降价超过200， 
                        {
                            if ((diffPrice < -3 || diffPrice > 3) && !neweBay.warn)
                            {
                                ModifyEbaySystemPrice(neweBay.cost
                                    , neweBay.profit
                                    , neweBay.ebay_fee
                                    , neweBay.shipping_fee
                                    , neweBay.ebayPrice
                                    , part.ItemId);
                                type = eBayModifyPriceType.Modify;
                                updateCount += 1;
                            }
                        }
                        else
                        {
                            warnCount += 1;
                        }
                    }
                    if (neweBay.warn)
                    {
                        warnCount += 1;
                    }
                }
                catch (Exception ex)
                {
                    log.WriteErrorLog(ex);
                }

                if (type != eBayModifyPriceType.None)
                {
                    var item = new ebayModifyPriceItem()
                    {
                        Cost = 0M,
                        eBayItemId = part.ItemId,
                        LuSku = part.SKU,
                        NewSold = neweBayPrice,
                        OldSold = part.OldSold,
                        Qty = 6,
                        Type = "system: " + type.ToString()
                    };
                    WriteLog(item, log);
                }
            }
            #endregion

            var title = string.Format("system: warning:{1}, modify:{2}", endCount, warnCount, updateCount);
            EmailHelper.Send("terryeah@gmail.com", title, title);
            //  SetStatus(title);
            /**/
            // 生成系统配置文件
            WriteSystemParts(context, log);
        }


        /// <summary>
        /// 把没有库存的零件掦除。
        /// </summary>
        public void RemovePartQuantityIsZone(nicklu2Entities context, Logs log)
        {
            // 取得part group id
            var sysPartGroupIds = context.tb_ebay_system_parts.Select(p => p.part_group_id.Value).Distinct().ToList();

            // 取得part sku
            var querySku = context.tb_part_group_detail.Where(p => sysPartGroupIds.Contains(p.part_group_id.Value))
                .Select(p => p.product_serial_no).ToList();

            // 取得part
            var queryParts = (from p in context.tb_product
                              where querySku.Contains(p.product_serial_no)
                              select new
                              {
                                  qty = p.product_store_sum.HasValue ? p.product_store_sum.Value : 0,
                                  p.product_serial_no
                              }).ToList();

            //select distinct ip.luc_sku, count(ip.luc_sku) c from tb_other_inc_part_info ip	where date_format(now(), '%y%j')-date_format(last_regdate, '%y%j') <15 group by ip.luc_sku 
            var lessDate = DateTime.Now.AddDays(-15);
            var uriClear = BLL.WebUrl.ClearPartForSys();
            var resultClear = BLL.WebClientHelper.GetPage(uriClear);

            foreach (var part in queryParts)
            {
                var qty = part.qty;
                if (qty < 1)
                {
                    qty = context.tb_other_inc_part_info.Count(p => p.other_inc_store_sum.HasValue &&
                          p.other_inc_store_sum.Value > 0 && p.last_regdate.HasValue &&
                          p.last_regdate.Value > lessDate && p.luc_sku.Value.Equals(part.product_serial_no));
                    log.WriteLog(string.Format("{0} {1}", part.product_serial_no, qty));
                }
                SetStatus(string.Format("{0} remove zone qty : {1}", part.product_serial_no, qty));
                var uri = BLL.WebUrl.RemovePartNoStock(part.product_serial_no, qty);
                var result = BLL.WebClientHelper.GetPage(uri);
                if (result.ToLower().Trim() != "ok")
                {
                    log.WriteLog(string.Format("{0} remove zone qty ERROR: {1}", part.product_serial_no, qty));
                }
            }
        }

        /// <summary>
        /// 把系统配置生成文件缓存
        /// </summary>
        public void WriteSystemParts(nicklu2Entities context, Logs log)
        {
            var querySku = (from s in context.tb_ebay_selling
                            join sys in context.tb_ebay_system on s.sys_sku.Value equals sys.id
                            where s.sys_sku.HasValue
                            && s.sys_sku.Value > 0
                            && sys.is_shrink.HasValue
                            && sys.is_shrink.Value.Equals(false)
                            select new
                            {
                                s.sys_sku.Value
                            }).ToList();
            foreach (var sys in querySku)
            {
                SetStatus(string.Format("{0} generate file.", sys.Value));
                var uri = BLL.WebUrl.GetGenerateSystemPartFile(sys.Value);
                var result = BLL.WebClientHelper.GetPage(uri);
                if (result.ToLower().Trim() != "ok")
                {
                    log.WriteLog(string.Format("generate file ERROR: {1}", sys.Value));
                }
            }

            // SetStatus("generate file end.");

            new BLL.GenerateNewSiteProductInfo().GenerateListData(context);

            new BLL.GenerateNewSiteProductInfo().DeletePartPriceCanche();
        }

        ///<summary>
        /// 下架（ebay)
        /// </summary>
        /// <param name="itemid"></param>
        static void DeleteEbayItem(string itemid)
        {
            string uri = BLL.WebUrl.GeteBayPartEndUrl(itemid);
            BLL.WebClientHelper.GetPage(uri);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sku"></param>
        static PriceItem NewEbayPrice(decimal cost, decimal screen, decimal adjustment, int sku, Logs log)
        {
            string uri = BLL.WebUrl.GeteBayPriceUrl(cost, screen, adjustment, sku);
            log.WriteLog(uri);
            string priceContent = BLL.WebClientHelper.GetPage(uri);
            var priceItem = JsonConvert.DeserializeObject<List<PriceItem>>(priceContent);
            return priceItem[0];
        }

        /// <summary>
        /// 系统 的价格
        /// </summary>
        /// <param name="sku"></param>
        static SystemPriceItem NewEbaySystemPrice(bool isShrink, decimal adjustment, int sysSku)
        {
            string uri = BLL.WebUrl.GeteBaySystemPriceUrl(isShrink, adjustment, sysSku);

            string priceContent = BLL.WebClientHelper.GetPage(uri);

            var priceItem = JsonConvert.DeserializeObject<List<SystemPriceItem>>(priceContent);
            return priceItem[0];
        }

        /// <summary>
        /// 修改系统价格
        /// </summary>
        /// <param name="cost"></param>
        /// <param name="profit"></param>
        /// <param name="ebayfee"></param>
        /// <param name="shippingfee"></param>
        /// <param name="price"></param>
        /// <param name="itemid"></param>
        /// <returns></returns>
        static string ModifyEbaySystemPrice(decimal cost, decimal profit, decimal ebayfee, decimal shippingfee, decimal price, string itemid)
        {
            var url = BLL.WebUrl.GeteBaySystemModifyPriceUrl(cost, profit, ebayfee, shippingfee, price, itemid);

            string priceContent = BLL.WebClientHelper.GetPage(url);

            return priceContent;
        }

        /// <summary>
        /// 修改零件价格
        /// </summary>
        /// <param name="cost"></param>
        /// <param name="profit"></param>
        /// <param name="ebayfee"></param>
        /// <param name="shippingfee"></param>
        /// <param name="price"></param>
        /// <param name="itemid"></param>
        /// <returns></returns>
        static string ModifyEbayPrice(decimal cost, decimal profit, decimal ebayfee
            , decimal shippingfee, decimal price, string itemid)
        {
            var url = BLL.WebUrl.GeteBayModifyPriceUrl(cost, profit, ebayfee, shippingfee, price, itemid);

            string priceContent = BLL.WebClientHelper.GetPage(url);

            return priceContent;
        }

        /// <summary>
        /// 修改描述 
        /// </summary>
        /// <param name="cost"></param>
        /// <param name="profit"></param>
        /// <param name="ebayfee"></param>
        /// <param name="shippingfee"></param>
        /// <param name="price"></param>
        /// <param name="itemid"></param>
        /// <param name="isSys"></param>
        /// <returns></returns>
        static string ModifyEbayDesc(decimal cost, decimal profit, decimal ebayfee
            , decimal shippingfee, decimal price, string itemid, bool isSys)
        {
            var url = BLL.WebUrl.GeteBayModifyPriceUrlWithDesc(cost, profit, ebayfee, shippingfee, price, itemid, isSys);

            string priceContent = BLL.WebClientHelper.GetPage(url);

            return priceContent;
        }

        /// <summary>
        /// 网站下载ebay的价格是否完全执行。
        /// </summary>
        /// <returns></returns>
        //static bool CanRunOK()
        //{
        //    //System.IO.StreamReader sw2 = new System.IO.StreamReader("C:\\LUComputer\\Web\\soft_img\\match_ebay_price.txt");
        //    //var content = sw2.ReadToEnd();
        //    //sw2.Close();
        //    //return content.ToLower().IndexOf("ok") > -1;
        //}
    }
}
