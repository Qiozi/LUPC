// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2007-6-20 20:33:39
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;
using MySql.Data;
using System.Data;
using MySql.Data.MySqlClient;

[ActiveRecord("tb_order_helper")]
[Serializable]
public class OrderHelperModel : ActiveRecordBase<OrderHelperModel>
{

    public OrderHelperModel()
    {

    }

    public static OrderHelperModel GetOrderHelperModel(int _order_helper_serial_no)
    {
        OrderHelperModel[] models = OrderHelperModel.FindAllByProperty("order_helper_serial_no", _order_helper_serial_no);
        if (models.Length == 1)
            return models[0];
        else
            return new OrderHelperModel();
    }

    int _order_helper_serial_no;

    [PrimaryKey(PrimaryKeyType.Identity)]
    public int order_helper_serial_no
    {
        get { return _order_helper_serial_no; }
        set { _order_helper_serial_no = value; }
    }

    int _order_code;

    [Property]
    public int order_code
    {
        get { return _order_code; }
        set { _order_code = value; }
    }

    int _customer_serial_no;

    [Property]
    public int customer_serial_no
    {
        get { return _customer_serial_no; }
        set { _customer_serial_no = value; }
    }

    decimal _sub_total;

    [Property]
    public decimal sub_total
    {
        get { return _sub_total; }
        set { _sub_total = value; }
    }

    decimal _discount;

    [Property]
    public decimal discount
    {
        get { return _discount; }
        set { _discount = value; }
    }

    decimal _total;

    [Property]
    public decimal total
    {
        get { return _total; }
        set { _total = value; }
    }

    DateTime _ready_date;

    [Property]
    public DateTime ready_date
    {
        get { return _ready_date; }
        set { _ready_date = value; }
    }

    int _rush;

    [Property]
    public int rush
    {
        get { return _rush; }
        set { _rush = value; }
    }

    string _pay_method;

    [Property]
    public string pay_method
    {
        get { return _pay_method; }
        set { _pay_method = value; }
    }

    DateTime _order_date;

    [Property]
    public DateTime order_date
    {
        get { return _order_date; }
        set { _order_date = value; }
    }

    int _system_category_serial_no;

    [Property]
    public int system_category_serial_no
    {
        get { return _system_category_serial_no; }
        set { _system_category_serial_no = value; }
    }

    DateTime _create_datetime;

    [Property]
    public DateTime create_datetime
    {
        get { return _create_datetime; }
        set { _create_datetime = value; }
    }

    string _note;

    [Property]
    public string note
    {
        get { return _note; }
        set { _note = value; }
    }

    int _out_status;

    [Property]
    public int out_status
    {
        get { return _out_status; }
        set { _out_status = value; }
    }

    decimal _tax_charge;

    [Property]
    public decimal tax_charge
    {
        get { return _tax_charge; }
        set { _tax_charge = value; }
    }

    decimal _cost;

    [Property]
    public decimal cost
    {
        get { return _cost; }
        set { _cost = value; }
    }

    int _is_pay_end;

    [Property]
    public int is_pay_end
    {
        get { return _is_pay_end; }
        set { _is_pay_end = value; }
    }

    int _pre_status_serial_no;

    [Property]
    public int pre_status_serial_no
    {
        get { return _pre_status_serial_no; }
        set { _pre_status_serial_no = value; }
    }

    DateTime _prick_up_datetime1;

    [Property]
    public DateTime prick_up_datetime1
    {
        get { return _prick_up_datetime1; }
        set { _prick_up_datetime1 = value; }
    }

    DateTime _prick_up_datetime2;

    [Property]
    public DateTime prick_up_datetime2
    {
        get { return _prick_up_datetime2; }
        set { _prick_up_datetime2 = value; }
    }

    int _shipping_company;

    [Property]
    public int shipping_company
    {
        get { return _shipping_company; }
        set { _shipping_company = value; }
    }

    int _tag;

    [Property]
    public int tag
    {
        get { return _tag; }
        set { _tag = value; }
    }

    decimal _shipping_charge;

    [Property]
    public decimal shipping_charge
    {
        get { return _shipping_charge; }
        set { _shipping_charge = value; }
    }

    string _Msg_from_Seller;

    [Property]
    public string Msg_from_Seller
    {
        get { return _Msg_from_Seller; }
        set { _Msg_from_Seller = value; }
    }

    int _call_me;

    [Property]
    public int call_me
    {
        get { return _call_me; }
        set { _call_me = value; }
    }

    string _out_note;

    [Property]
    public string out_note
    {
        get { return _out_note; }
        set { _out_note = value; }
    }

    int _tax_rate;

    [Property]
    public int tax_rate
    {
        get { return _tax_rate; }
        set { _tax_rate = value; }
    }

    int _is_old;

    [Property]
    public int is_old
    {
        get { return _is_old; }
        set { _is_old = value; }
    }

    decimal _gst_rate;

    [Property]
    public decimal gst_rate
    {
        get { return _gst_rate; }
        set { _gst_rate = value; }
    }

    decimal _pst_rate;

    [Property]
    public decimal pst_rate
    {
        get { return _pst_rate; }
        set { _pst_rate = value; }
    }

    decimal _hst_rate;

    [Property]
    public decimal hst_rate
    {
        get { return _hst_rate; }
        set { _hst_rate = value; }
    }

    decimal _sur_charge_rate;

    [Property]
    public decimal sur_charge_rate
    {
        get { return _sur_charge_rate; }
        set { _sur_charge_rate = value; }
    }

