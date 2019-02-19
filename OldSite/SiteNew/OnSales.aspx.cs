using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OnSales : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var cateNames = (from m in db.tb_on_sale
                             join p in db.tb_product on m.product_serial_no.Value equals p.product_serial_no
                             join pc in db.tb_product_category on p.menu_child_serial_no.Value equals pc.menu_child_serial_no
                             orderby m.product_serial_no descending
                             select new
                             {
                                 CateName = pc.menu_child_name
                             }).Distinct().ToList();

            rptList.DataSource = cateNames;
            rptList.DataBind();
            rptList2.DataSource = cateNames;
            rptList2.DataBind();
        }
    }

    protected void rptList2_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Footer && e.Item.ItemType != ListItemType.Header)
        {
            var subRpt = e.Item.FindControl("rptSubList") as Repeater;
            var cateName = (e.Item.FindControl("ltCateName") as Literal).Text;
            var list = (from m in db.tb_on_sale
                        join p in db.tb_product on m.product_serial_no.Value equals p.product_serial_no
                        join pc in db.tb_product_category on p.menu_child_serial_no.Value equals pc.menu_child_serial_no
                        where pc.menu_child_name.Equals(cateName)
                        orderby m.product_serial_no descending
                        select new
                        {
                            SKU = p.product_serial_no,
                            ProdName = string.IsNullOrEmpty(p.product_ebay_name) ? p.product_short_name : p.product_ebay_name,
                            BeginDate = m.begin_datetime,
                            EndDate = m.end_datetime,
                            Price = m.save_price,
                            Href = p.new_href_url,
                            MFP = p.manufacturer_part_number
                        }).ToList();

            var subList = (from m in list
                           select new
                           {
                               m.SKU,
                               ProdName = "<a href='" + (LU.BLL.Util.PartUrl(m.SKU, m.MFP) + "'>") + m.ProdName + "</a>",
                               m.BeginDate,
                               m.EndDate,
                               m.Price,
                               m.Href
                           });
            subRpt.DataSource = subList;
            subRpt.DataBind();
        }



    }

    private string Concat(int a, string b)
    {
        // (string.IsNullOrEmpty(p.new_href_url) ? "<a href='/detail_part.aspx?sku=" + p.product_serial_no + "'>" : "<a href='" + p.new_href_url + "'>") + (string.IsNullOrEmpty(p.product_ebay_name) ? p.product_short_name : p.product_ebay_name) + "</a>",
        return a.ToString() + b;
    }
}