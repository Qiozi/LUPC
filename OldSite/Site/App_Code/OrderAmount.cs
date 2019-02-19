using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for OrderAmount
/// </summary>
public class OrderAmount
{
	public OrderAmount()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public bool Amount(string order_code)
    {
        if (order_code.Length == 6)
        {

        }
        else
        {
            throw new Exception("order code length is error.");
        }

        return false;
    }
}
