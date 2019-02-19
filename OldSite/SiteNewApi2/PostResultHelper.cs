using LU.BLL;
using LU.Model.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace SiteNewApi2
{
    public class PostResultHelper
    {
        public HttpRequestMessage _Request { get; set; }

        public PostResultHelper(HttpRequestMessage req)
        {
            _Request = req;
        }

        public PostResult Error(string errMsg = "网络错误。")
        {
            return new PostResult
            {
                Data = new DataColl
                {
                    List = string.Empty
                },
                Success = false,
                ErrMsg = errMsg,
                ToUrl = string.Empty
            };
        }

        /// <summary>
        /// 返回执行完
        /// </summary>
        /// <param name="data"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public PostResult End(object data, string errMsg)
        {
            return new PostResult
            {
                Data = new DataColl { List = data, HttpToken = string.Empty },
                Success = errMsg == string.Empty,
                ErrMsg = errMsg,
                ToUrl = string.Empty
            };
        }

        public void WriteErrorLog(Exception exp)
        {
            Logs.WriteErrorLog(exp, _Request.RequestUri.ToString(), _Request.RequestUri.LocalPath);
        }

        public void WriteLog(string logString)
        {
            Logs.WriteLog(logString, _Request.RequestUri.ToString(), _Request.RequestUri.LocalPath);
        }
    }
}