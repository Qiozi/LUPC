using LU.Model.M;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LU.BLL
{
    public class SystemFlashProvider
    {
        public static List<LU.Model.M.SystemForFlash> GetSystemForFlash(Data.nicklu2Entities context, int sysSku, out decimal adjustment)
        {
            var result = new List<LU.Model.M.SystemForFlash>();
            //var query = context.tb_ebay_system_parts.Where(p => p.system_sku.HasValue &&
            //p.system_sku.Value.Equals(sysSku) &&
            //p.part_group_id.HasValue &&
            //p.part_group_id.Value > 0).ToList();
            var query = (from c in context.tb_ebay_system_parts
                         join ce in context.tb_ebay_system_part_comment on c.comment_id.Value equals ce.id
                         where c.system_sku.HasValue && c.system_sku.Value.Equals(sysSku) &&
                         c.part_group_id.HasValue && c.part_group_id.Value > 0
                         orderby ce.priority ascending
                         select new
                         {
                             Part = c,
                             Comm = ce
                         }).ToList();

            //var comments = context.tb_ebay_system_part_comment.ToList();

            var system = context.tb_ebay_system.Single(p => p.id.Equals(sysSku));
            adjustment = system.adjustment.HasValue ? system.adjustment.Value : 0M;
            var partAdjustment = 0M;
            foreach (var item in query)
            {
                var commentModel = item.Comm;// comments.Single(p => p.id.Equals(item.comment_id.Value));
                var prod = context.tb_product.Single(p => p.product_serial_no.Equals(item.Part.luc_sku.Value));
                var ebay = CacheProvider.GeteBayCodes(context).FirstOrDefault(p => p.Sku.Equals(item.Part.luc_sku.Value));
                partAdjustment += prod.adjustment.HasValue ? prod.adjustment.Value : 0M;
                result.Add(new Model.M.SystemForFlash
                {
                     
                    CommentId = item.Part.comment_id.Value,
                    CommentText = commentModel.comment,
                    DetailId = item.Part.id,
                    PartGroupId = item.Part.part_group_id.Value,
                    IsLabelOfFlash = item.Part.is_label_of_flash.Value,
                    IsAudio = commentModel.is_audio.Value,
                    IsCpu = commentModel.is_cpu.Value,
                    IsCpuFan = commentModel.is_cpu_fan.Value,
                    IsNetwork = commentModel.is_network.Value,
                    IsVideo = commentModel.is_video.Value,
                    IsMotherboard = commentModel.is_mb.Value,
                    eBayCode = ebay != null ? ebay.ItemId : "",
                    ShortNameForSystem = prod.short_name_for_sys,
                    PartPrice = prod.product_current_price.Value - prod.product_current_discount.Value,
                    PartName = string.IsNullOrEmpty(prod.product_ebay_name) ? prod.product_name : prod.product_ebay_name,
                    ImgSku = prod.other_product_sku.HasValue && prod.other_product_sku.Value > 0 ? prod.other_product_sku.Value : prod.product_serial_no,
                    PartSku = prod.product_serial_no,
                    SubParts = item.Part.eBayShowit.HasValue &&
                        item.Part.eBayShowit.Value.Equals(true) &&
                        item.Part.is_label_of_flash.HasValue &&
                        item.Part.is_label_of_flash.Value.Equals(false) ?
                            GetSystemSubParts(context, item.Part.part_group_id.Value) : new List<SystemSubPart>()

                });
            }
            adjustment = adjustment == 0 ? partAdjustment : adjustment;
            return result;
        }

        public static List<SystemSubPart> GetSystemSubParts(Data.nicklu2Entities context, int partGroupid)
        {
            var query = (from c in context.tb_part_group_detail
                         join p in context.tb_product on c.product_serial_no.Value equals p.product_serial_no
                         where p.tag.HasValue && p.tag.Value.Equals(1)
                         && p.for_sys.HasValue && p.for_sys.Value.Equals(true) &&                           
                         c.showit.HasValue && c.showit.Value.Equals(1) &&
                         c.part_group_id.HasValue && c.part_group_id.Value.Equals(partGroupid)
                         orderby p.short_name_for_sys
                         select new SystemSubPart
                         {
                             PartName = string.IsNullOrEmpty(p.product_ebay_name) ? p.product_name : p.product_ebay_name,
                             PartPrice = p.product_current_price.Value - p.product_current_discount.Value,
                             PartSku = p.product_serial_no,
                             ShortNameForSystem = p.short_name_for_sys,
                             ImgSku = p.other_product_sku.HasValue && p.other_product_sku.Value > 0 ? p.other_product_sku.Value : p.product_serial_no,
                             eBayCode = null,// eBayProvider.GetItemIdByPartSku(context, p.product_serial_no)
                             PartGroupId = partGroupid
                         }).ToList();
            var ebays = CacheProvider.GeteBayCodes(context);//.FirstOrDefault(p => p.Sku.Equals(item.Part.luc_sku.Value));
            for(int i = 0; i < query.Count; i++)
            {
                var ebay = ebays.FirstOrDefault(p => p.Sku.Equals(query[i].PartSku));
                query[i].eBayCode = ebay == null ? "" : ebay.ItemId;
            }
            return query;
        }
    }
}
