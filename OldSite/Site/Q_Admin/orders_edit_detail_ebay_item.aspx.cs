using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Q_Admin_orders_edit_detail_ebay_item2 : PageBase
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitPage();
        }
    }
    
    void InitPage()
    {
        DataTable dt = Config.ExecuteDataTable(string.Format(@"
SET SQL_BIG_SELECTS=1;
select part_group_id,is_belong_price, luc_sku, product_ebay_name, product_current_price, product_current_cost, em.comment commentName, p.product_current_discount, p.menu_child_serial_no
 from tb_ebay_system_parts ep 
 inner join tb_product p on p.product_serial_no= ep.luc_sku 
 inner join tb_ebay_code_and_luc_sku ec on ec.sku = ep.system_sku and ec.ebay_code='{0}' 
 inner join tb_ebay_system_part_comment em on em.id=ep.comment_id where is_belong_price =1", ReqItemid));

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (dt.Rows[i]["luc_sku"].ToString() == "16684")
            {
                dt.Rows.RemoveAt(i);
                i--;
            }
        }

        ItemList = dt;
        this.DataList1.DataSource = dt;
        this.DataList1.DataBind();
    }

    DataTable ItemList
    {
        get
        {
            object obj = ViewState["ItemList"];
            if (obj != null)
                return (DataTable)obj;
            return null;
        }
        set { ViewState["ItemList"] = value; }
    }

    string ReqItemid
    {
        get { return Util.GetStringSafeFromQueryString(Page, "itemid"); }
    }

    int ReqOrderCode
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "OrderCode", -1); }
    }


    protected void btn_add_Click(object sender, EventArgs e)
    {
        try
        {
            string sysSku = Config.ExecuteScalarInt32(string.Format("select sku from tb_ebay_code_and_luc_sku where ebay_code='{0}' limit 0,1 ", ReqItemid)).ToString();
            var oh = new OrderHelper(DBContext);
            var ohm = OrderHelperModel.GetModelByOrderCode(DBContext, ReqOrderCode);
            XmlStore xs = new XmlStore();
            DataTable partGroup = xs.FindPartGroupComment();
            string error = "";
            oh.CopySystemToOrder(sysSku, false, ReqOrderCode.ToString(), partGroup, ohm.price_unit == "cad" ? CountryCategory.CA : CountryCategory.US, ref error);
            
            InsertTraceInfo(DBContext, string.Format("add system{1} to order({0})", ReqOrderCode, ReqItemid));

            //OrderHelperModel OH = OrderHelperModel.GetModelByOrderCode(ReqOrderCode);
            //DataTable dt = ItemList;
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    DataRow dr = dt.Rows[i];

            //    int _product_id = 0;
            //    int.TryParse(dr["luc_sku"].ToString(), out _product_id);

            //    int menu_child_serial_no = int.Parse(dr["menu_child_serial_no"].ToString());

            //    decimal price;
            //    decimal.TryParse(dr["product_current_price"].ToString(), out price);

            //    decimal cost;
            //    decimal.TryParse(dr["product_current_cost"].ToString(), out cost);

            //    decimal discount;
            //    decimal.TryParse(dr["product_current_discount"].ToString(), out discount);

            //    // ProductModel product = ProductModel.GetProductModel(_product_id);
            //    ProductCategoryModel pc = ProductCategoryModel.GetProductCategoryModel(menu_child_serial_no);
            //    OrderProductModel order = new OrderProductModel();

            //    order.menu_child_serial_no = menu_child_serial_no;
            //    order.order_code = ReqOrderCode.ToString();
            //    order.order_product_cost = ConvertPrice.Price(OH.price_unit.ToString() == "cad" ? CountryCategory.CA : CountryCategory.US, cost);
            //    order.order_product_price = ConvertPrice.Price(OH.price_unit.ToString() == "cad" ? CountryCategory.CA : CountryCategory.US, price);

            //    order.order_product_sum = 1;
            //    order.product_name = dr["product_ebay_name"].ToString();
            //    order.product_serial_no = _product_id;
            //    order.sku = _product_id.ToString();

            //    order.order_product_sold = ConvertPrice.Price(OH.price_unit.ToString() == "cad" ? CountryCategory.CA : CountryCategory.US, price - discount);// ProductModel.FindOnSaleDiscountByPID(_product_id);
            //    //throw new Exception(CC.ToString());
            //    order.tag = 1;
            //    //order.menu_pre_serial_no = product.menu_child_serial_no;
            //    order.product_type = Product_category_helper.product_category_value(pc.is_noebook == byte.Parse("1") ? product_category.noebooks : product_category.part_product);
            //    order.product_type_name = pc.is_noebook == byte.Parse("1") ? "Noebook" : "Unit";
            //    order.product_current_price_rate = ConvertPrice.Price(OH.price_unit.ToString() == "cad" ? CountryCategory.CA : CountryCategory.US, price);
            //    order.Create();

            //    InsertTraceInfo(string.Format("insert part({0}) in order({1}) form itemid: " + ReqItemid, _product_id, ReqOrderCode.ToString()));
            //    //
            //    //  if the order is OK then save a product after create.
            //    //
            //    if (OH.is_ok)
            //    {
            //        string error = "";
            //        OrderHelper oh = new OrderHelper();
            //        if (!oh.CopyProductToHistoryStore(order, true, ref error))
            //            throw new Exception(error);
            //    }
            //}
            OrdersSavePageRedirect(ReqOrderCode);

            Response.Write("<script>parent.location.href= '/q_admin/orders_edit_detail_new.aspx?order_code=" + ReqOrderCode.ToString() + "'; this.close();</script>");

        }
        catch (Exception ex)
        {
            CH.Alert(ex.Message, this.DataList1);
            CH.CloseParentWatting(this.DataList1);
        }
    }
}