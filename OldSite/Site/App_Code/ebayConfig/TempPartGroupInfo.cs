using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TempPartGroupInfo
/// </summary>

public class TempPartGroupInfo
{
    public TempPartGroupInfo() { }

    public string GroupComment { set; get; }
    public List<int> PartGroupIds { get; set; }
    public int Priority { set; get; }
}