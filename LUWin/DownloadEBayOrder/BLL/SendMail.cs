using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DownloadEBayOrder.BLL
{
    public class SendMail
    {
        /// <summary>
        /// 执行网站上的邮件发送
        /// </summary>
        /// <param name="OrderCode"></param>
        public static void Send(int OrderCode)
        {
            try
            {
                var page = WebClientHelper.GetPage("http://manager.lucomputers.com/q_admin/email_simple_invoice.aspx?issend=true&order_code=" + OrderCode.ToString());

                // System.Diagnostics.Process.Start("http://manager.lucomputers.com/q_admin/email_simple_invoice.aspx?issend=true&order_code=" + OrderCode.ToString());
            }
            catch
            {
                System.Diagnostics.Process.Start("IEXPLORE.EXE", "http://manager.lucomputers.com/q_admin/email_simple_invoice.aspx?issend=true&order_code=" + OrderCode.ToString());
            }
        }
    }
}
