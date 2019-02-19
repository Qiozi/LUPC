using LU.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Q_Admin_ebayMaster_LU_eBayTempletePageComment : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitPage();
        }
    }
    

    private void InitPage()
    {
        BindCommentList();
    }

    private void BindCommentList()
    {
        //
        // bind GridView
        this.DataList1.DataSource = GetCommentList();
        this.DataList1.DataBind();
    }

    private DataTable GetCommentList()
    {
        return Config.ExecuteDataTable("Select id, comm_name from tb_ebay_system_main_comment order by id desc");

    }

    protected void btn_submit_Click(object sender, EventArgs e)
    {
        string comm_name = this.txt_comm_name.Text.Trim();
        string comment = this.txt_comment.Text.Trim();
        try
        {
            if (VID > 0)
            {
                var esmcm = EbaySystemMainCommentModel.GetEbaySystemMainCommentModel(DBContext, VID);
                esmcm.comm_name = comm_name;
                esmcm.comment = comment;
                DBContext.SaveChanges();
            }
            else
            {
                var esmcm = new tb_ebay_system_main_comment();
                esmcm.comm_name = comm_name;
                esmcm.comment = comment;

                DBContext.tb_ebay_system_main_comment.Add(esmcm);
                DBContext.SaveChanges();
            }
            this.txt_comm_name.Text = "";
            this.txt_comment.Text = "";
        }
        catch (Exception ex)
        {
            this.Label_note.Text = ex.Message;
        }
        this.Label_note.Text = "It is OK.";
        BindCommentList();
        VID = -1;
    }

    protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
    {
        this.Label_note.Text = "";
        switch (e.CommandName)
        {
            case "EditComment":
                //int id;
                //int.TryParse(e.CommandSource, out id);
                this.txt_comm_name.Text = e.CommandArgument.ToString();
                DataTable dt = Config.ExecuteDataTable("Select * from tb_ebay_system_main_comment where id='" + e.CommandArgument.ToString() + "'");
                if (dt.Rows.Count == 1)
                {
                    VID = int.Parse(e.CommandArgument.ToString());
                    this.txt_comm_name.Text = dt.Rows[0]["comm_name"].ToString();
                    this.txt_comment.Text = dt.Rows[0]["comment"].ToString();
                }

                break;

            case "Delete":
                int id = int.Parse(e.CommandArgument.ToString());
                Config.ExecuteNonQuery("delete from tb_ebay_system_main_comment where id='" + e.CommandArgument.ToString() + "'");
                BindCommentList();
                break;
        }
    }

    #region preporites
    public int VID
    {
        get
        {
            if (ViewState["id"] == null)
                return -1;
            return int.Parse(ViewState["id"].ToString());
        }
        set { ViewState["id"] = value; }
    }
    #endregion
    protected void Button1_Click(object sender, EventArgs e)
    {
        VID = 0;
    }
}
