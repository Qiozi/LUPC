using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Q_Admin_EditPartDetail : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Sku = ReqSku;
            InitialDatabase();


        }
        this.Title = "Edit Part: " + this.Sku.ToString();
    }

    public string txtAdjustmentEndDateClientID
    {
        get { return txtAdjustmentEndDate.ClientID; }
    }

    public override void InitialDatabase()
    {
        base.InitialDatabase();

        InitPreNextButton();

        this.lbl_sku.Text = this.Sku.ToString();

        SetLabelCategoryName(Sku);
        BindPartSize();
        if (Sku != -1)
        {
            BindPageControl(Sku);

            if (ReqCmd != EditPartCmd.Create)
            {
                BindIncPriceGV(Sku);
                BindIncInfoGV();
            }

            this.ddl_category_1.Enabled = false;
            this.ddl_category_2.Enabled = false;
            this.ddl_category_3.Enabled = false;

            this.btn_save_match_sku.Text = "Save Match(" + Sku.ToString() + ")";
        }
        //else
        //{
        //    this.btn_SetSKU.Enabled = false;
        //}

        CurrentCategoryID = MenuChildSerialNoRequest;

        // 
        // load title
        BindTitle();

        //
        // 
        //
        if (ReqCmd == EditPartCmd.Create)
            if (CurrentCategoryID < 1)
                BindCategory1DDL();

        //BindCategoryTitle(CurrentCategoryID);

        //
        // Page Controls is Show Or Hide
        //
        if (ReqCmd == EditPartCmd.Create)
        {
            this.btn_SetSKU.Enabled = false;
            this.panel_match_sku.Visible = false;
            this.lbl_img_sku.Visible = false;
            this.txt_img_sku.Visible = false;
            this.lbl_img_quantity.Visible = false;
            this.txt_img_quantity.Visible = false;

        }

        if (ReqCmd == EditPartCmd.modifyComment)
        {
            this.panel_comment.Visible = true;
            this.panel_part_info.Visible = false;
        }
        if (ReqCmd == EditPartCmd.ModifyAndNoComment)
        {
            this.panel_comment.Visible = false;
            this.panel_part_info.Visible = true;
        }
        // Response.Write(Cmd.ToString());
    }

    void InitPreNextButton()
    {
        if (ReqSku > 10)
        {
            int preSku = 0;
            int nextSku = 0;

            DataTable skuDt = Config.ExecuteDataTable("Select product_serial_no from tb_product where tag=1 and menu_child_serial_no=(select max(menu_child_serial_no) from tb_product where product_serial_no='" + ReqSku + "')");
            for (int i = 0; i < skuDt.Rows.Count; i++)
            {
                if (ReqSku.ToString() == skuDt.Rows[i][0].ToString())
                {
                    if (i != 0)
                    {
                        preSku = int.Parse(skuDt.Rows[i - 1][0].ToString());

                    }

                    if (i + 1 <= skuDt.Rows.Count - 1)
                    {
                        nextSku = int.Parse(skuDt.Rows[i + 1][0].ToString());
                    }
                }
            }

            litPreNextButton.Text = string.Format("{0}::::{1}"
                , preSku > 0 ? "<a href='editPartDetail.aspx?id=" + preSku.ToString() + "' >Pre Part</a>" : "<a>Pre Part</a>"
                , nextSku > 0 ? "<a href='editPartDetail.aspx?id=" + nextSku.ToString() + "' >Next Part</a>" : "<a>Next Part</a>"
                );
        }
        else
            litPreNextButton.Visible = false;

    }

    /// <summary>
    /// 
    /// </summary>
    public void BindTitle()
    {
        if (CurrentCategoryID > 0 && Sku > 0)
        {
            this.lbl_sku.Text += Sku.ToString();
        }
    }

    #region Bind Methods

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sku"></param>
    /// <param name="desc"></param>
    private void GenerateFile(int sku, string desc, string desc_short)
    {
        FileHelper.GenerateFile(string.Format("{0}{1}_comment.html", Server.MapPath(Config.Part_Comment_Path), sku), desc);
        FileHelper.GenerateFile(string.Format("{0}{1}_comment_short.html", Server.MapPath(Config.Part_Comment_Path), sku), desc_short);
        InsertTraceInfo(string.Format("Create file {0}", string.Format("{0}/{1}_comment.html", Server.MapPath(Config.Part_Comment_Path), sku)));
    }

    /// <summary>
    /// 
    /// </summary>
    private void BindPartSize()
    {
        this.ddl_size.DataSource = Config.ExecuteDataTable(@"select product_size_id, concat(product_size_name, '(low:$',(select round( charge, 2 ) from tb_account a where shipping_company_id=1 and a.product_size_id=p.product_size_id) ,')') c
 from tb_product_size p where product_type=1
");
        this.ddl_size.DataTextField = "c";
        this.ddl_size.DataValueField = "product_size_id";
        this.ddl_size.DataBind();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="lu_sku"></param>
    private void BindPageControl(int lu_sku)
    {
        ProductModel pm = ProductModel.GetProductModel(lu_sku);

        if (ReqCmd == EditPartCmd.modifyComment)
        {

            //ProductDescModel[] pdm = ProductDescModel.FindAllByProperty("part_sku", lu_sku);
            //if (pdm.Length > 0)
            //    this.txt_desc.Text = pdm[0].part_comment;
            string commFullFile = string.Format("{0}{1}_comment.html", Server.MapPath(Config.Part_Comment_Path), lu_sku);
            if (System.IO.File.Exists(commFullFile))
            {
                this.txt_desc.Text = FileHelper.ReadFile(commFullFile); ;
            }
            commFullFile = string.Format("{0}{1}_comment_short.html", Server.MapPath(Config.Part_Comment_Path), lu_sku);
            if (System.IO.File.Exists(commFullFile))
            {
                this.txt_desc_short.Text = FileHelper.ReadFile(commFullFile); ;
            }

            this.txt_img_sku.Text = pm.other_product_sku.ToString();
            this.txt_img_quantity.Text = pm.product_img_sum.ToString();
        }

        if (ReqCmd == EditPartCmd.ModifyAndNoComment)
        {
            this.txt_keywords.Text = pm.keywords;
            this.txt_long_name.Text = pm.product_name_long_en;
            this.txt_manufactuere_part_number.Text = pm.manufacturer_part_number;
            this.txt_middle_name.Text = pm.product_name;
            this.txt_product_ebay_name.Text = pm.product_ebay_name;
            this.txt_other_sku.Text = pm.other_product_sku.ToString();
            this.txt_product_current_cost.Text = pm.product_current_cost.ToString();
            this.txt_product_current_special_cash_price.Text = (pm.product_current_special_cash_price + pm.product_current_discount).ToString();
            this.txt_product_current_price.Text = pm.product_current_price.ToString();
            this.txt_product_img_sum.Text = pm.product_img_sum.ToString();
            this.txt_product_store_sum.Text = pm.product_store_sum.ToString();
            this.txt_producter.Text = pm.producter_serial_no;
            this.txt_producter_url.Text = pm.producter_url;
            this.txt_short_name.Text = pm.product_short_name;
            this.txt_supplier_sku.Text = pm.supplier_sku;
            this.txt_priority.Text = pm.product_order.ToString();
            this.txt_model.Text = pm.model;
            this.cb_export.Checked = pm.export;
            this.cb_hot.Checked = pm.hot == 1;
            this.cb_new.Checked = pm.new_product == 1;
            this.cb_non.Checked = pm.is_non == 1;
            this.cb_split_line.Checked = pm.split_line == 1;
            this.cb_showit.Checked = pm.tag == 1;
            this.cb_fixed.Checked = pm.is_fixed == 1;
            this.cb_for_sys.Checked = pm.for_sys == 1;
            this.ddl_size.SelectedValue = pm.product_size_id.ToString();
            this.txt_adjustment.Text = pm.adjustment.ToString();
            this.txt_part_ebay_describe.Text = pm.part_ebay_describe;
            this.txt_price_sku.Text = pm.price_sku.ToString();
            this.txt_price_sku_quantity.Text = pm.price_sku_quantity.ToString();
            this.txt_screen_size.Text = pm.screen_size.ToString();
            this.txt_UPC.Text = pm.UPC;
            this.txt_product_ebay_name_2.Text = pm.product_ebay_name_2;
            this.txt_weight.Text = pm.weight.ToString();
            this.txtAdjustmentEndDate.Text = pm.adjustment_enddate.Year < 2000 ? "" : pm.adjustment_enddate.ToString("yyyy-MM-dd");

        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="lu_sku"></param>
    private void BindIncPriceGV(int lu_sku)
    {
        LtdHelper LH = new LtdHelper();
        DataTable dt = Config.ExecuteDataTable(string.Format(@"select other_inc_price price, other_inc_id, '' inc_name, last_regdate,other_inc_store_sum qty from tb_other_inc_part_info where luc_sku='{0}' ", lu_sku));
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow dr = dt.Rows[i];
            int other_inc_id;
            int.TryParse(dr["other_inc_id"].ToString(), out other_inc_id);
            if (other_inc_id > 0)
                dr["inc_name"] = LH.FilterText(LH.LtdModelByValue(other_inc_id).ToString());
        }

        this.GridView1.DataSource = dt;
        this.GridView1.DataBind();
    }

    /// <summary>
    /// 
    /// </summary>
    private void BindIncInfoGV()
    {
        LtdHelper LH = new LtdHelper();

        DataTable dt = LH.LtdHelperToDataTableNoLU();
        dt.Columns.Add("other_inc_sku");
        DataTable otherSKUDT = Config.ExecuteDataTable("select other_inc_sku, other_inc_type from tb_other_inc_match_lu_sku where lu_sku='" + Sku.ToString() + "'");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow dr = dt.Rows[i];
            for (int j = 0; j < otherSKUDT.Rows.Count; j++)
            {
                if (otherSKUDT.Rows[j]["other_inc_type"].ToString() == dr["id"].ToString())
                {
                    dr["other_inc_sku"] = otherSKUDT.Rows[j]["other_inc_sku"].ToString();
                }
            }
        }
        this.gv_inc_info.DataSource = dt;
        this.gv_inc_info.DataBind();
    }

    /// <summary>
    /// 
    /// </summary>
    private void SetControlsNull()
    {
        if (ReqCmd != EditPartCmd.modifyComment)
        {
            this.txt_keywords.Text = "";
            this.txt_long_name.Text = "";
            this.txt_manufactuere_part_number.Text = "";
            this.txt_middle_name.Text = "";
            this.txt_product_ebay_name.Text = "";
            this.txt_other_sku.Text = "";
            this.txt_product_current_cost.Text = "";
            this.txt_product_current_special_cash_price.Text = "";
            this.txt_product_current_price.Text = "";
            this.txt_product_img_sum.Text = "";
            this.txt_product_store_sum.Text = "";
            this.txt_producter.Text = "";
            this.txt_producter_url.Text = "";
            this.txt_short_name.Text = "";
            this.txt_supplier_sku.Text = "";
            this.txt_priority.Text = "";
            this.txt_price_sku_quantity.Text = "";
            this.txt_price_sku.Text = "";

            this.cb_export.Checked = true;
            this.cb_hot.Checked = false;
            this.cb_new.Checked = true;
            this.cb_non.Checked = false;
            this.cb_fixed.Checked = false;
            this.cb_for_sys.Checked = false;
            this.cb_split_line.Checked = false;
            this.cb_showit.Checked = true;
            this.lbl_discount.Text = "";
            this.txt_adjustment.Text = "";
            this.txt_part_ebay_describe.Text = "";
            this.txt_UPC.Text = "";
            this.txt_product_ebay_name_2.Text = "";
            this.txt_weight.Text = "";
            this.txtAdjustmentEndDate.Text = "";
        }
        if (ReqCmd != EditPartCmd.ModifyAndNoComment)
        {
            this.txt_desc.Text = "";
            this.txt_desc_short.Text = "";
            this.txt_img_sku.Text = "";
            this.txt_img_quantity.Text = "";
        }
    }
    #endregion

    #region properties
    public int ReqSku
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "id", -1); }
    }

    public int Sku
    {
        get { return (int)ViewState["Sku"]; }
        set { ViewState["Sku"] = value; }
    }


    public int MenuChildSerialNoRequest
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "category_id", -1); }

    }
    public int CurrentCategoryID
    {
        get { return (int)ViewState["CurrentCategoryID"]; }
        set { ViewState["CurrentCategoryID"] = value; }
    }
    #endregion

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_save_match_sku_Click(object sender, EventArgs e)
    {
        if (Sku > 0)
        {
            Config.ExecuteNonQuery("delete from tb_other_inc_match_lu_sku where lu_sku='" + Sku.ToString() + "'");
            for (int i = 0; i < this.gv_inc_info.Items.Count; i++)
            {
                HiddenField _hf_other_inc_id = (HiddenField)this.gv_inc_info.Items[i].FindControl("_hf_other_inc_id");
                TextBox _txt_other_inc = (TextBox)this.gv_inc_info.Items[i].FindControl("_txt_other_inc_sku");
                int other_id;
                int.TryParse(_hf_other_inc_id.Value, out other_id);
                string other_inc_sku = _txt_other_inc.Text.Trim();
                if (other_inc_sku.Length > 0)
                {
                    OtherIncMatchLuSkuModel oims = new OtherIncMatchLuSkuModel();
                    oims.lu_sku = Sku;
                    oims.other_inc_sku = other_inc_sku;
                    oims.other_inc_type = other_id;
                    oims.Create();
                }
                if (Config.ExecuteScalarInt32("Select count(id) from tb_other_inc_part_info where other_inc_id='" + other_id.ToString() + "' and other_inc_sku='" + other_inc_sku + "'") > 0)
                {
                    Config.ExecuteNonQuery("Update tb_other_inc_part_info set luc_sku='" + Sku.ToString() + "' where  other_inc_id='" + other_id.ToString() + "' and other_inc_sku='" + other_inc_sku + "'");
                }
            }
            CH.Alert(KeyFields.SaveIsOK, this.txt_desc_short);
            BindIncPriceGV(Sku);
        }
        else
        {
            CH.Alert("只有修改状态才可以保存匹配数据", this.txt_desc_short);
            return;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_save_Click(object sender, EventArgs e)
    {
        try
        {
            // validate category 
            if (Sku < 1 && CurrentCategoryID < 1)
            {
                CH.Alert("请选择正确的目录", this.txt_desc_short);
                return;
            }
            string result = string.Empty;

            ProductModel pm = new ProductModel();
            if (Sku != -1)
                pm = ProductModel.GetProductModel(Sku);

            if (ReqCmd != EditPartCmd.modifyComment)
            {
                pm.product_size_id = int.Parse(this.ddl_size.SelectedValue.ToString());
                pm.export = this.cb_export.Checked;
                pm.hot = byte.Parse(this.cb_hot.Checked ? "1" : "0");
                pm.is_non = byte.Parse(this.cb_non.Checked ? "1" : "0");
                pm.keywords = this.txt_keywords.Text.Trim();
                pm.last_regdate = DateTime.Now;
                pm.manufacturer_part_number = this.txt_manufactuere_part_number.Text.Trim();
                pm.new_product = byte.Parse(this.cb_new.Checked ? "1" : "0");
                pm.model = this.txt_model.Text.Trim();
                pm.part_ebay_describe = this.txt_part_ebay_describe.Text;
                decimal screenSize = 0M;
                decimal.TryParse(this.txt_screen_size.Text, out screenSize);
                pm.screen_size = screenSize;
                int other_sku;
                int.TryParse(this.txt_other_sku.Text.Trim(), out other_sku);
                pm.other_product_sku = other_sku;

                decimal cost;
                decimal.TryParse(this.txt_product_current_cost.Text, out cost);
                pm.product_current_cost = cost;

                int price_sku;
                int.TryParse(this.txt_price_sku.Text, out price_sku);
                pm.price_sku = price_sku;

                int price_sku_quantity;
                int.TryParse(this.txt_price_sku_quantity.Text, out price_sku_quantity);
                pm.price_sku_quantity = price_sku_quantity;

                //decimal special_cash_price;
                //decimal.TryParse(this.txt_product_current_special_cash_price.Text, out special_cash_price);
                //if (special_cash_price == 0M)
                //{
                //    pm.product_current_special_cash_price = special_cash_price;
                //    pm.product_current_price = 0M;
                //}
                //else
                //{
                //    pm.product_current_price = ConvertPrice.SpecialCashPriceConvertToCardPrice(special_cash_price);
                //    pm.product_current_special_cash_price = ConvertPrice.ChangePriceToNotCard(pm.product_current_price);
                //    this.txt_product_current_special_cash_price.Text = pm.product_current_special_cash_price.ToString();
                //}

                decimal adjustment;
                decimal.TryParse(this.txt_adjustment.Text, out adjustment);
                pm.adjustment = adjustment;



                byte img_sum;
                byte.TryParse(this.txt_product_img_sum.Text, out img_sum);
                pm.product_img_sum = img_sum;

                pm.product_name = this.txt_middle_name.Text.Trim();
                pm.product_name_long_en = this.txt_long_name.Text.Trim();

                pm.product_ebay_name = this.txt_product_ebay_name.Text.TrimStart();
                pm.product_ebay_name_2 = this.txt_product_ebay_name_2.Text.Trim();
                int priority;
                int.TryParse(this.txt_priority.Text, out priority);
                pm.product_order = priority;

                pm.product_short_name = this.txt_short_name.Text.Trim();

                int store_sum;
                int.TryParse(this.txt_product_store_sum.Text, out store_sum);
                pm.product_store_sum = store_sum;

                pm.producter_serial_no = this.txt_producter.Text.Trim();
                pm.producter_url = this.txt_producter_url.Text.Trim();
                pm.last_regdate = DateTime.Now;
                pm.split_line = byte.Parse(this.cb_split_line.Checked ? "1" : "0");
                pm.supplier_sku = this.txt_supplier_sku.Text.Trim();
                pm.tag = byte.Parse(this.cb_showit.Checked ? "1" : "0");
                pm.UPC = this.txt_UPC.Text.Trim();
                pm.for_sys = this.cb_for_sys.Checked ? 1 : 0;
                pm.is_fixed = this.cb_fixed.Checked ? 1 : 0;

                decimal weight;
                decimal.TryParse(this.txt_weight.Text.Trim(), out weight);
                pm.weight = weight;

                if (pm.product_name == "" && pm.product_short_name == "")
                {
                    CH.Alert("请输入产品信息", this.txt_desc_short);
                    return;
                }

                if (Sku != -1 && CurrentCategoryID == -1)
                {
                    pm.product_serial_no = Sku;
                    pm.Update();
                }
                else
                {
                    if (CurrentCategoryID > 0)
                    {
                        if (Config.ExecuteScalarInt32("Select count(product_serial_no) from tb_product where manufacturer_part_number='" + pm.manufacturer_part_number + "'") > 0)
                        {
                            throw new Exception("Product is exist.");
                        }

                        pm.prodType = "New";
                        pm.regdate = DateTime.Now;
                        pm.menu_child_serial_no = CurrentCategoryID;
                        pm.Create();
                        Sku = pm.product_serial_no;
                        InsertTraceInfo("Create Part Info...:" + pm.product_serial_no.ToString());
                    }
                    else
                    {
                        throw new Exception("参数错误.");
                    }
                }

                //
                // 按公式更新价格, 需要先更新cost.
                //
                DateTime adjustEndDate;
                if (txtAdjustmentEndDate.Text.Trim() == "")
                    adjustEndDate = DateTime.MinValue;
                else
                    DateTime.TryParse(this.txtAdjustmentEndDate.Text.Trim(), out adjustEndDate);

                new PriceHelper().SaveAdjust(pm.product_serial_no, adjustment, adjustEndDate);
                new PriceHelper().ModifyRelevancePrice(pm.product_serial_no, pm.product_current_cost, adjustment, adjustEndDate);

                pm = ProductModel.GetProductModel(pm.product_serial_no);
                this.txt_product_current_price.Text = pm.product_current_price.ToString();
                this.txt_product_current_special_cash_price.Text = pm.product_current_special_cash_price.ToString();
                this.lbl_discount.Text = pm.product_current_discount.ToString();
                result += " Part Save Success (" + Sku.ToString() + ").<br>";
            }

            //
            // modify Comment
            //
            if (ReqCmd != EditPartCmd.ModifyAndNoComment)
            {
                string comment = this.txt_desc.Text;
                string comment_short = this.txt_desc_short.Text;
                ProductDescModel pdm = new ProductDescModel();
                pdm.SavePartComment(pm.product_serial_no, comment, comment_short);
                GenerateFile(pm.product_serial_no, comment, comment_short);
                if (ReqCmd == EditPartCmd.modifyComment)
                {
                    //
                    // Other Part SKU
                    int img_sku;
                    int.TryParse(this.txt_img_sku.Text, out img_sku);
                    //
                    // img quantity
                    //
                    byte img_quantity;
                    byte.TryParse(this.txt_img_quantity.Text, out img_quantity);

                    pm.product_img_sum = img_quantity;
                    pm.other_product_sku = img_sku;
                    pm.Update();
                }
                result += " Part Save Success (" + Sku.ToString() + ").<br>";
                InsertTraceInfo("Modify Part Info...:" + pm.product_serial_no.ToString());
            }

            if (ReqCmd == EditPartCmd.Create)
            {
                SetControlsNull();
                Sku = -1;
                BindTitle();
            }
            CH.CloseParentWatting(this.btn_save);
            CH.Alert(result, this.btn_save);

        }
        catch (Exception ex)
        {
            CH.CloseParentWatting(this.btn_save);
            CH.Alert(ex.Message, this.btn_save);
        }
    }

    #region Calendar
    //protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    //{
    //    Calendar c = (Calendar)sender;
    //    this.txt_on_sale_begin.Text = c.SelectedDate.ToString("yyyy-MM-dd");
    //}
    //protected void Calendar2_DayRender(object sender, DayRenderEventArgs e)
    //{
    //    if (e.Day.IsToday)
    //    {
    //        e.Cell.ForeColor = System.Drawing.Color.Blue;
    //        e.Cell.BackColor = System.Drawing.Color.Pink;
    //    }
    //}

    //protected void Calendar2_SelectionChanged(object sender, EventArgs e)
    //{
    //    Calendar c = (Calendar)sender;
    //    this.txt_on_sale_end.Text = c.SelectedDate.ToString("yyyy-MM-dd");
    //}
    //protected void Calendar3_SelectionChanged(object sender, EventArgs e)
    //{
    //    Calendar c = (Calendar)sender;
    //    this.txt_rebate_begin.Text = c.SelectedDate.ToString("yyyy-MM-dd");
    //}
    //protected void Calendar4_SelectionChanged(object sender, EventArgs e)
    //{
    //    Calendar c = (Calendar)sender;
    //    this.txt_rebate_end.Text = c.SelectedDate.ToString("yyyy-MM-dd");
    //}
    #endregion

    #region category


    private void BindCategory1DDL()
    {
        DataTable dt = Config.ExecuteDataTable(@"select menu_child_serial_no, menu_child_name from (select 0 menu_child_serial_no, 'SELECT' menu_child_name , 0 menu_child_order
union all 
select menu_child_serial_no, menu_child_name ,menu_child_order from tb_product_category where tag=1 and menu_pre_serial_no=0 
and is_virtual=0  and menu_parent_serial_no=1) t order by menu_child_order asc ");
        this.ddl_category_1.DataSource = dt;
        this.ddl_category_1.DataTextField = "menu_child_name";
        this.ddl_category_1.DataValueField = "menu_child_serial_no";
        this.ddl_category_1.DataBind();
    }

    private void BindCategorySubDDL(string parent_id, DropDownList ddl)
    {
        DataTable dt;

        dt = Config.ExecuteDataTable(string.Format(@"select menu_child_serial_no, menu_child_name from (select 0 menu_child_serial_no, 'SELECT' menu_child_name , -10 menu_child_order
union all 
select menu_child_serial_no, menu_child_name ,menu_child_order from tb_product_category where tag=1 and menu_pre_serial_no='{0}' 
and is_virtual=0 and page_category=1 ) t order by menu_child_order asc ", parent_id));
        if (dt.Rows.Count == 1 || parent_id == "0")
            ddl.DataSource = null;
        else
            ddl.DataSource = dt;
        ddl.DataValueField = "menu_child_serial_no";
        ddl.DataTextField = "menu_child_name";
        ddl.DataBind();
    }

    protected void ddl_category_1_SelectedIndexChanged(object sender, EventArgs e)
    {
        CurrentCategoryID = -1;
        BindCategorySubDDL(this.ddl_category_1.SelectedValue.ToString(), this.ddl_category_2);
        this.ddl_category_3.Items.Clear();
        //BindCategoryTitle(CurrentCategoryID);
    }
    protected void ddl_category_2_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.ddl_category_3.Items.Clear();
        CurrentCategoryID = -1;
        if (Config.ExecuteScalarInt32("select menu_is_exist_sub from tb_product_category where menu_child_serial_no='" + this.ddl_category_2.SelectedValue.ToString() + "'") != 0)
        {
            BindCategorySubDDL(this.ddl_category_2.SelectedValue.ToString(), this.ddl_category_3);

            if (this.ddl_category_3.Items.Count == 0)
            {
                int category_id;
                int.TryParse(this.ddl_category_2.SelectedValue.ToString(), out category_id);

                if (category_id > 0)
                    CurrentCategoryID = category_id;
                //CH.Alert(this.ddl_category_3.Items.Count.ToString(), this.txt_desc);

            }
            //BindCategoryTitle(CurrentCategoryID);
        }
        else
        {
            CurrentCategoryID = int.Parse(this.ddl_category_2.SelectedValue.ToString());
        }


    }
    protected void ddl_category_3_SelectedIndexChanged(object sender, EventArgs e)
    {
        int category_id;
        int.TryParse(this.ddl_category_3.SelectedValue.ToString(), out category_id);

        if (category_id > 0)
            CurrentCategoryID = category_id;
        //BindCategoryTitle(CurrentCategoryID);
    }

    //private void BindCategoryTitle(int categoryID)
    //{
    //    DataTable dt = Config.ExecuteDataTable("select menu_child_name from tb_product_category where menu_child_serial_no = '" + categoryID.ToString() + "'");
    //    if (dt.Rows.Count == 0)
    //        this.lbl_current_category_title.Text = "None";
    //    else
    //        this.lbl_current_category_title.Text = dt.Rows[0][0].ToString();
    //}
    #endregion

    protected void btn_SetSKU_Click(object sender, EventArgs e)
    {
        int new_Sku;
        int.TryParse(this.txt_SetSKU.Text, out new_Sku);
        if (new_Sku == 0)
        {
            var dt = Config.ExecuteDataTable("Select product_serial_no from tb_product where manufacturer_part_number='" + this.txt_SetSKU.Text.Trim() + "'");
            if (1 == dt.Rows.Count)
            {
                Sku = int.Parse(dt.Rows[0][0].ToString());
                InitialDatabase();
            }
            else if (dt.Rows.Count > 1)
            {
                CH.Alert("have more product", this.Literal1);
            }
            else
            {
                CH.Alert("Invalid SKU, MFP#", this.Literal1);
            }

        }
        else
        {
            if (1 == Config.ExecuteScalarInt32("Select count(product_serial_no) from tb_product where product_serial_no='" + new_Sku.ToString() + "'"))
            {
                Sku = new_Sku;

                InitialDatabase();
            }
            else
            {
                CH.Alert("Invalid SKU, MFP#", this.Literal1);
            }
        }
    }

    private void SetLabelCategoryName(int sku)
    {
        DataTable dt = Config.ExecuteDataTable(@"select pc.menu_child_name from  tb_product p inner join tb_product_category pc on p.menu_child_serial_no=pc.menu_child_serial_no
where p.product_serial_no='" + sku.ToString() + "'");
        if (dt.Rows.Count > 0)
        {
            this.lbl_current_category_title.Text = dt.Rows[0][0].ToString();
        }
    }

    #region Properties Page Cmd
    public EditPartCmd ReqCmd
    {
        get
        {
            int cmd = Util.GetInt32SafeFromQueryString(Page, "cmd", -1);
            if (Util.GetStringSafeFromQueryString(Page, "cmd") == "menu")
                return EditPartCmd.Create;
            if (cmd == 0)
                return EditPartCmd.Create;
            if (cmd == 1)
                return EditPartCmd.modifyComment;
            if (cmd == 2)
                return EditPartCmd.ModifyAndNoComment;
            return EditPartCmd.ModifyAndNoComment;
        }
    }
    #endregion
}

public enum EditPartCmd
{
    Create = 0,
    modifyComment = 1, 
    ModifyAndNoComment = 2
}