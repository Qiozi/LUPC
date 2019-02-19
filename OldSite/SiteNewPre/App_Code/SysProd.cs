using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SysProd
/// </summary>
public class SysProd
{
    public SysProd()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    /// <summary>
    /// sys sku length 8 convert to sys sku length 6.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="Sku8"></param>
    /// <param name="comments"></param>
    /// <returns></returns>
    public static int ConvertSys8ToSys(nicklu2Model.nicklu2Entities context, int Sku8, List<nicklu2Model.tb_ebay_system_part_comment> comments)
    {
        var sysSku = Sku8.ToString();
        var query = (from c in context.tb_sp_tmp_detail
                     join p in context.tb_product on c.product_serial_no.Value equals p.product_serial_no
                     where c.sys_tmp_code.Equals(sysSku)
                     select new
                     {
                         Part = c,
                         Prod = p
                     }).ToList();
        var cost = (from p in query
                    select new
                    {
                        p.Prod.product_current_cost.Value
                    }).Select(p => p.Value).Sum();

        var newSys = new nicklu2Model.tb_ebay_system()
        {
            category_id = null,
            adjustment = 0M,
            system_title1 = string.Concat("System#: " + Sku8),
            is_barebone = false,
            is_disable_flash_customize = false,
            is_from_ebay = false,
            is_include_shipping = false,
            is_issue = false,
            is_online = false,
            is_shrink = false,
            keywords = string.Empty,
            large_pic_name = string.Empty,
            cutom_label = string.Empty,
            cost = cost,
            ebay_fee = 0M,
            ebay_subtitle = string.Empty,
            ebay_system_current_number = string.Empty,
            ebay_system_name = string.Empty,
            ebay_system_price = 0M,
            eBayCategoryName = string.Empty,
            flashVersion = 3,
            logo_filenames = string.Empty,
            no_selected_ebay_sell = 0,
            main_comment_ids = string.Empty,
            parent_ebay_itemid = string.Empty,
            parentID = null,
            profit = null,
            regdate = DateTime.Now,
            selected_ebay_sell = 0M,
            shipping_fee = 40M,
            showit = true,
            source_code = 0,
            sub_part_quantity = 0,
            system_title2 = string.Empty,
            system_title3 = string.Empty,
            templete_id = 45,
            view_count = 0,
            tmp_discount = 0M,
            tmp_sell = 0M
        };
        context.AddTotb_ebay_system(newSys);
        context.SaveChanges();

        var newSysId = newSys.id;
        foreach (var item in query)
        {
            var comentId = comments.Single(c => c.id.Equals(item.Part.comment_id.Value)).id;
            var part = new nicklu2Model.tb_ebay_system_parts
            {
                comment_id = comentId,
                comment_name = item.Part.cate_name,
                compatibility_parts = string.Empty,
                cost = item.Prod.product_current_cost,
                eBayShowit = false,
                is_belong_price = true,
                is_label_of_flash = true,
                is_online = false,
                luc_sku = item.Prod.product_serial_no,
                max_quantity = item.Part.part_max_quantity,
                part_group_id = item.Part.part_group_id,
                part_quantity = item.Part.part_quantity,
                price = item.Prod.product_current_price,
                regdate = DateTime.Now,
                system_sku = newSysId

            };
            context.AddTotb_ebay_system_parts(part);
        }
        context.SaveChanges();
        return newSysId;
    }


    /// <summary>

