using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SiteApp.UC
{
    public partial class TopMenu2 : System.Web.UI.UserControl
    {
        public string ProdSystem { set; get; }
        public string ProdMobile { set; get; }
        public string ProdPart { set; get; }

        SiteDB.nicklu2Entities db = new SiteDB.nicklu2Entities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitProdCateString();
            }
        }


        void InitProdCateString()
        {
            // mobile computers
            ProdMobile = "";
            var pcMobile = SiteDB.ProdCateHelper.GetProdCates(1, 1, db);

            foreach (var m in pcMobile)
            {
                ProdMobile += string.Format(@"<li><a href='index.aspx?cid="+m.menu_child_serial_no+@"'><span class=""glyphicon glyphicon-arrow-right""></span> {0}</a></li>", m.menu_child_name);
            }
            
            // part
            ProdPart = "";
            var  pcPart = SiteDB.ProdCateHelper.GetProdCates(2, 1, db);
             foreach (var m in pcPart)
            {
                ProdPart += string.Format(@"<li><a href='index.aspx?cid=" + m.menu_child_serial_no + @"'><span class=""glyphicon glyphicon-arrow-right""></span> {0}</a></li>", m.menu_child_name);
            }

            // desktop computers
            ProdSystem = "";
             var  pcSystem = SiteDB.ProdCateHelper.GetProdCates(52, 1, db);
            foreach (var m in pcSystem)
            {
                ProdSystem += string.Format(@"<li><a href='indexsys.aspx?cid=" + m.menu_child_serial_no + @"'><span class=""glyphicon glyphicon-arrow-right""></span> {0}</a></li>", m.menu_child_name);
            }
        }
    }
}