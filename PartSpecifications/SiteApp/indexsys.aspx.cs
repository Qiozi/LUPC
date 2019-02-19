using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SiteApp
{
    public partial class indexsys : System.Web.UI.Page
    {
        public int ReqCateId
        {
            get { return Util.GetInt32SafeFromQueryString(Page, "cid", 0); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}