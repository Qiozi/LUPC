using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Q_Admin_Manager_eBay_SysList : PageBase
{

    public class Item
    {
        public int SysSku { get; set; }

        public string CustomLabel { get; set; }

        public string eBayItemId { get; set; }

        public string eBayTitle { get; set; }

        public string PartShortNameLists { get; set; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        BindList();
    }
    

    void BindList()
    {
        var query = DBContext
                        .tb_ebay_system
                        .Where(me =>
                                me.is_online.HasValue &&
                                me.is_online.Value.Equals(true))
                        .OrderByDescending(me => me.cutom_label)
                        .ToList();
        var data = (from c in query
                    join c2 in DBContext.tb_ebay_selling on c.id equals c2.sys_sku.Value
                    select new Item
                    {
                        eBayItemId = c2.ItemID,
                        CustomLabel = c2.SKU,
                        eBayTitle = c2.Title,
                        SysSku = c.id,
                        PartShortNameLists = string.Empty
                    }).ToList();

        this.rptList.DataSource = data;
        this.rptList.DataBind();

    }
}