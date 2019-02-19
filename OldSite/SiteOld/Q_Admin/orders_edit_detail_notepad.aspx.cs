using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Q_Admin_orders_edit_detail_notepad : PageBase
{
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
        BindList();

    }

    void BindList()
    {
        DataTable dt = Config.ExecuteDataTable("Select * from tb_order_notepad where OrderCode='" + ReqOrderCode.ToString() + "'");
        this.dl_msg_list.DataSource = dt;
        this.dl_msg_list.DataBind();
        Response.Write(@"<script>window.parent.loadOrderNotepad(""" + ReqOrderCode.ToString() + @""");</script>");
    }

    protected void Button_save_Click(object sender, EventArgs e)
    {
        if (ReqOrderCode > 0)
        {
            string msg = this.TextBox1.Text;
            OrderNotepadModel onm = new OrderNotepadModel();
            onm.Msg = msg;
            onm.OrderCode = ReqOrderCode;
            onm.Author = LoginUser.RealName;
            onm.Create();
            BindList();
           
        }
    }

    int ReqOrderCode
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "order_code", -1); }
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton ib = sender as ImageButton;
        //int id;
       // int.TryParse(ib.an

        //for (int i = 0; i < dl_msg_list.Items.Count; i++)
        //{
        //    ImageButton childIB = dl_msg_list.Items[i].FindControl("_imgButtonDelete") as ImageButton;
        //    if (childIB == ib)
        //    {
        //        this.Button_save.Text = "DD";
        //    }
        //}
    }
    protected void dl_msg_list_DeleteCommand(object source, DataListCommandEventArgs e)
    {
        int id;
        int.TryParse(e.CommandArgument.ToString(), out id);
        Config.ExecuteNonQuery("Delete from tb_order_notepad where id='" + id.ToString() + "'");
     
        BindList();
    }
}