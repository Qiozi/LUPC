using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Q_Admin_Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }

    public void ValidateLogin()
    {
        //string code = this.txt_code.Text.Trim();
        string username = this.txt_username.Text.Trim();
        string password = this.txt_password.Text.Trim();

        object sm = StaffModel.FindModelsByLoginName(username);
        if (sm == null)
        {
            this.lbl_error.Text = "* username isn't exist";

            return;
        }

        StaffModel m = (StaffModel)sm;
        if (m.staff_password == password && m.tag)
        {
            HttpContext hc = new HttpContext(this.Request, this.Response);
            LoginInfo li = new LoginInfo(hc);
            li.LoginID = m.staff_serial_no.ToString();
            li.RealName = m.staff_realname;
            li.UserName = username;
            m.staff_last_login_date = DateTime.Now;
            m.Update();

            decimal currency;
            DataTable dt = Config.ExecuteDataTable("Select currency_usd from tb_currency_convert where is_current=1 order by id desc limit 0,1");
            if (dt.Rows.Count > 0)
                decimal.TryParse(dt.Rows[0][0].ToString(), out currency);
            else
                currency = 1M;
            ConvertPrice.CurrentCurrencyConverter = currency;

            TrackModel.InsertInfo("Login", m.staff_serial_no);
            if (m.staff_type == 1)
                Response.Redirect("sales_order_system_list.aspx");
            else
                Response.Redirect(Config.login_redirect_path);

        }
        else
        {
            this.lbl_error.Text = "* Password Error!";
            return;
        }
        // AnthemHelper.Alert("asdf");
    }

    public string Code
    {
        get
        {
            object o = ViewState["code"];
            if (o != null)
                return o.ToString();
            return "";
        }
        set { ViewState["code"] = value; }
    }
    protected void btn_login_Click(object sender, EventArgs e)
    {
        try
        {
            ValidateLogin();
        }
        catch (Exception ex)
        {
            this.lbl_error.Text = ex.Message;
        }
    }
}
