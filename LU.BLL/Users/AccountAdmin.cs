using LU.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LU.BLL.Users
{
    public class AccountAdmin
    {
        nicklu2Entities _context;

        public AccountAdmin(nicklu2Entities context)
        {
            _context = context;
        }

        /// <summary>
        /// 用户登入信息
        /// </summary>
        /// <param name="context"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public LU.Model.Account.UserInfoAdmin GetUserInfo( Guid token)
        {
            var query = _context.tb_user_token.SingleOrDefault(me => me.Token.Equals(token) && me.Expired>=DateTime.Now);
            if (query != null)
            {
                var userId = query.UserId;
                var staff = _context.tb_staff.SingleOrDefault(me => me.staff_serial_no.Equals(userId));
                if (staff != null)
                {
                    return new Model.Account.UserInfoAdmin
                    {
                        Id = staff.staff_serial_no,
                        RealName = staff.staff_realname,
                        UserName = staff.staff_login_name
                    };
                }
            }
            return null;
        }
    }
}
