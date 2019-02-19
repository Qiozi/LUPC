using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Q_Admin_indexAdmin : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindLCD();
        }

    }
    

    void BindLCD()
    {
        DataTable dt = Config.ExecuteDataTable("Select * from tb_pre_index_page_setting where id<=6");
        rptSysList.DataSource = dt;
        rptSysList.DataBind();

      
    }
  
  

    protected void btnSave_Click(object sender, EventArgs e)
    {
        Repeater rpt = rptSysList;
        lblSysNote.Text = "<span style='color:red;'>...</span>";
        for (int i = 0; i < rptSysList.Items.Count; i++)
        {
            HiddenField id = rpt.Items[i].FindControl("hfid") as HiddenField;
            TextBox txtSku = rpt.Items[i].FindControl("txtSku") as TextBox;
            TextBox txtTitle = rpt.Items[i].FindControl("txtTitle") as TextBox;
            TextBox txtImgFilename = rpt.Items[i].FindControl("txtLCDImgFilename") as TextBox;
            TextBox txtImgX = rpt.Items[i].FindControl("txtImgX") as TextBox;
            TextBox txtImgY = rpt.Items[i].FindControl("txtImgY") as TextBox;
            TextBox txtCaseX = rpt.Items[i].FindControl("txtCaseX") as TextBox;
            TextBox txtCaseY = rpt.Items[i].FindControl("txtCaseY") as TextBox;

            Config.ExecuteNonQuery(string.Format("Update tb_pre_index_page_setting set sku='{0}', title='{1}',LCDImage='{3}',lcd_p_X='{4}',lcd_p_Y='{5}',case_p_X='{6}',case_p_Y='{7}' where id='{2}';"
               , string.IsNullOrEmpty(txtSku.Text.Trim()) ? "0" : txtSku.Text.Trim()
               , txtTitle.Text.Trim().Replace("'", "\\'")
               , id.Value
               , txtImgFilename.Text.Trim()
               , txtImgX.Text.Trim()
               , txtImgY.Text.Trim()
               , txtCaseX.Text.Trim()
               , txtCaseY.Text.Trim()
               ));
        }
        lblSysNote.Text = "<span style='color:red;'>save is OK</span>";
    }
}