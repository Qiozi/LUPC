using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ShoppingCartTo : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (ReqSKU > 0 && IsLocalHostFrom)
            {
                if (!IsLogin)
                {
                    Response.Redirect("login.aspx?u=ShoppingCartTo.aspx?sku=" + ReqSKU, true);
                }
                else
                {
                    //
                    // 先判断购物车是否有已存在的商品。 
                    // 判断订单编号是否已有正式订单， 如果有，删除购物车数据。
                    // 
                    int code = 0;
                    int custSerialNo;
                    int.TryParse(CustomerSerialNo, out custSerialNo);
                    var cart = db.tb_cart_temp.FirstOrDefault(p => p.customer_serial_no.HasValue
                        && p.customer_serial_no.Value.Equals(custSerialNo));

                    if (cart != null)
                    {
                        code = cart.cart_temp_code.Value > 0 ? cart.cart_temp_code.Value : Codes.NewOrderCode(db);

                        if (cart.cart_temp_code.Value == 0)
                        {
                            CurrOrderCode = code;
                        }

                        var order = db.tb_order_helper.FirstOrDefault(p => p.order_code.HasValue
                            && p.order_code.Value.Equals(code));
                        if (order != null)
                        {
                            db.DeleteObject(cart); // 删除临时订单
                            db.SaveChanges();
                        }
                    }
                    else
                        code = Codes.NewOrderCode(db);

                    CurrOrderCode = code;

                    var cartList = db.tb_cart_temp.Where(p => p.customer_serial_no.HasValue
                       && p.customer_serial_no.Value.Equals(custSerialNo));
                    foreach (var cl in cartList)
                    {
                        cl.cart_temp_code = code;
                    }
                    db.SaveChanges();

                    var customer = CurrCustomer;
                    //
                    // 零件，笔记本
                    if (ReqSKU.ToString().Length < 6)
                    {
                        #region 零件
                        var part = db.tb_product.FirstOrDefault(p => p.product_serial_no.Equals(ReqSKU));
                        int categoryID = part.menu_child_serial_no.Value;

                        var categoryModel = db.tb_product_category.FirstOrDefault(p => p.menu_child_serial_no.Equals(categoryID));

                        var newCart = nicklu2Model.tb_cart_temp.Createtb_cart_temp(0);
                        newCart.cart_temp_code = code;
                        newCart.customer_serial_no = customer.customer_serial_no;
                        newCart.cart_temp_Quantity = 1;
                        newCart.cost = part.curr_change_cost;
                        //newCart.country_id = customer.customer_country;
                        newCart.create_datetime = DateTime.Now;

                        newCart.current_system = CurrSiteCountry == CountryType.CAD ? 1 : 2;
                        newCart.customer = CurrCustomer.customer_serial_no.Value.ToString();
                        newCart.customer_serial_no = CurrCustomer.customer_serial_no.Value;
                        newCart.ip = Page.Request.UserHostAddress;
                        newCart.is_noebook = categoryModel != null ? categoryModel.is_noebook : 0;
                        newCart.menu_child_serial_no = categoryID;
                        newCart.old_price = part.product_current_price;
                        newCart.pay_method = -1;
                        newCart.pick_datetime_1 = DateTime.MinValue;
                        newCart.pick_datetime_2 = DateTime.MinValue;
                        newCart.price = part.product_current_price - part.product_current_discount;
                        newCart.price_rate = PRate;
                        newCart.price_unit = CurrSiteCountry.ToString();
                        newCart.prodType = part.prodType;
                        newCart.product_name = string.IsNullOrEmpty(part.product_ebay_name) ? part.product_name : part.product_ebay_name;
                        newCart.product_serial_no = ReqSKU;
                        newCart.sale_tax = 0M;
                        newCart.save_price = part.product_current_discount;
                        newCart.shipping_charge = 0M;
                        newCart.shipping_company = 0;
                        newCart.shipping_country_code = customer.shipping_country_code;
                        newCart.shipping_state_code = customer.shipping_state_code;
                        newCart.state_shipping = customer.state_serial_no;
                        db.AddTotb_cart_temp(newCart);
                        db.SaveChanges();
                        #endregion
                    }
                    else if (ReqSKU.ToString().Length == 8)
                    {
                        #region 客户配置的系统
                        var sysSku = ReqSKU.ToString();
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
                                            Discount = p.product_current_discount.Value

                                        }).ToList();

                        var newCart = nicklu2Model.tb_cart_temp.Createtb_cart_temp(0);
                        newCart.cart_temp_code = code;
                        newCart.customer_serial_no = customer.customer_serial_no;
                        newCart.cart_temp_Quantity = 1;
                        newCart.cost = prodList.Sum(p => p.Cost);
                        //newCart.country_id = customer.customer_country;
                        newCart.create_datetime = DateTime.Now;
                        newCart.current_system = CurrSiteCountry == CountryType.CAD ? 1 : 2;
                        newCart.customer = CurrCustomer.customer_serial_no.Value.ToString();
                        newCart.customer_serial_no = CurrCustomer.customer_serial_no.Value;
                        newCart.ip = Page.Request.UserHostAddress;
                        newCart.is_noebook = 0;
                        newCart.menu_child_serial_no = 0;
                        newCart.old_price = prodList.Sum(p => p.Price);
                        newCart.pay_method = -1;
                        newCart.pick_datetime_1 = DateTime.MinValue;
                        newCart.pick_datetime_2 = DateTime.MinValue;
                        newCart.price = prodList.Sum(p => p.Price - p.Discount);
                        newCart.price_rate = PRate;
                        newCart.price_unit = CurrSiteCountry.ToString();
                        newCart.prodType = "NEW";
                        newCart.product_name = "Customize System: " + sysSku;
                        newCart.product_serial_no = ReqSKU;
                        newCart.sale_tax = 0M;
                        newCart.save_price = prodList.Sum(p => p.Discount);
                        newCart.shipping_charge = 0M;
                        newCart.shipping_company = 0;
                        newCart.shipping_country_code = customer.shipping_country_code;
                        newCart.shipping_state_code = customer.shipping_state_code;
                        newCart.state_shipping = customer.state_serial_no;
                        db.AddTotb_cart_temp(newCart);
                        db.SaveChanges();
                        #endregion
                    }
                    else if (ReqSKU.ToString().Length == 6)
                    {
                        #region 系统
                        var sysSku = ReqSKU;
                        var part = db.tb_ebay_system.FirstOrDefault(p => p.id.Equals(sysSku));
                        //int categoryID = 0;
                        var newSysSku = SysProd.CustomizeNewSys(""
                            , ReqSKU
                            , IsLogin ? CustomerName : ""
                            , Request.UserHostAddress
                            , CurrSiteCountry
                            , false
                            , db);
                        //var categoryModel = db.tb_product_category.FirstOrDefault(p => p.menu_child_serial_no.Equals(categoryID));

                        var prodList = (from sp in db.tb_ebay_system_parts
                                        join p in db.tb_product on sp.luc_sku.Value equals p.product_serial_no
                                        where sp.system_sku.Value.Equals(sysSku)
                                        select new
                                        {
                                            Price = p.product_current_price.Value,
                                            Cost = p.product_current_cost.Value,
                                            Discount = p.product_current_discount.Value
                                        }).ToList();

                        var newCart = nicklu2Model.tb_cart_temp.Createtb_cart_temp(0);
                        newCart.cart_temp_code = code;
                        newCart.customer_serial_no = customer.customer_serial_no;
                        newCart.cart_temp_Quantity = 1;
                        newCart.cost = prodList.Sum(p => p.Cost);
                        //newCart.country_id = customer.customer_country;
                        newCart.create_datetime = DateTime.Now;
                        newCart.current_system = CurrSiteCountry == CountryType.CAD ? 1 : 2;
                        newCart.customer = CurrCustomer.customer_serial_no.Value.ToString();
                        newCart.customer_serial_no = CurrCustomer.customer_serial_no.Value;
                        newCart.ip = Page.Request.UserHostAddress;
                        newCart.is_noebook = 0;
                        newCart.menu_child_serial_no = 0;
                        newCart.old_price = prodList.Sum(p => p.Price);
                        newCart.pay_method = -1;
                        newCart.pick_datetime_1 = DateTime.MinValue;
                        newCart.pick_datetime_2 = DateTime.MinValue;
                        newCart.price = prodList.Sum(p => p.Price - p.Discount);
                        newCart.price_rate = PRate;
                        newCart.price_unit = CurrSiteCountry.ToString();
                        newCart.prodType = "NEW";
                        newCart.product_name = part.ebay_system_name;
                        newCart.product_serial_no = newSysSku;
                        newCart.sale_tax = 0M;
                        newCart.save_price = prodList.Sum(p => p.Discount);
                        newCart.shipping_charge = 0M;
                        newCart.shipping_company = 0;
                        newCart.shipping_country_code = customer.shipping_country_code;
                        newCart.shipping_state_code = customer.shipping_state_code;
                        newCart.state_shipping = customer.state_serial_no;
                        db.AddTotb_cart_temp(newCart);
                        db.SaveChanges();
                        #endregion
                    }

                    Response.Redirect("ShoppingCart.aspx", true);
                }
            }
            else
            {
                // 不是本站进来的连接。。。
                Response.Write("other");
            }
        }
    }
}