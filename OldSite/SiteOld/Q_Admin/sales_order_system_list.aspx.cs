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

public partial class Q_Admin_sales_order_system_list : PageBase
{
    ProductModel PM = new ProductModel();
    int _pagesize = 5;
    int _start_index = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialDatabase();
        }
    }

    public override void InitialDatabase()
    {
        base.InitialDatabase();
        BindListView();
    }
    /// <summary>
    /// 
    /// </summary>
    public void BindListView()
    {
        try
        {
            BindListView(string.Empty);
        }
        catch (Exception ex)
        {
            CH.Alert(ex.Message, this.ListView1);
        }
    }
    public void BindListView(string keyword)
    {
        int count = 0;
        DataTable dt = new DataTable();
        if (keyword != string.Empty)
        {
            OrderProductModel opm = new OrderProductModel();
            dt= opm.FindModelsForSystemList(keyword, _start_index, _pagesize, ref count);
            
        }
        else
        {
            OrderProductModel opm = new OrderProductModel();
            dt = opm.FindModelsForSystemList(_start_index, _pagesize, ref count);           
        }
        //CH.Alert(dt.Rows.Count.ToString(), this.ListView1);
        this.ListView1.DataSource = dt;
        this.ListView1.DataBind();
        this.AspNetPager1.RecordCount = count;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="system_templete_serial_no"></param>
    /// <returns></returns>
    public string GetSystemDetailList(int system_templete_serial_no)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        DataTable sdm = SpTmpDetailModel.GetModelsBySysCode(system_templete_serial_no.ToString());
        sb.Append("<ul>");
        for (int j = 0; j < sdm.Rows.Count; j++)
        {
            if (sdm.Rows[j]["is_non"].ToString() == "0" || sdm.Rows[j]["product_name"].ToString().ToLower().IndexOf("onboard") != -1)
                sb.Append("<li>&nbsp;&nbsp;&nbsp;(" + sdm.Rows[j]["product_serial_no"].ToString() + ")&nbsp;" + sdm.Rows[j]["product_name"].ToString() + "" + (sdm.Rows[j]["product_serial_no"].ToString().Length == 8 ? "" : PM.FindStockByLuSkuForOrder(int.Parse(sdm.Rows[j]["product_serial_no"].ToString()), -1)) + "</li>");
        }
        sb.Append("</ul>");
        return sb.ToString();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="system_code"></param>
    /// <returns></returns>
    public string GetSystemDetailNote(int system_code)
    {
        OrderSystemNoteModel osnm = new OrderSystemNoteModel();
        DataTable dt = osnm.FindNoteBySystemCode(system_code);
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("<table cellpadding=\"0\" cellspacing=\"0\">");
        for (int i = 0; i < dt.Rows.Count; i++)       
        {
            sb.Append("<tr>");
            sb.Append(string.Format("<td style='width:180px;'>{0}</td>", dt.Rows[i]["regdate"].ToString()));
            sb.Append(string.Format("<td><i>{0}</i></td>", dt.Rows[i]["note"].ToString()));
            sb.Append("</tr>");
        }
        sb.Append("</table>");
        return sb.ToString();
    }

    protected void ListView1_ItemCanceling(object sender, ListViewCancelEventArgs e)
    {

    }
    protected void ListView1_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        int order_code;
        int.TryParse(((Label)e.Item.FindControl("_label_order_code")).Text, out order_code);

        int system_code;
        int.TryParse(((Label)e.Item.FindControl("_label_system_templete_serial_no")).Text, out system_code);

        switch (e.CommandName)
        {
            case "SaveNote":
                TextBox _note = (TextBox)e.Item.FindControl("_txt_note");
                if (_note.Text.Trim() != "")
                {
                    OrderSystemNoteModel osnm = new OrderSystemNoteModel();
                    osnm.note = _note.Text.Trim();
                    osnm.order_code = order_code;
                    osnm.regdate = DateTime.Now;
                    osnm.system_code = system_code;
                    osnm.Create();

                    InsertTraceInfo(string.Format("Save system note:{0}||{1}", system_code, order_code));
                    this.BindListView(this.txt_keyword.Text.Trim());
                    CH.CloseParentWatting(this.ListView1);
                    CH.Alert(KeyFields.SaveIsOK, this.ListView1);                    
                }
                break;
            case "SaveFinishDate":
                TextBox _date = (TextBox)e.Item.FindControl("_txt_end_datetime");
                if (_date.Text.Trim() != "")
                {
                    OrderSystemFinishDateModel osfd = new OrderSystemFinishDateModel();
                    osfd.finish_date = DateTime.Parse(_date.Text);
                    osfd.order_code = order_code;
                    osfd.regdate = DateTime.Now;
                    osfd.system_code = system_code;
                    osfd.Create();

                    InsertTraceInfo(string.Format("Save system Estimated shipping date:{0}||{1}", system_code, order_code));
                    this.BindListView(this.txt_keyword.Text.Trim());
                    CH.CloseParentWatting(this.ListView1);
                    CH.Alert(KeyFields.SaveIsOK, this.ListView1); 
                }
                break;
        }
    }
    protected void ListView1_ItemEditing(object sender, ListViewEditEventArgs e)
    {

    }
    protected void ListView1_ItemUpdating(object sender, ListViewUpdateEventArgs e)
    {

    }
    protected void ListView1_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        Label lbl_system_templete_serial_no = (Label)e.Item.FindControl("_label_system_templete_serial_no");
        int system_code;
        int.TryParse(lbl_system_templete_serial_no.Text, out system_code);

        Literal literal_system_detail = (Literal)e.Item.FindControl("_literal_detail");
        literal_system_detail.Text = GetSystemDetailList(system_code);


        // Estimated shipping date
        OrderSystemFinishDateModel osfd = new OrderSystemFinishDateModel();
        
        TextBox _date = (TextBox)e.Item.FindControl("_txt_end_datetime");
        _date.Text= osfd.FindDateBySystemCode(system_code);

        // note
        Literal _note = (Literal)e.Item.FindControl("_literal_note");
        _note.Text = this.GetSystemDetailNote(system_code);
    }
    protected void Calendar2_SelectionChanged(object sender, EventArgs e)
    {
        Calendar c = (Calendar)sender;
        for (int i = 0; i < this.ListView1.Items.Count; i++)
        {
            Calendar _c = (Calendar)this.ListView1.Items[i].FindControl("Calendar1");
            if (_c == c)
            {
                TextBox tb = (TextBox)this.ListView1.Items[i].FindControl("_txt_end_datetime");
                tb.Text = _c.SelectedDate.ToString("yyyy-MM-dd");
            }
        }
    }
    protected void Calendar2_DayRender(object sender, DayRenderEventArgs e)
    {
        if (e.Day.IsToday)
        {
            e.Cell.ForeColor = System.Drawing.Color.Blue;
            e.Cell.BackColor = System.Drawing.Color.Pink;
        }
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        try
        {
            string keyword = this.txt_keyword.Text.Trim();
            BindListView(keyword);
            CH.CloseParentWatting(this.ListView1);
        }
        catch (Exception ex)
        {
            CH.CloseParentWatting(this.ListView1);
            CH.Alert(ex.Message, this.ListView1);
        }
    }
    protected void btn_clear_Click(object sender, EventArgs e)
    {
        try
        {
            this.txt_keyword.Text = "";
            BindListView(string.Empty);
            CH.CloseParentWatting(this.ListView1);
        }
        catch (Exception ex)
        {
            CH.CloseParentWatting(this.ListView1);
            CH.Alert(ex.Message, this.ListView1);
        }
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        _start_index = AspNetPager1.StartRecordIndex - 1;
        this.BindListView(this.txt_keyword.Text.Trim());
    }
}
