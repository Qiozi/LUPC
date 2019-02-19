using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Q_Admin_product_keyword_sort : PageBase
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
        BindLV();

        this.lbl_system_title.Text = Config.ExecuteScalar(string.Format("select concat(pc.menu_child_name, '==>', ck.keyword) n from tb_product_category_keyword ck inner join tb_product_category pc on pc.menu_child_serial_no=ck.category_id where id='{0}'", ReqID)).ToString();

    }

    #region Bind
    private void BindLV()
    {
        this.ListView1.DataSource = Config.ExecuteDataTable(string.Format(@"select * from tb_product_category_keyword_sub where parent_id='{0}' order by priority asc ", ReqID));
        this.ListView1.DataBind();
    }


    #endregion

    #region Properties
    private int ReqID
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "id", -1); }
    }
    #endregion

    protected void ListView1_ItemDataBound(object sender, ListViewItemEventArgs e)
    {

    }
    protected void ListView1_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        Button btn = (Button)e.CommandSource;
        switch (e.CommandName)
        {
            case "UP":
                int index=0;
                for (int i = 0; i < this.ListView1.Items.Count; i++)
                {
                    Button _btn = (Button)this.ListView1.Items[i].FindControl("btn_up");
                    if (btn == _btn)
                    {
                        index = i;
                        break;
                    }
                }

                if (index != 0)
                {
                    int sp_id;
                    int.TryParse(((HiddenField)e.Item.FindControl("_hf_sp_id")).Value, out sp_id);

                    int current_priority;
                    int.TryParse(((Literal)e.Item.FindControl("_literal_priority")).Text, out current_priority);

                    int pre_sp_id;
                    int.TryParse(((HiddenField)this.ListView1.Items[index - 1].FindControl("_hf_sp_id")).Value, out pre_sp_id);

                    int pre_priority;
                    int.TryParse(((Literal)this.ListView1.Items[index - 1].FindControl("_literal_priority")).Text, out pre_priority);
                    if (pre_priority == 0)
                        current_priority = 888;

                    if (sp_id > 0)
                    {
                        Config.ExecuteNonQuery(string.Format(@"Update tb_product_category_keyword_sub set priority='{0}' where id='{1}';
                    Update tb_product_category_keyword_sub set priority='{2}' where id='{3}';", pre_priority, sp_id, current_priority, pre_sp_id));
                        InsertTraceInfo(string.Format("Modify keyword sort in category . :{0}::{1}", sp_id, pre_sp_id));
                    }
                }
                BindLV();
                break;
        }
        
        CH.CloseParentWatting(this.ListView1);
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < ListView1.Items.Count; i++)
        {
            int sp_id;
            int.TryParse(((HiddenField)ListView1.Items[i].FindControl("_hf_sp_id")).Value, out sp_id);

            Config.ExecuteNonQuery(string.Format(@"Update tb_product_category_keyword_sub set priority='{0}' where id='{1}';", i + 1, sp_id));
        }
        BindLV();
       
    }
}

