using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Chat 的摘要说明
/// </summary>
public class ChartSubItem
{
    public string fillColor { get; set; }

    public string strokeColor { get; set; }

    public List<double> data { get; set; }
}

public class ChartModel
{
    public ChartJsItem data { get; set; }

    public string title { get; set; }
}

public class ChartJsItem
{
    public List<string> labels { get; set; }

    public List<ChartSubItem> datasets { get; set; }

}