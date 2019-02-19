using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Linq;
using LU.Data;

public partial class Q_Admin_order_status_history : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.ValidateLoginRule(Role.order_list);
            var context = new LU.Data.nicklu2Entities();

            BindStatusDDL(context);
            BindStatusDG(context, false);

            this.lbl_order_code.Text = OrderCode.ToString();
        }
    }

    

    public void BindStatusDDL(nicklu2Entities context)
    {
        this.ddl_status.DataSource = context.tb_facture_state.ToList();// FactureStateModel.FindAll();
        this.ddl_status.DataTextField = "facture_state_name";
        this.ddl_status.DataValueField = "facture_state_serial_no";
        // this.ddl_status.DataBind();

        var ohm = OrderHelperModel.GetModelsByOrderCode(context, OrderCode);
        if (ohm.Length == 1)
            this.ddl_status.SelectedValue = ohm[0].out_status.ToString();

        this.ddl_status.DataBind();

    }

    public void BindStatusDG(nicklu2Entities context, bool autoUpdate)
    {
        this.DataGrid1.DataSource = OrderFactureStateModel.GetModelsByOrderCode(context, OrderCode);
        this.DataGrid1.DataBind();
        this.DataGrid1.UpdateAfterCallBack = autoUpdate;
    }

    protected void lb_save_status_Click(object sender, EventArgs e)
    {
        var context = new LU.Data.nicklu2Entities();
        var model = new tb_order_facture_state();// OrderFactureStateModel();
        model.order_code = OrderCode.ToString();
        model.facture_state_serial_no = int.Parse(this.ddl_status.SelectedValue.ToString());
        model.order_facture_note = this.txt_note.Text;
        model.order_facture_state_create_date = DateTime.Now;
        context.tb_order_facture_state.Add(model);
        context.SaveChanges();
        OrderHelperModel.UpdateOutStatus(int.Parse(this.ddl_status.SelectedValue.ToString()), OrderCode);

        BindStatusDG(context, true);
        Anthem.Manager.AddScriptForClientSideEval("window.opener.location.reload()");
    }

    public int OrderCode
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "order_code", -1); }
    }
    protected void lbl_close_Click(object sender, EventArgs e)
    {
        AnthemHelper.Close();
    }
    protected void DataGrid1_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Footer && e.Item.ItemType != ListItemType.Header)
        {
            var context = new LU.Data.nicklu2Entities();
            int _status = AnthemHelper.GetAnthemDataGridCellText(e.Item, 0);
            e.Item.Cells[0].Text = FactureStateModel.GetFactureStateModel(context, _status).facture_state_name;
        }
    }
}
