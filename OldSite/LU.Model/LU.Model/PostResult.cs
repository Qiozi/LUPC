using LU.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LU.Model
{
    [Serializable]
    public class PostResult : BaseResult
    {
        public string ToUrl { get; set; }

    }
}