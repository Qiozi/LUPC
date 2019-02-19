using LU.Model.ModelV1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class detail_sys_customize : PageBase
{
    public string SysListString = "";
    public string SysListStringAccessories = "";
    public int CateID = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region 取得系统配置

            var sysSku = ReqSKU;

            // sku length : 8 to 6
            if (sysSku.ToString().Length == 8)
            {
                var comments = LU.BLL.CacheProvider.GetSysCommentList(db);
                sysSku = SysProd.ConvertSys8ToSys(db, sysSku, comments);
            }

            var sysDetailList = new List<SysCustomerModel>();
            var sysAccessoriesList = new List<SysCustomerModel>();

            #region categories
            var prod = db.tb_ebay_system.FirstOrDefault(p => p.id.Equals(sysSku));

            var CateID = SysProdCate.GetSysCate(sysSku, prod.is_barebone.Value, db);

            var cateModel = db.tb_product_category.FirstOrDefault(p => p.menu_child_serial_no.Equals(CateID));
            if (cateModel != null)
            {
                var preId = cateModel.menu_pre_serial_no.Value;
                var subCateModel = db.tb_product_category.FirstOrDefault(p => p.menu_child_serial_no.Equals(preId));
                if (subCateModel != null)
                {
                    ltCateNameParent.Text = subCateModel.menu_child_name + "";// +cateModel.menu_child_name;
                    ltCateName.Text = cateModel.menu_child_name;

                    #region cate list
                    var parentCateList = db.tb_product_category.Where(p => p.menu_pre_serial_no.HasValue
                        && p.menu_pre_serial_no.Value.Equals(0)
                        && p.tag.HasValue
                        && p.tag.Value.Equals(1)
                        ).OrderBy(p => p.menu_child_order).ToList();
                    for (int i = 0; i < parentCateList.Count; i++)
                    {
                        //ltCatesParent.Text += "<li>" + parentCateList[i].menu_child_name + "</li>";
                    }

                    var cateList = db.tb_product_category.Where(p => p.menu_pre_serial_no.HasValue
                        && p.menu_pre_serial_no.Value.Equals(preId)
                        && p.tag.HasValue
                        && p.tag.Value.Equals(1)
                        ).OrderBy(p => p.menu_child_order).ToList();
                    for (int i = 0; i < cateList.Count; i++)
                    {
                        if (cateList[i].page_category == 0)
                            ltCates.Text += "<li role='pressntation'><a href='list_sys.aspx?cid=" + cateList[i].menu_child_serial_no + "'>" + cateList[i].menu_child_name + "</a></li>";

                        else
                            ltCates.Text += "<li role='pressntation'><a href='list_part.aspx?cid=" + cateList[i].menu_child_serial_no + "'>" + cateList[i].menu_child_name + "</a></li>";
                    }
                    #endregion
                }

            }
            #endregion

            var sysCommentAccessories = (from em in db.tb_ebay_system_part_comment
                                         where em.showit.HasValue
                                         && setting.SysCustomizePartAccessoriesCommentID.Contains(em.id)
                                         orderby em.priority.Value ascending
                                         select new
                                         {
                                             ID = em.id,
                                             CommentName = em.comment,
                                             DefaultPartGroupId = em.defaultPartGroupId.Value
                                         }).ToList();

            var sysDetail = (
                 from c in db.tb_ebay_system_parts
                 join p in db.tb_product on c.luc_sku.Value equals p.product_serial_no
                 join cm in db.tb_ebay_system_part_comment on c.comment_id.Value equals cm.id
                 join sc in db.tb_ebay_system on c.system_sku.Value equals sc.id
                 where c.luc_sku.HasValue && 
                 c.system_sku.HasValue && 
                 c.system_sku.Value.Equals(sysSku) &&
                 !setting.SysCustomizePartAccessoriesCommentID.Contains(cm.id)

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
                    m.PartSKU = setting.NoneSelectedID;
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
                m.PartSKU = setting.NoneSelectedID;
                m.PartTitle = "None Selected";
                sysAccessoriesList.Add(m);
            }
            #endregion

            SysListString = GetListString(sysDetailList);
            SysListStringAccessories = GetListString(sysAccessoriesList);
        }
    }

    string GetListString(List<SysCustomerModel> list)
    {
        string result = "";
        for (int i = 0; i < list.Count; i++)
        {
            result += string.Format(@"
<a class=""list-group-item text-left sys-part-title list-group-item-success"" 
data-toggle=""collapse"" 
href=""#collapseExample"" 
aria-expanded=""false"" 
aria-controls=""collapseExample"" 
onclick='showPartDetailList($(this));' 
partsku='{2}' 
groupid='{3}' 
commentid='{5}'>
    <strong style=''>{1}</strong> 
    <small class='note'>{0}</small>
    <span class='itemPrice'>${4}</span>
    <span class='badge'>
        <span class='glyphicon glyphicon-chevron-down'></span>
    </span>
</a>
<div class='PlaneSysPartDetail' id='sysPartGroupDetailArea{2}' style='display:none;'></div>
"
                , list[i].PartTitle
                , list[i].CommentName
                , list[i].PartSKU
                , list[i].GroupID
                , list[i].Sold
                , list[i].CommentID
                );

        }
        return result;
    }

}

