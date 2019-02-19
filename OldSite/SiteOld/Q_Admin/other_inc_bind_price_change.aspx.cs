using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Q_Admin_other_inc_bind_price_change : PageBase
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
        

        //
        // load mark.
        // 
        this.ddl_record_mark.DataSource = Config.ExecuteDataTable(@"
select 'of_late' mark 
union 
select distinct mark from tb_other_inc_bind_price_tmp_history order by mark desc limit 0,10 ");
        this.ddl_record_mark.DataTextField = "mark";
        this.ddl_record_mark.DataValueField = "mark";
        this.ddl_record_mark.DataBind();

        BindLV();
    }

    public void RunScript()
    {
        this.literal_run_script.Text = @"<script>
MergeCellsVertical(document.getElementById('table_part_list'), 0);
</script>";

    }

    private void BindLV()
    {
        string mark = this.ddl_record_mark.SelectedValue.ToString();
        this.lbl_mark.Text = mark;
        DataTable dt = new DataTable();
        if (this.ddl_record_mark.SelectedIndex == 0)
        {
            dt = Config.ExecuteDataTable(@"
select ob.product_serial_no lu_sku, p.product_name,pc.menu_child_name, p.product_current_cost new_cost, p.product_current_price new_price, category_id,
ob.product_current_price old_price,  ob.product_current_cost old_cost ,
case when p.product_current_price<= ob.product_current_cost then 'blue'
 when p.product_current_price<=  p.product_current_cost then 'blue'
 when p.product_current_price<=  p.product_current_price then '#cccccc'
 when p.product_current_price>ob.product_current_price then 'green'
else 'red' end as color
,'"+ this.ddl_record_mark.Items[0].Text+@"' mark
from tb_product p inner join tb_other_inc_bind_price_tmp ob on ob.product_serial_no=p.product_serial_no 
inner join tb_product_category pc on pc.menu_child_serial_no = p.menu_child_serial_no order by pc.menu_child_order, id asc 
");
        }
        else
        {
            dt = Config.ExecuteDataTable(@"
select ob.product_serial_no lu_sku, p.product_name,pc.menu_child_name, ob.other_inc_price new_cost, ob.product_current_price_tmp new_price, category_id,
ob.product_current_price old_price,  ob.product_current_cost old_cost ,
case when ob.product_current_price_tmp<= ob.product_current_cost then 'blue'
 when p.product_current_price<=  ob.other_inc_price then 'blue'
 when p.product_current_price<=  ob.product_current_price_tmp then '#cccccc'
 when ob.product_current_price_tmp>ob.product_current_price then 'green'
else 'red' end as color
, ob.mark
from tb_product p inner join tb_other_inc_bind_price_tmp_history ob on ob.product_serial_no=p.product_serial_no
            inner join tb_product_category pc on pc.menu_child_serial_no = p.menu_child_serial_no  where mark='" + mark + @"' order by pc.menu_child_order, id asc 
            ");
        }
        this.lv_change_list.DataSource = dt;
        this.lv_change_list.DataBind();
        RunScript();
    }
    protected void ddl_record_mark_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindLV();
    }
    protected void lv_change_list_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        Label _lbl_new_cost = (Label)e.Item.FindControl("_lbl_new_cost");
        decimal new_cost;
        decimal.TryParse(_lbl_new_cost.Text, out new_cost);

        Label _lbl_old_cost = (Label)e.Item.FindControl("_lbl_old_cost");
        decimal old_cost;
        decimal.TryParse(_lbl_old_cost.Text, out old_cost);

        Label _lbl_differences = (Label)e.Item.FindControl("_lbl_differences");
        _lbl_differences.Text = (new_cost - old_cost).ToString();

        if (new_cost > old_cost)
        {
            _lbl_new_cost.ForeColor = System.Drawing.Color.Green;
            _lbl_differences.ForeColor = System.Drawing.Color.Green;
        }
    }
}
