using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadEBayOrder.BLL
{
    public class GenerateWebApiToken
    {
        static string Token = string.Empty;
        static DateTime GenerateTime = DateTime.Now;

        public static string GetToken()
        {
            if (Token == string.Empty || GenerateTime.AddMinutes(30) < DateTime.Now)
            {
                nicklu2Entities db = new nicklu2Entities();
                var exchange = new tb_exchange
                {
                    Pwd = Guid.NewGuid().ToString(),
                    Regdate = DateTime.Now,
                    Source = "WinDownOrder",
                    ExchangeType = (int)SiteEnum.ExchangeType.ChangeeBayStockQuantityOnline
                };
                db.tb_exchange.Add(exchange);
                db.SaveChanges();
                Token = exchange.Pwd;
                GenerateTime = DateTime.Now;
                return Token;
            }
            else
            {
                return Token;
            }
        }
    }
}
