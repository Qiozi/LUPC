using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class cmds_Shopping : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (!IsLogin && IsLocalHostFrom)
            {
                Response.Write("no login");
                Response.End();
            }

            if (CurrOrderCode == 0 && ReqCmd != "getShoppingList")
            {
                Response.Write("Order is not find.");
                Response.End();
            }

            var orderCode = CurrOrderCode;

            switch (ReqCmd)
            {
                case "getShoppingList":
                 
                    #region getShoppingList
                    int CustSerialNo;
                    int.TryParse(CustomerSerialNo, out CustSerialNo);

                    var tmpCartList = (from c in db.tb_cart_temp
                                       where c.cart_temp_code.HasValue &&
                                       c.cart_temp_code.Value.Equals(orderCode)
                                       select new
                                       {
                                           SKU = c.product_serial_no.Value,
                                           Qty = c.cart_temp_Quantity.Value,
                                           ID = c.cart_temp_serial_no,
                                           OrderCode = c.cart_temp_code,
                                           Title = c.product_name
                                       }).ToList();
                    //Response.Write(tmpCartList.Count.ToString());

                    decimal sub_total = 0M;
                    List<ShoppingListModel> ShopList = new List<ShoppingListModel>();
                    foreach (var tmpM in tmpCartList)
                    {
                        // CurrOrderCode = tmpM.OrderCode.Value;
                        orderCode = tmpM.OrderCode.Value;
                        int sku = tmpM.SKU;
                        if (sku.ToString().Length < 6)
                        {
                            var prod = db.tb_product.FirstOrDefault(p => p.product_serial_no.Equals(sku));
                            if (prod != null)
                            {
                                #region 零件 笔记本
                                ShoppingListModel m = new ShoppingListModel();
                                m.ID = tmpM.ID;
                                m.SKU = sku;
                                m.Qty = tmpM.Qty;
                                m.Price = ConvertPrice(prod.product_current_price.Value);
                                m.PriceString = PriceRate.Format(m.Price);
                                m.Sold = ConvertPrice(prod.product_current_price.Value - prod.product_current_discount.Value);
                                m.SoldString = PriceRate.Format(m.Sold);
                                m.ImgUrl = "https://lucomputers.com/pro_img/COMPONENTS/" + (prod.other_product_sku > 0 ? prod.other_product_sku : prod.product_serial_no) + "_list_1.jpg";
                                m.Title = string.IsNullOrEmpty(prod.product_ebay_name) ? prod.product_name : prod.product_ebay_name;
                                m.SubSold = PriceRate.Multiply(m.Sold, m.Qty);
                                m.SubSoldString = PriceRate.Format(m.SubSold);
                                m.PriceUnit = CurrSiteCountry.ToString();

                                sub_total += m.SubSold;
                                ShopList.Add(m);

                                SaveToCartTemp(m.SKU
                                    , CustSerialNo
                                    , ConvertPrice(prod.product_current_price.Value)
                                    , m.Sold

                                    , ConvertPrice(prod.product_current_discount.Value)
                                    , m.Title
                                    , ConvertPrice(prod.product_current_cost.Value)
                                    , m.Qty
                                    , CurrOrderCode);
                                #endregion
                            }
                        }
                        else if (sku.ToString().Length == 8)
                        {
                            #region 客户配的系统
                            var sysSku = tmpM.SKU.ToString();
                            var part = db.tb_sp_tmp.FirstOrDefault(p => p.sys_tmp_code.Equals(sysSku));
                            //int categoryID = 0;

                            //var categoryModel = db.tb_product_category.FirstOrDefault(p => p.menu_child_serial_no.Equals(categoryID));

                            var prodList = (from sp in db.tb_sp_tmp_detail
                                            join p in db.tb_product on sp.product_serial_no.Value equals p.product_serial_no
                                            where sp.sys_tmp_code.Equals(sysSku)
                                            select new
                                            {
                                                Price = p.product_current_price.Value,
                                                Cost = p.product_current_cost.Value,
                                                Discount = p.product_current_discount.Value,
                                                IsCase = sp.cate_name.ToLower().Trim() == "case",
                                                PartSku = p.product_serial_no
                                            }).ToList();

                            ShoppingListModel m = new ShoppingListModel();
                            m.ID = tmpM.ID;
                            m.SKU = sku;
                            m.Qty = tmpM.Qty;
                            m.Price = ConvertPrice(prodList.Sum(p => p.Price));
                            m.PriceString = PriceRate.Format(m.Price);
                            m.Sold = ConvertPrice(prodList.Sum(p => p.Price - p.Discount));
                            m.SoldString = PriceRate.Format(m.Sold);
                            var caseProd = prodList.FirstOrDefault(p => p.IsCase);

                            m.ImgUrl = caseProd == null ? "" : "https://lucomputers.com/pro_img/COMPONENTS/" + (caseProd.PartSku) + "_list_1.jpg";
                            m.Title = tmpM.Title;
                            m.SubSold = PriceRate.Multiply(m.Sold, m.Qty);
                            m.SubSoldString = PriceRate.Format(m.SubSold);
                            m.PriceUnit = CurrSiteCountry.ToString();

                            sub_total += m.SubSold;
                            ShopList.Add(m);

                            SaveToCartTemp(m.SKU
                                , CustSerialNo
                                , m.Price
                                , m.Sold
                                , m.Price - m.Sold
                                , m.Title
                                , prodList.Sum(p => p.Cost)
                                , m.Qty
                                , CurrOrderCode);
                            
                            #endregion
                        }
                    }

                    var oc = orderCode.ToString();
                    if (tmpCartList.Count > 0)
                    {
                        // 保存sub total
                        // 没有价格纪录，就创建一条
                        var cartPrice = db.tb_cart_temp_price.FirstOrDefault(p => p.order_code.Equals(oc));
                        if (cartPrice == null)
                        {
                            cartPrice = nicklu2Model.tb_cart_temp_price.Createtb_cart_temp_price(0);
                            cartPrice.create_datetime = DateTime.Now;
                            cartPrice.sub_total = sub_total;
                            cartPrice.order_code = oc;
                            cartPrice.price_unit = CurrSiteCountry.ToString();
                            db.AddTotb_cart_temp_price(cartPrice);
                        }
                        else
                        {
                            cartPrice.sub_total = sub_total;
                        }
                        db.SaveChanges();
                    }
                    else // 订单没有商品了，删除价格纪录
                    {
                         var cartPrice = db.tb_cart_temp_price.FirstOrDefault(p => p.order_code.Equals(oc));
                         if (cartPrice != null)
                         {
                             db.DeleteObject(cartPrice);
                             db.SaveChanges();
                         }
                    }
                    
                    CartQty = ShopList.Count;
                    //Response.Write(tmpCartList.Count);
                    string json = Newtonsoft.Json.JsonConvert.SerializeObject(ShopList);
                    Response.Write("{\"total\":\"" +PriceRate.Format( sub_total) + "\", row:" + json + "}");
                    #endregion

                    break;


                case "ChangeQty":
                    var tmpCart = db.tb_cart_temp.FirstOrDefault(p => p.cart_temp_serial_no.Equals(ReqID));
                    if (tmpCart != null)
                    {
                        tmpCart.cart_temp_Quantity = ReqQty < 1 ? 1 : ReqQty;
                        db.SaveChanges();
                    }
                    break;

                case "del":
                    var tmpDel = db.tb_cart_temp.FirstOrDefault(p => p.cart_temp_serial_no.Equals(ReqID));
                    if (tmpDel != null)
                    {
                        db.DeleteObject(tmpDel);
                        db.SaveChanges();
                    }
                    break;
                case "getpaymentAll":

                    #region get payment all
                    var paymentList = (from c in db.tb_pay_method_new
                                       where c.tag.HasValue
                                       && c.tag.Value.Equals(true)
                                       && c.is_card.HasValue
                                       orderby c.taxis ascending
                                       select new
                                       {
                                           Name = c.pay_method_name,
                                           ID = c.pay_method_serial_no,
                                           SupperCountry = c.supper_country,
                                           IsCard = c.is_card.Value
                                       }).ToList();
                    string jsonPayment = Newtonsoft.Json.JsonConvert.SerializeObject(paymentList);
                    Response.Write(jsonPayment);
                    #endregion

                    break;

                case "getShippingCompanyALL":

                    #region get shipping company all
                    var shippingList = (from c in db.tb_shipping_company
                                        where c.showit.HasValue
                                        && c.showit.Value.Equals(true)
                                        && c.is_sales_promotion.HasValue
                                        && c.is_sales_promotion.Value.Equals(false)
                                        orderby c.qty ascending
                                        select new
                                        {
                                            Name = c.shipping_company_name,
                                            ID = c.shipping_company_id,
                                            Country = c.system_category.Value == 3 ? "Other" : (c.system_category.Value == 1 ? "CA" : "US")

                                        }).ToList();
                    string jsonShippingAll = Newtonsoft.Json.JsonConvert.SerializeObject(shippingList);
                    Response.Write(jsonShippingAll);
                    #endregion

                    break;

                case "getShippingCharge":

                    #region get shipping charge
                    AccountOrder ao = new AccountOrder();
                    try
                    {
                        decimal shippingCharge = ao.AccountCharge(ReqShippingID
                             , CurrOrderCode
                             , ReqStateID
                             , ReqPayment
                             , db);

                        if (shippingCharge > 0)
                            Response.Write(shippingCharge.ToString("0.00"));
                        else
                        {
                            Response.Write("Service not available.");
                        }
                    }
                    catch (Exception ex)
                    {
                        ErrorHelper.Save(ex, db);
                        Response.Write("Service not available.");
                    }
                    #endregion

                    break;
            }
        }
        Response.End();
    }

    /// <summary>
    /// 保存购物车当前商品价格
    /// 
    /// </summary>
    /// <param name="sku"></param>
    /// <param name="customerSerialNo"></param>
    /// <param name="oldPrice"></param>
    /// <param name="price"></param>
    /// <param name="discount"></param>
    /// <param name="title"></param>
    /// <param name="orderCode"></param>
    void SaveToCartTemp(int sku
        , int customerSerialNo
        , decimal oldPrice
        , decimal price
        , decimal discount
        , string title
        , decimal cost
        , int qty
        , int orderCode)
    {
        var cartTemp = db.tb_cart_temp.FirstOrDefault(p => p.cart_temp_code.HasValue
            && p.cart_temp_code.Value.Equals(orderCode)
            && p.product_serial_no.HasValue
            && p.product_serial_no.Value.Equals(sku)
            );
        if (cartTemp != null)
        {
            cartTemp.old_price = oldPrice;
            cartTemp.price = price;
            cartTemp.product_name = title;
            cartTemp.price_unit = CurrSiteCountry.ToString();
            cartTemp.save_price = discount;
            cartTemp.cost = cost;
            cartTemp.cart_temp_Quantity = qty;
            db.SaveChanges();
        }
    }

    /// <summary>
    /// 清空临时价格表。。。只保留 sub total;
    /// </summary>
    /// <param name="orderCode"></param>
    /// <param name="subTotal"></param>
    void SaveSubTotal(int orderCode, decimal subTotal)
    {
        var cartPrice = db.tb_cart_temp_price.FirstOrDefault(p => p.order_code.Equals(orderCode.ToString()));
        if (cartPrice != null)
        {
            cartPrice.sub_total = subTotal;
            cartPrice.cost = 0M;
            cartPrice.grand_total = 0M;
            cartPrice.grand_total_rate = 0M;
            cartPrice.gst = 0M;
            cartPrice.gst_charge_rate = 0M;
            cartPrice.gst_rate = 0M;
            cartPrice.hst = 0M;
            cartPrice.hst_charge_rate = 0M;
            cartPrice.hst_rate = 0M;
            cartPrice.price_unit = CurrSiteCountry.ToString();
            cartPrice.pst = 0M;
            cartPrice.pst_charge_rate = 0M;
            cartPrice.pst_rate = 0M;
            cartPrice.sales_tax = 0M;
            cartPrice.sales_tax_rate = 0M;
            cartPrice.shipping_and_handling = 0M;
            cartPrice.shipping_and_handling_rate = 0M;
            cartPrice.sub_total_rate = 0M;
            cartPrice.sur_charge = 0M;
            cartPrice.sur_charge_rate = 0M;
            cartPrice.taxable_total = 0M;
            cartPrice.price_unit = "";
            db.SaveChanges();
        }
        else
        {
            var cp = nicklu2Model.tb_cart_temp_price.Createtb_cart_temp_price(0);
            cp.create_datetime = DateTime.Now;
            cp.sub_total = subTotal;
            cp.cost = 0M;
            cp.grand_total = 0M;
            cp.grand_total_rate = 0M;
            cp.gst = 0M;
            cp.gst_charge_rate = 0M;
            cp.gst_rate = 0M;
            cp.hst = 0M;
            cp.hst_charge_rate = 0M;
            cp.hst_rate = 0M;
            cp.price_unit = CurrSiteCountry.ToString();
            cp.pst = 0M;
            cp.pst_charge_rate = 0M;
            cp.pst_rate = 0M;
            cp.sales_tax = 0M;
            cp.sales_tax_rate = 0M;
            cp.shipping_and_handling = 0M;
            cp.shipping_and_handling_rate = 0M;
            cp.sub_total_rate = 0M;
            cp.sur_charge = 0M;
            cp.sur_charge_rate = 0M;
            cp.taxable_total = 0M;
            cp.price_unit = "";
            db.AddTotb_cart_temp_price(cp);
            db.SaveChanges();
        }
    }

    public int ReqQty
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "qty", 1); }
    }

    int ReqID
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "id", 0); }
    }


    int ReqShippingID
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "shippingid", 0); }
    }

    int ReqStateID
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "stateid", 0); }
    }

    int ReqPayment
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "payment", 0); }
    }
}