    decimal _sur_charge;

    [Property]
    public decimal sur_charge
    {
        get { return _sur_charge; }
        set { _sur_charge = value; }
    }

    decimal _gst;

    [Property]
    public decimal gst
    {
        get { return _gst; }
        set { _gst = value; }
    }

    decimal _pst;

    [Property]
    public decimal pst
    {
        get { return _pst; }
        set { _pst = value; }
    }

    decimal _hst;

    [Property]
    public decimal hst
    {
        get { return _hst; }
        set { _hst = value; }
    }

    decimal _sub_total_rate;

    [Property]
    public decimal sub_total_rate
    {
        get { return _sub_total_rate; }
        set { _sub_total_rate = value; }
    }

    decimal _total_rate;

    [Property]
    public decimal total_rate
    {
        get { return _total_rate; }
        set { _total_rate = value; }
    }

    decimal _grand_total;

    [Property]
    public decimal grand_total
    {
        get { return _grand_total; }
        set { _grand_total = value; }
    }

    bool _is_ok;

    [Property]
    public bool is_ok
    {
        get { return _is_ok; }
        set { _is_ok = value; }
    }

    decimal _taxable_total;

    [Property]
    public decimal taxable_total
    {
        get { return _taxable_total; }
        set { _taxable_total = value; }
    }

    int _tax_export;

    [Property]
    public int tax_export
    {
        get { return _tax_export; }
        set { _tax_export = value; }
    }

    int _order_pay_status_id;

    [Property]
    public int order_pay_status_id
    {
        get { return _order_pay_status_id; }
        set { _order_pay_status_id = value; }
    }

    string _order_invoice;

    [Property]
    public string order_invoice
    {
        get { return _order_invoice; }
        set { _order_invoice = value; }
    }

    bool _is_download_invoice;

    [Property]
    public bool is_download_invoice
    {
        get { return _is_download_invoice; }
        set { _is_download_invoice = value; }
    }

    decimal _input_order_discount;

    [Property]
    public decimal input_order_discount
    {
        get { return _input_order_discount; }
        set { _input_order_discount = value; }
    }

    bool _is_lock_input_order_discount;

    [Property]
    public bool is_lock_input_order_discount
    {
        get { return _is_lock_input_order_discount; }
        set { _is_lock_input_order_discount = value; }
    }

    bool _is_lock_shipping_charge;

    [Property]
    public bool is_lock_shipping_charge
    {
        get { return _is_lock_shipping_charge; }
        set { _is_lock_shipping_charge = value; }
    }

    int _is_send_email;

    [Property]
    public int is_send_email
    {
        get { return _is_send_email; }
        set { _is_send_email = value; }
    }

    int _order_source;

    [Property]
    public int order_source
    {
        get { return _order_source; }
        set { _order_source = value; }
    }

    decimal _weee_charge;

    [Property]
    public decimal weee_charge
    {
        get { return _weee_charge; }
        set { _weee_charge = value; }
    }

    string _price_unit;

    [Property]
    public string price_unit
    {
        get { return _price_unit; }
        set { _price_unit = value; }
    }

    string _current_system;

    [Property]
    public string current_system
    {
        get { return _current_system; }
        set { _current_system = value; }
    }

    bool _Is_Modify;

    [Property]
    public bool Is_Modify
    {
        get { return _Is_Modify; }
        set { _Is_Modify = value; }
    }

    bool _is_lock_tax_change;
    [Property]
    public bool is_lock_tax_change
    {
        get { return _is_lock_tax_change; }
        set { _is_lock_tax_change = value; }
    }

    public static int GetNewOrderCode()
    {
        //int code = Config.sixCode;
        //if(!GetNewOrderCode(code))
        //{
        //    return GetNewOrderCode();
        //}
        //Config.ExecuteNonQuery("insert into tb_order_code (order_code, regdate, is_order)	values	( '" + code.ToString() + "', now(), 1)");
        //return code;
        int code = Code.NewOrderCode();
        Config.ExecuteNonQuery("insert into tb_order_code (order_code, regdate, is_order)	values	( '" + code.ToString() + "', now(), 1)");
        return code;
    }


    public static OrderHelperModel[] GetModelsByOrderCode(int order_code)
    {
        return OrderHelperModel.FindAllByProperty("order_code", order_code);
    }

    public static OrderHelperModel GetModelByOrderCode(int order_code)
    {
        OrderHelperModel[] ms = OrderHelperModel.FindAllByProperty("order_code", order_code);
        if (ms != null)
            return ms[0];
        return null;
    }

    public static DataTable GetModelsDTByOrderCode(int order_code)
    {
        return Config.ExecuteDataTable("select * from tb_order_helper where order_code =" + order_code);
    }

    public static DataTable GetModelsBySearch(string order_code, string first_name, string email, string date, Showit showit, int pre_status, int back_status)
    {
        string sql = "select * from tb_order_helper where ( 1=1";

        if (order_code != "")
            sql += " or order_code like '%" + order_code + "%'";
        if (first_name != "")
            sql += " or first_name like '%" + first_name + "%' or last_name like '%" + first_name + "%'";
        if (email != "")
            sql += " or email like '%" + email + "%' ";
        if (date != "")
            sql += " or order_date  like '%" + date + "%'";

        sql += " ) ";

        if (pre_status != -1)
            sql += " and pre_status_serial_no=" + pre_status;

        if (back_status != -1)
            sql += " and out_status=" + back_status;

        if (showit != Showit.all)
            sql += " and tag= " + (Showit.show_true == showit ? 1 : 0);
        sql += " order by order_helper_serial_no desc ";
        return Config.ExecuteDataTable(sql);
    }

