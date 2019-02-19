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

public partial class Q_Admin_UC_order_list : CtrlBase
{

    int pageSize = 30;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialDatabase();
        }
    }

    public void InitialDatabase()
    {
        SearchIndex = 1;

        FactureStateDB = FactureStateModel.FindModelsByShowit();
        PreStatusDB = PreStatusModel.FindModelsByShowit();
        PayMethodDB = PayMethodNewModel.FindAll();

        BindListView(0, pageSize);
        BindPayMethods();
        BindOutStatus();
    }

    private void BindPayMethods()
    {
        this.DropDownList_pay_method.DataSource = PayMethodDB;
        this.DropDownList_pay_method.DataTextField = "pay_method_short_name";
        this.DropDownList_pay_method.DataValueField = "pay_method_serial_no";
        this.DropDownList_pay_method.DataBind();
    }
    /// <summary>
    /// 绑定后台订单状态
    /// </summary>
    public void BindOutStatus()
    {
        this.DropDownList_OutStatus.DataSource = this.FactureStateDB;
        this.DropDownList_OutStatus.DataTextField = "facture_state_name";
        this.DropDownList_OutStatus.DataValueField = "facture_state_serial_no";
        this.DropDownList_OutStatus.DataBind();
        this.DropDownList_OutStatus.Items.Insert(0, new ListItem("select", "-1"));
    }
    private void BindListView(int startIndex, int endIndex)
    {
        int count = 0;
        int top_count = pageSize;
        DataTable OrderListDT = new DataTable();

        if (SearchIndex == 2)
            OrderListDT = OrderHelperModel.GetModelsBySearch("", "-1", this.DropDownList_OutStatus.SelectedValue.ToString(), 0, top_count, startIndex, endIndex, ref count);
        else if (SearchIndex == 3)
            OrderListDT = OrderHelperModel.GetModelsBySearch("", "-1", "-1", int.Parse(this.DropDownList_pay_method.SelectedValue.ToString()), top_count, startIndex, endIndex, ref count);
        else if (SearchIndex == 4)
        {
            OrderListDT = OrderHelperModel.GetModelsBySearch("", "-1", "-1", 0, top_count, startIndex, endIndex, ref count);
            //this.AspNetPager1.CurrentPageIndex = 0;
        }
        else
        {
            string keyword = this.txt_keyword.Text.Trim();
            if (keyword.Length > 0)
                keyword = keyword.Replace("788", "").Replace("660", "");
            OrderListDT = OrderHelperModel.GetModelsBySearch(keyword, this.ddl_search_filed.SelectedValue, "-1", 0, top_count, startIndex, endIndex, ref count);
        }
        CurrentPageCount = count;
        this.AspNetPager1.RecordCount = count;
        this.ListView1.DataSource = OrderListDT;
        this.ListView1.DataBind();

    }

    #region Properties
    /// <summary>
    /// store facture state datatable.
    /// </summary>
    public FactureStateModel[] FactureStateDB
    {
        get { return (FactureStateModel[])ViewState["FactureStateDB"]; }
        set { ViewState["FactureStateDB"] = value; }
    }

    /// <summary>
    /// store pre atatus datatable.
    /// </summary>
    public PreStatusModel[] PreStatusDB
    {
        get { return (PreStatusModel[])ViewState["PreStatusDB"]; }
        set { ViewState["PreStatusDB"] = value; }
    }

    public PayMethodNewModel[] PayMethodDB
    {
        get { return (PayMethodNewModel[])ViewState["PayMethodDB"]; }
        set { ViewState["PayMethodDB"] = value; }
    }
    public int SearchIndex
    {
        get { return (int)ViewState["SearchIndex"]; }
        set { ViewState["SearchIndex"] = value; }
    }

    public int CurrentPageCount
    {
        get { return (int)ViewState["CurrentPageCount"]; }
        set { ViewState["CurrentPageCount"] = value; }
    }
    #endregion

    #region Events

    protected void ListView1_ItemEditing(object sender, ListViewEditEventArgs e)
    {
        this.ListView1.EditIndex = e.NewEditIndex;
        this.BindListView(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);

        ListView lv = (ListView)sender;
        HiddenField _fh_out_status = (HiddenField)lv.Items[e.NewEditIndex].FindControl("_hf_back_end_status");
        HiddenField _fh_pre_status = (HiddenField)lv.Items[e.NewEditIndex].FindControl("_hf_pre_status");

        DropDownList _ddl_out_status = (DropDownList)lv.Items[e.NewEditIndex].FindControl("_dropDownList_out_status");
        DropDownList _ddl_pre_status = (DropDownList)lv.Items[e.NewEditIndex].FindControl("_dropDownList_pre_status");
        _ddl_out_status.SelectedValue = _fh_out_status.Value;
        _ddl_pre_status.SelectedValue = _fh_pre_status.Value;




    }
    protected void ListView1_ItemUpdated(object sender, ListViewUpdatedEventArgs e)
    {



    }
    protected void ListView1_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        //try
        //{
        //    switch (e.CommandName)
        //    {
        //        case "Cancel":

        //            break;
        //        case "Update":
        //            int id = 0;
        //            Literal _lt_order_helper_serial_no = (Literal)e.Item.FindControl("_lt_order_helper_serial_no");
        //            int.TryParse(_lt_order_helper_serial_no.Text, out id);

        //            DropDownList ddl = (DropDownList)e.Item.FindControl("_dropDownList_out_status");
        //            int out_status = int.Parse(ddl.SelectedValue);

        //            DropDownList pre_ddl = (DropDownList)e.Item.FindControl("_dropDownList_pre_status");
        //            int pre_status = int.Parse(pre_ddl.SelectedValue.ToString());
        //            if (out_status == -1 || pre_status == -1)
        //            {
        //                ScriptManager.RegisterClientScriptBlock(this.ListView1, this.ListView1.GetType(), "alert", "sAlert(\"请选择正确的状态\")", true);
        //                return;
        //            }

        //            TextBox _txt_note = (TextBox)e.Item.FindControl("_txt_order_note");

        //            OrderHelperModel.UpdateOutStatus(out_status, pre_status, _txt_note.Text, id);



        //            this.Page.InsertTraceInfo("Save Order Note And Status:" + id.ToString());

        //            ScriptManager.RegisterClientScriptBlock(this.ListView1, this.ListView1.GetType(), "alert", "sAlert(\"" + KeyFields.SaveIsOK + "\")", true);
        //            this.ListView1.EditIndex = -1;
        //            this.BindListView(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
        //            //OrderFactureStateModel.SaveModels(this.lbl_current_order_code.Text, out_status, "");
        //            break;
        //    }
        //}
        //catch (Exception ex)
        //{
        //    ScriptManager.RegisterClientScriptBlock(this.ListView1, this.ListView1.GetType(), "alert", "sAlert(\"" + KeyFields.SaveIsOK + "\")", true);

        //}
    }

    protected void ListView1_ItemUpdating(object sender, ListViewUpdateEventArgs e)
    {

    }
    protected void ListView1_ItemCanceling(object sender, ListViewCancelEventArgs e)
    {
        this.ListView1.InsertItemPosition = InsertItemPosition.None;
        this.ListView1.EditIndex = -1;
        this.BindListView(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);

    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        try
        {
            SearchIndex = 1;
            this.AspNetPager1.CurrentPageIndex = 0;
            this.BindListView(0, pageSize);
            //ScriptManager.RegisterClientScriptBlock(this.ListView1, this.ListView1.GetType(), "alert", "sAlert(\""+SearchIndex.ToString()+"\")", true);

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.ListView1, this.ListView1.GetType(), "alert", "sAlert(\"" + ex.Message + "\")", true);

        }
    }

    protected void btn_search_2_Click(object sender, EventArgs e)
    {
        SearchIndex = 2;
        this.txt_keyword.Text = "";
        this.AspNetPager1.CurrentPageIndex = 0;
        this.BindListView(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);

    }

    protected void btn_search_3_Click(object sender, EventArgs e)
    {
        SearchIndex = 3;
        this.txt_keyword.Text = "";
        this.AspNetPager1.CurrentPageIndex = 0;
        this.BindListView(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
    }
    protected void btn_new_order_Click(object sender, EventArgs e)
    {
        int customerID = 8888;
        int OrderCode = OrderHelperModel.GetNewOrderCode();
        OrderHelperModel ohm = new OrderHelperModel();
        ohm.order_code = OrderCode;
        ohm.customer_serial_no = customerID;
        ohm.create_datetime = DateTime.Now;
        ohm.order_date = DateTime.Now;
        ohm.tag = 1;
        ohm.Create();
        CustomerHelper ch = new CustomerHelper();
        ch.CopyCustomer(OrderCode.ToString(), customerID);
        this.Page.InsertTraceInfo("Create One Order (" + OrderCode.ToString() + ")");

        OrderHelperModel[] oh = OrderHelperModel.GetModelsByOrderCode(OrderCode);
        CustomerStoreModel[] csm = CustomerStoreModel.FindModelsByOrderCode(OrderCode.ToString());

        for (int i = 0; i < oh.Length; i++)
        {
            oh[i].pay_method = Config.pay_method_pick_up_id_default.ToString();
            oh[i].Update();

            csm[0].pay_method = Config.pay_method_pick_up_id_default;
            csm[0].Update();
        }
        Response.Redirect("sale_add_order.aspx?menu_id=2&order_code=" + OrderCode.ToString() + "&pay_method=" + Config.pay_method_pick_up_ids.ToString());

    }
    protected void btn_clear_search_Click(object sender, EventArgs e)
    {
        SearchIndex = 4;
        this.txt_keyword.Text = "";
        this.ListView1.EditIndex = -1;
        this.AspNetPager1.CurrentPageIndex = 0;
        this.BindListView(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
    }
    #endregion


    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {

        this.BindListView(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
    }
}
