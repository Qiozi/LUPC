using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

/// <summary>
/// Summary description for OrderSource
/// </summary>
public class OrderSourceHelper
{
	public OrderSourceHelper()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public DataTable OrderSourceToDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("id");
        dt.Columns.Add("text");
        foreach (int i in Enum.GetValues(typeof(OrderSource)))
        {
            DataRow dr = dt.NewRow();
            dr["id"] = i;
            dr["text"] = Enum.GetName(typeof(OrderSource), i);
            dt.Rows.Add(dr);
        }
        return dt;
    }

    public int OrderSource_value(OrderSource ct)
    {
        foreach (int i in Enum.GetValues(typeof(OrderSource)))
        {
            if (Enum.GetName(typeof(OrderSource), i) == ct.ToString())
            {
                return i;
            }
        }
        return -1;
    }

    public  OrderSource GetOrderSourceByValue(int id)
    {
        try
        {
            return (OrderSource)Enum.Parse(typeof(OrderSource), Enum.GetName(typeof(OrderSource), id));
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
}

public enum OrderSource
{
    None=0,
    LuWeb = 1,
    Input = 2,
    Ebay = 3
}