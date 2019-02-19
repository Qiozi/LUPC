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

public partial class Q_Admin_change_state_priority : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            InitialDatabase();
    }

    public override void InitialDatabase()
    {
        base.InitialDatabase();
        BindDV();
    }

    private void BindDV()
    {
        StateShippingModel ssm = new StateShippingModel();
        this.GridView1.DataSource = ssm.FindAllByPriority();
        this.GridView1.DataBind();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        for (int i = 0; i < this.GridView1.Rows.Count; i++)
        {
            int _state_serial_no = int.Parse(this.GridView1.Rows[i].Cells[0].Text);
            int _priority = 0;
            int.TryParse(((TextBox)this.GridView1.Rows[i].FindControl("_txt_priority")).Text, out _priority);
            StateShippingModel ssm = StateShippingModel.GetStateShippingModel(_state_serial_no);
            ssm.priority = _priority;
            ssm.Update();
        }
        Response.Write("<script> alert('数据已保存，请重新查询统计');</script>");
        BindDV();
    }
}