    public static DataTable GetModelsBySearch(string keyword, Showit showit, int pre_status, int back_status, int top_count, string shipping_first_name, string shipping_lost_name, ref int count)
    {
        string date_sql = "";
        string limit_sql = "";
        if (top_count == -1)
        {
            date_sql = " and date_format(oh.create_datetime,'%d-%b-%y')=date_format(date_sub(current_date, interval 0 day),'%d-%b-%y')";
        }
        else if (top_count == -2)
        {
            date_sql = @" and (date_format(oh.create_datetime,'%d-%b-%y')=date_format(date_sub(current_date, interval 0 day),'%d-%b-%y')  
or 
 date_format(oh.create_datetime,'%d-%b-%y') =date_format(date_sub(current_date, interval 1 day), '%d-%b-%y'))  ";

        }
        else
        {
            limit_sql = " limit 0," + top_count.ToString();
        }


        string sql_row = @"select oh.order_helper_serial_no
,oh.order_code
,cs.customer_serial_no ,
(select state_name from tb_state_shipping ss where ss.state_serial_no=cs.customer_shipping_state) customer_shipping_state,
(select pay_method_short_name from tb_pay_method_new pmn where pmn.pay_method_serial_no=cs.pay_method) pay_method,
oh.tag,
Concat(cs.customer_shipping_first_name ,' ',cs.customer_shipping_last_name) name,
oh.create_datetime order_date,
oh.out_status,
oh.grand_total,
oh.out_note,
oh.pre_status_serial_no from tb_order_helper oh inner join tb_customer_store cs on cs.order_code=oh.order_code where 1=1";

        string sql = "";
        if (keyword != "")
        {
            sql += " and (";
            sql += " oh.order_code = '" + keyword + "'";
            sql += " or oh.order_helper_serial_no='" + keyword + "'";
            sql += " or cs.customer_serial_no='" + keyword + "'";
            sql += " ) ";
        }
        if (pre_status != -1)
            sql += " and oh.pre_status_serial_no=" + pre_status;

        if (back_status != -1)
            sql += " and oh.out_status=" + back_status;

        if (showit != Showit.all)
            sql += " and oh.tag= " + (Showit.show_true == showit ? 1 : 0);
        if (date_sql != "")
            sql += date_sql;
        if (shipping_first_name != "")
            sql += " and cs.customer_shipping_first_name like ('%" + shipping_first_name + "%')";
        if (shipping_lost_name != "")
            sql += " and cs.customer_shipping_last_name like ('%" + shipping_lost_name + "%')";
        sql += " order by order_helper_serial_no desc " + limit_sql;

        //
        // get count
        //
        object o = Config.ExecuteScalar("select count(oh.order_helper_serial_no) from tb_order_helper oh inner join tb_customer_store cs on cs.order_code=oh.order_code where 1=1 and oh.tag=1");
        if (o != null)
        {
            int.TryParse(o.ToString(), out count);
        }
        //throw new Exception(sql_row + sql);
        return Config.ExecuteDataTable(sql_row + sql);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="keyword"></param>
    /// <param name="field_name"></param>
    /// <param name="pre_status_serial_no"></param>
    /// <param name="pay_method_id"></param>
    /// <param name="top_count"></param>
    /// <param name="startIndex"></param>
    /// <param name="endIndex"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public static DataTable GetModelsBySearch(string keyword, string field_name, string pre_status_serial_no, int pay_method_id, int top_count, int startIndex, int endIndex, ref int count)
    {
        //(select state_name from tb_state_shipping ss where ss.state_serial_no=cs.customer_shipping_state) customer_shipping_state,
        string date_sql = "";
        string limit_sql = "";
        if (top_count == -1)
        {
            date_sql = " and date_format(oh.create_datetime,'%d-%b-%y')=date_format(date_sub(current_date, interval 0 day),'%d-%b-%y')";
        }
        else if (top_count == -2)
        {
            date_sql = @" and (date_format(oh.create_datetime,'%d-%b-%y')=date_format(date_sub(current_date, interval 0 day),'%d-%b-%y')  
or 
 date_format(oh.create_datetime,'%d-%b-%y') =date_format(date_sub(current_date, interval 1 day), '%d-%b-%y'))  ";

        }
        else
        {
            limit_sql = string.Format(" limit {0},{1}", startIndex, top_count);// +top_count.ToString();
        }

        string sql_count = @"select count(oh.order_helper_serial_no) from tb_order_helper oh inner join tb_customer_store cs on cs.order_code=oh.order_code
left join tb_pre_status ps on ps.pre_status_serial_no=oh.pre_status_serial_no
left join tb_facture_state fs on fs.facture_state_serial_no=oh.out_status
where 1=1 and oh.tag=1 and oh.is_ok=1 ";
        string sql_row = @"select oh.order_helper_serial_no
,oh.order_code
,cs.customer_serial_no ,

(select pay_method_short_name from tb_pay_method_new pmn where pmn.pay_method_serial_no=cs.pay_method) pay_method,
oh.tag,
Concat(cs.customer_shipping_first_name ,' ',cs.customer_shipping_last_name) name,
date_format(oh.create_datetime, '%b/%d/%Y') order_date,
oh.out_status,
fs.facture_state_name,
fs.back_color fs_back_color,
ps.pre_status_name,
ps.back_color pre_back_color,
oh.grand_total,
oh.out_note,

oh.pre_status_serial_no from tb_order_helper oh inner join tb_customer_store cs on cs.order_code=oh.order_code
left join tb_pre_status ps on ps.pre_status_serial_no=oh.pre_status_serial_no
left join tb_facture_state fs on fs.facture_state_serial_no=oh.out_status
where 1=1 and oh.tag=1 and oh.is_ok=1 ";

