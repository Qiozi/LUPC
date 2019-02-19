using System;
using System.Data;

/// <summary>
/// PayMethod 的摘要说明
/// </summary>
public class LtdHelper
{
    const string WHOLESALER_TAG = "wholesaler_";
    const string RIVAL_TAG = "Rival_";
    public LtdHelper()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    public DataTable LtdHelperToDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("id");
        dt.Columns.Add("text");
        foreach (int i in Enum.GetValues(typeof(Ltd)))
        {
            DataRow dr = dt.NewRow();
            dr["id"] = i;
            dr["text"] = Enum.GetName(typeof(Ltd), i);
            dt.Rows.Add(dr);
        }
        return dt;
    }
    public DataTable LtdHelperToDataTableNoLU()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("id");
        dt.Columns.Add("text");
        foreach (int i in Enum.GetValues(typeof(Ltd)))
        {
            if (i != 1)
            {
                DataRow dr = dt.NewRow();
                dr["id"] = i;
                dr["text"] = FilterText(Enum.GetName(typeof(Ltd), i));
                dt.Rows.Add(dr);
            }
        }
        return dt;
    }

    public DataTable LtdHelperWholesalerToDT()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("id");
        dt.Columns.Add("text");
        foreach (int i in Enum.GetValues(typeof(Ltd)))
        {
            string text = Enum.GetName(typeof(Ltd), i);
            if (text.IndexOf(WHOLESALER_TAG) != -1)
            {
                DataRow dr = dt.NewRow();
                dr["id"] = i;
                dr["text"] = text.Replace(WHOLESALER_TAG, "");
                dt.Rows.Add(dr);
            }
        }
        return dt;
    }

    public DataTable LtdHelperRivalToDT()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("id");
        dt.Columns.Add("text");
        foreach (int i in Enum.GetValues(typeof(Ltd)))
        {
            string text = Enum.GetName(typeof(Ltd), i);
            if (text.IndexOf(RIVAL_TAG) != -1)
            {
                DataRow dr = dt.NewRow();
                dr["id"] = i;
                dr["text"] = text.Replace(RIVAL_TAG, "");
                dt.Rows.Add(dr);
            }
        }
        return dt;
    }


    public int LtdHelperValue(Ltd ct)
    {
        foreach (int i in Enum.GetValues(typeof(Ltd)))
        {
            if (Enum.GetName(typeof(Ltd), i) == ct.ToString())
            {
                return i;
            }
        }
        return -1;
    }

    public Ltd LtdModelByValue(int v)
    {
        try
        {
            return (Ltd)Enum.Parse(typeof(Ltd), Enum.GetName(typeof(Ltd), v));
        }
        catch (Exception ex) { throw ex; }
    }

    public string FilterText(string text)
    {
        if (text.Trim().Length > 2)
            return text.Replace(WHOLESALER_TAG, "").Replace(RIVAL_TAG, "");
        else
            return text;
    }
}


public enum Ltd
{
    lu = 1,
    wholesaler_supercom = 2,
    wholesaler_asi = 3,
    wholesaler_EPROM = 4,
    wholesaler_DAIWA = 5,
    wholesaler_MUTUAL = 6,
    wholesaler_OCZ = 7,
    wholesaler_COMTRONIX = 8,
    wholesaler_SINOTECH = 9,
    wholesaler_MINIMICRO = 10,
    wholesaler_ALC = 11,
    wholesaler_SAMTACH = 12,
    wholesaler_MMAX = 13,

    wholesaler_CanadaComputer = 15,
    wholesaler_dandh = 16,
    wholesaler_d2a  =   17,
    wholesaler_BellMicroproducts=18,
    wholesaler_Smartvision_Direct = 19,
    wholesaler_Synnex = 20,

    Rival_ETC = 100,
    Rival_Ncix = 101,
    Rival_PcvOnline = 103,
    Rival_DirectDial = 104,
    Rival_TigerDirect = 105,
    Rival_NewEgg = 106

}
