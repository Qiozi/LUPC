
using LU.Model.M.ModelV1;
using LU.Model.ModelV1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LU.Model.M.ModelV1.PartForHomeItem.Part;

namespace LU.BLL
{
    public class ProductProvider
    {
        public static LU.Model.Product GetProduct(LU.Data.nicklu2Entities context,
            int sku,
            Model.Enums.CountryType priceUnit,
            bool isWithSpecifications = false)
        {
            var rate = new LU.BLL.PRateProvider(context);
            var prodEbayItem = LU.BLL.CacheProvider.GeteBayCodes(context).FirstOrDefault(p => p.Sku.Equals(sku));

            var prod = context.tb_product.SingleOrDefault(p => p.product_serial_no.Equals(sku));

            if (prod == null)
            {
                return null;
            }
            return new Model.Product
            {
                ProduName = string.IsNullOrEmpty(prod.product_ebay_name) || prod.product_ebay_name.ToLower() == "null" ? prod.product_name : prod.product_ebay_name,
                Sku = prod.product_serial_no,
                Discount = rate.ConvertPrice(prod.product_current_discount.Value, priceUnit),
                MFP = prod.manufacturer_part_number,
                MfpForFilename = Util.FilterName(prod.manufacturer_part_number),
                eBayCode = prodEbayItem == null ? string.Empty : prodEbayItem.ItemId,
                Price = rate.ConvertPrice(prod.product_current_price.Value, priceUnit),
                Avatar = BLL.QiNiuImgHelper.Get(Util.GetImgSku(prod.product_serial_no, prod.other_product_sku)),
                Brand = prod.producter_serial_no,
                PriceUnit = priceUnit.ToString(),
                Keywords = prod.keywords,
                CateId = prod.menu_child_serial_no.Value,
                eBayPrice = prodEbayItem == null ? 0M : prodEbayItem.BuyItNowPrice,
                ShortName = prod.product_short_name,
                Specifications = isWithSpecifications ? GetPartSpecification(context, prod.product_serial_no) : string.Empty,
                AvatarGallery = GetAvatarGallery(Util.GetImgSku(prod.product_serial_no, prod.other_product_sku), prod.product_img_sum.Value)
            };
        }

        /// <summary>
        /// 获取商品图片地址列表
        /// </summary>
        /// <param name="imgSku"></param>
        /// <param name="qty"></param>
        /// <returns></returns>
        public static List<string> GetAvatarGallery(int imgSku, int qty)
        {
            var result = new List<string>();
            for (var i = 0; i < qty; i++)
            {
                result.Add(LU.BLL.QiNiuImgHelper.Get(imgSku, 600, 600, i + 1));
            }
            return result;
        }

        /// <summary>
        /// 获取商品规格描述
        /// </summary>
        /// <param name="sku"></param>
        /// <returns></returns>
        public static string GetPartSpecification(Data.nicklu2Entities context, int sku)
        {
            //string filename = "C:\\Workspaces\\Web\\Part_Comment\\" + ReqSKU + "_comment.html";
            //if (File.Exists(filename))
            //{
            //    string cont = File.ReadAllText(filename);

            //    return LU.Toolkit.DescriptionFilter.Done(cont);
            //}
            var query = context.tb_part_comment
                               .SingleOrDefault(me => me.part_sku.HasValue &&
                                                      me.part_sku.Value.Equals(sku));
            return query == null ? string.Empty : query.part_comment;
        }

