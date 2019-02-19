using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class cmds_SaveQuestion : PageBase
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int sku = Util.GetInt32SafeFromString(Page, "sku", 0);
            string email = Util.GetStringSafeFromString(Page, "username");
            string subject = Util.GetStringSafeFromString(Page, "subject");
            string body = Util.GetStringSafeFromString(Page, "questBody");
            string partName = "";

            if (sku == 0)
            {
                Response.Write("SKU error.");
                Response.End();
            }
            string skustr = sku.ToString();
            if (db.tb_ask_question.Count(p => p.product_serial_no.Equals(skustr)) > 6)
            {
                Response.Write("Quantity is limited.");
                Response.End();
            }

            if (sku.ToString().Length < 6)
            {
                var prod = db.tb_product.SingleOrDefault(p => p.product_serial_no.Equals(sku));
                if (prod != null)
                    partName = string.IsNullOrEmpty(prod.product_ebay_name) ? prod.product_name : prod.product_ebay_name;
            }
            else if (sku.ToString().Length == 6)
            {
                var prod = db.tb_ebay_system.SingleOrDefault(p => p.id.Equals(sku));
                if (prod != null)
                    partName = prod.system_title1;
            }
            else if (sku.ToString().Length == 8)
            {
                var prod = db.tb_sp_tmp.SingleOrDefault(p => p.sys_tmp_code.Equals(sku));
                if (prod != null)
                    partName = prod.sys_tmp_product_name;
            }
            body = string.Concat("LU SKU:" + sku, "<br/>", body);
            var question = nicklu2Model.tb_ask_question.Createtb_ask_question(0);
            question.aq_body = body;
            question.aq_email = email;
            question.create_datetime = DateTime.Now;
            question.aq_title = subject;
            question.product_serial_no = sku.ToString();
            question.aq_product_title = partName;
            question.ip = Request.UserHostAddress;
            question.aq_send = 0;
            db.AddTotb_ask_question(question);
            db.SaveChanges();
            EmailHelper.send(body
                , "It from " + email + ", <<" + subject + ">>"
                , "sales@lucomputers.com");
            Response.Write("You have successfully sent the seller a question!");
        }
        Response.End();
    }
}