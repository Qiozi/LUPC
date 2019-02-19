using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LU.Model
{
    public class UserInfo
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        /// <summary>
        ///     ca,us
        /// </summary>
        public string CurrCountry { get; set; }

        public string Token { get; set; }
    }
}