        public static List<LU.Model.Product> GetHomeProducts(LU.Data.nicklu2Entities context
            , int cateId
            , Model.Enums.CountryType priceUnit)
        {
            var rate = new LU.BLL.PRateProvider(context);

            var prodSkus = context.tb_pre_index_page_setting.Where(p => p.CateId.HasValue &&
            p.CateId.Value.Equals(cateId))
            .Select(p => p.sku.Value).ToList();
            var query = context.tb_product.Where(p => prodSkus.Contains(p.product_serial_no)).ToList();

            var result = (from c in query
                          select new LU.Model.Product
                          {
                              ProduName = string.IsNullOrEmpty(c.product_ebay_name) ? c.product_name : c.product_ebay_name,
                              Sku = c.product_serial_no,
                              Discount = rate.ConvertPrice(c.product_current_discount.Value, priceUnit),
                              MFP = c.manufacturer_part_number,
                              MfpForFilename = Util.FilterName(c.manufacturer_part_number),
                              eBayCode = string.Empty,
                              Price = rate.ConvertPrice(c.product_current_price.Value, priceUnit),
                              Avatar = BLL.QiNiuImgHelper.Get(Util.GetImgSku(c.product_serial_no, c.other_product_sku)),
                              Brand = c.producter_serial_no,
                              PriceUnit = priceUnit.ToString()
                          }).ToList();
            var ebayCodes = CacheProvider.GeteBayCodes(context);
            for (int i = 0; i < result.Count; i++)
            {
                var item = result[i];
                var ebayPart = ebayCodes.FirstOrDefault(p => p.Sku.Equals(item.Sku));
                if (ebayPart != null)
                {
                    item.eBayCode = ebayPart.ItemId;
                    item.eBayPrice = ebayPart.BuyItNowPrice;
                }
            }
            return result;
        }

        /// <summary>
        /// for v1
        /// </summary>
        /// <param name="context"></param>
        /// <param name="priceUnit"></param>
        /// <returns></returns>
        public static List<PartForHomeItem> GetHomeProducts(Data.nicklu2Entities context, Model.Enums.CountryType priceUnit)
        {
            var result = new List<PartForHomeItem>();
            var ebayParts = CacheProvider.GeteBayCodes(context);
            var catestring = System.IO.File.ReadAllText(string.Concat(ConfigV1.CacheFilePath, "homeCate.txt"));
            List<HomeCate> cateList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<HomeCate>>(catestring);
            foreach (var item in cateList)
            {
                var cateId = int.Parse(item.Value);
                var partQuery = GetHomeProducts(context, cateId, priceUnit);

                // get baseinfo
                var parts = (from c in partQuery
                             select new PartForHomeItem.Part
                             {
                                 BaseInfo = new PartForHomeItem.Part.BaseInfoItem
                                 {
                                     CateId = cateId,
                                     Discount = c.Discount,
                                     MFP = c.MFP,
                                     MfpForFilename = c.MfpForFilename,
                                     Price = c.Price,
                                     PriceUnit = c.PriceUnit,
                                     ProductName = c.ProduName,
                                     ShortName = c.ShortName,
                                     Sku = c.Sku,
                                     Avatar = c.Avatar,
                                     Brand = c.Brand
                                 }
                             }).ToList();
                // get ebay info
                for (var i = 0; i < parts.Count; i++)
                {
                    var ebayItem = ebayParts.SingleOrDefault(me => me.Sku.Equals(parts[i].BaseInfo.Sku));
                    if (ebayItem != null)
                    {
                        parts[i].eBayInfo = new eBayInfo
                        {
                            Code = ebayItem.ItemId,
                            Price = ebayItem.BuyItNowPrice
                        };
                    }
                }
                result.Add(new PartForHomeItem
                {
                    CateName = item.Text,
                    CateId = cateId,
                    IconName = item.LogoFont,
                    Parts = parts
                });
            }
            return result;
        }


        public static List<LU.Model.PartInfoForCache> GetAllPartInfoForCache(Data.nicklu2Entities context, List<int> cateIds
            , Model.Enums.CountryType priceUnit)
        {
            var rate = new PRateProvider(context);
            var prodEbayItems = context.tb_ebay_selling.Where(p => p.luc_sku.HasValue && p.luc_sku.Value > 0).ToList();
            var query = context.tb_product.Where(p =>
                             p.tag.HasValue
                            && p.tag.Value.Equals(1)
                            && p.menu_child_serial_no.HasValue
                            && cateIds.Contains(p.menu_child_serial_no.Value)
                            && p.product_current_price.HasValue
                            && p.product_current_price.Value > 0).OrderByDescending(p => p.product_serial_no).ToList();
            var result = (from c in query
                          select new LU.Model.PartInfoForCache
                          {
                              ProduName = string.IsNullOrEmpty(c.product_ebay_name) || c.product_ebay_name.ToLower() == "null" ? c.product_name : c.product_ebay_name,
                              Sku = c.product_serial_no,
                              MFP = c.manufacturer_part_number,
                              ShortName = c.product_short_name,
                              MfpForFilename = Util.FilterName(c.manufacturer_part_number)

                          }).ToList();
            return result;
        }

