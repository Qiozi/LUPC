using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;

public partial class Q_Admin_orders_cmd : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            InitialDatabase();
    }

    public override void InitialDatabase()
    {
        try
        {
            base.InitialDatabase();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            Response.ClearContent();
            switch (ReqCMD)
            {
                case "get_out_status_DTC":
                    DataTable dt = new XmlStore().FindPreStatus();// Config.ExecuteDataTable(@"select pre_status_serial_no, pre_status_name from tb_pre_status where showit=1 order by priority asc ");
                    //dt = new XmlStore().FindPreStatus();
                    sb.Append("<option value='-1'>Select</option>");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];
                        sb.Append(string.Format(@"<option value='{0}'>{1}</option>"
                            , dr[0].ToString()
                            , dr[1].ToString()));
                    }
                    Response.Write(sb.ToString());
                    break;
                case "get_pay_method_DTC":
                    dt = FindPayMethods();
                    sb = new System.Text.StringBuilder();
                    sb.Append("<option value='-1'>Select</option>");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];
                        sb.Append(string.Format(@"<option value='{0}'>{1}</option>"
                            , dr["pay_method_serial_no"].ToString()
                            , dr["pay_method_name"].ToString()));
                    }
                    Response.Write(sb.ToString());
                    break;

                case "create_default_order":
                    int OrderCode = new OrderHelper().CreateNewDefaultOrder(7888888, false);
                    Response.Write(OrderCode.ToString() + "OK");
                    InsertTraceInfo("Create One Order (" + OrderCode.ToString() + ")");
                    break;
                case "createNewByCustomerID":
                    if (ReqCustomerID != -1)
                    {
                        OrderCode = new OrderHelper().CreateNewDefaultOrder(ReqCustomerID, !string.IsNullOrEmpty(ReqTRANSACTIONID));

                        if (!string.IsNullOrEmpty(ReqTRANSACTIONID))
                        {
                            if (Config.ExecuteScalarInt32("Select count(pay_id) from tb_order_paypal_record where transaction='" + ReqTRANSACTIONID + "'") == 0)
                            {
                                Config.ExecuteNonQuery(@"insert into tb_order_paypal_record 
	( transaction,   order_code,  regdate
	)
	values
	( '" + ReqTRANSACTIONID + "',   '" + OrderCode.ToString() + @"',  now());");
                            }
                            else
                            {

                                Config.ExecuteNonQuery(@"update tb_order_paypal_record  
set order_code='" + OrderCode + "' where transaction='" + ReqTRANSACTIONID + "'");

                                Config.ExecuteNonQuery("update tb_order_pay_record set order_code='" + OrderCode + "' where balance = 999999");
                                Config.ExecuteNonQuery("update tb_order_pay_record set balance = 0 where order_code='" + OrderCode + "' ");
                            }
                            Config.ExecuteNonQuery("update tb_order_helper set order_pay_status_id=2 where order_code='" + OrderCode + "' ");
                        }
                        
                        Response.Write(OrderCode.ToString() + "OK");
                        InsertTraceInfo("Create One Order (" + OrderCode.ToString() + ") by paypal t id:"+ ReqTRANSACTIONID);
                    }
                    break;
                case "getOrderList":
                    Response.Write(GetOrderList(false));
                    break;
                case "getOrderListPageCount":
                    Response.Write(GetOrderList(true));
                    break;
                case "get_frt_stat":
                    sb = new System.Text.StringBuilder();
                    //PreStatusModel[] psms = PreStatusModel.FindModelsByShowit();
                    dt = new XmlStore().FindPreStatus();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sb.Append(string.Format(@"<option value='{0}' {2} {3}>{1}</option>"
                            , dt.Rows[i]["pre_status_serial_no"].ToString()
                            , dt.Rows[i]["pre_status_name"].ToString()
                            , ReqCurrID.ToString() == dt.Rows[i]["pre_status_serial_no"].ToString() ? " selected='true' " : ""
                            , dt.Rows[i]["back_color"].ToString().Length > 2 ? " style='background:" + dt.Rows[i]["back_color"].ToString() + ";'" : ""
                            ));

                    }
                    Response.Write(sb.ToString());
                    break;

                case "get_bak_stat":
                    sb = new System.Text.StringBuilder();
                    //FactureStateModel[] fsms = FactureStateModel.FindModelsByShowit();
                    dt = new XmlStore().FindFactureState();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sb.Append(string.Format(@"<option value='{0}' {2} {3}>{1}</option>"
                            , dt.Rows[i]["facture_state_serial_no"].ToString()
                            , dt.Rows[i]["facture_state_name"].ToString()
                            , ReqCurrID.ToString() == dt.Rows[i]["facture_state_serial_no"].ToString() ? " selected='true' " : ""
                            , dt.Rows[i]["back_color"].ToString().Length > 2 ? " style='background:" + dt.Rows[i]["back_color"].ToString() + ";'" : ""));

                    }                    
                    Response.Write(sb.ToString());
                    break;
                case "save_order_status":
                    new OrderHelperModel().UpdateOutStatus(-1, ReqFRTStat, "", ReqOrderID, -1);
                    this.InsertTraceInfo("Save Order Note And Status:" + ReqOrderID.ToString());
                    Response.Write( GetOrderStatName(ReqOrderID));
                    break;

                case "getOrderNotepad":
                    WriteOrderNotepad(ReqOrderID);
                    break;

                case "GetEbayItemIDByOrderCode":
                    GetEbayItemIDByOrderCode(ReqOrderID);
                    break;

                case "getEbayOrderProductName":
                    GetEbayOrderProductName(ReqOrderID);
                    break;

                case "matchEBayProduct":
                    matchEBayProduct();
                    break;

                case "getOrderListStoreCodes":
                    Response.Write(getOrderListStoreCodes());
                    break;
                case "setOrderListStoreCodes":
                    setOrderListStoreCodes(ReqOrderCode);
                    break;
                case "removeOrderListStoreCodes":
                    removeOrderListStoreCodes(ReqOrderCode);
                    break;
                case "getOtherLtdNames":
                    getOtherLtdNames();
                    break;
                case "getOrderPaypalRecordAMT":
                    var payPrice = Config.ExecuteScalar("select ifnull(sum(amt), 0) price from tb_order_paypal_record where order_code='" + ReqOrderCode + "'");
                    Response.Write(payPrice.ToString());
                    break;
            }
        }
        catch (Exception ex)
        {
            Response.Write("error:"+ex.Message);
        }
        Response.End();
       
        
    }

    void getOtherLtdNames()
    {
        DataTable dt = Config.ExecuteDataTable("select other_inc_name text from tb_other_inc where other_inc_type=1 and id>1");
        DataRow dr = dt.NewRow();
        dr[0] = "";
        dt.Rows.InsertAt(dr,0);
        Response.Write(DataToJson.ToJson(dt, "item"));
    }

    #region 用于订单列表，选中订单号
    void setOrderListStoreCodes(string code)
    {
        if (Session["OrderListStoreCodes"] == null)
            Session["OrderListStoreCodes"] = "";

        if (Session["OrderListStoreCodes"].ToString().IndexOf(code) == -1)
            Session["OrderListStoreCodes"] = Session["OrderListStoreCodes"]+"|" + code;
        Response.Write(getOrderListStoreCodes());
    }

    void removeOrderListStoreCodes(string code)
    {
    

        if (Session["OrderListStoreCodes"] != null)
        {
            Session["OrderListStoreCodes"] = Session["OrderListStoreCodes"].ToString().Replace(code, "").Replace("||", "|");
        }
        Response.Write(getOrderListStoreCodes());
    }

    string getOrderListStoreCodes()
    {
        if (Session["OrderListStoreCodes"] == null)
            Session["OrderListStoreCodes"] = "";
        string codes = Session["OrderListStoreCodes"].ToString();
        return codes.Trim(new char[] { '|' });
    }
    #endregion

    /// <summary>
    /// 执行服务器下载ebay 产品功能.
    /// http://www.lucomputers.com/q_admin/netcmd/GetEbayActiveItems.aspx?cmd=Qiozi@msn.com&isclose=1
    /// </summary>
    void matchEBayProduct()
    {
        //string path = "~/ebay/watchs";
        //if (!Directory.Exists(Server.MapPath(path)))
        //    Directory.CreateDirectory(Server.MapPath(path));
        //DirectoryInfo dir = new DirectoryInfo(Server.MapPath(path));
        //FileInfo[] fis = dir.GetFiles();
        //for (int i = 0; i < fis.Length; i++)
        //{
        //    if (fis[i].Extension.ToLower().IndexOf("pdf") > -1)
        //        File.Delete(fis[i].FullName);
        //}

        //StreamWriter sw = new StreamWriter(Server.MapPath(path + "/ebayOrder.txt"), false);
        //sw.Write("macthebayproduct");
        //sw.Close();
        //sw.Dispose();
        Config.ExecuteNonQuery("delete from tb_timer where Cmd = '3'");
        Config.ExecuteNonQuery("Insert into tb_timer (CmdContent, Cmd,regdate) values ('macthebayproduct','3',now())");
    }

    void GetEbayOrderProductName(int orderCode)
    {
        if (orderCode.ToString().Length == 5)
        {
            DataTable dt = Config.ExecuteDataTable("Select product_name from tb_order_product where order_code='" + orderCode.ToString() + "'");
            if (dt.Rows.Count == 1)
            {
                Response.Write(dt.Rows[0][0].ToString());
            }
            else
                Response.Write("");
        }
        else
            Response.Write("");

    }

    void GetEbayItemIDByOrderCode(int orderCode)
    {
        DataTable dt = Config.ExecuteDataTable("Select ebayItemId from tb_order_product where order_code='" + orderCode.ToString() + "'");
        if (dt.Rows.Count == 1)
        {
            Response.Write(dt.Rows[0][0].ToString());
        }
        else
            Response.Write("");
    }

    void WriteOrderNotepad(int orderCode)
    {
        Response.Clear();

        DataTable dt = Config.ExecuteDataTable("Select * from tb_order_notepad where orderCode='" + orderCode.ToString() + "' order by id asc");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            Response.Write(string.Format(@"<span style='color:green;font-weight:bold;'>{0}</span> <span style='color:#666;'>last submit ({1}):<br>&nbsp;&nbsp;{2}<br></span>"
                , dt.Rows[i]["Author"].ToString()
                , dt.Rows[i]["regdate"].ToString()
                , dt.Rows[i]["msg"].ToString().Replace("\"", "'").Replace("\r\n", " ").Replace("\r", " ").Replace("\n", " ")
            ));
        }
        if(dt.Rows.Count ==0)
            Response.Write( string.Format(@"&nbsp;"));
        Response.End();
    }

    private string GetOrderStatName(int orderID)
    {
        OrderHelperModel ohm = OrderHelperModel.GetOrderHelperModel(orderID);
        return string.Format("{0}|{1}", ohm.pre_status_serial_no, ohm.out_status);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private string GetOrderList(bool isGetPageCount)
    {
        int count = 0;
        int top_count = ReqPageSize;
        int pageSize = ReqPageSize;
        DataTable OrderListDT = new DataTable();
        int startIndex = 0;
        if (ReqPageID > 1)
            startIndex = (ReqPageID-1) * pageSize;

        

        if (ReqSearchIndex == 2)
            OrderListDT = OrderHelperModel.GetModelsBySearch2("", "-1", this.ReqPreStatus.ToString(), -1, top_count, startIndex, pageSize, -1, ReqOrderSource,isGetPageCount, ref count);
        else if (ReqSearchIndex == 3)
            OrderListDT = OrderHelperModel.GetModelsBySearch2("", "-1", "-1", ReqPayMethodID, top_count, startIndex, pageSize, -1, ReqOrderSource, isGetPageCount,ref count);
        else if (ReqSearchIndex == 4)
        {
            OrderListDT = OrderHelperModel.GetModelsBySearch2("", "-1", "-1", -1, top_count, startIndex, pageSize, -1, ReqOrderSource, isGetPageCount,ref count);
            //this.AspNetPager1.CurrentPageIndex = 0;
        }
        else if (ReqSearchIndex == 5)
        {
            OrderListDT = OrderHelperModel.GetModelsBySearch2("", "-1", "-1", -1, top_count, startIndex, pageSize, LoginUser.LoginIDInt, ReqOrderSource,isGetPageCount, ref count);
        }
        else
        {
            // throw new Exception("D");
            string keyword = ReqKeyword;
            OrderListDT = OrderHelperModel.GetModelsBySearch2(keyword, ReqSearchFieldName, "-1", -1, top_count, startIndex, pageSize, -1, ReqOrderSource,isGetPageCount, ref count);
        }

        // throw new Exception((DateTime.Now - begin).ToString());

        CurrentPageCount = count;
        //
        // get Page Count.
        //
        if (count % pageSize == 0)
            count = count / pageSize;
        else
            count = count / pageSize + 1;
        if (!isGetPageCount)
            return WriteOrderListJson(OrderListDT, count);
        else
           return count.ToString();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    private string WriteOrderListJson(DataTable dt, int page_count)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //{"options":"[{\"text\":\"TESt1\",\"value\":\"9\"},{\"text\":\"TEST2\",\"value\":\"10\"},{\"text\":\"TEst3\",\"value\":\"13\"}]"} 
        sb.Append(@"""[");
        //int pagecount = 
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow dr = dt.Rows[i];

            DateTime orderDate;
            DateTime.TryParse(dr["order_date"].ToString(), out orderDate);
            int isGetin = Config.ExecuteScalarInt32("select count(id) from tb_part_getin_order where ordercode='" + dr["order_code"].ToString() + "'");

            sb.Append(string.Format(@"{{customer_serial_no:\""{0}\"",order_pay_status_id:\""{1}\"",order_source:\""{2}\"",pay_method:\""{3}\"",name:\""{4}\"",facture_state_name:\""{5}\"",fs_back_color:\""{6}\"",pre_status_name:\""{7}\"",pre_back_color:\""{8}\"",order_pay_status_id:\""{9}\"",order_source:\""{10}\"",is_send_email:\""{11}\"",price_unit:\""{12}\"",order_helper_serial_no:\""{13}\"",order_code:\""{14}\"",tag:\""{15}\"",order_date:\""{16}\"",out_status:\""{17}\"",pre_status_serial_no:\""{18}\"",grand_total:\""{19}\"",out_note:\""{20}\"",is_ok:\""{21}\"",order_invoice:\""{22}\"",assigned_to_staff_name:\""{23}\"",shipping_date:\""{24}\"",balance:\""{25}\"",pagercount:\""{26}\"",customer_shipping_city:\""{27}\"",customer_shipping_address:\""{28}\"",shipping_state_code:\""{29}\"",shipping_country_code:\""{30}\"",customer_shipping_zip_code:\""{31}\"",customerName:\""{32}\"",phone_d:\""{33}\"",phone_n:\""{34}\"",phone_c:\""{35}\"",msgCount:\""{36}\"", notepadCount:\""{37}\"",isGetIn:\""{38}\""}}"
                , dr["customer_serial_no"].ToString()
                , dr["order_pay_status_id"].ToString()
                , dr["order_source"].ToString()
                , dr["pay_method"].ToString()
                , dr["name"].ToString()
                , dr["facture_state_name"].ToString()
                , dr["fs_back_color"].ToString()
                , dr["pre_status_name"].ToString()
                , dr["pre_back_color"].ToString()
                , dr["order_pay_status_id"].ToString()
                , dr["order_source"].ToString()
                , dr["is_send_email"].ToString()
                , dr["price_unit"].ToString()
                , dr["order_helper_serial_no"].ToString()
                , dr["order_code"].ToString()
                , dr["tag"].ToString()
                , ViewDateFormat.View(orderDate).Replace("12:00 AM,", "")
                , dr["out_status"].ToString()
                , dr["pre_status_serial_no"].ToString()
                , dr["grand_total"].ToString()
                , dr["out_note"].ToString()
                , dr["is_ok"].ToString()
                , dr["order_invoice"].ToString()
                , dr["assigned_to_staff_name"].ToString()
                , dr["shipping_date"].ToString()
                , dr["balance"].ToString()
                , page_count.ToString()
                , dr["customer_shipping_city"].ToString()
                , dr["customer_shipping_address"].ToString().Replace("\"", "'").Replace("\r\n", " ").Replace("\r", " ").Replace("\n", " ")
                , dr["shipping_state_code"].ToString()
                , dr["shipping_country_code"].ToString()
                , dr["customer_shipping_zip_code"].ToString()
                , dr["CustomerName"].ToString().Trim()
                , PhoneFormat.Format(dr["phone_d"].ToString())
                , PhoneFormat.Format(dr["phone_n"].ToString())
                , PhoneFormat.Format(dr["phone_c"].ToString())
                , dr["msgCount"].ToString()
                , dr["notepadCount"].ToString() ?? "0"
                , isGetin.ToString()
                ));
            if (i != dt.Rows.Count - 1)
                sb.Append(",");
        }
        sb.Append(@"]""");
        return sb.ToString();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public DataTable FindPayMethods()
    {
       //XmlStore xs = new XmlStore();
        return new XmlStore().FindPayMethods();
    }

    #region properties
    /// <summary>
    /// 
    /// </summary>
    int ReqCustomerID
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "customerID", -1); }
    }
    /// <summary>
    /// 
    /// </summary>
    public string ReqCMD
    {
        get
        {
            string cmd = Util.GetStringSafeFromString(Page, "cmd");
            if (cmd.Length > 0)
                return cmd;
            return Util.GetStringSafeFromQueryString(Page, "cmd");
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public int ReqSearchIndex
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "searchIndex", 0); }
    }

    /// <summary>
    /// store pre atatus datatable.
    /// </summary>
    public PreStatusModel[] PreStatusDB
    {
        get { return (PreStatusModel[])ViewState["PreStatusDB"]; }
        set { ViewState["PreStatusDB"] = value; }
    }

    public string ReqOrderSource
    {
        get
        {
            string order_source = Util.GetStringSafeFromQueryString(Page, "order_source");
            if (order_source == "")
                return "0,1,2,3";
            return order_source;
        }

    }

    public string ReqPreStatus
    {
        get { return Util.GetStringSafeFromQueryString(Page, "out_status"); }
    }

    public int ReqPayMethodID
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "PayMethodID", 0); }
    }

    public int CurrentPageCount
    {
        get { return (int)ViewState["CurrentPageCount"]; }
        set { ViewState["CurrentPageCount"] = value; }
    }

    public string ReqKeyword
    {
        get { return Util.GetStringSafeFromQueryString(Page, "keyword"); }
    }

    public string ReqSearchFieldName
    {
        get { return Util.GetStringSafeFromQueryString(Page, "field_name"); }
    }

    public int ReqPageID
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "pageid", 1); }
    }
    int ReqPageSize
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "pageSize", 10); }
    }
    public int ReqCurrID
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "currentID", -1); }
    }

    public int ReqBK_Stat
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "bk_stat", -1); } 
    }

    public int ReqFRTStat
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "frt_stat", -1); }
    }

    public int ReqOrderID
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "OrderID", -1); }
    }

    string ReqTRANSACTIONID
    {
        get { return Util.GetStringSafeFromQueryString(Page, "TRANSACTIONID"); }
    }

    string ReqOrderCode
    {
        get { return Util.GetStringSafeFromQueryString(Page, "order_code"); }
    }
    #endregion
}
