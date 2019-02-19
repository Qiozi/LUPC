using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Q_Admin_advertisement_page_top : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            //
            // load current flash
            StreamReader sr = new StreamReader(Server.MapPath("/top3.asp"));
            string s = sr.ReadToEnd();
            sr.Close();
            sr.Dispose();
            if (s.IndexOf("top.swf") != -1)
                this.RadioButton_page_top_flash_1.Checked = true;
            else
                this.RadioButton_page_top_flash_2.Checked = true;
        }
    }

    protected void btn_save_page_top_flash_Click(object sender, EventArgs e)
    {
        string flash_name = "top.swf";
        if (!this.RadioButton_page_top_flash_1.Checked)
        {
            flash_name = "top_chres.swf";

        }
        StreamWriter sw = new StreamWriter(Server.MapPath("/top3.asp"));
        sw.Write(string.Format(@"<script type=""text/javascript"" src=""/js/advflash.js""></script>
<div id=""top_flash_area""></div>
<script language=""javascript"">
var FocusFlash = new sinaFlash(""/flash/{0}"", ""top_av"", 960, 128, ""7"", ""#FFFFFF"", false, ""High"");
FocusFlash.write(""top_flash_area"");
</script>", flash_name));
        sw.Close();
        sw.Dispose();


    }
}
