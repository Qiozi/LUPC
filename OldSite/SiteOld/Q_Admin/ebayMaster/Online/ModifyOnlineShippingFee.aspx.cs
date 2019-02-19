using System;
using System.Xml;
using System.Web.UI.WebControls;

public partial class Q_Admin_ebayMaster_Online_ModifyOnlineShippingFee : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            IsSystem = false;
            InitialDatabase();
        }
    }

    public override void InitialDatabase()
    {
        base.InitialDatabase();
        LoadShippingService();
        bool IsStop = GenerateFee();

        if (IsStop)
        {
            return;
        }
        this.Literal_sku.Text = string.Format(@"<b>{0}</b><br><b>{1}</b>", ReqSKU, ReqItemID);
        this.Title = "Modify Shipping:" + string.Format(@" {0} | {1}", ReqSKU, ReqItemID);

        if (ReqIsClose)
        {
            Button1_Click(null, null);
            Response.Write("<script>this.close();</script>");
            Response.End();
        }
        else
            this.Button1.Visible = false;
    }

    bool GenerateFee()
    {

        decimal buyItNowPrice = 0M;
        eBayPriceHelper eH = new eBayPriceHelper();
        EbaySystemModel ESM = EbaySystemModel.GetEbaySystemModel(ReqSKU);

        if (ESM.id > 0)    // is system
        {
            IsSystem = true;
            if (ESM.is_barebone || ESM.is_shrink)
                buyItNowPrice = ESM.selected_ebay_sell;
            else
                buyItNowPrice = ESM.ebay_system_price;
        }

        if (IsSystem)
        {

            if (ESM == null)
                return false;

            decimal price = buyItNowPrice;

            decimal standardPrice = eBayShipping.SysShippingCa(price);
            if (this.ddl_domestic_services_2.SelectedValue == "CA_UPSStandardCanada")
                this.txt_domestic_service_2_cost.Text = "0";// (ESM.is_shrink || ESM.is_barebone) ? "0" : standardPrice.ToString();
            if (this.ddl_domestic_services_3.SelectedValue == "CA_UPSExpeditedCanada")
                this.txt_domestic_service_3_cost.Text = (eBayShipping.SysShippingScLvlCA(price) - 0M).ToString();// (ESM.is_shrink || ESM.is_barebone) ? (eBayShipping.SysShippingScLvlCA(price) - standardPrice).ToString() : eBayShipping.SysShippingScLvlCA(price).ToString();


            if (this.ddl_International_services_1.SelectedValue == "CA_UPSStandardUnitedStates")
                this.txt_international_service_1_cost.Text = "0";// (eBayShipping.SysShippingUS(price) - 0M).ToString();// (ESM.is_shrink || ESM.is_barebone) ? (eBayShipping.SysShippingUS(price) - standardPrice).ToString() : eBayShipping.SysShippingUS(price).ToString();
            if (this.ddl_International_services_2.SelectedValue == "CA_UPS3DaySelectUnitedStates")
            {
                this.txt_international_service_2_cost.Text = (eBayShipping.SysShippingScLvlUS(price) - 0M).ToString();// (ESM.is_shrink || ESM.is_barebone) ? (eBayShipping.SysShippingScLvlUS(price) - standardPrice).ToString() : eBayShipping.SysShippingScLvlUS(price).ToString();
                // shipping + 40%
                //decimal use3DayUSFee;
                //decimal.TryParse(this.txt_international_service_2_cost.Text, out use3DayUSFee);
                ////this.txt_international_service_2_cost.Text = (use3DayUSFee + use3DayUSFee * 0.4M).ToString("000.00");
                //this.txt_international_service_2_cost.Text = (use3DayUSFee ).ToString("00.00");
            }


        }
        else
        {
            ProductModel PM = ProductModel.GetProductModel(ReqSKU);
            if (PM == null)
                return false;


            //throw new Exception(IsNotebook.ToString());
            decimal adjustment = PM.adjustment;
            decimal cost = PM.product_current_cost + adjustment;
            decimal screen = PM.screen_size;

            decimal _profit = 0M;
            decimal _ebay_fee = 0M;
            decimal _shiping_fee = 0M;
            decimal _bank_fee = 0M;

            buyItNowPrice = eH.eBayNetbookPartPrice(cost
                , screen
                , adjustment
                , ref _shiping_fee
                , ref _profit
                , ref _ebay_fee
                , ref _bank_fee);

            decimal newShippingFee = 0M;

            IsNotebook = ProductCategoryModel.IsNotebook(PM.menu_child_serial_no);
            if (IsNotebook)
            {
                if (this.ddl_domestic_services_2.SelectedValue == "CA_UPSStandardCanada")
                {
                    newShippingFee = eBayShipping.BasicShippingFee(screen);
                    if (_shiping_fee <= newShippingFee)
                        newShippingFee -= _shiping_fee;

                    this.txt_domestic_service_2_cost.Text = newShippingFee.ToString();
                }
                if (this.ddl_domestic_services_3.SelectedValue == "CA_UPSExpeditedCanada")
                {
                    newShippingFee = eBayShipping.UPS_WorldExpedited_ShippingFee(screen);
                    if (_shiping_fee <= newShippingFee)
                        newShippingFee -= _shiping_fee;

                    this.txt_domestic_service_3_cost.Text = newShippingFee.ToString();
                }
                if (this.ddl_International_services_1.SelectedValue == "CA_UPSStandardUnitedStates")
                {
                    newShippingFee = eBayShipping.BasicShippingFee(screen);
                    if (_shiping_fee <= newShippingFee)
                        newShippingFee -= _shiping_fee;

                    this.txt_international_service_1_cost.Text = newShippingFee.ToString();
                }

                if (this.ddl_International_services_2.SelectedValue == "CA_UPS3DaySelectUnitedStates")
                {
                    newShippingFee = eBayShipping.UPS3Days_ShippingFee(screen);
                    if (_shiping_fee <= newShippingFee)
                        newShippingFee -= _shiping_fee;

                    this.txt_international_service_2_cost.Text = (newShippingFee).ToString();

                }

                if (this.ddl_International_services_3.SelectedValue == "CA_UPSWorldWideExpedited")
                {
                    newShippingFee = eBayShipping.UPS_WorldExpedited_ShippingFee(screen);
                    if (_shiping_fee <= newShippingFee)
                        newShippingFee -= _shiping_fee;

                    this.txt_international_service_3_cost.Text = newShippingFee.ToString();
                }
                if (this.ddl_International_services_4.SelectedValue == "CA_UPSWorldWideExpress")
                {
                    newShippingFee = eBayShipping.UPS_Express_ShippingFee(screen);
                    if (_shiping_fee <= newShippingFee)
                        newShippingFee -= _shiping_fee;

                    this.txt_international_service_4_cost.Text = newShippingFee.ToString();
                }
            }
            else
            {
                string shippingString = eBayShipping.GetPartShippingFeeString(PM);
                // throw new Exception(shippingString);
                eBayCmdReviseItem.Revise(ReqItemID, IsSystem, null, 0M, ReqSKU, shippingString, null);
                Response.Write("<script>this.close();</script>");
                Response.End();
                return true;
            }
        }
        return false;
    }

    #region Shipping service Method
    private void LoadShippingServiceCategory(XmlNode categoriesNode)
    {

        //get the "Categories" node    
        //throw new Exception(categoriesNode.ChildNodes.Count.ToString());
        foreach (XmlNode cat in categoriesNode.ChildNodes)
        {
            if (cat.ChildNodes.Count > 5)
            {
                if (cat.InnerText.IndexOf("Calculated") == -1)
                {
                    this.ddl_domestic_services_1.Items.Add(new ListItem(cat["Description"].InnerText, cat["ShippingService"].InnerText));
                    this.ddl_domestic_services_2.Items.Add(new ListItem(cat["Description"].InnerText, cat["ShippingService"].InnerText));
                    this.ddl_domestic_services_3.Items.Add(new ListItem(cat["Description"].InnerText, cat["ShippingService"].InnerText));

                    this.ddl_International_services_1.Items.Add(new ListItem(cat["Description"].InnerText, cat["ShippingService"].InnerText));
                    this.ddl_International_services_2.Items.Add(new ListItem(cat["Description"].InnerText, cat["ShippingService"].InnerText));
                    this.ddl_International_services_3.Items.Add(new ListItem(cat["Description"].InnerText, cat["ShippingService"].InnerText));
                    this.ddl_International_services_4.Items.Add(new ListItem(cat["Description"].InnerText, cat["ShippingService"].InnerText));
                }
            }
        }

        this.ddl_domestic_services_1.Items.Insert(0, new ListItem("Select", "-1"));
        this.ddl_domestic_services_1.SelectedValue = "CA_Pickup";
        this.ddl_domestic_services_2.Items.Insert(0, new ListItem("Select", "-1"));
        this.ddl_domestic_services_2.SelectedValue = "CA_UPSStandardCanada";
        this.ddl_domestic_services_3.Items.Insert(0, new ListItem("Select", "-1"));
        this.ddl_domestic_services_3.SelectedValue = "CA_UPSExpeditedCanada";

        this.ddl_International_services_1.Items.Insert(0, new ListItem("Select", "-1"));
        this.ddl_International_services_1.SelectedValue = "CA_UPSStandardUnitedStates";
        this.ddl_International_services_2.Items.Insert(0, new ListItem("Select", "-1"));
        this.ddl_International_services_2.SelectedValue = "CA_UPS3DaySelectUnitedStates";
        this.ddl_International_services_3.Items.Insert(0, new ListItem("Select", "-1"));
        this.ddl_International_services_3.SelectedValue = "CA_UPSWorldWideExpedited";
        this.ddl_International_services_4.Items.Insert(0, new ListItem("Select", "-1"));
        this.ddl_International_services_4.SelectedValue = "CA_UPSWorldWideExpress";
    }
    /// <summary>
    /// 
    /// </summary>
    private void LoadShippingService()
    {
        LoadShippingServiceCategory(new EbayGetXmlHelper().GetShippingServiceXML());
    }
    #endregion

    #region Properties
    bool ReqIsClose
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "isclose", -1) == -1; }
    }

    int ReqSKU
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "sku", -1); }
    }

    string ReqItemID
    {
        get { return Util.GetStringSafeFromQueryString(Page, "itemid"); }
    }

    bool IsNotebook
    {
        get
        {
            if (ViewState["IsNotebook"] != null)
                return (bool)ViewState["IsNotebook"];
            return false;
        }
        set { ViewState["IsNotebook"] = value; }
    }
    bool IsSystem
    {
        get
        {
            if (ViewState["IsSystem"] != null)
                return (bool)ViewState["IsSystem"];
            throw new Exception("IsSystem is null.");
        }
        set { ViewState["IsSystem"] = value; }
    }
    #endregion

    protected void Button1_Click(object sender, EventArgs e)
    {
        string shippingString = GetShippingDetail();

        //this.Literal1.Text = shippingString;
        //return;
        CH.Alert(eBayCmdReviseItem.Revise(ReqItemID, IsSystem, null, 0M, ReqSKU, shippingString, null), this.Literal1);
    }

    private string GetShippingDetail()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        EbaySystemModel ESM = EbaySystemModel.GetEbaySystemModel(ReqSKU);

        decimal domestic_service_1_cost;
        decimal.TryParse(this.txt_domestic_service_1_cost.Text, out domestic_service_1_cost);

        decimal domestic_service_2_cost;
        decimal.TryParse(this.txt_domestic_service_2_cost.Text, out domestic_service_2_cost);

        decimal domestic_service_3_cost;
        decimal.TryParse(this.txt_domestic_service_3_cost.Text, out domestic_service_3_cost);

        decimal international_service_1_cost;
        decimal.TryParse(this.txt_international_service_1_cost.Text, out international_service_1_cost);

        decimal international_service_2_cost;
        decimal.TryParse(this.txt_international_service_2_cost.Text, out international_service_2_cost);

        decimal international_service_3_cost = 0M;
        decimal.TryParse(this.txt_international_service_3_cost.Text, out international_service_3_cost);

        decimal international_service_4_cost;
        decimal.TryParse(this.txt_international_service_4_cost.Text, out international_service_4_cost);
        ProductModel pm = ProductModel.GetProductModel(ReqSKU);


        return eBayShipping.GetShippingDetail2(
            IsNotebook
            , ReqSKU
            , IsSystem
            , (ESM.is_shrink || ESM.is_barebone)
            , ddl_domestic_services_1.SelectedValue.ToString()
            , domestic_service_1_cost
            , this.cb_domestic_free_shipping.Checked
            , ddl_domestic_services_2.SelectedValue.ToString()
            , domestic_service_2_cost
            , ddl_domestic_services_3.SelectedValue.ToString()
            , domestic_service_3_cost
            , "-1"
            , 0M
            , ddl_International_services_1.SelectedValue.ToString()
            , international_service_1_cost
            , ddl_International_services_2.SelectedValue.ToString()
            , international_service_2_cost
            , ddl_International_services_3.SelectedValue.ToString()
            , international_service_3_cost
            , ddl_International_services_4.SelectedValue.ToString()
            , international_service_4_cost
            , pm != null ? pm.screen_size : 0M
            , pm != null ? pm.weight : 0M
            , this.Server);

    }
}