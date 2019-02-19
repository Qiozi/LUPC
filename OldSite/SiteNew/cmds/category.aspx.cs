using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class cmds_category : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            switch (ReqCmd)
            {
                case "get_all_cate":                   
                        string json = WriteAllCate(ReqID);
                        Response.Write(json);
                       
                    break;
            }
        }
        Response.End();
    }

    string WriteAllCate(int cateID)
    {
        //List<LU.Data.tb_product_category> cateList = db.tb_product_category.Where(p =>p.menu_pre_serial_no.HasValue
        //&& p.menu_pre_serial_no.Value.Equals(0)).OrderBy(p=>p.menu_child_order).ToList();

        var list = from c in db.tb_product_category
                   where c.menu_pre_serial_no.HasValue && c.menu_pre_serial_no.Value.Equals(0)
                   && c.tag.HasValue && c.tag.Value.Equals(1)
                   && (cateID > 0 ? c.menu_child_serial_no.Equals(cateID) : true)
                   && c.is_view_menu.HasValue && c.is_view_menu.Value.Equals(true)
                   orderby c.menu_child_order ascending
                   select new
                   {
                       ID = c.menu_child_serial_no,
                       Name = c.menu_child_name,
                       SubList = (from s in db.tb_product_category where s.menu_pre_serial_no.HasValue && s.menu_pre_serial_no.Value.Equals(c.menu_child_serial_no) && s.tag.HasValue && s.tag.Value.Equals(1) orderby s.menu_child_order ascending select new { ID = s.menu_child_serial_no, Name = s.menu_child_name, PageType= s.page_category })
                   };


        string json = Newtonsoft.Json.JsonConvert.SerializeObject(list.ToList());
        return json;

    }


    string ReqCmd
    {
        get { return Util.GetStringSafeFromQueryString(Page, "cmd"); }
    }

    int ReqID
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "id", 0); }
    }

}