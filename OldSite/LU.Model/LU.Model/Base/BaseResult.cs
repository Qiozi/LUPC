using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LU.Model.Base
{
    public abstract class BaseResult
    {
        public bool Success { get; set; }

        public string ErrMsg { get; set; }

        public object Data { get; set; }
    }
}