        public static List<LU.Model.Product> GetAllProducts(Data.nicklu2Entities context, int cateId
            , Model.Enums.CountryType priceUnit, int[] cateIds)
        {
            var rate = new LU.BLL.PRateProvider(context);
            var prodEbayItems = LU.BLL.CacheProvider.GeteBayCodes(context);

            var query = context.tb_product.Where(p =>
                             p.tag.HasValue
                            && p.tag.Value.Equals(1)
                            && p.menu_child_serial_no.HasValue
                            && p.product_current_price.HasValue
                            && p.product_current_price.Value > 0);
            if (cateId > 0)
            {
                query = query.Where(p => p.menu_child_serial_no.Value.Equals(cateId));
            }
            if (cateIds.Length > 0)
            {
                query = query.Where(p => cateIds.Contains(p.menu_child_serial_no.Value));
            }
            // query.OrderBy(p=>p.other_product_sku).ThenBy(p => p.product_serial_no).ToList();

            var result = (from c in query.ToList()
                          orderby c.other_product_sku.Value ascending, c.product_serial_no descending
                          select new LU.Model.Product
                          {
                              ProduName = string.IsNullOrEmpty(c.product_ebay_name) || c.product_ebay_name.ToLower() == "null" ? c.product_name : c.product_ebay_name,
                              Sku = c.product_serial_no,
                              Discount = rate.ConvertPrice(c.product_current_discount.Value, priceUnit),
                              MFP = c.manufacturer_part_number,
                              MfpForFilename = Util.FilterName(c.manufacturer_part_number),
                              eBayCode = string.Empty,
                              Price = rate.ConvertPrice(c.product_current_price.Value, priceUnit),
                              Avatar = BLL.QiNiuImgHelper.Get(Util.GetImgSku(c.product_serial_no, c.other_product_sku)),
                              Brand = c.producter_serial_no,
                              PriceUnit = priceUnit.ToString(),
                              Keywords = c.keywords,
                              CateId = c.menu_child_serial_no.Value
                          }).ToList();
            for (int i = 0; i < result.Count; i++)
            {
                var ebayItem = prodEbayItems.FirstOrDefault(p => p.Sku.Equals(result[i].Sku));
                if (ebayItem != null)
                {
                    result[i].eBayCode = ebayItem.ItemId;
                    result[i].eBayPrice = ebayItem.BuyItNowPrice;
                }
            }
            return result;
        }

