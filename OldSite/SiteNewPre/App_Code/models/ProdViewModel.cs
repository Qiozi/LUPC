using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ProdViewModel
/// </summary>
[Serializable]
public class ProdViewModel
{
    public ProdViewModel() { }

    public int SKU { get; set; }

    public string ShortName { get; set; }

    public string Name { get; set; }

    public string PriceUnit { get; set; }

    public decimal Price { get; set; }

    public decimal Sold { get; set; }

    public decimal Discount { get; set; }

    public int ImgSku { get; set; }
}