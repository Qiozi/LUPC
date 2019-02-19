using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class cmds_user : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            switch (ReqCmd)
            {
                case "login":
                    #region login
                    if (string.IsNullOrEmpty(ReqUsername))
                    {
                        Response.Write(string.Format(@"{{result:'err',msg:'username is invalid.'}}"));
                    }
                    else if (string.IsNullOrEmpty(ReqPwd1))
                    {
                        Response.Write(string.Format(@"{{result:'err',msg:'Password is error.'}}"));
                    }
                    else
                    {
                        var customer = db.tb_customer.FirstOrDefault(p => p.customer_login_name.Equals(ReqUsername)
                            || p.customer_email1.Equals(ReqUsername));
                        if (customer != null)
                        {
                            if (customer.customer_password.Equals(ReqPwd1))
                            {

                                CartQty = db.tb_cart_temp.Count(p => p.cart_temp_code.HasValue && p.cart_temp_code.Value.Equals(CurrOrderCode));
                                SetCustomerInfo(customer);
                                Response.Write(string.Format(@"{{result:'OK'}}"));
                            }
                            else
                                Response.Write(string.Format(@"{{result:'err',msg:'Password is error.'}}"));
                        }
                        else
                        {
                            Response.Write(string.Format(@"{{result:'err',msg:'username is invalid.'}}"));
                        }
                    }
                    #endregion
                    break;

                case "register":
                    #region register
                    if (ReqUsername.ToLower().IndexOf("@") == -1)
                    {
                        Response.Write(string.Format(@"{{result:'err',msg:'username is error.'}}"));
                    }
                    else if (string.IsNullOrEmpty(ReqPwd1))
                    {
                        Response.Write(string.Format(@"{{result:'err',msg:'Password is null.'}}"));
                    }
                    else if (ReqPwd1 != ReqPwd2)
                    {
                        Response.Write(string.Format(@"{{result:'err',msg:'Password is error.'}}"));
                    }
                    else if (ReqPwd1.Length < 4 && ReqPwd1.Length > 20)
                    {
                        Response.Write(string.Format(@"{{result:'err',msg:'Password length is error.'}}"));
                    }
                    else
                    {
                        var newCustomer = db.tb_customer.FirstOrDefault(p => p.customer_login_name.Equals(ReqUsername)
                               || p.customer_email1.Equals(ReqUsername));
                        if (newCustomer == null)
                        {
                            var newCust = nicklu2Model.tb_customer.Createtb_customer(0
                                , 0);
                            newCust.create_datetime = DateTime.Now;
                            newCust.customer_login_name = ReqUsername;
                            newCust.customer_email1 = ReqUsername;
                            newCust.customer_password = ReqPwd1;
                            newCust.customer_serial_no = Codes.NewCustomerCode(db);
                            newCust.state_code = "ON";
                            newCust.shipping_state_code = "ON";
                            newCust.customer_business_state_code = "ON";
                            newCust.state_serial_no = 8;
                            newCust.customer_card_state_code = "ON";
                            newCust.shipping_country_code = "CA";
                            newCust.customer_business_country_code = "CA";

                            newCust.customer_card_country_code = "CA";
                            newCust.customer_country_code = "CA";
                            newCust.is_all_tax_execmtion = false;
                            newCust.Is_Modify = false;
                            newCust.is_old = false;
                            newCust.tag = 1;
                            newCust.system_category_serial_no = 1;
                            newCust.source = 1;
                            newCust.customer_country = "1";
                            newCust.system_category_serial_no = 1;
                            newCust.customer_card_state = 8;
                            newCust.customer_card_country = 1;
                            newCust.customer_shipping_country = 1;
                            db.AddTotb_customer(newCust);
                            db.SaveChanges();

                            SetCustomerInfo(newCust);

                            Response.Write(string.Format(@"{{result:'OK'}}"));
                        }
                        else
                        {
                            Response.Write(string.Format(@"{{result:'err',msg:'username is exist.'}}"));
                        }
                    }
                    #endregion
                    break;

                case "findpwd":
                    #region find password
                    if (!string.IsNullOrEmpty(ReqUsername) && IsLocalHostFrom)
                    {
                        var email = ReqUsername.Trim();
                        var userName = db.tb_customer.OrderByDescending(p => p.ID).FirstOrDefault(u => u.customer_login_name.Equals(email) ||
                            u.customer_email1.Equals(email) ||
                            u.customer_email2.Equals(email));
                        email = string.Empty;
                        if (userName.customer_login_name.IndexOf("@") > -1)
                        {
                            email = userName.customer_login_name;
                        }
                        else if (userName.customer_email1.IndexOf("@") > -1)
                        {
                            email = userName.customer_email1;
                        }
                        else if (userName.customer_email2.IndexOf("@") > -1)
                        {
                            email = userName.customer_email2;
                        }

                        if (!string.IsNullOrEmpty(email))
                        {
                            string sendBody = string.Format(@"
<br>Hi {0}
<br>
<br>
User name: {0}
<br>
<br>
Password: {1}"
                                , userName.customer_login_name
                                , userName.customer_password);
                            EmailHelper.send(sendBody, "Lu Computers - User Info", email);
                            Response.Write("You have successfully submitted request to retrieve your password.<br>Please check your email in a few minutes. ");
                            Response.End();
                        }

                    }
                    Response.Write("User don't found!");

                    #endregion
                    break;

            }
            Response.End();
        }
    }

    /// <summary>
    /// 保存客户登入信息
    /// </summary>
    /// <param name="cust"></param>
    void SetCustomerInfo(nicklu2Model.tb_customer cust)
    {
        SetCustomerID(cust.ID);
        SetCustomerName(!string.IsNullOrEmpty(cust.customer_first_name) ? cust.customer_first_name + " " + cust.customer_last_name : cust.customer_login_name);
        SetCustomerSerialNo(cust.customer_serial_no.Value.ToString());

        if (ReqRememberMe)
        {
            Response.Cookies["CustomerID"].Expires = DateTime.Now.AddDays(300);
            Response.Cookies["CustomerSerialNo"].Expires = DateTime.Now.AddDays(300);
            Response.Cookies["CustomerName"].Expires = DateTime.Now.AddDays(300);
        }
    }

    string ReqUsername
    {
        get
        {
            return Util.GetStringSafeFromQueryString(Page, "username").Trim();
        }
    }

    string ReqPwd1
    {
        get { return Util.GetStringSafeFromQueryString(Page, "pwd1"); }
    }

    string ReqPwd2
    {
        get { return Util.GetStringSafeFromQueryString(Page, "pwd2"); }
    }

    bool ReqRememberMe
    {
        get { return Util.GetStringSafeFromQueryString(Page, "rememberMe") == "true"; }
    }
}