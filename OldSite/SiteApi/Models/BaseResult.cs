using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SiteApi.Models
{
    public abstract class BaseResult
    {
        public bool Success { get; set; }

        public string ErrMsg { get; set; }
    }
}