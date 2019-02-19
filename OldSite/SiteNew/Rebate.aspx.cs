using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Rebate : PageBase
{
    DateTime endDate = DateTime.Parse(DateTime.Now.AddMonths(1).ToString("yyyy-MM-01"));
    DateTime beginDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01"));
    DateTime preMonthEndDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01"));
    DateTime PreMonthBeginDate = DateTime.Parse(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-01"));

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {

            var bands = (from m in db.tb_sale_promotion
                         join p in db.tb_product on m.product_serial_no.Value equals p.product_serial_no
                         where (m.end_datetime > beginDate || m.end_datetime > PreMonthBeginDate)

                         orderby m.product_serial_no descending
                         select new
                         {
                             Brand = p.producter_serial_no
                         }).Distinct().ToList();

            rptList.DataSource = bands;
            rptList.DataBind();
            rptList2.DataSource = bands;
            rptList2.DataBind();
        }
    }

    protected void rptList2_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Footer && e.Item.ItemType != ListItemType.Header)
        {
            var subRpt = e.Item.FindControl("rptSubList") as Repeater;
            var brand = (e.Item.FindControl("ltBrand") as Literal).Text;
            var list = (from m in db.tb_sale_promotion
                        join p in db.tb_product on m.product_serial_no.Value equals p.product_serial_no
                        where p.producter_serial_no.Equals(brand)
                        && (m.end_datetime > beginDate || m.end_datetime > PreMonthBeginDate)
                        orderby m.end_datetime descending, m.product_serial_no descending
                        select new
                        {
                            SKU = p.product_serial_no,
                            ProdName = p.product_short_name + m.comment,
                            BeginDate = m.begin_datetime,
                            EndDate = m.end_datetime,
                            Price = m.save_cost,
                            Href = p.new_href_url,
                            Url = m.pdf_filename,
                            MFP = p.manufacturer_part_number
                        }).ToList();

            var subList = (from m in list
                           select new
                           {
                               m.SKU,
                               ProdName = "<a href='https://lucomputers.com/pro_img/rebate_pdf/" + m.Url.ToString() + "' target='_blank'>" + m.ProdName + "</a>",
                               m.BeginDate,
                               m.EndDate,
                               m.Price,
                               m.Href,
                               m.MFP,
                               BgColor = m.EndDate > DateTime.Now ? "" : " class='active' "
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