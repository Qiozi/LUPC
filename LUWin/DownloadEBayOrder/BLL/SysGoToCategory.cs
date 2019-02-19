using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DownloadEBayOrder.BLL
{
    public class SysGoToCategory
    {
        public static void Run(nicklu2Entities context)
        {
            var sysList = (from s in context.tb_ebay_selling
                           where s.sys_sku.HasValue && s.sys_sku.Value > 0
                           select new
                           {
                               s.Title,
                               s.sys_sku
                           }).ToList();
            foreach (var sysku in sysList)
            {
                var title = sysku.Title;
                var sysSku = sysku.sys_sku.Value;
                var cateId = GetSysOnWebCategory(context, title, sysSku);

                var sysCate = context.tb_ebay_system_and_category.SingleOrDefault(p => p.SystemSku.HasValue && p.SystemSku.Value.Equals(sysSku));
                if (sysCate == null)
                {
                    //context.AddTotb_ebay_system_and_category(new tb_ebay_system_and_category()
                    context.tb_ebay_system_and_category.Add(new tb_ebay_system_and_category
                    {
                        SystemSku = sysSku,
                        eBaySysCategoryID = cateId
                    });
                    context.SaveChanges();
                }
            }
        }

        static int GetSysOnWebCategory(nicklu2Entities context, string title, int sysSku)
        {
            var videoCommentIds = new int[] { 4, 27, 28, 29, 30 };

            if (title.ToLower().IndexOf("i3") > -1)
            {
                if (title.ToLower().IndexOf("barebone") > -1)
                {
                    return 417;
                }

                var sysPart = context.tb_ebay_system_parts.Where(s => videoCommentIds.Contains(s.comment_id.Value) && s.system_sku.HasValue && s.system_sku.Value.Equals(sysSku))
                    .Select(s => s.luc_sku).ToList();
                foreach (var sku in sysPart)
                {
                    if (sku == 16684)
                    {
                        return 439;
                    }
                    else
                        return 428;
                }
            }
            else if (title.ToLower().IndexOf("i5") > -1)
            {
                if (title.ToLower().IndexOf("barebone") > -1)
                {
                    return 418;
                }
                var sysPart = context.tb_ebay_system_parts.Where(s => videoCommentIds.Contains(s.comment_id.Value) && s.system_sku.HasValue && s.system_sku.Value.Equals(sysSku))
                     .Select(s => s.luc_sku).ToList();
                foreach (var sku in sysPart)
                {
                    if (sku == 16684)
                    {
                        return 440;
                    }
                    else
                        return 429;
                }
            }
            else if (title.ToLower().IndexOf("i7") > -1)
            {
                if (title.ToLower().IndexOf("barebone") > -1)
                {
                    return 420;
                }
                var sysPart = context.tb_ebay_system_parts.Where(s => videoCommentIds.Contains(s.comment_id.Value) && s.system_sku.HasValue && s.system_sku.Value.Equals(sysSku))
                      .Select(s => s.luc_sku).ToList();
                foreach (var sku in sysPart)
                {
                    if (sku == 16684)
                    {
                        return 442;
                    }
                    else
                        return 431;
                }
            }
            else if (title.ToLower().IndexOf("celeron") > -1)
            {
                if (title.ToLower().IndexOf("barebone") > -1)
                {
                    return 415;
                }
                var sysPart = context.tb_ebay_system_parts.Where(s => videoCommentIds.Contains(s.comment_id.Value) && s.system_sku.HasValue && s.system_sku.Value.Equals(sysSku))
                       .Select(s => s.luc_sku).ToList();
                foreach (var sku in sysPart)
                {
                    if (sku == 16684)
                    {
                        return 437;
                    }
                    else
                        return 426;
                }
            }
            else if (title.ToLower().IndexOf("pentium") > -1)
            {
                if (title.ToLower().IndexOf("barebone") > -1)
                {
                    return 416;
                }
                var sysPart = context.tb_ebay_system_parts.Where(s => videoCommentIds.Contains(s.comment_id.Value) && s.system_sku.HasValue && s.system_sku.Value.Equals(sysSku))
                        .Select(s => s.luc_sku).ToList();
                foreach (var sku in sysPart)
                {
                    if (sku == 16684)
                    {
                        return 438;
                    }
                    else
                        return 427;
                }
            }
            else if (title.ToLower().IndexOf("athlon") > -1)
            {
                if (title.ToLower().IndexOf("barebone") > -1)
                {
                    return 422;
                }
                var sysPart = context.tb_ebay_system_parts.Where(s => videoCommentIds.Contains(s.comment_id.Value) && s.system_sku.HasValue && s.system_sku.Value.Equals(sysSku))
                        .Select(s => s.luc_sku).ToList();
                foreach (var sku in sysPart)
                {
                    if (sku == 16684)
                    {
                        return 444;
                    }
                    else
                        return 433;
                }
            }
            else if (title.ToLower().IndexOf("fx") > -1)
            {
                if (title.ToLower().IndexOf("barebone") > -1)
                {
                    return 425;
                }
                var sysPart = context.tb_ebay_system_parts.Where(s => videoCommentIds.Contains(s.comment_id.Value) && s.system_sku.HasValue && s.system_sku.Value.Equals(sysSku))
                         .Select(s => s.luc_sku).ToList();
                foreach (var sku in sysPart)
                {
                    if (sku == 16684)
                    {
                        return 447;
                    }
                    else
                        return 436;
                }
            }
            else if (title.ToLower().IndexOf("fm1") > -1 || title.ToLower().IndexOf("fm2") > -1)
            {
                if (title.ToLower().IndexOf("barebone") > -1)
                {
                    return 423;
                }
                var sysPart = context.tb_ebay_system_parts.Where(s => videoCommentIds.Contains(s.comment_id.Value) && s.system_sku.HasValue && s.system_sku.Value.Equals(sysSku))
                         .Select(s => s.luc_sku).ToList();
                foreach (var sku in sysPart)
                {
                    if (sku == 16684)
                    {
                        return 445;
                    }
                    else
                        return 434;
                }
            }

            return 0;
        }
    }
}