        public static List<LU.Model.SystemProduct> GetSystems(LU.Data.nicklu2Entities context, int[] sysSkus,
            Model.Enums.CountryType priceUnit, bool isHaveAccessories = false)
        {
            var rate = new PRateProvider(context);

            var ebayParts = CacheProvider.GeteBayCodes(context);

            var systems = (from c in context.tb_ebay_system
                           where sysSkus.Contains(c.id)
                           select new
                           {
                               Id = c.id,
                               Title = c.ebay_system_name,
                           }).ToList();




            var sysComents = CacheProvider.GetSysCommentList(context);
            var result = new List<Model.SystemProduct>();
            foreach (var sys in systems)
            {
                var parts = (from s in context.tb_ebay_system_parts
                             join p in context.tb_product on s.luc_sku.Value equals p.product_serial_no
                             where s.system_sku.HasValue && s.system_sku.Value.Equals(sys.Id)
                             orderby s.id
                             select new Model.SysPart
                             {
                                 MFP = p.manufacturer_part_number,
                                 CommentName = string.Empty,
                                 eBayCode = string.Empty,
                                 Sku = p.product_serial_no,
                                 eBayTitle = p.product_ebay_name,
                                 WebTitle = p.product_short_name,
                                 Price = p.product_current_price.Value,
                                 Discount = p.product_current_discount.Value,
                                 CommentId = s.comment_id.Value,
                                 ImgSku = p.other_product_sku.HasValue && p.other_product_sku.Value > 0 ? p.other_product_sku.Value : p.product_serial_no,
                                 GroupId = s.part_group_id.Value
                               
                             }).ToList();

                if (isHaveAccessories)
                {
                    var sysCommentAccessories = (from em in context.tb_ebay_system_part_comment
                                                 where em.showit.HasValue
                                                 && ConfigV1.SysCustomizePartAccessoriesCommentID.Contains(em.id)
                                                 orderby em.priority.Value ascending
                                                 select new
                                                 {
                                                     ID = em.id,
                                                     CommentName = em.comment,
                                                     DefaultPartGroupId = em.defaultPartGroupId.Value
                                                 }).ToList();

                    foreach (var s in sysCommentAccessories)
                    {
                        var m = new Model.SysPart();
                        m.CommentId = s.ID;
                        m.CommentName = s.CommentName;
                        m.GroupId = s.DefaultPartGroupId;
                        m.Price = 0M;
                        m.Sku = ConfigV1.NoneSelectedID;
                        m.WebTitle = "None Selected";
                        parts.Add(m);
                    }
                }

                for (int i = 0; i < parts.Count; i++)
                {
                    var partEbay = ebayParts.FirstOrDefault(p => p.Sku.Equals(parts[i].Sku));
                    if (partEbay != null)
                    {
                        parts[i].eBayCode = partEbay.ItemId;
                        parts[i].eBayHref = eBayProvider.GetEBayHref(partEbay.ItemId);
                    }

                    parts[i].PriceUnit = priceUnit.ToString();
                    parts[i].Price = rate.ConvertPrice(parts[i].Price, priceUnit);
                    parts[i].Discount = rate.ConvertPrice(parts[i].Discount, priceUnit);
                    parts[i].WebHref = LU.BLL.Util.PartUrl(parts[i].Sku, parts[i].MFP);
                    parts[i].CommentName = sysComents.Single(p => p.Id.Equals(parts[i].CommentId)).Name;

                }
                var ebayCode = ebayParts.FirstOrDefault(p => p.Sku.Equals(sys.Id));


                result.Add(new Model.SystemProduct
                {
                    PriceUnit = priceUnit.ToString(),
                    Title = sys.Title,
                    eBayCode = ebayCode != null ? ebayCode.ItemId : string.Empty,
                    eBayHref = ebayCode != null ? eBayProvider.GetEBayHref(ebayCode.ItemId) : string.Empty,
                    eBaySold = ebayCode != null ? ebayCode.BuyItNowPrice : 0M,
                    Id = sys.Id,
                    Parts = parts,
                    Price = rate.ConvertPrice(parts.Sum(p => p.Price), priceUnit),
                    Discount = rate.ConvertPrice(parts.Sum(p => p.Discount), priceUnit),
                    LogoSku = parts.Single(p => p.CommentId.Equals(BLL.Config.SysCaseCommentId)).Sku,
                    LogoUrl = LU.BLL.QiNiuImgHelper.Get(parts.Single(p => p.CommentId.Equals(BLL.Config.SysCaseCommentId)).Sku, 230, 0, 0)                    
                });
            }
            return result;
        }

        public static List<LU.Model.SystemProduct> GetHomeSystem(Data.nicklu2Entities context,
            Model.Enums.CountryType priceUnit)
        {
            var skus = context.tb_pre_index_page_setting.Where(p => (p.id.Equals(1) || p.id.Equals(2) || p.id.Equals(3) || p.id.Equals(6))
            && p.sku.HasValue)
                       .OrderBy(p => p.id)
                       .Select(s => s.sku.Value).ToList();
            return Remove16684(GetSystems(context, skus.ToArray(), priceUnit)).OrderBy(me=>me.Id).ToList();
        }

        public static List<Model.SysComment> GetSysComments(Data.nicklu2Entities context)
        {
            return (from c in context.tb_ebay_system_part_comment
                    select new Model.SysComment
                    {
                        Id = c.id,
                        Name = c.comment
                    }).ToList();
        }

        public static List<Model.SystemProduct> Remove16684(List<LU.Model.SystemProduct> systems)
        {
            for (int i = 0; i < systems.Count; i++)
            {
                for (int j = 0; j < systems[i].Parts.Count; j++)
                {
                    if (systems[i].Parts[j].Sku == 16684)
                    {
                        systems[i].Parts.Remove(systems[i].Parts[j]);
                        j--;
                    }
                }
            }
            return systems;
        }

