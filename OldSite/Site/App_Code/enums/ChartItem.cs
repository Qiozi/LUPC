using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ChartItem
/// </summary>
public class ChartItem
{
	public ChartItem()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public List<double> Data { set; get; }
    public KeyValuePair<string, string> LabelColor { set; get; }

 
}