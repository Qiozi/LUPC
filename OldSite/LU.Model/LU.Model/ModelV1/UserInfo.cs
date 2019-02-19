using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LU.Model.ModelV1
{
    public class UserInfo : LU.Model.UserInfo
    {
        public ModelV1.ShoppingCartBaseInfo ShoppingInfo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 当前价格类型，美金或加币
        /// </summary>
        public LU.Model.Enums.CountryType CountryType { get; set; }
    }
}