    /// </summary>
    /// <param name="parts">partID|groupID|commentID,partID|groupID|commentID,</param>
    /// <param name="SysSku"></param>
    /// <param name="Email"></param>
    /// <param name="UserIPAddress"></param>
    /// <param name="ct"></param>
    /// <param name="IsCustomize"></param>
    /// <param name="db"></param>
    /// <returns>返回零表示不正确</returns>
    public static int CustomizeNewSys(string parts
        , int SysSku
        , string Email
        , string UserIPAddress
        , CountryType ct
        , bool IsCustomize
        , nicklu2Model.nicklu2Entities db)
    {
        var itemString = parts;// Util.GetStringSafeFromString(Page, "parts");
        var sysSKU = SysSku;// Util.GetInt32SafeFromString(Page, "syssku", 0);
        if (sysSKU == 0)
        {
            throw new Exception("Sys sku is null.");
        }
        // Response.Write(sysSKU.ToString());
        if (itemString.IndexOf(",") > 0 || !IsCustomize)
        {
            //Response.Write(itemString);
            var items = itemString.Split(new char[] { ',' });
            decimal cost = 0M;
            decimal discount = 0M;
            decimal price = 0M;
            decimal sold = 0M;

            // 新的系统编号
            int newSysCode = Codes.NewSysCode(db);


            var partList = (from sp in db.tb_ebay_system_parts
                            where sp.system_sku.HasValue && sp.system_sku.Value.Equals(SysSku)
                            select new
                            {
                                PartSku = sp.luc_sku.Value,
                                GroupID = sp.part_group_id.Value,
                                CommentID = sp.comment_id.Value
                            }).ToList();
            int itemCount = IsCustomize ? items.Length : partList.Count;

            for (int i = 0; i < itemCount; i++)
            {
                // if (IsCustomize)


                int partID;
                int groupID;
                int commentID;
                if (IsCustomize) // 客户配置
                {
                    if (items[i].IndexOf("|") == -1)
                        continue;

                    var partAndGroup = items[i].Split(new char[] { '|' });
                    // part and group 数据不完整
                    if (!IsCustomize)
                    {
                        if (partAndGroup.Length < 2)
                            continue;
                    }

                    int.TryParse(partAndGroup[0], out partID);
                    int.TryParse(partAndGroup[1], out groupID);
                    int.TryParse(partAndGroup[2], out commentID);
                }
                else// 非配置系统 
                {
                    partID = partList[i].PartSku;
                    groupID = partList[i].GroupID;
                    commentID = partList[i].CommentID;
                }


                var commModel = db.tb_ebay_system_part_comment.FirstOrDefault(p => p.id.Equals(commentID));
                var prod = db.tb_product.FirstOrDefault(p => p.product_serial_no.Equals(partID));
                if (commModel != null
                    && prod != null)
                {
                    var spDetail = nicklu2Model.tb_sp_tmp_detail.Createtb_sp_tmp_detail(0);
                    spDetail.cate_name = commModel.comment;
                    spDetail.ebay_number = "";
                    spDetail.is_lock = false;
                    spDetail.old_price = prod.product_current_price;
                    spDetail.part_group_id = groupID;
                    spDetail.part_max_quantity = 1;
                    spDetail.part_quantity = 1;
                    spDetail.product_current_cost = prod.product_current_cost;
                    spDetail.product_current_price = prod.product_current_price;
                    spDetail.product_current_price_rate = prod.product_current_price;
                    spDetail.product_current_sold = prod.product_current_price - prod.product_current_discount;
                    spDetail.product_name = string.IsNullOrEmpty(prod.product_ebay_name) ? prod.product_ebay_name : prod.product_ebay_name;
                    spDetail.product_order = i;
                    spDetail.product_serial_no = partID;
                    spDetail.re_sys_tmp_detail = 0;
                    spDetail.save_price = prod.product_current_discount;
                    spDetail.sys_tmp_code = newSysCode.ToString();
                    spDetail.system_product_serial_no = 0;
                    spDetail.system_templete_serial_no = sysSKU;
                    spDetail.comment_id = commentID;
                    db.AddTotb_sp_tmp_detail(spDetail);

                    cost += prod.product_current_cost.Value;
                    discount += prod.product_current_discount.Value;
                    price += prod.product_current_price.Value;
                    sold += prod.product_current_price.Value - prod.product_current_discount.Value;

                }
                db.SaveChanges();
            }

            nicklu2Model.tb_sp_tmp sysMain = nicklu2Model.tb_sp_tmp.Createtb_sp_tmp(0);
            sysMain.create_datetime = DateTime.Now;
            //  ebay item id
            if (IsCustomize)
                sysMain.ebay_number = "";
            else
            {
                var ebay = db.tb_ebay_selling.FirstOrDefault(p => p.sys_sku.HasValue
                    && p.sys_sku.Value.Equals(SysSku));
                if (ebay != null)
                {
                    sysMain.ebay_number = ebay.ItemID;

                }
                else
                    sysMain.ebay_number = "";
            }
            sysMain.email = Email;
            sysMain.ip = UserIPAddress;
            sysMain.is_customize = true;
            sysMain.is_modify = true;
            sysMain.is_noebook = 0;
            sysMain.is_old = false;
            sysMain.is_templete = false;
            sysMain.old_part_id = 0;
            sysMain.old_price = price;
            sysMain.price_unit = ct.ToString();
            sysMain.save_price = discount;
            sysMain.sys_tmp_code = newSysCode.ToString();
            sysMain.sys_tmp_cost = cost;
            sysMain.sys_tmp_price = sold;
            sysMain.sys_tmp_product_name = "";
            sysMain.syst_tmp_price_rate = price;
            sysMain.system_category_serial_no = 0;
            sysMain.system_templete_serial_no = sysSKU;
            sysMain.tag = 1;
            db.AddTotb_sp_tmp(sysMain);
            db.SaveChanges();

            return newSysCode;
        }
        return 0;
    }
}