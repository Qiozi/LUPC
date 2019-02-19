using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class cmds_generateCateInfo : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string folderName = "/Computer/";
        if (!IsPostBack)
        {
            #region parts
            var cateList = db.tb_product_category.Where(p => p.tag.HasValue
                && p.tag.Value.Equals(1)
                ).ToList();
            foreach (var c in cateList)
            {
                int cid = c.menu_child_serial_no;
                string dirName = folderName + "Parts/" + cid;
                if (!Directory.Exists(Server.MapPath(dirName)))
                {
                    Directory.CreateDirectory(Server.MapPath(dirName));
                }

                string filename = Server.MapPath(dirName) + "\\list.txt";

                var prodList =
                    (from p in db.tb_product
                     where p.tag.HasValue
                     && p.tag.Value.Equals(1)
                     && p.menu_child_serial_no.HasValue
                     && p.menu_child_serial_no.Value.Equals(cid)
                     orderby p.product_serial_no descending
                     select new
                     {
                         SKU = p.product_serial_no,
                         ShortName = p.product_short_name,
                         Name = !string.IsNullOrEmpty(p.product_ebay_name) ? p.product_ebay_name : p.product_name,
                         Price = p.product_current_price.Value,
                         Discount = p.product_current_discount.HasValue ? p.product_current_discount.Value : 0,
                         Sold = p.product_current_price.Value - p.product_current_discount.Value,
                         ImgSku = p.other_product_sku.Value > 0 ? p.other_product_sku.Value : p.product_serial_no,
                         IsOnsale = p.product_current_discount > 0 ? 1 : 0,
                         IsRebate = 0,
                         priceUnit = "CAD",
                         Keyword = p.keywords.ToLower()
                     }).ToList();

                string json = Newtonsoft.Json.JsonConvert.SerializeObject(prodList);
                File.WriteAllText(filename, json);
            }
            #endregion
        }
    }



}