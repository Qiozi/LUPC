using LU.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Q_Admin_other_inc_bind_price : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //
            //
            // load other inc
            LtdHelper lh = new LtdHelper();
            DataTable dt = lh.LtdHelperWholesalerToDT();
            this.ddl_other_inc.DataSource = dt;
            this.ddl_other_inc.DataTextField = "text";
            this.ddl_other_inc.DataValueField = "id";
            this.ddl_other_inc.DataBind();

            this.lb_vendor_manufacture.DataSource = dt;
            this.lb_vendor_manufacture.DataTextField = "text";
            this.lb_vendor_manufacture.DataValueField = "id";
            this.lb_vendor_manufacture.DataBind();

            this.lb_vendor_real_price.DataSource = dt;
            this.lb_vendor_real_price.DataTextField = "text";
            this.lb_vendor_real_price.DataValueField = "id";
            this.lb_vendor_real_price.DataBind();


            this.lb_vendor_real_price_cate.DataSource = dt;
            this.lb_vendor_real_price_cate.DataTextField = "text";
            this.lb_vendor_real_price_cate.DataValueField = "id";
            this.lb_vendor_real_price_cate.DataBind();

            //
            // load vendor
            this.lb_manufacture.DataSource = Config.ExecuteDataTable("select distinct producter_serial_no from tb_product where tag=1 and producter_serial_no<>'' order by producter_serial_no asc ");
            this.lb_manufacture.DataTextField = "producter_serial_no";
            this.lb_manufacture.DataValueField = "producter_serial_no";
            this.lb_manufacture.DataBind();

            this.lb_manufacture_real_price.DataSource = Config.ExecuteDataTable("select distinct producter_serial_no from tb_product where tag=1 and producter_serial_no<>'' order by producter_serial_no asc ");
            this.lb_manufacture_real_price.DataTextField = "producter_serial_no";
            this.lb_manufacture_real_price.DataValueField = "producter_serial_no";
            this.lb_manufacture_real_price.DataBind();



            BindLV();
        }
    }
    
    public void RunScript()
    {
        this.Literal1.Text = @"
<script>
MergeCellsVertical(document.getElementById('table_part_list'), 0);
</script>";

    }
    private void BindLV()
    {
        this.lv_other_inc_bind_info.DataSource = Config.ExecuteDataTable(@"select id, priority, ifnull(pc.menu_child_name, '') category_name
, ifnull(manufactory, '') manufacture,other_inc_id, case when is_relating=1 then 'Y' else 'N' end as relating 
, case when bind_type=1 then 'Category' when bind_type=2 then 'Manufacture' when bind_type=3 then 'Real Price Manufacture' when bind_type=4 then 'Real Price Category' end as bind_type_name from tb_other_inc_bind_price oi left join tb_product_category pc on pc.menu_child_serial_no=oi.category_id where is_single=0
order by category_id, id asc 
");
        this.lv_other_inc_bind_info.DataBind();
        RunScript();
    }

    protected void btn_Save_category_Click(object sender, EventArgs e)
    {
        int other_inc_id;
        int.TryParse(this.ddl_other_inc.SelectedValue.ToString(), out other_inc_id);
        int luc_sku;
        int.TryParse(this.txt_lu_sku.Text, out luc_sku);
        string manufactory = this.lb_manufacture.Text.Trim();

        if (this.CategoryDropDownLoad1.categoryId > 0 && other_inc_id != 0)
        {

            if (Config.ExecuteScalarInt32(string.Format(@"select count(id) c  from tb_other_inc_bind_price where  bind_type = 1  {2}   and is_single = '{3}' and luc_sku='{1}' 
and  category_id='{0}' ", this.CategoryDropDownLoad1.categoryId, luc_sku, " and (manufactory='' or manufactory is null) ", luc_sku > 0 ? 1 : 0)) > 0)
            {
                Config.ExecuteNonQuery(string.Format(@"update tb_other_inc_bind_price set is_relating=1 where bind_type = 1  {2}  and is_single = '{3}' and luc_sku='{1}' 
and  category_id='{0}' ", this.CategoryDropDownLoad1.categoryId, luc_sku, " and (manufactory='' or manufactory is null) ", luc_sku > 0 ? 1 : 0));
                CH.Alert("配置已存在<br/>相同的配置不能与多个上家挂勾<br/>变化为‘挂勾’状态成功", this.Literal1);
            }
            else
            {

                var oibpm = new tb_other_inc_bind_price();// OtherIncBindPriceModel();
                if (luc_sku > 0)
                {
                    oibpm.luc_sku = luc_sku;
                    oibpm.is_single = true;
                }
                else
                {
                    oibpm.is_single = false;
                }

                oibpm.bind_type = 1;

                oibpm.category_id = this.CategoryDropDownLoad1.categoryId;
                oibpm.other_inc_id = other_inc_id;

                oibpm.is_relating = true;
                oibpm.priority = Config.ExecuteScalarInt32("select ifnull(c, 1) c from (select max(priority +1) c from tb_other_inc_bind_price)tmp");
                DBContext.tb_other_inc_bind_price.Add(oibpm);
                DBContext.SaveChanges(); 
                CH.Alert(KeyFields.SaveIsOK, this.Literal1);

            }
            BindLV();
        }
        else
        {
            CH.Alert("Please select", this.Literal1);
            if (other_inc_id == 0)
                this.ddl_other_inc.Focus();
            else
                this.CategoryDropDownLoad1.Focus();
        }

    }

    protected void lv_other_inc_bind_info_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        LtdHelper lh = new LtdHelper();

        Label lbl_other_inc = (Label)e.Item.FindControl("_lbl_other_inc_name");
        if (lbl_other_inc.Text != "0")
            lbl_other_inc.Text = lh.FilterText(lh.LtdModelByValue(int.Parse(lbl_other_inc.Text)).ToString());

        Label lbl_relating = (Label)e.Item.FindControl("_lbl_relating");
        if (lbl_relating.Text == "Y")
            lbl_relating.ForeColor = System.Drawing.Color.FromName("green");
    }
    protected void lv_other_inc_bind_info_ItemCommand(object sender, ListViewCommandEventArgs e)
    {

        switch (e.CommandName)
        {
            case "Up":

                break;
            case "DeleteRecord":
                Label lbl_id = (Label)e.Item.FindControl("_lbl_id");
                Config.ExecuteNonQuery("delete from  tb_other_inc_bind_price where id='" + lbl_id.Text + "'");
                BindLV();
                break;
            case "Relating":
                Label lbl_manufacture = (Label)e.Item.FindControl("_lbl_manufacture");
                if (lbl_manufacture.Text.Trim() != "")
                {
                    Label lbl_relating = (Label)e.Item.FindControl("_lbl_relating");
                    Config.ExecuteNonQuery("update  tb_other_inc_bind_price set is_relating='" + (lbl_relating.Text == "Y" ? "0" : "1") + "' where  id='" + e.CommandArgument.ToString() + "'");
                }
                else
                {
                    CH.Alert("不能对整个目录进行脱机绑定，如果需要，请删除已存在的目录绑定即可", this.Literal1);
                }
                BindLV();
                break;
        }
    }
    //    protected void btn_unrelating_Click(object sender, EventArgs e)
    //    {
    //        int other_inc_id;
    //        int.TryParse(this.ddl_other_inc.SelectedValue.ToString(), out other_inc_id);
    //        int luc_sku;
    //        int.TryParse(this.txt_lu_sku.Text, out luc_sku);

    //        string manufactory = this.lb_manufacture.Text.Trim();

    //        if (this.CategoryDropDownLoad1.categoryId > 0 && other_inc_id != 0)
    //        {
    //            if (Config.ExecuteScalarInt32(string.Format(@"select count(*) c  from tb_other_inc_bind_price where   1=1  {2}   and is_single = '{3}' and luc_sku='{1}' 
    //and  category_id='{0}' ", this.CategoryDropDownLoad1.categoryId, luc_sku, this.cb_none.Checked ? "  and (manufactory='' or manufactory is null)  " : "  and manufactory='" + manufactory + "' ", luc_sku > 0 ? 1 : 0)) > 0)
    //            {
    //                Config.ExecuteNonQuery(string.Format(@"update tb_other_inc_bind_price set is_relating=0 where   1=1  {2}   and is_single = '{3}' and luc_sku='{1}' 
    //and  category_id='{0}' ", this.CategoryDropDownLoad1.categoryId, luc_sku, this.cb_none.Checked ? "  and (manufactory='' or manufactory is null)  " : "  and manufactory='" + manufactory + "' ", luc_sku > 0 ? 1 : 0));
    //                CH.Alert("变化为‘脱勾’状态成功", this.Literal1);
    //            }
    //            else
    //            {

    //                OtherIncBindPriceModel oibpm = new OtherIncBindPriceModel();
    //                if (luc_sku > 0)
    //                {
    //                    oibpm.luc_sku = luc_sku;
    //                    oibpm.is_single = true;
    //                }
    //                else
    //                {
    //                    oibpm.is_single = false;
    //                }
    //                if (this.cb_none.Checked)
    //                {
    //                    oibpm.bind_type = 1;
    //                }
    //                else
    //                {
    //                    oibpm.bind_type = 2;
    //                    oibpm.manufactory = manufactory;
    //                }

    //                oibpm.category_id = this.CategoryDropDownLoad1.categoryId;
    //                oibpm.other_inc_id = other_inc_id;

    //                oibpm.is_relating = false;
    //                oibpm.priority = Config.ExecuteScalarInt32("select ifnull(c, 1) c from (select max(priority +1) c from tb_other_inc_bind_price)tmp");
    //                oibpm.Create();
    //                CH.Alert(KeyFields.SaveIsOK, this.Literal1);

    //            }
    //            BindLV();
    //        }
    //        else
    //        {
    //            CH.Alert("Please select", this.Literal1);
    //            if (other_inc_id == 0)
    //                this.ddl_other_inc.Focus();
    //            else
    //                this.CategoryDropDownLoad1.Focus();
    //        }
    //    }
    protected void btn_relating_manufacture_Click(object sender, EventArgs e)
    {
        int other_inc_id;
        int.TryParse(this.lb_vendor_manufacture.SelectedValue.ToString(), out other_inc_id);
        int luc_sku;
        int.TryParse(this.txt_lu_sku.Text, out luc_sku);
        string manufactory = this.lb_manufacture.Text.Trim();

        if (this.CategoryDropDownLoad1.categoryId > 0 && other_inc_id != 0)
        {

            if (Config.ExecuteScalarInt32(string.Format(@"select count(id) c  from tb_other_inc_bind_price where  bind_type = 2  {2}   and is_single = '{3}' and luc_sku='{1}' 
and  category_id='{0}' ", this.CategoryDropDownLoad1.categoryId, luc_sku, "  and manufactory='" + manufactory + "'  ", luc_sku > 0 ? 1 : 0)) > 0)
            {
                Config.ExecuteNonQuery(string.Format(@"update tb_other_inc_bind_price set is_relating=1 where bind_type = 2  {2}  and is_single = '{3}' and luc_sku='{1}' 
and  category_id='{0}' ", this.CategoryDropDownLoad1.categoryId, luc_sku, "   and manufactory='" + manufactory + "'  ", luc_sku > 0 ? 1 : 0));
                CH.Alert("配置已存在<br/>相同的配置不能与多个上家挂勾<br/>变化为‘挂勾’状态成功", this.Literal1);
            }
            else
            {

                var oibpm = new tb_other_inc_bind_price();// OtherIncBindPriceModel();
                if (luc_sku > 0)
                {
                    oibpm.luc_sku = luc_sku;
                    oibpm.is_single = true;
                }
                else
                {
                    oibpm.is_single = false;
                }

                oibpm.bind_type = 2;
                oibpm.manufactory = manufactory;
                oibpm.category_id = this.CategoryDropDownLoad1.categoryId;
                oibpm.other_inc_id = other_inc_id;

                oibpm.is_relating = true;
                oibpm.priority = Config.ExecuteScalarInt32("select ifnull(c, 1) c from (select max(priority +1) c from tb_other_inc_bind_price)tmp");
                DBContext.tb_other_inc_bind_price.Add(oibpm);
                DBContext.SaveChanges();
                CH.Alert(KeyFields.SaveIsOK, this.Literal1);

            }
            BindLV();
        }
        else
        {
            CH.Alert("Please Select Category", this.Literal1);
            if (other_inc_id == 0)
                this.ddl_other_inc.Focus();
            else
                this.CategoryDropDownLoad1.Focus();
        }
    }
    protected void btn_relating_real_price_Click(object sender, EventArgs e)
    {
        int other_inc_id;
        int.TryParse(this.lb_vendor_real_price.SelectedValue.ToString(), out other_inc_id);
        int luc_sku;
        int.TryParse(this.txt_lu_sku.Text, out luc_sku);
        string manufactory = this.lb_manufacture_real_price.Text.Trim();

        if (this.CategoryDropDownLoad1.categoryId > 0 && other_inc_id != 0)
        {

            if (Config.ExecuteScalarInt32(string.Format(@"select count(id) c  from tb_other_inc_bind_price where  bind_type = 4  {2}   and is_single = '{3}' and luc_sku='{1}' 
and  category_id='{0}' ", this.CategoryDropDownLoad1.categoryId, luc_sku, "   and manufactory='" + manufactory + "' ", luc_sku > 0 ? 1 : 0)) > 0)
            {
                Config.ExecuteNonQuery(string.Format(@"update tb_other_inc_bind_price set is_relating=1 where bind_type = 4  {2}  and is_single = '{3}' and luc_sku='{1}' 
and  category_id='{0}' ", this.CategoryDropDownLoad1.categoryId, luc_sku, "   and manufactory='" + manufactory + "'  ", luc_sku > 0 ? 1 : 0));
                CH.Alert("配置已存在<br/>相同的配置不能与多个上家挂勾<br/>变化为‘挂勾’状态成功", this.Literal1);
            }
            else
            {

                var oibpm = new tb_other_inc_bind_price();
                if (luc_sku > 0)
                {
                    oibpm.luc_sku = luc_sku;
                    oibpm.is_single = true;
                }
                else
                {
                    oibpm.is_single = false;
                }

                oibpm.bind_type = 4;
                oibpm.manufactory = manufactory;
                oibpm.category_id = this.CategoryDropDownLoad1.categoryId;
                oibpm.other_inc_id = other_inc_id;

                oibpm.is_relating = true;
                oibpm.priority = Config.ExecuteScalarInt32("select ifnull(c, 1) c from (select max(priority +1) c from tb_other_inc_bind_price)tmp");

                DBContext.tb_other_inc_bind_price.Add(oibpm);
                DBContext.SaveChanges();
                CH.Alert(KeyFields.SaveIsOK, this.Literal1);

            }
            BindLV();
        }
        else
        {
            CH.Alert("Please Select Category", this.Literal1);
            if (other_inc_id == 0)
                this.ddl_other_inc.Focus();
            else
                this.CategoryDropDownLoad1.Focus();
        }
    }
    protected void btn_relating_real_price_cate_Click(object sender, EventArgs e)
    {
        int other_inc_id;
        int.TryParse(this.lb_vendor_real_price_cate.SelectedValue.ToString(), out other_inc_id);
        int luc_sku;
        int.TryParse(this.txt_lu_sku.Text, out luc_sku);
        string manufactory = this.lb_manufacture_real_price.Text.Trim();

        if (this.CategoryDropDownLoad1.categoryId > 0 && other_inc_id != 0)
        {

            if (Config.ExecuteScalarInt32(string.Format(@"select count(id) c  from tb_other_inc_bind_price where  bind_type = 3  {2}   and is_single = '{3}' and luc_sku='{1}' 
and  category_id='{0}' ", this.CategoryDropDownLoad1.categoryId, luc_sku, "   and (manufactory='' or manufactory is null) ", luc_sku > 0 ? 1 : 0)) > 0)
            {
                Config.ExecuteNonQuery(string.Format(@"update tb_other_inc_bind_price set is_relating=1 where bind_type = 3  {2}  and is_single = '{3}' and luc_sku='{1}' 
and  category_id='{0}' ", this.CategoryDropDownLoad1.categoryId, luc_sku, "    and (manufactory='' or manufactory is null) ", luc_sku > 0 ? 1 : 0));
                CH.Alert("配置已存在<br/>相同的配置不能与多个上家挂勾<br/>变化为‘挂勾’状态成功", this.Literal1);
            }
            else
            {

                var oibpm = new tb_other_inc_bind_price();
                if (luc_sku > 0)
                {
                    oibpm.luc_sku = luc_sku;
                    oibpm.is_single = true;
                }
                else
                {
                    oibpm.is_single = false;
                }

                oibpm.bind_type = 3;

                oibpm.category_id = this.CategoryDropDownLoad1.categoryId;
                oibpm.other_inc_id = other_inc_id;

                oibpm.is_relating = true;
                oibpm.priority = Config.ExecuteScalarInt32("select ifnull(c, 1) c from (select max(priority +1) c from tb_other_inc_bind_price)tmp");
                DBContext.tb_other_inc_bind_price.Add(oibpm);
                DBContext.SaveChanges();
                CH.Alert(KeyFields.SaveIsOK, this.Literal1);

            }
            BindLV();
        }
        else
        {
            CH.Alert("Please Select Category", this.Literal1);
            if (other_inc_id == 0)
                this.ddl_other_inc.Focus();
            else
                this.CategoryDropDownLoad1.Focus();
        }
    }
}
