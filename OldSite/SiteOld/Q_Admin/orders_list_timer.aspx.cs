using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Q_Admin_orders_list_timer : PageBase
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
        BindControl();
    }

    private void BindControl()
    {
        this.lbl_assinged_to_sum.Text = Config.ExecuteScalar(string.Format(@"
            select count(*) from (select (select assigned_to_staff_id from tb_order_assigned_to ass where ass.order_code=o.order_code order by assigned_to_id desc limit 0,1 ) assigned_to_staff_id
                from 
                tb_order_helper o where  1=1 and tag=1 and is_ok=1  
            ) t where assigned_to_staff_id='1' ", LoginUser.LoginID)).ToString();
    }
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        BindControl();
    }
}
