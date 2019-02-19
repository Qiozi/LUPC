using LU.Model.ModelV1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LU.BLL
{
    public class ShoppingCartProvider
    {
        public static ShoppingCart GetShoppingCartGoodsList(Data.nicklu2Entities context,
            int orderCode,
            int customerSerialNo,
            Model.Enums.CountryType priceType,
            decimal rate
             )
        {
            #region getShoppingList



            var tmpCartList = (from c in context.tb_cart_temp
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
                    var prod = context.tb_product.FirstOrDefault(p => p.product_serial_no.Equals(sku));
                    if (prod != null)
                    {
                        #region 零件 笔记本
                        ShoppingListModel m = new ShoppingListModel();
                        m.ID = tmpM.ID;
                        m.SKU = sku;
                        m.Qty = tmpM.Qty;
                        m.Price = PRateProvider.ConvertPrice(prod.product_current_price.Value, priceType, rate);
                        m.PriceString = LU.BLL.FormatProvider.Price(m.Price);
                        m.Sold = PRateProvider.ConvertPrice(prod.product_current_price.Value - prod.product_current_discount.Value, priceType, rate);
                        m.SoldString = LU.BLL.FormatProvider.Price(m.Sold);
                        m.ImgUrl = ConfigV1.ImgHost + "pro_img/COMPONENTS/" + (prod.other_product_sku > 0 ? prod.other_product_sku : prod.product_serial_no) + "_list_1.jpg";
                        m.Title = string.IsNullOrEmpty(prod.product_ebay_name) ? prod.product_name : prod.product_ebay_name;
                        m.SubSold = LU.BLL.PRateProvider.Multiply(m.Sold, m.Qty);
                        m.SubSoldString = LU.BLL.FormatProvider.Price(m.SubSold);
                        m.PriceUnit = priceType.ToString();

                        sub_total += m.SubSold;
                        ShopList.Add(m);

                        SaveToCartTemp(context, m.SKU
                            , customerSerialNo
                            , PRateProvider.ConvertPrice(prod.product_current_price.Value, priceType, rate)
                            , m.Sold

                            , PRateProvider.ConvertPrice(prod.product_current_discount.Value, priceType, rate)
                            , m.Title
                            , PRateProvider.ConvertPrice(prod.product_current_cost.Value, priceType, rate)
                            , m.Qty
                            , orderCode
                            , priceType);
                        #endregion
                    }
                }
                else if (sku.ToString().Length == 8)
                {
                    #region 客户配的系统
                    var sysSku = tmpM.SKU.ToString();
                    var part = context.tb_sp_tmp.FirstOrDefault(p => p.sys_tmp_code.Equals(sysSku));
                    //int categoryID = 0;

                    //var categoryModel = db.tb_product_category.FirstOrDefault(p => p.menu_child_serial_no.Equals(categoryID));

                    var prodList = (from sp in context.tb_sp_tmp_detail
                                    join p in context.tb_product on sp.product_serial_no.Value equals p.product_serial_no
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
                    m.Price = PRateProvider.ConvertPrice(prodList.Sum(p => p.Price), priceType, rate);
                    m.PriceString = LU.BLL.FormatProvider.Price(m.Price);
                    m.Sold = PRateProvider.ConvertPrice(prodList.Sum(p => p.Price - p.Discount), priceType, rate);
                    m.SoldString = LU.BLL.FormatProvider.Price(m.Sold);
                    var caseProd = prodList.FirstOrDefault(p => p.IsCase);

                    m.ImgUrl = caseProd == null ? "" : ConfigV1.ImgHost + "pro_img/COMPONENTS/" + (caseProd.PartSku) + "_list_1.jpg";
                    m.Title = tmpM.Title;
                    m.SubSold = LU.BLL.PRateProvider.Multiply(m.Sold, m.Qty);
                    m.SubSoldString = LU.BLL.FormatProvider.Price(m.SubSold);
                    m.PriceUnit = priceType.ToString();

                    sub_total += m.SubSold;
                    ShopList.Add(m);

                    SaveToCartTemp(context, m.SKU
                        , customerSerialNo
                        , m.Price
                        , m.Sold
                        , m.Price - m.Sold
                        , m.Title
                        , prodList.Sum(p => p.Cost)
                        , m.Qty
                        , orderCode
                        , priceType);

                    #endregion
                }
            }

            var oc = orderCode.ToString();
            if (tmpCartList.Count > 0)
            {
                // 保存sub total
                // 没有价格纪录，就创建一条
                var cartPrice = context.tb_cart_temp_price.FirstOrDefault(p => p.order_code.Equals(oc));
                if (cartPrice == null)
                {
                    cartPrice = new LU.Data.tb_cart_temp_price();
                    cartPrice.create_datetime = DateTime.Now;
                    cartPrice.sub_total = sub_total;
                    cartPrice.order_code = oc;
                    cartPrice.price_unit = priceType.ToString();
                    context.tb_cart_temp_price.Add(cartPrice);
                }
                else
                {
                    cartPrice.sub_total = sub_total;
                }
                context.SaveChanges();
            }
            else // 订单没有商品了，删除价格纪录
            {
                var cartPrice = context.tb_cart_temp_price.FirstOrDefault(p => p.order_code.Equals(oc));
                if (cartPrice != null)
                {
                    context.tb_cart_temp_price.Remove(cartPrice);
                    context.SaveChanges();
                }
            }

            return new ShoppingCart
            {
                Goods = ShopList,
                OrderCode = orderCode,
                SubTotal = sub_total,
                SubTotalText = FormatProvider.Price(sub_total)
            };
            #endregion
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
        static void SaveToCartTemp(
            Data.nicklu2Entities context
            , int sku
            , int customerSerialNo
            , decimal oldPrice
            , decimal price
            , decimal discount
            , string title
            , decimal cost
            , int qty
            , int orderCode
            , Model.Enums.CountryType countryType)
        {
            var cartTemp = context.tb_cart_temp.FirstOrDefault(p => p.cart_temp_code.HasValue
                && p.cart_temp_code.Value.Equals(orderCode)
                && p.product_serial_no.HasValue
                && p.product_serial_no.Value.Equals(sku)
                );
            if (cartTemp != null)
            {
                cartTemp.old_price = oldPrice;
                cartTemp.price = price;
                cartTemp.product_name = title;
                cartTemp.price_unit = countryType.ToString();
                cartTemp.save_price = discount;
                cartTemp.cost = cost;
                cartTemp.cart_temp_Quantity = qty;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// 修改购物车商品数量
        /// </summary>
        /// <param name="context"></param>
        /// <param name="id"></param>
        /// <param name="qty"></param>
        /// <returns></returns>
        public static bool ChangeQty(Data.nicklu2Entities context, int id, int qty)
        {
            var tmpCart = context.tb_cart_temp.FirstOrDefault(p => p.cart_temp_serial_no.Equals(id));
            if (tmpCart != null)
            {
                tmpCart.cart_temp_Quantity = qty < 1 ? 1 : qty;
                context.SaveChanges();
            }
            return true;
        }

        /// <summary>
        /// 删除购物车商品
        /// </summary>
        /// <param name="context"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool Del(Data.nicklu2Entities context, int id)
        {
            var tmpDel = context.tb_cart_temp.FirstOrDefault(p => p.cart_temp_serial_no.Equals(id));
            if (tmpDel != null)
            {
                context.tb_cart_temp.Remove(tmpDel);
                context.SaveChanges();
            }
            return true;
        }

        /// <summary>
        /// 获取支付信息
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static object GetPaymentInfo(Data.nicklu2Entities context)
        {
            return (from c in context.tb_pay_method_new
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
        }

        /// <summary>
        /// 运输公司。
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static object GetShippingCompany(Data.nicklu2Entities context)
        {
            return (from c in context.tb_shipping_company
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
        }

        /// <summary>
        /// 计算运费
        /// </summary>
        /// <param name="context"></param>
        /// <param name="orderCode"></param>
        /// <param name="shippingId"></param>
        /// <param name="stateId"></param>
        /// <param name="paymentId"></param>
        /// <returns></returns>
        public static string GetOrderCharge(Data.nicklu2Entities context, int orderCode,
            int shippingId, int stateId, int paymentId)
        {

            #region get shipping charge
            AccountOrder ao = new AccountOrder();
            try
            {
                decimal shippingCharge = ao.AccountCharge(shippingId
                     , orderCode
                     , stateId
                     , paymentId
                     , context);

                if (shippingCharge > 0)
                {
                    return shippingCharge.ToString("0.00");
                }
                else
                {
                    return "Service not available.";
                }
            }
            catch (Exception ex)
            {
                return "Service not available.";
            }
            #endregion
        }

        /// <summary>
        /// 购物车商品数量 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="orderCode"></param>
        /// <returns></returns>
        public static int GetShoppingCartQty(Data.nicklu2Entities context, int orderCode)
        {
            return context.tb_cart_temp
                          .Count(me => me.cart_temp_code.HasValue &&
                                       me.cart_temp_code.Value.Equals(orderCode));
        }
    }
}
