using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Q_Admin_inventory_purchase : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.ValidateLoginRule(Role.Purchase);
            InitialDatabase();
        }
    }

    #region methods
    public override void InitialDatabase()
    {
        base.InitialDatabase();
        BindVendorDDL();
        BindPurchaseDG(false);
        //this.lbl_product_list_clientID.Text = this.txtpurchase_product_list.ClientID;
    }

    public void BindVendorDDL()
    {
        this.ddl_vendor_serial_no.DataSource = VendorModel.FindAll();
        this.ddl_vendor_serial_no.DataTextField = "company_name";
        this.ddl_vendor_serial_no.DataValueField = "vendor_serial_no";
        this.ddl_vendor_serial_no .DataBind();
        AnthemHelper.SetDropDownListCheckItem(this.ddl_vendor_serial_no);
    }

    private void BindPurchaseDG(bool autoUpdate)
    {
        this.dg_purchase.DataSource = PurchaseModel.GetPurchaseModels();
        this.dg_purchase.DataBind();
        this.dg_purchase.UpdateAfterCallBack = autoUpdate;
    }

    private void SavePurchase()
    {
        string purchase_invoice = AnthemHelper.GetAnthemTextBox(this.txtpurchase_invoice);
        string purchase_net_amount = AnthemHelper.GetAnthemTextBox(this.txtpurchase_net_amount);
        string purchase_gst = AnthemHelper.GetAnthemTextBox(this.txtpurchase_gst);
        string purchase_pst = AnthemHelper.GetAnthemTextBox(this.txtpurchase_pst);
        string purchase_paid_amount = AnthemHelper.GetAnthemTextBox(this.txtpurchase_paid_amount);
        string purchase_check_no = AnthemHelper.GetAnthemTextBox(this.txtpurchase_check_no);
        string purchase_bank = AnthemHelper.GetAnthemTextBox(this.txtpurchase_bank);
        string purchase_date = AnthemHelper.GetAnthemTextBox(this.txtpurchase_date);
        string purchase_note = AnthemHelper.GetAnthemTextBox(this.txtpurchase_note);
        int vendor_serial_no = AnthemHelper.GetDropDownList(this.ddl_vendor_serial_no);
        string staff_serial_no = AnthemHelper.GetAnthemTextBox(this.txtstaff_serial_no);
        string purchase_product_list = AnthemHelper.GetAnthemTextBox(this.txtpurchase_product_list);



        PurchaseModel model = Cmd == Command.modif ? PurchaseModel.GetPurchaseModel(PurchaseID) : new PurchaseModel();

        if (purchase_date == "")
        {
            AnthemHelper.Alert("日期不能为空");
            return;
        }

        if (purchase_product_list == "")
        {
            AnthemHelper.Alert("选择产品");
            return;
        }


        model.purchase_invoice = purchase_invoice;
        model.purchase_net_amount = purchase_net_amount;
        model.purchase_gst = purchase_gst;
        model.purchase_pst = purchase_pst;
        model.purchase_paid_amount = purchase_paid_amount;
        model.purchase_check_no = purchase_check_no;
        model.purchase_bank = purchase_bank;
        model.purchase_date = DateTime.Parse(purchase_date);
        model.purchase_note = purchase_note;
        model.vendor_serial_no = vendor_serial_no;
        model.staff_serial_no = staff_serial_no;

        model.purchase_product_list = purchase_product_list;
      
        if (Cmd == Command.modif)
        {
            model.Update();
            AnthemHelper.SetAnthemButton(this.btn_save, "add");

        }
        else
        {
            model.Create();
        }
        AnthemHelper.Alert(KeyFields.SaveIsOK);
       // SetControlsNULL();
    }

    private void SetControlsValue(int purchase_serial_no)
    {
        PurchaseModel model = PurchaseModel.GetPurchaseModel(purchase_serial_no);

        PurchaseID = model.purchase_serial_no;
        AnthemHelper.SetAnthenTextBox(this.txtpurchase_invoice, model.purchase_invoice);
        AnthemHelper.SetAnthenTextBox(this.txtpurchase_net_amount, model.purchase_net_amount);
        AnthemHelper.SetAnthenTextBox(this.txtpurchase_gst, model.purchase_gst);
        AnthemHelper.SetAnthenTextBox(this.txtpurchase_pst, model.purchase_pst);
        AnthemHelper.SetAnthenTextBox(this.txtpurchase_paid_amount, model.purchase_paid_amount);
        AnthemHelper.SetAnthenTextBox(this.txtpurchase_check_no, model.purchase_check_no);
        AnthemHelper.SetAnthenTextBox(this.txtpurchase_bank, model.purchase_bank);
        AnthemHelper.SetAnthenTextBox(this.txtpurchase_date, model.purchase_date.ToString());
        AnthemHelper.SetAnthenTextBox(this.txtpurchase_note, model.purchase_note);
        AnthemHelper.SetDropDownListValue(this.ddl_vendor_serial_no, model.vendor_serial_no.ToString());
        AnthemHelper.SetAnthenTextBox(this.txtstaff_serial_no, model.staff_serial_no);
        AnthemHelper.SetAnthenTextBox(this.txtpurchase_product_list, model.purchase_product_list);
    }

    private void SetControlsNULL()
    {
        AnthemHelper.SetAnthenTextBoxNULL(this.txtpurchase_invoice);
        AnthemHelper.SetAnthenTextBoxNULL(this.txtpurchase_net_amount);
        AnthemHelper.SetAnthenTextBoxNULL(this.txtpurchase_gst);
        AnthemHelper.SetAnthenTextBoxNULL(this.txtpurchase_pst);
        AnthemHelper.SetAnthenTextBoxNULL(this.txtpurchase_paid_amount);
        AnthemHelper.SetAnthenTextBoxNULL(this.txtpurchase_check_no);
        AnthemHelper.SetAnthenTextBoxNULL(this.txtpurchase_bank);
        AnthemHelper.SetAnthenTextBoxNULL(this.txtpurchase_date);
        AnthemHelper.SetAnthenTextBoxNULL(this.txtpurchase_note);
        AnthemHelper.SetDropDownListValue(this.ddl_vendor_serial_no, "-1");
        AnthemHelper.SetAnthenTextBoxNULL(this.txtstaff_serial_no);
        AnthemHelper.SetAnthenTextBoxNULL(this.txtpurchase_product_list);

    }

    private void BindProductsDDL(string[] products)
    {
        this.ddl_product_serial_no.Items.Clear();
        for (int i = 0; i < products.Length; i++)
        {
            ListItem li = new ListItem(ProductModel.GetProductModel(int.Parse(products[i])).product_short_name, products[i]);
            this.ddl_product_serial_no.Items.Add(li);
        }
        this.ddl_product_serial_no.AutoUpdateAfterCallBack = true;
    }

    private void InitialScan(int purchase_serial_no)
    {
        PurchaseModel model = PurchaseModel.GetPurchaseModel(purchase_serial_no);

        string products = model.purchase_product_list;
        string[] productss = products.Split(',');
        BindProductsDDL(productss);

        AnthemHelper.SetLabel(this.lbl_invoice, model.purchase_invoice);
        AnthemHelper.SetLabel(this.lbl_vendor, VendorModel.GetVendorModel(model.vendor_serial_no).company_name.ToString());

    }
    
    private void SaveScanIn(int purchase_serial_no)
    {
        ProductInModel model = new ProductInModel();

        PurchaseModel purchase = PurchaseModel.GetPurchaseModel(this.PurchaseID);
        ProductInModel[] ins = ProductInModel.GetProductInModelsByPurchase(PurchaseID);
        int productID = AnthemHelper.GetDropDownList(this.ddl_product_serial_no);
        string product_sns =  AnthemHelper.GetAnthemTextBox(this.txt_product_sns);

        for (int i = 0; i < ins.Length; i++)
        {
            if(productID == ins[i].product_serial_no)
                model = ins[i];
        }

        model.product_in_cost = double.Parse(AnthemHelper.GetAnthemTextBox(this.txt_product_in_cost));
        model.product_in_date = DateTime.Now;
        string date = AnthemHelper.GetAnthemTextBox(this.txt_product_in_end_date);
        if(date == "")
        {
            AnthemHelper.Alert("请输入保修终止日期");
            return;
        }
        model.product_in_end_date = DateTime.Parse(date) ;
        model.product_in_staff = int.Parse(LoginUser.LoginID);
        model.product_serial_no = productID;
        model.product_sns = product_sns;
        model.purchase_serial_no = PurchaseID.ToString();
        model.tag = 1;
        model.system_category_serial_no = Config.SystemCategory;
        model.Save();
        // 入库
        string[] ts = product_sns.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        SaveProductDetail(ts, model.product_in_serial_no, productID, PurchaseID);

        // 变更数量
        ProductModel p = ProductModel.GetProductModel(productID);
        p.product_store_sum = ProductDetailModel.GetCountByProduct(productID);
        p.Update();

    }

    private void SaveProductDetail(string[] product_sns, int product_in_serial_no, int product_serial_no, int purchase_serial_no)
    {
        ProductDetailModel.DeleteByProductInSerial(product_in_serial_no);
        for (int i = 0; i < product_sns.Length; i++)
        {
            ProductDetailModel model = new ProductDetailModel();
            model.product_detail_create_date = DateTime.Now;
            model.product_detail_is_sale = 0;
            model.product_in_serial_no = product_in_serial_no;
            model.product_serial_no = product_serial_no;
            model.product_sn = product_sns[i];
            model.purchase_serial_no = purchase_serial_no;
            model.tag = 1;
            model.Create();
        }
    }

    private void BindPurchasePayDG()
    {
        PurchasePayModel[] ms = PurchasePayModel.GetModelsByPurchaseSerial(PurchaseID);
        PurchasePays = ms;
        this.dg_purchase_pay.DataSource = ms;
        this.dg_purchase_pay.DataBind();
        this.dg_purchase_pay.AutoUpdateAfterCallBack = true;
    }

    private void NewPurchasePay(int purchase_id)
    {
        if (purchase_id == -1)
        {
            AnthemHelper.Alert("Check Invoice, please!");
            return;
        }
        PurchasePayModel model = new PurchasePayModel();
        model.create_datetime = DateTime.Now;
        model.purchase_serial_no = purchase_id;
        model.tag = 1;
        model.Create();
    }
    #endregion


    #region Events
    protected void dg_purchase_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Header && e.Item.ItemType != ListItemType.Footer)
        {
            System.Web.UI.WebControls.LinkButton lb = (System.Web.UI.WebControls.LinkButton)e.CommandSource;
           
            switch (lb.Text)
            {
                case "Pay":
                    PurchaseID = AnthemHelper.GetAnthemDataGridCellText(e.Item, 0);
                    BindPurchasePayDG();
                    SetControlsValue(PurchaseID);
                    break;

                case "SCAN":
                    try
                    {
                        PurchaseID = AnthemHelper.GetAnthemDataGridCellText(e.Item, 0);
                       //this.ddl_product_serial_no.Items.Clear();
                       // this.ddl_product_serial_no.UpdateAfterCallBack = true;

                        InitialScan(PurchaseID);
                        SetControlsValue(PurchaseID);
                       
                    }
                    catch (Exception ex)
                    {
                        AnthemHelper.Alert(ex.Message);
                    }
                    break;

                case "Modify":
                    PurchaseID = AnthemHelper.GetAnthemDataGridCellText(e.Item, 0);
                    SetControlsValue(PurchaseID);
                    AnthemHelper.SetAnthemButton(this.btn_save, "Modify");
                    Cmd = Command.modif;

                    break;
            }
        }

    }
    protected void btn_save_Click(object sender, EventArgs e)
    {

    }
    protected void btn_save_Click1(object sender, EventArgs e)
    {
        try
        {
            SavePurchase();
            BindPurchaseDG(true);
        }
        catch (Exception ex)
        {
            AnthemHelper.Alert(ex.Message);
        }
    }

    protected void btn_save_scan_in_Click(object sender, EventArgs e)
    {
        if (PurchaseID != -1)
        {
            try
            {
                SaveScanIn(PurchaseID);
            }
            catch (Exception ex)
            {
                AnthemHelper.Alert(ex.Message);
            }
        }
        else
            AnthemHelper.Alert("请选择Invocie");
    }

    protected void txt_product_sns_TextChanged(object sender, EventArgs e)
    {
        string text = this.txt_product_sns.Text;

        if (text != "")
        {
            try
            {
                if (text.IndexOf("\r\n") > 0)
                {
                    string[] ts = text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    this.lbl_product_count.Text = ts.Length.ToString() + "&nbsp;Count";
                    this.lbl_product_count.AutoUpdateAfterCallBack = true;
                }
            }
            catch (Exception ex)
            {
                AnthemHelper.Alert(ex.Message);
            }
        }
    }

    protected void btn_new_pay_Click(object sender, EventArgs e)
    {
        NewPurchasePay(PurchaseID);
        BindPurchasePayDG();
    }
    #endregion

    #region Fields
    public int PurchaseID
    {
        get
        {
            object o = ViewState["PurchaseID"];
            if (o != null)
                return int.Parse(o.ToString());
            return -1;
        }
        set
        {
            ViewState["PurchaseID"] = value;
        }
    }

    public PurchasePayModel[] PurchasePays
    {
        get
        {
            object o = ViewState["PurchasePays"];
            if (o != null)
                return (PurchasePayModel[])o;
            return PurchasePayModel.GetModelsByPurchaseSerial(PurchaseID);
        }
        set { ViewState["PurchasePays"] = value; }
    }

    #endregion

    protected void dg_purchase_pay_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Header && e.Item.ItemType != ListItemType.Footer)
        {
            try
            {
                int pay_id = AnthemHelper.GetAnthemDataGridCellText(e.Item, 0);
                PurchasePayModel m = PurchasePay(pay_id);
                AnthemHelper.SetAnthemDataGridCellTextBoxText(e.Item, 1, "_txt_amount", m.amount.ToString());
                Anthem.DropDownList ddl = (Anthem.DropDownList)e.Item.Cells[2].FindControl("_ddl_pay_method");
                BindPayMehtod(ddl, m.pay_method_serial_no.ToString());
                AnthemHelper.SetAnthemDataGridCellTextBoxText(e.Item, 1, "_txt_check_code", m.check_code.ToString());
                AnthemHelper.SetAnthemDataGridCellTextBoxText(e.Item, 1, "_txt_date", m.date.ToString("yyyy-MM-dd"));
                AnthemHelper.SetAnthemDataGridCellTextBoxText(e.Item, 1, "_txt_balance", m.balance.ToString());
            }
            catch (Exception ex)
            {
                AnthemHelper.Alert(ex.Message);
            }
        }
    }

    private void BindPayMehtod(Anthem.DropDownList ddl , string select_value)
    {
            ddl.DataSource = PayMethodHelper.pay_method_ToDataTable();
            ddl.DataTextField = KeyText.description;
            ddl.DataValueField = KeyText.value;
           
            ddl.DataBind();
            ddl.SelectedValue = select_value;
            ddl.AutoUpdateAfterCallBack = true;
    }


    private PurchasePayModel PurchasePay(int id)
    {
        for (int i = 0; i < PurchasePays.Length; i++)
        {
            if (PurchasePays[i].purchase_pay_serial_no == id)
                return PurchasePays[i];
        }
        return new PurchasePayModel();
    }
    protected void btn_save_pay_Click(object sender, EventArgs e)
    {
        Anthem.DataGrid dg = (Anthem.DataGrid)this.dg_purchase_pay;
        for (int i = 0; i < dg.Items.Count; i++)
        {
            DataGridItem item = dg.Items[i];
            int pay_id = AnthemHelper.GetAnthemDataGridCellText(item, 0);
            PurchasePayModel model = PurchasePayModel.GetPurchasePayModel(pay_id);
            model.amount = AnthemHelper.GetAnthemDataGridCellTextBoxTextInt(item, 1, "_txt_amount");
            model.pay_method_serial_no = AnthemHelper.GetAnthemDataGridCellDropDownList(item, 2, "_ddl_pay_method");
            model.check_code = AnthemHelper.GetAnthemDataGridCellTextBoxText(item, 3, "_txt_check_code");
            string date = AnthemHelper.GetAnthemDataGridCellTextBoxText(item, 4, "_txt_date");
            if (date != "")
                model.date = DateTime.Parse(date);
            model.balance = AnthemHelper.GetAnthemDataGridCellTextBoxTextDouble(item, 5, "_txt_balance");
            model.Update();
        }
    }
    protected void btn_bring_Click(object sender, EventArgs e)
    {
        int number = 0;
        try
        {
            number = int.Parse(this.txt_number.Text.Trim());

            BringProductSN(number);
        }
        catch
        {
            AnthemHelper.Alert("数据只能是数字类型");
            return;
        }



    }

    public void BringProductSN(int number)
    {
        int product_id = 0;
        try{
            product_id = int.Parse(this.ddl_product_serial_no.SelectedValue);
        }
        catch{
            AnthemHelper.Alert("当前没有产品选中");
            return;
        }
        string numbers = "";
        for (int i = 0; i < number; i++)
        {
            numbers += int.Parse(this.ddl_product_serial_no.SelectedValue).ToString("00000") + Config.GetDateTimeString + (i+1).ToString("0000")+"\r\n";
        }

        this.txt_product_sns.Text = numbers;
        this.txt_product_sns.UpdateAfterCallBack = true;
    }
    protected void dg_purchase_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        try
        {
            this.dg_purchase.CurrentPageIndex = e.NewPageIndex;
            this.BindPurchaseDG(true);
        }
        catch (Exception ex)
        {
            AnthemHelper.Alert(ex.Message);
        }
    }
}
