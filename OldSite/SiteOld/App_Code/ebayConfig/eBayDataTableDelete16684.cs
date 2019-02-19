using System.Data;

/// <summary>
/// Summary description for eBayDataTableDelete16684
/// </summary>
public class eBayDataTableDelete16684
{
	public eBayDataTableDelete16684()
	{
		//
		// TODO: Add constructor logic here
		//
        
	}

    public static void Delete(DataTable dt)
    {
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (dt.Rows[i]["product_serial_no"].ToString() == "16684")
            {
                dt.Rows.Remove(dt.Rows[i]);
                i -= 1;
            }
        }
        
    }

     
}