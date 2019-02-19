using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace LU.Model.Enums
{
    /// <summary>
    /// Summary description for PaypalPayStatus
    /// </summary>
    public enum PaypalPayStatus
    {
        paypal_no_paed = 1,
        paypal_success = 2,
        paypal_failure = 4,
        pay_record_method_paypal = 15
    }
}