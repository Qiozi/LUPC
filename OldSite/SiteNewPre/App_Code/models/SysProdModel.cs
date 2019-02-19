using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SysProdModel
/// </summary>
public class SysProdModel
{
    public SysProdModel()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public string Title { get; set; }

    public int Sku { get; set; }

    public decimal Price { get; set; }

    public decimal Discount { get; set; }

    public List<SysProdPart> Parts { get; set; }
}

public class SysProdPart
{
    public SysProdPart() { }

    public int ImgSku { get; set; }

    public string Title { get; set; }

    public int Sku { get; set; }

    public string Comment { get; set; }
}