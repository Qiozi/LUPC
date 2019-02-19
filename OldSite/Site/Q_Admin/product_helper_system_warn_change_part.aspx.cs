using LU.Data;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Q_Admin_product_helper_system_warn_change_part : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // set part name 
            //var pm = ProductModel.GetProductModel(PartID);
            DataTable dt = Config.ExecuteDataTable("Select product_name, product_current_price-product_current_discount price from tb_product where product_serial_no='" + PartID.ToString() + "'");
            if (dt.Rows.Count == 1)
            {
                this.Label1.Text = dt.Rows[0]["product_name"].ToString() + string.Format("({0})", dt.Rows[0]["price"].ToString());
            }
            BindRadioList();
        }
    }
    

    private void BindRadioList()
    {
        var pgdm = new tb_part_group_detail();// PartGroupDetailModel();// PartGroupDetailModel.GetPartGroupDetailModelsByPartGroupID(GroupID);
        this.RadioButtonList1.DataSource = PartGroupDetailModel.FindPartIDAndNameByGroupID(GroupID);
        this.RadioButtonList1.DataValueField = "product_serial_no";
        this.RadioButtonList1.DataTextField = "product_name";
        this.RadioButtonList1.DataBind();
        this.RadioButtonList1.SelectedValue = PartID.ToString();

    }

    #region preporties
    public int PartID
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "part_id", -1); }
    }

    public int GroupID
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "group_id", -1); }
    }
    public int SystemProductID
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "system_product_serial_no", -1); }
    }
    #endregion


    protected void btn_save_Click(object sender, EventArgs e)
    {
        try
        {
            var spm = DBContext.tb_ebay_system_parts.Single(me => me.id.Equals(SystemProductID));// EbaySystemPartsModel.FindAllByProperty("id",SystemProductID)[0];
            spm.luc_sku = int.Parse(this.RadioButtonList1.SelectedValue.ToString());
            DBContext.SaveChanges();

            //            DataTable dt = Config.ExecuteDataTable(@"select system_product_serial_no from tb_system_product sp inner join tb_system_templete st 
            //on sp.system_templete_serial_no=st.system_templete_serial_no where st.tag=1 and sp.product_serial_no='" + PartID.ToString() + "'");

            DataTable dt = Config.ExecuteDataTable(@"select id from tb_ebay_system_parts esp inner join tb_ebay_system es 
on es.id=esp.system_sku where es.showit=1 and esp.luc_sku='" + PartID.ToString() + "'");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int id;
                int.TryParse(dt.Rows[i][0].ToString(), out id);

                Config.ExecuteNonQuery(string.Format("Update tb_ebay_system_parts set luc_sku='{0}' where id='{1}'", spm.luc_sku, id));
            }

            CH.Alert(KeyFields.SaveIsOK + "<br> 更新了 <span style='color:red;'>" + (dt.Rows.Count + 1).ToString() + "条</span> 纪录", this.RadioButtonList1);
            // CH.RunJavaScript("opener.window.location.reload();", this.RadioButtonList1);
        }
        catch (Exception ex)
        {
            CH.Alert(ex.Message, this.RadioButtonList1);
        }
    }
}
