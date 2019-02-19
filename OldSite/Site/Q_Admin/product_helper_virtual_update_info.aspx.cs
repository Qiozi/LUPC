using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Q_Admin_product_helper_virtual_update_info : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.MenuChildName1.menu_child_serial_no = CategoryID;
            BindGV();
            CH.CloseParentWatting(this.ListView1);
        }
    }


    #region Bind
    public void BindGV()
    {
        this.ListView1.DataSource = Config.ExecuteDataTable(@"Select p.product_serial_no,p.split_line, p.manufacturer_part_number,
p.menu_child_serial_no,(p.product_current_price-p.product_current_discount ) product_current_sold, product_current_cost, product_current_price
, case when p.product_name='' then p.product_short_name else p.product_name end as product_name, case when other_product_sku = 0 then product_serial_no else other_product_sku end as img_sku, pv.priority from tb_product p inner join 
tb_product_virtual pv on p.product_serial_no=pv.lu_sku and pv.showit=1 and pv.menu_child_serial_no='" + CategoryID + "' order by pv.priority asc "); 
        this.ListView1.DataBind();
    }
    #endregion

    #region preporties
    public int CategoryID
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "categoryid", -1); }
    }

    DataTable _virtual_info = null;
    public DataTable VirtualInfo
    {
        get
        {
            if (_virtual_info == null)
            {
                _virtual_info = Config.ExecuteDataTable(@"Select p.product_serial_no,p.split_line, p.manufacturer_part_number,
p.menu_child_serial_no,(p.product_current_price-p.product_current_cost ) product_current_sold, product_current_cost, product_current_price
, case when p.product_name='' then p.product_short_name else p.product_name end as product_name, case when other_product_sku = 0 then product_serial_no else other_product_sku end as img_sku, pv.priority from tb_product p inner join 
tb_product_virtual pv on p.product_serial_no=pv.lu_sku and pv.showit=1 and pv.menu_child_serial_no='" + CategoryID + "' order by pv.priority asc ");
            }
            return _virtual_info;
        }
    }

   
    #endregion

    #region Change Virtual DB
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sku"> lu sku</param>
    /// <returns>priority</returns>
    private int GetPriorityFromVirtualDB(int sku)
    {
        for (int i = 0; i < VirtualInfo.Rows.Count; i++)
        {
            if (sku == int.Parse(VirtualInfo.Rows[i]["product_serial_no"].ToString()))
            {
                int priority;
                int.TryParse(VirtualInfo.Rows[i]["priority"].ToString(), out priority);
                return priority;
            }
        }
        return 0;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="lu_sku1"></param>
    /// <param name="lu_sku2"></param>
    private void ChangeVirtualDB(int lu_sku1, int lu_sku2)
    {

        Config.ExecuteNonQuery(string.Format("update tb_temp_exchange set exchange_value=(select max(priority) from tb_product_virtual where lu_sku='{0}')", lu_sku1));
        Config.ExecuteNonQuery(string.Format("update tb_product_virtual p, tb_product_virtual pp set p.priority=pp.priority where p.lu_sku='{0}' and pp.lu_sku='{1}';", lu_sku1, lu_sku2));
        Config.ExecuteNonQuery(string.Format("update tb_product_virtual set priority=(select max(exchange_value) from tb_temp_exchange) where lu_sku='{0}';", lu_sku2));
    }
    #endregion

    protected void btn_downLoad_Click(object sender, EventArgs e)
    {
        try
        {
            LtdHelper lh = new LtdHelper();
            DataTable dt = Config.ExecuteDataTable(@"Select LU_SKU, Priority, showit from tb_product_virtual where menu_child_serial_no='" + CategoryID.ToString() + "' order by priority asc");
            
            ExcelHelper eh = new ExcelHelper(dt);
            eh.MaxRecords = 20000;
            eh.FileName = "table.xls";
            eh.Export();

        }
        catch (Exception ex)
        {
            CH.Alert(ex.Message, this.btn_downLoad);
        }
    }

    protected void btn_upload_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.file_upload.PostedFile != null)
            {
                string newFilename = Server.MapPath(string.Format("{0}{1}",
                  Config.update_product_data_excel_path,
                  string.Format("virtual_{0}.xls", DateTime.Now.ToString("yyyyMMddhhmmss"))));
                this.file_upload.PostedFile.SaveAs(newFilename);
                InsertTraceInfo(DBContext, string.Format(" Upload virtual menuChild:{0}", CategoryID));
                using (OleDbConnection conn = new OleDbConnection(Config.ExcelConnstring(newFilename)))
                {
                    conn.Open();
                    //[LU_SKU],[Priority],[showit]
                    OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM [table$]", conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    conn.Close();

                    Config.ExecuteNonQuery("Delete from tb_product_virtual where menu_child_serial_no='" + CategoryID.ToString() + "'");
                    InsertTraceInfo(DBContext, string.Format(" Delete virtual menuChild: {0}", CategoryID));

                    int error_sum = 0; int success_sum = 0;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];
                        try
                        {
                            int lu_sku;
                            int.TryParse(dr["lu_sku"].ToString(), out lu_sku);

                            int showit;
                            int.TryParse(dr["showit"].ToString(), out showit);

                            if (lu_sku > 0)
                            {
                                Config.ExecuteNonQuery(string.Format(@"insert into tb_product_virtual (lu_sku, menu_child_serial_no, priority, showit)
                                                                values ({0}, {1}, {2},{3})", dr["lu_sku"].ToString(), CategoryID, dr["priority"].ToString(), showit));
                                success_sum += 1;
                            }
                        }
                        catch { error_sum += 1; }
                    }
                    CH.Alert(success_sum + " Upload Success " + (error_sum != 0 ? " <br> " + error_sum + " upload error" : ""), this.file_upload);
                }
                CH.CloseParentWatting(this.file_upload);
                CH.Alert(KeyFields.SaveIsOK, this.file_upload);
            }
        }
        catch (Exception ex)
        {
            CH.CloseParentWatting(this.file_upload);
            CH.Alert(ex.Message, this.file_upload);
        }
    }

    protected void ListView1_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        try
        {
            int current_index = 0;
            int current_sku;
            int.TryParse(((Label)e.Item.FindControl("_lbl_lu_sku")).Text, out current_sku);

            for (int i = 0; i < this.ListView1.Items.Count; i++)
            {
                int _current_sku;
                int.TryParse(((Label)this.ListView1.Items[i].FindControl("_lbl_lu_sku")).Text, out _current_sku);
                if (current_sku == _current_sku)
                    current_index = i;
            }

            //int current_priority = GetPriorityFromVirtualDB(current_sku);

            switch (e.CommandName)
            {
                case "Up":
                    if (current_index == 0)
                    {
                        CH.Alert("this is first, don't UP", this.btn_downLoad);
                        return;
                    }
                    int sku2;
                    int.TryParse(((Label)this.ListView1.Items[current_index - 1].FindControl("_lbl_lu_sku")).Text, out sku2);
                    if (sku2 == 0)
                        int.TryParse(((Label)this.ListView1.Items[current_index - 1].FindControl("_lbl_lu_sku_title")).Text, out sku2);
                    //CH.Alert(string.Format("{0}|{1}", current_sku, sku2), this.ListView1);
                    ChangeVirtualDB(current_sku, sku2);
                    CH.CloseParentWatting(this.ListView1);
                    CH.Alert(string.Format("<span style='color:green'>{0}|{1}</span> priority is change.", current_sku, sku2), this.ListView1);
                    break;

                case "DeletePart":
                    Config.ExecuteNonQuery(string.Format(" delete from tb_product_virtual where menu_child_serial_no='{0}' and lu_sku='{1}'",
                         CategoryID, current_sku));
                    CH.CloseParentWatting(this.ListView1);
                    CH.Alert(string.Format("<span style='color:green'>{0}</span> is deleted.", current_sku), this.ListView1);
                    
                    break;
            } this.BindGV();
        }
        catch 
        {
            //CH.CloseParentWatting(this.ListView1);
            //CH.Alert(ex.Message, this.ListView1);
        }

    }
    protected void ListView1_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        // split_line
        int split_line;
        int.TryParse(((HiddenField)e.Item.FindControl("_hiddenField_split_line")).Value, out split_line);
        //this.btn_search.Text += split_line.ToString();

        int lu_sku;
        int.TryParse(((Label)e.Item.FindControl("_lbl_lu_sku")).Text, out lu_sku);

        string manufacture_code = ((Label)e.Item.FindControl("_lbl_lu_manufacture")).Text;

        

        if (split_line == 1)
        {
            ((Panel)e.Item.FindControl("_panel_part_title")).Visible = true;
            ((Panel)e.Item.FindControl("_panel_part_commont")).Visible = false;
            return;
        }
        else
        {
            ((Panel)e.Item.FindControl("_panel_part_commont")).Visible = true;
            ((Panel)e.Item.FindControl("_panel_part_title")).Visible = false;
        }

        // price,cost
        TextBox _txt_cost = (TextBox)e.Item.FindControl("_txt_lu_cost");
        TextBox _txt_price = (TextBox)e.Item.FindControl("_txt_lu_price");

       
    }
    protected void btn_initial_priority_Click(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i < this.ListView1.Items.Count; i++)
            {
                int lu_sku;
                int.TryParse(((Label)this.ListView1.Items[i].FindControl("_lbl_lu_sku")).Text, out lu_sku);
                if (lu_sku == 0)
                    int.TryParse(((Label)this.ListView1.Items[i].FindControl("_lbl_lu_sku_title")).Text, out lu_sku);
                Config.ExecuteNonQuery(string.Format("Update tb_product_virtual set priority='{0}' where menu_child_serial_no='{1}' and lu_sku='{2}'",
                    i + 1000, CategoryID, lu_sku));
            }
            
            BindGV();
            CH.CloseParentWatting(this.ListView1);
            CH.Alert(KeyFields.SaveIsOK, this.ListView1);
        }
        catch (Exception ex)
        {
            CH.CloseParentWatting(this.ListView1);
            CH.Alert(ex.Message, this.ListView1);
        }
    }
}
