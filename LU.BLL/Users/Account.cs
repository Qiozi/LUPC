using LU.Model;
using LU.Toolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LU.BLL.Users
{
    public class Account
    {
        /// <summary>
        /// 用户登入
        /// </summary>
        /// <param name="context"></param>
        /// <param name="username"></param>
        /// <param name="pwd"></param>
        /// <param name="returnUrl"></param>
        /// <param name="userHostAddress"></param>
        /// <param name="errMsg"></param>
        /// <returns>true: token; false: string.empty.</returns>
        public static string Login(Data.nicklu2Entities context,
            string username,
            string pwd,
            string returnUrl,
            string userHostAddress,
            out string errMsg)
        {
            #region login


            if (string.IsNullOrEmpty(username))
            {
                errMsg = "Username is null.";
                return string.Empty;
            }
            else if (string.IsNullOrEmpty(pwd))
            {
                errMsg = "Password is null.";
                return string.Empty;
            }
            else
            {
                var customer = context.tb_customer
                            .FirstOrDefault(p => p.customer_login_name.Equals(username) ||
                                                 p.customer_email1.Equals(username));
                if (customer != null)
                {
                    var dbPwd = MD5.Encode(customer.customer_password.Trim()).Replace("-", "").ToLower();
                    if (dbPwd == pwd)
                    {
                        var token = UserToken.GetNewUserToken(context, customer.ID, LU.Model.Enums.UserType.WebSite, customer.customer_country_code);

                        var loginLog = new LU.Data.tb_login_log
                        {
                            http_user_agent = string.Empty,
                            login_datetime = DateTime.Now,
                            remote_address = userHostAddress,
                            login_name = customer.ID,
                            login_log_category = "c"
                        };
                        context.tb_login_log.Add(loginLog);
                        context.SaveChanges();
                        //Response.Write(JsonConvert.SerializeObject(new PostResult
                        //{
                        //    Success = true,
                        //    Data = new UserInfo
                        //    {
                        //        CurrCountry = "CA",
                        //        FirstName = customer.customer_first_name,
                        //        LastName = customer.customer_last_name,
                        //        Token = LU.Toolkit.GuidExtension.GuidToBase64(token)
                        //    },
                        //    ToUrl = (string.IsNullOrEmpty(ReqU) || ReqU.ToLower().IndexOf("findpwd.aspx") > -1 || ReqU.ToLower().IndexOf("register") >= 1) ? "/MyAccount.aspx" : ReqU
                        //}));
                        errMsg = string.Empty;
                        return LU.Toolkit.GuidExtension.GuidToBase64(token);
                    }
                    else
                    {
                        // Response.Write(string.Format(@"{{result:'err',msg:''}}"));
                        //Response.Write(JsonConvert.SerializeObject(new LU.Model.PostResult
                        //{
                        //    Success = false,
                        //    ErrMsg = "Password is error."
                        //}));
                        errMsg = "Password is error.";
                        return string.Empty;
                    }
                }
                else
                //{
                //    Response.Write(JsonConvert.SerializeObject(new PostResult
                //    {
                //        Success = false,
                //        ErrMsg = "username is invalid."
                //    }));}
                {
                    errMsg = "username is invalid.";
                    return string.Empty;
                    //Response.Write(string.Format(@"{{result:'err',msg:'username is invalid.'}}"));
                }
            }

            #endregion
        }

        /// <summary>
        /// 用户注册，，成功，返回token
        /// </summary>
        /// <param name="context"></param>
        /// <param name="username"></param>
        /// <param name="pwd1"></param>
        /// <param name="pwd2"></param>
        /// <param name="returnUrl"></param>
        /// <param name="userHostAddress"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static string Register(Data.nicklu2Entities context,
            string username,
            string pwd1,
            string pwd2,
            string returnUrl,
            string userHostAddress,
            out string errMsg)
        {
            #region register
            if (username.ToLower().IndexOf("@") == -1)
            {
                errMsg = string.Format(@"{{result:'err',msg:'username is error.'}}");
            }
            else if (string.IsNullOrEmpty(pwd1))
            {
                errMsg = string.Format(@"{{result:'err',msg:'Password is null.'}}");
            }
            else if (pwd1 != pwd2)
            {
                errMsg = string.Format(@"{{result:'err',msg:'Password is error.'}}");
            }
            else if (pwd1.Length < 4 && pwd2.Length > 20)
            {
                errMsg = string.Format(@"{{result:'err',msg:'Password length is error.'}}");
            }
            else
            {
                var newCustomer = context.tb_customer
                                         .FirstOrDefault(p => p.customer_login_name.Equals(username) ||
                                                              p.customer_email1.Equals(username));
                if (newCustomer == null)
                {
                    var newCust = new LU.Data.tb_customer();
                    newCust.create_datetime = DateTime.Now;
                    newCust.customer_first_name = username;
                    newCust.customer_login_name = username;
                    newCust.customer_email1 = username;
                    newCust.customer_password = pwd1;
                    newCust.customer_serial_no = CodeHelper.NewCustomerCode(context);
                    newCust.state_code = "ON";
                    newCust.shipping_state_code = "ON";
                    newCust.customer_business_state_code = "ON";
                    newCust.state_serial_no = 8;
                    newCust.customer_card_state_code = "ON";
                    newCust.shipping_country_code = "CA";
                    newCust.customer_business_country_code = "CA";
                   // newCust.customer_no = Guid.NewGuid();
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
                    context.tb_customer.Add(newCust);
                    context.SaveChanges();

                    errMsg = string.Empty;
                    return Toolkit.GuidExtension.GuidToBase64(UserToken.GetNewUserToken(context, newCust.ID, LU.Model.Enums.UserType.WebSite, newCust.customer_country_code.ToUpper()));
                }
                else
                {
                    errMsg = "User with the same username already has an account.";
                }
            }
            return string.Empty;
            #endregion
        }

        /// <summary>
        /// 找回密码
        /// </summary>
        /// <param name="context"></param>
        /// <param name="username"></param>
        /// <param name="userHostAddress"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static bool FindPwd(Data.nicklu2Entities context, string username,
            string userHostAddress,
            out string errMsg)
        {
            #region find password
            if (!string.IsNullOrEmpty(username))
            {
                var email = username.Trim();
                var userName = context.tb_customer
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
                    context.SaveChanges();

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
                    errMsg = "You have successfully submitted request to retrieve your password.<br>Please check your email in a few minutes. ";
                    return true;
                }

            }
            errMsg = "User don't found!";
            return false;
            #endregion
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="context"></param>
        /// <param name="userId"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <param name="confirmPassword"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static bool ModifyPassword(Data.nicklu2Entities context,
            int userId,
            string oldPassword,
            string newPassword,
            string confirmPassword,
            out string errMsg)
        {
            errMsg = string.Empty;
            if (string.IsNullOrEmpty(oldPassword))
            {
                errMsg = " Old password is empty.";
                return false;
            }
            if (string.IsNullOrEmpty(newPassword))
            {
                errMsg = "New password is empty.";
                return false;
            }
            if (string.IsNullOrEmpty(confirmPassword))
            {
                errMsg = "Confirm password is empty.";
                return false;
            }
            if (newPassword != confirmPassword)
            {
                errMsg = "Confirm password is not equal NewPassword.";
                return false;
            }
            var query = context.tb_customer.SingleOrDefault(me => me.ID.Equals(userId));
            if (query == null)
            {
                errMsg = "No find user account.";
                return false;
            }
            else
            {
                if (oldPassword.Trim() == query.customer_password.Trim())
                {
                    query.customer_password = newPassword;
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    errMsg = "Old password is error.";
                    return false;
                }
            }
        }

        /// <summary>
        /// 返回用户数据表
        /// </summary>
        /// <param name="context"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static Model.ModelV1.UserInfo GetUserInfo(Data.nicklu2Entities context, Guid token)
        {
            var tokenInfo = LU.BLL.Users.UserToken.GetUserTokenInf(context, token);
            var query = context.tb_customer.SingleOrDefault(p => p.ID.Equals(tokenInfo.UserId));

            // 订单号
            var tempCode = context.tb_cart_temp
                                  .OrderByDescending(me => me.cart_temp_serial_no)
                                  .FirstOrDefault(me => me.customer_serial_no.HasValue && me.customer_serial_no.Value.Equals(tokenInfo.UserId));
            var orderCode = tempCode == null ? 0 : tempCode.cart_temp_code.Value;
            // 购物车数量 
            var qty = tempCode == null ? 0 : context.tb_cart_temp.Count(me => me.cart_temp_code.HasValue && me.cart_temp_code.Value.Equals(orderCode));

            return new LU.Model.ModelV1.UserInfo
            {
                FirstName = query.customer_first_name,
                LastName = query.customer_last_name,
                Id = query.customer_serial_no.Value,
                Token = GuidExtension.GuidToBase64(token),
                CurrCountry = tokenInfo.SysCountry,
                ShoppingInfo = orderCode == 0 ? null : new Model.ModelV1.ShoppingCartBaseInfo
                {
                    OrderCode = orderCode,
                    Qty = qty
                }
            };
        }


    }
}
