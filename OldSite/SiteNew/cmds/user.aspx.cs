using LU.BLL;
using LU.BLL.Users;
using LU.Model;
using LU.Toolkit;
using Newtonsoft.Json;
using System;
using System.Linq;

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
                        //// Response.Write(string.Format(@"{{result:'err',msg:'username is invalid.'}}"));
                        Response.Write(JsonConvert.SerializeObject(new PostResult
                        {
                            Success = false,
                            ErrMsg = "User Name is null."
                        }));
                    }
                    else if (string.IsNullOrEmpty(ReqPwd1))
                    {
                        // Response.Write(string.Format(@"{{result:'err',msg:'Password is error.'}}"));
                        Response.Write(JsonConvert.SerializeObject(new PostResult
                        {
                            Success = false,
                            ErrMsg = "Password is null."
                        }));
                    }
                    else
                    {
                        var customer = db.tb_customer
                                    .FirstOrDefault(p => p.customer_login_name.Equals(ReqUsername) ||
                                                         p.customer_email1.Equals(ReqUsername));
                        if (customer != null)
                        {
                            var dbPwd = MD5.Encode(customer.customer_password.Trim()).Replace("-", "").ToLower();
                            if (dbPwd == ReqPwd1)
                            {
                                this.cookiesHelper.CartQty = db.tb_cart_temp.Count(p => p.cart_temp_code.HasValue && p.cart_temp_code.Value.Equals(this.cookiesHelper.CurrOrderCode));
                                var token = UserToken.GetNewUserToken(db, customer.ID, LU.Model.Enums.UserType.WebSite);
                                SetCustomerInfo(token);
                                var loginLog = new LU.Data.tb_login_log
                                {
                                    http_user_agent = string.Empty,
                                    login_datetime = DateTime.Now,
                                    remote_address = Request.UserHostAddress,
                                    login_name = customer.ID,
                                    login_log_category = "c"
                                };
                                db.tb_login_log.Add(loginLog);
                                db.SaveChanges();
                                Response.Write(JsonConvert.SerializeObject(new PostResult
                                {
                                    Success = true,
                                    Data = new UserInfo
                                    {
                                        CurrCountry = "CA",
                                        FirstName = customer.customer_first_name,
                                        LastName = customer.customer_last_name,
                                        Token = LU.Toolkit.GuidExtension.GuidToBase64(token)
                                    },
                                    ToUrl = (string.IsNullOrEmpty(ReqU) || ReqU.ToLower().IndexOf("findpwd.aspx") > -1 || ReqU.ToLower().IndexOf("register") >= 1) ? "/MyAccount.aspx" : ReqU
                                }));
                            }
                            else
                            {
                                // Response.Write(string.Format(@"{{result:'err',msg:''}}"));
                                Response.Write(JsonConvert.SerializeObject(new LU.Model.PostResult
                                {
                                    Success = false,
                                    ErrMsg = "Password is error."
                                }));
                            }
                        }
                        else
                        {
                            Response.Write(JsonConvert.SerializeObject(new PostResult
                            {
                                Success = false,
                                ErrMsg = "User Name is invalid."
                            }));
                            //Response.Write(string.Format(@"{{result:'err',msg:'username is invalid.'}}"));
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
                            var newCust = new LU.Data.tb_customer();
                            newCust.create_datetime = DateTime.Now;
                            newCust.customer_first_name = ReqUsername;
                            newCust.customer_login_name = ReqUsername;
                            newCust.customer_email1 = ReqUsername;
                            newCust.customer_password = ReqPwd1;
                            newCust.customer_serial_no = CodeHelper.NewCustomerCode(db);
                            newCust.state_code = "ON";
                            newCust.shipping_state_code = "ON";
                            newCust.customer_business_state_code = "ON";
                            newCust.state_serial_no = 8;
                            newCust.customer_card_state_code = "ON";
                            newCust.shipping_country_code = "CA";
                            newCust.customer_business_country_code = "CA";
                            //newCust.customer_no = Guid.NewGuid();
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
                            db.tb_customer.Add(newCust);
                            db.SaveChanges();

                            SetCustomerInfo(UserToken.GetNewUserToken(db, newCust.ID, LU.Model.Enums.UserType.WebSite));

                            Response.Write(string.Format(@"{{result:'OK'}}"));
                        }
                        else
                        {
                            Response.Write(string.Format(@"{{result:'err',msg:'User with the same username already has an account.'}}"));
                        }
                    }
                    #endregion
                    break;

                case "findpwd":
                    #region find password
                    if (!string.IsNullOrEmpty(ReqUsername) && IsLocalHostFrom)
                    {
                        var email = ReqUsername.Trim();
                        var userName = db.tb_customer
                                         .OrderByDescending(p => p.ID)
                                         .FirstOrDefault(u => u.customer_login_name.Equals(email) ||
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
                            var radmon = DateTime.Now.ToString("HHssff");
                            userName.customer_password = radmon;
                            db.SaveChanges();

                            string sendBody = string.Format(@"
<br>Hi {0}
<br>
<br>
User name: {0}
<br>
<br>
Password(Random number): {1}

<br>
<hr>
<a href='https://www.lucomputers.com/'>www.lucomputers.com</a>
"
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

                case "modifyPassword":
                    #region modify password
                    if (!IsLogin)
                    {
                        Response.Redirect("/Login.aspx", true);
                    }
                    else
                    {
                        var errMsg = string.Empty;
                        LU.BLL.Users.Account.ModifyPassword(db, CurrCustomer.ID, ReqOldPwd, ReqPwd1, ReqPwd2, out errMsg);
                        if (string.IsNullOrEmpty(errMsg))
                        {
                            Response.Write("Password modify is OK.");
                        }
                        else
                        {
                            Response.Write(errMsg);
                        }
                    }
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
    void SetCustomerInfo(Guid token)
    {
        this.cookiesHelper.UserToken = token;
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

    string ReqOldPwd
    {
        get { return Util.GetStringSafeFromQueryString(Page, "oldPassword"); }
    }

    //bool ReqRememberMe
    //{
    //    get { return Util.GetStringSafeFromQueryString(Page, "rememberMe") == "true"; }
    //}
}