        string sql = "";
        if (keyword != "" && field_name != "")
        {
            sql += " and (" + field_name + "='" + keyword + "'";
            //sql += " oh.order_code = '" + keyword + "'";
            //sql += " or oh.order_helper_serial_no='" + keyword + "'";
            //sql += " or cs.customer_serial_no='" + keyword + "'";
            sql += " ) ";
        }
        //if (pre_status != -1)
        //    sql += " and oh.pre_status_serial_no=" + pre_status;

        if (pre_status_serial_no != "-1")
        {
            if (pre_status_serial_no == Config.porder_order_status)
            {
                sql += " and oh.pre_status_serial_no not in (" + pre_status_serial_no + ")";
            }
            else
                sql += " and oh.pre_status_serial_no=" + pre_status_serial_no;
        }
        //if (showit != Showit.all)
        //    sql += " and oh.tag= " + (Showit.show_true == showit ? 1 : 0);
        if (date_sql != "")
            sql += date_sql;
        if (pay_method_id > 0)
            sql += " and cs.pay_method='" + pay_method_id + "'";
        //if (shipping_first_name != "")
        //    sql += " and cs.customer_shipping_first_name like ('%" + shipping_first_name + "%')";
        //if (shipping_lost_name != "")
        //    sql += " and cs.customer_shipping_last_name like ('%" + shipping_lost_name + "%')";

        object o = Config.ExecuteScalar(sql_count + sql);
        if (o != null)
        {
            int.TryParse(o.ToString(), out count);
        }
        sql += " order by order_helper_serial_no desc " + limit_sql;

        // throw new Exception(sql_row + sql);

