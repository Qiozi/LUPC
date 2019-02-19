using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using LU.Data;

/// <summary>
/// Summary description for OrderHelper
/// </summary>
public class OrderHelper
{
    LU.Data.nicklu2Entities _context;

    public OrderHelper(nicklu2Entities context)
    {
        _context = context;
        //
        // TODO: Add constructor logic here
        //
    }
    public string FilterOrderCode(string order_code)
    {
        return "660" + order_code;
    }
    /// <summary>
    /// Copy part to order list.
    /// </summary>
    /// <param name="partID"></param>
    /// <param name="tag"></param>
    /// <param name="order_code"></param>
    /// <param name="error_string"></param>
    /// <returns></returns>
    public bool CopyProductToOrder(int partID, bool tag, string order_code, ref string error_string)
    {
        var pm = ProductModel.GetProductModel(_context, partID);
        if (pm == null)
        {
            error_string = "it is not exist!";
            return false;
        }
        else
        {
            if (pm.tag != byte.Parse(tag == true ? "1" : "0"))
            {
                error_string = "it is showit = 0";
                return false;
            }
            var pc = ProductCategoryModel.GetProductCategoryModel(_context, pm.menu_child_serial_no.Value);
            var order = new tb_order_product();
            order.menu_child_serial_no = pm.menu_child_serial_no;
            order.order_code = order_code;
            order.order_product_cost = pm.product_current_cost;
            order.order_product_price = pm.product_current_price;
            order.order_product_sum = 1;
            order.product_name = pm.product_name;
            order.product_serial_no = pm.product_serial_no;
            order.sku = pm.product_serial_no.ToString();
            order.order_product_sold = decimal.Parse(pm.product_current_price.ToString());
            order.tag = 1;
            order.menu_pre_serial_no = pm.menu_child_serial_no;
            order.product_type = Product_category_helper.product_category_value(pc.is_noebook == byte.Parse("1") ? product_category.noebooks : product_category.part_product);
            order.product_type_name = pc.is_noebook == byte.Parse("1") ? "Noebook" : "Unit";
            order.product_current_price_rate = pm.product_current_price;
            // order.Create();
            _context.tb_order_product.Add(order);
            _context.SaveChanges();
            return true;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="systemID"></param>
    /// <param name="order_code"></param>
    /// <returns></returns>
    public string CopySystemToOrderReturnNewCode(string systemID, string order_code)
    {
        string new_system_code = SpTmpDetailModel.GetNewCode(_context, int.Parse(systemID)).ToString();

        Config.ExecuteNonQuery(string.Format(@"insert into tb_sp_tmp 
	( sys_tmp_code, sys_tmp_price, create_datetime, tag, ip, system_templete_serial_no, 
	email, 
	system_category_serial_no, 
	is_noebook, 
	sys_tmp_cost, 
	sys_tmp_product_name, 
	save_price, 
	old_price, 
	is_old, 
	old_part_id, 
	syst_tmp_price_rate, 
	is_templete
	)
select '{1}', 
	 max(pp.sold)

, max(now()), max(tag), max(ip), max(spt.system_templete_serial_no), 
	max(email), 
	max(system_category_serial_no), 
	max(is_noebook), 
	max(pp.cost)
, 
	max(sys_tmp_product_name), 
	max(pp.discount), 
	max(pp.price), 
	max(is_old), 
	max(old_part_id), 
	max(pp.price), 
	max(is_templete)
	from tb_sp_tmp spt 
inner join (
		select sum((p.product_current_price*sp.part_quantity)-(p.product_current_discount*sp.part_quantity)) sold, max(sp.sys_tmp_code) sys_tmp_code
, sum(p.product_current_price*sp.part_quantity) price, sum(p.product_current_cost*sp.part_quantity) cost, sum(p.product_current_discount*sp.part_quantity) discount
	from tb_product p inner join tb_sp_tmp_detail sp on sp.product_serial_no=p.product_serial_no where sp.sys_tmp_code='{0}') pp on pp.sys_tmp_code=spt.sys_tmp_code
where spt.sys_tmp_code = '{0}';

insert into tb_system_code_store 
(  system_code, create_datetime, ip, old_system_code) 
values (  '{1}', now(), '', '{0}');

insert into tb_sp_tmp_detail 
	( sys_tmp_code, product_serial_no, product_current_price, product_current_cost, 
	product_order, 
	system_templete_serial_no, 
	system_product_serial_no, 
	part_group_id, 
	save_price, 
	old_price, 
	re_sys_tmp_detail, 
	product_current_price_rate, 
	product_current_sold
    ,part_quantity
    ,part_max_quantity
    ,product_name
    ,cate_name
,is_lock
,ebay_number
	)
	select 
	'{1}', sp.product_serial_no, p.product_current_price, p.product_current_cost, 
	sp.product_order, 
	sp.system_templete_serial_no, 
	sp.system_product_serial_no, 
	sp.part_group_id, 
	p.product_current_discount, 
	sp.old_price, 
	sp.re_sys_tmp_detail, 
	p.product_current_price, 
	p.product_current_price-p.product_current_discount
    ,sp.part_quantity
    ,sp.part_max_quantity
    ,sp.product_name
    ,sp.cate_name
,sp.is_lock
,sp.ebay_number
	from tb_sp_tmp_detail sp inner join tb_product p on p.product_serial_no = sp.product_serial_no where sys_tmp_code='{0}';
insert into tb_order_product_sys_detail
	( sys_tmp_code, product_serial_no, product_current_price, product_current_cost, 
	product_order, 
	system_templete_serial_no, 
	system_product_serial_no, 
	part_group_id, 
	save_price, 
	old_price, 
	re_sys_tmp_detail, 
	product_current_price_rate, 
	product_current_sold
    ,part_quantity
    ,part_max_quantity
    ,product_name
    ,cate_name
,is_lock
,ebay_number
	)
	select 
	sys_tmp_code,sp.product_serial_no, p.product_current_price, p.product_current_cost, 
	sp.product_order, 
	sp.system_templete_serial_no, 
	sp.system_product_serial_no, 
	sp.part_group_id, 
	p.product_current_discount, 
	sp.old_price, 
	sp.re_sys_tmp_detail, 
	p.product_current_price, 
	p.product_current_price-p.product_current_discount
    ,sp.part_quantity
    ,sp.part_max_quantity
    ,sp.product_name
    ,sp.cate_name
,sp.is_lock
,sp.ebay_number
	from tb_sp_tmp_detail sp inner join tb_product p on p.product_serial_no = sp.product_serial_no where sys_tmp_code='{1}';
", systemID, new_system_code));

        DataTable sp = SpTmpModel.GetModelsByTmpCodeNotConationSystemTemplete(new_system_code.ToString());
        if (sp.Rows.Count == 1)
        {
            DataRow dr = sp.Rows[0];
            var order = new tb_order_product();// OrderProductModel();
            //ProductModel p = ProductModel.GetProductModel(int.Parse(dr["product_serial_no"].ToString()));
            order.order_code = order_code;
            order.menu_pre_serial_no = int.Parse(dr["system_templete_serial_no"].ToString());
            order.order_product_cost = decimal.Parse(dr["sys_tmp_cost"].ToString());
            order.order_product_price = decimal.Parse(dr["old_price"].ToString());
            order.order_product_sum = 1;
            order.product_serial_no = int.Parse(new_system_code);
            order.sku = new_system_code.ToString();
            order.menu_child_serial_no = int.Parse(dr["system_templete_serial_no"].ToString());
            order.tag = sbyte.Parse("1");
            order.order_product_sold = decimal.Parse(dr["sys_tmp_price"].ToString());
            order.product_name = dr["sys_tmp_product_name"].ToString();
            order.product_type = Product_category_helper.product_category_value(product_category.system_product);
            order.product_current_price_rate = decimal.Parse(dr["sys_tmp_price"].ToString());
            order.product_type_name = "System";
            // order.Create();
            _context.tb_order_product.Add(order);
            _context.SaveChanges();
            return new_system_code;
        }
        else
        {

            throw new Exception("it didn't fold.");
        }
    }

    public bool CopySystemToOrder(string systemID, bool tag, string order_code, DataTable partGroup, CountryCategory cc, ref string error_string)
    {
        // 8 sku
        if (systemID.Length == Config.system_product_sku_length)
        {
            #region sys length 8
            if (Config.ExecuteScalarInt32("select count(serial_no) c from tb_order_product where product_serial_no='" + systemID + "'") > 0)
            {
                throw new Exception("请用‘Duplicate Sys To New’按钮");
            }
            DataTable sp = SpTmpModel.GetModelsByTmpCodeNotConationSystemTemplete(systemID.ToString());

            if (sp.Rows.Count == 1)
            {

                DataRow dr = sp.Rows[0];
                var op = new tb_order_product();// OrderProductModel();

                op.order_code = order_code;
                op.menu_pre_serial_no = int.Parse(dr["system_templete_serial_no"].ToString());

                op.order_product_sum = 1;
                op.product_serial_no = int.Parse(systemID);
                op.menu_child_serial_no = int.Parse(dr["system_templete_serial_no"].ToString());
                op.tag = 1;
                op.sku = systemID;
                op.product_name = dr["sys_tmp_product_name"].ToString();
                op.product_type = Product_category_helper.product_category_value(product_category.system_product);
                op.product_type_name = "System";

                decimal _system_price = 0M;
                decimal _system_cost = 0M;

                var sdm = SpTmpDetailModel.GetModelsBySysTmpCode(_context, systemID);
                for (int x = 0; x < sdm.Length; x++)
                {

                    var m = sdm[x];
                    var pm = ProductModel.GetProductModel(_context, m.product_serial_no.Value);
                    var opsdm = new tb_order_product_sys_detail();// OrderProductSysDetailModel();
                    opsdm.cate_name = m.cate_name;
                    opsdm.ebay_number = m.ebay_number;
                    opsdm.is_lock = m.is_lock;
                    opsdm.old_price = m.old_price;
                    opsdm.part_group_id = m.part_group_id;
                    opsdm.part_max_quantity = m.part_max_quantity;
                    opsdm.part_quantity = m.part_quantity;
                    opsdm.product_current_cost = pm.product_current_cost;
                    opsdm.product_current_price = pm.product_current_price - ProductModel.FindOnSaleDiscountByPID(pm.product_serial_no);
                    opsdm.product_current_price_rate = m.product_current_price_rate;
                    opsdm.product_current_sold = pm.product_current_price - ProductModel.FindOnSaleDiscountByPID(pm.product_serial_no);
                    opsdm.product_name = m.product_name;
                    opsdm.product_order = m.product_order;
                    opsdm.product_serial_no = m.product_serial_no;
                    opsdm.re_sys_tmp_detail = m.re_sys_tmp_detail;
                    opsdm.save_price = m.save_price;
                    opsdm.sys_tmp_code = m.sys_tmp_code;
                    opsdm.system_product_serial_no = m.system_product_serial_no;
                    opsdm.system_templete_serial_no = m.system_templete_serial_no;
                    // opsdm.Create();
                    _context.tb_order_product_sys_detail.Add(opsdm);
                    _context.SaveChanges();
                    _system_price += (pm.product_current_price.Value - ProductModel.FindOnSaleDiscountByPID(pm.product_serial_no)) * m.part_quantity.Value;
                    _system_cost += (pm.product_current_cost.Value) * m.part_quantity.Value;

                }
                op.order_product_cost = ConvertPrice.Price(cc, _system_cost);
                op.order_product_price = ConvertPrice.Price(cc, _system_price);
                op.order_product_sold = ConvertPrice.Price(cc, _system_price);
                op.product_current_price_rate = ConvertPrice.Price(cc, _system_price);
                _context.tb_order_product.Add(op);
                _context.SaveChanges();
                /// op.Create();
                return true;
            }
            else
            {
                error_string = "it didn't fold.";
                return false;
            }
            //  
            #endregion
        }
        else
        {
            var st = EbaySystemModel.GetEbaySystemModel(_context, int.Parse(systemID));

            if (st == null)
            {
                error_string = "it didn't fold.";
                return false;
            }
            var ms = _context.tb_ebay_system_parts.Where(me => me.system_sku.Value.Equals(systemID)).ToList();// EbaySystemPartsModel.FindAllByProperty(_context, "system_sku", systemID);

            string system_tmp_sku = SpTmpDetailModel.GetNewCode(_context, int.Parse(systemID)).ToString();
            decimal _system_price = 0M;
            decimal _system_cost = 0M;
            int m = 0;
            for (int j = 0; j < ms.Count; j++)
            {

                var p = ProductModel.GetProductModel(_context, ms[j].luc_sku.Value);
                SetProductToSystemDetail(int.Parse(system_tmp_sku), int.Parse(systemID), p, ms[j], partGroup, cc);

                _system_price += (p.product_current_price.Value - ProductModel.FindOnSaleDiscountByPID(p.product_serial_no)) * ms[j].part_quantity.Value;
                _system_cost += (p.product_current_cost.Value) * ms[j].part_quantity.Value;
                m += 1;

            }
            error_string = ms.Count.ToString();
            var opm = new tb_order_product();// OrderProductModel();
            // ProductModel product= ProductModel.GetProductModel(model[j].product_serial_no);
            //ProductCategoryModel  cate = ProductCategoryModel.GetProductCategoryModel(product.menu_child_serial_no);
            opm.order_code = order_code;
            opm.menu_pre_serial_no = EbaySystemAndCategoryModel.GetSystemCategoryId(_context, st.id);
            opm.order_product_cost = ConvertPrice.Price(cc, _system_cost);
            opm.order_product_price = ConvertPrice.Price(cc, _system_price);
            opm.order_product_sold = ConvertPrice.Price(cc, _system_price);
            opm.order_product_sum = 1;
            opm.product_serial_no = int.Parse(system_tmp_sku);
            opm.menu_child_serial_no = int.Parse(systemID);
            opm.tag = 1;
            opm.sku = systemID;
            opm.product_name = st.ebay_system_name;
            opm.product_type = Product_category_helper.product_category_value(product_category.system_product);
            opm.product_type_name = "System";
            opm.product_current_price_rate = ConvertPrice.Price(cc, _system_price);
            _context.tb_order_product.Add(opm);
            _context.SaveChanges();
            // opm.Create();

            try
            {
                var sp = new tb_sp_tmp();// SpTmpModel();
                sp.create_datetime = DateTime.Now;
                sp.sys_tmp_code = system_tmp_sku;
                sp.sys_tmp_price = ConvertPrice.Price(cc, _system_price);
                sp.system_category_serial_no = (sbyte)EbaySystemAndCategoryModel.GetSystemCategoryId(_context, st.id);
                sp.system_templete_serial_no = int.Parse(systemID);
                sp.tag = 1;
                _context.tb_sp_tmp.Add(sp);
                _context.SaveChanges();
                //sp.Create();
            }
            catch { }
            //
            // store SystemCode
            //SystemCodeStoreModel scs = new SystemCodeStoreModel();
            //scs.create_datetime = DateTime.Now;
            //scs.ip = "-1";
            //scs.is_buy = true;
            //scs.system_code = int.Parse(system_tmp_sku);
            //scs.system_templete_serial_no = int.Parse(systemID);
            //scs.Create();

            return true;
        }
    }

    /// <summary>
    /// 把产品插入system 产品明细表中
    /// </summary>
    /// <param name="system_code"></param>
    /// <param name="product_id"></param>
    private void SetProductToSystemDetail(int system_code, int system_templete_serial_no, tb_product p, tb_ebay_system_parts spm, DataTable partGroup, CountryCategory cc)
    {
        // SpDetailModel m = new SpDetailModel();
        var m = new tb_sp_tmp_detail();// SpTmpDetailModel();

        //ProductCategoryModel pc = ProductCategoryModel.GetProductCategoryModel(p.menu_child_serial_no);
        m.product_current_cost = ConvertPrice.Price(cc, p.product_current_cost.Value);
        m.product_current_price = ConvertPrice.Price(cc, p.product_current_price.Value);
        m.product_order = spm.id;
        m.product_serial_no = p.product_serial_no;
        m.product_current_sold = ConvertPrice.Price(cc, p.product_current_price.Value - ProductModel.FindOnSaleDiscountByPID(p.product_serial_no));
        m.sys_tmp_code = system_code.ToString();
        m.system_templete_serial_no = system_templete_serial_no;
        m.system_product_serial_no = spm.id;
        m.part_quantity = spm.part_quantity;
        m.part_max_quantity = spm.max_quantity;
        m.product_name = p.product_name_long_en != "" ? p.product_name_long_en : p.product_name;

        for (int i = 0; i < partGroup.Rows.Count; i++)
        {
            DataRow dr = partGroup.Rows[i];
            if (dr["part_group_id"].ToString() == spm.part_group_id.ToString())
                m.cate_name = dr["part_group_name"].ToString();
        }
        _context.tb_sp_tmp_detail.Add(m);
        _context.SaveChanges();
        // m.Create();

        var opsdm = new tb_order_product_sys_detail();// OrderProductSysDetailModel();
        opsdm.cate_name = m.cate_name;
        opsdm.ebay_number = m.ebay_number;
        opsdm.is_lock = m.is_lock;
        opsdm.old_price = m.old_price;
        opsdm.part_group_id = m.part_group_id;
        opsdm.part_max_quantity = m.part_max_quantity;
        opsdm.part_quantity = m.part_quantity;
        opsdm.product_current_cost = m.product_current_cost;
        opsdm.product_current_price = m.product_current_price;
        opsdm.product_current_price_rate = m.product_current_price_rate;
        opsdm.product_current_sold = m.product_current_sold;
        opsdm.product_name = m.product_name;
        opsdm.product_order = m.product_order;
        opsdm.product_serial_no = m.product_serial_no;
        opsdm.re_sys_tmp_detail = m.re_sys_tmp_detail;
        opsdm.save_price = m.save_price;
        opsdm.sys_tmp_code = m.sys_tmp_code;
        opsdm.system_product_serial_no = m.system_product_serial_no;
        opsdm.system_templete_serial_no = m.system_templete_serial_no;
        // opsdm.Create();
        _context.tb_order_product_sys_detail.Add(opsdm);
        _context.SaveChanges();

    }


    public bool CopyProductToHistoryStore(tb_order_product opm, bool is_add, ref string error)
    {
        try
        {
            var ophm = new tb_order_product_history();// OrderProductHistoryModel();
            ophm.add_del = is_add;
            ophm.create_datetime = DateTime.Now;
            ophm.menu_child_serial_no = opm.menu_child_serial_no;
            ophm.menu_pre_serial_no = opm.menu_pre_serial_no;
            ophm.order_code = opm.order_code;
            ophm.order_product_cost = opm.order_product_cost;
            ophm.order_product_price = opm.order_product_price;
            ophm.order_product_sold = opm.order_product_sold;
            ophm.order_product_sum = opm.order_product_sum;
            ophm.product_current_price_rate = opm.product_current_price_rate;
            ophm.product_name = opm.product_name;
            ophm.product_serial_no = opm.product_serial_no;
            ophm.product_type = opm.product_type;
            ophm.product_type_name = opm.product_type_name;
            ophm.sku = opm.sku;
            ophm.tag = opm.tag;

            // ophm.Create();
            _context.tb_order_product_history.Add(ophm);
            _context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            error = ex.Message;
            return false;
        }
    }

    public bool CopyProductToHistoryStore(tb_product pm, tb_order_product_sys_detail m, string order_code, int sum, bool is_add, ref string error)
    {
        try
        {
            var ophm = new tb_order_product_history();// OrderProductHistoryModel();

            ophm.add_del = is_add;
            ophm.create_datetime = DateTime.Now;
            ophm.menu_child_serial_no = pm.menu_child_serial_no;
            ophm.menu_pre_serial_no = pm.menu_child_serial_no;
            ophm.order_code = order_code;
            ophm.order_product_cost = m.product_current_cost;
            ophm.order_product_price = m.product_current_price;
            ophm.order_product_sold = m.product_current_sold;
            ophm.order_product_sum = sum * m.part_quantity;
            ophm.product_current_price_rate = m.product_current_price;
            ophm.product_name = m.product_name;
            ophm.product_serial_no = pm.product_serial_no; ;
            ophm.product_type = Product_category_helper.product_category_value(product_category.part_product);
            ophm.product_type_name = "Unit";
            ophm.sku = pm.product_serial_no.ToString();
            ophm.tag = pm.tag.Value;
            //ophm.Create();
            _context.tb_order_product_history.Add(ophm);
            _context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            error = ex.Message;
            return false;
        }
    }


    public int CreateNewDefaultOrder(int customerID)
    {
        return CreateNewDefaultOrder(customerID, false);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public int CreateNewDefaultOrder(int customerID, bool IsPaypal)
    {
        //int  = 8888;

        int OrderCode = OrderHelperModel.GetNewOrderCode(_context);
        var ohm = new tb_order_helper();// OrderHelperModel();
        ohm.order_code = OrderCode;
        ohm.customer_serial_no = customerID;
        ohm.create_datetime = DateTime.Now;
        ohm.order_date = DateTime.Now;
        ohm.tag = 1;
        ohm.out_status = sbyte.Parse(Config.DefaultOutStatus.ToString());
        ohm.pre_status_serial_no = int.Parse(Config.new_order_status);
        ohm.is_ok = true;
        ohm.pay_method = IsPaypal ? "15" : Config.pay_method_pick_up_id_default.ToString();
        ohm.order_source = 2;
        ohm.price_unit = "CAD";
        ohm.current_system = 1;
        ohm.shipping_company = -1;
        ohm.Is_Modify = true;
        _context.tb_order_helper.Add(ohm);
        _context.SaveChanges();
        //ohm.Create();
        var ch = new CustomerHelper(_context);
        ch.CopyCustomer(OrderCode.ToString(), customerID);


        var oh = OrderHelperModel.GetModelsByOrderCode(_context, OrderCode);
        var csm = CustomerStoreModel.FindModelsByOrderCode(_context, OrderCode.ToString());

        for (int i = 0; i < oh.Length; i++)
        {
            oh[i].pay_method = IsPaypal ? "15" : Config.pay_method_pick_up_id_default.ToString();
            // oh[i].Update


            csm[0].pay_method = IsPaypal ? 15 : Config.pay_method_pick_up_id_default;
            //csm[0].Update();
            _context.SaveChanges();
        }
        return OrderCode;
    }

}
