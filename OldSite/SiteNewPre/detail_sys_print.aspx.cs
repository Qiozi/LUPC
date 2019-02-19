using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class detail_sys_print : PageBase
{
    public string CaseImgUrl = "";
    public string SysListString = "";
    public string RegularPrice = "";
    public string PriceUnit = "";
    public string SpecialCashPrice = "";
    public string QuoteNumber = "";
    public string SysDate = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            string sysCode = ReqSKU.ToString();
            var sysList =  (from ep in db.tb_sp_tmp_detail
                            join p in db.tb_product on ep.product_serial_no.Value equals p.product_serial_no
                            where ep.sys_tmp_code.Equals(sysCode) && p.product_serial_no != setting.NoneSelectedID
                           select new
                           {
                               CommentName =ep.cate_name,
                               PartTitle = ep.product_name,
                               IsCase = ep.cate_name.ToLower().Trim() =="case"?true:false,
                               PartSku = ep.product_serial_no
                              
                           }).ToList();
            if (sysList.Count == 0)
            {
                SysListString = "<p style='padding:5em;'> Sorry, No data.</p><script>$(document).ready(function(){ $('#sysLogoArea').css({display:'none'});$('#sysComment').css({display:'none'});});</script>";
            }
            else
            {
                QuoteNumber = ReqSKU.ToString();
                var sys = db.tb_sp_tmp.FirstOrDefault(p => p.sys_tmp_code.Equals(QuoteNumber));
                if (sys != null)
                {
                    SysDate = sys.create_datetime.ToString();
                    PriceUnit = sys.price_unit;
                    SpecialCashPrice = PriceRate.Format(sys.sys_tmp_price.Value * (1 - setting.CardRate));
                    RegularPrice = PriceRate.Format(sys.sys_tmp_price.Value);
                }

            
                SysListString = "<table class='table table-striped'>";
                for (int i = 0; i < sysList.Count; i++)
                {
                    SysListString += "<tr><td><b>" + sysList[i].CommentName + "</b></td>";
                    SysListString += "<td>" + sysList[i].PartTitle + "</td>";
                    SysListString += "</tr>";

                    if (sysList[i].IsCase)
                        CaseImgUrl = "https://lucomputers.com/pro_img/COMPONENTS/" + sysList[i].PartSku + "_list_2.jpg";
                }
                SysListString += "</table>";
            }
        }
    }
}