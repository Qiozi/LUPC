using System;
using System.Data;

/// <summary>
/// Summary description for CountryCategoryHelper
/// </summary>
public class CountryCategoryHelper
{
	public CountryCategoryHelper()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static DataTable CountryCategoryToDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("id");
        dt.Columns.Add("text");
        foreach (int i in Enum.GetValues(typeof(CountryCategory)))
        {
            DataRow dr = dt.NewRow();
            dr["id"] = i;
            dr["text"] = Enum.GetName(typeof(CountryCategory), i);
            dt.Rows.Add(dr);
        }
        return dt;
    }

    public static int CountryCategory_value(CountryCategory ct)
    {
        foreach (int i in Enum.GetValues(typeof(CountryCategory)))
        {
            if (Enum.GetName(typeof(CountryCategory), i) == ct.ToString())
            {
                return i;
            }
        }
        return -1;
    }

    public static CountryCategory GetCountryCategoryByValue(int id)
    {
        try
        {
            return (CountryCategory)Enum.Parse(typeof(CountryCategory), Enum.GetName(typeof(CountryCategory), id));
        }
        catch (Exception ex)
        {
             throw ex;
        }

    }

    public static string ConvertToCode(int country_id)
    {
        if (country_id == 1)
            return "CA";
        if (country_id == 2)
            return "US";
        throw new Exception("country id is error.");
    }
    public static string ConvertToCurrencyCode(CountryCategory CC)
    {
        if (CC ==  CountryCategory.CA)
            return "CAD";
        if (CC == CountryCategory.US)
            return "USD";
        throw new Exception("country id is error.");
    }

}
/// <summary>
/// 2011-08-22 
/// 删掉other=999
/// 2011-12-21 加上other=999
/// </summary>
[Serializable]
public enum CountryCategory
{
   
    CA = 1,
    US =2,
    Other = 999
}