        return Config.ExecuteDataTable(sql_row + sql);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="keyword"></param>
    /// <param name="field_name"></param>
    /// <param name="pre_status_serial_no"></param>
    /// <param name="pay_method_id"></param>
    /// <param name="top_count"></param>
    /// <param name="startIndex"></param>
    /// <param name="endIndex"></param>
    /// <param name="assdinged_to_id"></param>
    /// <param name="order_source"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public static DataTable GetModelsBySearch2(string keyword, string field_name
       , string pre_status_serial_no, int pay_method_id
       , int top_count, int startIndex, int endIndex
       , int assdinged_to_id, string order_source, ref int count)
    {
        return GetModelsBySearch2(keyword, field_name
       , pre_status_serial_no, pay_method_id
       , top_count, startIndex, endIndex
       , assdinged_to_id, order_source, false, ref count);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="keyword"></param>
    /// <param name="field_name"></param>
    /// <param name="pre_status_serial_no"></param>
    /// <param name="pay_method_id"></param>
    /// <param name="top_count"></param>
    /// <param name="startIndex"></param>
    /// <param name="endIndex"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public static DataTable GetModelsBySearch2(string keyword, string field_name
        , string pre_status_serial_no, int pay_method_id
        , int top_count, int startIndex, int endIndex
        , int assdinged_to_id, string order_source, bool isGetCount, ref int count)
    {

        //return null;
        //string date_sql = "";

        string limit_sql_OH = string.Format(" order by order_date desc limit {0},{1}", startIndex, top_count);
        string limit_sql_CS = string.Format(" order by serial_no desc limit {0},{1}", startIndex, top_count);

        string sql_LU_Sku = "";
        string sql_count = "";
        //string sql_row = "";
        string ohWhereSql = "";
        string csWhereSQL = "";
        string sql = string.Format(@"
SET SQL_BIG_SELECTS=1;
select 
cs.customer_shipping_city
,cs.customer_shipping_address
,cs.shipping_state_code
,cs.shipping_country_code
,cs.customer_shipping_zip_code
, Concat(cs.customer_first_name ,' ',cs.customer_last_name) CustomerName
,cs.phone_d
,cs.phone_n
,cs.phone_c
,cs.customer_serial_no
,order_pay_status_id
,order_source
,(select pay_method_short_name from tb_pay_method_new pmn where pmn.pay_method_serial_no=cs.pay_method) pay_method
, Concat(cs.customer_shipping_first_name ,' ',cs.customer_shipping_last_name) name
,fs.facture_state_name
,fs.back_color fs_back_color
,ps.pre_status_name
,ps.back_color pre_back_color
,case when oh.order_source=3 then oh.is_send_email else -1 end as is_send_email
,oh.price_unit
,oh.order_helper_serial_no
,oh.order_pay_status_id
,oh.order_source
,oh.order_code
,oh.tag
,date_format(oh.order_date, '%b/%d/%Y') order_date
,oh.out_status
,oh.pre_status_serial_no
,oh.grand_total
,oh.out_note
,is_ok
,oh.order_invoice
,ifnull((select  assigned_to_staff_name from tb_order_assigned_to ass  where ass.order_code=oh.order_code order by assigned_to_id desc limit 0,1 ) , 'None') as assigned_to_staff_name
,date_format((select regdate from tb_order_ups_tracking_number osd where osd.order_code=oh.order_code order by id desc limit 0,1), '%b/%d/%Y') shipping_date 
,oh.grand_total- (select ifnull(sum(pay_cash), 0) from tb_order_pay_record where order_code=oh.order_code) balance
,(select count(msg_id) from tb_chat_msg where msg_order_code = oh.order_code) msgCount
,(select count(id) from tb_order_notepad where orderCode=oh.order_code) notepadCount
");



        sql_count = string.Format(@"select 
count(oh.order_helper_serial_no)
 from tb_order_helper oh
inner join tb_customer_store cs on cs.order_code=oh.order_code
left join tb_pre_status ps on ps.pre_status_serial_no=oh.pre_status_serial_no
left join tb_facture_state fs on fs.facture_state_serial_no=oh.out_status
where  oh.order_source in (" + order_source + ")  and oh.tag=1 and oh.is_ok=1");

        if (field_name.IndexOf("order_code") != -1)
        {
            #region order code 
            ohWhereSql = " and order_code='" + keyword + "'";
            #endregion
        }
        else if (field_name.IndexOf("customer_serial_no") != -1)
        {
            #region customer serial no
            csWhereSQL = " and customer_serial_no='" + keyword + "'";
            #endregion
        }
        else if (field_name.IndexOf("order_invoice") != -1)
        {
            #region order invoice 
            ohWhereSql = " and order_invoice='" + keyword + "'";
            #endregion
        }
        else if (field_name.IndexOf("customer_shipping_first_name") != -1)
        {
            #region first name
            csWhereSQL = string.Format(" and (customer_shipping_first_name='{0}'  or customer_first_name='{0}' or customer_card_first_name='{0}') ", keyword);

            #endregion
        }
        else if (field_name.IndexOf("customer_shipping_last_name") != -1)
        {
            #region last name
            csWhereSQL = string.Format(" and (customer_shipping_last_name='{0}' or customer_last_name='{0}' or customer_card_last_name='{0}') ", keyword);

            #endregion
        }
        else if (field_name.IndexOf("eBay_Userid") != -1)
        {
            csWhereSQL = string.Format(" and (EBay_ID='{0}') ", keyword);

        }
        else if (field_name.IndexOf("eBay_phone") != -1)
        {
            csWhereSQL = string.Format(" and (phone_d like '%{0}') ", keyword);

        }
        else if (pre_status_serial_no != "-1")
        {
            #region web status
            ohWhereSql = " and pre_status_serial_no='" + pre_status_serial_no + "'";

            #endregion
        }
        else if (pay_method_id != -1)
        {
            #region paymethod
            ohWhereSql = " and pay_method='" + pay_method_id + "'";

            #endregion
        }
        else if (assdinged_to_id != -1)
        {
            #region assdinged to

            //            sql_row = string.Format(@"select 
            //cs.customer_shipping_city
            //,cs.customer_shipping_address
            //,cs.shipping_state_code
            //,cs.shipping_country_code
            //,cs.customer_shipping_zip_code
            //, Concat(cs.customer_first_name ,' ',cs.customer_last_name) CustomerName
            //,cs.phone_d
            //,cs.phone_n
            //,cs.phone_c
            //,
            //cs.customer_serial_no ,order_pay_status_id,order_source,
            //(select pay_method_short_name from tb_pay_method_new pmn where pmn.pay_method_serial_no=cs.pay_method) pay_method,
            // Concat(cs.customer_shipping_first_name ,' ',cs.customer_shipping_last_name) name,
            //fs.facture_state_name,
            //fs.back_color fs_back_color,
            //ps.pre_status_name,
            //ps.back_color pre_back_color,
            //oh.*
            //,ifnull((select  assigned_to_staff_name from tb_order_assigned_to ass  where ass.order_code=cs.order_code order by assigned_to_id desc limit 0,1 ) , 'None') as assigned_to_staff_name
            //,date_format((select regdate from tb_order_ups_tracking_number osd where osd.order_code=cs.order_code order by id desc limit 0,1), '%b/%d/%Y') shipping_date 
            //, grand_total- (select ifnull(sum(pay_cash), 0) from tb_order_pay_record where order_code=oh.order_code) balance
            //
            //from (
            //            select * from (select  case when order_source=3 then is_send_email else -1 end as is_send_email,
            //            price_unit,
            //                order_helper_serial_no,order_pay_status_id,order_source,o.order_code,tag,date_format(create_datetime, '%b/%d/%Y') order_date,out_status,pre_status_serial_no,grand_total,out_note,is_ok , order_invoice
            //                , (select assigned_to_staff_id from tb_order_assigned_to ass where ass.order_code=o.order_code order by assigned_to_id desc limit 0,1 ) assigned_to_staff_id
            //                from 
            //                tb_order_helper o where order_source in ({2}) and  1=1 and tag=1 and is_ok=1  order by order_helper_serial_no desc {0}
            //            ) oo where assigned_to_staff_id='{1}' 
            //        ) oh inner join tb_customer_store cs on cs.order_code=oh.order_code
            //left join tb_pre_status ps on ps.pre_status_serial_no=oh.pre_status_serial_no
            //left join tb_facture_state fs on fs.facture_state_serial_no=oh.out_status
            // order by order_helper_serial_no desc", limit_sql, assdinged_to_id, order_source);
            //            sql_count = string.Format(@"select count(order_helper_serial_no)  
            //from (select 
            //order_helper_serial_no,order_code,tag,date_format(create_datetime, '%b/%d/%Y') order_date,out_status,pre_status_serial_no,grand_total,out_note,is_ok from 
            //tb_order_helper where order_source in ({0}) and  1=1 and tag=1 and is_ok=1  order by order_helper_serial_no desc) oh inner join tb_customer_store cs on cs.order_code=oh.order_code
            //left join tb_pre_status ps on ps.pre_status_serial_no=oh.pre_status_serial_no
            //left join tb_facture_state fs on fs.facture_state_serial_no=oh.out_status
            // order by order_helper_serial_no desc", order_source);
            #endregion
        }
        else if (field_name.ToLower().IndexOf("luc_sku") != -1)
        {
            #region luc sku
            sql_LU_Sku = @" inner join ( select * from (select distinct order_code oc, serial_no from tb_order_product where product_serial_no='" + keyword + @"' 
union all 
select distinct opp.order_code, opp.serial_no from tb_order_product_sys_detail sd inner join tb_order_product opp on opp.product_serial_no = sd.sys_tmp_code where sd.product_serial_no = '" + keyword + @"' 
)t order by serial_no desc limit " + startIndex + ", " + top_count + ") orderL on orderL.oc=oh.order_code";

            #endregion
        }
        else if (field_name.ToLower().IndexOf("customer_company") != -1)
        {
            csWhereSQL = string.Format(" and customer_company like '%{0}%' ", keyword);

        }

        if (sql_LU_Sku != "")
        {
            limit_sql_OH = "";
            limit_sql_CS = "";
        }
        if (!string.IsNullOrEmpty(csWhereSQL))
        {
            sql += string.Format(@"
 from tb_order_helper oh
inner join ( select * from tb_customer_store where 1=1 " + csWhereSQL + @" " + limit_sql_CS + @") cs on cs.order_code=oh.order_code
" + sql_LU_Sku + @"
left join tb_pre_status ps on ps.pre_status_serial_no=oh.pre_status_serial_no
left join tb_facture_state fs on fs.facture_state_serial_no=oh.out_status
where  oh.tag=1 and oh.is_ok=1 order by oh.order_helper_serial_no desc ");
        }
        else
        {
            sql += string.Format(@"
 from (select * from tb_order_helper where {0} and tag=1 and is_ok=1 " + ohWhereSql + @" " + limit_sql_OH + @") oh
inner join tb_customer_store cs on cs.order_code=oh.order_code
" + sql_LU_Sku + @" 
left join tb_pre_status ps on ps.pre_status_serial_no=oh.pre_status_serial_no
left join tb_facture_state fs on fs.facture_state_serial_no=oh.out_status
where oh.tag=1 and oh.is_ok=1 order by oh.order_helper_serial_no desc"
                                        , order_source.Trim().Length != 1 ? " order_source in (" + order_source + ") " : "order_source=" + order_source);
        }


        //sql += limit_sql_OH;
        //throw new Exception(sql);
        if (isGetCount)
        {
            object o = Config.ExecuteScalar(sql_count + ohWhereSql);
            if (o != null)
            {
                int.TryParse(o.ToString(), out count);
            }
            return new DataTable();
        }
        else
            return Config.ExecuteDataTable(sql);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public int FindOrderSum()
    {
        //
        // get count
        //
        int count = 0;
        object o = Config.ExecuteScalar("select count(oh.order_helper_serial_no) from tb_order_helper oh inner join tb_customer_store cs on cs.order_code=oh.order_code where 1=1 and oh.tag=1 and is_ok=1");
        if (o != null)
        {
            int.TryParse(o.ToString(), out count);
        }
        return count;
    }

    public static DataTable GetModelsByID(int _id)
    {
        string sql = "select * from tb_order_helper where order_helper_serial_no = " + _id;
        return Config.ExecuteDataTable(sql);
    }

    public void UpdateOutStatus(int _out_status, int _pre_status, string note, int _id, int pay_status_id)
    {
        string sql = string.Format("update tb_order_helper set {0} Is_Modify=1, pre_status_serial_no='{1}' {3} where order_helper_serial_no = '{2}'"
            , _out_status > 0 ? " out_status='" + _out_status.ToString() + "' ," : "", _pre_status, _id, pay_status_id != -1 ? ", order_pay_status_id='" + pay_status_id.ToString() + "'" : "");
        // throw new Exception(sql);
        // update invoice
        //if (Config.order_complete.IndexOf("[" + _pre_status + "]") != -1)
        //{
        //    int order_code = Config.ExecuteScalarInt32("select order_code from tb_order_helper where order_helper_serial_no = '"+ _id.ToString()+"'");
        //    OrderHelperModel ohm = new OrderHelperModel();
        //    ohm.SetInvoiceToOrder(order_code);
        //}

        Config.ExecuteNonQuery(sql);
        if (note.Trim().Length > 0)
        {
            OrderHelperModel ohm = OrderHelperModel.GetOrderHelperModel(_id);

            ohm.out_note += DateTime.Now.ToString("(MM/dd/yyyy hh:mm:ss)") + note;
            ohm.Update();
        }
    }
    public void UpdateOutStatus(int _out_status, int _pre_status, int _id, int pay_status_id)
    {
        UpdateOutStatus(_out_status, _pre_status, "", _id, pay_status_id);
    }
    public static void UpdateOutStatus(int _out_status, int order_code)
    {
        string sql = "update tb_order_helper set out_status=" + _out_status + ",Is_Modify=1 where order_code = " + order_code;
        Config.ExecuteNonQuery(sql);

    }

    public static void UpdateTag(bool _tag, int _id)
    {
        string sql = "update tb_order_helper set tag=" + (_tag == true ? 1 : 0) + ",Is_Modify=1  where order_helper_serial_no = " + _id;
        Config.ExecuteNonQuery(sql);

    }

    /// <summary>
    /// 根据用户ID取得它的所有订单
    /// </summary>
    /// <param name="customer_serial_no"></param>
    /// <returns></returns>
    public static DataTable FindOrderByCustomerID(int customer_serial_no)
    {
        return Config.ExecuteDataTable(@"
select oh.sub_total, oh.order_helper_serial_no,oh.order_code,oh.sub_total,oh.discount,oh.total, 
oh.order_date, oh.out_status,oh.tax_charge,oh.shipping_charge,oh.pre_status_serial_no,
cs.customer_shipping_state , cs.customer_email1 , oh.customer_serial_no, cs.phone_d, oh.grand_total
 from tb_order_helper oh inner join tb_customer_store cs on oh.order_code=cs.order_code
 where oh.customer_serial_no=" + customer_serial_no + " and oh.tag=1 order by oh.create_datetime desc");
    }

    public void FindOrderCodeNOFinished(ref string back_order_status, ref string new_order_status)
    {
        DataTable o = Config.ExecuteDataTable(@"
select count(order_helper_serial_no) from tb_order_helper where tag=1 and is_OK=1 and pre_status_serial_no not in (" + back_order_status + @")
 union all 
select count(order_helper_serial_no) from tb_order_helper where tag=1 and is_OK=1 and pre_status_serial_no in (" + new_order_status + @")");
        if (o != null)
        {
            back_order_status = o.Rows[0][0].ToString();
            new_order_status = o.Rows[1][0].ToString();
        }
    }

    public DataTable FindOrderInvalid(string order_code, string first_name, string last_name)
    {
        string sql = "select o.order_helper_serial_no, o.order_code, Concat(customer_shipping_first_name ,' ',customer_shipping_last_name) name, date_format(order_date, '%b %d %Y') order_date from tb_order_helper o inner join tb_customer_store s on o.order_code=s.order_code where (o.tag=0 or o.is_ok=0) ";
        if (order_code != "")
        {
            sql += " and o.order_code='" + order_code + "' ";
        }
        if (first_name != "")
        {
            sql += " and customer_shipping_first_name like '%" + first_name + "%' ";
        }
        if (last_name != "")
        {
            sql += " and customer_shipping_last_name like '%" + last_name + "%' ";
        }
        sql += " order by o.create_datetime desc limit 0,50";
        return Config.ExecuteDataTable(sql);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="order_helper_serial_no"></param>
    public void SetOrderValid(int order_helper_serial_no)
    {
        Config.ExecuteNonQuery("Update tb_order_helper set tag=1, is_ok=1,Is_Modify=1 where order_helper_serial_no='" + order_helper_serial_no + "'");
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="year"></param>
    /// <param name="monty"></param>
    /// <returns></returns>
    public DataTable FindModelsByMontyExport(int year, int monty, bool is_true)
    {
        return Config.ExecuteDataTable(string.Format(@" select oh.order_helper_serial_no
,oh.order_code
,cs.customer_serial_no ,
(select state_name from tb_state_shipping ss where ss.state_serial_no=cs.customer_shipping_state) customer_shipping_state,
(select priority from tb_state_shipping ss where ss.state_serial_no=cs.customer_shipping_state) priority,

(select 
	case when system_category_serial_no = 1 then 'CA' else 'US' end 
	from tb_state_shipping ss where ss.state_serial_no=cs.customer_shipping_state) country,
(select pay_method_short_name from tb_pay_method_new pmn where pmn.pay_method_serial_no=cs.pay_method) pay_method,
oh.tag,
Concat(cs.customer_shipping_first_name ,' ',cs.customer_shipping_last_name) name,
date_format(oh.order_date,'%b %d') order_date,
(select facture_state_name from tb_facture_state where facture_state_serial_no=oh.out_status) out_status_name,
oh.grand_total,
oh.tax_charge,
oh.tax_export,
oh.gst,
oh.pst,
oh.hst,
oh.taxable_total,
oh.pre_status_serial_no from tb_order_helper oh inner join tb_customer_store cs on cs.order_code=oh.order_code where 1=1  and oh.tag=1 and oh.is_ok=1
and date_format(order_date, '%b%y')=date_format('{0}-{1}-01', '%b%y') and oh.out_status not in (" + Config.notStatOrderStatus + ") {2}", year, monty, is_true ? " and oh.tax_export=1 " : ""));

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="order_helper_serial_no"></param>
    /// <param name="is_export"></param>
    public void ChangeExportValue(int order_helper_serial_no, bool is_export)
    {
        Config.ExecuteNonQuery(string.Format("Update tb_order_helper set tax_export={0},Is_Modify=1 where order_helper_serial_no={1} ", is_export ? 1 : 0, order_helper_serial_no));
    }


    public DataTable FindStatByYear()
    {
        return Config.ExecuteDataTable(@"select order_year,  sum(o.gst) gst, sum(o.pst) pst, sum(o.hst) hst, sum(o.taxable_total) taxable_total,sum(o.grand_total) grand_total from tb_order_helper o inner join ( 
select  distinct date_format(create_datetime, '%Y') order_year from tb_order_helper) d on d.order_year= date_format(o.create_datetime, '%Y') and o.tag=1 and o.is_ok=1 and o.out_status<> 6
 group by order_year
");
    }

    public DataTable FindStatByMonty(int year)
    {
        return Config.ExecuteDataTable(string.Format(@"select order_year order_month,  date_format(create_datetime,'%m') m, sum(o.gst) gst, sum(o.pst) pst, sum(o.hst) hst, sum(o.taxable_total) taxable_total,sum(o.grand_total) grand_total from tb_order_helper o inner join ( 
select  distinct date_format(create_datetime, '%b %y') order_year from tb_order_helper where date_format(create_datetime, '%Y')='{0}') d on d.order_year= date_format(o.create_datetime, '%b %y') and o.tag=1 and o.is_ok=1 and o.out_status<> 6 and date_format(o.create_datetime, '%Y')='{0}'
 group by order_year order by create_datetime asc ", year));
    }

    /// <summary>
    /// new order part of last two days.
    /// </summary>
    /// <returns></returns>
    public DataTable FindOrderSkuByLastTwoDay()
    {
        return Config.ExecuteDataTable(string.Format(@"select p.product_serial_no from tb_product p inner join (
select sp.product_serial_no from tb_sp_tmp_detail sp inner join tb_order_product op on op.product_serial_no=sp.sys_tmp_code 
inner join (select order_code from tb_order_helper where tag=1 and  is_ok=1 and pre_status_serial_no='{0}' and date_format(`create_datetime`,'%Y%j') >= date_format(date_sub(current_date, interval 2 day), '%Y%j')) oc
on oc.order_code=op.order_code
union all 
select op.product_serial_no from tb_order_product op 
inner join (select order_code from tb_order_helper where tag=1 and is_ok=1 and pre_status_serial_no='{0}' and date_format(`create_datetime`,'%Y%j') >= date_format(date_sub(current_date, interval 2 day), '%Y%j')) oc
on oc.order_code=op.order_code and length(op.product_serial_no) <> 8 
) ptd on ptd.product_serial_no = p.product_serial_no and p.tag=1 and split_line=0 and is_non=0
 ", Config.new_order_status));
    }


    #region Invoice 
    /// <summary>
    /// 
    /// </summary>
    /// <param name="order_code"></param>
    /// <returns></returns>
    public string SetInvoiceToOrder(int order_code)
    {
        return SetInvoiceToOrder(order_code, false, false);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="order_code"></param>
    /// <param name="is_pick_up"></param>
    /// <param name="is_void_invoice"></param>
    /// <returns></returns>
    public string SetInvoiceToOrder(int order_code, bool is_pick_up, bool is_void_invoice)
    {
        DataTable o = Config.ExecuteDataTable(string.Format("select order_invoice, order_source,out_status from tb_order_helper where order_code='{0}'", order_code));

        if (o.Rows.Count > 0)
        {
            if (o.Rows[0]["order_invoice"].ToString().Length < 4)
            {
                DataTable dt = Config.ExecuteDataTable(@"select pay_method from tb_customer_store where order_code='" + order_code.ToString() + "' ");
                if (dt.Rows.Count > 0)
                {
                    //int pay_method;
                    //int.TryParse(dt.Rows[0][0].ToString(), out pay_method);
                    //throw new Exception(o.Rows[0]["out_status"].ToString() + "||" + Config.notStatOrderStatus_back_status);
                    if (Config.notStatOrderStatus_back_status.IndexOf(o.Rows[0]["out_status"].ToString()) == -1)
                    {
                        int new_invoice_code = Config.ExecuteScalarInt32(string.Format("select invoice_code from {0} where is_lock =0 limit 0,1", is_void_invoice ? " tb_order_invoice_void " : " tb_order_invoice "));
                        Config.ExecuteNonQuery(string.Format(@"Update tb_order_helper set order_invoice='{0}',Is_Modify=1 where order_code='{1}';
                    update {2} set is_lock =1, order_code='{1}' where invoice_code='{0}';", new_invoice_code, order_code, is_void_invoice ? " tb_order_invoice_void " : " tb_order_invoice "));
                        return new_invoice_code.ToString();
                    }
                }
                else
                    throw new Exception(" order is ill");
            }
        }
        return string.Empty;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="order_code"></param>
    public void CancelInvoiceOfOrder(int order_code)
    {
        using (MySqlConnection conn = new MySqlConnection(Config.ConnString))
        {
            conn.Open();
            MySqlTransaction mt = conn.BeginTransaction();
            DataTable dt = new DataTable();
            string sql = string.Format("select order_invoice from tb_order_helper where order_code='{0}'", order_code);
            MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conn);
            try
            {

                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    string invoice_code = dt.Rows[0][0].ToString();
                    if (invoice_code.Length == Config.OrderInvoiceLength)
                    {
                        new MySqlCommand(string.Format(@"
update tb_order_helper set order_invoice='0',is_download_invoice=0,Is_Modify=1 where order_code='{0}';

", order_code), conn).ExecuteNonQuery();
                        new MySqlCommand(string.Format(@"

update tb_order_invoice set is_lock =0, order_code='0' where invoice_code='{0}';

", invoice_code), conn).ExecuteNonQuery();
                        new MySqlCommand(string.Format(@"

update tb_order_invoice_void set is_lock =0, order_code='0' where invoice_code='{0}';
", invoice_code), conn).ExecuteNonQuery();
                    }
                }
                mt.Commit();
            }
            catch (Exception ex)
            {
                mt.Rollback();
                conn.Close();
                throw ex;
            }
            conn.Close();

        }

    }
    #endregion
}
