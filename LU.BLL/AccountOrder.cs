using LU.Model.Enums;
using LU.Model.ModelV1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LU.BLL
{
    public class AccountOrder
    {
        public AccountOrder()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// 计算订单的价格
        /// 
        /// </summary>
        /// <param name="shippingCompany"></param>
        /// <param name="orderCode"></param>
        /// <param name="stateID"></param>
        /// <param name="PayMethod"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public decimal AccountCharge(int shippingCompany
            , int orderCode
            , int stateID
            , int PayMethod
            , Data.nicklu2Entities db)
        {
            if (ConfigV1.LocalAll.Contains(PayMethod))
                return 0;
            if (shippingCompany == 0)
                return 0;

            //string error_msg = "Service not available.";

            if (orderCode.ToString().Length != 6)
                throw new Exception("Order code is error");

            List<AccountProduct> ap = new List<AccountProduct>();


            decimal _price_sum = 0;


            //   decimal sale_promotion_charge = 0M;

            // int ap_count = 0;
            var cartTemp = (from c in db.tb_cart_temp
                            where c.cart_temp_code.HasValue
                            && c.cart_temp_code.Value.Equals(orderCode)
                            select c).ToList();

            for (int i = 0; i < cartTemp.Count; i++)
            {

                var cart = cartTemp[i];

                int count = cart.cart_temp_Quantity.Value;


                AccountProduct model = new AccountProduct();
                model.product_id = cart.product_serial_no.Value;

                model.shipping_company_id = shippingCompany;
                model.price = cart.price.Value;
                model.sum = count;

                if (model.product_id.ToString().Length == 8)
                {
                    model.ProductType = ProdType.system_product;
                    model.product_size = SysProdSize.GetSize(model.price, ProdType.system_product, db);
                }
                else if (cart.is_noebook.HasValue && cart.is_noebook.Value.Equals(1))
                {
                    model.ProductType = ProdType.noebooks;
                    model.product_size = SysProdSize.GetSize(model.price, ProdType.noebooks, db);
                }
                else
                {
                    // var pm = ProductModel.GetProductModel(int.Parse(dr["product_serial_no"].ToString()));
                    var pm = db.tb_product.FirstOrDefault(p => p.product_serial_no.Equals(cart.product_serial_no.Value));
                    model.ProductType = ProdType.part_product;
                    model.product_size = pm.product_size_id.Value;
                }

                ap.Add(model);


                _price_sum += model.price * count;

            }
            try
            {
                if (stateID < 1)
                {
                    //error_msg = "";
                    throw new Exception("State is not exist");
                }
                if (ap.Count > 0)
                {
                    Account a = new Account(ap, stateID, db);

                    return a.getResult();

                }
                else
                {
                    return 0;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 计算单个商品的价格
        /// 
        /// </summary>
        /// <returns></returns>
        public decimal AccountChargeOne(ProdType product_type
            , int sku

            , int ShippingCompanyID
            , bool IsNoebook
            , int ShippingStateID
            , Data.nicklu2Entities db
            )
        {
            string error_msg = "Service not available.";

            List<AccountProduct> ap = new List<AccountProduct>();

            int _state_shipping = -1;
            decimal _price_sum = 0;
            //decimal _sale_tax = 0;
            //decimal result = 0;
            int count = 1;

            if (product_type == ProdType.part_product || product_type == ProdType.noebooks)
            {

                var prod = db.tb_product.FirstOrDefault(p => p.product_serial_no.Equals(sku));

                AccountProduct model = new AccountProduct();
                model.product_id = sku;
                model.shipping_company_id = ShippingCompanyID;
                model.price = prod.product_current_price.Value;
                model.sum = count;

                model.ProductType = IsNoebook ? ProdType.noebooks : ProdType.part_product;

                model.product_size = !IsNoebook ? prod.product_size_id.Value : SysProdSize.GetSize(model.price, ProdType.noebooks, db); ;

                ap.Add(model);
                _state_shipping = ShippingStateID;
                _price_sum += model.price * count;

            }
            if (product_type == ProdType.system_product)
            {
                // system 产品暂时不做


                //if (dr["product_serial_no"].ToString().Length == 8)
                //{
                //    model.product_cate = dr["is_noebook"].ToString() == "1" ? product_category.noebooks : product_category.system_product;
                //    model.product_size = AccountHelper.GetSystemSize(model.price, product_category.system_product);
                //}
            }
            try
            {
                if (ap.Count > 0)
                {
                    Account a = new Account(ap, ShippingStateID, db);
                    return a.getResult();
                }
            }
            catch (Exception em)
            {
                throw em;
            }
            throw new Exception(error_msg);
        }
    }
}
