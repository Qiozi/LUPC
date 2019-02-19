using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LU.BLL.Users
{
    public class UserToken
    {
        public static Guid GetNewUserToken(Data.nicklu2Entities context, int userId, Model.Enums.UserType userType = Model.Enums.UserType.WebSite, string sysCountry = "CA")
        {
            sysCountry = sysCountry == "US" ? sysCountry : "CA"; // 除了美国，其他都按加币结算。

            var guid = Guid.NewGuid();
            var model = new Data.tb_user_token
            {
                Expired = DateTime.Now.AddDays(1),
                Regdate = DateTime.Now,
                Token = guid,
                UserId = userId,
                UserType = (int)userType,
                SysCountry = sysCountry,
                CartGoodsQty = 0 // TODO

            };
            context.tb_user_token.Add(model);
            context.SaveChanges();
            return guid;
        }

        private static bool DeleteExpired(Data.nicklu2Entities context)
        {
            var date = DateTime.Now.AddDays(-30);
            var query = context.tb_user_token.Where(me => me.Expired < date).ToList();
            foreach (var item in query)
            {
                context.tb_user_token.Remove(item);
            }
            context.SaveChanges();
            return true;
        }

        public static int GetUserId(Data.nicklu2Entities context, Guid token)
        {
            var query = GetUserTokenInf(context, token);
            if (query != null)
            {
                return query.UserId;
            }
            else
            {
                return 0;
            }
        }

        public static Data.tb_user_token GetUserTokenInf(Data.nicklu2Entities context, Guid token)
        {
            DeleteExpired(context);

            return context.tb_user_token
                          .SingleOrDefault(me => me.Token.Equals(token));
        }

        public static Data.tb_customer GetCustomerWithToken(Data.nicklu2Entities context, Guid token)
        {
            var userId = GetUserId(context, token);
            if (userId > 0)
            {
                return context.tb_customer.SingleOrDefault(me => me.ID.Equals(userId));

            }
            return null;
        }
    }
}
