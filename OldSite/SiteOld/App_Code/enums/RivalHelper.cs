using System;
using System.Data;

/// <summary>
/// Summary description for RivalHelper
/// </summary>
public class RivalHelper
{
	public RivalHelper()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public DataTable RivalToDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("id");
        dt.Columns.Add("text");
        foreach (int i in Enum.GetValues(typeof(Rival)))
        {
            DataRow dr = dt.NewRow();
            dr["id"] = i;
            dr["text"] = Enum.GetName(typeof(Rival), i);
            dt.Rows.Add(dr);
        }
        return dt;
    }

    public int RivalValue(Ltd ct)
    {
        foreach (int i in Enum.GetValues(typeof(Rival)))
        {
            if (Enum.GetName(typeof(Rival), i) == ct.ToString())
            {
                return i;
            }
        }
        return -1;
    }

    public Rival RivalByValue(int v)
    {
        try
        {
            return (Rival)Enum.Parse(typeof(Rival), Enum.GetName(typeof(Rival), v));
        }
        catch (Exception ex) { throw ex; }
    }
}

public enum Rival
{
    ETC =0,
    Ncix =1,
    CanadaComputer=2,
    PcvOnline = 3,
    DirectDial = 4,
    TigerDirect = 5
}

public enum Rival2
{
    ETC = 100,
    Ncix = 101,
    CanadaComputer = 102,
    PcvOnline = 103,
    DirectDial = 104,
    TigerDirect = 105
}