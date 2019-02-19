using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LU.Model.M
{
    public class OrderListItem
    {
        public int OrderCode { get; set; }

        public DateTime OrderDate { get; set; }

        public string ShippingFirstName { get; set; }

        public string ShippingLastName { get; set; }

        public string ShippingName
        {
            get
            {
                return string.Concat(ShippingFirstName, " ", ShippingLastName);
            }
        }
        public decimal Amount { get; set; }

        public string PriceUnit { get; set; }

        public string PreStatusName { get; set; }

        public int PreStatusId { get; set; }

        public string UpsTrackingNumber { get; set; }

        public DateTime? ShippingDate { get; set; }

        public string ShippingDateText
        {
            get
            {
                if (ShippingDate.HasValue && ShippingDate.Value != DateTime.MinValue)
                {
                    return ShippingDate.Value.ToShortDateString();
                }
                return string.Empty;
            }
        }
    }
}