        public static List<Model.M.SysMiniModel> GetAllSystemsMiniInfo(Data.nicklu2Entities context, System.Web.HttpContext httpContext)
        {
            var result = new List<Model.M.SysMiniModel>();
            var sysMiniBaseList = new List<Model.M.SysMiniBase>();
            var ebayCodes = LU.BLL.CacheProvider.GeteBayCodes(context);
            foreach (var cateId in Config.SysCateIds)
            {
                var filename = httpContext.Server.MapPath("/Computer/systems/" + cateId + ".txt");
                if (File.Exists(filename))
                {
                    var content = File.ReadAllText(filename);
                    var sysminis = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Model.M.SysMiniBase>>(content);

                    foreach (var sys in sysminis)
                    {
                        var sysPartList = (from ep in context.tb_ebay_system_parts
                                           join p in context.tb_product on ep.luc_sku.Value equals p.product_serial_no
                                           where ep.system_sku.HasValue && ep.system_sku.Value.Equals(sys.SysSku)
                                           select new
                                           {
                                               PartPrice = p.product_current_price.Value,
                                               PartDiscount = p.product_current_discount.Value
                                           }).ToList();
                        var price = sysPartList.Sum(p => p.PartPrice);
                        var discount = sysPartList.Sum(p => p.PartDiscount);
                        var sold = price - discount;

                        var subFilename = httpContext.Server.MapPath("/Computer/systems/detail/" + sys.SysSku + ".txt");
                        var subContext = File.ReadAllText(subFilename);

                        var miniSubPart = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Model.M.SysMiniSubPart>>(subContext);
                        var subParts = new List<Model.M.SysMiniSubPartEBay>();
                        for (int i = 0; i < miniSubPart.Count; i++)
                        {
                            var item = miniSubPart[i];
                            var ebayCodeModel = ebayCodes.FirstOrDefault(p => p.Sku.Equals(item.PartSku));

                            subParts.Add(new Model.M.SysMiniSubPartEBay
                            {
                                Comment = item.Comment,
                                eBayCode = ebayCodeModel == null ? string.Empty : ebayCodeModel.ItemId,
                                eBayHref = ebayCodeModel == null ? string.Empty : eBayProvider.GetEBayHref(ebayCodeModel.ItemId),
                                PartDiscount = item.PartDiscount,
                                PartImgSku = item.PartImgSku,
                                PartPrice = item.PartPrice,
                                PartSku = item.PartSku,
                                PartTitle = item.PartTitle,
                                ShortNameForSys = item.ShortNameForSys
                            });
                        }
                        result.Add(new Model.M.SysMiniModel
                        {
                            Discount = discount,
                            eBayId = sys.eBayId,
                            eBayPrice = sys.eBayPrice,
                            eBayTitle = sys.eBayTitle,
                            Price = price,
                            SysMiniSubParts = subParts,
                            SysSku = sys.SysSku,
                            Sold = sold,
                            CateId = cateId
                        });
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 获取首页系统， 可配置模式
        /// </summary>
        /// <param name="context"></param>
        /// <param name="priceUnit"></param>
        /// <returns></returns>
        public static object GetHomeSystemForCustom(Data.nicklu2Entities context,
            Model.Enums.CountryType priceUnit)
        {
            var skus = context.tb_pre_index_page_setting.Where(p => (p.id.Equals(1) || p.id.Equals(2) || p.id.Equals(3) || p.id.Equals(6))
                                                                && p.sku.HasValue)
             .OrderBy(p => p.id)
             .Select(s => s.sku.Value).ToList();

            return GetSystems(context, skus.ToArray(), priceUnit, true);

        }

        /// <summary>
        /// 获取配置System (零件)信息
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sysSku"></param>
        /// <returns></returns>
        public static List<SysCustomerModel> GetSystemPartsForCustmize(Data.nicklu2Entities context, int sysSku,
            Model.Enums.CountryType priceUnit)
        {
            var sysDetailList = new List<SysCustomerModel>();
            // var sysAccessoriesList = new List<SysCustomerModel>();

            var sysCommentAccessories = (from em in context.tb_ebay_system_part_comment
                                         where em.showit.HasValue
                                         && ConfigV1.SysCustomizePartAccessoriesCommentID.Contains(em.id)
                                         orderby em.priority.Value ascending
                                         select new
                                         {
                                             ID = em.id,
                                             CommentName = em.comment,
                                             DefaultPartGroupId = em.defaultPartGroupId.Value
                                         }).ToList();

            var sysDetail = (
                 from c in context.tb_ebay_system_parts
                 join p in context.tb_product on c.luc_sku.Value equals p.product_serial_no
                 join cm in context.tb_ebay_system_part_comment on c.comment_id.Value equals cm.id
                 join sc in context.tb_ebay_system on c.system_sku.Value equals sc.id
                 where c.luc_sku.HasValue &&
                       c.system_sku.HasValue &&
                       c.system_sku.Value.Equals(sysSku)
                 orderby cm.priority ascending
                 select new
                 {
                     CommentName = cm.comment,
                     CommentID = c.comment_id.Value,
                     Sold = p.product_current_price.Value - p.product_current_discount.Value,
                     PartSKU = c.luc_sku.Value,
                     PartTitle = string.IsNullOrEmpty(p.product_ebay_name) ? p.product_name_long_en : p.product_ebay_name,
                     PartGroupID = c.part_group_id.Value,

                 }).ToList();

            foreach (var s in sysDetail)
            {
                var m = new SysCustomerModel();
                m.CommentID = s.CommentID;
                m.CommentName = s.CommentName;
                m.SysSKU = sysSku;
                m.GroupID = s.PartGroupID;
                m.PartSKU = s.PartSKU;
                m.PartTitle = s.PartTitle;
                m.Sold = s.Sold;
                if (m.PartSKU == 0)
                {
                    m.PartSKU = ConfigV1.NoneSelectedID;
                    m.PartTitle = "None Selected";
                    m.GroupID = 0;
                }
                sysDetailList.Add(m);
            }

            foreach (var s in sysCommentAccessories)
            {
                var m = new SysCustomerModel();
                m.CommentID = s.ID;
                m.CommentName = s.CommentName;
                m.SysSKU = sysSku;
                m.GroupID = s.DefaultPartGroupId;
                m.Sold = 0M;
                m.PartSKU = ConfigV1.NoneSelectedID;
                m.PartTitle = "None Selected";
                sysDetailList.Add(m);
            }

            return sysDetailList;//.Concat(sysAccessoriesList);
        }

        /// <summary>
        /// 获取首页Brand logo 数据
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static object GetBrandForHome(Data.nicklu2Entities context)
        {
            var query = context.tb_producter.Where(p => !string.IsNullOrEmpty(p.logo_url)).ToList();
            return (from c in query
                    select new
                    {
                        Brand = c.producter_name,
                        ImgUrl = c.logo_url
                    }).ToList();
        }

        /// <summary>
        /// 按品牌获取所有商品
        /// </summary>
        /// <param name="context"></param>
        /// <param name="brand"></param>
        /// <returns></returns>
        public static object GetPartsByBrand(Data.nicklu2Entities context, string brand)
        {
            var query = context.tb_product
                               .Where(p => p.tag.HasValue &&
                                           p.tag.Value.Equals(1) &&
                                           p.producter_serial_no.Equals(brand))
                               .OrderBy(p => p.menu_child_serial_no.Value)
                               .ToList();

            var cateIds = query.Select(p => p.menu_child_serial_no.Value).ToList();

            var cates = context.tb_product_category
                               .Where(p => cateIds.Contains(p.menu_child_serial_no) &&
                                           !string.IsNullOrEmpty(p.menu_child_name_f))
                               .ToList();

            var result = new List<PartForBrandListItem>();

            foreach (var cate in cates)
            {
                var cateId = cate.menu_child_serial_no;
                var brandName = cate.menu_child_name;

                var subQuery = context.tb_product.Where(p => p.producter_serial_no.Equals(brandName)
                                   && p.menu_child_serial_no.HasValue
                                   && p.menu_child_serial_no.Value.Equals(cateId)
                                   && p.tag.HasValue
                                   && p.tag.Value.Equals(1)).OrderByDescending(p => p.product_serial_no).ToList();
                var dbSource = (from c in query
                                select new PartForBrandListItem
                                {
                                    logo = LU.BLL.QiNiuImgHelper.Get(LU.BLL.Util.GetImgSku(c.product_serial_no, c.other_product_sku), 50, 50),
                                    name = string.IsNullOrEmpty(c.product_ebay_name) ? c.product_name : c.product_ebay_name,
                                    price = c.product_current_price.Value - c.product_current_discount.Value,
                                    webHref = LU.BLL.Util.PartUrl(c.product_serial_no, c.manufacturer_part_number),
                                    Sku = c.product_serial_no,
                                    eBayCode = "",
                                    eBayHref = "",
                                    eBayPrice = 0
                                }).OrderByDescending(me => me.Sku).ToList();

                var prodEbayItems = CacheProvider.GeteBayCodes(context);
                for (int i = 0; i < dbSource.Count; i++)
                {
                    var ebayItem = prodEbayItems.FirstOrDefault(p => p.Sku.Equals(dbSource[i].Sku));
                    if (ebayItem != null)
                    {
                        dbSource[i].eBayCode = ebayItem.ItemId;
                        dbSource[i].eBayPrice = ebayItem.BuyItNowPrice;
                        dbSource[i].eBayHref = LU.BLL.Util.eBayUrl(ebayItem.ItemId);
                    }
                    var sourceItem = dbSource[i];
                    result.Add(new PartForBrandListItem
                    {
                        eBayCode = sourceItem.eBayCode,
                        eBayHref = sourceItem.eBayHref,
                        eBayPrice = sourceItem.eBayPrice,
                        logo = sourceItem.logo,
                        name = sourceItem.name,
                        price = sourceItem.price,
                        Sku = sourceItem.Sku,
                        webHref = sourceItem.webHref
                    });
                }
            }
            return result;
        }

        /// <summary>
        /// 获取系统零件所在的群组商品明细
        /// </summary>
        /// <param name="context"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static object GetSystemPartGroupDetail(Data.nicklu2Entities context, int groupId)
        {
            return (from pgd in context.tb_part_group_detail
                    join p in context.tb_product on pgd.product_serial_no.Value equals p.product_serial_no
                    where (p.tag.HasValue && p.tag.Value.Equals(1)
                    && pgd.part_group_id.HasValue
                    && pgd.part_group_id.Value.Equals(groupId) &&
                    ((p.is_fixed.HasValue && p.is_fixed.Value.Equals(true)) || (p.for_sys.HasValue && p.for_sys.Value.Equals(true)))
                    )
                    orderby pgd.nominate.Value descending
                    orderby p.producter_serial_no ascending
                    orderby p.product_ebay_name ascending
                    select new
                    {
                        Title = string.IsNullOrEmpty(p.product_ebay_name) ? p.product_name_long_en : p.product_ebay_name,
                        Price = p.product_current_price.Value,
                        Sold = p.product_current_price.Value - p.product_current_discount.Value,
                        Discount = p.product_current_discount.Value,
                        SKU = p.product_serial_no,
                        ImgSku = p.other_product_sku.Value > 0 ? p.other_product_sku.Value : p.product_serial_no

                    }).ToList();
        }

        /// <summary>
        /// 获取单个系统价格
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sysSku"></param>
        /// <param name="countryType"></param>
        /// <returns></returns>
        public static object GetSingleSystemPrice(Data.nicklu2Entities context, int sysSku, LU.Model.Enums.CountryType countryType)
        {
            var rate = new LU.BLL.PRateProvider(context);
            var sysPartList = (from ep in context.tb_ebay_system_parts
                               join p in context.tb_product on ep.luc_sku.Value equals p.product_serial_no
                               where ep.system_sku.HasValue && ep.system_sku.Value.Equals(sysSku)
                               select new
                               {
                                   PartPrice = p.product_current_price.Value,
                                   PartDiscount = p.product_current_discount.Value
                               }).ToList();
            return new
            {
                Price = rate.ConvertPrice(sysPartList.Sum(p => p.PartPrice), countryType),
                Discount = rate.ConvertPrice(sysPartList.Sum(p => p.PartDiscount), countryType),
                Sold = rate.ConvertPrice(sysPartList.Sum(p => p.PartPrice - p.PartDiscount), countryType),
                Unit = countryType,
            };
        }

    }
}
