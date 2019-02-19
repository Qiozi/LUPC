using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ShoppingListModel
/// </summary>
[Serializable]
public class ShoppingListModel
{
	public ShoppingListModel()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int ID { get; set; }

    public int SKU { get; set; }

    public decimal Price { get; set; }
    public string PriceString { get; set; }

    public decimal Sold { get; set; }
    public string SoldString { get; set; }

    public int Qty { get; set; }

    public string ImgUrl { get; set; }

    public string Title { get; set; }

    public decimal SubSold { get; set; }
    public string SubSoldString { get; set; }

    public string PriceUnit { get; set; }

}