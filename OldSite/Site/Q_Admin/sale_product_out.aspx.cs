using LU.Data;
using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class Q_Admin_sale_product_out : PageBase
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.ValidateLoginRule(Role.add_order);
        }
    }
    

    public void BindProductDG(bool autoUpdate, int order_code)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("SKU");
        dt.Columns.Add("product_name");
        dt.Columns.Add("product_price");

        decimal price = 0;

        // parts 
        DataTable os = OrderProductModel.GetModelsBySearch(order_code.ToString(), product_category.part_product);
        for (int i = 0; i < os.Rows.Count; i++)
        {
            DataRow dr = dt.NewRow();
            dr["SKU"] = os.Rows[i]["product_serial_no"].ToString();
            dr["product_name"] = os.Rows[i]["product_name"].ToString();
            dr["product_price"] = os.Rows[i]["order_product_price"].ToString();
            price += decimal.Parse(os.Rows[i]["order_product_price"].ToString());
            dt.Rows.Add(dr);
        }
        // noebook
        os = OrderProductModel.GetModelsBySearch(order_code.ToString(), product_category.noebooks);
        for (int i = 0; i < os.Rows.Count; i++)
        {
            DataRow dr = dt.NewRow();
            dr["SKU"] = os.Rows[i]["product_serial_no"].ToString();
            dr["product_name"] = os.Rows[i]["product_name"].ToString();
            dr["product_price"] = os.Rows[i]["order_product_price"].ToString();
            price += decimal.Parse(os.Rows[i]["order_product_price"].ToString());
            dt.Rows.Add(dr);
        }
        // system
        os = OrderProductModel.GetModelsBySearch(order_code.ToString(), product_category.system_product);
        for (int i = 0; i < os.Rows.Count; i++)
        {
            var ms = SpTmpDetailModel.GetModelsBySysTmpCode(DBContext, os.Rows[i]["product_serial_no"].ToString());
            for (int j = 0; j < ms.Length; j++)
            {
                var p = ProductModel.GetProductModel(DBContext, ms[j].product_serial_no.Value);
                DataRow dr = dt.NewRow();
                dr["SKU"] = ms[j].product_serial_no;
                dr["product_name"] = p.product_name;
                dr["product_price"] = ms[j].product_current_price;
                price += ms[j].product_current_price.Value;
                dt.Rows.Add(dr);
            }
        }

        this.dg_product.DataSource = dt;
        this.dg_product.DataBind();
        this.dg_product.UpdateAfterCallBack = autoUpdate;

        this.lbl_product_count.Text = dt.Rows.Count.ToString();
        this.lbl_product_count.UpdateAfterCallBack = true;

        this.lbl_product_price.Text = Config.ConvertPrice(price);
        this.lbl_product_price.UpdateAfterCallBack = true;
    }

    protected void lb_search_Click(object sender, EventArgs e)
    {
        string order_code = this.txt_keyword.Text.Trim();

        BindProductDG(true, int.Parse(order_code));
    }
    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {
        string[] ts = this.txt_out_sn.Text.Trim().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        BindStoreDG(ts);
    }

    public DataTable StoreDT
    {
        get
        {
            object o = ViewState["StoreDT"];
            if (o != null)
            {
                return (DataTable)o;

            }
            return new DataTable();
        }
        set { ViewState["StoreDT"] = value; }
    }
    public void BindStoreDG(string[] sns)
    {
        decimal price = 0;

        DataTable dt = new DataTable();
        dt.Columns.Add("SN");
        dt.Columns.Add("in_date");
        dt.Columns.Add("sku");
        dt.Columns.Add("product_name");
        dt.Columns.Add("factory_date");
        dt.Columns.Add("current_price");
        dt.Columns.Add("product_serial_no");

        for (int i = 0; i < sns.Length; i++)
        {
            DataRow dr = dt.NewRow();
            var m = ProductDetailModel.GetModelsBySN(DBContext, sns[i]);
            if (m.Length == 1)
            {
                var p = ProductModel.GetProductModel(DBContext, m[0].product_serial_no.Value);
                var p_in = ProductInModel.GetProductInModel(DBContext, m[0].product_in_serial_no.Value);
                dr["SN"] = sns[i];
                dr["in_date"] = p_in.product_in_date;
                dr["sku"] = m[0].product_serial_no;
                dr["product_name"] = p.product_name;
                if (m[0].product_detail_is_sale == 0)
                    dr["factory_date"] = p_in.product_in_end_date;
                else
                    dr["factory_date"] = "<span style='color:green;'>已出售</span>";
                dr["current_price"] = p.product_current_price;
                price += p.product_current_price.Value;
            }
            else
            {
                dr["SN"] = sns[i];
                dr["factory_date"] = "<span style='color:red;'>产品不存在</span>";

            }
            dt.Rows.Add(dr);

        }

        StoreDT = dt;

        this.dg_store.DataSource = dt;
        this.dg_store.DataBind();
        this.dg_store.UpdateAfterCallBack = true;

        this.lbl_store_count.Text = dt.Rows.Count.ToString();
        this.lbl_store_count.UpdateAfterCallBack = true;

        this.lbl_store_price.Text = Config.ConvertPrice(price);
        this.lbl_store_price.UpdateAfterCallBack = true;
    }
    protected void lb_save_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable _store_dt = StoreDT;
            for (int i = 0; i < _store_dt.Rows.Count; i++)
            {
                DataRow dr = _store_dt.Rows[i];
                var model = new tb_product_out();// ProductOutModel();
                model.create_datetime = DateTime.Now;
                //model.product_serial_no = int.Parse(dr["product_serial_no"]);
                model.system_category_serial_no = Config.SystemCategory;
                model.product_sn = dr["SN"].ToString();
                DBContext.tb_product_out.Add(model);
                DBContext.SaveChanges();

                var pms = ProductDetailModel.GetModelsBySN(DBContext, model.product_sn);
                for (int j = 0; j < pms.Length; j++)
                {
                    pms[i].product_detail_is_sale = 1;
                    pms[i].sale_create_date = DateTime.Now;
                    DBContext.SaveChanges();
                }

            }
            AnthemHelper.Alert(KeyFields.SaveIsOK);
        }
        catch (Exception ex)
        {
            AnthemHelper.Alert(ex.Message);
        }
    }
    protected void dg_product_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Footer && e.Item.ItemType != ListItemType.Header)
        {
            LinkButton lb = (LinkButton)e.Item.Cells[0].Controls[0];

            lb.Attributes.Add("onclick", "return confirm('are you sure?')");
        }

    }
    protected void dg_product_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "SelectSN":
                int product_id = AnthemHelper.GetAnthemDataGridCellText(e.Item, 1);
                SetTextProudctSN(product_id);
                break;
        }
    }

    public void SetTextProudctSN(int product_id)
    {
        string sn = ProductDetailModel.GetSNByProductID(product_id);
        this.txt_out_sn.Text += this.txt_out_sn.Text.Trim() == "" ? sn : "\r\n" + sn;
        this.txt_out_sn.UpdateAfterCallBack = true;

    }
}
