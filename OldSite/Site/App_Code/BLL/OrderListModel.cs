using System;
using System.Data;
using System.Configuration;
using System.Runtime.Serialization;
using System.Web;
using System.IO;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Runtime.Serialization.Json;

/// <summary>
/// Summary description for OrderListModel
/// </summary>
[DataContract]
public class OrderListModel
{
	public OrderListModel()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    [DataMember]
    public string order_code { set; get; }
}
