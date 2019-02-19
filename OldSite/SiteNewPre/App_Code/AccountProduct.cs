using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for AccountProduct
/// </summary>
[Serializable]
public class AccountProduct
{

    int _product_id;
    ProdType _product_cate;
    int _product_size;
    decimal _price;
    decimal _charge;
    int _shipping_company_id;
    int _ap_id;
    int _sum = 1;

    public AccountProduct() { }
    public int product_id
    {
        get
        {
            return _product_id;
        }
        set { _product_id = value; }
    }
    public int ap_id
    {
        get { return _ap_id; }
        set { _ap_id = value; }
    }
    public int product_size
    {
        get { return _product_size; }
        set { _product_size = value; }
    }

    public ProdType ProductType
    {
        get { return _product_cate; }
        set { _product_cate = value; }
    }

    public decimal price
    {
        get { return _price; }
        set { _price = value; }
    }

    public decimal charge
    {
        get { return _charge; }
        set { _charge = value; }
    }

    public int shipping_company_id
    {
        get { return _shipping_company_id; }
        set { _shipping_company_id = value; }
    }

    public int sum
    {
        get { return _sum; }
        set { _sum = value; }
    }


}